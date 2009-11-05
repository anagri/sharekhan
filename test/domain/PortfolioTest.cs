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

            Instrument instrument = new MutualFund(symbol, new Price(100.00), "Reliance Mutual Fund", "SUNMF", "SUN Magma", "Growth");
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
        public void ShouldIncludeUnitDividendWhileGettingCurrentMarketValue()
        {
            Portfolio portfolio = new Portfolio();
            Symbol symbol = new Symbol("RILMF");

            Instrument instrument = new MutualFund(symbol, new Price(100.00), "Reliance Mutual Fund", "SUNMF", "SUN Magma", "Growth");
            instrument.Id = 10;

            IList<Symbol> symbolList = new List<Symbol>();
            symbolList.Add(symbol);

            Transaction transaction1 = new BuyTransaction(new DateTime(2009, 09, 09), instrument, 10, new Price(100.00), 10, 10);
            Transaction transaction2 = new SellTransaction(new DateTime(2009, 09, 10), instrument, 5, new Price(200.00), 10, 10);
            Transaction transaction3 = new UnitDividendTransaction(instrument, 5, new DateTime(2009, 09, 10));

            List<Transaction> listTransaction = new List<Transaction>();
            listTransaction.Add(transaction1);
            listTransaction.Add(transaction2);
            listTransaction.Add(transaction3);

            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.LookupBySymbol<Instrument>(symbol)).Returns(instrument);
            mock.Setup(repo => repo.ListAllSymbols<Symbol>()).Returns(symbolList);
            mock.Setup(repo => repo.ListTransactionsByInstrumentId<Transaction>(instrument.Id)).Returns(listTransaction);
            
            portfolio.Repository = mock.Object;
            Assert.AreEqual(1000.00, portfolio.CurrentMarketValue().Value);

            mock.VerifyAll();
        }

        [Test]
        public void ShouldRecordBuyofMf()
        {
            Portfolio portfolio = new Portfolio();
            Symbol symbol = new Symbol("RILMF");
            
            Instrument instrument = new MutualFund(symbol, new Price(100.00), "Reliance Mutual Fund", "SUNMF", "SUN Magma", "Growth");
            instrument.Id = 10;

            Transaction transaction1 = new BuyTransaction(new DateTime(2009, 09, 09), instrument, 10, new Price(100.00), 10, 10);
            List<Transaction> listTransaction = new List<Transaction> {transaction1};


            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.LookupBySymbol<Instrument>(symbol)).Returns(instrument);
            mock.Setup(repo => repo.ListTransactionsByInstrumentId<Transaction>(instrument.Id)).Returns(listTransaction);

            portfolio.Repository = mock.Object;
            Assert.AreEqual(1000, portfolio.CurrentMarketValue(symbol).Value);

            mock.VerifyAll();
        }
        [Test]
        public void ShouldRecordSaleofMf()
        {
            Portfolio portfolio = new Portfolio();
            Symbol symbol = new Symbol("RILMF");


            Instrument instrument = new MutualFund(symbol, new Price(100.00), "Reliance Mutual Fund", "SUNMF", "SUN Magma", "Growth");
            instrument.Id = 10;

            Transaction transaction1 = new SellTransaction(new DateTime(2009, 09, 09), instrument, 10, new Price(100.00), 10, 10);
            List<Transaction> listTransaction = new List<Transaction> { transaction1 };


            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.LookupBySymbol<Instrument>(symbol)).Returns(instrument);
            mock.Setup(repo => repo.ListTransactionsByInstrumentId<Transaction>(instrument.Id)).Returns(listTransaction);

            portfolio.Repository = mock.Object;
            Assert.AreEqual(-1000, portfolio.CurrentMarketValue(symbol).Value);

            mock.VerifyAll();
        }

        [Test]
        public void ShouldAbleToGetCurrentMarketValueOfPortfolio()
        {
            Portfolio portfolio = new Portfolio();

            Symbol symbol = new Symbol("RILMF");
            Instrument instrument = new MutualFund(symbol, new Price(100.00), "Reliance Mutual Fund", "SUNMF", "SUN Magma", "Growth");
            instrument.Id = 10;

            Symbol symbol2 = new Symbol("FICO");
            Instrument instrument2 = new MutualFund(symbol2, new Price(200.00), "FICO Corp", "SUNMF", "SUN Magma", "Growth");
            instrument2.Id = 11;

            Transaction transaction1 = new BuyTransaction(new DateTime(2009, 09, 09), instrument, 10, new Price(100.00), 10, 10);
            Transaction transaction2 = new SellTransaction(new DateTime(2009, 09, 10), instrument, 5, new Price(200.00), 10, 10);

            Transaction transaction3 = new BuyTransaction(new DateTime(2009, 09, 09), instrument, 10, new Price(200.00), 10, 10);

            List<Transaction> listTransaction = new List<Transaction>();
            listTransaction.Add(transaction1);
            listTransaction.Add(transaction2);

            List<Transaction> listTransaction2 = new List<Transaction>();
            listTransaction2.Add(transaction3);


            List<Symbol> symbolList = new List<Symbol>();
            symbolList.Add(symbol);
            symbolList.Add(symbol2);


            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.ListAllSymbols<Symbol>()).Returns(symbolList);
            mock.Setup(repo => repo.LookupBySymbol<Instrument>(symbol)).Returns(instrument);
            mock.Setup(repo => repo.LookupBySymbol<Instrument>(symbol2)).Returns(instrument2);
            mock.Setup(repo => repo.ListTransactionsByInstrumentId<Transaction>(instrument.Id)).Returns(listTransaction);
            mock.Setup(repo => repo.ListTransactionsByInstrumentId<Transaction>(instrument2.Id)).Returns(listTransaction2);
            
            portfolio.Repository = mock.Object;
            Assert.AreEqual(2500 , portfolio.CurrentMarketValue().Value);
        }


        [Test, Ignore]
        public void CanCalculateEffectiveRateOfReturn()
        {
            Symbol s1 = new Symbol("STK1");
            Symbol s2 = new Symbol("STK2");

            Instrument i1 = new Stock(s1, new Price(100.00), "My first stock");
            Instrument i2 = new Stock(s2, new Price(50.00), "Another Stock");

            //var transactions = new List<Transaction>
            //                       {
            //                           new BuyTransaction(new DateTime(2007, 1, 1), i1, 10, new Price(100.00), 10, 0.5),
            //                           new BuyTransaction(new DateTime(2007, 3, 1), i2, 20, new Price(50.0), 10, 0.5),
            //                           new SellTransaction(new DateTime(2008, 8, 1), i1, 5, new Price(600.0), 10, 0.5)
            //                       };

            var mockRepository = new Mock<IRepository>();

            Portfolio p = new Portfolio();
            Assert.AreEqual(0.719, p.GetEffectiveRateOfReturn(), 0.005);           

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