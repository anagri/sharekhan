using System;
using System.Collections.Generic;

namespace Sharekhan.domain
{
    public class SinglePayOut : Instrument
    {
        public Term Term { get; set; }
        public InterestRate InterestRate { get; set; }
        public DepositDate DepositDate { get; set; }
        public Price InvestedAmount { get; set; }

        public override Price CurrentMarketValue(IList<Transaction> transactions)
        {
            int noOfYears = DateTime.Now.Subtract(DepositDate.DateOfDeposit).Days/365;

            return new Price(Math.Round((InvestedAmount.GetEffectiveValue(noOfYears,InterestRate.RateOfInterest/100)).Value,2));

        }

        public SinglePayOut(Term term, Price investedAmount,
                            Symbol symbol, string description,
                            InterestRate interestRate) : base(symbol, investedAmount, description)
        {
            Term = term;
            InterestRate = interestRate;
            InvestedAmount = investedAmount;
            Validate();
        }

        public double CalculateRealizedProfits(ITransactionCollection listOfTransactions)
        {
            Price realizedProfit = new Price(0);
            foreach (Transaction transaction in listOfTransactions.TransactionList)
            {
                realizedProfit += transaction.EffectiveTransactionAmount();
            }

            return realizedProfit.Value;
        }

        private void Validate()
        {
            if(!Term.IsValid() || !InterestRate.IsValid() || InvestedAmount.Value <= 0 )
            {
                throw new InvalidTermDepositException();
            }
        }
    }

    public class DepositDate
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

    public class InterestRate
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

    public class Term
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