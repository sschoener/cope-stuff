using System.Drawing;

namespace cope.Graphics
{
    /// <summary>
    /// Interface for classes processing Bitmaps.
    /// </summary>
    public interface IBitmapProcessor
    {
        Bitmap ProcessBitmap(Bitmap bmp);
        int GetProcessedPixels();
        int GetPixelCount();
    }
}