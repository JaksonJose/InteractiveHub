
using AdaptiveKitCore.Requests;
using AdaptiveKitCore.Responses;
using Dapper;
using InteractiveLead.Core.Extentions;
using InteractiveLead.Core.Interfaces.IRepository;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;
using System.Data;
using static Dapper.SqlBuilder;

namespace InteractiveLead.Data.Repository
{
    public class ConsultantRepository : IConsultantRepository
    {
        private readonly IDbConnection _dbConnection;

        public ConsultantRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<ConsultantResponse> FetchConsultantsByRequest(FilterRequest filterRequest, int limit = 0)
        {
            ConsultantResponse response = new();

            try
            {
                string whereClause = filterRequest.BuildWhereClause();
                
                // TODO: Create the request to handle this
                // Should create Limit, pagination and sort expression dynamically
                if (limit > 0)
                {
                    whereClause += $" Order By \"{nameof(Consultant.TimeLastLeadAssigned)}\" ASC LIMIT {limit} ";
                }

                SqlBuilder builder = new();
                string querySql = string.Join(' ', "SELECT *", $"FROM \"{nameof(Consultant)}\"", $"{whereClause}");
                Template sqlTemplate = builder.AddTemplate(querySql);
                string sql = sqlTemplate.RawSql;

                IEnumerable<Consultant> result = await _dbConnection.QueryAsync<Consultant>(sql);

                response.ResponseData.AddRange(result);
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage(ex.Message);
            }

            return response;
        }

        public async Task<ModelResponse> RegisterConsultantAsync(Consultant consultant)
        {
            ModelResponse response = new();

            try
            {
                string insertConsultant = "INSERT INTO \"Consultant\" (\"FullName\", \"CompanyId\", \"PhotoUrl\", \"Active\", \"AspNetUserId\") ";
                string insertConsultant2 = $"{insertConsultant} VALUES (@FullName, @CompanyId, @PhotoUrl, @Active, @AspNetUserId) RETURNING \"Id\"";

                int result = await _dbConnection.QuerySingleAsync<int>(insertConsultant2, consultant);

                if (result == 0)
                {
                    response.AddErrorMessage("Consultant was not successfully registered");
                }

                response.AddInfoMessage("Consultant successfully registered");
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage(ex.Message);
            }

            return response;
        }

        public async Task UpdateConsultantsByRequestAsync(Consultant consultant)
        {
            try
            {
                string updateQuery = "UPDATE \"Consultant\" SET \"FullName\" = @FullName, \"CompanyId\" = @CompanyId, \"PhotoUrl\" = @PhotoUrl, \"Active\" = @Active, \"TimeLastLeadAssigned\" = @TimeLastLeadAssigned WHERE \"Id\" = @Id";

                await _dbConnection.ExecuteAsync(updateQuery, consultant);
            }
            catch(Exception ex) 
            {
            }
        }
    }
}
