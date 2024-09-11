
using AdaptiveKitCore.Requests;
using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;

namespace InteractiveLead.Core.Interfaces.IBac
{
    public interface IConsultantBac
    {
        Task<ConsultantResponse> FetchConsultantsByRequestAsync(FilterRequest filterRequest, int limit = 0);

        Task<ModelResponse> RegisterConsultantAsync(Consultant consultant);

        Task UpdateConsultantsByRequestAsync(Consultant consultant);
    }
}
