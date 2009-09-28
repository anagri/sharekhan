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
            
            Instrument instrument = new MutualFund();
            Price fourRupees = new Price(4.0d);
            instrument.UpdateCurrentPrice(fourRupees);

            Assert.AreEqual(fourRupees,instrument.currentPrice());
            
        }

        [Test]
        public void should_be_able_to_create_instrument_object()
        {
            Instrument instrument = new MutualFund();
            Assert.AreEqual(Price.Null, instrument.currentPrice());
        }
    
        
    
    }
}