using NUnit.Framework;
using Sharekhan.domain;
using Sharekhan.service;

namespace Sharekhan.test.domain
{
    [TestFixture]
    public class MutualFundTest : PersistenceTestBase
    {
        [Test]
        public void ShouldPersistMutualFund()
        {
            var fourThousand = new Price(4000);

            Instrument instrument = new MutualFund(new Symbol("SUN"), fourThousand, "Sun MF");
            repository.Save(instrument);

            Instrument lookedUpObject = repository.Lookup<Instrument>(instrument.Id);

//            instrument.CurrentPrice = new Price();
//            lookedUpObject.CurrentPrice
//            instrument.UpdateCurrentPrice(four);
            
        }
    }
}