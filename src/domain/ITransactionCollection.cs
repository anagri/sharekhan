using System.Collections;
using System.Collections.Generic;

namespace Sharekhan.domain
{
    public interface ITransactionCollection : IEnumerator<Transaction>
    {
        bool Add(Transaction transaction);
        bool Remove(int Id);
    }
}