using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBytes.Limn.Charting
{
    using NGraphics;

    public class HorizontalAxis
    {
        public string Text { get; set; }

        public Font TextFont { get; set; } = new Font("Arial", 10);

        public Brush TextBrush { get; set; } = new SolidBrush(Colors.Black);

        public TextAlignment TextAlignment { get; set; }

        public Font LabelsFont { get; set; } = new Font("Arial", 9);

        public string[] Categories { get; private set; }

        public HorizontalAxis(string[] categories)
        {
            if (categories == null || categories.Length == 0)
            {
                throw new ArgumentNullException(nameof(categories));
            }

            this.Categories = categories;
        }

        public HorizontalAxis(double minimum, double maximum, double step)
        {
            if (double.IsInfinity(minimum) || double.IsNaN(minimum))
            {
                throw new ArgumentException("The minimum should be a valid value.");
            }

            if (minimum >= maximum || double.IsInfinity(maximum) || double.IsNaN(maximum))
            {
                throw new ArgumentException("The minimum should be less than the maximum.", nameof(maximum));
            }

            if (step >= (maximum - minimum) || double.IsInfinity(step) || double.IsNaN(step))
            {
                throw new ArgumentException("The step should be less than the distance between maximum and minimum.", nameof(step));
            }

            this.Minimum = minimum;
            this.Maximum = maximum;
            this.Step = step;
        }

        public double Step { get; set; }

        public double Maximum { get; set; }

        public double Minimum { get; set; }

        public double StemHeightFactor { get; set; } = 0.025;

        public double MeasureHeight(IImageCanvas canvas, ref Rect available)
        {
            double height = 0;
            if (!string.IsNullOrWhiteSpace(this.Text))
            {
                // Height of the axis text.
                Size textMetrics = canvas.MeasureText("My", this.TextFont);
                height += textMetrics.Height * 1.1;
            }

            // Height of lables.
            Size labelsMetrics = canvas.MeasureText("My", this.LabelsFont);
            height += labelsMetrics.Height * 1.1;

            // Height of the lines connecting the label to the axis.
            height += available.Height * this.StemHeightFactor;

            return height;
        }

        public void Draw(IImageCanvas canvas, ref Rect available)
        {
            double textHeight = 0;
            if (!string.IsNullOrWhiteSpace(this.Text))
            {
                Size textMetrics = canvas.MeasureText(this.Text, this.TextFont);
                textHeight = textMetrics.Height * 1.1;
                Rect textFrame = new Rect(available.Left, available.Bottom, available.Width, textHeight);
                canvas.DrawText(this.Text, textFrame, this.TextFont, this.TextAlignment, pen: null, brush: this.TextBrush);
            }

            available = new Rect(available.X, available.Y, available.Width, available.Height - textHeight);
        }
    }
}
