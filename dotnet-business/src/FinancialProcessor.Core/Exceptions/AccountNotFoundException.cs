using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialProcessor.Core.Exceptions
{
    internal class AccountNotFoundException : DomainException
    {
        public Guid AccountId { get; init; }

        public AccountNotFoundException(
            Guid accountId,
            string? accountNumber = null,
            Exception? innerException = null)
        : base(
            errorCode: "ACOUNT_NOT_FOUND",
            userMessage: $"Acount {accountNumber ?? accountId.ToString()} not found. ",
            techincalDetails: $"AccountId: {accountId}",
            innerException: innerException)
        {
            AccountId = accountId;
        }
    }
}
