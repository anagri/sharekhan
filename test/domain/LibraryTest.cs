using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Sharekhan.test.domain
{
    [TestFixture]
    public class LibraryTest
    {
        [Test]
        public void ShouldBeAbleToFindDateDifference()
        {
            DateTime d1 = new DateTime(2008, 06, 01);
            DateTime d2 = new DateTime(2008, 12, 01);
            Assert.AreEqual(-183,d1.Subtract(d2).Days);
        }
    }
}
