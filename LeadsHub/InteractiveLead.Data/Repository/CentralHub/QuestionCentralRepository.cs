
using Dapper;
using InteractiveLead.Core.Interfaces.CentralHub;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Utility;
using Npgsql;
using static Dapper.SqlMapper;

namespace InteractiveLead.Data.Repository.CentralHub
{
    public class QuestionCentralRepository : IQuestionCentralHubRepository
    {
        public async Task<Question> FetchQuestionAsync(Question question)
        {
            Question? response = new();

            using var connection = new NpgsqlConnection(SD.ConnectString);
            connection.Open();

            string questionQuery = "SELECT * FROM Question WHERE PortalQuestionId = @PortalQuestionId;";
            string answerQuery = "SELECT * FROM Answer WHERE PortalQuestionId = @PortalQuestionId;";
            string query = questionQuery + " " + answerQuery;

            try
            {
                GridReader gridReader = await connection.QueryMultipleAsync(query, question);

                var readQuestion = await gridReader.ReadAsync<Question>();
                var readAnswer = await gridReader.ReadAsync<Answer>();

                Question? questionResult = readQuestion.FirstOrDefault();
                Answer? answerResult = readAnswer.FirstOrDefault();

                response = questionResult;

                if (response != null)
                {
                    response.Answer = answerResult;
                }                
            }
            catch (Exception ex)
            {
                //TODO: must implement log.
            }

            return response;
        }

        public async Task<long> RegisterQuestionAsync(Question question)
        {
            long response = 0;

            using var connection = new NpgsqlConnection(SD.ConnectString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                string query = "INSERT INTO Question (PortalQuestionId, CompanyId, ConsultantId, BuyerId, ItemId, Message, DateCreated, ChannelType) " +
                                "VALUES (@PortalQuestionId, @CompanyId, @ConsultantId, @BuyerId, @ItemId, @Message, @DateCreated, @ChannelType) RETURNING Id";

                response = await connection.QuerySingleAsync<long>(query, question);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                //TODO: must implement log.
                transaction.Rollback();
            }

            return response;
        }

        public async Task RegisterAnswerAsync(Answer answer)
        {
            using var connection = new NpgsqlConnection(SD.ConnectString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                string query = "INSERT INTO Answer (QuestionId, PortalQuestionId, Message, DateCreated) " +
                                "VALUES (@QuestionId, @PortalQuestionId, @Message, @DateCreated)";

                int result = await connection.ExecuteAsync(query, answer);

                transaction.Commit();
            }
            catch
            {
                //TODO: must implement log.
                transaction.Rollback();
            }
        }

        public async Task<long> VerifyQuestionExistAsync(Question question)
        {
            long result = 0;

            using var connection = new NpgsqlConnection(SD.ConnectString);
            await connection.OpenAsync();

            string query = $"SELECT Id FROM Question WHERE CompanyId = @CompanyId AND PortalQuestionId = @PortalQuestionId";

            try
            {
                result = await connection.QueryFirstOrDefaultAsync<long>(query, question);
            }
            catch (Exception ex)
            {
                // Should register a log
            }

            return result;
        }
    }
}
