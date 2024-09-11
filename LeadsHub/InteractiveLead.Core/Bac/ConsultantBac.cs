
using AdaptiveKitCore.Requests;
using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Interfaces.IBac;
using InteractiveLead.Core.Interfaces.IRepository;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;

namespace InteractiveLead.Core.Bac
{
    public class ConsultantBac : IConsultantBac
    {
        private readonly IConsultantRepository _consultantRepository;

        public ConsultantBac(IConsultantRepository consultantRepository)
        {
            _consultantRepository = consultantRepository;
        }

        public async Task<ConsultantResponse> FetchConsultantsByRequestAsync(FilterRequest filterRequest, int limit = 0)
        {
            var response = await _consultantRepository.FetchConsultantsByRequest(filterRequest, limit);

            return response;
        }

        public async Task<ModelResponse> RegisterConsultantAsync(Consultant consultant)
        {
            return await _consultantRepository.RegisterConsultantAsync(consultant);
        }

        public async Task UpdateConsultantsByRequestAsync(Consultant consultant)
        {
            await _consultantRepository.UpdateConsultantsByRequestAsync(consultant);
        }
    }
}
