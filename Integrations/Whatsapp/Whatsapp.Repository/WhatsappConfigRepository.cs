
using Dapper;
using Whatsapp.Core.Interfaces.IRepository;
using Whatsapp.Core.Models;
using System.Data;

namespace Whatsapp.Data.Repository
{
    public class WhatsappConfigRepository : IWhatsappConfigRepository
    {

        private readonly IDbConnection _dbConnection;

        public WhatsappConfigRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<WhatsappConfig> FetchWhatsappConfigByPhoneNumberIdAsync(string phoneNumberId)
        {
            WhatsappConfig response = new();

            try
            {
                var result = await _dbConnection.QueryAsync<WhatsappConfig>($"SELECT * FROM WhatsappConfig WHERE PhoneNumberId = '{phoneNumberId}'");

                response = result.ToList().FirstOrDefault() ?? new();
            }
            catch (Exception ex)
            {

            }

            return response;
        }

        public async Task<WhatsappConfig> FetchWhatsappConfigByComapnyIdAsync(long companyId)
        {
            WhatsappConfig response = new();

            try
            {
                var result = await _dbConnection.QueryAsync<WhatsappConfig>($"SELECT * FROM WhatsappConfig WHERE CompanyId = '{companyId}'");

                response = result.ToList().FirstOrDefault() ?? new();
            }
            catch (Exception ex)
            {

            }

            return response;
        }
    }
}
