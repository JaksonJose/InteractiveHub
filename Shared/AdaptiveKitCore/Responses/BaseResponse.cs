
using AdaptiveKitCore.Enums;
using AdaptiveKitCore.Model;

namespace AdaptiveKitCore.Responses
{
    /// <summary>
    /// Base response 
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// A collection of messages related to the response.
        /// </summary>
        public List<Message> Messages { get; set; } = [];

        /// <summary>
        /// Validates if has any error message
        /// </summary>
        public bool HasErrorMessage { get { return HasMessageType(MessageTypeEnum.Error); } }

        /// <summary>
        /// Validates if has any exception message
        /// </summary>
        public bool HasExcptionMessage { get { return HasMessageType(MessageTypeEnum.Exception); } }

        /// <summary>
        /// Validates if has any info message
        /// </summary>
        public bool HasInfoMessage { get { return HasMessageType(MessageTypeEnum.Info); } }

        /// <summary>
        /// Validate if has any warning message
        /// </summary>
        public bool HasWarningMessage { get { return HasMessageType(MessageTypeEnum.Info); } }

        /// <summary>
        /// Collection of error message
        /// </summary>
        /// <param name="text">Text of error message</param>
        /// <returns>Collection of error message</returns>
        public BaseResponse AddErrorMessage(string text)
        {
            this.Messages.Add(new Message(MessageTypeEnum.Error, text));

            return this;
        }

        /// <summary>
        /// Collection of exception message
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public BaseResponse AddExceptionMessage(string text)
        {
            this.Messages.Add(new Message(MessageTypeEnum.Exception, text));

            return this;
        }

        /// <summary>
        /// Collection of warning message
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public BaseResponse AddWarningMessage(string text)
        {
            this.Messages.Add(new Message(MessageTypeEnum.Warning, text));

            return this;
        }

        /// <summary>
        /// Collection of informational message
        /// </summary>
        /// <param name="text">Text of the informational message</param>
        /// <returns>Collection of info message</returns>
        public BaseResponse AddInfoMessage(string text)
        {
            this.Messages.Add(new Message(MessageTypeEnum.Info, text));

            return this;
        }

        private bool HasMessageType(MessageTypeEnum messageType)
        {
            return Messages.Any(item => item.MessageType == messageType);
        }
    }
}
