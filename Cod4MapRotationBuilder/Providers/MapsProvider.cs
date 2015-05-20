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
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Windows.Forms;
using Cod4MapRotationBuilder.Collections;
using Cod4MapRotationBuilder.Data;
using Cod4MapRotationBuilder.Properties;

namespace Cod4MapRotationBuilder.Providers
{
    /// <summary>
    ///     Represents a <see cref="Map" /> provider.
    /// </summary>
    public class MapsProvider
    {
        /// <summary>
        ///     The user maps folder name.
        /// </summary>
        private const string UserMapsFolderName = "usermaps";

        /// <summary>
        ///     The call of duty 4 executable name.
        /// </summary>
        private const string CallOfDuty4ExecutableName = "iw3mp.exe";

        private readonly MapCollection _collection = new MapCollection();

        /// <summary>
        ///     Gets the collection of maps.
        /// </summary>
        public MapCollection Collection
        {
            get { return _collection; }
        }

        private static string CallOfDuty4Path
        {
            get { return Settings.Default.Path; }
            set
            {
                Settings.Default.Path = value;
                Settings.Default.Save();
            }
        }

        public static string CallOfDuty4UserMapsPath
        {
            get { return Path.Combine(CallOfDuty4Path, UserMapsFolderName); }
        }

        public static string GetPathForUserMap(string map)
        {
            return Path.Combine(CallOfDuty4UserMapsPath, map);
        }

        /// <summary>
        ///     Gets the stock maps.
        /// </summary>
        /// <returns>All stock maps.</returns>
        private IEnumerable<Map> GetStockMaps()
        {
            return Settings.Default.StockMaps.Split(';').Select(map => new Map(map, null));
        }

        /// <summary>
        ///     Gets the user maps.
        /// </summary>
        /// <returns>All user maps.</returns>
        private IEnumerable<Map> GetUserMaps()
        {
            string userMapsDirectory = Path.Combine(CallOfDuty4Path, UserMapsFolderName);

            if (!Directory.Exists(userMapsDirectory))
                yield break;

            foreach (string dir in Directory.GetDirectories(userMapsDirectory).Select(Path.GetFileName))
                yield return new Map(dir, Path.Combine(userMapsDirectory, dir));
        }

        /// <summary>
        ///     Refreshes the collection.
        /// </summary>
        public void Refresh()
        {
            Map[] detetedMaps = GetUserMaps().ToArray();
            Map[] stockMaps = GetStockMaps().ToArray();

            IEnumerable<Map> oldMaps =
                Collection.Where(m => detetedMaps.All(n => n.Name != m.Name) && stockMaps.All(n => n.Name != m.Name));
            IEnumerable<Map> newUserMaps = detetedMaps.Where(m => Collection.All(n => n.Name != m.Name));
            IEnumerable<Map> newStockMaps = stockMaps.Where(m => Collection.All(n => n.Name != m.Name));

            foreach (Map map in oldMaps.ToArray())
            {
                Collection.Remove(map);
                map.Dispose();
            }

            foreach (Map map in newUserMaps.ToArray())
                Collection.Add(map, MapType.UserMap);

            foreach (Map map in newStockMaps.ToArray())
                Collection.Add(map, MapType.Stock);
        }

        /// <summary>
        /// Determines whether the given <paramref name="path"/> to call of duty 4 is valid.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private bool IsPathCoD4PathValid(string path)
        {
            return File.Exists(Path.Combine(path, CallOfDuty4ExecutableName));
        }

        /// <summary>
        /// Asserts the path ok.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns></returns>
        public bool AssertPathOk(IWin32Window owner)
        {
            if (IsPathCoD4PathValid(CallOfDuty4Path)) return true;

            var dialog = new FolderBrowserDialog {Description = "Select your Call of Duty 4: Modern Warfare folder:"};
            if (dialog.ShowDialog(owner) != DialogResult.OK) return false;

            CallOfDuty4Path = dialog.SelectedPath;

            if (!IsPathCoD4PathValid(CallOfDuty4Path))
            {
                return MessageBox.Show(owner, "The given path is not a valid Call of Duty 4: Modern Warfare folder\nTry again?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) !=
                       DialogResult.No && AssertPathOk(owner);
            }

            return true;
        }
    }
}