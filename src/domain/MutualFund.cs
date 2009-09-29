using System;
using NUnit.Framework;
using System.Text;

namespace Sharekhan.domain
{
    
 
    public class MutualFund : Instrument
    {

        protected MutualFund()
        {
        }

        public MutualFund(Symbol symbol, Price currentPrice,String description ) : base(symbol,currentPrice,description)
        {
            
        }

    }



}