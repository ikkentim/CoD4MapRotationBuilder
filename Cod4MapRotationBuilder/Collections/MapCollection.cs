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
    ///     Represents a collection of maps.
    /// </summary>
    public class MapCollection : IEnumerable<Map>
    {
        private readonly List<Map> _maps = new List<Map>();
        private readonly List<Map> _stocks = new List<Map>();
        private readonly List<Map> _unknown = new List<Map>();

        /// <summary>
        ///     Gets the <see cref="Map" /> with the specified name.
        /// </summary>
        public Map this[string name]
        {
            get
            {
                Map map = this.FirstOrDefault(m => m.Name == name);

                if (map == null)
                {
                    map = new Map(name, null);
                    _unknown.Add(map);
                }

                return map;
            }
        }

        /// <summary>
        ///     Occurs when a map is added.
        /// </summary>
        public event EventHandler<MapEventArgs> MapAdded;

        /// <summary>
        ///     Occurs when a map is removed.
        /// </summary>
        public event EventHandler<MapEventArgs> MapRemoved;

        /// <summary>
        ///     Raises the <see cref="MapAdded" /> event.
        /// </summary>
        /// <param name="args">The <see cref="MapEventArgs" /> instance containing the event data.</param>
        protected virtual void OnMapAdded(MapEventArgs args)
        {
            if (MapAdded != null)
                MapAdded(this, args);
        }

        /// <summary>
        ///     Raises the <see cref="MapRemoved" /> event.
        /// </summary>
        /// <param name="args">The <see cref="MapEventArgs" /> instance containing the event data.</param>
        protected virtual void OnMapRemoved(MapEventArgs args)
        {
            if (MapRemoved != null)
                MapRemoved(this, args);
        }

        /// <summary>
        ///     Adds the specified map.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <param name="type">The type.</param>
        /// <exception cref="ArgumentException">Invalid type</exception>
        public void Add(Map map, MapType type)
        {
            switch (type)
            {
                case MapType.UserMap:
                    _maps.Add(map);
                    break;
                case MapType.Stock:
                    _stocks.Add(map);
                    break;
                default:
                    throw new ArgumentException("Invalid type");
            }

            OnMapAdded(new MapEventArgs(map));
        }

        /// <summary>
        ///     Removes the specified map.
        /// </summary>
        /// <param name="map">The map.</param>
        public void Remove(Map map)
        {
            _maps.Remove(map);
            _stocks.Remove(map);
            _unknown.Remove(map);

            OnMapRemoved(new MapEventArgs(map));
        }

        /// <summary>
        ///     Cleans up unknown maps that are not not used in the specified map rotation.
        /// </summary>
        /// <param name="rotation">The rotation.</param>
        public void CleanUp(MapRotation rotation)
        {
            if (rotation == null) _unknown.Clear();
            _unknown.RemoveAll(m => rotation.All(e => e.Map != m));
        }

        #region Implementation of IEnumerable

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Map> GetEnumerator()
        {
            return _maps.Concat(_stocks).GetEnumerator(); //.Concat(_unknown)
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