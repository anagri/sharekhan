using System;

using System.Collections.Generic;
using Sharekhan.domain;
using ShareKhan.persist;


namespace ShareKhan.domain
{
    public class Portfolio
    {   

        public IRepository Repository { get; set;}
        
        public Portfolio()
        {
            this.Repository = new Repository();
        }

        public Price CurrentMarketValue(Symbol symbol)
        {
           
            Instrument instrument = Repository.LookupBySymbol<Instrument>(symbol);
            Price CurrentPrice = instrument.CurrentPrice;
            Price value = new Price(0.0);
            List<Transaction> list=Repository.ListTransactionsByInstrumentId<Transaction>(instrument.Id);
            int count=0;

            foreach(Transaction trans in list)
            {
                if(trans is BuyTransaction)
                {
                    count += trans.Quantity;
                }else if(trans is SellTransaction)
                {
                    count -= trans.Quantity;
                }
            }

            value.Value = count*CurrentPrice.Value;
            return value;
        }

        public void CalcShortTermCapitalGainTax(FinYear year)
        {
            throw new NotImplementedException();
        }
    }
}


