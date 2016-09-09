using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCombine
{
    class Program
    {
        static void Main(string[] args)
        {
            /// 
            /// Step 1: Load the stocks names. Every S&P500 stock will be download by default if nothing entered
            /// 

            // We ask the user which stocks to download, plus the dates
            Console.WriteLine("Please write the name of the stocks you want, separated by comma (Eg: AAPL, GM, T, etc). " + 
                "Leaving this blank and pressing enter, will Combine all history data for S&P500 Stocks.");
            var userStocks = Console.ReadLine().ToUpper();

            // fixed date period: 2013-09-01 to 2016-08-31
            string startDay = "01", startMonth = "09", startYear = "2013";
            string finishDay = "31", finishMonth = "08", finishYear = "2016";

            // The List, where we will save the stock's names that could actually be downloaded.
            // We use a list, because we don't know the size it will have; it changes on each loop.
            var tickers = new List<string>();

            // We can have 2 different inputs: a list of stocks, or the default ALL stocks
            // First, the default choice: ALL stocks
            if (string.IsNullOrWhiteSpace(userStocks))
            {
                //We have 2 files with the stock names. By default, the program will download all of then in .csv
                var SP500 = @"..\..\data\sp500CompanyList.csv";
                 
                //We create the fileLists as an array, for the case we would like to add another file with a stock list
                var fileLists = new string[] { SP500 };
                foreach (var file in fileLists)
                {
                    // make sure the file exists
                    
                    if (!File.Exists(file))
                    {
                        Console.WriteLine("Could not load file: " + file);
                        continue;
                    }
                    
                    // The following code will read the  names from de csv file with all S&P500 stocks
                    // we use a "using" block so that C# will automatically clean up all unused resources
                    using (var reader = new StreamReader(file))
                    {
                        while (!reader.EndOfStream)
                        {
                            var csvLine = reader.ReadLine();
                            var firstCommaIndex = csvLine.IndexOf(",");
                            var stock = csvLine.Substring(0, firstCommaIndex);

                            // In case there are symbols in the stock names that would not be recognized by Yahoo API
                            if (!tickers.Contains(stock) && !stock.Contains('/'))
                            {
                                tickers.Add(stock);
                            }
                        }
                    }
                }
            }
            // Given the user wrote what stocks to search for, we build the list with them
            else
            {
                // remove whitespaces from the string
                userStocks = userStocks.Replace("  ", string.Empty);
                var companyLists = userStocks.Split(',');
                foreach (var stock in companyLists)
                {
                    // ignore empty stock symbols
                    if (!string.IsNullOrWhiteSpace(stock))
                    {
                        tickers.Add(stock);
                    }
                }
            }


            /// 
            /// Step 2: Combine history data for the listed stocks
            ///

            string directory = "", dayDirectory = "";

            // The directory where each file is saved
            directory = @"..\..\data\";
            dayDirectory = directory + @"Daily_Data\";
            //weekDirectory = directory + @"\Weekly_Data\";

            // Create the directories in case they don't exist
            if (!Directory.Exists(dayDirectory))
            {
                Directory.CreateDirectory(dayDirectory);
                Console.WriteLine("Daily Data directory created.");
            }

            var dayFilePrototype = dayDirectory + "{0}_{1}.{2}.{3}-{4}.{5}.{6}.csv";
            var outputFile = directory + "SP500_history.csv";
            System.IO.TextWriter tw;
            tw = new StreamWriter(outputFile);

            // Parameters for download
            foreach (var stock in tickers)
            {
                // File name for each stock
                var dayFileName = string.Format(dayFilePrototype, stock.ToUpper(), startMonth, startDay, startYear, finishMonth, finishDay, finishYear);
                Console.WriteLine(dayFileName);

                // make sure the file exists
                if (!File.Exists(dayFileName))
                {
                    Console.WriteLine("Could not load file: " + dayFileName);
                    continue;
                }
                var count = 0;
                // The following code will read the  names from de csv file with all S&P500 stocks
                // we use a "using" block so that C# will automatically clean up all unused resources
                using (var reader = new StreamReader(dayFileName))
                {
                    while (!reader.EndOfStream)
                    {
                        var csvLine = reader.ReadLine();
                        //skip the first line
                        if (count == 0) { count = 1; continue; }
                        
                        var line = stock + "," + csvLine;
                        //Console.WriteLine(line);
                        tw.WriteLine(line);
                        //System.IO.File.WriteAllText(outputFile, stock + "," + csvLine);
                    }
                }

            }
            tw.Close();

            Console.WriteLine("Data Combine completed. Press enter to close the program.");
            Console.ReadLine();
        }
    }
}
