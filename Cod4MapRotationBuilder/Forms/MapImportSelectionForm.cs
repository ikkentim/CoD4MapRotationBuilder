using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cod4MapRotationBuilder.Providers;

namespace Cod4MapRotationBuilder.Forms
{
    public partial class MapImportSelectionForm : Form
    {
        private readonly string _downloadurl;
        private readonly MapsProvider _mapsProvider;

        public MapImportSelectionForm(IEnumerable<string> maps, string downloadurl, MapsProvider mapsProvider)
        {
            InitializeComponent();

            DialogResult = DialogResult.Cancel;
            webClient.DownloadProgressChanged += WebClientDownloadProgressChanged;

            _downloadurl = downloadurl;
            _mapsProvider = mapsProvider;
            mapsProvider.Refresh();
            var totalCount = maps.Count();
            var existsCount = 0;
            foreach (var map in maps.OrderBy(m => m))
            {
                if (mapsProvider.Collection.Any(m => m.Name == map))
                {
                    existsCount++;
                    continue;
                }

                checkedListBox.Items.Add(map);
            }

            MessageBox.Show(
                string.Format("Found {0} maps, of which you already have downloaded {1}. Showing remaining maps...", totalCount, existsCount),
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("button2_Click!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < checkedListBox.Items.Count; i++)
                checkedListBox.SetItemChecked(i, !checkedListBox.GetItemChecked(i));
        }

        #region Overrides of Form

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Closing"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data. </param>
        protected override void OnClosing(CancelEventArgs e)
        {
            Debug.WriteLine("OnClosing!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            if (isDownloading)
            {
                e.Cancel = true;
            }
            else
            {
                DialogResult = hasDoneSomething ? DialogResult.OK : DialogResult.Cancel;
            }
        }

        #endregion

        private bool hasDoneSomething;
        private bool isCanceled;
        private bool isDownloading;

        WebClient webClient = new WebClient();

        private async Task DownloadFile(Uri uri, string path)
        {
            Log(string.Format("Downloading {0}...", uri));
            try
            {
                await webClient.DownloadFileTaskAsync(uri, path);
                Log("Done!\n");
            }
            catch (Exception)
            {
                Log("Failed. Skipping...\n");
            }
        }

        private async Task DownloadUsermap(string name, string type)
        {
            var folder = MapsProvider.GetPathForUserMap(name);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            await
                DownloadFile(new Uri(string.Format("{0}/{1}/{1}{2}", _downloadurl, name, type)),
                    Path.Combine(folder, name + type));
        }
        private void Log(string message)
        {
            if (logTextBox.InvokeRequired)
            {
                logTextBox.Invoke((MethodInvoker)(() =>
                {
                    logTextBox.AppendText(message);
                    logTextBox.SelectionStart = logTextBox.TextLength;
                    logTextBox.ScrollToCaret();
                }));
            }
            else
            {

                logTextBox.AppendText(message);
                logTextBox.SelectionStart = logTextBox.TextLength;
                logTextBox.ScrollToCaret();
            }
        }

        private async void downloadButton_Click(object sender, EventArgs e)
        {
            isCanceled = false;
            isDownloading = true;

            mapsGroupBox.Enabled = false;
            downloadButton.Enabled = false;
            invertSelectionButton.Enabled = false;

            if (!Directory.Exists(MapsProvider.CallOfDuty4UserMapsPath)) return;

            var count = 0;
            while (!isCanceled && checkedListBox.CheckedItems.Count > 0)
            {
                var file = checkedListBox.CheckedItems[0] as string;
                if (file == null) return;

                await DownloadUsermap(file, ".iwd");
                if (!checkBox1.Checked)
                {
                    await DownloadUsermap(file, ".ff");
                    await DownloadUsermap(file, "_load.ff");
                }

                if (!isCanceled)
                {
                    checkedListBox.Items.Remove(file);
                    hasDoneSomething = true;
                }

                progressBar.Value = 0;
                count ++;
            }

            if (isCanceled)
                Log("Stopped downloading after " + count + " maps. Operation was canceled.\n\n");
            else
                Log("Completed downloading after " + count + " maps.\n\n");
        
            isDownloading = false;

            mapsGroupBox.Enabled = true;
            downloadButton.Enabled = true;
            invertSelectionButton.Enabled = true;
        }

        private void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = Math.Min(100, Math.Max(0, e.ProgressPercentage));
        }
    }
}
