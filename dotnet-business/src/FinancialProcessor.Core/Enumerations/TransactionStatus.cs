namespace FinancialProcessor.Core.Enumerations
{
    internal enum TransactionStatus
    {
        Pending = 1,      // Created, wait processing
        Processing = 2,   // Processing
        Completed = 3,    // Completed
        Failed = 4,       // Failed with error
        Cancelled = 5     // Cancelled
    }
}
