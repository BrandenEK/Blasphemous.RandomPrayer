using ModdingAPI;
using Framework.Managers;

namespace RandomPrayerUse
{
    public class PenitenceRandomPrayer : ModPenitence
    {
        protected override string Id => "PE_RANDOM_PRAYER";

        protected override string Name => Main.RandomPrayer.Localize("pname");

        protected override string Description => Main.RandomPrayer.Localize("pdesc1") + "\n" + Main.RandomPrayer.Localize("pdesc2");

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
