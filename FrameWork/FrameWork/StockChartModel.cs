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
        public StockChartModel(string ticker)
        {
            //string ticker = "A";
            DateTime startDate = DateTime.Parse("01/01/2016");
            DateTime endDate = DateTime.Now;
            List<StockPriceByDay> list = db.GetStockPriceByDayByTicker(ticker, startDate);
            string title = "Stock "+ ticker + ": From " + startDate.ToShortDateString() + " to " + endDate.ToShortDateString() + "\n" + "Candle line, Channel, Volumn";
            var model = new PlotModel { Title = title  };
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            var highestPriceLine = new LineSeries { MarkerType = MarkerType.Diamond, Color = OxyColors.Beige };
            var lowestPriceLine = new LineSeries { Title = " Channel", MarkerType = MarkerType.Diamond, Color = OxyColors.AliceBlue };

            double volumnUnit = (list[0].TransAmount / list[0].HighestPrice) * 10;
            double x = 0;
            double y = list[0].LowestPrice * 0.7;
            foreach (StockPriceByDay s in list)
            {
                highestPriceLine.Points.Add(new DataPoint(x, s.HighestPrice * 1.05));
                lowestPriceLine.Points.Add(new DataPoint(x, s.LowestPrice * 0.95));
                model.Series.Add(drawCandleLine(x, s.OpenPrice, s.HighestPrice, s.LowestPrice, s.ClosePrice));  // draw one single candle
                double volumn = s.TransAmount / volumnUnit;
                if (volumn > (s.HighestPrice - volumnUnit)) volumn /= 2;
                model.Series.Add(drawTransAmountLine(x, y , volumn));  // draw one single volumn
                x++;
            }
            model.Series.Add(highestPriceLine); // draw the channel top line
            model.Series.Add(lowestPriceLine); // draw the channel bottom line

            this.ChartModel = model;
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

        private LineSeries drawTransAmountLine(double startX, double startY, double volumn)
        {
            var volumnLine = new LineSeries { MarkerType = MarkerType.None, Color = OxyColors.DarkOrange };
            volumnLine.Points.Add(new DataPoint(startX, startY));
            volumnLine.Points.Add(new DataPoint(startX, startY+volumn));
            return volumnLine;
        }
    }
}
