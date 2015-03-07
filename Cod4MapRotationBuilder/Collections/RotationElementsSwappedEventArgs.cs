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
    ///     Provides data for the <see cref="MapRotation.ElementsSwapped" /> event.
    /// </summary>
    public class RotationElementsSwappedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RotationElementsSwappedEventArgs" /> class.
        /// </summary>
        /// <param name="leftElement">Element on the left.</param>
        /// <param name="rightElement">Element on the right.</param>
        public RotationElementsSwappedEventArgs(RotationElement leftElement, RotationElement rightElement)
        {
            LeftElement = leftElement;
            RightElement = rightElement;
        }

        /// <summary>
        ///     Gets the element on the left.
        /// </summary>
        public RotationElement LeftElement { get; private set; }

        /// <summary>
        ///     Gets the element on the right.
        /// </summary>
        public RotationElement RightElement { get; private set; }
    }
}