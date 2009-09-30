using System;

namespace Sharekhan.domain
{
    public class Stock : Instrument
    {
        public Stock()
        {
        }

        public Stock(Symbol symbol, Price currentPrice, string description)
            : base(symbol, currentPrice, description)
        {
        }

        public virtual CashDividendTransaction CreateDividendTransaction(Price price,DateTime transactionDate)
        {
            return new CashDividendTransaction(this, price, transactionDate);
        }
    }
}