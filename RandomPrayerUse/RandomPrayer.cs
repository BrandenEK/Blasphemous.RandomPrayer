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
            RegisterPenitence(new PenitenceRandomPrayer());
            RegisterItem(new BeadRandomPrayer().AddEffect<RandomPrayerBeadEffect>());

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
