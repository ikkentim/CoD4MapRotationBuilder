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
    ///     Provides data for the <see cref="MapRotation.ElementAdded" />, <see cref="MapRotation.ElementRemoved" /> or
    ///     <see cref="MapRotation.ElementUpdated" /> event.
    /// </summary>
    public class RotationElementEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RotationElementEventArgs" /> class.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="index">The index.</param>
        public RotationElementEventArgs(RotationElement element, int index)
        {
            Element = element;
            Index = index;
        }

        /// <summary>
        ///     Gets the element.
        /// </summary>
        public RotationElement Element { get; private set; }

        /// <summary>
        ///     Gets the index.
        /// </summary>
        public int Index { get; private set; }
    }
}