using SkiaSharp;


namespace DeckMancer.Core
{
    public class ImageLoader
    {
        public static byte[] LoadImage(string filePath)
        {
            SKEncodedImageFormat format = GetImageFormat(filePath);

            using (var bitmap = SKBitmap.Decode(filePath))
            {
                using (var image = SKImage.FromBitmap(bitmap))
                {
                    using (var data = image.Encode(format, 100))
                    {
                        return data.ToArray();
                    }
                }
            }
        }
        public static SKBitmap LoadBitmap(string filePath)
        {
            var bitmap = SKBitmap.Decode(filePath);
       
            return bitmap;
        }

        public static SKEncodedImageFormat GetImageFormat(string filePath)
        {
            string extension = System.IO.Path.GetExtension(filePath)?.ToLowerInvariant();

            switch (extension)
            {
                case ".png":
                    return SKEncodedImageFormat.Png;
                case ".jpg":
                case ".jpeg":
                    return SKEncodedImageFormat.Jpeg;

            }
            return 0;
        }
    }
}
