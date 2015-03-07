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

using System.Collections.Generic;
using System.Linq;
using Cod4MapRotationBuilder.Collections;
using Cod4MapRotationBuilder.Data;
using Cod4MapRotationBuilder.Properties;

namespace Cod4MapRotationBuilder.Providers
{
    /// <summary>
    ///     Represents a <see cref="GameMode" /> provider.
    /// </summary>
    public class GameModesProvider
    {
        private readonly GameModeCollection _collection = new GameModeCollection();

        /// <summary>
        ///     Gets the collection.
        /// </summary>
        /// <value>
        ///     The collection.
        /// </value>
        public GameModeCollection Collection
        {
            get { return _collection; }
        }

        /// <summary>
        ///     Gets the game modes.
        /// </summary>
        /// <returns>All game modes.</returns>
        private IEnumerable<GameMode> GetGameModes()
        {
            return from gameMode in Settings.Default.GameModes.Split(';')
                select gameMode.Split(':')
                into parts
                where parts.Length == 2
                select new GameMode(parts[1], parts[0]);
        }

        /// <summary>
        ///     Refreshes the collection.
        /// </summary>
        public void Refresh()
        {
            IEnumerable<GameMode> newCollection = GetGameModes();
            IEnumerable<GameMode> oldGameModes = Collection.Where(m => !newCollection.Contains(m));
            IEnumerable<GameMode> newGameModes = newCollection.Where(m => !Collection.Contains(m));

            foreach (GameMode mode in oldGameModes.ToArray())
                Collection.Remove(mode);
            foreach (GameMode mode in newGameModes.ToArray())
                Collection.Add(mode);
        }
    }
}