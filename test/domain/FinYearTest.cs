using System;
using NUnit.Framework;

namespace ShareKhan.domain
{
    [TestFixture]
    public class FinYearTest
    {
        [Test]
        [TestCase(-2009)]
        [TestCase(-2008)]
        [ExpectedException(typeof (ArgumentException))]
        public void FinYearInputShouldBeNonZeroPositive(int startYear)
        {
            new FinYear(startYear);
        }


        [Test]
        public void ShouldGetTheTaxationPeriodForFinYear()
        {
            int curYear = DateTime.Today.Year;
            int nextYear = DateTime.Today.Year + 1;
            int prevYear = curYear - 1;

            var finYear = new FinYear(curYear);
            var taxationPeriod = new FinYear.TaxationPeriod(new DateTime(curYear, 4, 1),
                                                            new DateTime(nextYear, 3, 31));
            Assert.AreEqual(taxationPeriod, finYear.GetTaxationPeriod());

            var finYearPrev = new FinYear(prevYear);
            var prevtaxationPeriod = new FinYear.TaxationPeriod(new DateTime(prevYear, 4, 1),
                                                                new DateTime(curYear, 3, 31));
            Assert.AreEqual(prevtaxationPeriod, finYearPrev.GetTaxationPeriod());
        }
    }
}