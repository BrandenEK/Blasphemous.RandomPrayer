using ModdingAPI.Penitences;
using Framework.Managers;
using UnityEngine;

namespace RandomPrayerUse
{
    public class PenitenceRandomPrayer : ModPenitence
    {
        protected override string Id => "PE_RANDOM_PRAYER";

        protected override string Name => Main.RandomPrayer.Localize("pname");

        protected override string Description => Main.RandomPrayer.Localize("pdesc");

        protected override string ItemIdToGive => "RB401";

        protected override InventoryManager.ItemType ItemTypeToGive => InventoryManager.ItemType.Bead;

        protected override void Activate()
        {
            Main.RandomPrayer.UseRandomPrayer = true;
        }

        protected override void Deactivate()
        {
            if (!Core.InventoryManager.IsRosaryBeadEquipped("RB401"))
                Main.RandomPrayer.UseRandomPrayer = false;
        }

        protected override void LoadImages(out Sprite inProgress, out Sprite completed, out Sprite abandoned, out Sprite gameplay, out Sprite chooseSelected, out Sprite chooseUnselected)
        {
            if (Main.RandomPrayer.FileUtil.loadDataImages("menuSlots.png", 16, 16, 32, 0, true, out Sprite[] menuSlotImages))
            {
                inProgress = menuSlotImages[0];
                completed = menuSlotImages[1];
                abandoned = menuSlotImages[2];
            }
            else
            {
                inProgress = null;
                completed = null;
                abandoned = null;
            }

            gameplay = Main.RandomPrayer.FileUtil.loadDataImages("gameSlot.png", 18, 18, 32, 0, true, out Sprite[] gameSlotImages) ? gameSlotImages[0] : null;
            chooseSelected = Main.RandomPrayer.FileUtil.loadDataImages("chooseSlotSelected.png", 94, 110, 32, 0, true, out Sprite[] chooseSelectedSlotImages) ? chooseSelectedSlotImages[0] : null;
            chooseUnselected = Main.RandomPrayer.FileUtil.loadDataImages("chooseSlotUnselected.png", 92, 108, 32, 0, true, out Sprite[] chooseUnselectedSlotImages) ? chooseUnselectedSlotImages[0] : null;
        }
    }
}
