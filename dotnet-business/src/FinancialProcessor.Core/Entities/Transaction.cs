using FinancialProcessor.Core.Exceptions;
using TransactionStatus = FinancialProcessor.Core.Enumerations.TransactionStatus;

namespace FinancialProcessor.Core.Entities
{
    internal class Transaction
    {
        public Guid Id { get; private set; }
        public Guid AccountId { get; private set; }
        public string MerchantId { get; private set; }
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }
        public TransactionStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ProcessedAt { get; private set; }
        public string? FailureReason { get; private set; }

        public virtual Account Account { get; private set; }

        private Transaction()
        {
        } // EF

        public Transaction(Guid accountId, string merchantId, decimal amount, string currency)
        {
            if (accountId == Guid.Empty)
                throw new ArgumentException("Account ID cannot be empty", nameof(accountId));

            if (string.IsNullOrWhiteSpace(merchantId))
                throw new ArgumentException("Merchant ID cannot be empty", nameof(merchantId));

            if (amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency cannot be empty", nameof(currency));

            Id = Guid.NewGuid();
            AccountId = accountId;
            MerchantId = merchantId;
            Amount = amount;
            Currency = currency;
            Status = TransactionStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkAsProcessing()
        {
            if (Status != TransactionStatus.Pending)
                throw new TransactionAlreadyProcessedException(Id, Status);

            Status = TransactionStatus.Processing;
        }

        public void MarkAsCompleted()
        {
            if (Status != TransactionStatus.Processing && Status != TransactionStatus.Pending)
                throw new TransactionAlreadyProcessedException(Id, Status);

            Status = TransactionStatus.Completed;
            ProcessedAt = DateTime.UtcNow;
            FailureReason = null;
        }

        public void MarkAsFailed(string reason)
        {
            if (Status != TransactionStatus.Processing && Status != TransactionStatus.Pending)
                throw new TransactionAlreadyProcessedException(Id, Status);

            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Failure reason cannot be empty", nameof(reason));

            Status = TransactionStatus.Failed;
            ProcessedAt = DateTime.UtcNow;
            FailureReason = reason;
        }

        public void MarkAsCancelled(string reason)
        {
            if (Status != TransactionStatus.Pending)
                throw new TransactionAlreadyProcessedException(Id, Status);

            Status = TransactionStatus.Cancelled;
            ProcessedAt = DateTime.UtcNow;
            FailureReason = reason;
        }

        public bool CanBeProcessed()
        {
            return Status == TransactionStatus.Pending;
        }

        public bool IsFinalStatus()
        {
            return Status == TransactionStatus.Completed ||
                   Status == TransactionStatus.Failed ||
                   Status == TransactionStatus.Cancelled;
        }

        public TimeSpan GetProcessingTime()
        {
            if (ProcessedAt == null)
                return TimeSpan.Zero;

            return ProcessedAt.Value - CreatedAt;
        }

        public bool IsAmountValid()
        {
            return Amount > 0;
        }

        public bool IsCurrencyValid()
        {
            return !string.IsNullOrWhiteSpace(Currency) && Currency.Length == 3;
        }
    }
}
