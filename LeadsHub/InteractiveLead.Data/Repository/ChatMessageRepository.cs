
using AdaptiveKitCore.Requests;
using AdaptiveKitCore.Responses;
using Dapper;
using InteractiveLead.Core.Interfaces.IRepository;
using InteractiveLead.Core.Extentions;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;
using System.Data;
using static Dapper.SqlBuilder;

namespace InteractiveLead.Data.Repository
{
    /// <summary>
    /// This represents Chat Message Repository.
    /// It should contains only operations about data context.
    /// </summary>
    public sealed class ChatMessageRepository : IChatMessageRepository
    {
        #region sql statements
        // This is temporary, I wish doing it dynamically
        private const string chatMessageSelect = "SELECT \"Id\", "
                                                 + "\"LeadId\", "
                                                 + "\"ConsultantId\", "
                                                 + "\"MessageBody\", "
                                                 + "\"MessageDate\", "
                                                 + "\"MessageType\", "
                                                 + "\"MessageSender\", "
                                                 + "\"MessageStatus\"";

        private const string newMessageCountSelect = "SELECT \"LeadId\", "
                                                     + "COUNT(*) AS \"TotalNewMessages\" "
                                                     + "FROM \"ChatMessage\" " 
                                                     + "WHERE \"LeadId\" IN (@leadIds) "
                                                     + "AND \"MessageStatus\" = 'New' "
                                                     + "AND \"MessageSender\" = 'Customer' "
                                                     + "GROUP BY \"LeadId\";";

        #endregion

        private readonly IDbConnection _dbConnection;

        public ChatMessageRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<ChatMessageResponse> FetchChatMessagesByRequestAsync(FilterRequest filterRequest)
        {
            ChatMessageResponse response = new();

            try
            {
                string WhereClause = filterRequest.BuildWhereClause();               

                SqlBuilder builder = new();
                string querySql = string.Join(' ', chatMessageSelect, $"FROM \"{nameof(ChatMessage)}\"", $"{WhereClause}");
                Template sqlTemplate = builder.AddTemplate(querySql);
                string sql = sqlTemplate.RawSql;

                IEnumerable<ChatMessage> result = await _dbConnection.QueryAsync<ChatMessage>(sql);

                response.ResponseData.AddRange(result);
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage(ex.Message);
            }            

            return response;
        }       
               
        public async Task<SimpleResponse<ChatMessage>> RegisterChatMessageAsync(ChatMessage chatMessage)
        {
            SimpleResponse<ChatMessage> response = new();

            try
            {
                string insertChatMessage = "INSERT INTO \"ChatMessage\" (\"LeadId\", \"ConsultantId\", \"MessageBody\", \"MessageType\", \"MessageSender\", \"MessageStatus\", \"MessageDate\") ";                
                string insertChatMessage2 = $"{insertChatMessage} VALUES (@LeadId, @ConsultantId, @MessageBody, @MessageType, @MessageSender, @MessageStatus, @MessageDate) RETURNING \"Id\" ";

                int result = await _dbConnection.QuerySingleAsync<int>(insertChatMessage2, chatMessage);

                chatMessage.Id = result;
                response.Model = chatMessage;
            }
            catch (Exception ex)
            {
                // TODO: implement log
                response.AddExceptionMessage(ex.Message);
            }

            return response;
        }

        public async Task UpdateMessageByRequestAsync(ChatMessage chatMessage)
        {
            string updateQuery = "UPDATE \"ChatMessage\" SET \"LeadId\" = @LeadId, \"MessageBody\" = @MessageBody, \"MessageType\" = @MessageType, \"MessageSender\" = @MessageSender, \"MessageStatus\" = @MessageStatus WHERE \"Id\" = @Id";
            
            try
            {
                var result = await _dbConnection.ExecuteAsync(updateQuery, chatMessage);
            }
            catch (Exception ex)
            {
                // implement error to logs
            }
        }

        /// <summary>
        /// Set messages as read
        /// </summary>
        /// <param name="messageIds">Ids of messages to set as read</param>
        /// <returns></returns>
        public async Task SetMessageAsReadAsync(IEnumerable<long> messageIds)
        {
            try
            {
                string updateQuery = "UPDATE \"ChatMessage\" SET \"MessageStatus\" = 'Read' WHERE \"Id\" IN (@messageIds)";

                string idsToString = string.Join(", ", messageIds);
                updateQuery = updateQuery.Replace("@messageIds", idsToString);

                var result = await _dbConnection.ExecuteAsync(updateQuery);
            }
            catch (Exception ex)
            {
                // implement error to logs
            }
        }
    }
}
