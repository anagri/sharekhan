using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Sharekhan.domain;
using ShareKhan.service;



namespace Sharekhan.service
{
    [TestFixture]
    class RepositoryTest 
    {
        private IRepository repository;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            repository = new Repository();
        }

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



        [Test]
        public void TestSave()
        {
<<<<<<< HEAD:test/service/RepositoryTest.cs
            /*
            Instrument instrument = new MutualFund("Mutual003","MF001","Mutal Fund1",new Price(100));
            IRepository repository = new Repository(session);
            Assert.AreEqual("Mutual003", instrument.Id);
            repository.Save(instrument);
            Assert.AreNotEqual("",instrument.Id);
            Assert.AreEqual("Mutual003", instrument.Id);
             */
=======
            Price hundredRuppees = new Price(100);
            Symbol relianceMutuals = new Symbol("RELTICK");
            Instrument instrument = new MutualFund(relianceMutuals,hundredRuppees,"Test Fund");

            Assert.AreEqual(0, instrument.Id);
            repository.Save(instrument);
            Assert.AreNotEqual(0, instrument.Id);
            Assert.True(instrument.Id > 0);
>>>>>>> e855e45eebbb02c10271b68afb938583ab9af207:test/service/RepositoryTest.cs
        }

        [Test]
        public void TestLookupAfterSave()
        {
            Price hundredRuppees = new Price(100);
            Symbol relianceMutuals = new Symbol("RELTICK");
            Instrument instrument = new MutualFund(relianceMutuals, hundredRuppees, "Test Fund");

            
            repository.Save(instrument);

            Instrument actual = repository.Lookup<Instrument>(instrument.Id);

            Assert.AreEqual(typeof(MutualFund), actual.GetType());
            Assert.AreEqual(instrument,actual);
            


            //Instrument actual = repository.Lookup<Instrument>();

           
        }
    }
}
