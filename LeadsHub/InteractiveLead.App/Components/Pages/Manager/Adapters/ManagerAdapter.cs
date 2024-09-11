
using AdaptiveKitCore.Requests;
using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Interfaces.IBac;
using InteractiveLead.Core.Responses;
using InteractiveLead.Core.Views;

namespace InteractiveLead.App.Pages.Manager.Adapter
{
    public class ManagerAdapter
    {
        private readonly ILeadBac _leadBac;

        public ManagerAdapter(ILeadBac leadBac)
        {
            _leadBac = leadBac;
        }

        public async Task<SimpleResponse<TotalSummary>> FetchTotalCountsAsyc(FilterRequest filterRequest)
        {
            return await _leadBac.FetchTotalCountsAsyc(filterRequest);
        }
    }
}
