using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharekhan.domain;

namespace ShareKhan.src.domain
{
    public class Stock :Instrument
    {
        public Stock(string id, Symbol symbol, Price currentPrice, string description)
            : base(id, symbol, currentPrice, description)
        {

        }

     
    }
}