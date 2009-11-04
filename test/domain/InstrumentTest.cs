using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Sharekhan.domain
{
    [TestFixture]
    public class InstrumentTest
    {
        [Test]
        public void ShouldBeAbleToUpdateInstrumentCurrentPrice()
        {

            Instrument instrument = new MutualFund(new Symbol("SUN"), new Price(1000), "Sun MF", "SUNMF", "SUN Magma", "Growth");
            var four = new Price(4);
            instrument.UpdateCurrentPrice(four);
            Assert.AreEqual(four, instrument.CurrentPrice);
        }

        


    }
}