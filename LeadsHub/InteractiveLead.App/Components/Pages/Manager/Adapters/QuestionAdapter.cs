using AdaptiveKitCore.Requests;
using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Interfaces.IBac;
using InteractiveLead.Core.Responses;
using InteractiveLead.Core.Views;

namespace InteractiveLead.App.Components.Pages.Manager.Adapters
{
    public class QuestionAdapter
    {
        private readonly IQuestionBac _questionBac;

        public QuestionAdapter(IQuestionBac questionBac)
        {
            _questionBac = questionBac;
        }

        public async Task<QuestionResponse> FetchAllQuestionByRequest(FilterRequest filterRequest)
        {
            var response = await _questionBac.FetchAllQuestionByRequest(filterRequest);

            return response;
        }
    }
}
