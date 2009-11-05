using System;
using NUnit.Framework;
using Sharekhan.domain;

namespace Sharekhan.test.domain

{
    [TestFixture]
    public class TransactionCollectionTest
    {

        [Test]
        public void ShouldCheckForUniqueInstrumentsInTransactions()
        {
            TransactionCollection ts = new TransactionCollection();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            Stock infyShare = new Stock(new Symbol("INF"), new Price(100.00), "Infosys");
            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(1200), 0, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(1300), 0, 0));
            ts.Add(new BuyTransaction(new DateTime(1999, 8, 20), infyShare, 67, new Price(110), 0, 0));
            ts.Add(new SellTransaction(new DateTime(2000, 9, 20), infyShare, 63, new Price(101), 0, 0));
            Assert.AreEqual(2, ts.GetAllUniqueInstruments().Count);

        }

        [Test, Ignore]
        public void ShouldReturnTheNetValueGivenTheDateAndRateOfReturn()
        {
            var transactionCollection = new TransactionCollection();
            var symbol = new Symbol("STOCK1");
            var stock = new Stock(symbol, new Price(100.0), "My Stock 1");

            transactionCollection.Add(new BuyTransaction(new DateTime(2007, 1, 1),
                                                         stock,
                                                         100, new Price(80.0), 10, 5));
            transactionCollection.Add(new BuyTransaction(new DateTime(2007, 6, 1),
                                                         stock,
                                                         100, new Price(85.0), 15, 5));
            transactionCollection.Add(new SellTransaction(new DateTime(2008, 10, 5),
                                                         stock,
                                                         120, new Price(100.0), 15, 10));
            transactionCollection.Add(new CashDividendTransaction(stock,
                                                         new Price(800.0), new DateTime(2009, 5, 5)));


            const double expectedValue1 = -16201.73;
            Assert.AreEqual(expectedValue1, transactionCollection.GetEffectiveValue(new DateTime(2009, 11, 4), 0.3).Value, 0.005*-expectedValue1);
            const double expectedValue2 = -7226.7;
            Assert.AreEqual(expectedValue2, transactionCollection.GetEffectiveValue(new DateTime(2007, 1, 1), 0.25).Value, 0.005 *-expectedValue2);
            const double expectedValue3 = -5590.62;
            Assert.AreEqual(expectedValue3, transactionCollection.GetEffectiveValue(new DateTime(2006, 1, 1), 0.2).Value, 0.005 *-expectedValue3);

            transactionCollection.Add(new SellTransaction(new DateTime(2009, 11, 4),
                                                         stock,
                                                         80, new Price(100.0), 0, 0));
            const double expectedValue4 = 0.0;
            Assert.AreEqual(expectedValue4, transactionCollection.GetEffectiveValue(new DateTime(2007, 1, 1), 0.3).Value, 0.005 * -expectedValue4);

        }

    }
}

