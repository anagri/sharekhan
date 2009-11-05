using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharekhan.domain
{
    class ShortTermTaxCalculator
    {
        public List<Transaction> Transactions { get; set; }

        public ShortTermTaxCalculator(List<Transaction> transactions)
        {
            Transactions = transactions;
            
        }

        public virtual Price CalculateShortTermTaxForAPairOfTransactions(BuyTransaction buyTransaction, SellTransaction sellTransaction)
        {

            return sellTransaction.CalculateShortTermTax(buyTransaction);

        }

        public virtual Stack CreateBuyTransactionStack()
        {
            Stack buyStack = new Stack();

            for (int i = Transactions.Count - 1; i >= 0; i--)
            {
                if (Transactions[i].GetType() == typeof(BuyTransaction))
                {
                    buyStack.Push(Transactions[i]);

                }

            }
            return buyStack;
        }

        public virtual Stack CreateSellTransactionStack()
        {
            Stack sellStack = new Stack();

            for (int i = Transactions.Count - 1; i >= 0; i--)
            {
                if (Transactions[i].GetType() == typeof(SellTransaction))
                {
                    sellStack.Push(Transactions[i]);

                }

            }
            return sellStack;
        }

        public Price CalculateShortTermTax()
        {
            throw new NotImplementedException();
        }
    }
}
