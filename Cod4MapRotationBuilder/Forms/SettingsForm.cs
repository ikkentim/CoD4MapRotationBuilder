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
using System.Linq;
using System.Windows.Forms;
using Cod4MapRotationBuilder.Properties;

namespace Cod4MapRotationBuilder.Forms
{
    /// <summary>
    ///     Settings form.
    /// </summary>
    public partial class SettingsForm : Form
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SettingsForm" /> class.
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();

            LoadData();
        }

        /// <summary>
        ///     Loads the data.
        /// </summary>
        private void LoadData()
        {
            mapsTextBox.Text = string.Join("\r\n", Settings.Default.StockMaps.Split(';'));
            modesTextBox.Text = string.Join("\r\n", Settings.Default.GameModes.Split(';'));
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Settings.Default.StockMaps = string.Join(";",
                mapsTextBox.Text.Split('\n').Select(t => t.Trim()).Where(t => t.StartsWith("mp_")));

            Settings.Default.GameModes = string.Join(";",
                modesTextBox.Text.Split('\n')
                    .Select(t => t.Trim())
                    .Where(t => t.Split(':').All(n => n.Trim().Length > 0)));

            Settings.Default.Save();

            Close();
        }
    }
}