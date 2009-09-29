using System;
using Moq;
using NUnit.Framework;
using Sharekhan.domain;
using ShareKhan.persist;
using System.Collections.Generic;

namespace ShareKhan.domain
{
    [TestFixture]
    public class PorfolioTest
    {
        [Test]
        public void ShouldAbleToGetCurrentMarketValueOfInstrument()
        {
            Portfolio portfolio = new Portfolio();
            Symbol symbol = new Symbol("RILMF");

            Instrument instrument = new MutualFund(symbol, new Price(100.00), "Reliance Mutual Fund");
            instrument.Id = 10;

            Transaction transaction1 = new BuyTransaction(new DateTime(2009, 09, 09), instrument, 10, new Price(100.00), 10, 10);
            Transaction transaction2 = new SellTransaction(new DateTime(2009, 09, 10), instrument, 5, new Price(200.00), 10, 10);
            List<Transaction> listTransaction = new List<Transaction>();
            listTransaction.Add(transaction1);
            listTransaction.Add(transaction2);

            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.LookupBySymbol<Instrument>(symbol)).Returns(instrument);
            mock.Setup(repo => repo.ListTransactionsByInstrumentId<Transaction>(instrument.Id)).Returns(listTransaction);
            
            portfolio.Repository = mock.Object;
            Assert.AreEqual(500.00, portfolio.CurrentMarketValue(symbol).Value);

            mock.VerifyAll();
         }

        [Test]
        public void ShouldAbleToGetCurrentValueOfPorfolio()
        {
//            Portfolio portfolio = new Portfolio();
//            Symbol symbol = new Symbol("RILMF");
//
//            Instrument instrument = new MutualFund(symbol, new Price(100.00), "Reliance Mutual Fund");
//            instrument.Id = 10;
//
//            Transaction transaction1 = new BuyTransaction(new DateTime(2009, 09, 09), instrument, 10, new Price(100.00), 10, 10);
//            Transaction transaction2 = new SellTransaction(new DateTime(2009, 09, 10), instrument, 5, new Price(200.00), 10, 10);
//            List<Transaction> listTransaction = new List<Transaction>();
//            listTransaction.Add(transaction1);
//            listTransaction.Add(transaction2);
//
//            var mock = new Mock<IRepository>();
//            mock.Setup(repo => repo.LookupBySymbol<Instrument>(symbol)).Returns(instrument);
//            mock.Setup(repo => repo.ListTransactionsByInstrumentId<Transaction>(instrument.Id)).Returns(listTransaction);
//
//            portfolio.Repository = mock.Object;
//            Assert.AreEqual(500.00, portfolio.CurrentMarketValue());
            
        }
    }
}