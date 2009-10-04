namespace Sharekhan.domain
{
    public class PortfolioBalance : IPortfolioBalance
    {
        private readonly ITransactionCollection _collection;

        public PortfolioBalance(ITransactionCollection collection)
        {
            _collection = collection;
        }
    }
}