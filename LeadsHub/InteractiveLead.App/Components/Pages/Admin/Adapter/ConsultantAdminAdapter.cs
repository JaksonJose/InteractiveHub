using AdaptiveKitCore.Requests;
using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Interfaces.IBac;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;

namespace InteractiveLead.App.Components.Pages.Admin.Adapter
{
    public sealed class ConsultantAdminAdapter
    {
        private readonly IConsultantBac _consultantBac;

        public ConsultantAdminAdapter(IConsultantBac consultantBac)
        {
            _consultantBac = consultantBac;
        }

        public async Task<ConsultantResponse> FetchConsultantsByRequestAsync(FilterRequest filterRequest)
        {
            return await _consultantBac.FetchConsultantsByRequestAsync(filterRequest);
        }

        public async Task<ModelResponse> RegisterConsultantAsync(Consultant consultant)
        {
            return await _consultantBac.RegisterConsultantAsync(consultant);
        }
    }
}
