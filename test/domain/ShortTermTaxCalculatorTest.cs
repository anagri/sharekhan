using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Sharekhan.domain;

namespace Sharekhan.test.domain
{
    [TestFixture]
    public class ShortTermTaxCalculatorTest
    {

        [Test]
        public void ShouldBeAbleToCalculateShortTermTaxForOneBuyAndSell()
        {
            Instrument share = new Stock(new Symbol("REL"), new Price(10.00), "Reliance Power");
            BuyTransaction buyTransaction = new BuyTransaction(new DateTime(2008, 06, 01), share, 10, new Price(10.00), 5.00, 3.00);
            SellTransaction sellTransaction = new SellTransaction(new DateTime(2008, 12, 01), share, 10, new Price(20.00), 5.00, 3.00);


            Price price = new ShortTermTaxCalculator(null).CalculateShortTermTaxForAPairOfTransactions(buyTransaction, sellTransaction);
            Assert.AreEqual(20, price.Value);

        }


        [Test]
        public void ShouldNotCalculateShortTermTaxForLongTermTransactions()
        {
            Instrument share = new Stock(new Symbol("REL"), new Price(10.00), "Reliance Power");
            BuyTransaction buyTransaction = new BuyTransaction(new DateTime(2008, 06, 01), share, 10, new Price(10.00), 5.00, 3.00);
            SellTransaction sellTransaction = new SellTransaction(new DateTime(2009, 12, 01), share, 10, new Price(20.00), 5.00, 3.00);


            Price price = new ShortTermTaxCalculator(null).CalculateShortTermTaxForAPairOfTransactions(buyTransaction, sellTransaction);
            Assert.AreEqual(0, price.Value);

        }
        [Test]
        public void ShouldCreateStackOfBuyTransactionsGivenAListOfTransactions()
        {
            Instrument share = new Stock(new Symbol("REL"), new Price(10.00), "Reliance Power");
            Transaction buyTransaction1 = new BuyTransaction(new DateTime(2008, 06, 01), share, 10, new Price(10.00), 5.00, 3.00);
            Transaction sellTransaction1 = new SellTransaction(new DateTime(2008, 12, 01), share, 2, new Price(20.00), 5.00, 3.00);
            Transaction buyTransaction2 = new BuyTransaction(new DateTime(2009, 06, 01), share, 12, new Price(50.00), 5.00, 3.00);
            Transaction sellTransaction2 = new SellTransaction(new DateTime(2009, 12, 01), share, 20, new Price(100.00), 5.00, 3.00);

            List<Transaction> listTransaction = new List<Transaction> { buyTransaction1, sellTransaction1, buyTransaction2, sellTransaction2 };

            Stack buyStack = new ShortTermTaxCalculator(listTransaction).CreateBuyTransactionStack();
            Assert.AreEqual(2, buyStack.Count);
            Assert.AreEqual(buyTransaction1, buyStack.Pop());
            Assert.AreEqual(buyTransaction2, buyStack.Pop());

        }

        [Test]
        public void ShouldCreateStackOfSellTransactionsGivenAListOfTransactions()
        {
            Instrument share = new Stock(new Symbol("REL"), new Price(10.00), "Reliance Power");
            Transaction buyTransaction1 = new BuyTransaction(new DateTime(2008, 06, 01), share, 10, new Price(10.00), 5.00, 3.00);
            Transaction sellTransaction1 = new SellTransaction(new DateTime(2008, 12, 01), share, 2, new Price(20.00), 5.00, 3.00);
            Transaction buyTransaction2 = new BuyTransaction(new DateTime(2009, 06, 01), share, 12, new Price(50.00), 5.00, 3.00);
            Transaction sellTransaction2 = new SellTransaction(new DateTime(2009, 12, 01), share, 20, new Price(100.00), 5.00, 3.00);

            List<Transaction> listTransaction = new List<Transaction> { buyTransaction1, sellTransaction1, buyTransaction2, sellTransaction2 };

            Stack sellStack = new ShortTermTaxCalculator(listTransaction).CreateSellTransactionStack();
            Assert.AreEqual(2, sellStack.Count);
            Assert.AreEqual(sellTransaction1, sellStack.Pop());
            Assert.AreEqual(sellTransaction2, sellStack.Pop());

        }

        

    }
}
