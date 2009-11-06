using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sharekhan.domain;

namespace Sharekhan.test.domain
{
    [TestFixture]
    public class TermDepositTest
    {

        [Test]
        public void ShouldBeAbleToGiveCompoundInterestForDepositTransaction()
        {
            Price deposit = new Price(10000);
            PeriodicPayout termDeposit = new PeriodicPayout(new Term(4), deposit, new Symbol("CITI"), "Term Deposit", new InterestRate(10), 24);
            Transaction termDepositTransaction = new TermDepositTransaction(new DateTime(2006, 11, 6), termDeposit, new Price(10000.00));

            int noOfYears = DateTime.Now.Subtract(new DateTime(2006, 11, 6)).Days / 365;
            double expectedAmount = Math.Round((deposit.GetEffectiveReturn(noOfYears, 0.1).Value), 2);

            Assert.AreEqual(expectedAmount, termDepositTransaction.Amount().Value);
        }

        [Test]
        public void ShouldBeAbleToGiveCompoundInterestForWithdrawalTransaction()
        {
            Price deposit = new Price(2100);
            PeriodicPayout termDeposit = new PeriodicPayout(new Term(4), deposit, new Symbol("CITI"), "Term Deposit", new InterestRate(10), 24);
            Transaction termDepositWithdrawalTransaction = new TermDepositWithdrawalTransaction(new DateTime(2008, 11, 6), termDeposit, new Price(2100.0));

            int noOfYears = DateTime.Now.Subtract(new DateTime(2008, 11, 6)).Days / 365;
            double expectedAmount = Math.Round((deposit.GetEffectiveReturn(noOfYears, 0.1).Value), 2);

            Assert.AreEqual(-expectedAmount, termDepositWithdrawalTransaction.Amount().Value);
        }

        [Test]
        public void ShouldBeAbleToGiveCurrentMarketValueForPeriodicPayoutTermDeposit()
        {
            Price depositPrice = new Price(10000);
            Price withdrawalPrice = new Price(2100);

            TermDeposit termDeposit = new TermDeposit(new Term(4), new Price(10000), new Symbol("CITI"), "Term Deposit", new InterestRate(10), 24);
            Transaction termDepositTransaction = new TermDepositTransaction(new DateTime(2006, 11, 6), termDeposit, new Price(10000.00));
            Transaction termDepositWithdrawalTransaction = new TermDepositWithdrawalTransaction(new DateTime(2008, 11, 6), termDeposit, new Price(2100.0));

            IList<Transaction> transactionCollection = new List<Transaction>() { termDepositTransaction, termDepositWithdrawalTransaction };

            int noOfYears = DateTime.Now.Subtract(new DateTime(2006, 11, 6)).Days / 365;
            double depositAmountWithInterest = Math.Round((depositPrice.GetEffectiveReturn(noOfYears, 0.1).Value), 2);

            noOfYears = DateTime.Now.Subtract(new DateTime(2008, 11, 6)).Days / 365;
            double withDrawAmountWithInterest = Math.Round((withdrawalPrice.GetEffectiveReturn(noOfYears, 0.1).Value), 2);

            Assert.AreEqual(depositAmountWithInterest - withDrawAmountWithInterest, termDeposit.CurrentMarketValue(transactionCollection).Value);

        }

        [Test]
        public void ShouldBeAbleToGiveRealizedProfitForSinglePayoutTermDeposit()
        {
            Price depositPrice = new Price(10000);
            Price withdrawalPrice = new Price(2100);

            TermDeposit termDeposit = new TermDeposit(new Term(4), new Price(10000), new Symbol("CITI"), "Term Deposit", new InterestRate(10), -1);
            Transaction termDepositTransaction = new TermDepositTransaction(new DateTime(2006, 11, 6), termDeposit, new Price(10000.00));

            ITransactionCollection transactionCollection = new TransactionCollection();
            transactionCollection.Add(termDepositTransaction);
            
            Assert.AreEqual(0, termDeposit.CalculateRealizedProfits(transactionCollection));

        }

        [Test]
        public void ShouldBeAbleToGiveRealizedProfitForMaturedSinglePayoutTermDeposit()
        {
            Price depositPrice = new Price(10000);
            Price withdrawalPrice = new Price(2100);
            TermDeposit termDeposit = new TermDeposit(new Term(2), new Price(10000), new Symbol("CITI"),
                                                         "Term Deposit", new InterestRate(10), 24);
            Transaction termDepositTransaction = new TermDepositTransaction(new DateTime(2006, 11, 6), termDeposit,
                                                                           null);
            Transaction termDepositWithdrawalTransaction =
                new TermDepositWithdrawalTransaction(new DateTime(2008, 11, 6), termDeposit, new Price(2310));

            ITransactionCollection transactionCollection = new TransactionCollection();
            transactionCollection.Add(termDepositTransaction);
            transactionCollection.Add(termDepositWithdrawalTransaction);
            
            int noOfYears = DateTime.Now.Subtract(new DateTime(2008, 11, 6)).Days / 365;
            double withDrawAmountWithInterest = Math.Round((withdrawalPrice.GetEffectiveReturn(noOfYears, 0.1).Value), 2);

            Assert.AreEqual(withDrawAmountWithInterest, termDeposit.CalculateRealizedProfits(transactionCollection));

        }

        [Test]
        public void ShouldBeAbleToGiveCurrentMarketValueForSinglePayoutTermDeposit()
        {
            Price depositPrice = new Price(10000);

            TermDeposit termDeposit = new TermDeposit(new Term(4), new Price(10000), new Symbol("CITI"), "Term Deposit", new InterestRate(10), 24);
            Transaction termDepositTransaction = new TermDepositTransaction(new DateTime(2007, 11, 6), termDeposit, new Price(10000.00));

            IList<Transaction> transactionCollection = new List<Transaction>() { termDepositTransaction};

            int noOfYears = DateTime.Now.Subtract(new DateTime(2007, 11, 6)).Days / 365;
            double depositAmountWithInterest = Math.Round((depositPrice.GetEffectiveReturn(noOfYears, 0.1).Value), 2);


            Assert.AreEqual(depositAmountWithInterest, termDeposit.CurrentMarketValue(transactionCollection).Value);

        }

        [Test, Ignore]
        public void ShouldBeAbleToGiveCurrentMarketValueForMaturedSinglePayoutTermDeposit()
        {
            Price depositPrice = new Price(10000);
            Price withdrawalPrice = new Price(2100);
            TermDeposit termDeposit = new TermDeposit(new Term(2), new Price(10000), new Symbol("CITI"),
                                                         "Term Deposit", new InterestRate(10), -1);
            Transaction termDepositTransaction = new TermDepositTransaction(new DateTime(2006, 11, 6), termDeposit,
                                                                           new Price(10000));
            Transaction termDepositWithdrawalTransaction =
                new TermDepositWithdrawalTransaction(new DateTime(2008, 11, 6), termDeposit, new Price(12310));

            IList<Transaction> transactionCollection = new List<Transaction>() { termDepositTransaction, termDepositWithdrawalTransaction };
            transactionCollection.Add(termDepositTransaction);
            transactionCollection.Add(termDepositWithdrawalTransaction);

            Assert.AreEqual(12310, termDeposit.CurrentMarketValue(transactionCollection).Value);

        }

    }
}
