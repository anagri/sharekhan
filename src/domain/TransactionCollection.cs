using System;
using System.Collections;
using System.Collections.Generic;
using ShareKhan.persist;

namespace Sharekhan.domain
{
    public class TransactionCollection:ITransactionCollection
    {
        private readonly IRepository _repository;

        public TransactionCollection(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerator<Transaction> GetEnumerator()
        {
            throw new NotImplementedException();
        }


        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public Transaction Current
        {
            get { throw new NotImplementedException(); }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool Add(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}