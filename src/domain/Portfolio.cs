using System;
using System.Collections.Generic;
using Sharekhan.domain;
using ShareKhan.persist;


namespace ShareKhan.domain
{
    public class Portfolio :IPortfolio
    {
        public ITransactionCollection TransactionCollection { get; set; }
        public IPortfolioBalance PortfolioBalance { get; set; }
        public IRepository Repository { get; set; }

        public Portfolio()
        {
            this.Repository = new Repository();
        }

        public  Portfolio(ITransactionCollection transactionCollection,
            IPortfolioBalance portfolioBalance)
        {
            TransactionCollection = transactionCollection;
            PortfolioBalance = portfolioBalance;
        }

        public Price CurrentMarketValue()
        {
            List<Symbol> symbolList = Repository.ListAllSymbols<Symbol>();

            Price portfolioPrice = new Price(0.0);

            foreach (Symbol symbol in symbolList)
            {
                portfolioPrice.Value += CurrentMarketValue(symbol).Value;
            }

            return portfolioPrice;
        }

        public Price CurrentMarketValue(Symbol symbol)
        {
            Instrument instrument = Repository.LookupBySymbol<Instrument>(symbol);
            Price CurrentPrice = instrument.CurrentPrice;
            Price value = new Price(0.0);
            List<Transaction> list = Repository.ListTransactionsByInstrumentId<Transaction>(instrument.Id);
            int count = 0;

            foreach (Transaction trans in list)
            {
                if (trans is BuyTransaction)
                {
                    count += trans.Quantity;
                }
                else if (trans is SellTransaction)
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

        
        public double CalculateRealizedProfits(TransactionStatement statement)
        {
            Dictionary<Instrument, double> realizedProfitsTbl = new Dictionary<Instrument, double>();
            Dictionary<Instrument, int> qty = new Dictionary<Instrument, int>();
            double realizedProfits = 0.0;
            TransactionCollection listOfTransactions = statement.GetTransactionCollection();

            listOfTransactions.BuildDictionariesWithSellingAmounts(realizedProfitsTbl, qty);

            listOfTransactions.UpdateRealizedProfits(realizedProfitsTbl, qty);

            foreach (KeyValuePair<Instrument, double> kvp in realizedProfitsTbl)
            {
                realizedProfits += kvp.Value;
            }
            return realizedProfits;
        }



        

        public object CalculateRealizedProfits(TransactionStatement statement, Instrument instrument)
        {
            TransactionStatement InstrumentSpecificTransaction = new TransactionStatement();
            foreach (Transaction transaction in statement.GetTransactionCollection())
            {
                if (transaction.Instrument == instrument)
                {
                    InstrumentSpecificTransaction.addTransaction(transaction);
                }
            }
            return CalculateRealizedProfits(InstrumentSpecificTransaction);
        }
    }
}