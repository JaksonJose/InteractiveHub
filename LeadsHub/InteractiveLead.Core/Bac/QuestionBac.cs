
using AdaptiveKitCore.Requests;
using InteractiveLead.Core.Interfaces.IBac;
using InteractiveLead.Core.Interfaces.IRepository;
using InteractiveLead.Core.Responses;

namespace InteractiveLead.Core.Bac
{
    public sealed class QuestionBac : IQuestionBac
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionBac(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        /// <summary>
        /// Fetch all question by request
        /// </summary>
        /// <param name="filterRequest">Filter to build the request</param>
        /// <returns>List of question</returns>
        public async Task<QuestionResponse> FetchAllQuestionByRequest(FilterRequest filterRequest)
        {
            return await _questionRepository.FetchAllQuestionByRequest(filterRequest);
        }
    }
}
