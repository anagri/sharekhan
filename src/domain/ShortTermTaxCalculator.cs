using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareKhan.domain;

namespace Sharekhan.domain
{
    class ShortTermTaxCalculator
    {
        public List<Transaction> Transactions { get; set; }
        public FinYear YearForStcg { get; set; }

        public ShortTermTaxCalculator(List<Transaction> transactions, FinYear yearForStcg)
        {
            Transactions = transactions;
            YearForStcg = yearForStcg;

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

        public Price CalculateTaxOverTheBuyAndSellStacks(Stack buyStack, Stack sellStack)
        {
            BuyTransaction buy;
            SellTransaction sell;

            Price shortTermTax = new Price(0);


            while (buyStack.Count > 0 && sellStack.Count > 0)
            {
                buy = (BuyTransaction)buyStack.Pop();
                sell = (SellTransaction)sellStack.Pop();

                if (buy.Quantity.Equals(sell.Quantity))
                {
                    shortTermTax += sell.CalculateShortTermTax(buy);
                }
                else
                    if (buy.Quantity > sell.Quantity)
                    {
                        BuyTransaction buyTransactionIntoStack = new BuyTransaction(buy.Date, buy.Instrument, buy.Quantity - sell.Quantity, buy.UnitPrice, buy.Tax, buy.Brokerage);

                        buyStack.Push(buyTransactionIntoStack);

                        shortTermTax += sell.CalculateShortTermTax(new BuyTransaction(buy.Date, buy.Instrument, sell.Quantity, buy.UnitPrice, buy.Tax, buy.Brokerage));

                    }
                    else
                    {
                        SellTransaction sellTransactionIntoStack = new SellTransaction(buy.Date, buy.Instrument, sell.Quantity - buy.Quantity, buy.UnitPrice, buy.Tax, buy.Brokerage);

                        sellStack.Push(sellTransactionIntoStack);

                        sell.Quantity = buy.Quantity;

                        shortTermTax += sell.CalculateShortTermTax(new BuyTransaction(buy.Date, buy.Instrument, sell.Quantity, buy.UnitPrice, buy.Tax, buy.Brokerage));

                    }
            }

            return shortTermTax;

        }

        public List<Transaction> filterListOfTransactionsOnYear()
        {
            return new List<Transaction>();
        }


        public Price CalculateShortTermTax()
        {
            Transactions = GetTransactionBalance();

            Stack buyStack = CreateBuyTransactionStack();
            Stack sellStack = CreateSellTransactionStack();

            return CalculateTaxOverTheBuyAndSellStacks(buyStack, sellStack);

        }


        public List<Transaction> GetTransactionBalance()
        {

            
            int leftOverBuys = 0;
            FinYear theYearBefore = new FinYear(YearForStcg.StartYear-1);
            Instrument theBuyInstrument = Transactions[0].Instrument;

            for (int i = 0; i < Transactions.Count;)
            {
                if (Transactions[i].Date.CompareTo(theYearBefore.GetTaxationPeriod().Value.StartDate) > 0)
                {
                    break;
                }
                if (Transactions[i].GetType() == typeof(BuyTransaction))
                 {
                     leftOverBuys += Transactions[i].Quantity;
                     Transactions.Remove(Transactions[i]);
                   
                 }
                else if (Transactions[i].GetType() == typeof(SellTransaction))
                {
                    leftOverBuys -= Transactions[i].Quantity;
                    Transactions.Remove(Transactions[i]);
                
                }
                
            }
            BuyTransaction theBuyToAppendToList = new BuyTransaction(new DateTime(2008, 06, 01), theBuyInstrument, leftOverBuys, new Price(0),0, 0);

            if (theBuyToAppendToList.Quantity > 0)
            {
                //             Transactions.Insert(0, theBuyToAppendToList);

                for (int i = 0; i < Transactions.Count;i++)
                {
                    if (Transactions[i].GetType() == typeof (SellTransaction))
                    {
                       leftOverBuys -= Transactions[i].Quantity;
                       if(leftOverBuys<0)
                       {
                           Transactions[i].Quantity = -leftOverBuys;
                           break;
                       }
                       else if(leftOverBuys>0)
                       {
                           Transactions.Remove(Transactions[i]);
                           i--;
                       }
                    }
                }
            }
            return Transactions;
           
        }
    }

}
