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

        // Quan: Connection for the school computer 213-18 and home
        const string CONN_STRING = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\xingquan\JohnAbbott\Courses\12CSharp\StockTradeMS.mdf;Integrated Security = True; Connect Timeout = 30";

        // Coonection for azure
        //const string CONN_STRING = @"Data Source=ipd8.database.windows.net;Initial Catalog=stocktrade;Integrated Security=False;User ID=ipd8abbott;Password=Abbott2000;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";
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
            SqlCommand cmd = new SqlCommand("SELECT Quantity, Price, ActionType FROM [Transaction]", conn);
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
            SqlCommand cmd = new SqlCommand("SELECT StockTicker, ((SELECT SUM(Quantity) AS sumBuy FROM [Transaction] WHERE ActionType = 1 GROUP BY StockTicker, ActionType) - (SELECT SUM(Quantity) AS sumSell FROM[Transaction] WHERE ActionType = 2 GROUP BY StockTicker, ActionType)) AS StockQuantity, ((SELECT SUM(Quantity*Price) AS sumBuyCost FROM [Transaction] WHERE ActionType = 1 GROUP BY StockTicker, ActionType) - (SELECT SUM(Quantity*Price) AS sumSellMoney FROM[Transaction] WHERE ActionType = 2 GROUP BY StockTicker, ActionType)) AS sumTotalCost FROM[Transaction] GROUP BY StockTicker", conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string ticker = reader.GetString(reader.GetOrdinal("StockTicker"));
                        int quantity = reader.GetInt32(reader.GetOrdinal("StockQuantity"));
                        double sumTotalCost = reader.GetDouble(reader.GetOrdinal("sumTotalCost"));
                        StockOwned s = new StockOwned { StockTicker = ticker, Quantity = quantity, TotalCost = sumTotalCost };
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
    }//end Class Database
}//end namespace FrameWork
