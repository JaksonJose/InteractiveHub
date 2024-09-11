using InteractiveLead.Core.Models;

namespace InteractiveLead.Core.Interfaces.CentralHub
{
    public interface IQuestionCentralHubRepository
    {
        Task<Question> FetchQuestionAsync(Question question);

        Task<long> RegisterQuestionAsync(Question question);

        Task<long> VerifyQuestionExistAsync(Question question);

        Task RegisterAnswerAsync(Answer answer);
    }
}
