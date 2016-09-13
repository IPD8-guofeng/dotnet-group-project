using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.Annotations;

namespace FrameWork
{
    class StockChartModel
    {
        Database db = new Database();
        public PlotModel ChartModel { get; set; }
        public StockChartModel()
        {
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
            List<StockPriceByDay> list = db.GetStockPriceByDayByTicker("ABT", DateTime.Parse("2013-01-01"));
            var model = new PlotModel { Title = "ABT: start date" + "   2013-01-01 \n" + "Candle line, Channel, Volumn" };
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            var highestPriceLine = new LineSeries { MarkerType = MarkerType.Diamond, Color = OxyColors.Beige };
            var lowestPriceLine = new LineSeries { Title = " Channel", MarkerType = MarkerType.Diamond, Color = OxyColors.AliceBlue };

            double i = (list[0].TransAmount / list[0].HighestPrice) * 5;
            //if (list[0].HighestPrice > 100) { while (i >= 100)    i /= 10; } else
            //if (list[0].HighestPrice > 10) { while (i >= 10)    i /= 10; } else
            //if (list[0].HighestPrice > 1) { while (i >= 1)    i /= 10; } else
            //{ while (i >= 0.1)    i /= 10; }
            double x = 0;
            foreach (StockPriceByDay s in list)
            {
                highestPriceLine.Points.Add(new DataPoint(x, s.HighestPrice * 1.05));
                lowestPriceLine.Points.Add(new DataPoint(x, s.LowestPrice * 0.95));
                model.Series.Add(drawCandleLine(x, s.OpenPrice, s.HighestPrice, s.LowestPrice, s.ClosePrice));  // draw one single candle
                model.Series.Add(drawTransAmountLine(x, s.TransAmount / i));  // draw one single volumn
                x++;
            }
            model.Series.Add(highestPriceLine); // draw the channel top line
            model.Series.Add(lowestPriceLine); // draw the channel bottom line

            //var s1 = new BarSeries { StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            //s1.Items.Add(new BarItem { Value = 25 });
            //s1.Items.Add(new BarItem { Value = 137 });
            //s1.Items.Add(new BarItem { Value = 18 });
            //s1.Items.Add(new BarItem { Value = 40 });

            //var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };
            //model.Series.Add(s1);
            //model.Axes.Add(valueAxis);


            this.ChartModel = model;

            //SetUpModel();
        }

        private LineSeries drawCandleLine(double startX, double openPrice, double highestPrice, double lowestPrice, double closePrice)
        {
            double lowLine, highLine;
            if (openPrice <= closePrice)
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
            rect.Points.Add(new DataPoint(startX - 0.5, lowLine));
            rect.Points.Add(new DataPoint(startX - 0.5, highLine));
            rect.Points.Add(new DataPoint(startX, highLine));
            rect.Points.Add(new DataPoint(startX, highestPrice));
            rect.Points.Add(new DataPoint(startX, highLine));
            rect.Points.Add(new DataPoint(startX + 0.5, highLine));
            rect.Points.Add(new DataPoint(startX + 0.5, lowLine));
            rect.Points.Add(new DataPoint(startX, lowLine));
            return rect;
        }

        private LineSeries drawTransAmountLine(double startX, double startY)
        {
            var volumnLine = new LineSeries { MarkerType = MarkerType.None, Color = OxyColors.DarkOrange };
            volumnLine.Points.Add(new DataPoint(startX, 30));
            volumnLine.Points.Add(new DataPoint(startX, 30 + startY));
            return volumnLine;
        }

        private double getHeighChart(StockPriceByDay s)
        {
            double height = s.HighestPrice;
            return height;
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
