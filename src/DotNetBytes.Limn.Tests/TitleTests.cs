using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using DotNetBytes.Limn.Charting;
using NGraphics;
using NUnit.Framework;

namespace DotNetBytes.Limn.Tests
{
    [TestFixture]
    [UseReporter(typeof(ImageReporter))]
    public class TitleTests
    {
        #region Test Methods

        [Test]
        public void Constructor_RequiresAValidString()
        {
            Assert.Throws<ArgumentNullException>(() => new Title(null));
            Assert.Throws<ArgumentNullException>(() => new Title(string.Empty));
            Assert.Throws<ArgumentNullException>(() => new Title(" "));
            var title = new Title("Sample");
        }

        [Test]
        public void Draw_WhenGivenJustTheText_ThenDrawsTheTitleOnTop()
        {
            var available = new Rect(0, 0, 400, 300);
            var canvas = CanvasExtensions.CreateColoredCanvas(available.Size);
            var title = new Title("Sample Title");

            title.Draw(canvas, ref available);

            Assert.That(available.X, Is.EqualTo(0));
            Assert.That(available.Y, Is.GreaterThan(0));
            Assert.That(available.Width, Is.EqualTo(400));
            Assert.That(available.Height, Is.LessThan(300));
            Approvals.VerifyBinaryFile(canvas.AsPngBytes(), "png");
        }

        [Test]
        public void Draw_WhenGivenTextAndSubtitle_ThenBothLinesAreDrawnOnTop()
        {
            var available = new Rect(0, 0, 400, 300);
            var canvas = CanvasExtensions.CreateColoredCanvas(available.Size);
            var title = new Title("Sample Title") { SubTitle = "Just a sample title" };

            title.Draw(canvas, ref available);

            Assert.That(available.X, Is.EqualTo(0));
            Assert.That(available.Y, Is.GreaterThan(0));
            Assert.That(available.Width, Is.EqualTo(400));
            Assert.That(available.Height, Is.LessThan(300));
            Approvals.VerifyBinaryFile(canvas.AsPngBytes(), "png");
        }

        [Test]
        public void Draw_WhenTheBrushesHaveChanged_ThenBothLinesAreDrawnWithDifferentColors()
        {
            var available = new Rect(0, 0, 400, 300);
            var canvas = CanvasExtensions.CreateColoredCanvas(available.Size);
            var title = new Title("Sample Title")
                        {
                            SubTitle = "Just a sample title",
                            TextBrush = new SolidBrush(Colors.Blue),
                            SubTitleBrush = new SolidBrush(Colors.DarkGray)
                        };

            title.Draw(canvas, ref available);

            Assert.That(available.X, Is.EqualTo(0));
            Assert.That(available.Y, Is.GreaterThan(0));
            Assert.That(available.Width, Is.EqualTo(400));
            Assert.That(available.Height, Is.LessThan(300));
            Approvals.VerifyBinaryFile(canvas.AsPngBytes(), "png");
        }

        #endregion
    }
}