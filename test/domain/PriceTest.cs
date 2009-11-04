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

        [Test]
        public void ShouldReturnEffectiveValueOfThePriceForADurationAndRate()
        {
            double basePrice = 1000.0;
            var price = new Price(basePrice);
            double delta = 0.005 * basePrice;

            Assert.AreEqual((new Price(1000*1.3)).Value, price.GetEffectiveValue(1, 0.3).Value, delta);
            Assert.AreEqual((new Price(1000/(1.2*1.2))).Value, price.GetEffectiveValue(-2, 0.2).Value, delta);
            Assert.AreEqual(1323.938, price.GetEffectiveValue(1.258, 0.25).Value, delta);
        }
    }

    public class AlternateClass
    {
        
            
    }
}
