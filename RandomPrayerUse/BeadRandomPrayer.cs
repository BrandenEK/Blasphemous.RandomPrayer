using ModdingAPI.Items;
using UnityEngine;

namespace RandomPrayerUse
{
    public class BeadRandomPrayer : ModRosaryBead
    {
        protected override string Id => "RB401";

        protected override string Name => "Reliquary of the Mistaken Heart";

        protected override string Description => "Description for the random prayer bead";

        protected override string Lore => "Lore for the random prayer bead";

        protected override bool CarryOnStart => false;

        protected override bool PreserveInNGPlus => true;

        protected override bool AddToPercentCompletion => false;

        protected override bool AddInventorySlot => true;

        protected override void LoadImages(out Sprite picture)
        {
            picture = null;
        }
    }

    public class RandomPrayerBeadEffect : ModItemEffectOnEquip
    {
        protected override void ApplyEffect()
        {
            // Turn on random prayer
        }

        protected override void RemoveEffect()
        {
            // Turn off random prayer
        }
    }
}
