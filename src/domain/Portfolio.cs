using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using Sharekhan.domain;
using ShareKhan.persist;


namespace ShareKhan.domain
{
    public class Portfolio : IPortfolio
    {
        public ITransactionCollection TransactionCollection { get; set; }
        public IPortfolioBalance PortfolioBalance { get; set; }
        public IRepository Repository { get; set; }

        public Portfolio()
        {
            this.Repository = new Repository();
        }

        public Portfolio(ITransactionCollection transactionCollection,
                         IPortfolioBalance portfolioBalance) : this()
        {
            TransactionCollection = transactionCollection;
            PortfolioBalance = portfolioBalance;
        }

        public Price CurrentMarketValue()
        {
            IList<Symbol> symbolList = Repository.ListAllSymbols<Symbol>();

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
            IList<Transaction> list = Repository.ListTransactionsByInstrumentId<Transaction>(instrument.Id);

            return instrument.CurrentMarketValue(list);
        }

        public Price CurrentMarketValueOther()
        {
            return new Price(100);
        }



        public void CalcShortTermCapitalGainTax(FinYear year)
        {
            throw new NotImplementedException();
        }


        public double CalculateRealizedProfits(ITransactionCollection listOfTransactions)
        {
            ISet<Instrument> instruments = listOfTransactions.GetAllUniqueInstruments();
            double realizedProfits = 0;
            foreach (Instrument instrument in instruments)
            {
                realizedProfits += CalculateRealizedProfits(listOfTransactions, instrument);
            }
            
            return realizedProfits;
        }


        public double CalculateRealizedProfits(ITransactionCollection statement, Instrument instrument)
        {
            ITransactionCollection instrumentSpecificTransaction = new TransactionCollection();
            foreach (Transaction transaction in statement.TransactionList)
            {
                if (transaction.Instrument == instrument)
                {
                    instrumentSpecificTransaction.Add(transaction);
                }
            }
            return instrumentSpecificTransaction.RealizedProfit().Profit.Value;
        }
        
        public double GetEffectiveRateOfReturn()
        {
            throw new NotImplementedException();
        }
    }
}