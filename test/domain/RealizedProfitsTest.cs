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
            int buyUnitPrice = 1200;
            int buyBrokerage = 100;
            int sellBrokerage = 100;
            int buyQuantity = 10;
            int sellQuantity = 5;
            int sellUnitPrice = 1300;


            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, buyQuantity, new Price(buyUnitPrice), buyBrokerage, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, sellQuantity, new Price(sellUnitPrice), 0, sellBrokerage));
            int dividend = 50;
            ts.Add(new CashDividendTransaction(relianceShare,new Price(dividend),new DateTime(1999, 5, 20)));

            Assert.AreEqual((sellUnitPrice-buyUnitPrice) * sellQuantity - sellBrokerage -buyBrokerage + dividend, d.CalculateRealizedProfits(ts));
        }

        [Test]
        public void ShouldIncludeUnitDividendSoldInRealizedProfit()
        {
            Portfolio d = new Portfolio();
            TransactionCollection ts = new TransactionCollection();

            var selectedMutualFund = new MutualFund(null, null, null, "SUNMF", "SUN Magma", "Growth");
            int buyUnitPrice = 1200;
            int buyBrokerage = 100;
            int sellBrokerage = 100;
            int buyQuantity = 10;
            int sellQuantity = 10;

            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), selectedMutualFund, buyQuantity, new Price(buyUnitPrice), buyBrokerage, 0));
            int  sellUnitPrice = 1300;
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), selectedMutualFund, 5, new Price(sellUnitPrice), 0, sellBrokerage));
            int unitReceived = 2;
            ts.Add(new UnitDividendTransaction(selectedMutualFund, unitReceived, new DateTime(1999, 5, 20)));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), selectedMutualFund, 7, new Price(sellUnitPrice), 0, sellBrokerage));

            Assert.AreEqual((sellUnitPrice - buyUnitPrice) * sellQuantity - buyBrokerage - sellBrokerage + sellUnitPrice * unitReceived - sellBrokerage, d.CalculateRealizedProfits(ts));
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

        [Test,Ignore]
        public void ShouldCalculateARealisedProfitForTermDeposit()
        {
            Portfolio portfolio = new Portfolio();
            TransactionCollection tc = new TransactionCollection();
            Instrument singliePayout = new SinglePayOut(new Term(2),new Price(10000),new Symbol("CITI"),"Two year deposit at CITI",new InterestRate(10));
            tc.Add(new BuyTransaction(new DateTime(2009, 11, 01), singliePayout, 1, new Price(10000), 0, 0));
            Assert.AreEqual(0,portfolio.CalculateRealizedProfits(tc));
        }
    }
}
