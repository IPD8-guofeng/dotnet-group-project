using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork
{
    static class GlobalVariable
    {
        public static double Balance { get; set; }
        public static Database db = new Database();
        public const int defaultTransQuantity = 100;
        public const double defaultStartBalance = 100000;
    }
}
