
using MercadoLivreTeste.App.Models.MLQuestions;

namespace MercadoLivre.Core.Models.MLMessage
{
    public class MLAnswerMessage
    {
        /// <summary>
        /// Sender
        /// </summary>
        public MLMessageFrom From { get; set; } = new();

        /// <summary>
        /// Receiver
        /// </summary>
        public MLMessageFrom To { get; set; } = new();

        /// <summary>
        /// Text of the message
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }
}
