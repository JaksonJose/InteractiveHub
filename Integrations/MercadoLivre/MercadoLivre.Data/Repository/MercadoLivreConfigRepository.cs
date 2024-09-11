
using Dapper;
using MercadoLivre.Core.Interface.IRepository;
using MercadoLivre.Core.Models;
using System.Data;

namespace MercadoLivre.Data.Repository
{
    public sealed class MercadoLivreConfigRepository : IMercadoLivreRepository
    {
        #region Sql statements
        private const string _updateConfig = "UPDATE MercadoLivreConfig SET " +
                                             "AccessToken = @AccessToken, " +
                                             "RefreshToken = @RefreshToken, " +
                                             "LastUpdateToken = @LastUpdateToken " +
                                             "WHERE Id = @Id";
        #endregion

        private readonly IDbConnection _dbConnection;

        public MercadoLivreConfigRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<MercadoLivreConfig> FetchMercadoLivreConfig(long applicationId)
        {
            MercadoLivreConfig response = new();

            try
            {
                string query = $"SELECT * FROM MercadoLivreConfig WHERE ApplicationId = {applicationId}";

                response = await _dbConnection.QueryFirstAsync<MercadoLivreConfig>(query);
            }
            catch 
            { 
                // should implement a error log here
            }

            return response;
        }

        public async Task<MercadoLivreConfig> FetchMercadoLivreConfigByCompany(long companyId)
        {
            MercadoLivreConfig response = new();

            try
            {
                string query = $"SELECT * FROM MercadoLivreConfig WHERE CompanyId = {companyId}";

                response = await _dbConnection.QueryFirstAsync<MercadoLivreConfig>(query);
            }
            catch
            {
                // should implement a error log here
            }

            return response;
        }

        /// <summary>
        /// Update the token refreshed
        /// </summary>
        /// <param name="mercadoLivreConfig">Mercado Livre configuration updated</param>
        /// <returns></returns>
        public async Task<int> UpdateRefreshTokenAsync(MercadoLivreConfig mercadoLivreConfig)
        {
            int response = 0;

            try
            {
                response = await _dbConnection.ExecuteAsync(_updateConfig, mercadoLivreConfig);
            }
            catch (Exception ex)
            {
                // should implement a erro log here
            }

            return response;
        }
    }
}
