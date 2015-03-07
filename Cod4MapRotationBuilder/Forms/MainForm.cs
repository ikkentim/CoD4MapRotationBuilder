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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Cod4MapRotationBuilder.Collections;
using Cod4MapRotationBuilder.Providers;

namespace Cod4MapRotationBuilder.Forms
{
    /// <summary>
    ///     The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly GameModesProvider _gameModesProvider = new GameModesProvider();
        private readonly MapRotation _mapRotation = new MapRotation();
        private readonly MapsProvider _mapsProvider = new MapsProvider();

        private string _currentFilePath;
        private bool _isFileModified;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainForm" /> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // Register MapRotation events
            _mapRotation.ElementAdded += _mapRotation_ElementAdded;
            _mapRotation.ElementRemoved += _mapRotation_ElementRemoved;
            _mapRotation.ElementUpdated += _mapRotation_ElementUpdated;
            _mapRotation.ElementsSwapped += _mapRotation_ElementsSwapped;

            // Prepare MapRotationEditor properties
            mapRotationEditor.MapRotation = _mapRotation;

            // Prepare RotationElementEditor properties
            rotationElementEditor.MapsProvider = _mapsProvider;
            rotationElementEditor.GameModesProvider = _gameModesProvider;
            rotationElementEditor.ThumbnailLoadingProgressChanged +=
                rotationElementEditor_ThumbnailLoadingProgressChanged;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Check CoD4 path is OK
            if (!_mapsProvider.AssertPathOk(this))
            {
                Close();
                return;
            }

            // Load data
            ReloadData();
        }

        #region Properties

        /// <summary>
        ///     Gets the display file modified string.
        /// </summary>
        public string DisplayFileModifiedString
        {
            get { return IsFileModified ? "*" : string.Empty; }
        }

        /// <summary>
        ///     Gets the display file name string.
        /// </summary>
        public string DisplayFileNameString
        {
            get
            {
                return string.IsNullOrWhiteSpace(_currentFilePath) ? "New file" : Path.GetFileName(_currentFilePath);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the file modified.
        /// </summary>
        public bool IsFileModified
        {
            get { return _isFileModified; }
            set
            {
                _isFileModified = value;
                Text = string.Format("{0}{1} - Map Rotation Builder", DisplayFileNameString, DisplayFileModifiedString);
            }
        }

        #endregion

        #region Data loading methods

        /// <summary>
        ///     Reloads the data.
        /// </summary>
        private void ReloadData()
        {
            rotationElementPreview.Element = null;
            _mapRotation.Clear();

            _gameModesProvider.Refresh();
            _mapsProvider.Refresh();

            mapRotationEditor.NextGameMode = _gameModesProvider.Collection.FirstOrDefault();
            mapRotationEditor.NextMap = _mapsProvider.Collection.FirstOrDefault();
        }

        #endregion

        #region Saving / Loading

        /// <summary>
        ///     Saves the rotation to the file at the specified <paramref name="path" />.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>True on success; False otherwise.</returns>
        private bool SaveToFile(string path)
        {
            try
            {
                File.WriteAllText(path, _mapRotation.ToString());

                IsFileModified = false;
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        ///     Prompts where to save it and saves it there.
        /// </summary>
        /// <returns>True on success; False otherwise.</returns>
        private bool SaveAs()
        {
            var dialog = new SaveFileDialog
            {
                CheckPathExists = true,
                Filter = "*.txt|*.txt"
            };

            return dialog.ShowDialog(this) == DialogResult.OK && SaveToFile(_currentFilePath = dialog.FileName);
        }

        /// <summary>
        ///     News this instance.
        /// </summary>
        private void New()
        {
            if (!AskForSaveBeforeClose()) return;

            _mapRotation.Clear();

            _currentFilePath = null;
            IsFileModified = false;
        }

        /// <summary>
        ///     Opens a file specified by prompt.
        /// </summary>
        private void Open()
        {
            if (!AskForSaveBeforeClose()) return;

            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "*.txt|*.txt"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            _mapRotation.Clear();
            try
            {
                _mapRotation.Add(File.ReadAllText(_currentFilePath = dialog.FileName), _mapsProvider.Collection,
                    _gameModesProvider.Collection);
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            IsFileModified = false;
        }

        /// <summary>
        ///     Saves the file, prompts where to save it if not saved before.
        /// </summary>
        /// <returns>True on success; False otherwise.</returns>
        private bool Save()
        {
            return string.IsNullOrWhiteSpace(_currentFilePath) ? SaveAs() : SaveToFile(_currentFilePath);
        }

        /// <summary>
        ///     Asks for save before close.
        /// </summary>
        /// <returns>True on success; False otherwise.</returns>
        private bool AskForSaveBeforeClose()
        {
            if (!IsFileModified) return true;

            DialogResult response = MessageBox.Show(this,
                string.Format("You have unsaved changes!\nDo you want to save {0} before closing the file?",
                    DisplayFileNameString),
                "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (response == DialogResult.Cancel) return false;
            return response == DialogResult.No || Save();
        }

        #endregion

        #region MapRotation events

        private void _mapRotation_ElementAdded(object sender, RotationElementEventArgs e)
        {
            IsFileModified = true;
        }

        private void _mapRotation_ElementRemoved(object sender, RotationElementEventArgs e)
        {
            IsFileModified = true;
        }

        private void _mapRotation_ElementUpdated(object sender, RotationElementEventArgs e)
        {
            mapRotationEditor.NextMap = e.Element.Map;
            mapRotationEditor.NextGameMode = e.Element.GameMode;
        }

        private void _mapRotation_ElementsSwapped(object sender, RotationElementsSwappedEventArgs e)
        {
            IsFileModified = true;
        }

        #endregion

        #region MapRotationEditor events

        private void mapRotationEditor_SelectedElementChanged(object sender, EventArgs e)
        {
            // Display element data on screen
            rotationElementPreview.Element = mapRotationEditor.SelectedElement;
            rotationElementEditor.SelectedElement = mapRotationEditor.SelectedElement;

            // If none selected, don't set the default map and game mode
            if (mapRotationEditor.SelectedElement == null) return;

            // Set the default map and game mode
            mapRotationEditor.NextMap = mapRotationEditor.SelectedElement.Map;
            mapRotationEditor.NextGameMode = mapRotationEditor.SelectedElement.GameMode;
        }

        #endregion

        #region User interaction events

        private void exportAsPlainTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var exporter = new PlainTextExporterForm(_mapRotation);
            exporter.ShowDialog(this);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog(this);
            ReloadData();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        #endregion

        #region RotationElement events

        private void rotationElementEditor_ThumbnailLoadingProgressChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = toolStripProgressBar.Value%100 == 0 ? "Ready" : "Loading map thumbnails...";
            toolStripProgressBar.Value = rotationElementEditor.ThumbnailLoadingProgress % 100;
        }

        #endregion

        #region Overrides of Form

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Forms.Form.Closing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (!AskForSaveBeforeClose())
            {
                e.Cancel = true;
                return;
            }

            base.OnClosing(e);
        }

        #endregion
    }
}