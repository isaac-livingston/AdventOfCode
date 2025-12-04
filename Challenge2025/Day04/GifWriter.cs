using SkiaSharp;

namespace Challenge2025.Day04;

public static class GifWriter
{
    public static void Create(string outputPath, string[] imagePaths, int delayMs = 100)
    {
        using var first = SKBitmap.Decode(imagePaths[0]);
        var width = first.Width;
        var height = first.Height;

        var palette = BuildPalette(imagePaths);

        using var output = File.Create(outputPath);
        WriteHeader(output, width, height, palette);
        WriteNetscapeExtension(output);

        foreach (var path in imagePaths)
        {
            using var bmp = SKBitmap.Decode(path);
            WriteFrame(output, bmp, palette, delayMs);
        }

        output.WriteByte(0x3B);
    }

    private static byte[] BuildPalette(string[] imagePaths)
    {
        var colors = new HashSet<int>();
        foreach (var path in imagePaths)
        {
            using var bmp = SKBitmap.Decode(path);
            var pixels = bmp.Pixels;
            foreach (var p in pixels)
                colors.Add((p.Red << 16) | (p.Green << 8) | p.Blue);
        }

        var paletteColors = colors.Take(256).ToList();
        var palette = new byte[256 * 3];
        for (int i = 0; i < paletteColors.Count; i++)
        {
            palette[i * 3] = (byte)(paletteColors[i] >> 16);
            palette[i * 3 + 1] = (byte)(paletteColors[i] >> 8);
            palette[i * 3 + 2] = (byte)paletteColors[i];
        }
        return palette;
    }

    private static void WriteHeader(Stream s, int w, int h, byte[] palette)
    {
        s.Write("GIF89a"u8);
        s.Write(BitConverter.GetBytes((ushort)w));
        s.Write(BitConverter.GetBytes((ushort)h));
        s.WriteByte(0xF7);
        s.WriteByte(0);
        s.WriteByte(0);
        s.Write(palette);
    }

    private static void WriteNetscapeExtension(Stream s) =>
        s.Write([0x21, 0xFF, 0x0B, 0x4E, 0x45, 0x54, 0x53, 0x43, 0x41, 
                 0x50, 0x45, 0x32, 0x2E, 0x30, 0x03, 0x01, 0x00, 0x00, 0x00]);

    private static void WriteFrame(Stream s, SKBitmap bmp, byte[] palette, int delayMs)
    {
        s.Write([0x21, 0xF9, 0x04, 0x00]);
        s.Write(BitConverter.GetBytes((ushort)(delayMs / 10)));
        s.Write([0x00, 0x00]);

        s.WriteByte(0x2C);
        s.Write(BitConverter.GetBytes((ushort)0));
        s.Write(BitConverter.GetBytes((ushort)0));
        s.Write(BitConverter.GetBytes((ushort)bmp.Width));
        s.Write(BitConverter.GetBytes((ushort)bmp.Height));
        s.WriteByte(0x00);

        WriteLzwData(s, bmp, palette);
    }

    private static void WriteLzwData(Stream s, SKBitmap bmp, byte[] palette)
    {
        var paletteDict = new Dictionary<int, byte>();
        for (int i = 0; i < 256; i++)
            paletteDict[(palette[i * 3] << 16) | (palette[i * 3 + 1] << 8) | palette[i * 3 + 2]] = (byte)i;

        var skPixels = bmp.Pixels;
        var pixels = new byte[skPixels.Length];
        for (int i = 0; i < skPixels.Length; i++)
        {
            var c = (skPixels[i].Red << 16) | (skPixels[i].Green << 8) | skPixels[i].Blue;
            pixels[i] = paletteDict.GetValueOrDefault(c);
        }

        s.WriteByte(8);
        var compressed = LzwEncode(pixels);

        for (int i = 0; i < compressed.Length; i += 255)
        {
            var blockSize = Math.Min(255, compressed.Length - i);
            s.WriteByte((byte)blockSize);
            s.Write(compressed, i, blockSize);
        }
        s.WriteByte(0x00);
    }

    private static byte[] LzwEncode(byte[] pixels)
    {
        const int clearCode = 256;
        const int endCode = 257;
        
        var codeSize = 9;
        var nextCode = 258;
        var table = new Dictionary<string, int>();
        
        for (int i = 0; i < 256; i++)
            table[((char)i).ToString()] = i;

        var output = new List<byte>();
        var bitBuffer = 0;
        var bitCount = 0;

        void WriteCode(int code)
        {
            bitBuffer |= code << bitCount;
            bitCount += codeSize;
            while (bitCount >= 8)
            {
                output.Add((byte)bitBuffer);
                bitBuffer >>= 8;
                bitCount -= 8;
            }
        }

        WriteCode(clearCode);
        var buffer = "";

        foreach (var pixel in pixels)
        {
            var next = buffer + (char)pixel;
            if (table.ContainsKey(next))
            {
                buffer = next;
            }
            else
            {
                WriteCode(table[buffer]);
                if (nextCode < 4096)
                {
                    table[next] = nextCode++;
                    if (nextCode > (1 << codeSize) && codeSize < 12)
                        codeSize++;
                }
                buffer = ((char)pixel).ToString();
            }
        }

        if (buffer.Length > 0)
            WriteCode(table[buffer]);

        WriteCode(endCode);
        if (bitCount > 0)
            output.Add((byte)bitBuffer);

        return [.. output];
    }
}
