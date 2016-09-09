using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace DownloadDataFromYahoo
{
    class Program
    {
        static void Main(string[] args)
        {

            /// 
            /// Step 1: Load the stocks names. Every S&P500 stock will be download by default if nothing entered
            /// 

            // We ask the user which stocks to download, plus the dates
            Console.WriteLine("Please write the name of the stocks you want, separated by comma (Eg: AAPL, GM, T, etc). Leaving this blank and pressing enter, will download ALL S&P500 Stocks.");
            var userStocks = Console.ReadLine().ToUpper();

            Console.WriteLine("Please enter date information. If you leave the fields in blank, the default range is 01/01/2000 - 09/01/2016");
            Console.WriteLine("What's the STARTING day? (From 1 to 31)");
            var startDay = Console.ReadLine();

            Console.WriteLine("Of what month? (01 = January, 12 = December)");
            var startMonth = Console.ReadLine();

            Console.WriteLine("Of what year? (Eg: 1997)");
            var startYear = Console.ReadLine();

            Console.WriteLine("And, what's the ENDING day? (From 1 to 31)");
            var finishDay = Console.ReadLine();

            Console.WriteLine("Of what month? (01 = January, 12 = December)");
            var finishMonth = Console.ReadLine();

            Console.WriteLine("Of what year? (Eg:: 2009)");
            var finishYear = Console.ReadLine();

            // If the user leaves date fields in blank, default range: 01/01/2000 - 01/01/2014
            if (string.IsNullOrWhiteSpace(startDay))
            {
                startDay = "01";
            }
            if (string.IsNullOrWhiteSpace(startMonth))
            {
                startMonth = "09";
            }
            if (string.IsNullOrWhiteSpace(startYear))
            {
                startYear = "2013";
            }
            if (string.IsNullOrWhiteSpace(finishDay))
            {
                finishDay = "31";
            }
            if (string.IsNullOrWhiteSpace(finishMonth))
            {
                finishMonth = "08";
            }
            if (string.IsNullOrWhiteSpace(finishYear))
            {
                finishYear = "2016";
            }

            // The List, where we will save the stock's names that could actually be downloaded.
            // We use a list, because we don't know the size it will have; it changes on each loop.
            var tickers = new List<string>();

            // We can have 2 different inputs: a list of stocks, or the default ALL stocks
            // First, the default choice: ALL stocks
            if (string.IsNullOrWhiteSpace(userStocks))
            {
                //We have 2 files with the stock names. By default, the program will download all of then in .csv
                var SP500 = @"..\..\data\sp500CompanyList.csv"; ;
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
            /// Step 2: Download the listed stocks from Yahoo Finance, daily & weekly data
            ///

            string url = "", dayFile = "", weekFile = "", directory = "", dayDirectory = "", weekDirectory = "";
            var dayOrWeek = new string[] { "d", "w" };
            var webClient = new WebClient();

            // The directory where each file will be saved
            directory = @"..\..\data\";
            dayDirectory = directory + @"\Daily_Data\";
            //weekDirectory = directory + @"\Weekly_Data\";

            // Create the directories in case they don't exist
            if (!Directory.Exists(dayDirectory))
            {
                Directory.CreateDirectory(dayDirectory);
                Console.WriteLine("Daily Data directory created.");
            }
            /*
            if (!Directory.Exists(weekDirectory))
            {
                Directory.CreateDirectory(weekDirectory);
                Console.WriteLine("Weekly data directory created.");
            }
            */
            var urlPrototype = @"http://ichart.finance.yahoo.com/table.csv?s={0}&a={1:00}&b={2}&c={3}&d={4:00}&e={5}&f={6}&g={7}&ignore=.csv";
            var dayFilePrototype = "{0}_{1}.{2}.{3}-{4}.{5}.{6}.csv";
            //var weekFilePrototype = "{0}_{1}.{2}.{3}-{4}.{5}.{6}.csv";


            // Parameters for download
            foreach (var stock in tickers)
            {
                // We will download Weekly and Daily historical data
                foreach (var dayWeek in dayOrWeek)
                {
                    // The Yahoo Finance URL for each parameter
                    url = string.Format(urlPrototype, stock, int.Parse(startMonth)-1, startDay, startYear, int.Parse(finishMonth)-1, finishDay, finishYear, dayWeek);

                    // Files Downloader for each scenario
                    try
                    {
                        switch (dayWeek)
                        {
                            case "d":
                                var dayFileName = string.Format(dayFilePrototype, stock.ToUpper(), startMonth, startDay, startYear, finishMonth, finishDay, finishYear);
                                dayFile = Path.Combine(dayDirectory, dayFileName);
                                if (!File.Exists(dayFile))
                                {
                                    Console.WriteLine(url);
                                    webClient.DownloadFile(url, dayFile);
                                    Console.WriteLine(stock + " Daily data downloaded successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("A file with Daily data for " + stock + " and the specified date already exists");
                                }
                                break;

                            case "w":
                                break;
                            case "":
                                /*
                                var weekFileName = string.Format(weekFilePrototype, stock.ToUpper(), startMonth, startDay, startYear, finishMonth, finishDay, finishYear);
                                weekFile = Path.Combine(weekDirectory, weekFileName);
                                if (!File.Exists(weekFile))
                                {
                                    webClient.DownloadFile(url, weekFile);
                                    Console.WriteLine(stock + " Weekly data downloaded successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("A file with Weekly data for " + stock + " and the specified date already exists");
                                }
                                */
                                break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("No files for the stock " + stock + " for the specified date range founded on the server.");
                        continue;
                    }
                }
            }
            Console.WriteLine("Downloads completed. Press enter to close the program.");
            Console.ReadLine();
        }
    }
}