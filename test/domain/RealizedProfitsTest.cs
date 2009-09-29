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
            DummyPortFolio d = new DummyPortFolio();
            TransactionStatement ts = new TransactionStatement();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            ts.addTransaction(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(1300), 0, 0));
            d.BuildDictionariesWithSellingAmounts(ts.listOfTransactions());
            Assert.AreEqual(1300*5, d.realizedProfitsDictionary[relianceShare]);
            Assert.AreEqual(5, d.qty[relianceShare]);
        }

        [Test]
        public void ShouldBuildDictionariesWithSoldAmountsForCaseWithoutTaxOrBrokerage()
        {
            DummyPortFolio d = new DummyPortFolio();
            TransactionStatement ts = new TransactionStatement();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            Stock suzlonEnergyShare = new Stock(new Symbol("SUZ"), new Price(1000.00), "Suzlon Energy");
            ts.addTransaction(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(13), 0, 0));
            ts.addTransaction(new SellTransaction(new DateTime(1999, 7, 20), relianceShare, 5, new Price(10), 0, 0));
            ts.addTransaction(new SellTransaction(new DateTime(1999, 5, 15), suzlonEnergyShare, 8, new Price(20), 0, 0));
            ts.addTransaction(new SellTransaction(new DateTime(1999, 5, 25), suzlonEnergyShare, 10, new Price(10), 0, 0));
            d.BuildDictionariesWithSellingAmounts(ts.listOfTransactions());
            Assert.AreEqual(115, d.realizedProfitsDictionary[relianceShare]);
            Assert.AreEqual(260, d.realizedProfitsDictionary[suzlonEnergyShare]);
            Assert.AreEqual(10, d.qty[relianceShare]);
            Assert.AreEqual(18, d.qty[suzlonEnergyShare]);
        }


        [Test]
        public void ShouldUpdateRealizedProfitsTrivialCase()
        {
            DummyPortFolio d = new DummyPortFolio();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            Dictionary<Instrument, double> mockRP = new Dictionary<Instrument, double>();
            mockRP.Add(relianceShare, 677.25);
            Dictionary<Instrument, int> mockQty = new Dictionary<Instrument, int>();
            mockQty.Add(relianceShare, 22);
            d.realizedProfitsDictionary = mockRP;
            d.qty = mockQty;

            TransactionStatement ts = new TransactionStatement();
            ts.addTransaction(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(25), 0, 0));
            d.UpdateRealizedProfits(ts.listOfTransactions());
            Assert.AreEqual(427.25, d.realizedProfitsDictionary[relianceShare]);
            Assert.AreEqual(12, d.qty[relianceShare]);
        }

        [Test]
        public void ShouldUpdateRealizedProfitsCaseWithMoreBuyThanSell()
        {
            DummyPortFolio d = new DummyPortFolio();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            Dictionary<Instrument, double> mockRP = new Dictionary<Instrument, double>();
            mockRP.Add(relianceShare, 677.25);
            Dictionary<Instrument, int> mockQty = new Dictionary<Instrument, int>();
            mockQty.Add(relianceShare, 5);
            d.realizedProfitsDictionary = mockRP;
            d.qty = mockQty;

            TransactionStatement ts = new TransactionStatement();
            ts.addTransaction(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(25), 0, 0));
            d.UpdateRealizedProfits(ts.listOfTransactions());
            Assert.AreEqual(552.25, d.realizedProfitsDictionary[relianceShare]);
            Assert.AreEqual(0, d.qty[relianceShare]);
        }

        [Test]
        public void ShouldCalculateRealizedProfitsDumbCaseWithoutTaxOrBrokerage()
        {
            DummyPortFolio d = new DummyPortFolio();
            TransactionStatement ts = new TransactionStatement();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            Stock suzlonEnergyShare = new Stock(new Symbol("SUZ"), new Price(1000.00), "Suzlon Energy");
            ts.addTransaction(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(1200), 0, 0));
            ts.addTransaction(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(1300), 0, 0));
            Assert.AreEqual(500, d.CalculateRealizedProfits(ts));
        }

        [Test]
        public void ShouldCalculateRealizedProfitsComplexCaseWithoutTaxOrBrokerage()
        {
            DummyPortFolio d = new DummyPortFolio();
            TransactionStatement ts = new TransactionStatement();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            Stock suzlonEnergyShare = new Stock(new Symbol("SUZ"), new Price(1000.00), "Suzlon Energy");
            ts.addTransaction(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(12), 0, 0));
            ts.addTransaction(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(13), 0, 0));
            ts.addTransaction(new SellTransaction(new DateTime(1999, 7, 20), relianceShare, 3, new Price(13), 0, 0));
            ts.addTransaction(new BuyTransaction(new DateTime(1999, 7, 20), suzlonEnergyShare, 11, new Price(15), 0, 0));
            ts.addTransaction(new SellTransaction(new DateTime(1999, 10, 20), suzlonEnergyShare, 5, new Price(20), 0, 0));
            ts.addTransaction(new SellTransaction(new DateTime(1999, 10, 20), suzlonEnergyShare, 3, new Price(20), 0, 0));
            ts.addTransaction(new BuyTransaction(new DateTime(1999, 10, 20), suzlonEnergyShare, 4, new Price(18), 0, 0));
            Assert.AreEqual(48, d.CalculateRealizedProfits(ts));
        }

    }

    public class DummyPortFolio
    {
        //TODO : Make Sure List is sorted in terms of Date
        public Dictionary<Instrument, double> realizedProfitsDictionary = new Dictionary<Instrument, double>();
        public Dictionary<Instrument, int> qty = new Dictionary<Instrument, int>();
        
        public double CalculateRealizedProfits(TransactionStatement statement)
        {
            double realizedProfits = 0.0;
            List<Transaction> listOfTransactions = statement.listOfTransactions();

            BuildDictionariesWithSellingAmounts(listOfTransactions);

            UpdateRealizedProfits(listOfTransactions);

            foreach (KeyValuePair<Instrument, double> kvp in realizedProfitsDictionary)
            {
                realizedProfits += kvp.Value;
            }
            return realizedProfits;
        }

        public void BuildDictionariesWithSellingAmounts(List<Transaction> listOfTransactions)
        {
            foreach (var transaction in listOfTransactions)
            {
                if (transaction.GetType() != typeof (SellTransaction))
                    continue;
                if (!realizedProfitsDictionary.ContainsKey(transaction.Instrument))
                {
                    realizedProfitsDictionary[transaction.Instrument] = 0;
                    qty[transaction.Instrument] = 0;
                }
                realizedProfitsDictionary[transaction.Instrument] += transaction.UnitPrice.Value*transaction.Quantity;
                qty[transaction.Instrument] += transaction.Quantity;
            }
        }

        public void UpdateRealizedProfits(List<Transaction> listOfTransactions)
        {
            foreach (var transaction in listOfTransactions)
            {
                if (transaction.GetType() != typeof(BuyTransaction) || !qty.ContainsKey(transaction.Instrument) || qty[transaction.Instrument] == 0)
                    continue;

                double buyingPrice;
                if (qty[transaction.Instrument] < transaction.Quantity)
                {
                    buyingPrice = qty[transaction.Instrument]*transaction.UnitPrice.Value;
                    qty[transaction.Instrument] = 0;
                }
                else
                {
                    buyingPrice = transaction.Quantity*transaction.UnitPrice.Value;
                    qty[transaction.Instrument] -= transaction.Quantity;
                }
                realizedProfitsDictionary[transaction.Instrument] -= buyingPrice;
            }
        }
    }
}
