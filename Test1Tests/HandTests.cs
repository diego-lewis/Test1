using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1.Tests
{
    [TestClass()]
    public class HandTests
    {
        [TestMethod()]
        public void getHandTest()
        {
            Hand hand = new Hand();
            Assert.IsTrue(hand.getHand("Kh Kc 3s 3h 2d"));
        }

        [TestMethod()]
        public void readHandTest()
        {
            Hand hand = new Hand();
            hand.getHand("2h 3d 4h 5c 6h");
            bool results = "Straight - Six high" == hand.readHand();
            Assert.IsTrue(results);
        }
    }
}