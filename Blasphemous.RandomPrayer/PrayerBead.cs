using Blasphemous.Framework.Items;
using Framework.Managers;
using Framework.Penitences;
using UnityEngine;

namespace Blasphemous.RandomPrayer;

internal class PrayerBead : ModRosaryBead
{
    protected override string Id => "RB401";

    protected override string Name => Main.RandomPrayer.LocalizationHandler.Localize("itmnam");

    protected override string Description => Main.RandomPrayer.LocalizationHandler.Localize("itmdes");

    protected override string Lore => Main.RandomPrayer.LocalizationHandler.Localize("itmlor");

    protected override Sprite Picture => Main.RandomPrayer.FileHandler.LoadDataAsSprite("reliquary.png", out Sprite image) ? image : null;

    protected override bool CarryOnStart => false;

    protected override bool PreserveInNGPlus => true;

    protected override bool AddToPercentCompletion => false;

    protected override bool AddInventorySlot => true;
}

internal class PrayerBeadEffect : ModItemEffectOnEquip
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
