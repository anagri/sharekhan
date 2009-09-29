using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareKhan.service
{
    public interface IRepository
    {
 
        void Save(Object entity);

        T Lookup<T>(int id);

        void Delete(Object entity);

        void Attach(Object entity);

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
