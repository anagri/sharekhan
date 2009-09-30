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

        [Ignore]
        [Test]
        public void ShouldGetTheTaxableDayForFinYear()
        {
            var apr1St2008 = new DateTime(2008, 4, 1);
            
            var apr1St2009 = new DateTime(2009, 4, 1);
            var mar31St2009 = new DateTime(2009, 3, 31);

            var apr1St2010 = new DateTime(2010, 4, 1);
            

            var taxableDaysOf2009 = new FinYear.TaxableDateRange(apr1St2009, DateTime.Now);
            Assert.Equals(taxableDaysOf2009,new FinYear(2009, 2010).GetTaxableDays());

            var taxableDaysOf2008 = new FinYear.TaxableDateRange(apr1St2008, mar31St2009);
            Assert.Equals(taxableDaysOf2008, new FinYear(2008, 2009).GetTaxableDays());

            var taxableDaysOf2010 = new FinYear.TaxableDateRange(apr1St2010, apr1St2010);
            Assert.Equals(taxableDaysOf2010, new FinYear(2010, 2011).GetTaxableDays());
        }
    }
}