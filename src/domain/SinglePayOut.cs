using System;
using System.Collections.Generic;

namespace Sharekhan.domain
{
    public class SinglePayOut : TermDeposit
    {
        public SinglePayOut(Term term, Price investedAmount,
                            Symbol symbol, string description,
                            InterestRate interestRate)
            : base(term, investedAmount, symbol, description, interestRate)
        {
        }

        public override Price CurrentMarketValue(IList<Transaction> transactions)
        {
            int noOfYears = DateTime.Now.Subtract(DepositDate.DateOfDeposit).Days/365;
            return new Price(Math.Round((InvestedAmount.GetEffectiveReturn(noOfYears,InterestRate.RateOfInterest/100)).Value,2));
        }

        public override double CalculateRealizedProfits(ITransactionCollection listOfTransactions)
        {
            Price realizedProfit = new Price(0);
            foreach (Transaction transaction in listOfTransactions.TransactionList)
            {
                realizedProfit += transaction.EffectiveTransactionAmount();
            }

            return realizedProfit.Value;
        }
        
    }

    
}