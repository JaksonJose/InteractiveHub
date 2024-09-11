
using AdaptiveKitCore.Responses;
using Dapper;
using Npgsql;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Utility;
using InteractiveLead.Core.Interfaces.CentralHub;

namespace InteractiveLead.Data.Repository.CentralHub
{
    public sealed class LeadCentralRepository : ILeadCentralHubRepository
    {
        public async Task<SimpleResponse<Consultant>> FetchNextConsultant(string functionQuery)
        {
            SimpleResponse<Consultant> response = new();

            using var connection = new NpgsqlConnection(SD.ConnectString);
            await connection.OpenAsync();

            try
            {
                Consultant consultant = await connection.QueryFirstAsync<Consultant>(functionQuery);

                response.Model = consultant;
            }
            catch (Exception ex)
            {
                // should register a log
                response.AddExceptionMessage(ex.Message);
            }

            return response;
        }

        public async Task<SimpleResponse<Lead>> RegisterLeadAsync(Lead lead)
        {
            SimpleResponse<Lead> response = new();

            using var connection = new NpgsqlConnection(SD.ConnectString);
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();

            try
            {
                string insertLead = "INSERT INTO \"Lead\" (\"Name\", \"PhoneNumber\", \"Email\", \"CompanyId\", \"ConsultantId\", \"Status\", \"Identifier\", \"SourceChannel\") ";
                string insertLead2 = $"{insertLead} VALUES (@Name, @PhoneNumber, @Email, @companyid, @ConsultantId, @Status, @Identifier, @SourceChannel) RETURNING \"Id\"";

                long leadid = await connection.ExecuteScalarAsync<long>(insertLead2, lead);

                lead.Id = leadid;
                response.Model = lead;

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // should store a log
                transaction.Rollback();
            }

            return response;
        }

        public async Task UpdateConsultantsByRequestAsync(Consultant consultant)
        {
            using var connection = new NpgsqlConnection(SD.ConnectString);
            await connection.OpenAsync();

            try
            {
                string updateQuery = "UPDATE \"Consultant\" SET \"FullName\" = @FullName, \"CompanyId\" = @CompanyId, \"PhotoUrl\" = @PhotoUrl, \"Active\" = @Active, \"TimeLastLeadAssigned\" = @TimeLastLeadAssigned WHERE \"Id\" = @Id";

                var result = await connection.ExecuteScalarAsync(updateQuery, consultant);
            }
            catch (Exception ex)
            {
                // should register a log
            }
        }

        /// <summary>
        /// Verifies if the lead is already persisted in the company.
        /// </summary>
        /// <param name="lead">Lead to be verified</param>
        /// <returns>The id of the lead found</returns>
        public async Task<Lead> VerifyLeadExistAsync(Lead lead)
        {
            using var connection = new NpgsqlConnection(SD.ConnectString);
            await connection.OpenAsync();

            string query = "SELECT \"Id\", \"ConsultantId\" FROM \"Lead\" WHERE \"CompanyId\" = @CompanyId AND \"PhoneNumber\" = @PhoneNumber";

            if (string.IsNullOrWhiteSpace(lead.PhoneNumber)) 
            {
                query = "SELECT \"Id\", \"ConsultantId\" FROM \"Lead\" WHERE \"CompanyId\" = @CompanyId AND \"Identifier\" = @Identifier";
            }

            try
            {
                Lead? result = await connection.QueryFirstOrDefaultAsync<Lead>(query, lead);

                lead.Id = result?.Id ?? 0;
                lead.ConsultantId = result?.ConsultantId ?? 0;
            }
            catch (Exception ex)
            {
                // Should register a log
            }

            return lead;
        }
    }
}
