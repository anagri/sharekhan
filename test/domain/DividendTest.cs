using System;
using NUnit.Framework;
using ShareKhan.domain;

namespace Sharekhan.domain
{
    [TestFixture]
    public class DividendTest
    {
        [Test]
        public void ShouldCreateCashDividendTransactionForMutualFund()
        {
            var firstOfJan2008 = new DateTime(2008, 1, 1);
           
            var selectedMutualFund = new MutualFund(null, null, null, "SUNMF", "SUN Magma", "Growth");
            DividendTransaction expectedTransaction = new CashDividendTransaction(selectedMutualFund, new Price(100), firstOfJan2008);

            CashDividendTransaction actualTransaction =
                selectedMutualFund.CreateCashDividendTransaction(new Price(100),
                                                                 firstOfJan2008);
            Assert.AreEqual(expectedTransaction.UnitPrice, actualTransaction.UnitPrice);
            Assert.AreEqual(expectedTransaction.Instrument, actualTransaction.Instrument);
            Assert.AreEqual(expectedTransaction.Date, actualTransaction.Date);
        }

        [Test]
        public void ShouldCreateCashDividendTransactionForStock()
        {
            var firstOfJan2008 = new DateTime(2008, 1, 1);
            var selectedStock = new Stock(null, null, null);
            var expectedTransaction = new CashDividendTransaction(selectedStock, new Price(100), firstOfJan2008);

            CashDividendTransaction actualTransaction =
                selectedStock.CreateDividendTransaction(new Price(100),
                                                        firstOfJan2008);
            Assert.AreEqual(expectedTransaction.Date, actualTransaction.Date);
            Assert.AreEqual(expectedTransaction.Instrument, actualTransaction.Instrument);
            Assert.AreEqual(expectedTransaction.Date, actualTransaction.Date);
        }

        [Test]
        public void ShouldCreateUnitDividendTransactionForMutualFund()
        {
            var firstOfJan2008 = new DateTime(2008, 1, 1);
            
            var selectedMutualFund = new MutualFund(null, null, null, "SUNMF", "SUN Magma", "Growth");
            var expectedTransaction = new UnitDividendTransaction(selectedMutualFund,
                                                                  100,
                                                                  firstOfJan2008);

            UnitDividendTransaction actualTransaction =
                selectedMutualFund.CreateUnitDividendTransaction(100,
                                                                 firstOfJan2008);
            Assert.AreEqual(expectedTransaction.Quantity, actualTransaction.Quantity);
            Assert.AreEqual(expectedTransaction.Instrument, actualTransaction.Instrument);
            Assert.AreEqual(expectedTransaction.Date, actualTransaction.Date);
        }

        [Test]
        public void ShouldUpdateQuantityOnAdditionOfUnitDividend()
        {
            Portfolio d = new Portfolio();
            TransactionCollection ts = new TransactionCollection();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(1200), 1000, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(1300), 0, 1000));
            ts.Add(new UnitDividendTransaction(relianceShare, 2, new DateTime(1999, 5, 20)));

            int actualQty = 0;

            foreach (Transaction transaction in ts.TransactionList)
            {
                if ((transaction is BuyTransaction) || (transaction is UnitDividendTransaction))
                {
                    actualQty += transaction.Quantity;
                }
                else
                {
                    actualQty -= transaction.Quantity;
                }
            }

            Assert.AreEqual(7, actualQty);
        }
    }
}