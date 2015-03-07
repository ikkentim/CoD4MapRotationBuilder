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
using Cod4MapRotationBuilder.Collections;
using Cod4MapRotationBuilder.Data;

namespace Cod4MapRotationBuilder.UI
{
    /// <summary>
    ///     MapRotation editor control.
    /// </summary>
    public partial class MapRotationEditor : UserControl
    {
        private MapRotation _mapRotation;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MapRotationEditor" /> class.
        /// </summary>
        public MapRotationEditor()
        {
            InitializeComponent();

            UpdateListViewItemCount();
        }

        /// <summary>
        ///     Gets or sets the next map.
        /// </summary>
        public Map NextMap { get; set; }

        /// <summary>
        ///     Gets or sets the next game mode.
        /// </summary>
        public GameMode NextGameMode { get; set; }

        /// <summary>
        ///     Gets the selected element.
        /// </summary>
        public RotationElement SelectedElement { get; private set; }

        /// <summary>
        ///     Gets or sets the map rotation.
        /// </summary>
        public MapRotation MapRotation
        {
            get { return _mapRotation; }
            set
            {
                if (_mapRotation == value) return;
                if (_mapRotation != null)
                {
                    _mapRotation.ElementAdded -= _mapRotation_ElementAdded;
                    _mapRotation.ElementRemoved -= _mapRotation_ElementRemoved;
                    _mapRotation.ElementUpdated -= _mapRotation_ElementUpdated;
                    _mapRotation.ElementsSwapped -= _mapRotation_ElementsSwapped;
                }
                if (value != null)
                {
                    value.ElementAdded += _mapRotation_ElementAdded;
                    value.ElementRemoved += _mapRotation_ElementRemoved;
                    value.ElementUpdated += _mapRotation_ElementUpdated;
                    value.ElementsSwapped += _mapRotation_ElementsSwapped;
                }

                CleanUp();

                _mapRotation = value;

                ShowInformation();
            }
        }

        /// <summary>
        ///     Occurs when an element is selected.
        /// </summary>
        public event EventHandler<EventArgs> SelectedElementChanged;

        /// <summary>
        ///     Raises the <see cref="SelectedElementChanged" /> event.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnSelectedElementChanged(EventArgs args)
        {
            if (SelectedElementChanged != null)
                SelectedElementChanged(this, args);
        }

        /// <summary>
        ///     Cleans up.
        /// </summary>
        private void CleanUp()
        {
            if (MapRotation == null) return;

            rotationListView.Items.Clear();

            UpdateListViewItemCount();
        }

        /// <summary>
        ///     Shows the information.
        /// </summary>
        private void ShowInformation()
        {
            if (MapRotation == null) return;

            foreach (RotationElement element in MapRotation)
                AddElementToListView(element);

            UpdateListViewItemCount();
        }

        private void _mapRotation_ElementsSwapped(object sender, RotationElementsSwappedEventArgs e)
        {
            ListViewItem left = GetItemOfElement(e.LeftElement);
            ListViewItem right = GetItemOfElement(e.RightElement);

            left.SubItems[0].Text = e.RightElement.GameMode.ToString();
            left.SubItems[1].Text = e.RightElement.Map.ToString();
            left.Tag = e.RightElement;

            right.SubItems[0].Text = e.LeftElement.GameMode.ToString();
            right.SubItems[1].Text = e.LeftElement.Map.ToString();
            right.Tag = e.LeftElement;

            bool tmp = left.Selected;
            left.Selected = right.Selected;
            right.Selected = tmp;
        }

        private void _mapRotation_ElementUpdated(object sender, RotationElementEventArgs e)
        {
            ListViewItem item = GetItemOfElement(e.Element);
            item.SubItems[0].Text = e.Element.GameMode.ToString();
            item.SubItems[1].Text = e.Element.Map.ToString();
        }

        private void _mapRotation_ElementRemoved(object sender, RotationElementEventArgs e)
        {
            rotationListView.Items.Remove(GetItemOfElement(e.Element));
            UpdateListViewItemCount();
        }

        private void _mapRotation_ElementAdded(object sender, RotationElementEventArgs e)
        {
            SelectElementInListView(AddElementToListView(e.Element, e.Index));
            UpdateListViewItemCount();
        }

        #region User interaction events

        private void addButton_Click(object sender, EventArgs e)
        {
            if (MapRotation == null) return;

            if (NextGameMode == null || NextMap == null)
            {
                MessageBox.Show(this, "No available maps or game modes", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            MapRotation.Add(new RotationElement(NextGameMode, NextMap));
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MapRotation == null) return;

            RotationElement element = SelectedElement;
            if (element == null) return;

            MapRotation.Remove(element);
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            if (MapRotation == null) return;

            RotationElement element = SelectedElement;
            if (element == null) return;

            MapRotation.MoveDown(SelectedElement);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            if (MapRotation == null) return;

            RotationElement element = SelectedElement;
            if (element == null) return;

            MapRotation.MoveUp(SelectedElement);
        }

        #endregion

        #region rotationListView events

        private void rotationListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            RotationElement newElement = rotationListView.SelectedItems.Count == 0
                ? null
                : rotationListView.SelectedItems[0].Tag as RotationElement;

            if (SelectedElement == newElement) return;

            SelectedElement = newElement;

            OnSelectedElementChanged(EventArgs.Empty);
        }

        #endregion

        #region rotationListView interaction methods

        /// <summary>
        ///     Gets the item of element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        private ListViewItem GetItemOfElement(RotationElement element)
        {
            return rotationListView.Items.OfType<ListViewItem>().FirstOrDefault(i => i.Tag == element);
        }

        /// <summary>
        ///     Updates the ListView item count.
        /// </summary>
        private void UpdateListViewItemCount()
        {
            rotationGroupBox.Text = string.Format("Rotation({0} maps)", rotationListView.Items.Count);
        }

        /// <summary>
        ///     Selects the element in ListView.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentNullException">item</exception>
        private void SelectElementInListView(ListViewItem item)
        {
            if (item == null) throw new ArgumentNullException("item");

            rotationListView.SelectedItems.Clear();
            item.Selected = true;

            int index = rotationListView.Items.IndexOf(item);
            if (index >= 0) rotationListView.EnsureVisible(index);
            rotationListView.Select();
        }

        /// <summary>
        ///     Adds the <paramref name="element" /> to ListView.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The added item.</returns>
        /// <exception cref="ArgumentNullException">element</exception>
        private void AddElementToListView(RotationElement element)
        {
            if (element == null) throw new ArgumentNullException("element");

            var item = new ListViewItem(element.GameMode.Name) {Tag = element};
            item.SubItems.Add(element.Map.Name);
            rotationListView.Items.Add(item);
        }

        /// <summary>
        ///     Adds the <paramref name="element" /> to ListView at the specified <paramref name="index" />.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="index">The index.</param>
        /// <returns>The added item.</returns>
        /// <exception cref="ArgumentNullException">element</exception>
        private ListViewItem AddElementToListView(RotationElement element, int index)
        {
            if (element == null) throw new ArgumentNullException("element");

            var item = new ListViewItem(element.GameMode.Name) {Tag = element};
            item.SubItems.Add(element.Map.Name);
            rotationListView.Items.Insert(index, item);

            return item;
        }

        #endregion
    }
}