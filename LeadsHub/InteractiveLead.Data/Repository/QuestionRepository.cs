
using AdaptiveKitCore.Requests;
using Dapper;
using InteractiveLead.Core.Extentions;
using InteractiveLead.Core.Interfaces.IRepository;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;
using static Dapper.SqlBuilder;
using System.Collections.Generic;
using System.Data;

namespace InteractiveLead.Data.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IDbConnection _dbConnection;

        public QuestionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        /// <summary>
        /// Fetch all question by request
        /// </summary>
        /// <param name="filterRequest">Filter to build the request</param>
        /// <returns>List of question</returns>
        public async Task<QuestionResponse> FetchAllQuestionByRequest(FilterRequest filterRequest)
        {
            QuestionResponse response = new();

            string questionSelect = "SELECT * FROM Question q LEFT JOIN Answer ar ON ar.QuestionId = q.Id";
            string questionCount = "SELECT COUNT(*) FROM Question q LEDR JOIN Answer ar ON ar.QuestionId = q.Id";

            try
            {
                string WhereClause = filterRequest.BuildWhereClause();

                string sortExpression = string.Empty;
                foreach (var sort in filterRequest.SortExpressions)
                {
                    sortExpression = $"ORDER BY {sort.TableAlias}.{sort.PropertyName} {sort.SortDirection}";
                }

                string offset = "";
                if (filterRequest.PageSize > 0)
                {
                    offset += $"OFFSET {filterRequest.Skip} ROWS FETCH NEXT {filterRequest.PageSize} ROW ONLY;";
                }

                SqlBuilder builder = new();
                string querySql = string.Join(' ', questionSelect, WhereClause, sortExpression, offset);
                Template sqlTemplate = builder.AddTemplate(querySql);
                string sql = sqlTemplate.RawSql;

                string querySqlCount = string.Join(' ', questionCount, WhereClause);

                IEnumerable<Question> result = await _dbConnection.QueryAsync<Question, Consultant, Question>(sql, (question, consultant) =>
                {
                    question.Consultant = consultant;

                    return question;
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
    }
}
