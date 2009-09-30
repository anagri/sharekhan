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
    }
}