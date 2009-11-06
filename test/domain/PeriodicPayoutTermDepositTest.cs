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
 

    }
}
