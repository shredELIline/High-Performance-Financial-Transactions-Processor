using FinancialProcessor.Core.Enumerations;

namespace FinancialProcessor.Core.Exceptions
{
    internal class TransactionAlreadyProcessedException : DomainException
    {
        public Guid TransactionId { get; }
        public TransactionStatus CurrentStatus { get; }

        public TransactionAlreadyProcessedException(
            Guid transactionId,
            TransactionStatus currentStatus)
            : base(
                errorCode: "TRANSACTION_ALREADY_PROCESSED",
                userMessage: $"Transaction cannot be modified because it's already in {currentStatus} status.",
                technicalDetails: $"TransactionId: {transactionId}, CurrentStatus: {currentStatus}")
        {
            TransactionId = transactionId;
            CurrentStatus = currentStatus;
        }

        public TransactionAlreadyProcessedException(
            Guid transactionId,
            TransactionStatus currentStatus,
            Exception innerException)
            : base(
                errorCode: "TRANSACTION_ALREADY_PROCESSED",
                userMessage: $"Transaction cannot be modified because it's already in {currentStatus} status.",
                technicalDetails: $"TransactionId: {transactionId}, CurrentStatus: {currentStatus}",
                innerException: innerException)
        {
            TransactionId = transactionId;
            CurrentStatus = currentStatus;
        }
    }
}
