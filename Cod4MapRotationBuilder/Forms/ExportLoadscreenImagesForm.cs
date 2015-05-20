using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cod4MapRotationBuilder.Providers;

namespace Cod4MapRotationBuilder.Forms
{
    public partial class ExportLoadscreenImagesForm : Form
    {
        private readonly MapsProvider _mapsProvider;
        private readonly string _outputPath;
        public ExportLoadscreenImagesForm(MapsProvider mapsProvider, string outputPath)
        {
            _mapsProvider = mapsProvider;
            _outputPath = outputPath;

            InitializeComponent();
            progressBar1.Value = 10;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 100;
            ExportImages();
        }

        private async void ExportImages()
        {
            await Task.Run(() =>
            {
                foreach (var map in _mapsProvider.Collection)
                {
                    try
                    {
                        map.LoadscreenImage.Save(Path.Combine(_outputPath, map.Name + ".jpg"), ImageFormat.Jpeg);
                    }
                    catch
                    {
                        
                    }
                }
            });
            Close();
        }
    }
}
