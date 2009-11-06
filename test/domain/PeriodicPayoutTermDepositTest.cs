using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sharekhan.domain;

namespace Sharekhan.test.domain
{
    [TestFixture]
    public class PeriodicPayoutTermDepositTest
    {
        [Test, Ignore]
        [ExpectedException(typeof(InvalidTermDepositException))]
        public void ShouldThrowExceptionForInValidSinglePayOutTermDeposit()
        {
            // Invalid Term
            new PeriodicPayout(new Term(0), new Price(1000), new Symbol("CITI"), "Term Deposit", new InterestRate(10),6);

            // Invalid InterestRate
            new PeriodicPayout(new Term(10), new Price(1000), new Symbol("CITI"), "Term Deposit", new InterestRate(0),3);

            // Invalid Price
            new PeriodicPayout(new Term(10), new Price(0), new Symbol("CITI"), "Term Deposit", new InterestRate(10),6);

            // Date not specified
            new PeriodicPayout(new Term(10), new Price(1000), new Symbol("CITI"), "Term Deposit", new InterestRate(10),3);

        }

        [Test, Ignore]
        public void ShouldBeAbleToGiveInterestForTermDepositForATimePeriod()
        {
            PeriodicPayout termDeposit = new PeriodicPayout(new Term(2), new Price(10000), new Symbol("CITI"), "Term Deposit", new InterestRate(10),6);
            Transaction termDepositTransaction = new TermDepositTransaction(new DateTime(2008, 12, 5), termDeposit, new Price(10000.00));
            Transaction termDepositWithdrawalTransaction = new TermDepositWithdrawalTransaction(new DateTime(2009, 6, 5), termDeposit, new Price(500.0));
           // Transaction termDepositWithdrawalTransaction = new TermDepositWithdrawalTransaction(new DateTime(2009, 12, 5), termDeposit, new Price(500.0));

            IList<Transaction> transactionCollection = new List<Transaction>() 
            { termDepositTransaction, termDepositWithdrawalTransaction };

            Assert.AreEqual(10400, termDeposit.CurrentMarketValue(transactionCollection).Value);

        }

        [Test, Ignore]
        public void ShouldBeAbleToGiveRealizedProfitForTermDeposit()
        {
            SinglePayOut termDeposit = new SinglePayOut(new Term(2), new Price(10000), new Symbol("CITI"), "Term Deposit", new InterestRate(10));
            Transaction termDepositTransaction = new TermDepositTransaction(new DateTime(2007, 11, 5), termDeposit, null);
            Transaction termDepositWithdrawalTransaction = new TermDepositWithdrawalTransaction(new DateTime(2009, 11, 6), termDeposit, new Price(12100.00));

            ITransactionCollection transactionCollection = new TransactionCollection();
            transactionCollection.Add(termDepositTransaction);
            transactionCollection.Add(termDepositWithdrawalTransaction);

            Assert.AreEqual(12100, termDeposit.CalculateRealizedProfits(transactionCollection));

        }

    }
}
