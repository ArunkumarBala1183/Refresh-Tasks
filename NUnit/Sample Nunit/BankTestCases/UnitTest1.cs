using BankingApp.Models;
using NUnit.Framework.Internal;

namespace BankTestCases;

[TestFixture]
public class Tests
{
    Bank account;

    [SetUp]
    public void Setup()
    {
        account = new Bank();
    }

    [Test]
    public void BalanceCheckTestCase()
    {
        this.account.AddBalance(500);
        
        Assert.That(account.getBalance() , Is.EqualTo(500));

    }

    [Test]
    public void TestInvalidBalanceEntry()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => this.account.addBalance(0));
    }
    
    [Test]
    public void TestInvalidWithraw()
    {
        Assert.Throws<ArgumentNullException>(() => this.account.withdraw(0));
    }



}