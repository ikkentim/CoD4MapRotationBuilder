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
    ///     Represents a map rotation.
    /// </summary>
    public class MapRotation : IEnumerable<RotationElement>
    {
        private readonly List<RotationElement> _items = new List<RotationElement>();

        /// <summary>
        ///     Occurs when an element is added.
        /// </summary>
        public event EventHandler<RotationElementEventArgs> ElementAdded;

        /// <summary>
        ///     Occurs when an element is removed.
        /// </summary>
        public event EventHandler<RotationElementEventArgs> ElementRemoved;

        /// <summary>
        ///     Occurs when an element is updated.
        /// </summary>
        public event EventHandler<RotationElementEventArgs> ElementUpdated;

        /// <summary>
        ///     Occurs when two elements are swapped.
        /// </summary>
        public event EventHandler<RotationElementsSwappedEventArgs> ElementsSwapped;

        /// <summary>
        ///     Raises the <see cref="ElementAdded" /> event.
        /// </summary>
        /// <param name="args">The <see cref="RotationElementEventArgs" /> instance containing the event data.</param>
        protected virtual void OnElementAdded(RotationElementEventArgs args)
        {
            if (ElementAdded != null)
                ElementAdded(this, args);
        }

        /// <summary>
        ///     Raises the <see cref="ElementRemoved" /> event.
        /// </summary>
        /// <param name="args">The <see cref="RotationElementEventArgs" /> instance containing the event data.</param>
        protected virtual void OnElementRemoved(RotationElementEventArgs args)
        {
            if (ElementRemoved != null)
                ElementRemoved(this, args);
        }

        /// <summary>
        ///     Raises the <see cref="ElementUpdated" /> event.
        /// </summary>
        /// <param name="args">The <see cref="RotationElementEventArgs" /> instance containing the event data.</param>
        protected virtual void OnElementUpdated(RotationElementEventArgs args)
        {
            if (ElementUpdated != null)
                ElementUpdated(this, args);
        }

        /// <summary>
        ///     Raises the <see cref="ElementsSwapped" /> event.
        /// </summary>
        /// <param name="args">The <see cref="RotationElementsSwappedEventArgs" /> instance containing the event data.</param>
        protected virtual void OnElementsSwapped(RotationElementsSwappedEventArgs args)
        {
            if (ElementsSwapped != null)
                ElementsSwapped(this, args);
        }

        /// <summary>
        ///     Adds the specified <paramref name="element" />.
        /// </summary>
        /// <param name="element">The element.</param>
        public void Add(RotationElement element)
        {
            _items.Add(element);

            element.Updated += element_Updated;
            OnElementAdded(new RotationElementEventArgs(element, _items.Count - 1));
        }

        private void element_Updated(object sender, EventArgs e)
        {
            var element = sender as RotationElement;
            if (element == null) return;

            OnElementUpdated(new RotationElementEventArgs(element, _items.IndexOf(element)));
        }

        /// <summary>
        ///     Adds the specified elements.
        /// </summary>
        /// <param name="elements">The elements.</param>
        /// <param name="maps">The maps.</param>
        /// <param name="gamemodes">The gamemodes.</param>
        /// <exception cref="Exception">
        ///     Invalid map name or Invalid file format or Invalid file format
        /// </exception>
        public void Add(string elements, IEnumerable<Map> maps, IEnumerable<GameMode> gamemodes)
        {
            bool mode = false;
            GameMode gm = gamemodes.First();
            bool map = false;
            foreach (string elementText in elements.Split(' '))
            {
                string element = elementText.Trim();

                if (mode)
                {
                    mode = false;
                    gm = gamemodes.FirstOrDefault(g => g.ShortName == element) ?? gm;
                    continue;
                }
                if (element == "gametype")
                {
                    mode = true;
                    continue;
                }

                if (map)
                {
                    if (!element.StartsWith("mp_")) throw new Exception("Invalid map name");
                    map = false;
                    Map m = maps.FirstOrDefault(z => z.Name == element) ?? new Map(element, null);
                    Add(new RotationElement(gm, m));

                    continue;
                }

                if (element != "map") throw new Exception("Invalid file format");

                map = true;
            }

            if (map || mode) throw new Exception("Invalid file format");
        }

        /// <summary>
        ///     Removes the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        public void Remove(RotationElement element)
        {
            int index = _items.IndexOf(element);
            _items.Remove(element);
            element.Updated -= element_Updated;
            OnElementRemoved(new RotationElementEventArgs(element, index));
        }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        public void Clear()
        {
            foreach (RotationElement element in this.ToArray())
                Remove(element);
        }

        /// <summary>
        ///     Moves the specified <paramref name="element" /> down.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        /// <exception cref="Exception">element not in collection</exception>
        public int MoveDown(RotationElement element)
        {
            if (!_items.Contains(element))
                throw new Exception("element not in collection");

            int index = _items.IndexOf(element);

            if (index == _items.Count - 1) return _items.Count - 1;

            _items.Remove(element);
            _items.Insert(index + 1, element);

            OnElementsSwapped(new RotationElementsSwappedEventArgs(element, _items.ElementAt(index)));

            return index + 1;
        }


        /// <summary>
        ///     Moves the specified <paramref name="element" /> up.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        /// <exception cref="Exception">element not in collection</exception>
        public int MoveUp(RotationElement element)
        {
            if (!_items.Contains(element))
                throw new Exception("element not in collection");

            int index = _items.IndexOf(element);

            if (index == 0) return 0;

            _items.Remove(element);
            _items.Insert(index - 1, element);

            OnElementsSwapped(new RotationElementsSwappedEventArgs(element, _items.ElementAt(index)));

            return index - 1;
        }

        /// <summary>
        ///     Gets the index the of the specified <paramref name="element" />;
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The index of the specified element</returns>
        public int IndexOf(RotationElement element)
        {
            return _items.IndexOf(element);
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
            return string.Join(" ", _items);
        }

        #endregion

        #region Implementation of IEnumerable

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<RotationElement> GetEnumerator()
        {
            return _items.GetEnumerator();
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