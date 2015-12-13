using System.Collections.Generic;
using LoLLauncher.RiotObjects.Platform.Catalog.Champion;

namespace EloBuddyAutoQueuer
{
    internal class ItemRow
    {
        public ItemRow()
        {
        }

        public bool CheckBox { get; set; }
        public ChampionDTO[] ComboList { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
    }
}