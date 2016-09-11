using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile
{
    public enum TransType { Deposit, Withdraw, Buy, Sell }

    public class Portfolio
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Transcation
    {
        public int portId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public TransType Type { get; set; }
        public DateTime Date { get; set; }
        public int Share { get; set; }
        public double Price { get; set; }
        public string Notes { get; set; }
    }

}
