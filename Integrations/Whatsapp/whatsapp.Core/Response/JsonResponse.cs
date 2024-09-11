
using AdaptiveKitCore.Responses;
using System.Text.Json.Nodes;

namespace whatsapp.Core.Response
{
    public sealed class JsonResponse : BaseResponse
    {
        public string DataJson { get; set; } = string.Empty;
    }
}
