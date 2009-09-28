using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace ShareKhan.service
{
    public class Repository : IRepository
    {

        private ISessionFactory _sessionFactory;
        private ISession session;

        public Repository(ISessionFactory sessionFactory)
        {
            this._sessionFactory = sessionFactory;
        }

        public Repository(ISession session)
        {
            this.session = session;
        }

        private ISession GetSession()
        {
            if(null == session)
            {
                session = _sessionFactory.OpenSession();
            }
            return session;
        }

        #region IRepository Members

        public void Save(object entity)
        {
            session.Save(entity);
        }

        public T Lookup<T>(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(object entity)
        {
            throw new NotImplementedException();
        }

        public void Attach(object entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
