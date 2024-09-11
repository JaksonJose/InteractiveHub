using AdaptiveKitCore.Requests;
using InteractiveLead.Core.Interfaces.IBac;
using InteractiveLead.Core.Interfaces.IServices;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;

namespace InteractiveLead.App.Adapters
{
    public sealed class LeadsManagerHomeAdapter
    {
        private readonly ILeadBac _leadBac;
        public LeadsManagerHomeAdapter(ILeadBac leadBac , IWhatsappService whatsappService)
        {
            _leadBac = leadBac;
        }

        /// <summary>
        /// Fetch all leads
        /// </summary>
        /// <returns>Response containing results and all leads fetched</returns>
        public async Task<LeadResponse> FetchLeadsByRequestAsync(FilterRequest filterRequest, bool isLeadList = false)
        {
            LeadResponse response = await _leadBac.FetchLeadsByRequestAsync(filterRequest, isLeadList);

            return response;
        }

        /// <summary>
        /// Update Lead status
        /// Ex.: New, InProgress, Scheduled, Closed, Refused
        /// </summary>
        /// <param name="leadId">The id of the lead</param>
        /// <param name="statusUpdate">Status to be updated</param>
        /// <returns></returns>
        public async Task UpdateLeadStatusAsync(Lead lead)
        {
            await _leadBac.UpdateLeadByRequestAsync(lead);
        }
    }
}
