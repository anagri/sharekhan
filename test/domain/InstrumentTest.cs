using NUnit.Framework;

namespace Sharekhan.domain
{
    [TestFixture]
    public class InstrumentTest
    {
        [Test]
        public void ShouldBeAbleToUpdateInstrumentCurrentPrice()
        {
            MutualFundParams parameters = new MutualFundParams();
            parameters.FundHouse = "SBI";
            parameters.FundNm = "Magnum";
            parameters.DivOption = DivOption.Growth.ToString();
            parameters.NoOfUnits = 100;
            parameters.UnitPrice = 23;
            Instrument instrument = new MutualFund(new Symbol("SUN"), new Price(1000), "Sun MF", parameters);
            var four = new Price(4);
            instrument.UpdateCurrentPrice(four);
            Assert.AreEqual(four, instrument.CurrentPrice);
        }



    }
}