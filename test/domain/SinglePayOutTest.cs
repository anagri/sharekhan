using System;
using NUnit.Framework;
using Sharekhan.domain;
using Sharekhan.src.domain;

namespace Sharekhan.test.domain
{
    [TestFixture]
    public class SinglePayOutTest
    {
        [Test]
        [ExpectedException(typeof(InvalidTermDepositException))]
        public void ShouldThrowExceptionForInValidSinglePayOutTermDeposit()
        {
            // Invalid Term
            new SinglePayOut(new Term(0), new Price(1000), new Symbol("CITI"), "Term Deposit", new InterestRate(10), new DepositDate(new DateTime(2009, 11, 11)));

            // Invalid InterestRate
            new SinglePayOut(new Term(10), new Price(1000), new Symbol("CITI"), "Term Deposit", new InterestRate(0), new DepositDate(new DateTime(2009, 11, 11)));

            // Invalid Price
            new SinglePayOut(new Term(10), new Price(0), new Symbol("CITI"), "Term Deposit", new InterestRate(10), new DepositDate(new DateTime(2009, 11, 11)));
            
            // Date not specified
            new SinglePayOut(new Term(10), new Price(1000), new Symbol("CITI"), "Term Deposit", new InterestRate(10), new DepositDate(new DateTime()));

        }

    }
}
