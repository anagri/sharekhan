using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShareKhan.domain;
using ShareKhan.persist;

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

        [Test]
        public void ShouldIncludeUnitDividendWhileGettingCurrentMarketValueOfInstrument()
        {
            Instrument instrument = new MutualFund(new Symbol("RILMF"), new Price(100.00), "Reliance Mutual Fund", "SUNMF", "SUN Magma", "Growth");

            Transaction transaction1 = new BuyTransaction(new DateTime(2009, 09, 09), instrument, 10, new Price(100.00), 10, 10);
            Transaction transaction2 = new SellTransaction(new DateTime(2009, 09, 10), instrument, 5, new Price(200.00), 10, 10);
            Transaction transaction3 = new UnitDividendTransaction(instrument, 5, new DateTime(2009, 09, 10));

            List<Transaction> listTransaction = new List<Transaction>();
            listTransaction.Add(transaction1);
            listTransaction.Add(transaction2);
            Assert.AreEqual(500.00, instrument.CurrentMarketValue(listTransaction).Value);
            listTransaction.Add(transaction3);

            Assert.AreEqual(1000.00, instrument.CurrentMarketValue(listTransaction).Value);

        }


    }
}