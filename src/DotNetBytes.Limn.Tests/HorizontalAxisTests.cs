using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBytes.Limn.Tests
{
    using ApprovalTests;
    using ApprovalTests.Reporters;
    using DotNetBytes.Limn.Charting;
    using NGraphics;
    using NUnit.Framework;

    [TestFixture]
    [UseReporter(typeof(ImageReporter))]
    public class HorizontalAxisTests
    {
        [Test]
        public void Constructor_RequiresAnArrayOfCategoriesOrScaleValues()
        {
            Assert.Throws<ArgumentNullException>(() => new HorizontalAxis(null));
            Assert.Throws<ArgumentNullException>(() => new HorizontalAxis(new string[] { }));
            Assert.Throws<ArgumentException>(() => new HorizontalAxis(double.PositiveInfinity, 10, 1));
            Assert.Throws<ArgumentException>(() => new HorizontalAxis(double.NegativeInfinity, 10, 1));
            Assert.Throws<ArgumentException>(() => new HorizontalAxis(double.NaN, 10, 1));
            Assert.Throws<ArgumentException>(() => new HorizontalAxis(0, double.PositiveInfinity, 1));
            Assert.Throws<ArgumentException>(() => new HorizontalAxis(0, double.NegativeInfinity, 1));
            Assert.Throws<ArgumentException>(() => new HorizontalAxis(0, double.NaN, 1));
            Assert.Throws<ArgumentException>(() => new HorizontalAxis(0, -1, 1));
            Assert.Throws<ArgumentException>(() => new HorizontalAxis(0, 10, double.PositiveInfinity));
            Assert.Throws<ArgumentException>(() => new HorizontalAxis(0, 10, double.NegativeInfinity));
            Assert.Throws<ArgumentException>(() => new HorizontalAxis(0, 10, double.NaN));
            Assert.Throws<ArgumentException>(() => new HorizontalAxis(0, 10, 20));
            new HorizontalAxis(new[] { "a", "b", "c" });
            new HorizontalAxis(0, 10, 1);
        }

        [Test]
        public void Draw_WhenAnArrayOfCategories_ThenDrawsTheCategoriesAtTheBottom()
        {
            var available = new Rect(0, 0, 400, 300);
            var canvas = CanvasExtensions.CreateColoredCanvas(available.Size);
            var horizontalAxis = new HorizontalAxis(new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun" }) { Text = "Months" };

            horizontalAxis.Draw(canvas, ref available);

            Assert.That(available.X, Is.EqualTo(0));
            Assert.That(available.Y, Is.EqualTo(0));
            Assert.That(available.Width, Is.EqualTo(400));
            Assert.That(available.Height, Is.LessThan(300));
            Approvals.VerifyBinaryFile(canvas.AsPngBytes(), "png");
        }
    }
}
