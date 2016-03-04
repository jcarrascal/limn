using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBytes.Limn.Tests
{
    using System.IO;
    using NGraphics;

    internal static class CanvasExtensions
    {
        public static IImageCanvas CreateColoredCanvas(Size size)
        {
            var backgroundBrush = new LinearGradientBrush(Point.Zero, new Point(1, 1), Color.FromRGB(0xB9DAFF), Color.FromRGB(0x93C5FF));
            var canvas = Platforms.Current.CreateImageCanvas(size);
            canvas.FillRectangle(0, 0, size.Width, size.Height, backgroundBrush);
            return canvas;
        }

        public static byte[] AsPngBytes(this IImageCanvas canvas)
        {
            using (var ms = new MemoryStream())
            {
                canvas.GetImage().SaveAsPng(ms);
                byte[] results = ms.ToArray();
                return results;
            }
        }
    }
}
