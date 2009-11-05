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

        [Test]
        public void ShouldReturnEffectiveValueGivenTheDateAndRateOfReturn()
        {
            DateTime transactionDate = new DateTime(2008, 3, 10);
            DateTime effectiveDate = new DateTime(2009, 11, 3);
            double effectiveRate = 0.3;

            var symbol = new Symbol("STOCK1");
            double delta = 0.005;

            double test1expected = -15441.744;
            var buy1 = new BuyTransaction(transactionDate, 
                new Stock(symbol, new Price(100.0), "Stock 1" ),
                100,
                new Price(100.0), 
                10,
                0.5);
            Assert.AreEqual(test1expected, buy1.GetEffectiveValue(effectiveDate, effectiveRate).Value, delta*-test1expected);

            double test2expected = 571.284;
            var sold1 = new SellTransaction(new DateTime(2008, 5, 21),
                                          new Stock(symbol, new Price(100), "Sold some the shares"),
                                           20,
                                           new Price(20),
                                           5,
                                           5);

            Assert.AreEqual(test2expected, sold1.GetEffectiveValue(effectiveDate, 0.3).Value, delta*test2expected);

            double test3expected = 148.068;
            DateTime effectiveDatePast = new DateTime(2007, 1, 1);
            var cashDividend = new CashDividendTransaction(
                                          new Stock(symbol, new Price(100), "Sold some the shares"),
                                           new Price(200),
                                           new DateTime(2008, 8, 25));

            Assert.AreEqual(test3expected, cashDividend.GetEffectiveValue(effectiveDatePast, 0.2).Value, delta * test3expected);

//            double test3expected = 0;
        }

        [Test]
        public void AmountShouldAccountForTaxAndBrokerage()
        {
            var symbol = new Symbol("STOCK1");
            var stock = new Stock(symbol, new Price(100.0), "My Stock 1");
            double delta = 0.005;

            Transaction transaction = new BuyTransaction(DateTime.Now, stock, 100, new Price(120.0), 0.0, 0.0);
            double expected = 12000.0;
            Assert.AreEqual(expected, transaction.Amount().Value, delta*expected);

            transaction = new BuyTransaction(DateTime.Now, stock, 10, new Price(120.0), 10.0, 15.0);
            expected = 1225.0;
            Assert.AreEqual(expected, transaction.Amount().Value, delta * expected);

            transaction = new SellTransaction(DateTime.Now, stock, 100, new Price(150.0), 50.0, 15.0);
            expected = 14935.0;
            Assert.AreEqual(expected, transaction.Amount().Value, delta * expected);
       
        }

        
        
        
    }
}
