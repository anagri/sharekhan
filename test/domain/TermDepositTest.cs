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
            TermDeposit termDeposit = new TermDeposit(new Term(4), deposit, new Symbol("CITI"), "Term Deposit", new InterestRate(10), 24);
            Transaction termDepositTransaction = new TermDepositTransaction(new DateTime(2006, 11, 6), termDeposit, new Price(10000.00));

            double noOfYears = ((double)DateTime.Now.Subtract(new DateTime(2006, 11, 6)).Days) / 365;
            double expectedAmount = Math.Round((deposit.GetEffectiveReturn(noOfYears, 0.1).Value), 2);

            Assert.AreEqual(Math.Round(expectedAmount), termDepositTransaction.Amount().Value);
            
        }

        [Test]
        public void ShouldBeAbleToGiveCompoundInterestForWithdrawalTransaction()
        {
            Price deposit = new Price(2100);
            TermDeposit termDeposit = new TermDeposit(new Term(4), deposit, new Symbol("CITI"), "Term Deposit", new InterestRate(10), 24);
            Transaction termDepositTransaction = new TermDepositTransaction(new DateTime(2006, 11, 6), termDeposit, new Price(10000.00));
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

            double noOfYears = ((double)DateTime.Now.Subtract(new DateTime(2006, 11, 6)).Days) / 365;
            double depositAmountWithInterest = Math.Round((depositPrice.GetEffectiveReturn(noOfYears, 0.1).Value), 2);

            noOfYears = ((double)DateTime.Now.Subtract(new DateTime(2008, 11, 6)).Days) / 365;
            double withDrawAmountWithInterest = Math.Round((withdrawalPrice.GetEffectiveReturn(noOfYears, 0.1).Value), 2);

            Assert.AreEqual(Math.Round(depositAmountWithInterest - withDrawAmountWithInterest), termDeposit.CurrentMarketValue(transactionCollection).Value);

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

            double noOfYears = ((double)DateTime.Now.Subtract(new DateTime(2007, 11, 6)).Days) / 365;
            double depositAmountWithInterest = Math.Round((depositPrice.GetEffectiveReturn(noOfYears, 0.1).Value), 2);


            Assert.AreEqual(Math.Round(depositAmountWithInterest), termDeposit.CurrentMarketValue(transactionCollection).Value);

        }

        [Test]
        public void ShouldBeAbleToGiveCurrentMarketValueForMaturedSinglePayoutTermDeposit()
        {
            Price depositPrice = new Price(10000);
            TermDeposit termDeposit = new TermDeposit(new Term(2), new Price(10000), new Symbol("CITI"),
                                                         "Term Deposit", new InterestRate(10), -1);
            Transaction termDepositTransaction = new TermDepositTransaction(new DateTime(2006, 11, 6), termDeposit,
                                                                           new Price(10000));
            Transaction termDepositWithdrawalTransaction =
                new TermDepositWithdrawalTransaction(new DateTime(2008, 11, 6), termDeposit, new Price(12100));
            
            IList<Transaction> transactionCollection = new List<Transaction>() { termDepositTransaction, termDepositWithdrawalTransaction };
            transactionCollection.Add(termDepositTransaction);
            transactionCollection.Add(termDepositWithdrawalTransaction);

            Console.WriteLine(termDepositTransaction.Amount().Value);
            Console.WriteLine(termDepositWithdrawalTransaction.EffectiveTransactionAmount().Value);

            Assert.AreEqual(0, termDeposit.CurrentMarketValue(transactionCollection).Value);

        }
        [Test]
        public void ShouldBeMaturedSinglePayoutTermDeposit()
        {
            Price depositPrice = new Price(10000);
            TermDeposit termDeposit = new TermDeposit(new Term(1), new Price(10000), new Symbol("CITI"),
                                                         "Term Deposit", new InterestRate(10), -1);
            Transaction termDepositTransaction = new TermDepositTransaction(new DateTime(2007, 11, 6), termDeposit,
                                                                           new Price(10000));
            Assert.AreEqual(true, termDeposit.IsMatured());

        }

        [Test]
        public void ShouldBeAbleToGiveCurrentMarketValueForPeriodicPayoutTermDepositForMultipleWithdrawalTransaction()
        {
            Price depositPrice = new Price(10000);
            Price withdrawalPrice = new Price(1000);

            TermDeposit termDeposit = new TermDeposit(new Term(3), new Price(10000), new Symbol("CITI"), "Term Deposit", new InterestRate(10), 12);
            Transaction termDepositTransaction = new TermDepositTransaction(new DateTime(2006, 11, 7), termDeposit, new Price(10000.00));
            Transaction termDepositWithdrawalTransaction = new TermDepositWithdrawalTransaction(new DateTime(2007, 11, 7), termDeposit, new Price(1000.0));
            Transaction termDepositWithdrawalTransaction1 = new TermDepositWithdrawalTransaction(new DateTime(2008, 11, 7), termDeposit, new Price(1000.0));

            IList<Transaction> transactionCollection = new List<Transaction>() { termDepositTransaction, termDepositWithdrawalTransaction, termDepositWithdrawalTransaction1 };

            double noOfYears = ((double)DateTime.Now.Subtract(new DateTime(2006, 11, 7)).Days) / 365;
            double depositAmountWithInterest = Math.Round((depositPrice.GetEffectiveReturn(noOfYears, 0.1).Value), 2);

            noOfYears = ((double)DateTime.Now.Subtract(new DateTime(2007, 11, 7)).Days) / 365;
            double withDrawAmountWithInterest1 = Math.Round((withdrawalPrice.GetEffectiveReturn(noOfYears, 0.1).Value), 2);

            noOfYears = ((double)DateTime.Now.Subtract(new DateTime(2008, 11, 7)).Days) / 365;
            double withDrawAmountWithInterest = Math.Round((withdrawalPrice.GetEffectiveReturn(noOfYears, 0.1).Value), 2);

            Assert.AreEqual(Math.Round(depositAmountWithInterest - (withDrawAmountWithInterest + withDrawAmountWithInterest1)), termDeposit.CurrentMarketValue(transactionCollection).Value);

        }
    }
}
