using System;
using NUnit.Framework;
using Sharekhan.domain;
using ShareKhan.domain;

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

    }
}
