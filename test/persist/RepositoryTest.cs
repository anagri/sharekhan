﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sharekhan.domain;
using ShareKhan.persist;

namespace ShareKhan.persist
{
    [TestFixture]
    public class RepositoryTest
    {

        private IRepository repository;
        
        [SetUp]
        public void SetUp()
        {
            repository.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            repository.RollbackTransaction();
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            repository = new Repository();
        }

        [Test]
        public void TestSave()
        {
            var hundredRuppees = new Price(100);
            var relianceMutuals = new Symbol("RELTICK");


            Instrument instrument = new MutualFund(relianceMutuals, hundredRuppees, "Test Fund", "SUNMF", "SUN Magma", "Growth");

            Assert.AreEqual(0, instrument.Id);
            repository.Save(instrument);
            Assert.AreNotEqual(0, instrument.Id);
            Assert.IsNotNull(instrument.Id);
        }


        [Test]
        public void TestLookupAfterSave()
        {
            var hundredRuppees = new Price(100);
            var relianceMutuals = new Symbol("RELTICK");


            Instrument instrument = new MutualFund(relianceMutuals, hundredRuppees, "Test Fund", "SUNMF", "SUN Magma", "Growth");


            repository.Save(instrument);

            var actual = repository.Lookup<Instrument>(instrument.Id);

            Assert.AreEqual(typeof(MutualFund), actual.GetType());

            Instrument expectedMutualFund = new MutualFund(relianceMutuals, hundredRuppees, "Test Fund", "SUNMF", "SUN Magma", "Growth")
                                                {Id = actual.Id};
            Assert.AreEqual(expectedMutualFund, actual);
            Assert.AreNotEqual(0, instrument.Id);
            Assert.True(instrument.Id > 0);

        }

        
        [Test]
        public void TestListAllSymbols()
        {
            var hundredRuppees = new Price(100);
            var relianceMutuals = new Symbol("RELTICK");

            Instrument instrument = new MutualFund(relianceMutuals, hundredRuppees, "Test Fund", "SUNMF", "SUN Magma", "Growth");


            repository.Save(instrument);
            
            IList<Symbol> list = repository.ListAllSymbols<Symbol>();

            Assert.AreEqual(1,list.Count);
            Assert.AreEqual("RELTICK",list[0].Value);
        }

        [Test]
        public void TestLookupBySymbol()
        {
            var hundredRuppees = new Price(100);
            var relianceMutuals = new Symbol("RELTICK");

            Instrument instrument = new MutualFund(relianceMutuals, hundredRuppees, "Test Fund", "SUNMF", "SUN Magma", "Growth");
            repository.Save(instrument);

            var symbol = new Symbol("RELTICK");
            Instrument resultInstrument = repository.LookupBySymbol<Instrument>(symbol);
            Assert.AreEqual(instrument, resultInstrument);
        }

       [Test]
        
        public void TestListTransactionsByInstrumentId()
        {
            var hundredRuppees = new Price(100);
            var relianceMutuals = new Symbol("RELTICK");
            Instrument instrument = new MutualFund(relianceMutuals, hundredRuppees, "Test Fund", "SUNMF", "SUN Magma", "Growth");
            repository.Save(instrument);

            Transaction buyTransaction = new BuyTransaction(DateTime.Now, instrument, 10, new Price(1000), 100, 100);
            repository.Save(buyTransaction);

            IList<Transaction> translist=repository.ListTransactionsByInstrumentId<Transaction>(instrument.Id);

            Assert.AreEqual(1,repository.ListTransactionsByInstrumentId<Transaction>(instrument.Id).Count);

        } 



    }
}