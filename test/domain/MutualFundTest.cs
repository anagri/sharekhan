﻿using NUnit.Framework;
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

            Instrument instrument = new MutualFund(new Symbol("SUN"), fourThousand, "Sun MF");
            repository.Save(instrument);

            var lookedUpObject = repository.Lookup<Instrument>(instrument.Id);
            Assert.AreEqual(new Price(4000), lookedUpObject.CurrentPrice);

            var newPrice = new Price(2500);
            instrument.UpdateCurrentPrice(newPrice);

            lookedUpObject = repository.Lookup<Instrument>(instrument.Id);
            Assert.AreEqual(new Price(2500), lookedUpObject.CurrentPrice);
        }
    }
}