using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Sharekhan.domain;

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
            configuration = new Configuration().Configure("hibernate.cfg.xml");
            _sessionFactory = configuration.BuildSessionFactory();
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

        public T LookupBySymbol<T>(Symbol symbol)
        {
            return (T)session.CreateQuery("from Instrument where symbol=:symbol").
                SetParameter("symbol", symbol.Value).UniqueResult();
        } 

        public IList<T> ListTransactionsByInstrumentId<T>(int id)
        {
            //return (IList<T>)session.CreateQuery("from Transaction  inner join Instrument.Id where Instrument.Id=:id").SetParameter("id", id).List();
            return (IList<T>)session.CreateQuery("from Transaction where Instrument=:id").SetParameter("id", id).List<T>();
            //throw new NotImplementedException();
        }

        public IList<T> ListAllSymbols<T>()
        {
            return session.CreateQuery("Select distinct i.Symbol from Instrument i").List<T>();
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
//                new SchemaExport(configuration).Execute(false, true, false, true, connection, null);
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