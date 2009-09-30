using System;

namespace ShareKhan.domain
{
    public class FinYear
    {
        public int StartYear { get; set; }
        public int EndYear { get; set; }

        public FinYear(int startYear, int endYear)
        {
            ValidateYears(startYear, endYear);
            StartYear = startYear;
            EndYear = endYear;
        }

        private void ValidateYears(int startYear, int endYear)
        {
            if (startYear < 1 || endYear < 1)
            {
                throw new ArgumentException(string.Format(
                    "Financial year could not have negative values. Start Year ={0} End Year={0}",startYear,endYear));
            }
//            if (endYear != (startYear + 1))
//            {
//                throw new ArgumentException("Start and end of financial year should be 1 year apart.");
//            }
        }
    }
}