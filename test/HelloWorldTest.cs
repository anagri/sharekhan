using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace sharekhan
{
    [TestFixture]
    public class HelloWorldTest
    {
        [Test]
        public void ShouldSayHello()
        {
            Assert.AreEqual("Hello", new HelloWorld().SayHello());
        }
    }
}
