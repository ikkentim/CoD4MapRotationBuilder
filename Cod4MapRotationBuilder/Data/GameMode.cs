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
    ///     Represents a Call of Duty game mode.
    /// </summary>
    public class GameMode
    {
        private string _name;
        private string _shortName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameMode" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="shortName">The short name.</param>
        public GameMode(string name, string shortName)
        {
            Name = name;
            ShortName = shortName;
        }

        /// <summary>
        ///     Gets or sets the short name.
        /// </summary>
        public virtual string ShortName
        {
            get { return _shortName; }
            set
            {
                _shortName = value;
                OnUpdated(EventArgs.Empty);
            }
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
            return string.IsNullOrEmpty(Name) ? ShortName : Name;
        }

        #endregion
    }
}