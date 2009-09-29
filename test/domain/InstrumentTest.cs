using NUnit.Framework;
using Sharekhan.domain;


namespace Sharekhan.domain
{
    [TestFixture]
    public class InstrumentTest
    {



        [Test]
        public void should_be_able_to_update_instrument_current_price()
        {

            Instrument instrument = new MutualFund(new Symbol("SUN"), new Price(1000), "Sun MF");
            Price four = new Price(4);
            instrument.UpdateCurrentPrice(four);

            Assert.AreEqual(four, instrument.CurrentPrice);
            
        }


        
    
    }
}