using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharekhan.domain;

namespace ShareKhan.persist
{
    public interface IRepository
    {
 
        void Save(Object entity);

        T Lookup<T>(int id);

        T LookupBySymbol<T>(Symbol symbol);

        List<T> ListTransactionsByInstrumentId<T>(int id);

        List<T> ListAllSymbols<T>();

        void Delete(Object entity);

        void Attach(Object entity);

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
