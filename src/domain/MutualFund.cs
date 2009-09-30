using System;
using NUnit.Framework;
using System.Text;

namespace Sharekhan.domain
{
    public class MutualFund : Instrument
    {
        public virtual string FundNm { get; set; }
        public virtual string FundHouse { get; set; }
        public virtual double NoOfUnits { get; set; }
        public virtual double UnitPrice { get; set; }
        public virtual string DivOption { get; set; }

        protected MutualFund()
        {
        }

        public MutualFund(Symbol symbol, Price currentPrice, String description, MutualFundParams parameters)
            : base(symbol, currentPrice, description)
        {
            FundNm = parameters.FundNm;
            FundHouse = parameters.FundHouse;
            NoOfUnits = parameters.NoOfUnits;
            UnitPrice = parameters.UnitPrice;
            DivOption = parameters.DivOption;

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
    }
    /// <summary>
    /// 
    /// </summary>
    public class MutualFundParams
    {
        public string FundNm { get; set; }
        public string FundHouse { get; set; }
        public double NoOfUnits { get; set; }
        public double UnitPrice { get; set; }
        public string DivOption { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum DivOption
    {
        Growth,Dividend
    }
}