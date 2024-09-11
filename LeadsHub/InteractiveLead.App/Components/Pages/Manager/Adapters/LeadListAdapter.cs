using AdaptiveKitCore.Requests;
using InteractiveLead.Core.Interfaces.IBac;
using InteractiveLead.Core.Responses;

namespace InteractiveLead.App.Components.Pages.Manager.Adapters
{
    public sealed class LeadListAdapter
    {
        private readonly ILeadBac _leadBac;

        public LeadListAdapter(ILeadBac leadBac)
        {
            _leadBac = leadBac;
        }

        public async Task<LeadInfoCardResponse> FetchLeadsInfoToCardsByRequestAsync(FilterRequest filterRequest)
        {
            LeadInfoCardResponse response = await _leadBac.FetchLeadsInfoToCardsByRequestAsync(filterRequest);

            return response;
        }
    }
}
