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
            /*
            Instrument instrument = new MutualFund("Mutual003","MF001","Mutal Fund1",new Price(100));
            IRepository repository = new Repository(session);
            Assert.AreEqual("Mutual003", instrument.Id);
            repository.Save(instrument);
            Assert.AreNotEqual("",instrument.Id);
            Assert.AreEqual("Mutual003", instrument.Id);
             */
        }
    }
}
