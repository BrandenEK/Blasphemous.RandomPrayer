using Blasphemous.ModdingAPI.Penitence;
using Framework.Managers;
using UnityEngine;

namespace Blasphemous.RandomPrayer;

public class PrayerPenitence : ModPenitence
{
    protected override string Id => "PE_RANDOM_PRAYER";

    protected override string Name => Main.RandomPrayer.LocalizationHandler.Localize("pname");

    protected override string Description => Main.RandomPrayer.LocalizationHandler.Localize("pdesc");

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
        bool loaded = Main.RandomPrayer.FileHandler.LoadDataAsFixedSpritesheet("menuSlots.png", new Vector2(16, 16), out Sprite[] menuSlotImages);

        inProgress = loaded ? menuSlotImages[0] : null;
        completed = loaded ? menuSlotImages[1] : null;
        abandoned = loaded ? menuSlotImages[2] : null;

        Main.RandomPrayer.FileHandler.LoadDataAsSprite("gameSlot.png", out gameplay);
        Main.RandomPrayer.FileHandler.LoadDataAsSprite("chooseSlotSelected.png", out chooseSelected);
        Main.RandomPrayer.FileHandler.LoadDataAsSprite("chooseSlotUnselected.png", out chooseUnselected);
    }
}
