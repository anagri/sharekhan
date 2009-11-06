using System;
using NUnit.Framework;
using Sharekhan.domain;

namespace Sharekhan.test.domain

{
    [TestFixture]
    public class TransactionCollectionTest
    {

        [Test]
        public void ShouldCheckForUniqueInstrumentsInTransactions()
        {
            TransactionCollection ts = new TransactionCollection();
            Stock relianceShare = new Stock(new Symbol("RIL"), new Price(10.00), "Reliance Industries");
            Stock infyShare = new Stock(new Symbol("INF"), new Price(100.00), "Infosys");
            ts.Add(new BuyTransaction(new DateTime(1999, 3, 20), relianceShare, 10, new Price(1200), 0, 0));
            ts.Add(new SellTransaction(new DateTime(1999, 5, 20), relianceShare, 5, new Price(1300), 0, 0));
            ts.Add(new BuyTransaction(new DateTime(1999, 8, 20), infyShare, 67, new Price(110), 0, 0));
            ts.Add(new SellTransaction(new DateTime(2000, 9, 20), infyShare, 63, new Price(101), 0, 0));
            Assert.AreEqual(2, ts.GetAllUniqueInstruments().Count);

        }

        [Test]
        public void ShouldReturnTheNetValueGivenTheDateAndRateOfReturn()
        {
            var transactionCollection = new TransactionCollection();
            var symbol = new Symbol("STOCK1");
            var stock = new Stock(symbol, new Price(100.0), "My Stock 1");

            transactionCollection.Add(new BuyTransaction(new DateTime(2007, 1, 1),
                                                         stock,
                                                         100, new Price(80.0), 10, 5));
            transactionCollection.Add(new BuyTransaction(new DateTime(2007, 6, 1),
                                                         stock,
                                                         100, new Price(85.0), 15, 5));
            transactionCollection.Add(new SellTransaction(new DateTime(2008, 10, 5),
                                                         stock,
                                                         120, new Price(100.0), 15, 10));
            transactionCollection.Add(new CashDividendTransaction(stock,
                                                         new Price(800.0), new DateTime(2009, 5, 5)));

            var finalDate = new DateTime(2009, 11, 4);
            transactionCollection.Add(new SellTransaction(finalDate,
                                                          stock, 80, new Price(100), 0, 0));

            const double delta = 0.001;
            double expectedReturns;

            const double guessRate = 0.11;
            expectedReturns = -10784.37 + -10979.45 + 13406.76 + 842.97 + 8000;
            Assert.AreEqual(expectedReturns, transactionCollection.GetEffectiveReturn(finalDate, guessRate).Value,
                            delta);

            const double correctRate = 0.122557646;
            expectedReturns = -11134.96 + -11283.75 + 13570.97 + 847.74 + 8000;
            Assert.AreEqual(expectedReturns, transactionCollection.GetEffectiveReturn(finalDate, correctRate).Value,
                            delta);

        }


        [Test]
        public void ShouldBeAbleToIterativelyCalculateIRRWithInitialGuess()
        {
            var transactionCollection = new TransactionCollection();
            var delta = 1e-6;

            Assert.AreEqual(0, transactionCollection.GetXIRR(.1, .2).Value, delta);


            var symbol = new Symbol("STOCK1");
            var stock = new Stock(symbol, new Price(100.0), "My Stock 1");

            transactionCollection.Add(new BuyTransaction(new DateTime(2007, 1, 1),
                                                         stock,
                                                         100, new Price(80.0), 10, 5));
            transactionCollection.Add(new BuyTransaction(new DateTime(2007, 6, 1),
                                                         stock,
                                                         100, new Price(85.0), 15, 5));
            transactionCollection.Add(new SellTransaction(new DateTime(2008, 10, 5),
                                                         stock,
                                                         120, new Price(100.0), 15, 10));
            transactionCollection.Add(new CashDividendTransaction(stock,
                                                         new Price(800.0), new DateTime(2009, 5, 5)));

            var finalDate = new DateTime(2009, 11, 4);
            transactionCollection.Add(new SellTransaction(finalDate,
                                                          stock, 80, new Price(100), 0, 0));

            var goodLowerBoundGuess = 0.11;
            var goodUpperBoundGuess = 0.13;
            var correctRate = new Rate(0.122557646);
            Assert.AreEqual(correctRate.Value, transactionCollection.GetXIRR(goodLowerBoundGuess, goodUpperBoundGuess).Value, delta);

            var decentLowerBoundGuess = -.99;
            var decentUpperBoundGuess = 0.99;
            Assert.AreEqual(correctRate.Value, transactionCollection.GetXIRR(decentLowerBoundGuess, decentUpperBoundGuess).Value, delta);

            var badLowerBoundGuess = -.99;
            var badUpperBoundGuess = 100000;
            Assert.AreEqual(correctRate.Value, transactionCollection.GetXIRR(badLowerBoundGuess, badUpperBoundGuess).Value, delta);

        }
        

    }
}

