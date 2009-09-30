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
            Transaction purchaseTransaction = new BuyTransaction(DateTime.Today, share, 5, new Price(10.00), 5.00, 3.00);
            repository.Save(purchaseTransaction);
            Assert.IsTrue(purchaseTransaction.Id > 0);
            Console.WriteLine(purchaseTransaction.Id);
            Transaction transaction = repository.Lookup<Transaction>(purchaseTransaction.Id);
            Assert.IsNotNull(transaction);
            Console.WriteLine(transaction.Id);
            Assert.AreEqual(transaction.Id,purchaseTransaction.Id);
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
    }

  
}
