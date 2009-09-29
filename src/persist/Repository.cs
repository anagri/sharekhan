using System.Data;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
namespace ShareKhan.persist
{
    public class Repository : IRepository
    {
        private ISessionFactory _sessionFactory;
        private Configuration configuration;
        private ISession session;
        private int transactionCounter = 0;

        public Repository()
        {
            configuration = new Configuration().Configure(typeof(Repository).Assembly,"hibernate.cfg.xml");
            _sessionFactory = configuration.BuildSessionFactory();
            ;
        }

        #region IRepository Members

        public void Save(object entity)
        {
            session.Save(entity);
        }

        public T Lookup<T>(int id)
        {
            return session.Get<T>(id);
        }

        public void Delete(object entity)
        {
            session.Delete(entity);
        }

        public void Attach(object entity)
        {
            session.Persist(entity);
        }

        public void BeginTransaction()
        {
            if (null == session)
            {
                session = _sessionFactory.OpenSession();
                session.BeginTransaction();
                IDbConnection connection = session.Connection;
                new SchemaExport(configuration).Execute(false, true, false, true, connection, null);
            }
            ++transactionCounter;
        }

        public void CommitTransaction()
        {
            if (0 == --transactionCounter)
            {
                session.Transaction.Commit();
                session.Close();
                session = null;
            }
        }

        public void RollbackTransaction()
        {
            if (null != session)
            {
                session.Transaction.Rollback();
                session.Close();
                session = null;
            }
            --transactionCounter;
        }

        #endregion
    }
}