namespace FinancialProcessor.Core.Exceptions
{
    internal class FraudDetectedException : DomainException
    {
        public Guid TransactionId { get; }
        public string[] TriggeredRules { get; }
        public decimal RiskScore { get; }
        public string FraudType { get; }

        public FraudDetectedException(
            Guid transactionId,
            string[] triggeredRules,
            decimal riskScore,
            string fraudType,
            Exception? innerException = null)
            : base(
                errorCode: "FRAUD_DETECTED",
                userMessage: $"Transaction flagged as potentially fraudulent. Please contact support.",
                technicalDetails: $"TransactionId: {transactionId}, " +
                                 $"RiskScore: {riskScore}, " +
                                 $"FraudType: {fraudType}, " +
                                 $"TriggeredRules: {string.Join(", ", triggeredRules)}",
                innerException: innerException)
        {
            TransactionId = transactionId;
            TriggeredRules = triggeredRules ?? Array.Empty<string>();
            RiskScore = riskScore;
            FraudType = fraudType;
        }

        public FraudDetectedException(
            Guid transactionId,
            string triggeredRule,
            decimal riskScore,
            string fraudType,
            Exception? innerException = null)
            : this(
                transactionId,
                new[] { triggeredRule },
                riskScore,
                fraudType,
                innerException)
        {
        }
    }
}
