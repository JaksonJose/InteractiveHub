using AdaptiveKitCore.Responses;
using InteractiveLead.Core.Enums;
using InteractiveLead.Core.Interfaces.CentralHub;
using InteractiveLead.Core.Interfaces.IBac;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.Responses;

namespace InteractiveLead.Core.Bac.CentralHub
{
    public class QuestionHubCentral : IQuestionCentralHubBac
    {
        private readonly IDistribuitionBac _distribuitionBac;
        private readonly IQuestionCentralHubRepository _questionRepository;

        public QuestionHubCentral(
            IDistribuitionBac distribuitionBac,
            IQuestionCentralHubRepository questionRepository)
        {
            _distribuitionBac = distribuitionBac;
            _questionRepository = questionRepository;
        }

        public async Task ReceiveQuestionAnswerAsync(Question question)
        {
            // Verifies if question alread exist
            var result = await _questionRepository.FetchQuestionAsync(question);

            if (result == null)
            {
                SimpleResponse<Consultant> consultantResponse = await _distribuitionBac.DistributeLeadsSequencialAsync(question.CompanyId);
                if (consultantResponse.HasErrorMessage || consultantResponse.HasExcptionMessage)
                {
                    // TODO: must implement a log here
                }

                Consultant consultant = consultantResponse.Model!;
                question.ConsultantId = consultant.Id;

                question.Id = await _questionRepository.RegisterQuestionAsync(question);
            }

            if (question.Answer != null && result?.Answer == null)
            {
                question.Answer.QuestionId = question.Id > 0 ? question.Id : result!.Id;

                await _questionRepository.RegisterAnswerAsync(question.Answer);
            }
        }
    }
}