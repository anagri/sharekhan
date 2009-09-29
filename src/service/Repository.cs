using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Sharekhan.common;

namespace ShareKhan.service
{
    public class Repository : IRepository
    {
        private Configuration configuration;
        private ISessionFactory _sessionFactory;
        private ISession session;
        private int transactionCounter = 0;

        public Repository()
        {
            configuration = new Configuration().Configure("hibernate.cfg.xml");
            this._sessionFactory = configuration.BuildSessionFactory(); ;
        }

        #region IRepository Members

        public void Save(object entity)
        {
            if (null == session) throw new InvalidStateException("session is not created. call beginTransaction first;");
            session.Save(entity);
        }

        public T Lookup<T>(int id)
        {
            if (null == session) throw new InvalidStateException("session is not created. call beginTransaction first;");
            return session.Get<T>(id);
        }

        public void Delete(object entity)
        {
            if (null == session) throw new InvalidStateException("session is not created. call beginTransaction first;");
            session.Delete(entity);
        }

        public void Attach(object entity)
        {
            if (null == session) throw new InvalidStateException("session is not created. call beginTransaction first;");
            session.Persist(entity);
        }

        public void BeginTransaction()
        {
            if(null == session)
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
            if(null != session)
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
