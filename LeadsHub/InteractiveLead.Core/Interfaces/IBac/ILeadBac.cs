
using AdaptiveKitCore.Requests;
using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;
using InteractiveLead.Core.Views;

namespace InteractiveLead.Core.Interfaces.IBac
{
    public interface ILeadBac
    {
        /// <summary>
        /// Fetch the lead list and cards infos
        /// </summary>
        /// <param name="filterRequest">Filter containing parameters to filter</param>
        /// <returns>Response containing a list of lead info</returns>
        Task<LeadInfoCardResponse> FetchLeadsInfoToCardsByRequestAsync(FilterRequest filterRequest);

        Task<LeadResponse> FetchLeadsByRequestAsync(FilterRequest filterRequest, bool isLeadList = false);

        Task<SimpleResponse<TotalSummary>> FetchTotalCountsAsyc(FilterRequest filterRequest);

        Task<ModelResponse> UpdateLeadByRequestAsync(Lead lead);
    }
}
