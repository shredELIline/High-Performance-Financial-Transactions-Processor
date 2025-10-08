using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialProcessor.Core.Exceptions
{
    internal class DomainException : Exception
    {
        public string ErrorCode { get; init; }
        public string UserMessage { get; init; }
        public string TechnicalDetails { get; init; }

        public DomainException(string userMessage) : base(userMessage) 
        { 
            this.UserMessage = userMessage;
        }

        public DomainException(
            string userMessage, 
            Exception? innerException = null) 
        : base(
            userMessage, 
            innerException) 
        { 
            this.UserMessage = userMessage;
        }

        public DomainException(
            string errorCode, 
            string userMessage, 
            Exception? innerException = null) 
        : base(
            $"{errorCode}: {userMessage}", 
            innerException) 
        { 
            this.ErrorCode = errorCode;
            this.UserMessage = userMessage;
        }

        public DomainException(
            string errorCode, 
            string userMessage, 
            string techincalDetails, 
            Exception? innerException = null) 
        : base(
            $"{errorCode}: {userMessage}, {techincalDetails}", 
            innerException)
        {
            this.ErrorCode = errorCode;
            this.UserMessage = userMessage;
            this.TechnicalDetails = techincalDetails;
        }
    }
}
