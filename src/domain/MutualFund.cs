using System;
using NUnit.Framework;
using System.Text;

namespace Sharekhan.domain
{
    
 
    public class MutualFund : Instrument
    {

<<<<<<< HEAD:src/domain/MutualFund.cs
        public MutualFund(int id, String Symbol, String Description, Price CurrentPrice):base(id,Symbol,Description,CurrentPrice)
=======
        protected MutualFund()
        {
        }

        public MutualFund(Symbol symbol, Price currentPrice,String description ) : base(symbol,currentPrice,description)
>>>>>>> e855e45eebbb02c10271b68afb938583ab9af207:src/domain/MutualFund.cs
        {
            
        }

    }



}