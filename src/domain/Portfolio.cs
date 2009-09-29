using System;
using System.Collections.Generic;
using Sharekhan.domain;
using ShareKhan.service;

namespace ShareKhan.domain
{
    public class Portfolio
    {
        public Price CurrentMarketValue(Symbol symbol)
        {
            IRepository repository = new Repository();
            Instrument instrument = repository.LookupBySymbol<Instrument>(symbol);
            Price unitPrice = instrument.CurrentPrice;
            Price value = new Price(0.0);
            List<Transaction> list=repository.listByCriteria<Transaction>("instr_id", instrument.Id.ToString());
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

            value.Value = count*unitPrice.Value;
            return value;
        }
    }
}


