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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Transaction item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Transaction item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Transaction[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Transaction item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }
    }
}