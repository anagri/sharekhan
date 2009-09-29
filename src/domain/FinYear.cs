using System;

namespace ShareKhan.domain
{
    public class FinYear
    {
        public int StartYear { get; set; }
        public int EndYear { get; set; }

        public FinYear(int startYear, int endYear)
        {
            StartYear = startYear;
            EndYear = endYear;
            if (startYear < 1 || endYear < 0)
                throw new ArgumentException("Financial year could not have negative values.");
            if (endYear != (startYear + 1))
                throw new ArgumentException("Start and end of financial year should be 1 year apart.");
        }
    }
}