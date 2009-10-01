using System;
using NUnit.Framework;
using Sharekhan.service;

namespace Sharekhan.domain
{
    [TestFixture]
    public class TransactionTest : PersistenceTestBase
    {
        [Test]
        public void  Verify_Purchase_Transaction()
        {
            Instrument share = new Stock(new Symbol("REL"),new Price(10.00),"Reliance Power" );
            Transaction buyTransaction = new BuyTransaction(DateTime.Today, share, 5, new Price(10.00), 5.00, 3.00);
            repository.Save(buyTransaction);
            Assert.IsTrue(buyTransaction.Id > 0);
            Transaction transaction = repository.Lookup<Transaction>(buyTransaction.Id);
            Assert.IsNotNull(transaction);
            Assert.AreEqual(transaction.Id,buyTransaction.Id);
        }

        [Test]
        public void Verify_Sell_Transaction()
        {
            Stock share = new Stock(new Symbol("REL"), new Price(10.00), "Reliance Power");
            Transaction sellTransaction = new SellTransaction(DateTime.Today, share, 2, new Price(50.00), 5.00, 3.00);
            repository.Save(sellTransaction);
            Assert.IsTrue(sellTransaction.Id > 0);
            Transaction transaction = repository.Lookup<Transaction>(sellTransaction.Id);
            Assert.IsNotNull(transaction);
            Assert.AreEqual(transaction.Id, sellTransaction.Id);
        }
        [Test]
        public void Verify_Purchase_MutualFund()
        {

            Instrument mf = new MutualFund(new Symbol("SBI"), new Price(10.00), "SBI Mutual Fund", "SUNMF", "SUN Magma", "Growth");
            Transaction buyTransaction = new BuyTransaction(DateTime.Today, mf, 5, new Price(10.00), 5.00, 3.00);
            repository.Save(buyTransaction);
            Assert.IsTrue(buyTransaction.Id > 0);
            Transaction transaction = repository.Lookup<Transaction>(buyTransaction.Id);
            Assert.IsNotNull(transaction);
            Assert.AreEqual(transaction.Id, buyTransaction.Id);
        }

        [Test]
        public void Verify_Sell_MutualFund()
        {
            Instrument mf = new MutualFund(new Symbol("SBI"), new Price(10.00), "SBI Mutual Fund", "SUNMF", "SUN Magma", "Growth");
            Transaction sellTransaction = new SellTransaction(DateTime.Today, mf, 2, new Price(50.00), 5.00, 3.00);
            repository.Save(sellTransaction);
            Assert.IsTrue(sellTransaction.Id > 0);
            Transaction transaction = repository.Lookup<Transaction>(sellTransaction.Id);
            Assert.IsNotNull(transaction);
            Assert.AreEqual(transaction.Id, sellTransaction.Id);
        }
    }

  
}
