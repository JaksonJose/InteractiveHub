
using AdaptiveKitCore.Enums;
using AdaptiveKitCore.Requests;
using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Interfaces.IBac;
using InteractiveLead.Core.Interfaces.IRepository;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;
using InteractiveLead.Core.Views;

namespace InteractiveLead.Core.Bac
{
    /// <summary>
    /// This represents Lead Business Logic
    /// </summary>
    public sealed class LeadBac : ILeadBac
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IChatMessageRepository _chatMessageRepository;

        public LeadBac(ILeadRepository leadRepository, IChatMessageRepository chatMessageRepository)
        {
            _leadRepository = leadRepository;
            _chatMessageRepository = chatMessageRepository;
        }

        /// <summary>
        /// Fetch leads
        /// </summary>
        /// <returns>Response contaning the leads</returns>
        public async Task<LeadResponse> FetchLeadsByRequestAsync(FilterRequest filterRequest, bool isLeadList = false)
        {
            LeadResponse response = await _leadRepository.FetchLeadsByRequestAsync(filterRequest);

            return response;
        }

        /// <summary>
        /// Fetch the lead list and cards infos
        /// </summary>
        /// <param name="filterRequest">Filter containing parameters to filter</param>
        /// <returns>Response containing a list of lead info</returns>
        public async Task<LeadInfoCardResponse> FetchLeadsInfoToCardsByRequestAsync(FilterRequest filterRequest)
        {
            LeadInfoCardResponse response = await _leadRepository.FetchLeadsInfoToCardsByRequestAsync(filterRequest);

            return response;
        }

        public async Task<SimpleResponse<TotalSummary>> FetchTotalCountsAsyc(FilterRequest filterRequest)
        {
            return await _leadRepository.FetchTotalCountsAsyc(filterRequest);
        }

        public async Task<ModelResponse> UpdateLeadByRequestAsync(Lead lead)
        {
            FilterRequest filterRequest = new();
            filterRequest.AddFilter(nameof(Lead.Id), FilterOperatorEnum.EqualTo, lead.Id, "ld");

            ModelResponse response = await _leadRepository.UpdateLeadByRequestAsync(lead, filterRequest);
            if (response.HasExcptionMessage)
            {
                response.AddExceptionMessage(response.Messages[0].MessageText);
                return response;
            }

            return response;
        }
    }
}
