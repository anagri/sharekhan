using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sharekhan.domain;

namespace Sharekhan.domain
{
    [TestFixture]
    public class PriceTest
    {
        [Test]
        public void should_be_equal_when_values_are_equal()
        {
            Price fourRuppees = new Price(4.0d);
            Price anotherFourRuppees = new Price(4.0d);
            
            
            Assert.AreEqual(fourRuppees,anotherFourRuppees);
            Assert.AreEqual(fourRuppees.GetHashCode(), anotherFourRuppees.GetHashCode());

            Assert.AreNotEqual(fourRuppees, new AlternateClass());
        }
    }

    public class AlternateClass
    {
        
            
    }
}
