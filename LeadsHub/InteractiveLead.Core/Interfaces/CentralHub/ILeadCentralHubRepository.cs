
using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;

namespace InteractiveLead.Core.Interfaces.CentralHub
{
    public interface ILeadCentralHubRepository
    {
        /// <summary>
        /// Find next lead according to the distribuition rule
        /// </summary>
        /// <param name="functionQuery"></param>
        /// <returns>Consultant to be assigned in the lead</returns>
        Task<SimpleResponse<Consultant>> FetchNextConsultant(string functionQuery);

        /// <summary>
        /// Register the lead
        /// </summary>
        /// <param name="lead">Lead to be registered</param>
        /// <returns></returns>
        Task<SimpleResponse<Lead>> RegisterLeadAsync(Lead lead);

        Task UpdateConsultantsByRequestAsync(Consultant consultant);

        /// <summary>
        /// Verifies whethe lead exists and return its id
        /// </summary>
        /// <param name="lead">Lead to be verified</param>
        /// <returns>Return Existing Lead</returns>
        Task<Lead> VerifyLeadExistAsync(Lead lead);
    }
}
