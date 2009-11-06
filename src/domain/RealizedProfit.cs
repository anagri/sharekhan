using System;
using System.Collections.Generic;

namespace Sharekhan.domain
{
    public class RealizedProfit
    {
        private IDictionary<Instrument, Net> instruments = new Dictionary<Instrument, Net>();

        public Price Profit
        {
            get
            {
                Price realizedProfit = new Price(0);
                foreach (Net net in instruments.Values)
                {
                    realizedProfit += net.Profit;
                }
                return realizedProfit;
            }
        }


        public Net For(Instrument instrument)
        {
            if(!instruments.ContainsKey(instrument))
            {
                instruments[instrument] = new Net(Price.Null,0);
            }
            return instruments[instrument];
        }
    }

    public class Net
    {
        public Price Profit { get; set; }
        public int Quantity { get; set; }

        public Net(Price profit, int quantity)
        {
            Profit = profit;
            Quantity = quantity;
        }
    }
}