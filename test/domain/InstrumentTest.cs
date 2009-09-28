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
            
            Instrument instrument = new MutualFund("Mutual002","SUN","Sun MF",new Price(1000));
            Price four = new Price(4000.0d);
            instrument.UpdateCurrentPrice(four);

            Assert.AreEqual(four, instrument.currentPrice());
            
        }


        
    
    }
}