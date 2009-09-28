using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareKhan.service
{
    public class Repository : IRepository
    {


        #region IRepository Members

        public void Save(object entity)
        {
            throw new NotImplementedException();
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
