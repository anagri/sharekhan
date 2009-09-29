﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sharekhan.domain;
using ShareKhan.domain;
using ShareKhan.domain;

namespace ShareKhan.domain
{
    [TestFixture]
    public class PorfolioTest
    {
        [Test]
        public void should_get_invested_value()
        {
            Portfolio portfolio = new Portfolio();
            //DateTime date, Instrument instrument, int quantity, double amount, double tax, double brokerage
            Instrument mutualFund = new MutualFund(new Symbol("RIL"), new Price(100.00), "Rel MF");

            Transaction buy = new BuyTransaction(DateTime.Today, mutualFund, 10, 1000, 100, 100);

            //portfolio.addTransaction(buy);


        }
    }
}
