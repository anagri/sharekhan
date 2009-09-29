using NUnit.Framework;
using ShareKhan.persist;


namespace Sharekhan.service
{
    public class PersistenceTestBase
    {
        protected IRepository repository;

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
    }
}