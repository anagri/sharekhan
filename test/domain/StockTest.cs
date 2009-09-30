using System;
using NUnit.Framework;
using Sharekhan.domain;
using Sharekhan.service;

namespace ShareKhan.domain
{
    [TestFixture]
    public class StockTest : PersistenceTestBase
    {
        [Test]
        public void ShouldBeAbleToCreateSaveAStock()
        {
            Price stockUnitPrice = new Price(10.00);

            Instrument relianceStock = new Stock(new Symbol("REL"), stockUnitPrice, "Reliance Power Stock");
            repository.Save(relianceStock);
            Console.WriteLine(relianceStock.Id);
            Assert.IsTrue(relianceStock.Id>0);

            var savedStock = repository.Lookup<Instrument>(relianceStock.Id);
            Console.WriteLine(savedStock.Id);

            Assert.AreEqual(relianceStock.Id,savedStock.Id);

        }


//        [Test]
//        public void ShouldBeAbleToViewTheStockGivenTheSymbol()
//        {
//            Price stockUnitPrice = new Price(10.00);
//
//            Instrument relianceStock = new Stock(new Symbol("REL"), stockUnitPrice, "Reliance Power Stock");
//            repository.Save(relianceStock);
//            Console.WriteLine(relianceStock.Symbol.Value);
//
//            var savedStockSymbol = repository.LookupBySymbol<Instrument>(new Symbol("REL"));
//
//            Assert.AreEqual(savedStockSymbol.Symbol,relianceStock.Symbol);
//
//        }

        [Test]
        public void ShouldUpdateStockPrice()
        {
            var fourThousand = new Price(4000);

            Instrument instrument = new Stock(new Symbol("SUN"), fourThousand, "Sun MF");
            repository.Save(instrument);

            var lookedUpObject = repository.Lookup<Instrument>(instrument.Id);
            Assert.AreEqual(new Price(4000), lookedUpObject.CurrentPrice);

            var newPrice = new Price(2500);
            instrument.UpdateCurrentPrice(newPrice);

            lookedUpObject = repository.Lookup<Instrument>(instrument.Id);
            Assert.AreEqual(new Price(2500), lookedUpObject.CurrentPrice);
        }
    }
}