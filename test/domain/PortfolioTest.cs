using System;
using NUnit.Framework;
using Sharekhan.domain;

namespace ShareKhan.domain
{
    [TestFixture]
    public class PorfolioTest
    {
        [Test]
        public void ShouldGetInvestedValue()
        {
            Portfolio portfolio = new Portfolio();
            Instrument mutualFund = new MutualFund(new Symbol("RIL"), new Price(100.00), "Rel MF");

            Transaction buy = new BuyTransaction(DateTime.Today, mutualFund, 10, new Price(1000), 100, 100);

         }

        [Test]
        
        public void ShouldBeAbleToCalcSTCGTax()
        {
            Portfolio portfolio = new Portfolio();
            try
            {
                portfolio.CalcShortTermCapitalGainTax(new FinYear(2009, 2010));
                Assert.Fail("Should Throw Exception for now");
            }catch(NotImplementedException e)
            {
                
            }
            
        }
    }
}