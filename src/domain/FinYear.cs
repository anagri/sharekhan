using System;

namespace ShareKhan.domain
{
    public sealed class FinYear
    {
        private enum Month
        {
            MARCH = 3,
            APRIL = 4
        } ;

        private enum Dates
        {
            THIRTY_FIRST = 31,
            FIRST = 1
        } ;

        public struct TaxationPeriod
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }

            public TaxationPeriod(DateTime startDate, DateTime endDate) : this()
            {
                StartDate = startDate;
                EndDate = endDate;
            }

            public bool Equals(TaxationPeriod other)
            {
                return other.StartDate.Equals(StartDate) && other.EndDate.Equals(EndDate);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (obj.GetType() != typeof (TaxationPeriod)) return false;
                return Equals((TaxationPeriod) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (StartDate.GetHashCode()*397) ^ EndDate.GetHashCode();
                }
            }

            public override string ToString()
            {
                return string.Format("StartDate: {0}, EndDate: {1}", StartDate, EndDate);
            }
        } ;



        public int StartYear { get; private set; }

        public FinYear(int year)
        {
            if ( 0 > year)
                throw new ArgumentException("Financial Year should be a non-negative value.");

            this.StartYear = year;
        }


        public TaxationPeriod? GetTaxationPeriod()
        {
            var finYearStart = new DateTime(StartYear, (int) Month.APRIL, (int) Dates.FIRST);
            var finYearEnd = new DateTime(StartYear + 1, (int) Month.MARCH, (int) Dates.THIRTY_FIRST);

            return new TaxationPeriod(finYearStart, finYearEnd);
        }
    }
}