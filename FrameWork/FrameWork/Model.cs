using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork
{
    // Define all the Entities used(Tables, Views, Stroed Procedures etc.)
    class Model
    {
    }
    class Transaction
    {
        public int TransId { get; set; }
        public string StockTicker { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int ActionType { get; set; }
    }
    class Stock
    {
        public string StockTicker { get; set; }
        public string StockName { get; set; }
        public string StockCatagory { get; set; }
        public int StockCommonShare { get; set; }
    }
    class StockPriceByDay
    {
        public string StockTicker { get; set; }
        public DateTime PriceDate { get; set; }
        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
        public double HighestPrice { get; set; }
        public double LowestPrice { get; set; }
        public double TransAmount { get; set; }
    }

    class TrendLine
    {
        public int TrendLineID { get; set; }
        public double PositionStartX { get; set; }
        public double PositionStartY { get; set; }
        public double PositionEndX { get; set; }
        public double PositionEndY { get; set; }
        public double Length { get; set; }
        public string Color { get; set; }
        public int Width { get; set; }
        public string StockTicker { get; set; }

    }



    class StockOwned
    {
        public string StockTicker { get; set; }
        public int Quantity { get; set; }
    }
}
