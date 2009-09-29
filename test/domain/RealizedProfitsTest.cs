using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sharekhan.domain;
using ShareKhan.domain;
using Moq;


namespace ShareKhan.test.domain
{
    [TestFixture]
    public class RealizedProfitsTest
    {
        [Test]
        public void ShouldCalculateRealizedProfitsDumbCaseWithoutTaxOrBrokerages()
        {
            DummyPortFolio d= new DummyPortFolio();
            TransactionStatement ts = new TransactionStatement();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            Stock suzlonEnergyShare = new Stock(new Symbol("SUZ"), new Price(1000.00), "Suzlon Energy");
            ts.addTransaction(new BuyTransaction(new DateTime(1999, 3, 20),relianceShare, 10, new Price(1200), 0,0));
            ts.addTransaction(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(1300), 0, 0));
            Assert.AreEqual(500, d.CalculateRealizedProfits(ts));
        }
    }

    public class DummyPortFolio
    {
        public double CalculateRealizedProfits(TransactionStatement statement)
        {
            double realizedProfits = 0.0;
            Dictionary<Instrument, double> realizedProfitsDictionary = new Dictionary<Instrument, double>();
            Dictionary<Instrument, int> qty = new Dictionary<Instrument, int>();
            List<Transaction> listOfTransactions = statement.listOfTransactions();
            foreach (var transaction in listOfTransactions)
            {
                if (transaction.GetType() != typeof(SellTransaction))
                    continue;
                if (!realizedProfitsDictionary.ContainsKey(transaction.Instrument))
                {
                    realizedProfitsDictionary[transaction.Instrument] = 0;
                    qty[transaction.Instrument] = 0;
                }
                realizedProfitsDictionary[transaction.Instrument] += transaction.UnitPrice.Value * transaction.Quantity;
                qty[transaction.Instrument] += transaction.Quantity;
                    
            }

            foreach (var transaction in listOfTransactions)
            {
                if (transaction.GetType() != typeof(BuyTransaction))
                    continue;
                if (!qty.ContainsKey(transaction.Instrument))
                    continue;
                double buyingPrice;
                if (qty[transaction.Instrument] < transaction.Quantity)
                {
                    buyingPrice = qty[transaction.Instrument] * transaction.UnitPrice.Value;
                } 
                else
                {
                    buyingPrice = transaction.Quantity * transaction.UnitPrice.Value;
                    qty[transaction.Instrument] -= transaction.Quantity;
                }
                realizedProfitsDictionary[transaction.Instrument] -= buyingPrice;
                
            }

            foreach (KeyValuePair<Instrument,double> kvp in realizedProfitsDictionary)
            {
                realizedProfits += kvp.Value;    
            }            
            return realizedProfits;
        }
    }
}
