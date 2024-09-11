
using AdaptiveKitCore.Requests;
using InteractiveLead.Core.Responses;

namespace InteractiveLead.Core.Interfaces.IRepository
{
    public interface IQuestionRepository
    {
        /// <summary>
        /// Fetch all question by request
        /// </summary>
        /// <param name="filterRequest">Filter to build the request</param>
        /// <returns>List of question</returns>
        Task<QuestionResponse> FetchAllQuestionByRequest(FilterRequest filterRequest);
    }
}
