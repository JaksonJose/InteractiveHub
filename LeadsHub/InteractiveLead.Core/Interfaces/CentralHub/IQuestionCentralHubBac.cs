using InteractiveLead.Core.Models;

namespace InteractiveLead.Core.Interfaces.CentralHub
{
    public interface IQuestionCentralHubBac
    {
        Task ReceiveQuestionAnswerAsync(Question question);
    }
}
