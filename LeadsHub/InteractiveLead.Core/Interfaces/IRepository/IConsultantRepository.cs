
using AdaptiveKitCore.Requests;
using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;

namespace InteractiveLead.Core.Interfaces.IRepository
{
    public interface IConsultantRepository
    {
        Task<ConsultantResponse> FetchConsultantsByRequest(FilterRequest filterRequest, int limit = 0);

        Task<ModelResponse> RegisterConsultantAsync(Consultant consultant);

        Task UpdateConsultantsByRequestAsync(Consultant consultant);
    }
}
