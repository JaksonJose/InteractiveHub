
using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Interfaces.CentralHub;
using InteractiveLead.Core.Interfaces.IBac;
using InteractiveLead.Core.Models;


namespace InteractiveLead.Core.Bac.CentralHub
{
    public class DistribuitionBac : IDistribuitionBac
    {
        private readonly ILeadCentralHubRepository _leadCentralRepository;

        public DistribuitionBac(ILeadCentralHubRepository leadCentralRepository)
        {
            _leadCentralRepository = leadCentralRepository;
        }

        /// <summary>
        /// Automatically finds the next consultant for assigning to the lead
        /// </summary>
        /// <param name="companyId">CompanyId to find the consuntants of the company</param>
        /// <returns>The consultant to be assigned</returns>
        public async Task<SimpleResponse<Consultant>> DistributeLeadsSequencialAsync(long companyId) // TODO: Move it to class to share it.
        {
            // Should verify the company configuration
            /*
             * Queue (Sequential) distribution
             * Less lead distribuition
             * Close lead distribuition (it is not trigged here)
             * Manual distribuition (Manager should choose who will attend the lead)
             */

            string sequencialRuleQuery = $"SELECT * FROM sequencial_next_consultant({companyId})";

            SimpleResponse<Consultant> consultantResponse = await _leadCentralRepository.FetchNextConsultant(sequencialRuleQuery);
            if (consultantResponse.HasErrorMessage || consultantResponse.HasExcptionMessage)
            {
                // Register a log
            }

            return consultantResponse;
        }
    }
}
