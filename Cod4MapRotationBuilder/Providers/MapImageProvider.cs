// Cod4MapRotationBuilder
// Copyright 2015 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Cod4MapRotationBuilder.Data;
using ImageMagick;

namespace Cod4MapRotationBuilder.Providers
{
    /// <summary>
    ///     Represents a <see cref="Map" /> <see cref="Image" /> provider.
    /// </summary>
    public static class MapImageProvider
    {
        /// <summary>
        ///     Gets the IWD-file path for the specified <paramref name="map" />.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <returns>The IWD-file path.</returns>
        private static string GetIWDPath(Map map)
        {
            if (map.Path == null) return null;

            string path = Path.Combine(map.Path, string.Format("{0}.iwd", map.Name));
            return File.Exists(path) ? path : null;
        }

        /// <summary>
        ///     Converts IWI byte map to DDS byte map.
        /// </summary>
        /// <param name="iwi">The iwi byte map.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">iwi</exception>
        /// <exception cref="ArgumentException">
        ///     Invalid iwi file.
        ///     or
        ///     Unsupported iwi version.
        ///     or
        ///     Invalid iwi size.
        ///     or
        /// </exception>
        private static byte[] IWI2DDS(byte[] iwi)
        {
            // Default dds header
            byte[] ddsHeader =
            {
                0x44, 0x44, 0x53, 0x20, 0x7c, 0x00, 0x00, 0x00, 0x07, 0x10, 0x02, 0x00, 0x00, 0x04, 0x00, 0x00, //0
                0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, //1
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //2
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //3
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, //4
                0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //5
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x40, 0x00 //6
            };

            const int iwiHeaderLength = 0x1C;
            const int ddsHeaderLength = 0x80;

            if (iwi == null) throw new ArgumentNullException("iwi");

            if (iwi.Length < iwiHeaderLength || new string(iwi.Take(3).Select(b => (char) b).ToArray()) != "IWi")
                throw new ArgumentException("Invalid iwi file.");

            byte version = iwi[0x03];

            if (version != 0x06)
                throw new ArgumentException("Unsupported iwi version.");

            int filesize = BitConverter.ToInt32(iwi.Skip(0x0C).Take(4).ToArray(), 0);

            if (iwi.Length != filesize)
                throw new ArgumentException("Invalid iwi size.");

            int[] mipmaps =
            {
                BitConverter.ToInt32(iwi.Skip(0x10).Take(4).ToArray(), 0),
                BitConverter.ToInt32(iwi.Skip(0x14).Take(4).ToArray(), 0),
                BitConverter.ToInt32(iwi.Skip(0x18).Take(4).ToArray(), 0)
            };

            int mipmap = mipmaps.Max();

            int imageOffset = mipmap == filesize ? iwiHeaderLength : mipmap;
            int imageSize = filesize - imageOffset;

            // Create dds image buffer
            var dds = new byte[imageSize + ddsHeaderLength];

            // Copy default dds buffer
            Array.Copy(ddsHeader, dds, ddsHeader.Length);

            // Copy header values
            Array.Copy(iwi, 0x06, dds, 0x0c, 2); // ports: 0x06-0x07 => 0x0c-0x0d (width)
            Array.Copy(iwi, 0x08, dds, 0x10, 2); // ports: 0x08-0x09 => 0x10-0x11 (height)

            Array.Copy(iwi, 0x0a, dds, 0x1c, 2); // ports: 0x0a-0x0b => 0x1c-0x1d (???)

            byte format = iwi[0x04];
            switch (format)
            {
                case 0x0b:
                    dds[0x54] = (byte) 'D';
                    dds[0x55] = (byte) 'X';
                    dds[0x56] = (byte) 'T';
                    dds[0x57] = (byte) '1';
                    break;

                case 0x0c:
                    dds[0x54] = (byte) 'D';
                    dds[0x55] = (byte) 'X';
                    dds[0x56] = (byte) 'T';
                    dds[0x57] = (byte) '3';
                    break;
                case 0x0d:
                    dds[0x54] = (byte) 'D';
                    dds[0x55] = (byte) 'X';
                    dds[0x56] = (byte) 'T';
                    dds[0x57] = (byte) '5';
                    break;
                default:
                    throw new ArgumentException(string.Format("Unsupported DXT format 0x{0:X}.", format));
            }
            //0x1 = ARGB8, 0x2 = RGB8, 0x3 = ARGB4, 0x4 = A8, 0x7 = JPG, 0xB = DXT1, 0xC = DXT3, 0xD = DXT5

            // Copy body
            Array.Copy(iwi, imageOffset, dds, ddsHeaderLength, imageSize);

            return dds;
        }

        /// <summary>
        ///     Creates an error image for the specified <paramref name="error" />.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>The created error image.</returns>
        private static Image CreateErrorImage(string error)
        {
            var bmp = new Bitmap(512, 512);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(SystemColors.ControlDark);
                using (var f = new Font("Arial", 25))
                {
                    SizeF s = g.MeasureString(error, f);
                    g.DrawString(error, f, Brushes.Black, (bmp.Width - s.Width)/2, (bmp.Height - s.Height)/2);
                }
            }
            return bmp;
        }

        /// <summary>
        ///     Gets the specified <paramref name="image" /> from the IWD-file of the specified <paramref name="map" />.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <param name="image">The image name.</param>
        /// <returns>The image</returns>
        private static Image GetImageFromIwd(Map map, string image)
        {
            if (map == null) return null;

            string iwdPath = GetIWDPath(map);
            if (iwdPath == null) return null;

            try
            {
                using (FileStream stream = File.OpenRead(iwdPath))
                using (var zip = new ZipArchive(stream))
                {
                    ZipArchiveEntry entry = zip.Entries.FirstOrDefault(e => e.Name.Equals(image));

                    if (entry == null) return CreateErrorImage("No preview available.");
                    if (entry.Length > int.MaxValue) return null;

                    using (Stream entryStream = entry.Open())
                    {
                        var buffer = new byte[entry.Length];
                        entryStream.Read(buffer, 0, (int) entry.Length);

                        Image img;
                        using (var magickImage = new MagickImage(IWI2DDS(buffer),
                            new MagickReadSettings {Format = MagickFormat.Dds}))
                            img = magickImage.ToBitmap();

                        return img;
                    }
                }
            }
            catch (Exception e)
            {
                return CreateErrorImage(e.Message);
            }
        }

        /// <summary>
        ///     Gets the loadscreen image.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <returns>The image</returns>
        public static Image GetLoadscreenImage(Map map)
        {
            return GetImageFromIwd(map, string.Format("loadscreen_{0}.iwi", map.Name));
        }

        /// <summary>
        ///     Gets the loadscreen thumbnail.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <returns>The image</returns>
        public static Image GetLoadscreenThumbnail(Map map)
        {
            using (Image img = GetLoadscreenImage(map))
            {
                return img == null ? null : new Bitmap(img, 64, 64);
            }
        }

        /// <summary>
        ///     Gets the compass image.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <returns>The image.</returns>
        public static Image GetCompassImage(Map map)
        {
            return GetImageFromIwd(map, string.Format("compass_map_{0}.iwi", map.Name));
        }
    }
}