
using AdaptiveKitCore.Requests;
using InteractiveLead.Core.Responses;

namespace InteractiveLead.Core.Interfaces.IBac
{
    public interface IQuestionBac
    {
        /// <summary>
        /// Fetch all question by request
        /// </summary>
        /// <param name="filterRequest">Filter to build the request</param>
        /// <returns>List of question</returns>
        Task<QuestionResponse> FetchAllQuestionByRequest(FilterRequest filterRequest);
    }
}
