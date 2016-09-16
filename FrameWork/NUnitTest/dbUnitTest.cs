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
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestAuthoriseFail()
        {
            Database db = new Database();

            PortTransaction p = new PortTransaction();
            p.portId = 1;
            p.Symbol = "AAPL";
            p.Type = TransType.Withdraw;
            p.Date = DateTime.Parse("01/01/2016");
            p.Share = 100000;
            p.Price = 30;
            p.Cashvalue = 3000;
            p.Notes = "sdsd";
            db.AddPortTransaction(p);
            //throw new InvalidOperationException();

            Transaction t = new Transaction();
            t.TransId = 0;
            t.StockTicker = "dasdasd";
            t.Price = 23.45;
            t.Quantity = 50;
            t.ActionType = 1;
            t.TransDate = DateTime.Parse("01/01/2016");

        }
    }
}

