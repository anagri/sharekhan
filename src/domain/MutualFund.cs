using System;
using NUnit.Framework;
using System.Text;

namespace Sharekhan.domain
{
    
 
    public class MutualFund : Instrument
    {

        public MutualFund(String id, String Symbol, String Description, Price CurrentPrice):base(id,Symbol,Description,CurrentPrice)
        {
            
        }

    }



}