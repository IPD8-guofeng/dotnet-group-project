using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrameWork;
using System.Collections.Generic;

namespace NUnitTest
{
    [TestClass]
    public partial class dbUnitTest
    {
        [TestMethod]
        public void ValidationMethod()
        {
            Database db = new Database();
            
            // check database for valid stock ticker
            Assert.AreEqual(db.IsValidTicker("AAPL"), true);
            Assert.AreEqual(db.IsValidTicker("A"), true);
            Assert.AreEqual(db.IsValidTicker(""), false);
            Assert.AreEqual(db.IsValidTicker(null), false); 

            // check database for close price from table StockPriceByDay
            Assert.AreEqual(db.getLatestPriceByTicker("AAPL"), 106.099998);
            Assert.AreEqual(db.getLatestPriceByTicker(""), 0);
            Assert.AreEqual(db.getLatestPriceByTicker(null), 0);

            // check ticker from Stock
            List<string> ticker1 = new List<string>();
            ticker1.Add("BBT");
            List<string> result1 = db.getTicker("BBT");
            for (int i = 0; i < ticker1.Count;i++ )
            {
                Assert.AreEqual(ticker1[i], result1[i]);
            }

            List<string> ticker2 = new List<string>();
            ticker2.Add("CA");
            ticker2.Add("CAH");
            ticker2.Add("CASH");
            ticker2.Add("CAT");
            List<string> result2 = db.getTicker("CA");
            for (int i = 0; i < ticker2.Count;i++ )
            {
                Assert.AreEqual(ticker2[i], result2[i]);
            }

            List<string> ticker3 = new List<string>();
            List<string> result3 = db.getTicker("ZHJK");

            for (int i = 0; i < result3.Count ; i++)
            {
                Assert.AreEqual(ticker3[i], result3[i]);
            }



        }
        
    }
}

