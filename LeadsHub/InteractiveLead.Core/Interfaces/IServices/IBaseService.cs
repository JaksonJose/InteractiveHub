
using AdaptiveKitCore.Responses;

namespace InteractiveLead.Core.Interfaces.IServices
{
    public interface IBaseService
    {
        Task<BaseResponse> SendMessageAsync(string messageSerielized, string uri);
    }
}
