
using AdaptiveKitCore.Requests;
using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;
using InteractiveLead.Core.Views;

namespace InteractiveLead.Core.Interfaces.IRepository
{
    public interface ILeadRepository
    {
        /// <summary>
        /// Fetch the lead list and cards infos
        /// </summary>
        /// <param name="filterRequest">Filter containing parameters to filter</param>
        /// <returns>Response containing a list of lead info</returns>
        Task<LeadInfoCardResponse> FetchLeadsInfoToCardsByRequestAsync(FilterRequest filterRequest);

        Task<LeadResponse> FetchLeadsByRequestAsync(FilterRequest filterRequest);

        Task<SimpleResponse<TotalSummary>> FetchTotalCountsAsyc(FilterRequest filterRequest);

        Task<bool> VerifyIfExistAnyLeadByRequestAsync(Lead request);

        Task<LeadResponse> RegisterLeadAsync(Lead lead);

        Task<ModelResponse> UpdateLeadByRequestAsync(Lead request, FilterRequest filterRequest);
    }
}
