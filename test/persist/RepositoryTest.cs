using System;
using NUnit.Framework;
using Sharekhan.domain;
using ShareKhan.service;

namespace Sharekhan.service
{
    [TestFixture]
    class RepositoryTest
    {
        #region Setup/Teardown

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

        #endregion

        private IRepository repository;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            repository = new Repository();
        }


        [Test]
        public void TestLookupAfterSave()
        {
            var hundredRuppees = new Price(100);
            var relianceMutuals = new Symbol("RELTICK");
            Instrument instrument = new MutualFund(relianceMutuals, hundredRuppees, "Test Fund");


            repository.Save(instrument);

            var actual = repository.Lookup<Instrument>(Convert.ToInt16(instrument.Id));

            Assert.AreEqual(typeof (MutualFund), actual.GetType());
            Assert.AreEqual(instrument, actual);
        }

        [Test]
        public void TestSave()
        {
            var hundredRuppees = new Price(100);
            var relianceMutuals = new Symbol("RELTICK");
            Instrument instrument = new MutualFund(relianceMutuals, hundredRuppees, "Test Fund");

            Assert.AreEqual(0, instrument.Id);
            repository.Save(instrument);
            Assert.AreNotEqual(0, instrument.Id);
            Assert.IsNotNull(instrument.Id);
        }
    }
}