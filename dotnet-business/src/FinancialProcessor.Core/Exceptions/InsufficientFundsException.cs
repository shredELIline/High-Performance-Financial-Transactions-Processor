namespace FinancialProcessor.Core.Exceptions
{
    internal class InsufficientFundsException : DomainException
    {
        public Guid AccountId { get; init; }
        public decimal CurrentBalance { get; init; }
        public decimal RequestedAmount { get; init; }
        public decimal ShortageAmount => CurrentBalance - RequestedAmount;

        public InsufficientFundsException(
            Guid accountId,
            decimal currentBalance,
            decimal requestedAmount,
            string? accountNumber = null,
            Exception? innerException = null)
        : base(
            errorCode: "INSUFFICIENT_FUNDS",
            userMessage: $"Insufficient funds in account {accountNumber ?? accountId.ToString()}. " +
                        $"Current balance: {currentBalance:C}, requested: {requestedAmount:C}",
            technicalDetails: $"AccountId: {accountId}, Balance: {currentBalance}, " +
                             $"Requested: {requestedAmount}, Shortage: {requestedAmount - currentBalance}",
            innerException: innerException)
        {
            AccountId = accountId;
            CurrentBalance = currentBalance;
            RequestedAmount = requestedAmount;
        }
    }
}
