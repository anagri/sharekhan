using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sharekhan.domain;

namespace Sharekhan.test.domain
{
    [TestFixture]
    public class SinglePayOutTest
    {
        [Test, Ignore]
        [ExpectedException(typeof(InvalidTermDepositException))]
        public void ShouldThrowExceptionForInValidSinglePayOutTermDeposit()
        {
            // Invalid Term
            new SinglePayOut(new Term(0), new Price(1000), new Symbol("CITI"), "Term Deposit", new InterestRate(10));

            // Invalid InterestRate
            new SinglePayOut(new Term(10), new Price(1000), new Symbol("CITI"), "Term Deposit", new InterestRate(0));

            // Invalid Price
            //new SinglePayOut(new Term(10), new Price(0), new Symbol("CITI"), "Term Deposit", new InterestRate(10));
            
            // Date not specified
           // new SinglePayOut(new Term(10), new Price(1000), new Symbol("CITI"), "Term Deposit", new InterestRate(10));
        }

        [Test]
        public void ShouldBeAbleToGiveTotalAmountForTermDepositForATimePeriod()
        {
            Price deposit = new Price(10000);
            SinglePayOut termDeposit = new SinglePayOut(new Term(2), deposit, new Symbol("CITI"), "Term Deposit", new InterestRate(10));
            Transaction termDepositTransaction = new TermDepositTransaction(new DateTime(2007, 11, 5), termDeposit, new Price(10000.00));

            IList<Transaction> transactionCollection = new List<Transaction>() { termDepositTransaction };

            int noOfYears = DateTime.Now.Subtract(new DateTime(2007, 11, 5)).Days / 365;

            double expectedAmount = Math.Round((deposit.GetEffectiveReturn(noOfYears, 0.1).Value),2);

            Assert.AreEqual(expectedAmount, termDeposit.CurrentMarketValue(transactionCollection).Value);

        }

        [Test]
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
