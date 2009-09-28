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
    public class RepositoryTest : NHibernateInMemoryTestFixtureBase
    {
        private ISession session;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            InitalizeSessionFactory(new FileInfo("src/domain/Instrument.hbm.xml"));
        }

        [SetUp]
        public void SetUp()
        {
            session = this.CreateSession();
        }

        [TearDown]
        public void TearDown()
        {
            session.Dispose();
        }



        [Test]
        public void TestSave()
        {
            Instrument instrument = new MutualFund();
            IRepository repository = new Repository(session);
            Assert.AreEqual(0, instrument.Id);
            repository.Save(instrument);
            Assert.AreNotEqual(0,instrument.Id);
            Assert.True(instrument.Id > 0);
        }
    }
}
