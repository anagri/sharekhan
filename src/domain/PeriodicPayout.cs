using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharekhan.domain
{
    class PeriodicPayout : TermDeposit
    {
        public int InterestPayoutFrequency { get; set; }

        public PeriodicPayout(Term term, Price investedAmount,
                            Symbol symbol, string description,
                            InterestRate interestRate, int interestPayoutFrequency)
            : base(term, investedAmount, symbol, description, interestRate, 0)
        {
            InterestPayoutFrequency = interestPayoutFrequency;
  
        }



    }
}

