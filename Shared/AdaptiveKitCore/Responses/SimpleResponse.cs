
using AdaptiveKitCore.Responses;

namespace AdaptiveKitCore.Responses
{
    /// <summary>
    /// Simple response containing a model or only a generic response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleResponse<T> : BaseResponse
    {
        public SimpleResponse()
        {            
        }

        public SimpleResponse(T model)
        {
            Model = model;
        }

        public T? Model { get; set; } = default;
    }
}
