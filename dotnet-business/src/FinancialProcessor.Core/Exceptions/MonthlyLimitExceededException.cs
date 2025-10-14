using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialProcessor.Core.Exceptions
{
    internal class MonthlyLimitExceededException : DomainException
    {
        public Guid AccountId { get; init; }
        public decimal AttemptedAmount { get; init; }
        public decimal MonthlyLimit { get; init; }
        public decimal AlreadyUsedThisMoth { get; init; }
        public decimal RemainingLimit => MonthlyLimit - AlreadyUsedThisMoth;

        public MonthlyLimitExceededException(
            Guid accountId,
            decimal attemptedAmount,
            decimal monthlyLimit,
            decimal alreadyUsedThisMonth,
            string? accountNumber = null,
            Exception? innerException = null)
        : base(
            errorCode: "MONTHLY_LIMIT_EXCEEDED",
            userMessage: $"Monthly limit in account {accountNumber ?? accountId.ToString()}. " +
                        $"Monthly limit: {monthlyLimit:C}, attempted amount: {attemptedAmount:C}, " +
                        $"already used this month: {alreadyUsedThisMonth:C}, remaining limit {monthlyLimit - alreadyUsedThisMonth}",
            techincalDetails: $"AccountId: {accountId}, Monthly limit: {monthlyLimit: C}, Attempted amount: {attemptedAmount:C}" +
                        $"Already used this month: {alreadyUsedThisMonth:C}, Remaining limit {monthlyLimit - alreadyUsedThisMonth}",
            innerException: innerException)
        {
            AccountId = accountId;
            AttemptedAmount = attemptedAmount;
            MonthlyLimit = monthlyLimit;
            AlreadyUsedThisMoth = alreadyUsedThisMonth;
        }
    }
}
