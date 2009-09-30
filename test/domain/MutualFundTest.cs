using NUnit.Framework;
using Sharekhan.domain;
using Sharekhan.service;

namespace Sharekhan.test.domain
{
    [TestFixture]
    public class MutualFundTest : PersistenceTestBase
    {
        [Test]
        public void ShouldUpdatePriceForMutualFund()
        {
            var fourThousand = new Price(4000);

            MutualFundParams parameters = new MutualFundParams();
            parameters.FundHouse = "SBI";
            parameters.FundNm = "Magnum";
            parameters.DivOption = DivOption.Growth.ToString();
            parameters.NoOfUnits = 100;
            parameters.UnitPrice = 23;

            Instrument instrument = new MutualFund(new Symbol("SUN"), fourThousand, "Sun MF", parameters);
            repository.Save(instrument);

            var lookedUpObject = repository.Lookup<Instrument>(instrument.Id);
            Assert.AreEqual(new Price(4000), lookedUpObject.CurrentPrice);

            var newPrice = new Price(2500);
            instrument.UpdateCurrentPrice(newPrice);

            lookedUpObject = repository.Lookup<Instrument>(instrument.Id);
            Assert.AreEqual(new Price(2500), lookedUpObject.CurrentPrice);
        }
        [Test]
        public void ShouldPersistMutualFund()
        {
            var fourThousand = new Price(4000);
            MutualFundParams parameters = new MutualFundParams();
            parameters.FundHouse = "SBI";
            parameters.FundNm = "Magnum";
            parameters.DivOption = DivOption.Growth.ToString();
            Instrument instrument = new MutualFund(new Symbol("SUN"), fourThousand, "Sun MF", parameters);
            repository.Save(instrument);
            var lookedUpObject = repository.Lookup<Instrument>(instrument.Id);
            Assert.AreEqual(new Price(4000), lookedUpObject.CurrentPrice);
        }
        [Test]
        public void ShouldDeleteMutualFund()
        {
            var fourThousand = new Price(4000);
            MutualFundParams parameters = new MutualFundParams();
            parameters.FundHouse = "SBI";
            parameters.FundNm = "Magnum";
            parameters.DivOption = DivOption.Growth.ToString();
            Instrument instrument = new MutualFund(new Symbol("SUN"), fourThousand, "Sun MF", parameters);
            repository.Delete(instrument);
            var lookedUpObject = repository.Lookup<Instrument>(instrument.Id);
            Assert.IsNull(lookedUpObject);
        }
    }
}