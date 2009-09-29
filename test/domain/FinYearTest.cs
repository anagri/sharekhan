using System;
using NUnit.Framework;

namespace ShareKhan.domain
{
    [TestFixture]
    public class FinYearTest
    {
        [Test]
        [Ignore]
        public void DifferenceBetweenTheStartAndEndYearShouldBeOne()
        {
            try
            {
                FinYear finYear = new FinYear(2009, 2011);
                Assert.Fail("Should Throw an exception");
            }catch(ArgumentException e)
            {
                Console.WriteLine(e);
            }
        }

        [Test]
        [Ignore]
        public void FinYearInputShouldNotBeNegative()
        {
            try
            {
                FinYear finYear = new FinYear(-2009, -2011);
                Assert.Fail("Should Throw an exception");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}