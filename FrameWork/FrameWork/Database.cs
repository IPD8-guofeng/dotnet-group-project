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
        
        // Connection for the school computer 213-18
        //const string CONN_STRING = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=H:\x\dotnet-group-project\StockTradeVS.mdf;Integrated Security=True;Connect Timeout=30";

        // Coonection for azure
        const string CONN_STRING = @"Data Source=ipd8.database.windows.net;Initial Catalog=stocktrade;Integrated Security=False;User ID=ipd8abbott;Password=Abbott2000;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";
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

        public void buyStockByticker(Transaction t)
        {

            using (SqlCommand cmd = new SqlCommand("INSERT INTO [Transaction] (StockTicker,Price,Quantity, ActionType) VALUES (@StockTicker,@Price,@Quantity, @ActionType)"))
                       {
                           cmd.CommandType = System.Data.CommandType.Text;
                           cmd.Connection = conn;
                           cmd.Parameters.AddWithValue("@StockTicker", t.StockTicker);
                           cmd.Parameters.AddWithValue("@Price", t.Price);
                           cmd.Parameters.AddWithValue("@Quantity", t.Quantity);
                           cmd.Parameters.AddWithValue("@ActionType", t.Action);
                           MessageBox.Show(cmd.CommandText.ToString());
                           cmd.ExecuteNonQuery();
                       }
        }







    }//end Class Database
}//end namespace FrameWork
