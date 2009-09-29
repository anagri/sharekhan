using System;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;
using Sharekhan.domain;
using ShareKhan.service;
using ShareKhan.src.domain;

namespace ShareKhan.test.domain
{
    [TestFixture]
    public class StockTest
    {
        
        [Test]
        public void Should_be_able_to_create_a_stock_transaction_purchase()
            {

            ISession session = OpenSession();
            Console.WriteLine("hwllo");
            Stock _stock = new Stock("STCK001", new Symbol("RLISTCK"), new Price(20.0), "Reliance Stock");
            Transaction transaction= new Transaction("STCKTRANID",100,_stock,new DateTime(2009,09,27),2.0,11.0 );
            //Repository repository=new Repository(OpenSession());
            //repository.Save(transaction);


        }

        static ISessionFactory SessionFactory;
        static ISession OpenSession()
        {
            if (SessionFactory == null) //not threadsafe
            { //SessionFactories are expensive, create only once
                Configuration configuration = new Configuration();
                configuration.AddAssembly(Assembly.GetCallingAssembly());
                SessionFactory = configuration.BuildSessionFactory();
            }
            return SessionFactory.OpenSession();
        }


    }

  
}
