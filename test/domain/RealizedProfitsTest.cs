using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sharekhan.domain;
using ShareKhan.domain;
using Moq;


namespace Sharekhan.test.domain
{
    [TestFixture]
    public class RealizedProfitsTest
    {
        [Test]
        public void ShouldBuildDictionariesWithSoldAmountsForDumbCase()
        {
            TransactionCollection ts =  new TransactionCollection();
            Dictionary<Instrument, Price> tbl = new Dictionary<Instrument, Price>();
            Dictionary<Instrument, int> qty = new Dictionary<Instrument, int>();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(1300), 0, 0));
            ts.BuildDictionariesWithSellingAmounts(tbl, qty);

            Assert.AreEqual(1300 * 5, tbl[relianceShare].Value);
            Assert.AreEqual(5, qty[relianceShare]);
        }

        [Test]
        public void ShouldBuildDictionariesWithSoldAmountsForCaseWithoutTaxOrBrokerage()
        {
            TransactionCollection ts = new TransactionCollection();
            Dictionary<Instrument, Price> tbl = new Dictionary<Instrument, Price>();
            Dictionary<Instrument, int> qty = new Dictionary<Instrument, int>();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            Stock suzlonEnergyShare = new Stock(new Symbol("SUZ"), new Price(1000.00), "Suzlon Energy");
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(13), 0, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 7, 20), relianceShare, 5, new Price(10), 0, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 15), suzlonEnergyShare, 8, new Price(20), 0, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 25), suzlonEnergyShare, 10, new Price(10), 0, 0));
            ts.BuildDictionariesWithSellingAmounts(tbl, qty);
            Assert.AreEqual(115, tbl[relianceShare].Value);
            Assert.AreEqual(260, tbl[suzlonEnergyShare].Value);
            Assert.AreEqual(10, qty[relianceShare]);
            Assert.AreEqual(18, qty[suzlonEnergyShare]);
        }


        [Test]
        public void ShouldUpdateRealizedProfitsTrivialCase()
        {
            TransactionCollection ts = new TransactionCollection();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            Dictionary<Instrument, Price> mockRP = new Dictionary<Instrument, Price>();
            mockRP.Add(relianceShare, new Price(677.25));
            Dictionary<Instrument, int> mockQty = new Dictionary<Instrument, int>();
            mockQty.Add(relianceShare, 22);

            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(25), 0, 0));
            ts.UpdateRealizedProfits(mockRP, mockQty);
            Assert.AreEqual(427.25, mockRP[relianceShare].Value);
            Assert.AreEqual(12, mockQty[relianceShare]);
        }

        [Test]
        public void ShouldUpdateRealizedProfitsCaseWithMoreBuyThanSell()
        {
            TransactionCollection ts = new TransactionCollection();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            Dictionary<Instrument, Price> mockRP = new Dictionary<Instrument, Price>();
            mockRP.Add(relianceShare, new Price(677.25));
            Dictionary<Instrument, int> mockQty = new Dictionary<Instrument, int>();
            mockQty.Add(relianceShare, 5);

            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(25), 0, 0));
            ts.UpdateRealizedProfits(mockRP, mockQty);
            Assert.AreEqual(552.25, mockRP[relianceShare].Value);
            Assert.AreEqual(0, mockQty[relianceShare]);
        }

        [Test]
        public void ShouldCalculateRealizedProfitsDumbCaseWithoutTaxOrBrokerage()
        {
            Portfolio d = new Portfolio();
            TransactionCollection ts = new TransactionCollection();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(1200), 0, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(1300), 0, 0));
            Assert.AreEqual(500, d.CalculateRealizedProfits(ts));
        }


        [Test]
        public void ShouldCalculateRealizedLossesDumbCaseWithoutTaxOrBrokerage()
        {
            Portfolio d = new Portfolio();
            TransactionCollection ts = new TransactionCollection();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            Stock suzlonEnergyShare = new Stock(new Symbol("SUZ"), new Price(1000.00), "Suzlon Energy");
            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(1300), 0, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(1200), 0, 0));
            Assert.AreEqual(-500, d.CalculateRealizedProfits(ts));
        }

  

        [Test]
        public void ShouldCalculateRealizedProfitsComplexCaseWithoutTaxOrBrokerage()
        {
            Portfolio d = new Portfolio();
            TransactionCollection ts = new TransactionCollection();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            Stock suzlonEnergyShare = new Stock(new Symbol("SUZ"), new Price(1000.00), "Suzlon Energy");
            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(12), 0, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(13), 0, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 7, 20), relianceShare, 3, new Price(13), 0, 0));
            ts.Add(new BuyTransaction(new DateTime(1999, 7, 20), suzlonEnergyShare, 11, new Price(15), 0, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 10, 20), suzlonEnergyShare, 5, new Price(20), 0, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 10, 20), suzlonEnergyShare, 3, new Price(20), 0, 0));
            ts.Add(new BuyTransaction(new DateTime(1999, 10, 20), suzlonEnergyShare, 4, new Price(18), 0, 0));
            Assert.AreEqual(48, d.CalculateRealizedProfits(ts));
        }

        [Test]
        public void ShouldCalculateRealizedProfitsAtInstrumentLevel()
        {
            Portfolio d = new Portfolio();
            TransactionCollection ts = new TransactionCollection();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(1200), 0, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(1300), 0, 0));
            Assert.AreEqual(500, d.CalculateRealizedProfits(ts,relianceShare));
        }

        [Test]
        public void ShouldCalculateRealizedProfitsAtInstrumentLevelComplexCase()
        {
            Portfolio d = new Portfolio();
            TransactionCollection ts = new TransactionCollection();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            Stock infyShare = new Stock(new Symbol("INF"), new Price(100.00), "Infosys");
            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(1200), 0, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(1300), 0, 0));
            ts.Add(new BuyTransaction(new DateTime(1999, 8, 20), infyShare, 67, new Price(110), 0, 0));
            ts.Add(new SellTransaction(new DateTime(2000, 9, 20), infyShare, 63, new Price(101), 0, 0));
            Assert.AreEqual(500, d.CalculateRealizedProfits(ts, relianceShare));
            Assert.AreEqual(-567, d.CalculateRealizedProfits(ts, infyShare));
        }

        [Test]
        public void ShouldCalculateRealizedProfitsWithTaxAndBrokerage()
        {
            Portfolio d = new Portfolio();
            TransactionCollection ts = new TransactionCollection();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            Stock suzlonEnergyShare = new Stock(new Symbol("SUZ"), new Price(1000.00), "Suzlon Energy");
            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(1200), 1000, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(1300), 0, 1000));
            Assert.AreEqual(-1500, d.CalculateRealizedProfits(ts));
            
        }
        
        [Test]
        public void ShouldIncludeDividendInRealizedProfit()
        {
            Portfolio d = new Portfolio();
            TransactionCollection ts = new TransactionCollection();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(1200), 100, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(1300), 0, 100));
            ts.Add(new CashDividendTransaction(relianceShare,new Price(50),new DateTime(1999, 5, 20)));

            Assert.AreEqual(350, d.CalculateRealizedProfits(ts));

            var selectedMutualFund = new MutualFund(null, null, null, "SUNMF", "SUN Magma", "Growth");
            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), selectedMutualFund, 10, new Price(1200), 100, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), selectedMutualFund, 5, new Price(1300), 0, 100));
            ts.Add(new UnitDividendTransaction(selectedMutualFund, 2, new DateTime(1999, 5, 20)));
            
            Assert.AreEqual(650, d.CalculateRealizedProfits(ts));
        }


        [Test,Ignore]
        public void ShouldCalculateRealizedProfitsComplexCaseWithDateMismatch()
        {
            Portfolio d = new Portfolio();
            TransactionCollection ts = new TransactionCollection();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 5, new Price(12), 0, 0));
            ts.Add(new BuyTransaction(new DateTime(1999, 2, 20), relianceShare, 5, new Price(10), 0, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(13), 0, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 7, 20), relianceShare, 3, new Price(13), 0, 0));
            Assert.AreEqual(18, d.CalculateRealizedProfits(ts));
        }
    }

}
