namespace Sharekhan.domain
{
    public class Stock : Instrument
    {
        public Stock(Symbol symbol, Price currentPrice, string description)
            : base(symbol, currentPrice, description)
        {
        }

    }
}