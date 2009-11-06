using System;
using System.Collections.Generic;

namespace Sharekhan.domain
{
    public class SinglePayOut : TermDeposit
    {
        public SinglePayOut(Term term, Price investedAmount,
                            Symbol symbol, string description,
                            InterestRate interestRate)
            : base(term, investedAmount, symbol, description, interestRate, 0)
        {
        }

        
        
    }

    
}