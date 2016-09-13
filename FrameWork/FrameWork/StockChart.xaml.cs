using System;
using System.Collections.Generic;
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
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.Annotations;

namespace FrameWork
{
    /// <summary>
    /// Interaction logic for StockChart.xaml
    /// </summary>
    public partial class StockChart 
    {
        Database db = new Database();
        public PlotModel ChartModel { get; set; }
        public StockChart()
        {
            //InitializeComponent();
            /*
                        var model = new PlotModel { Title = "Stock Name" };

                        Func<double, double> batFn1 = (x) => Math.Abs(x) - 1;
                        model.Series.Add(new FunctionSeries(batFn1, 0, 1000, 0.001));
                        Func<double, double> batFn2 = (x) => Math.Abs(x) + 1;
                        model.Series.Add(new FunctionSeries(batFn2, 0, 1000, 0.01));

                        //model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MaximumPadding = 0.1, MinimumPadding = 0.1 });
                        //model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.1, MinimumPadding = 0.1 });

                        this.ChartModel = model;

            */
            List<StockPriceByDay> list = db.GetStockPriceByDayByTicker("A", DateTime.Parse("2016-01-01"));
            var model = new PlotModel { Title = "A: start date" + "   2016-01-01" };
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            var highestPriceLine = new LineSeries { Title = " High price line", MarkerType = MarkerType.Diamond };
            var lowestPriceLine = new LineSeries { Title = "Low price line", MarkerType = MarkerType.Diamond };
            double x = 0;
            foreach (StockPriceByDay s in list)
            {
                //double x = s.PriceDate.Day + s.PriceDate.Month * 100 + (s.PriceDate.Year  % 100) * 10000;
                //highestPriceLine.Points.Add(new DataPoint(DateTimeAxis.ToDouble(s.PriceDate), s.HighestPrice));
                //lowestPriceLine.Points.Add(new DataPoint(DateTimeAxis.ToDouble(s.PriceDate), s.LowestPrice));
                highestPriceLine.Points.Add(new DataPoint(x, s.HighestPrice));
                lowestPriceLine.Points.Add(new DataPoint(x, s.LowestPrice));
                x++;
            }
           // model.Series.Add(highestPriceLine);
            //model.Series.Add(lowestPriceLine);

            double y = 0;
            foreach (StockPriceByDay s in list)
            {
                model.Series.Add(drawCandleLine(y,s.OpenPrice, s.HighestPrice, s.LowestPrice, s.ClosePrice));
                y++;
            }

            this.ChartModel = model;

            //SetUpModel();


        }
        private LineSeries drawCandleLine(double startX, double openPrice, double highestPrice, double lowestPrice, double closePrice)
        {
            double lowLine, highLine;
            if (openPrice<= closePrice)
            {
                lowLine = openPrice;
                highLine = closePrice;
            }
            else
            {
                lowLine = closePrice;
                highLine = openPrice;
            }
            var rect = new LineSeries { MarkerType = MarkerType.None };
            rect.Points.Add(new DataPoint(startX, lowestPrice));
            rect.Points.Add(new DataPoint(startX, lowLine));
            rect.Points.Add(new DataPoint(startX - 0.5,lowLine));
            rect.Points.Add(new DataPoint(startX - 0.5, highLine));
            rect.Points.Add(new DataPoint(startX, highLine));
            rect.Points.Add(new DataPoint(startX, highestPrice));
            rect.Points.Add(new DataPoint(startX, highLine));
            rect.Points.Add(new DataPoint(startX + 0.5, highLine));
            rect.Points.Add(new DataPoint(startX + 0.5, lowLine));
            rect.Points.Add(new DataPoint(startX, lowLine));
            return rect;
        }
        //private void SetUpModel()
        //{
        //    var model = new PlotModel { Title = "A: start date" + "   2015-01-01" };

        //    model.LegendTitle = "Legend";
        //    model.LegendOrientation = LegendOrientation.Horizontal;
        //    model.LegendPlacement = LegendPlacement.Outside;
        //    model.LegendPosition = LegendPosition.TopRight;
        //    model.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
        //    model.LegendBorder = OxyColors.Black;

        //    model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = -20, Maximum = 80 });
        //    model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -10, Maximum = 10 });

        //   /* var dateAxis = new DateTimeAxis(AxisPosition.Bottom, "Date", "dd/MM/yy HH:mm") { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
        //    ChartModel.Axes.Add(dateAxis);
        //    var valueAxis = new LinearAxis(AxisPosition.Left, 0) { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Value" };
        //    ChartModel.Axes.Add(valueAxis);*/
        //}

    }


}
