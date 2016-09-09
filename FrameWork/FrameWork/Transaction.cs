using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork
{
    class Transaction
    {
        public int TransId { get; set; }
        public string StockTicker { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set}
        public bool Action { get; set; }
    }
}
