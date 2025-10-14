using FinancialProcessor.Core.Exceptions;

namespace FinancialProcessor.Core.Entities
{
    internal class Account
    {
        public Guid Id { get; private set; }
        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }
        public string Currency {  get; private set; }
        public decimal DailyLimit { get; private set; }
        public decimal MonthlyLimit { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public virtual ICollection<Transaction> Transactions { get; private set; }

        private Account() 
        {
            Transactions = new List<Transaction>();
        } // EF

        public Account(string accountNumber, string currency, decimal dailyLimit, decimal monthlyLimit)
        {
            Id = Guid.NewGuid();
            AccountNumber = accountNumber;
            Balance = 0;
            Currency = currency;
            DailyLimit = dailyLimit;
            MonthlyLimit = monthlyLimit;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public bool CanWithdraw(decimal amount)
        {
            if (!IsActive)
                return false;

            if (amount <= 0)
                return false;

            if (Balance < amount)
                return false;

            // TODO: реализовать лимиты
            return true;
        }

        public void Withdraw(decimal amount)
        {
            if (!IsActive)
                throw new InvalidOperationException("Account is not active");

            if (amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));

            if (Balance < amount)
            {
                throw new InsufficientFundsException(
                    accountId: Id,
                    currentBalance: Balance,
                    requestedAmount: amount,
                    accountNumber: AccountNumber
                );
            }

            Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            if (!IsActive)
                throw new InvalidOperationException("Account is not active");

            if (amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));

            Balance += amount;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("Account is already deactivated");

            IsActive = false;
        }

        public void Activate()
        {
            if (IsActive)
                throw new InvalidOperationException("Account is already active");

            IsActive = true;
        }

        public void UpdateLimits(decimal dailyLimit, decimal monthlyLimit)
        {
            if (dailyLimit <= 0)
                throw new ArgumentException("Daily limit must be positive", nameof(dailyLimit));

            if (monthlyLimit <= 0)
                throw new ArgumentException("Monthly limit must be positive", nameof(monthlyLimit));

            if (monthlyLimit < dailyLimit)
                throw new ArgumentException("Monthly limit cannot be less than daily limit");

            DailyLimit = dailyLimit;
            MonthlyLimit = monthlyLimit;
        }

        public bool HasSufficientBalance(decimal amount)
        {
            return IsActive && Balance >= amount;
        }

        public decimal GetAvailableBalance()
        {
            return IsActive ? Balance : 0;
        }

        // dummy's лимитов
        public bool IsWithinDailyLimit(decimal amount) => true;
        public bool IsWithinMonthlyLimit(decimal amount) => true;
    }
}
