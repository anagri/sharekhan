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
        public int EndYear { get; private set; }

        public FinYear(int startYear, int endYear)
        {
            ValidateYears(startYear, endYear);
            StartYear = startYear;
            EndYear = endYear;
            ValidateYears(this.StartYear, this.EndYear);
        }

        private void ValidateYears(int startYear, int endYear)
        {
            if (startYear < 1 || endYear < 1)
            {
                throw new ArgumentException(string.Format(
                                                "Financial year cannot have negative values. Start Year ={0} End Year={0}",
                                                startYear, endYear));
            }
            if (endYear != (startYear + 1))
            {
                throw new ArgumentException("Start and end of financial year should be 1 year apart.");
            }
        }

        public bool IsCurrent()
        {
            DateTime minDate = new DateTime(StartYear, (int) Month.APRIL, (int) Dates.FIRST);
            DateTime maxDate = new DateTime(EndYear, (int) Month.MARCH, (int) Dates.THIRTY_FIRST);

            return (minDate <= DateTime.Now && DateTime.Now <= maxDate);
        }

        public bool IsFuture()
        {
            return (StartYear > DateTime.Now.Year);
        }

        public TaxationPeriod? GetTaxationPeriod()
        {
            var finYearStart = new DateTime(StartYear, (int) Month.APRIL, (int) Dates.FIRST);
            var finYearEnd = new DateTime(EndYear, (int) Month.MARCH, (int) Dates.THIRTY_FIRST);
            var today = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

            if (IsCurrent())
            {
                return new TaxationPeriod(finYearStart, today);
            }

            if (IsFuture())
            {
                return new TaxationPeriod(finYearStart, finYearStart);
            }


            return new TaxationPeriod(finYearStart, finYearEnd);
        }
    }
}