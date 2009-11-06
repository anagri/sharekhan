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

    }
}
