using System;
using Sharekhan.domain;

namespace Sharekhan.src.domain
{
    class SinglePayOut : Instrument
    {
        public Term Term { get; set; }
        public InterestRate InterestRate { get; set; }
        public DepositDate DepositDate { get; set; }
        public Price InvestedAmount { get; set; }

        private SinglePayOut()
        {
            
        }
        public override Price CurrentMarketValue(System.Collections.Generic.IList<Transaction> transactions)
        {
            return new Price(100);
        }

        public SinglePayOut(Term term, Price investedAmount,
                            Symbol symbol, string description,
                            InterestRate interestRate, DepositDate depositDate) : base(symbol, investedAmount, description)
        {
            Term = term;
            InterestRate = interestRate;
            DepositDate = depositDate;
            InvestedAmount = investedAmount;
            Validate();
        }

        private void Validate()
        {
            if(!Term.IsValid() || !InterestRate.IsValid() || InvestedAmount.Value <= 0 || !DepositDate.IsValid())
            {
                throw new InvalidTermDepositException();
            }
        }
    }

    class DepositDate
    {
        public DateTime DateOfDeposit { get; set; }

        public DepositDate(DateTime dateOfDeposit)
        {
            DateOfDeposit = dateOfDeposit;
        }

        public bool IsValid()
        {
            if(new DateTime().CompareTo(DateOfDeposit) == 0)
                return false;
            return true;
        }
    }

    class InterestRate
    {
        public float RateOfInterest { get; private set; }

        public InterestRate(float rateOfInterest)
        {
            RateOfInterest = rateOfInterest;
        }

        public bool IsValid()
        {
            return RateOfInterest >= 0;
        }
    }

    class Term
    {
        public int DepositTerm { get; private set; }

        public Term(int depositTerm)
        {
            DepositTerm = depositTerm;
        }

        public bool IsValid()
        {
            return DepositTerm >= 0;
        }
    }

    class InvalidTermDepositException : Exception
    {
        
    }
}
