using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareKhan.service
{
    public interface IRepository<T>
    {
        T Save(T entity);

        T Lookup(int id);

        void Delete(T entity);

    }
}
