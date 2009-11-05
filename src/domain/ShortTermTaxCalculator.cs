﻿using System;
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

         public Price CalculateTaxOverTheBuyAndSellStacks(Stack buyStack, Stack sellStack)
        {
             BuyTransaction buy;
             SellTransaction sell;

             Price shortTermTax = new Price(0);


             while(buyStack.Count>0 && sellStack.Count>0){
                 buy =(BuyTransaction)buyStack.Pop();
                 sell = (SellTransaction)sellStack.Pop();

             if(buy.Quantity.Equals(sell.Quantity))
                {
                    shortTermTax += sell.CalculateShortTermTax(buy);
                }
             else 
                 if(buy.Quantity>sell.Quantity)
                 {
                     BuyTransaction buyTransactionIntoStack = new BuyTransaction(buy.Date,buy.Instrument,buy.Quantity-sell.Quantity,buy.UnitPrice,buy.Tax,buy.Brokerage);

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

        public Price CalculateShortTermTax()
        {
            throw new NotImplementedException();
        }


      
    }
}
