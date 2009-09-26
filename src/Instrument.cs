using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sharekhan.src
{
    public abstract class Instrument
    {
        public virtual int Id{get; set;}
        public virtual string Symbol{get; set;}
        public virtual string Description{get; set;}
        public virtual double CurrentValue { get; set; }
    }
}
