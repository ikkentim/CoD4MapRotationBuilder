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
using System.Windows.Forms;
using Cod4MapRotationBuilder.Data;

namespace Cod4MapRotationBuilder.UI
{
    /// <summary>
    ///     RotationElement preview control.
    /// </summary>
    public partial class RotationElementPreview : UserControl
    {
        private RotationElement _element;
        private string _mapNameCache;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RotationElementPreview" /> class.
        /// </summary>
        public RotationElementPreview()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Gets or sets the element.
        /// </summary>
        public RotationElement Element
        {
            get { return _element; }
            set
            {
                if (_element == value) return;
                if (_element != null) _element.Updated -= _element_Updated;
                if (value != null) value.Updated += _element_Updated;
                CleanUp();
                _element = value;

                ShowInformation();
            }
        }

        /// <summary>
        ///     Cleans up.
        /// </summary>
        private void CleanUp()
        {
            if (Element == null) return;

            mapNameLabel.Text = string.Empty;
            gameModeNameLabel.Text = string.Empty;
            _mapNameCache = null;

            if (loadscreenPictureBox.Image != null)
            {
                loadscreenPictureBox.Image.Dispose();
                loadscreenPictureBox.Image = null;
            }

            if (compassPictureBox.Image != null)
            {
                compassPictureBox.Image.Dispose();
                compassPictureBox.Image = null;
            }
        }

        /// <summary>
        ///     Shows the information.
        /// </summary>
        private void ShowInformation()
        {
            if (Element == null) return;

            mapNameLabel.Text = Element.Map.ToString();
            gameModeNameLabel.Text = Element.GameMode.ToString();

            if (_mapNameCache != Element.Map.Name)
            {
                if (loadscreenPictureBox.Image != null)
                {
                    loadscreenPictureBox.Image.Dispose();
                    loadscreenPictureBox.Image = null;
                }

                if (compassPictureBox.Image != null)
                {
                    compassPictureBox.Image.Dispose();
                    compassPictureBox.Image = null;
                }

                loadscreenPictureBox.Image = Element.Map.LoadscreenImage;
                compassPictureBox.Image = Element.Map.CompassImage;
            }
            _mapNameCache = Element.Map.Name;
        }

        private void _element_Updated(object sender, EventArgs e)
        {
            ShowInformation();
        }
    }
}