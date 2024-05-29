using System;

namespace DeckMancer.Core
{
    [Serializable]
    public enum WarpMode 
    {
        Repeat,
        Clamp,
        Mirror
    }
    [Serializable]
    public enum FilterMode 
    {
        NoFilter,
        Bilinear,
        Trilinear
    }
    [Serializable]
    public enum AnisotropicMode 
    {
        No = 0,
        _2X = 2,
        _4X = 4,
        _8X = 8,
        _16X = 16
    }
    [Serializable]
    public class Texture
    {
        public int Width { get; }
        public int Height { get; }
        public byte[] Data { get; }
        public WarpMode WarpMode_ { get; }
        public FilterMode FilterMode_ { get; }
        public AnisotropicMode AnisotropicMode_ { get; }

        public Texture()
        {
            WarpMode_ = WarpMode.Repeat;
            FilterMode_ = FilterMode.NoFilter;
            AnisotropicMode_ = AnisotropicMode.No;

            Width = 1;
            Height = 1;
            Data  = new byte[]
            {
                255, 255, 255, 255
            };
        }
        public Texture(int width, int height, byte[] data, WarpMode warpMode = WarpMode.Repeat,
            FilterMode filterMode = FilterMode.Bilinear, AnisotropicMode anisotropicMode = AnisotropicMode.No)
        {
            Width = width;
            Height = height;
            Data = data;
            WarpMode_ = warpMode;
            FilterMode_ = filterMode;
            AnisotropicMode_ = anisotropicMode;
        }
    }
}