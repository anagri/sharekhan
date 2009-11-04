using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShareKhan.persist;
using Sharekhan.service;
using ShareKhan.domain;

namespace Sharekhan.domain
{
    [TestFixture]
    public class TransactionTest : PersistenceTestBase
    {
        [Test]
        public void Verify_Purchase_Transaction()
        {
            Instrument share = new Stock(new Symbol("REL"),new Price(10.00),"Reliance Power" );
            Transaction buyTransaction = new BuyTransaction(DateTime.Today, share, 5, new Price(10.00), 5.00, 3.00);
            repository.Save(buyTransaction);
            Assert.IsTrue(buyTransaction.Id > 0);
            Transaction transaction = repository.Lookup<Transaction>(buyTransaction.Id);
            Assert.IsNotNull(transaction);
            Assert.AreEqual(transaction.Id,buyTransaction.Id);
        }

        [Test]
        public void Verify_Sell_Transaction()
        {
            Stock share = new Stock(new Symbol("REL"), new Price(10.00), "Reliance Power");
            Transaction sellTransaction = new SellTransaction(DateTime.Today, share, 2, new Price(50.00), 5.00, 3.00);
            repository.Save(sellTransaction);
            Assert.IsTrue(sellTransaction.Id > 0);
            Transaction transaction = repository.Lookup<Transaction>(sellTransaction.Id);
            Assert.IsNotNull(transaction);
            Assert.AreEqual(transaction.Id, sellTransaction.Id);
        }        
        
        [Test]
        public void Verify_Cash_Dividend_Transaction()
        {
            Stock share = new Stock(new Symbol("REL"), new Price(10.00), "Reliance Power");
            Transaction cashDividendTransaction = new CashDividendTransaction(share, new Price(500), DateTime.Today);
            repository.Save(cashDividendTransaction);
            Assert.IsTrue(cashDividendTransaction.Id > 0);
            Transaction transaction = repository.Lookup<Transaction>(cashDividendTransaction.Id);
            Assert.IsNotNull(transaction);
            Assert.AreEqual(transaction.Id, cashDividendTransaction.Id);
        }

        [Test]
        public void Verify_Purchase_MutualFund()
        {

            Instrument mf = new MutualFund(new Symbol("SBI"), new Price(10.00), "SBI Mutual Fund", "SUNMF", "SUN Magma", "Growth");
            Transaction buyTransaction = new BuyTransaction(DateTime.Today, mf, 5, new Price(10.00), 5.00, 3.00);
            repository.Save(buyTransaction);
            Assert.IsTrue(buyTransaction.Id > 0);
            Transaction transaction = repository.Lookup<Transaction>(buyTransaction.Id);
            Assert.IsNotNull(transaction);
            Assert.AreEqual(transaction.Id, buyTransaction.Id);
        }

        [Test]
        public void Verify_Sell_MutualFund()
        {
            Instrument mf = new MutualFund(new Symbol("SBI"), new Price(10.00), "SBI Mutual Fund", "SUNMF", "SUN Magma", "Growth");
            Transaction sellTransaction = new SellTransaction(DateTime.Today, mf, 2, new Price(50.00), 5.00, 3.00);
            repository.Save(sellTransaction);
            Assert.IsTrue(sellTransaction.Id > 0);
            Transaction transaction = repository.Lookup<Transaction>(sellTransaction.Id);
            Assert.IsNotNull(transaction);
            Assert.AreEqual(transaction.Id, sellTransaction.Id);
        }


        [Test]
        public void ShouldBeAbleToCheckIfShortTerm()
        {
            Instrument relianceshare = new Stock(new Symbol("REL"), new Price(10.00), "Reliance Power");
            IList<Transaction> txList = new List<Transaction>();
            txList.Add(new BuyTransaction(new DateTime(2007, 1, 1), relianceshare, 5, new Price(10.00), 5.00,
                                          3.00));
            txList.Add(new BuyTransaction(DateTime.Today, relianceshare, 10, new Price(100.00), 5.00,
                                          2.00));
            txList.Add(new SellTransaction(new DateTime(2007, 1, 1), relianceshare, 12, new Price(50.00),
                                           1.00,
                                           5.00));
            txList.Add(new SellTransaction(DateTime.Today, relianceshare, 5, new Price(10.00), 5.00,
                                           3.00));

            var fy = new FinYear(2009);
            var longTerm = 0;
            var shortTerm = 0;

            foreach (var tx in txList)
            {
                if (tx.IsLongTerm(fy))
                {
                    longTerm++;
                }
                else
                {
                    shortTerm++;
                }
            }

            Assert.AreEqual(2, longTerm);
            Assert.AreEqual(2, shortTerm);
        }

        [Test]
        public void ShouldBeAbleToGetTransactionsForInstrument()
        {
            Instrument share = new Stock(new Symbol("REL"), new Price(10.00), "Reliance Power");
            Transaction buyTransaction1 = new BuyTransaction(new DateTime(2008, 06, 01), share, 10, new Price(10.00), 5.00, 3.00);
            Transaction sellTransaction1 = new SellTransaction(new DateTime(2008, 12, 01), share, 2, new Price(20.00), 5.00, 3.00);
            Transaction buyTransaction2 = new BuyTransaction(new DateTime(2009, 06, 01), share, 12, new Price(50.00), 5.00, 3.00);
            Transaction sellTransaction2 = new SellTransaction(new DateTime(2009, 12, 01), share, 20, new Price(100.00), 5.00, 3.00);

            List<Transaction> listTransaction = new List<Transaction> { buyTransaction1, sellTransaction1, buyTransaction2, sellTransaction2 };
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.ListTransactionsByInstrumentId<Transaction>(share.Id)).Returns(listTransaction);


            IList<Transaction> list = mock.Object.ListTransactionsByInstrumentId<Transaction>(share.Id);


            Assert.AreEqual(4, list.Count);
        }

        [Test, Ignore]
        public void ShouldReturnEffectiveValueGivenTheDateAndRateOfReturn()
        {
            // TODO: Assign Expected Value
            double expectedValue = 0.0;
            DateTime transactionDate = new DateTime(2008, 3, 10);
            DateTime effectiveDate = new DateTime(2009, 11, 3);
            double effectiveRate = 0.3;

            var tx = new BuyTransaction(transactionDate, 
                new Stock(new Symbol("STOCK1"), new Price(100.0), "Stock 1" ),
                100,
                new Price(100.0), 
                10,
                0.5);
            Assert.AreEqual(expectedValue, tx.GetEffectiveValue(effectiveDate, effectiveRate).Value, 0.005*expectedValue);
        }

        [Test]
        public void ShouldBeAbleToCalculateShortTermTaxForOneBuyAndSell()
        {
            Instrument share = new Stock(new Symbol("REL"), new Price(10.00), "Reliance Power");
            BuyTransaction buyTransaction = new BuyTransaction(new DateTime(2008, 06, 01), share, 10, new Price(10.00), 5.00, 3.00);
            SellTransaction sellTransaction = new SellTransaction(new DateTime(2008, 12, 01), share, 10, new Price(20.00), 5.00, 3.00);


            Price price = share.CalculateShortTermTax(buyTransaction, sellTransaction);
            Assert.AreEqual(20,price.Value);
            
        }


        [Test]
        public void ShouldNotCalculateShortTermTaxForLongTermTransactions()
        {
            Instrument share = new Stock(new Symbol("REL"), new Price(10.00), "Reliance Power");
            BuyTransaction buyTransaction = new BuyTransaction(new DateTime(2008, 06, 01), share, 10, new Price(10.00), 5.00, 3.00);
            SellTransaction sellTransaction = new SellTransaction(new DateTime(2009, 12, 01), share, 10, new Price(20.00), 5.00, 3.00);


            Price price = share.CalculateShortTermTax(buyTransaction, sellTransaction);
            Assert.AreEqual(0, price.Value);

        }
        
    }
}
