using System;
using NGraphics;

namespace DotNetBytes.Limn.Charting
{
    public class Title
    {
        #region Properties

        /// <summary>
        /// Gets or sets the alignment.
        /// </summary>
        /// <value>
        /// The alignment.
        /// </value>
        public TextAlignment Alignment { get; set; } = TextAlignment.Center;

        /// <summary>
        /// Gets or sets the sub title.
        /// </summary>
        /// <value>
        /// The sub title.
        /// </value>
        public string SubTitle { get; set; }

        /// <summary>
        /// Gets or sets the sub title brush.
        /// </summary>
        /// <value>
        /// The sub title brush.
        /// </value>
        public Brush SubTitleBrush { get; set; } = new SolidBrush(Colors.Black);

        /// <summary>
        /// Gets or sets the sub title font.
        /// </summary>
        /// <value>
        /// The sub title font.
        /// </value>
        public Font SubTitleFont { get; set; } = new Font("Arial", 10);

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; private set; }

        /// <summary>
        /// Gets or sets the text brush.
        /// </summary>
        /// <value>
        /// The text brush.
        /// </value>
        public Brush TextBrush { get; set; } = new SolidBrush(Colors.Black);

        /// <summary>
        /// Gets or sets the text font.
        /// </summary>
        /// <value>
        /// The text font.
        /// </value>
        public Font TextFont { get; set; } = new Font("Arial", 12);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Title" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public Title(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(nameof(text));
            }

            this.Text = text;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draws this Title onto the specified canvas.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="available">The available.</param>
        public void Draw(ICanvas canvas, ref Rect available)
        {
            Size titleMetrics = canvas.MeasureText(this.Text, this.TextFont);
            double textHeight = titleMetrics.Height * 1.1;
            Rect titleFrame = new Rect(available.Left, available.Top + textHeight, available.Width, textHeight);
            canvas.DrawText(this.Text, titleFrame, this.TextFont, this.Alignment, pen: null, brush: this.TextBrush);

            double subTitleHeight = 0;
            if (!string.IsNullOrWhiteSpace(this.SubTitle))
            {
                Size subTitleMetrics = canvas.MeasureText(this.SubTitle, this.SubTitleFont);
                subTitleHeight = subTitleMetrics.Height * 1.1;
                Rect subTitleFrame = new Rect(available.Left, titleFrame.Y + subTitleHeight, available.Width, subTitleHeight);
                canvas.DrawText(this.SubTitle, subTitleFrame, this.SubTitleFont, this.Alignment, pen: null, brush: this.SubTitleBrush);
            }

            available = new Rect(available.X, available.Y + textHeight + subTitleHeight, available.Width, available.Height - textHeight - subTitleHeight);
        }

        #endregion
    }
}