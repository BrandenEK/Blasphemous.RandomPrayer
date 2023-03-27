using ModdingAPI;
using UnityEngine;
using Framework.Managers;
using Framework.Inventory;

namespace RandomPrayerUse
{
    public class RandomPrayer : Mod
    {
        public RandomPrayer(string modId, string modName, string modVersion) : base(modId, modName, modVersion) { }
        public const float FervourCost = 35f;

        private bool m_UseRandomPrayer;
        public bool UseRandomPrayer
        {
            get { return m_UseRandomPrayer; }
            set
            {
                m_UseRandomPrayer = value;
                Core.InventoryManager.SetPrayerInSlot(0, (Prayer)null);
                LogWarning("Setting random prayer to " + value);
            }
        }

        public bool DecreasedFervourCost { get; set; }

        protected override void Initialize()
        {
            RegisterPenitence(new PenitenceRandomPrayer());
            RegisterItem(new BeadRandomPrayer().AddEffect<RandomPrayerBeadEffect>());
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
