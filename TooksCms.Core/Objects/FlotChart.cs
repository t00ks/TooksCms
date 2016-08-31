using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Objects
{
    /// <summary>
    /// object to mirror the flot chart java axis object
    /// </summary>
    public class FlotChartAxis
    {
        public FlotChartAxis()
        {
            this.show = true;
        }
        public object min { get; set; }
        public object max { get; set; }

        /// <summary>
        /// "bottom" or "top" or "left" or "right"
        /// </summary>
        public string position { get; set; }

        /// <summary>
        /// null or true/false
        /// </summary>
        public bool show { get; set; }

        /// <summary>
        /// null or "time"
        /// </summary>
        public string mode { get; set; }

        /// <summary>
        /// null or color spec
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// null or color spec
        /// </summary>
        public string tickColor { get; set; }

        /// <summary>
        /// null or number
        /// </summary>
        public int? alignTicksWithAxis { get; set; }

        public List<List<object>> CustomTicks { get; set; }
    }

    public class FlotChart : List<FlotChartItem>
    {
        public new FlotChartItem this[int index] { get { return base[index]; } }

        public int AddSeries(string label, FlotChartSeries lines, int? xaxis = 1, int? yaxis = 1)
        {
            FlotChartItem fci = FlotChartItem.CreateNew(label, lines);
            fci.xaxis = xaxis;
            fci.yaxis = yaxis;
            base.Add(fci);
            return base.Count - 1;
        }

        public void AddData(int index, object x, decimal y)
        {
            this[index].data.Add(new List<object> { x, y });
        }

        public void AddData(string label, object x, decimal y)
        {
            this.Single(fci_ => fci_.label == label).data.Add(new List<object> { x, y });
        }
    }

    public class FlotPie: List<FlotPieItem>
    {
        public int AddSeries(string label)
        {
            base.Add(new FlotPieItem {label = label});
            return base.Count - 1;
        }

        public new FlotPieItem this[int index] { get { return base[index]; } }

        public void SetData(int index, decimal x)
        {
            this[index].data = x;
        }

        public void SetData(string label, decimal x)
        {
            this.Single(fpi_ => fpi_.label == label).data = x;
        }
    }

    public class FlotPieItem
    {
        public FlotPieItem() { data = new List<object>(); }
        public string label { get; set; }

        public object data { get; set; }
    }

    public class FlotChartItem
    {
        public FlotChartItem() { data = new List<List<object>>(); }
        public string label { get; set; }
        /// <summary>
        /// The "xaxis" and "yaxis" options specify which axis to use. The axes
        /// are numbered from 1 (default), so { yaxis: 2} means that the series
        /// should be plotted against the second y axis.
        /// </summary>
        public int? xaxis { get; set; }
        public int? yaxis { get; set; }

        public List<List<object>> data { get; set; }

        public static FlotChartItem CreateNew(string label, FlotChartSeries series)
        {
            if (series is FlotChartLine)
            {
                return new FlotChartItemLine(label, series);
            }
            else if (series is FlotChartDashed)
            {
                return new FlotChartItemDashes(label, series);
            }
            else if (series is FlotChartBar)
            {
                return new FlotChartItemBar(label, series);
            }
            else
            {
                throw new Exception("unknown flot chart series type : " + series.GetType().ToString());
            }
        }
    }

    public class FlotChartItemLine : FlotChartItem
    {
        public FlotChartItemLine(string label, FlotChartSeries series)
        {
            this.label = label;
            this.lines = (FlotChartLine)series;
        }
        public FlotChartLine lines { get; set; }
    }

    public class FlotChartItemDashes : FlotChartItem
    {
        public FlotChartItemDashes(string label, FlotChartSeries series)
        {
            this.label = label;
            this.lines = (FlotChartDashed)series;
        }
        public FlotChartDashed lines { get; set; }
    }

    public class FlotChartItemBar : FlotChartItem
    {
        public FlotChartItemBar(string label, FlotChartSeries series)
        {
            this.label = label;
            this.bars = (FlotChartBar)series;
        }
        public FlotChartBar bars { get; set; }
    }

    public abstract class FlotChartSeries
    {
        public FlotChartSeries()
        {
            this.fill = null;
            this.lineWidth = 1;
        }
        public bool show { get; set; }
        public decimal? fill { get; set; }
        public string fillColor { get; set; }
        public int lineWidth { get; set; }
    }

    public class FlotPieSeries
    {
        
    }

    public class FlotChartBar : FlotChartSeries
    {
        public FlotChartBar(bool show = true, int width = 20, bool horizontal = true)
        {
            this.show = show;
            this.barWidth = width / (double)100;
            this.horizontal = horizontal;
            this.align = "center";
            this.fill = 0.4m;
        }

        public double barWidth { get; set; }
        public bool horizontal { get; set; }
        public string align { get; set; }
    }

    public class FlotChartLine : FlotChartSeries
    {
        public FlotChartLine(bool show = true, int width = 2)
        {
            this.show = show;
            this.lineWidth = width;
        }


    }

    public class FlotChartDashed : FlotChartSeries
    {
        public FlotChartDashed(bool show = false, int width = 0, int length = 10, int spacing = 2)
        {
            this.show = show;
            this.lineWidth = 0;
            this.dashWidth = width;
            this.dashLength = new int[2];
            this.dashLength[0] = length; this.dashLength[1] = spacing;
        }

        public int dashWidth { get; set; }
        public int[] dashLength { get; set; }
        public bool dashed = true;
    }
}
