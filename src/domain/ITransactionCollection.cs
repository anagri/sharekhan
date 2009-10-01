using System.Collections;
using System.Collections.Generic;

namespace Sharekhan.domain
{
    public interface ITransactionCollection
    {
        bool Add(Transaction transaction);
    }
}