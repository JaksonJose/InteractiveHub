

using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Models;

namespace InteractiveLead.Core.Interfaces.IBac
{
    public interface IDistribuitionBac
    {
        Task<SimpleResponse<Consultant>> DistributeLeadsSequencialAsync(long companyId);
    }
}
