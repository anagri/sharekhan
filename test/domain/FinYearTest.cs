using System;
using NUnit.Framework;

namespace ShareKhan.domain
{
    [TestFixture]
    public class FinYearTest
    {
//        [Test]
//        public void DifferenceBetweenTheStartAndEndYearShouldBeOne()
//        {
//            try
//            {
//                var finYear = new FinYear(2009, 2011);
//                Assert.Fail("Should Throw an exception");
//            }
//            catch (ArgumentException e)
//            {
//            }
//        }

        [TestCase(-2009, -2010)]
        [TestCase(2009, -2010)]
        [TestCase(-2009, 2010)]
        [ExpectedException(typeof(ArgumentException))]
        public void FinYearInputShouldBeNonZeroPositive(int startYear, int endYear)
        {
            new FinYear(startYear, endYear);
        }
    }
}