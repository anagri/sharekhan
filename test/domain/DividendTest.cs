using System;
using NUnit.Framework;

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
    }
}