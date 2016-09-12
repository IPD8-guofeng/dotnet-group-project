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

namespace FrameWork
{
    /// <summary>
    /// Interaction logic for StockChart.xaml
    /// </summary>
    public partial class StockChart 
    {
        public StockChart()
        {
            //InitializeComponent();

            var model = new PlotModel { Title = "Stock Name" };

            Func<double, double> batFn1 = (x) => Math.Abs(x) - 1;// Math.Sqrt(-Math.Abs(Math.Abs(x) - 1) * Math.Abs(3 - Math.Abs(x)) / ((Math.Abs(x) - 1) * (3 - Math.Abs(x)))) * (1 + Math.Abs(Math.Abs(x) - 3) / (Math.Abs(x) - 3)) * Math.Sqrt(1 - Math.Pow((x / 7), 2)) + (5 + 0.97 * (Math.Abs(x - 0.5) + Math.Abs(x + 0.5)) - 3 * (Math.Abs(x - 0.75) + Math.Abs(x + 0.75))) * (1 + Math.Abs(1 - Math.Abs(x)) / (1 - Math.Abs(x)));


            model.Series.Add(new FunctionSeries(batFn1, 0, 1000, 0.001));

            //model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MaximumPadding = 0.1, MinimumPadding = 0.1 });
            //model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.1, MinimumPadding = 0.1 });

            this.ChartModel = model;

        }

        public PlotModel ChartModel { get; set; }
    }


}
