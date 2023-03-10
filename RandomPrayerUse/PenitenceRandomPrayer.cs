using ModdingAPI;
using UnityEngine;
using Framework.Managers;

namespace RandomPrayerUse
{
    public class PenitenceRandomPrayer : ModPenitence
    {
        public PenitenceRandomPrayer(Sprite inProgress, Sprite completed, Sprite abandoned, Sprite gameplay, Sprite chooseSelected, Sprite chooseUnselected) : base(inProgress, completed, abandoned, gameplay, chooseSelected, chooseUnselected) { }

        protected override string Id => "PE_RANDOM_PRAYER";

        protected override string Name => "Penitence of the Misheard Words";

        protected override string Description => "-Your pray is full of intention, but lacks precision.  The Miracle's whim can be cruel when understanding you.\n-Prayers can not be equipped, and when casting, a completly random one will be invoked.";

        protected override string ItemIdToGive => null;

        protected override InventoryManager.ItemType ItemTypeToGive => InventoryManager.ItemType.Bead;

        protected override void Activate()
        {
            Main.RandomPrayer.UseRandomPrayer = true;
        }

        protected override void Deactivate()
        {
            Main.RandomPrayer.UseRandomPrayer = false;
        }
    }
}
