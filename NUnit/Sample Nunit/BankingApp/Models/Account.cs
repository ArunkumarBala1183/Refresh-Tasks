namespace BankingApp.Models
{
    public abstract class Account
    {
        // public string AccountHolderName;
        // public long AccountNumber;
        private double Balance;

        public double getBalance()
        {
            return this.Balance;
        }

        public void addBalance(double balance)
        {
            if(balance <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.Balance += balance;
        }

        public void withdraw(double balance)
        {
            if(balance <= 0)
            {
                throw new ArgumentNullException();
            }

            this.Balance -= balance;
        }
    }
}