using System;
using NUnit.Framework;
using System.Text;

namespace Sharekhan.domain
{
    public class MutualFund : Instrument
    {
        public virtual string FundNm { get; set; }
        public virtual string FundHouse { get; set; }
        public virtual string DivOption { get; set; }

        protected MutualFund()
        {
        }

        public MutualFund(Symbol symbol, Price currentPrice, String description, String fundNm, String fundHouse, String divOption)
            : base(symbol, currentPrice, description)
        {
            FundNm = fundNm;
            FundHouse = fundHouse;
            DivOption = divOption;

        }

        public virtual bool Equals(MutualFund other)
        {
            return base.Equals(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as MutualFund);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(MutualFund left, MutualFund right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MutualFund left, MutualFund right)
        {
            return !Equals(left, right);
        }

        public virtual CashDividendTransaction CreateCashDividendTransaction(Price price, DateTime transactionDate)
        {
            return new CashDividendTransaction(this, price, transactionDate);
        }

        public virtual UnitDividendTransaction CreateUnitDividendTransaction(int quantity, DateTime transactionDate)
        {
            return new UnitDividendTransaction(this, quantity, transactionDate);
        }
    }
   
  
}