using ModdingAPI.Items;
using UnityEngine;

namespace RandomPrayerUse
{
    public class BeadRandomPrayer : ModRosaryBead
    {
        protected override string Id => "RB401";

        protected override string Name => Main.RandomPrayer.Localize("itmnam");

        protected override string Description => Main.RandomPrayer.Localize("itmdes");

        protected override string Lore => Main.RandomPrayer.Localize("itmlor");

        protected override bool CarryOnStart => false;

        protected override bool PreserveInNGPlus => true;

        protected override bool AddToPercentCompletion => false;

        protected override bool AddInventorySlot => true;

        protected override void LoadImages(out Sprite picture)
        {
            picture = Main.RandomPrayer.FileUtil.loadDataImages("reliquary.png", 32, 32, 32, 0, true, out Sprite[] images) ? images[0] : null;
        }
    }

    public class RandomPrayerBeadEffect : ModItemEffectOnEquip
    {
        protected override void ApplyEffect()
        {
            Main.RandomPrayer.UseRandomPrayer = true; // These need to check if the penitence is active first
        }

        protected override void RemoveEffect()
        {
            Main.RandomPrayer.UseRandomPrayer = false;
        }
    }
}
