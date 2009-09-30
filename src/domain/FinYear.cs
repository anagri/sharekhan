using System;

namespace ShareKhan.domain
{
    public sealed class FinYear
    {
        private enum Month
        {
            MARCH = 3,
            APRIL = 4
        };

        private enum Dates
        {
            THIRTY_FIRST = 31,
            FIRST = 1
        };

        public struct TaxableDateRange
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }

            public TaxableDateRange(DateTime startDate, DateTime endDate) : this()
            {
                StartDate = startDate;
                EndDate = endDate;
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

        public TaxableDateRange GetTaxableDays()
        {
            if (IsCurrent())
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            return new DateTime(EndYear, (int) Month.MARCH, (int) Dates.THIRTY_FIRST);
        }
    }
}