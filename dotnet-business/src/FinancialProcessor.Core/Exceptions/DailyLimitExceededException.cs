namespace FinancialProcessor.Core.Exceptions
{
    internal class DailyLimitExceededException : DomainException
    {
        public Guid AccountId { get; init; }
        public decimal AttemptedAmount { get; init; }
        public decimal DailyLimit { get; init; }
        public decimal AlreadyUsedToday { get; init; }
        public decimal RemainingLimit => DailyLimit - AlreadyUsedToday;

        public DailyLimitExceededException(
            Guid accountId,
            decimal attemptedAmount,
            decimal dailyLimit,
            decimal alreadyUsedToday,
            string? accountNumber = null,
            Exception? innerException = null)
        : base(
            errorCode: "DAILY_LIMIT_EXCEEDED",
            userMessage: $"Daily limit in account {accountNumber ?? accountId.ToString()}. " +
                        $"Daily limit: {dailyLimit:C}, attempted amount: {attemptedAmount:C}, " +
                        $"already used today: {alreadyUsedToday:C}, remaining limit {dailyLimit - alreadyUsedToday}",
            technicalDetails: $"AccountId: {accountId}, Daily limit: {dailyLimit: C}, Attempted amount: {attemptedAmount:C}" +
                        $"Already used today: {alreadyUsedToday:C}, Remaining limit {dailyLimit - alreadyUsedToday}",
            innerException: innerException)
        {
            AccountId = accountId;
            AttemptedAmount = attemptedAmount;
            DailyLimit = dailyLimit;
            AlreadyUsedToday = alreadyUsedToday;
        }
    }
}
