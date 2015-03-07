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
using Cod4MapRotationBuilder.Providers;
using Cod4MapRotationBuilder.Tools;

namespace Cod4MapRotationBuilder.Data
{
    /// <summary>
    ///     Represents a Call of Duty map.
    /// </summary>
    public class Map : Disposable
    {
        private string _name;
        private string _path;
        private Image _thumbnail;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Map" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="path">The path.</param>
        public Map(string name, string path)
        {
            Name = name;
            Path = path;
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnUpdated(EventArgs.Empty);
            }
        }

        /// <summary>
        ///     Gets or sets the path.
        /// </summary>
        public virtual string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnUpdated(EventArgs.Empty);
            }
        }

        /// <summary>
        ///     Gets the loadscreen image.
        /// </summary>
        public virtual Image LoadscreenImage
        {
            get
            {
                AssertNotDisposed();
                return MapImageProvider.GetLoadscreenImage(this);
            }
        }

        /// <summary>
        ///     Gets the compass image.
        /// </summary>
        public virtual Image CompassImage
        {
            get
            {
                AssertNotDisposed();
                return MapImageProvider.GetCompassImage(this);
            }
        }

        /// <summary>
        ///     Gets the loadscreen thumbnail.
        /// </summary>
        public virtual Image Icon
        {
            get
            {
                AssertNotDisposed();
                return _thumbnail ?? (_thumbnail = MapImageProvider.GetLoadscreenThumbnail(this));
            }
        }

        /// <summary>
        ///     Occurs when updated.
        /// </summary>
        public event EventHandler Updated;

        /// <summary>
        ///     Raises the <see cref="Updated" /> event.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnUpdated(EventArgs args)
        {
            if (Updated != null)
                Updated(this, args);
        }

        #region Overrides of Object

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Overrides of Disposable

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (_thumbnail != null)
            {
                _thumbnail.Dispose();
                _thumbnail = null;
            }
        }

        #endregion
    }
}