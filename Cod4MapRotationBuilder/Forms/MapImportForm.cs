using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cod4MapRotationBuilder.Forms
{
    public partial class MapImportForm : Form
    {
        public MapImportForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        public IEnumerable<string> Maps { get; private set; }


        public string MapDownloadURL
        {
            get { return downloadTextBox.Text; }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Uri uri;
            try
            {
                uri = new Uri(downloadTextBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid URL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            groupBox1.Enabled = false;

            try
            {
                var webClient = new WebClient();
                var result = await webClient.DownloadStringTaskAsync(uri);

                Regex regex = new Regex("\">mp_(.*?)<\\/A>");

                var matches = regex.Matches(result);

                Maps = matches.OfType<Match>().Select(m => "mp_" + m.Groups[1]);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to gather information from specified URL", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                groupBox1.Enabled = true;

                return;
            }
        }
    }
}
