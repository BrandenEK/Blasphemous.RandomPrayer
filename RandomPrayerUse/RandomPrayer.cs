using ModdingAPI;
using UnityEngine;
using Framework.Managers;
using Framework.Inventory;
using System.Collections.ObjectModel;

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
                LogWarning("Setting random prayer to " + value);
                m_UseRandomPrayer = value;

                if (value)
                    RandomizeNextPrayer();
                else
                    Core.InventoryManager.SetPrayerInSlot(0, (Prayer)null);
            }
        }

        public bool DecreasedFervourCost { get; set; }
        public Prayer NextPrayer { get; private set; }

        protected override void Initialize()
        {
            RegisterPenitence(new PenitenceRandomPrayer());
            RegisterItem(new BeadRandomPrayer().AddEffect<RandomPrayerBeadEffect>());
        }

        protected override void Update()
        {
            Prayer current = Core.InventoryManager.GetPrayerInSlot(0);
            LogWarning(current == null ? "None" : current.id);
        }

        public void RandomizeNextPrayer()
        {
            ReadOnlyCollection<Prayer> allPrayers = Core.InventoryManager.GetAllPrayers();
            int index = Random.RandomRangeInt(0, allPrayers.Count);
            NextPrayer = allPrayers[index];
            Core.InventoryManager.SetPrayerInSlot(0, NextPrayer); // Cant do this if prayer is currently active
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
