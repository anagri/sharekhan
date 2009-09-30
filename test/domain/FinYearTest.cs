using System;
using NUnit.Framework;

namespace ShareKhan.domain
{
    [TestFixture]
    public class FinYearTest
    {
        [Test]
        [TestCase(2009, 2011)]
        [TestCase(2009, 2008)]
        [ExpectedException(typeof (ArgumentException))]
        public void EndYearShouldBeOneGreaterThanStartYear(int startYear, int endYear)
        {
            new FinYear(startYear, endYear);
        }

        [Test]
        [TestCase(-2009, -2010)]
        [TestCase(2009, -2010)]
        [TestCase(-2009, 2010)]
        [ExpectedException(typeof (ArgumentException))]
        public void FinYearInputShouldBeNonZeroPositive(int startYear, int endYear)
        {
            new FinYear(startYear, endYear);
        }

        [Test]
        [TestCase(2009, 2010, Result = true)]
        [TestCase(2008, 2009, Result = false)]
        [TestCase(2010, 2011, Result = false)]
        [TestCase(2011, 2012, Result = false)]
        public bool ShouldAbleToDetermineCurFinYear(int startYear, int endYear)
        {
            var year = new FinYear(startYear, endYear);
            return year.IsCurrent();
        }

        [Test]
        public void ShouldGetTheTaxationPeriodForFinYear()
        {
            int curYear = DateTime.Today.Year;
            int nextYear = DateTime.Today.Year + 1;
            int prevYear = curYear - 1;
            int nextToNextYear = curYear + 2;
            int nextToNextToNextYear = curYear + 3; //I should get award for variable names

            var apr1StPrevYear = new DateTime(prevYear, 4, 1);

            var apr1StThisYear = new DateTime(curYear, 4, 1);
            var mar31StThisYear = new DateTime(curYear, 3, 31);


            var apr1StNextToNextYear = new DateTime(nextToNextYear, 4, 1);
            var today = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);


            var taxableDaysOfThisYear = new FinYear.TaxationPeriod(apr1StThisYear, today);
            Assert.AreEqual(taxableDaysOfThisYear, new FinYear(curYear, nextYear).GetTaxationPeriod());

            var taxableDaysOfLastYear = new FinYear.TaxationPeriod(apr1StPrevYear, mar31StThisYear);
            Assert.AreEqual(taxableDaysOfLastYear, new FinYear(prevYear, curYear).GetTaxationPeriod());

            var taxableDaysOfNextToNextYear = new FinYear.TaxationPeriod(apr1StNextToNextYear, apr1StNextToNextYear);
            Assert.AreEqual(taxableDaysOfNextToNextYear,
                            new FinYear(nextToNextYear, nextToNextToNextYear).GetTaxationPeriod());
        }
    }
}