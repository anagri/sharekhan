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
        }
    }
}