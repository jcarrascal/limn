using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NGraphics;

namespace DotNetBytes.Limn.Charting
{
    public class LineChart<TX>
    {
        private LineChartOptions mOptions;

        public LineChart(LineChartOptions options)
        {
            this.mOptions = options;
        }

        public List<Series<TX>> Series { get; private set; } = new List<Series<TX>>();

        public void Draw(IImageCanvas canvas, Size size)
        {
            canvas.DrawLine(Point.Zero, new Point(size.Width, size.Height), Pens.Black);
        }
    }
}
