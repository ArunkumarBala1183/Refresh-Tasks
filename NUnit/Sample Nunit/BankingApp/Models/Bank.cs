namespace BankingApp.Models;

public class Bank : Account
{
    public double GetBalance()
    {
        return base.getBalance();
    }

    public void AddBalance(double amount)
    {
        base.addBalance(amount);
    }

    public void Withdraw(double amount)
    {
        base.withdraw(amount);
    }
}
