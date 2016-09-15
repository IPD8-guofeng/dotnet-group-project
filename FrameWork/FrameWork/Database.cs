using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FrameWork
{
    //all the operations related to the database objects(Tables, Views, Stroed Procedures etc.)
    class Database
    {
        //const string CONN_STRING = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ipd\Documents\peopledb1.mdf;Integrated Security=True;Connect Timeout=30";
        //Login: ipd8abbott@gmail.com Pass: Abbott2000
        //Data Source=ipd8.database.windows.net;Initial Catalog=stocktrade;Integrated Security=False;User ID=ipd8abbott;Password=********;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False
        //const string CONN_STRING = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=H:\x\dotnet-group-project\StockTrade.mdf;Integrated Security=True;Connect Timeout=30";

        // Quan: Connection for home
        //const string CONN_STRING = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\xingquan\JohnAbbott\Courses\12CSharp\StockTradeMS.mdf;Integrated Security = True; Connect Timeout = 30";
        // Quan: Connection for school
        //const string CONN_STRING = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=H:\x\dotnet-group-project\StockTradeVS.mdf;Integrated Security=True;Connect Timeout=30";

        // Coonection for azure
        const string CONN_STRING = @"Data Source= ipd8vs.database.windows.net;Initial Catalog=StockTrade;Integrated Security=False;User ID=sqladmin;Password=IPD8rocks!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";
        private SqlConnection conn;
        /*
        private static string userName = "ipd8abbott@gmail.com";
        private static string password = "Abbott2000";
        private static string dataSource = "ipd8.database.windows.net";
        private static string databaseName = "stocktrade";
        */

        public Database()
        {
            /*
            SqlConnectionStringBuilder connString2Builder;
            connString2Builder = new SqlConnectionStringBuilder();

            connString2Builder.DataSource = dataSource;
            connString2Builder.InitialCatalog = databaseName;
            connString2Builder.Encrypt = true;
            connString2Builder.TrustServerCertificate = false;
            connString2Builder.UserID = userName;
            connString2Builder.Password = password;
           
            conn = new SqlConnection(connString2Builder.ConnectionString);
   */
            conn = new SqlConnection(CONN_STRING);
            conn.Open();
        }

        // during prototyping stage we make methods that are
        // not yet implemented throw new NotImplementedException();
        public void AddObject(Object p)
        {
            throw new NotImplementedException();
            /*           using (SqlCommand cmd = new SqlCommand("INSERT INTO Object (Name, Age) VALUES (@Name, @Age)"))
                       {
                           cmd.CommandType = System.Data.CommandType.Text;
                           cmd.Connection = conn;
                           cmd.Parameters.AddWithValue("@Name", p.Name);
                           cmd.Parameters.AddWithValue("@Age", p.Age);
                           cmd.ExecuteNonQuery();
                       }
            */
        }

        public List<Object> GetAllObjects()
        {
            throw new NotImplementedException();
            /*            List<Object> list = new List<Object>();

                      SqlCommand cmd = new SqlCommand("SELECT * FROM Object", conn);
                      using (SqlDataReader reader = cmd.ExecuteReader())
                      {
                          if (reader.HasRows)
                          {
                              while (reader.Read())
                              {
                                  // column by name - the better (preferred) way
                                  int id = reader.GetInt32(reader.GetOrdinal("Id"));
                                  string name = reader.GetString(reader.GetOrdinal("Name"));
                                  int age = reader.GetInt32(reader.GetOrdinal("Age"));
                                  Object p = new Object() { Id = id, Name = name, Age = age };
                                  list.Add(p);
                                  // Console.WriteLine("Object[{0}]: {1} is {2} y/o", id, name, age);
                              }
                          }
                      }
                      return list;
           */
        }

        public Object GetObjectById(int Id)
        {
            throw new NotImplementedException();
        }

        public void DeleteObjectById(int Id)
        {
            throw new NotImplementedException();
            /*
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Object WHERE Id=@Id", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
            }
            */
        }

        public void UpdateObject(Object p)
        {
            throw new NotImplementedException();
            /*
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Object SET Name = @Name, Age = @Age WHERE Id=@Id", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Age", p.Age);
                cmd.Parameters.AddWithValue("@Id", p.Id);
                cmd.ExecuteNonQuery();
            }
           
        }
         */
        }
        public void AddPortTransaction(PortTransaction p)
        {
            /* [portId]      INT           NOT NULL,
    [StockTicker] VARCHAR (10)  NULL,
    [Type]        INT           NULL,
    [Date]        DATE          NOT NULL,
    [Share]       INT           NULL,
    [Price]       FLOAT (53)    NULL,
    [Notes]       VARCHAR (250) NULL,*/
            using (SqlCommand cmd = new SqlCommand("INSERT INTO PortTransaction (portId,StockTicker,Type,Date,Share,Price,Cashvalue,Notes)"+
                " VALUES (@portId,@StockTicker,@Type,@Date,@Share,@Price,@Cashvalue,@Notes)", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                //cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@portId", p.portId);
                cmd.Parameters.AddWithValue("@StockTicker", p.Symbol);
                cmd.Parameters.AddWithValue("@Type", p.Type);
                cmd.Parameters.AddWithValue("@Date", p.Date);
                cmd.Parameters.AddWithValue("@Share", p.Share);
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@Cashvalue", p.Cashvalue);
                cmd.Parameters.AddWithValue("@Notes", p.Notes);
                cmd.ExecuteNonQuery();
            }
            /*  */
        }
        public List<TransactionView> GetAllTranscationsByPortId( int portId)
        {
            List<TransactionView> list = new List<TransactionView>();
            using (SqlCommand cmd = new SqlCommand("select s.StockName, p.* from PortTransaction p left join Stock s on p.StockTicker=s.StockTicker and p.portId=@portId", conn))
            {
                cmd.Parameters.AddWithValue("@portId", portId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // column by name - the better (preferred) way

                            string ticker = reader.GetString(reader.GetOrdinal("StockTicker"));

                            string name;
                            if ( reader.IsDBNull(reader.GetOrdinal("StockName"))) { name = ""; }
                            else { name = reader.GetString(reader.GetOrdinal("StockName")); }
                            
                            int id = reader.GetInt32(reader.GetOrdinal("Id"));
                            TransType type = (TransType)reader.GetInt32(reader.GetOrdinal("Type"));
                            DateTime date = reader.GetDateTime(reader.GetOrdinal("Date"));
                            int share = reader.GetInt32(reader.GetOrdinal("Share"));
                            double price = reader.GetDouble(reader.GetOrdinal("Price"));
                            double cash = reader.GetDouble(reader.GetOrdinal("Cashvalue"));
                            string notes = reader.GetString(reader.GetOrdinal("Notes"));
                            TransactionView p = new TransactionView() { Id = id, portId=portId, Name = name, Symbol=ticker, Type=type, Date=date,
                            Share=share, Price=price, Cashvalue=cash, Notes=notes};
                            list.Add(p);
                        }
                    }
                }
            }
            return list;

        }
        public List<string> GetAllStockNames()
        {
            List<string> list = new List<string>();

            SqlCommand cmd = new SqlCommand("SELECT StockTicker,StockName FROM Stock ORDER BY StockTicker", conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // column by name - the better (preferred) way
                        string name = reader.GetString(reader.GetOrdinal("StockName"));
                        string ticker = reader.GetString(reader.GetOrdinal("StockTicker"));
                        string p = ticker + " | " + name;
                        list.Add(p);
                    }
                }
            }
            return list;

        }

        public List<Portfolio> GetAllPortfolios()
        {
            List<Portfolio> list = new List<Portfolio>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Portfolio ORDER BY ID", conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // column by name - the better (preferred) way
                        int id = reader.GetInt32(reader.GetOrdinal("Id"));
                        string name = reader.GetString(reader.GetOrdinal("Name"));
                        Portfolio p = new Portfolio() { Id = id, Name = name };
                        list.Add(p);
                        // Console.WriteLine("Object[{0}]: {1} is {2} y/o", id, name, age);
                    }
                }
            }
            return list;

        }
        public void AddPortfolio(string name)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Portfolio (Name) VALUES (@Name)", conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                //cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.ExecuteNonQuery();
            }
            /*  */
        }
        public int PortIdByName(string name)
        {
            int id = 0;
            using (SqlCommand cmd = new SqlCommand("SELECT ID FROM Portfolio WHERE Name = @name", conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            id = reader.GetInt32(reader.GetOrdinal("ID"));
                        }
                    }
                }
            }
            return id;

        }
        public void stockActionByTicker(Transaction t)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO [Transaction] (StockTicker,Price,Quantity, ActionType) VALUES (@StockTicker,@Price,@Quantity, @ActionType)"))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@StockTicker", t.StockTicker);
                cmd.Parameters.AddWithValue("@Price", t.Price);
                cmd.Parameters.AddWithValue("@Quantity", t.Quantity);
                cmd.Parameters.AddWithValue("@ActionType", t.ActionType);
                //MessageBox.Show(cmd.CommandText.ToString());
                cmd.ExecuteNonQuery();
            }
        }

        // get StockTicker list from table Stock if there is partOfTicker in the StockTicker string 
        public List<string> getTicker(string partOfTicker)
        {
            List<string> tickerList = new List<string>();
            SqlCommand cmd = new SqlCommand("SELECT StockTicker FROM [Stock] WHERE StockTicker LIKE '%' + @Ticker + '%' ", conn);
            cmd.Parameters.AddWithValue("@Ticker", partOfTicker);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string ticker = reader.GetString(reader.GetOrdinal("StockTicker"));
                        tickerList.Add(ticker);
                    }
                }
            }
            return tickerList;
        }

        // check string is valid in table Stock incasesensitive 
        public bool IsValidTicker(string ticker)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT StockTicker FROM [Stock] WHERE StockTicker = @Ticker ", conn);
                cmd.Parameters.AddWithValue("@Ticker", ticker);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception e)
            {
                return false;
            }

        }
        public double getLatestPriceByTicker(string ticker)
        {
            // sql query -- get specific stock latest close price 
            SqlCommand cmd = new SqlCommand(
                "SELECT ClosePrice FROM (SELECT    ClosePrice, StockTicker, PriceDate, max_date = MAX(PriceDate) OVER(PARTITION BY StockTicker)   FROM[StockPriceByDay]) as s  WHERE StockTicker = @Ticker AND PriceDate = max_date", conn);
            cmd.Parameters.AddWithValue("@Ticker", ticker);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return reader.GetDouble(reader.GetOrdinal("ClosePrice"));
                    }
                }
            }
            return 0;
        }

        public double getBalance()
        {
            // List<Transaction> list = new List<Transaction>();
            double balance = GlobalVariable.defaultStartBalance;
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Transaction]", conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // column by name - the better (preferred) way
                        int quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                        double price = reader.GetDouble(reader.GetOrdinal("Price"));
                        int actionType = reader.GetInt32(reader.GetOrdinal("ActionType"));
                        if (actionType == 1) { balance -= (quantity * price); };   // if buy, minus the transaction amount
                        if (actionType == 2) { balance += (quantity * price); };   // if sell, add the transaction amount
                    }
                }
            }
            return balance;
        }


        public List<StockOwned> getAllStockOwned()
        {
            List<StockOwned> list = new List<StockOwned>();
            SqlCommand cmd = new SqlCommand("SELECT  StockTicker, SUM(Quantity*ActionType) AS sumQty, SUM(Quantity*Price*ActionType) AS sumCost FROM [Transaction]  GROUP BY StockTicker", conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string ticker = reader.GetString(reader.GetOrdinal("StockTicker"));
                        int quantity = reader.GetInt32(reader.GetOrdinal("sumQty"));
                        double sumCost = reader.GetDouble(reader.GetOrdinal("sumCost"));
                        StockOwned s = new StockOwned { StockTicker = ticker, Quantity = quantity, TotalCost = sumCost };
                        list.Add(s);
                    }
                }
            }
            return list;

        }


        public List<WatchList> getWatchList()
        {
            List<WatchList> list = new List<WatchList>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM (SELECT *,max_date = MAX(PriceDate) OVER(PARTITION BY StockTicker)   FROM[StockPriceByDay]) as s  WHERE PriceDate = max_date", conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string ticker = reader.GetString(reader.GetOrdinal("StockTicker"));
                        double openPrice = reader.GetDouble(reader.GetOrdinal("OpenPrice"));
                        double closePrice = reader.GetDouble(reader.GetOrdinal("ClosePrice"));
                        double highestPrice = reader.GetDouble(reader.GetOrdinal("HighestPrice"));
                        double lowestPrice = reader.GetDouble(reader.GetOrdinal("LowestPrice"));
                        double transAmount = reader.GetDouble(reader.GetOrdinal("TransAmount"));
                        DateTime priceDate = reader.GetDateTime(reader.GetOrdinal("PriceDate"));
                        WatchList w = new WatchList { StockTicker = ticker, OpenPrice = openPrice, ClosePrice = closePrice, HighestPrice = highestPrice, LowestPrice = lowestPrice, TransAmount = transAmount, PriceDate = priceDate };
                        list.Add(w);
                    }
                }
            }
            return list;
        }

        public List<StockPriceByDay> GetStockPriceByDayByTicker(string ticker, DateTime startDate, DateTime endDate)
        {
            List<StockPriceByDay> list = new List<StockPriceByDay>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM StockPriceByDay WHERE StockTicker = @Ticker AND PriceDate>@StartDate AND PriceDate<@EndDate ORDER BY PriceDate", conn);
            cmd.Parameters.AddWithValue("@Ticker", ticker);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DateTime priceDate = reader.GetDateTime(reader.GetOrdinal("PriceDate"));
                        double openPrice = reader.GetDouble(reader.GetOrdinal("OpenPrice"));
                        double highestPrice = reader.GetDouble(reader.GetOrdinal("HighestPrice"));
                        double lowestPrice = reader.GetDouble(reader.GetOrdinal("LowestPrice"));
                        double closePrice = reader.GetDouble(reader.GetOrdinal("ClosePrice"));
                        double transAmount = reader.GetDouble(reader.GetOrdinal("transAmount"));
                        StockPriceByDay s = new StockPriceByDay()
                        {
                            StockTicker = ticker,
                            PriceDate = priceDate,
                            OpenPrice = openPrice,
                            HighestPrice = highestPrice,
                            LowestPrice = lowestPrice,
                            ClosePrice = closePrice,
                            TransAmount = transAmount
                        };
                        list.Add(s);
                    }
                }
            }
            return list;
        }
        public List<string> GetStockTickerFromPriceTable()
        {
            List<string> list = new List<string>();
            SqlCommand cmd = new SqlCommand("SELECT StockTicker FROM StockPriceByDay GROUP BY StockTicker", conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string ticker = reader.GetString(reader.GetOrdinal("StockTicker"));
                        list.Add(ticker);
                    }
                }
            }
            return list;
        }
    }//end Class Database
}//end namespace FrameWork
