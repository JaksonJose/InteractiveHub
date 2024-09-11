using CrossCutting.Models;

namespace InteractiveLead.Core.Interfaces.CentralHub
{
    public interface ILeadHubCentralBac
    {
        Task ReceiveMessagerToChatAsync(LeadMessage leadMessage);
    }
}
