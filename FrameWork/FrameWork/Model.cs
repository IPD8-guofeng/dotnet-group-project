using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork
{
    // Define all the Entities used(Tables, Views, Stroed Procedures etc.)
    public enum TransType { Deposit, Withdraw, Buy, Sell }

    public class Portfolio
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class PortTransaction
    {
        public int portId { get; set; }
        public string Symbol { get; set; }
        public TransType Type { get; set; }
        public DateTime? Date { get; set; }
        public int Share { get; set; }
        public double Price { get; set; }
        public double Cashvalue { get; set; }
        public string Notes { get; set; }
    }
    public class TransactionView
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int portId { get; set; }
        public string Symbol { get; set; }
        public TransType Type { get; set; }
        public DateTime? Date { get; set; }
        public int Share { get; set; }
        public double Price { get; set; }
        public double Cashvalue { get; set; }
        public string Notes { get; set; }
    }
    public class PortTransactionSum
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public TransType Type { get; set; }
        public int Share { get; set; }
        
        public double Cashvalue { get; set; }
    }
    public class PortTransactionSumView
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double LastPrice { get; set; }
        public double AvgPrice { get; set; }
        public int Share { get; set; }
        public double CostBase { get; set; }
        public double MarketValue { get; set; }
        public double Return { get; set; }
    }
    class Transaction
    {
        public int TransId { get; set; }
        public string StockTicker { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int ActionType { get; set; }
        public DateTime TransDate { get; set; } 
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
        public double TotalCost { get; set; }
    }

    public class WatchList
    {
        public string StockTicker { get; set; }
        public DateTime PriceDate { get; set; }
        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
        public double HighestPrice { get; set; }
        public double LowestPrice { get; set; }
        public double TransAmount { get; set; }
    }
}
