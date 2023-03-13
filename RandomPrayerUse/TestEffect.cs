using Framework.Inventory;
using ModdingAPI;

namespace RandomPrayerUse
{
    public class TestEffect : ModItemEffectEquip
    {
        protected override void ApplyEffect()
        {
            Main.RandomPrayer.LogWarning("ApplyEffect()");
        }

        protected override void RemoveEffect()
        {
            Main.RandomPrayer.LogWarning("RemoveEffect()");
        }
    }

    public class DebugEffect : ModItemEffectEquip
    {
        protected override void ApplyEffect()
        {
            Main.RandomPrayer.LogWarning("ApplyEffect()");
        }

        protected override void RemoveEffect()
        {
            Main.RandomPrayer.LogWarning("RemoveEffect()");
        }

        protected override void Awake()
        {
            Main.RandomPrayer.LogWarning("Awake()");
        }

        protected override void Start()
        {
            Main.RandomPrayer.LogWarning("Start()");
        }

        protected override void Update()
        {
            Main.RandomPrayer.LogWarning("Update()");
        }

        protected override void Dispose()
        {
            Main.RandomPrayer.LogWarning("Dispose()");
        }
    }
}
