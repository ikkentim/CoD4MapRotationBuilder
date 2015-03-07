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
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cod4MapRotationBuilder.Collections;
using Cod4MapRotationBuilder.Data;
using Cod4MapRotationBuilder.Providers;

namespace Cod4MapRotationBuilder.UI
{
    /// <summary>
    ///     RotationElement editor control.
    /// </summary>
    public partial class RotationElementEditor : UserControl
    {
        private readonly object _mapsAwaitingThumbnailLock = new object();
        private readonly Bitmap _placeholderBitmap = new Bitmap(1, 1);
        private GameModesProvider _gameModesProvider;
        private bool _isGeneratingMapImages;
        private MapsProvider _mapProvider;
        private Queue<ListViewItem> _mapsAwaitingThumbnail = new Queue<ListViewItem>();
        private RotationElement _selectedElement;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RotationElementEditor" /> class.
        /// </summary>
        public RotationElementEditor()
        {
            InitializeComponent();
        }

        #region Properties

        /// <summary>
        ///     Gets the thumbnail loading progress.
        /// </summary>
        public int ThumbnailLoadingProgress { get; private set; }

        /// <summary>
        ///     Gets or sets the maps provider.
        /// </summary>
        public MapsProvider MapsProvider
        {
            get { return _mapProvider; }
            set
            {
                if (_mapProvider == value) return;
                if (_mapProvider != null)
                {
                    _mapProvider.Collection.MapAdded -= Collection_MapAdded;
                    _mapProvider.Collection.MapRemoved -= Collection_MapRemoved;
                }
                if (value != null)
                {
                    value.Collection.MapAdded += Collection_MapAdded;
                    value.Collection.MapRemoved += Collection_MapRemoved;
                }

                lock (_mapsAwaitingThumbnailLock)
                {
                    _mapsAwaitingThumbnail.Clear();
                }

                mapsListView.Items.Clear();
                imageList.Images.Clear();

                _mapProvider = value;
                FillMaps();
                CheckStateOk();
            }
        }

        /// <summary>
        ///     Gets or sets the game modes provider.
        /// </summary>
        public GameModesProvider GameModesProvider
        {
            get { return _gameModesProvider; }
            set
            {
                if (_gameModesProvider == value) return;
                if (_gameModesProvider != null)
                {
                    _gameModesProvider.Collection.GameModeAdded -= Collection_GameModeAdded;
                    _gameModesProvider.Collection.GameModeRemoved -= Collection_GameModeRemoved;
                }
                if (value != null)
                {
                    value.Collection.GameModeAdded += Collection_GameModeAdded;
                    value.Collection.GameModeRemoved += Collection_GameModeRemoved;
                }

                gameModesComboBox.Items.Clear();
                _gameModesProvider = value;
                FillGameModes();
                CheckStateOk();
            }
        }

        /// <summary>
        ///     Gets or sets the selected element.
        /// </summary>
        public RotationElement SelectedElement
        {
            get { return _selectedElement; }
            set
            {
                CleanUp();
                _selectedElement = value;
                SetInfo();
                CheckStateOk();
            }
        }

        #endregion

        /// <summary>
        ///     Occurs when the thumbnail loading progress changed.
        /// </summary>
        public event EventHandler ThumbnailLoadingProgressChanged;

        /// <summary>
        ///     Raises the <see cref="ThumbnailLoadingProgressChanged" /> event.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnThumbnailLoadingProgressChanged(EventArgs args)
        {
            if (ThumbnailLoadingProgressChanged != null)
                ThumbnailLoadingProgressChanged(this, args);
        }

        #region Loading/setting data in element

        /// <summary>
        ///     Checks the state is OK. If OK, enable this control.
        /// </summary>
        private void CheckStateOk()
        {
            Enabled = !(MapsProvider == null || GameModesProvider == null || SelectedElement == null);
        }

        /// <summary>
        ///     Cleans up.
        /// </summary>
        private void CleanUp()
        {
            mapTextBox.Text = string.Empty;
            mapsListView.SelectedItems.Clear();
            gameModesComboBox.SelectedIndex = -1;
        }

        /// <summary>
        ///     Fills the game modes.
        /// </summary>
        private void FillGameModes()
        {
            gameModesComboBox.Items.Clear();
            gameModesComboBox.Items.AddRange(GameModesProvider.Collection.ToArray());
        }

        /// <summary>
        ///     Fills the maps.
        /// </summary>
        private void FillMaps()
        {
            lock (_mapsAwaitingThumbnailLock)
            {
                _mapsAwaitingThumbnail.Clear();
            }
            mapsListView.Items.Clear();

            foreach (ListViewItem item in MapsProvider.Collection.Select(m =>
                new ListViewItem(m.Name) {Tag = m}).ToArray())
            {
                LoadThumbnailForMap(item.Tag as Map, item);
                mapsListView.Items.Add(item);
            }
        }

        /// <summary>
        ///     Sets the information.
        /// </summary>
        private void SetInfo()
        {
            if (SelectedElement == null) return;

            mapsListView.SelectedItems.Clear();
            ListViewItem item =
                mapsListView.Items.OfType<ListViewItem>().FirstOrDefault(i => i.Tag == SelectedElement.Map);
            if (item != null) item.Selected = true;

            gameModesComboBox.SelectedItem = SelectedElement.GameMode;
            mapTextBox.Text = SelectedElement.Map.Name;
        }

        #endregion

        #region Thumbnail loading

        /// <summary>
        ///     Unloads the thumbnail for map.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <param name="item">The item.</param>
        private void UnloadThumbnailForMap(Map map, ListViewItem item)
        {
            //Remove from waiting list
            lock (_mapsAwaitingThumbnailLock)
            {
                if (_mapsAwaitingThumbnail.Contains(item))
                    _mapsAwaitingThumbnail = new Queue<ListViewItem>(_mapsAwaitingThumbnail.Except(new[] {item}));
            }
        }

        /// <summary>
        ///     Sets the thumbnail.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="icon">The icon.</param>
        private void SetThumbnail(ListViewItem item, Image icon)
        {
            imageList.Images[item.ImageIndex] = icon;
            mapsListView.Invalidate();
        }

        private int _thumbnailProcessed;

        /// <summary>
        ///     Generates the map thumbnails async.
        /// </summary>
        private async void GenerateMapThumbnailsASync()
        {
            if (_isGeneratingMapImages) return;
            _isGeneratingMapImages = true;
            await Task.Run(() =>
            {
                while (_mapsAwaitingThumbnail.Count > 0 && !Disposing && !IsDisposed)
                {
                    ListViewItem item;
                    lock (_mapsAwaitingThumbnailLock)
                        item = _mapsAwaitingThumbnail.Dequeue();

                    var map = item.Tag as Map;
                    Image icon = map.Icon;
                    if (icon != null)
                    {
                        if (mapsListView.InvokeRequired)
                            mapsListView.Invoke((MethodInvoker) (() => SetThumbnail(item, icon)));
                        else
                            SetThumbnail(item, icon);
                    }

                    _thumbnailProcessed++;

                    ThumbnailLoadingProgress =
                        (int) ((100.0f/(_thumbnailProcessed + _mapsAwaitingThumbnail.Count))*_thumbnailProcessed);

                    if (InvokeRequired)
                        Invoke((MethodInvoker) (() => OnThumbnailLoadingProgressChanged(EventArgs.Empty)));
                    else
                        OnThumbnailLoadingProgressChanged(EventArgs.Empty);
                }
            });

            ThumbnailLoadingProgress = 100;
            _thumbnailProcessed = 0;
            if (InvokeRequired)
                Invoke((MethodInvoker)(() => OnThumbnailLoadingProgressChanged(EventArgs.Empty)));
            else
                OnThumbnailLoadingProgressChanged(EventArgs.Empty);

            _isGeneratingMapImages = false;
        }

        /// <summary>
        ///     Loads the thumbnail for map.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <param name="item">The item.</param>
        private void LoadThumbnailForMap(Map map, ListViewItem item)
        {
            if (map.Path != null)
            {
                int index = imageList.Images.Count;
                imageList.Images.Add(_placeholderBitmap);
                item.ImageIndex = index;

                _mapsAwaitingThumbnail.Enqueue(item);

                GenerateMapThumbnailsASync();
            }
        }

        #endregion

        #region MapCollection events

        private void Collection_MapRemoved(object sender, MapEventArgs e)
        {
            ListViewItem item = mapsListView.Items.OfType<ListViewItem>().FirstOrDefault(i => i.Tag == e.Map);

            if (item != null)
            {
                //_placeholdBitmap

                UnloadThumbnailForMap(e.Map, item);
                mapsListView.Items.Remove(item);
            }

            if (mapsListView.Items.Count == 0)
            {
                Debug.WriteLine("Cleaned imagelist");
                imageList.Images.Clear(); // Quick cleanup
            }
        }

        private void Collection_MapAdded(object sender, MapEventArgs e)
        {
            var item = new ListViewItem(e.Map.Name) {Tag = e.Map};
            mapsListView.Items.Add(item);
            LoadThumbnailForMap(e.Map, item);
        }

        #endregion

        #region GameModeCollection events

        private void Collection_GameModeRemoved(object sender, GameModeEventArgs e)
        {
            gameModesComboBox.Items.Remove(e.GameMode);
        }

        private void Collection_GameModeAdded(object sender, GameModeEventArgs e)
        {
            GameMode[] ordered =
                gameModesComboBox.Items.OfType<GameMode>().Concat(new[] {e.GameMode}).OrderBy(g => g.Name).ToArray();

            gameModesComboBox.Items.Clear();
            gameModesComboBox.Items.AddRange(ordered);
        }

        #endregion

        #region gameModesComboBox events

        private void gameModesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedElement == null || gameModesComboBox.SelectedItem == null) return;

            SelectedElement.GameMode = gameModesComboBox.SelectedItem as GameMode;
        }

        #endregion

        #region mapTextBox events

        private void mapTextBox_TextChanged(object sender, EventArgs e)
        {
            if (SelectedElement == null || !mapTextBox.Text.StartsWith("mp_")) return;

            SelectedElement.Map = MapsProvider.Collection[mapTextBox.Text];

            mapsListView.SelectedItems.Clear();

            ListViewItem item =
                mapsListView.Items.OfType<ListViewItem>().FirstOrDefault(i => i.Tag == SelectedElement.Map);
            if (item != null) item.Selected = true;
        }

        #endregion

        #region mapsListView events

        private void mapsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedElement == null || mapsListView.SelectedItems.Count == 0) return;

            var map = mapsListView.SelectedItems[0].Tag as Map;
            if (map == null) return;

            SelectedElement.Map = map;
            mapTextBox.Text = map.Name;
        }

        #endregion

        #region goToMapFolderButton events

        private void goToMapFolderButton_Click(object sender, EventArgs e)
        {
            if (SelectedElement == null || SelectedElement.Map.Path == null) return;
            Process.Start(SelectedElement.Map.Path);
        }

        #endregion

    }
}