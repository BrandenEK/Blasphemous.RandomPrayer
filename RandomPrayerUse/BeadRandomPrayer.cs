using ModdingAPI.Items;
using UnityEngine;
using Framework.Managers;
using Framework.Penitences;

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
            Main.RandomPrayer.DecreasedFervourCost = true;
            Main.RandomPrayer.UseRandomPrayer = true;
        }

        protected override void RemoveEffect()
        {
            Main.RandomPrayer.DecreasedFervourCost = false;
            IPenitence pen = Core.PenitenceManager.GetCurrentPenitence();
            if (pen == null || pen.Id != "PE_RANDOM_PRAYER")
                Main.RandomPrayer.UseRandomPrayer = false;
        }
    }
}
