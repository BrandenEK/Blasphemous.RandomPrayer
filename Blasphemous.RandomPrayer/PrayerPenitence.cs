using Blasphemous.Framework.Penitence;
using Framework.Managers;
using UnityEngine;

namespace Blasphemous.RandomPrayer;

internal class PrayerPenitence : ModPenitenceWithBead
{
    protected override string Id => "PE_RANDOM_PRAYER";

    protected override string Name => Main.RandomPrayer.LocalizationHandler.Localize("pname");

    protected override string Description => Main.RandomPrayer.LocalizationHandler.Localize("pdesc");

    protected override string BeadId => "RB401";

    protected override PenitenceImageCollection Images
    {
        get
        {
            Main.RandomPrayer.FileHandler.LoadDataAsVariableSpritesheet("penitence.png",
            [
                new Rect(0, 0, 94, 110),
                new Rect(95, 1, 92, 108),
                new Rect(190, 94, 16, 16),
                new Rect(190, 78, 16, 16),
                new Rect(190, 62, 16, 16),
                new Rect(188, 0, 18, 18)
            ], out Sprite[] images);

            return new PenitenceImageCollection()
            {
                ChooseSelected = images[0],
                ChooseUnselected = images[1],
                InProgress = images[2],
                Completed = images[3],
                Abandoned = images[4],
                Gameplay = images[5]
            };
        }
    }

    protected override void Activate()
    {
        Main.RandomPrayer.UseRandomPrayer = true;
    }

    protected override void Deactivate()
    {
        if (!Core.InventoryManager.IsRosaryBeadEquipped("RB401"))
            Main.RandomPrayer.UseRandomPrayer = false;
    }
}
