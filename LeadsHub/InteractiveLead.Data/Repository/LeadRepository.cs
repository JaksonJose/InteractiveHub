
using AdaptiveKitCore.Requests;
using AdaptiveKitCore.Responses;
using Dapper;
using InteractiveLead.Core.Extentions;
using InteractiveLead.Core.Interfaces.IRepository;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;
using InteractiveLead.Core.Views;
using System.Data;
using static Dapper.SqlBuilder;

namespace InteractiveLead.Data.Repository
{
    /// <summary>
    /// This represents Lead Repository.
    /// It should contains only operations about data context.
    /// </summary>
    public sealed class LeadRepository : ILeadRepository
    {
        #region Sql
        private readonly string selectSummaryCount = "SELECT "
                                                    + "COUNT(1) FILTER (WHERE \"Status\" <> 'Canceled' OR \"Status\" <> 'Refused' OR \"Status\" <> 'Closed') AS TotalAllLeads, "
                                                    + "COUNT(1) FILTER (WHERE \"Status\" = 'New') AS TotalNewLeads, "
                                                    + "COUNT(1) FILTER (WHERE \"Status\" = 'InProgress') AS TotalInProgress, "
                                                    + "COUNT(1) FILTER (WHERE \"Status\" = 'Schedule') AS TotalScheduled "
                                                    + "FROM \"Lead\" ";

        private const string updateSql = "UPDATE \"Lead\" ld SET "
                                       + "\"CompanyId\" = @CompanyId, "
                                       + "\"ConsultantId\" = @ConsultantId, "
                                       + "\"Email\" = @Email, "
                                       + "\"Name\" = @Name, "
                                       + "\"PhoneNumber\" = @PhoneNumber, "
                                       + "\"SourceChannel\" = @SourceChannel, "
                                       + "\"Status\" = @Status ";

        private const string selectLead = "SELECT * " 
                                         + "FROM \"Lead\" ld "
                                         + "LEFT JOIN \"Consultant\" c ON c.\"Id\" = ld.\"ConsultantId\" ";

        private const string selectLeadCard = "SELECT ld.\"Id\" As LeadId, "
                                                + "ld.\"Name\" As LeadName, "
                                                + "c.\"FullName\" AS ConsultantName, "
                                                + "ld.\"CreatedAt\" AS CreatedAt, "
                                                + "COUNT(m.\"Id\") FILTER (WHERE m.\"MessageStatus\" = 'New') As TotalNewMessages, "
                                                + "MAX(m.\"MessageDate\") AS LastMessageDate "
                                            + "FROM \"Lead\" ld "
                                            + "LEFT JOIN \"Consultant\" c ON c.\"Id\" = ld.\"ConsultantId\" "
                                            + "LEFT JOIN \"ChatMessage\" m ON ld.\"Id\" = m.\"LeadId\" ";

        private const string selectLeadCount = "SELECT COUNT(*) "
                                             + "FROM \"Lead\" ld "
                                             + "LEFT JOIN \"Consultant\" c ON c.\"Id\" = ld.\"ConsultantId\" ";

        #endregion

        private readonly IDbConnection _dbConnection;

        public LeadRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        /// <summary>
        /// Fetch the lead list and cards infos
        /// </summary>
        /// <param name="filterRequest">Filter containing parameters to filter</param>
        /// <returns>Response containing a list of lead info</returns>
        public async Task<LeadInfoCardResponse> FetchLeadsInfoToCardsByRequestAsync(FilterRequest filterRequest)
        {
            LeadInfoCardResponse response = new();

            try
            {
                string WhereClause = filterRequest.BuildWhereClause();

                string groupBy = "GROUP BY ld.\"Id\", ld.\"Name\", c.\"FullName\", ld.\"CreatedAt\" ";

                string sortExpression = string.Empty;
                foreach (var sort in filterRequest.SortExpressions)
                {
                    sortExpression = $"ORDER BY {sort.PropertyName} {sort.SortDirection}";
                }

                string offset = "";
                if (filterRequest.PageSize > 0)
                {
                    offset += $"OFFSET {filterRequest.Skip} ROWS FETCH NEXT {filterRequest.PageSize} ROW ONLY;";
                }

                SqlBuilder builder = new();
                string querySql = string.Join(' ', selectLeadCard, WhereClause, groupBy, sortExpression, offset);
                Template sqlTemplate = builder.AddTemplate(querySql);
                string sql = sqlTemplate.RawSql;

                string querySqlCount = string.Join(' ', selectLeadCount, WhereClause);

                IEnumerable<LeadInfoCard> result = await _dbConnection.QueryAsync<LeadInfoCard>(sql);

                if (result.Any() && filterRequest.PageSize > 0)
                {
                    int totalCount = await _dbConnection.QueryFirstOrDefaultAsync<int>(querySqlCount);
                    response.TotalAvailableItems = totalCount;
                }

                response.ResponseData.AddRange(result);
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage(ex.Message);
            }

            return response;
        }


        public async Task<LeadResponse> FetchLeadsByRequestAsync(FilterRequest filterRequest)
        {
            LeadResponse response = new();

            try
            {
                string WhereClause = filterRequest.BuildWhereClause();

                string sortExpression = string.Empty;
                foreach(var sort in filterRequest.SortExpressions)
                {
                    sortExpression = $"ORDER BY {sort.TableAlias}.\"{sort.PropertyName}\" {sort.SortDirection}";
                }

                string offset = "";
                if (filterRequest.PageSize > 0)
                {
                    offset += $"OFFSET {filterRequest.Skip} ROWS FETCH NEXT {filterRequest.PageSize} ROW ONLY;";
                }

                SqlBuilder builder = new();               
                string querySql = string.Join(' ', selectLead, WhereClause, sortExpression, offset);
                Template sqlTemplate = builder.AddTemplate(querySql);
                string sql = sqlTemplate.RawSql;

                string querySqlCount = string.Join(' ', selectLeadCount, WhereClause);

                IEnumerable<Lead> result = await _dbConnection.QueryAsync<Lead, Consultant, Lead>(sql, (lead, consultant) => 
                {
                    lead.Consultant = consultant;

                    return lead;
                }, splitOn: "Id,Id");

                if (result.Any() && filterRequest.PageSize > 0)
                {
                    int totalCount = await _dbConnection.QueryFirstOrDefaultAsync<int>(querySqlCount);
                    response.TotalAvailableItems = totalCount;
                }
                
                response.ResponseData.AddRange(result);                
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage(ex.Message);
            }

            return response;
        }

        public async Task<SimpleResponse<TotalSummary>> FetchTotalCountsAsyc(FilterRequest filterRequest)
        {
            SimpleResponse<TotalSummary> response = new();

            try
            {
                string WhereClause = filterRequest.BuildWhereClause();

                SqlBuilder builder = new();
                string querySql = string.Join(' ', selectSummaryCount, $"{WhereClause}");
                Template sqlTemplate = builder.AddTemplate(querySql);
                string sql = sqlTemplate.RawSql;

                TotalSummary leadSummary = await _dbConnection.QueryFirstAsync<TotalSummary>(sql);

                response.Model = leadSummary;
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage(ex.Message);
            }

            return response;
        }

        public async Task<LeadResponse> RegisterLeadAsync(Lead lead)
        {
            LeadResponse response = new();

            try
            {
                string insertLead = "INSERT INTO \"Lead\" (\"Name\", \"PhoneNumber\", \"Email\", \"CompanyId\", \"ConsultantId\") ";
                string insertLead2 = $"{insertLead} VALUES (@Name, @PhoneNumber, @Email, @companyid, @ConsultantId) RETURNING \"Id\"";

                int leadid = await _dbConnection.QuerySingleAsync<int>(insertLead2, lead);
                
                lead.Id = leadid;

                response.ResponseData.Add(lead);
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage(ex.Message);
            }

            return response;
        }

        public async Task<ModelResponse> UpdateLeadByRequestAsync(Lead request, FilterRequest filterRequest)
        {
            ModelResponse response = new();

            try
            {
                string WhereClause = filterRequest.BuildWhereClause();
                string query = string.Join(" ", updateSql, WhereClause);

                int result = await _dbConnection.ExecuteAsync(query, request);
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage(ex.Message);
            }

            return response;
        }

        public async Task<bool> VerifyIfExistAnyLeadByRequestAsync(Lead request)
        {
            string query = "SELECT COUNT(*) FROM \"Lead\" WHERE \"PhoneNumber\" = @PhoneNumber And \"CompanyId\" = @CompanyId";

            int result = await _dbConnection.QueryFirstAsync<int>(query, request);

            return result >= 1;
        }
    }
}
