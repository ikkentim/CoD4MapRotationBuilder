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

namespace Cod4MapRotationBuilder.Data
{
    /// <summary>
    ///     Represents a map rotation element.
    /// </summary>
    public class RotationElement
    {
        private GameMode _gameMode;
        private Map _map;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RotationElement" /> class.
        /// </summary>
        /// <param name="gameMode">The game mode.</param>
        /// <param name="map">The map.</param>
        public RotationElement(GameMode gameMode, Map map)
        {
            GameMode = gameMode;
            Map = map;
        }

        /// <summary>
        ///     Gets or sets the map.
        /// </summary>
        public Map Map
        {
            get { return _map; }
            set
            {
                if (_map == value) return;
                if (_map != null) _map.Updated -= element_updated;
                if (value != null) value.Updated += element_updated;

                _map = value;
                OnUpdated(EventArgs.Empty);
            }
        }

        /// <summary>
        ///     Gets or sets the game mode.
        /// </summary>
        public GameMode GameMode
        {
            get { return _gameMode; }
            set
            {
                if (_gameMode == value) return;
                if (_gameMode != null) _gameMode.Updated -= element_updated;
                if (value != null) value.Updated += element_updated;

                _gameMode = value;
                OnUpdated(EventArgs.Empty);
            }
        }

        private void element_updated(object sender, EventArgs e)
        {
            OnUpdated(e);
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
            return string.Format("gametype {0} map {1}", GameMode.ShortName, Map);
        }

        #endregion
    }
}