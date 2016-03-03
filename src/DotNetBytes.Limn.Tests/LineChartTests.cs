using ApprovalTests;
using ApprovalTests.Reporters;
using DotNetBytes.Limn.Charting;
using NGraphics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBytes.Limn.Tests
{
    [TestFixture]
    [UseReporter(typeof(ImageReporter))]
    public class LineChartTests
    {
        [Test]
        public void Draw_WhenGivenOnlyNumericData_ThenDrawsACompleteLineChart()
        {
            var canvas = Platforms.Current.CreateImageCanvas(new Size(400, 300));
            var options = new LineChartOptions();
            options.Title = "Average Temperature in Bogotá, Colombia";
            options.SubTitle = "Courtesy of worldclimate.com";
            var lineChart = new LineChart<string>(options);
            lineChart.Series.Add(new Series<string>(new[]
            {
                Tuple.Create("January", 12.7),
                Tuple.Create("February", 13.0),
                Tuple.Create("March", 13.5),
                Tuple.Create("April", 13.7),
                Tuple.Create("May", 13.7),
                Tuple.Create("June", 13.2),
                Tuple.Create("July", 12.9),
                Tuple.Create("August", 12.9),
                Tuple.Create("September", 13.0),
                Tuple.Create("October", 13.1),
                Tuple.Create("November", 13.1),
                Tuple.Create("December", 12.8),
            }));

            lineChart.Draw(canvas, canvas.Size);

            Console.WriteLine(System.IO.Path.GetFullPath("."));

            byte[] results;
            using (var ms = new MemoryStream())
            {
                canvas.GetImage().SaveAsPng("results.png");
                results = ms.ToArray();
            }

            Approvals.VerifyBinaryFile(results, "png");
        }
    }
}
