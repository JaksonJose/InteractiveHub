using InteractiveLead.Core.Models;

namespace InteractiveLead.Core.Responses
{
    /// <summary>
    /// It represents a Response. 
    /// It helps easily delivery response throught method signatures and http bodies and headers
    /// It should contain only parameters and methods that define a response.
    /// </summary>
    public sealed class ChatMessageResponse : BaseResponse<ChatMessage>
    {
    }
}
