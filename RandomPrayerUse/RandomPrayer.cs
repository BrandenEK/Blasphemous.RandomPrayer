using ModdingAPI;
using UnityEngine;

namespace RandomPrayerUse
{
    public class RandomPrayer : Mod
    {
        public RandomPrayer(string modId, string modName, string modVersion) : base(modId, modName, modVersion) { }
        public const float FervourCost = 35f;

        public bool UseRandomPrayer { get; set; }
        public System.Random rng { get; private set; }

        protected override void Initialize()
        {
            LogError("Initialization for random prayer use");
            if (!FileUtil.loadDataImages("menuSlots.png", 16, 16, 32, 0, true, out Sprite[] menuSlotImages))
                LogError("Failed to load slot images!");
            if (!FileUtil.loadDataImages("gameSlot.png", 18, 18, 32, 0, true, out Sprite[]  gameSlotImages))
                LogError("Failed to load slot images!");
            if (!FileUtil.loadDataImages("selectSlot.png", 94, 110, 32, 0, true, out Sprite[]  selectSlotImages))
                LogError("Failed to load slot images!");

            if (menuSlotImages != null && gameSlotImages != null && selectSlotImages != null)
            {
                RegisterPenitence(new PenitenceRandomPrayer(menuSlotImages[0], menuSlotImages[1], menuSlotImages[2], gameSlotImages[0], selectSlotImages[0]));
            }

            rng = new System.Random();
        }

        private Sprite m_FrameImage;
        public Sprite FrameImage
        {
            get { return m_FrameImage; }
            set
            {
                if (m_FrameImage == null)
                    m_FrameImage = value;
            }
        }

        private Sprite m_BackImage;
        public Sprite BackImage
        {
            get { return m_BackImage; }
            set
            {
                if (m_BackImage == null)
                    m_BackImage = value;
            }
        }
    }
}
