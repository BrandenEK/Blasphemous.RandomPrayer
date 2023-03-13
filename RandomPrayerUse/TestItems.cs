using ModdingAPI;
using UnityEngine;

namespace RandomPrayerUse
{
    public class TestBead : ModRosaryBead
    {
        protected override string Id => "RB_TEST";

        protected override string Name => "Test Rosary Bead";

        protected override string Description => "Test description";

        protected override string Lore => "Test lore";

        protected override bool CarryOnStart => true;

        protected override bool PreserveInNGPlus => true;

        protected override bool AddToPercentageCompletion => false;

        protected override void LoadImages(out Sprite picture)
        {
            picture = Main.RandomPrayer.FileUtil.loadDataImages("empty.png", 32, 32, 32, 0, true, out Sprite[] images) ? images[0] : null;
        }
    }

    public class TestPrayer : ModPrayer
    {
        protected override string Id => "PR_TEST";

        protected override string Name => "Test Prayer";

        protected override string Description => "Test description";

        protected override string Lore => "Test lore";

        protected override bool CarryOnStart => true;

        protected override bool PreserveInNGPlus => true;

        protected override bool AddToPercentageCompletion => false;

        protected override int FervourCost => 35;

        protected override void LoadImages(out Sprite picture)
        {
            picture = Main.RandomPrayer.FileUtil.loadDataImages("empty.png", 32, 32, 32, 0, true, out Sprite[] images) ? images[0] : null;
        }
    }

    public class TestSwordHeart : ModSwordHeart
    {
        protected override string Id => "HE_TEST";

        protected override string Name => "Test Sword Heart";

        protected override string Description => "Test description";

        protected override string Lore => "Test lore";

        protected override bool CarryOnStart => true;

        protected override bool PreserveInNGPlus => true;

        protected override bool AddToPercentageCompletion => false;

        protected override void LoadImages(out Sprite picture)
        {
            picture = Main.RandomPrayer.FileUtil.loadDataImages("empty.png", 32, 32, 32, 0, true, out Sprite[] images) ? images[0] : null;
        }
    }

    public class TestQuestItem : ModQuestItem
    {
        protected override string Id => "QI_TEST";

        protected override string Name => "Test Quest Item";

        protected override string Description => "Test description";

        protected override string Lore => "Test lore";

        protected override bool CarryOnStart => true;

        protected override bool PreserveInNGPlus => false;

        protected override bool AddToPercentageCompletion => false;

        protected override void LoadImages(out Sprite picture)
        {
            picture = Main.RandomPrayer.FileUtil.loadDataImages("empty.png", 32, 32, 32, 0, true, out Sprite[] images) ? images[0] : null;
        }
    }

    public class TestCollectible : ModCollectible
    {
        protected override string Id => "CO_TEST";

        protected override string Name => "Test Collectible";

        protected override string Description => "Test description";

        protected override string Lore => "Test lore";

        protected override bool CarryOnStart => true;

        protected override bool PreserveInNGPlus => false;

        protected override bool AddToPercentageCompletion => false;

        protected override void LoadImages(out Sprite picture)
        {
            picture = Main.RandomPrayer.FileUtil.loadDataImages("empty.png", 32, 32, 32, 0, true, out Sprite[] images) ? images[0] : null;
        }
    }
}
