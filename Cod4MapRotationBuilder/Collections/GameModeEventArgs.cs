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
using Cod4MapRotationBuilder.Data;

namespace Cod4MapRotationBuilder.Collections
{
    /// <summary>
    ///     Provides data for the <see cref="GameModeCollection.GameModeAdded" /> or
    ///     <see cref="GameModeCollection.GameModeRemoved" /> event.
    /// </summary>
    public class GameModeEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MapEventArgs" /> class.
        /// </summary>
        /// <param name="gameMode">The gameMode.</param>
        public GameModeEventArgs(GameMode gameMode)
        {
            GameMode = gameMode;
        }

        /// <summary>
        ///     Gets the map.
        /// </summary>
        public GameMode GameMode { get; private set; }
    }
}