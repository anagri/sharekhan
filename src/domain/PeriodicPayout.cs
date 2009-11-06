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
            : base(term, investedAmount, symbol, description, interestRate)
        {
            InterestPayoutFrequency = interestPayoutFrequency;
  
        }

        public override Price CurrentMarketValue(IList<Transaction> transactions)
        {
/*
            int noOfYears = DateTime.Now.Subtract(DepositDate.DateOfDeposit).Days / 365;
            return new Price(Math.Round((InvestedAmount.GetEffectiveReturn(noOfYears, InterestRate.RateOfInterest / 100)).Value, 2));
*/
           // transactions.Select(){|transaction| => transactions is TermDepositWithdrawalTransaction && }
            var buy = from u20 in transactions where u20 is TermDepositWithdrawalTransaction select u20;
           // var buyLast = from u20 in buy where u20.Date select u20;
            

            return null;
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

