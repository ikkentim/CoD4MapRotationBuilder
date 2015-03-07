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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cod4MapRotationBuilder.Data;

namespace Cod4MapRotationBuilder.Collections
{
    /// <summary>
    ///     Represents a collection of game modes.
    /// </summary>
    public class GameModeCollection : IEnumerable<GameMode>
    {
        private readonly List<GameMode> _gameModes = new List<GameMode>();

        /// <summary>
        ///     Gets the <see cref="GameMode" /> with the specified name.
        /// </summary>
        public GameMode this[string name]
        {
            get { return this.FirstOrDefault(m => m.ShortName == name); }
        }

        /// <summary>
        ///     Occurs when a game mode is added.
        /// </summary>
        public event EventHandler<GameModeEventArgs> GameModeAdded;

        /// <summary>
        ///     Occurs when a game mode is removed.
        /// </summary>
        public event EventHandler<GameModeEventArgs> GameModeRemoved;

        /// <summary>
        ///     Raises the <see cref="GameModeAdded" /> event.
        /// </summary>
        /// <param name="args">The <see cref="GameModeEventArgs" /> instance containing the event data.</param>
        protected virtual void OnGameModeAdded(GameModeEventArgs args)
        {
            if (GameModeAdded != null)
                GameModeAdded(this, args);
        }

        /// <summary>
        ///     Raises the <see cref="GameModeRemoved" /> event.
        /// </summary>
        /// <param name="args">The <see cref="GameModeEventArgs" /> instance containing the event data.</param>
        protected virtual void OnGameModeRemoved(GameModeEventArgs args)
        {
            if (GameModeRemoved != null)
                GameModeRemoved(this, args);
        }

        /// <summary>
        ///     Adds the specified game mode.
        /// </summary>
        /// <param name="gameMode">The game mode.</param>
        public void Add(GameMode gameMode)
        {
            _gameModes.Add(gameMode);

            OnGameModeAdded(new GameModeEventArgs(gameMode));
        }

        /// <summary>
        ///     Removes the specified game mode.
        /// </summary>
        /// <param name="gameMode">The game mode.</param>
        public void Remove(GameMode gameMode)
        {
            _gameModes.Remove(gameMode);

            OnGameModeRemoved(new GameModeEventArgs(gameMode));
        }

        #region Implementation of IEnumerable

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<GameMode> GetEnumerator()
        {
            return _gameModes.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}