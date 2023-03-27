using ModdingAPI;
using UnityEngine;
using UnityEngine.UI;
using Framework.Managers;
using Framework.Inventory;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RandomPrayerUse
{
    public class RandomPrayer : Mod
    {
        public RandomPrayer(string modId, string modName, string modVersion) : base(modId, modName, modVersion) { }
        private const int NORMAL_FERVOUR_COST = 35;
        private const int REDUCED_FERVOUR_COST = 25;

        private bool m_UseRandomPrayer;
        public bool UseRandomPrayer
        {
            get { return m_UseRandomPrayer; }
            set
            {
                LogWarning("Setting random prayer to " + value);
                m_UseRandomPrayer = value;

                if (value)
                {
                    foreach (Prayer prayer in Core.InventoryManager.GetAllPrayers())
                        prayer.fervourNeeded = DecreasedFervourCost ? REDUCED_FERVOUR_COST : NORMAL_FERVOUR_COST;
                    RandomizeNextPrayer();
                }
                else
                {
                    foreach (Prayer prayer in Core.InventoryManager.GetAllPrayers())
                        prayer.fervourNeeded = prayerCosts[prayer.id];
                    Core.InventoryManager.SetPrayerInSlot(0, (Prayer)null);
                }
            }
        }

        public bool DecreasedFervourCost { get; set; }
        //public Prayer NextPrayer { get; private set; }

        protected override void Initialize()
        {
            RegisterPenitence(new PenitenceRandomPrayer());
            RegisterItem(new BeadRandomPrayer().AddEffect<RandomPrayerBeadEffect>());
        }

        protected override void LevelLoaded(string oldLevel, string newLevel)
        {
            if (prayerCosts == null)
                StorePrayerCosts();
            if (newLevel != "MainMenu" && UseRandomPrayer)
                RandomizeNextPrayer();
        }

        protected override void Update()
        {
            if (PrayerImage != null && Core.Logic.Penitent != null)
            {
                PrayerImage.transform.parent.gameObject.SetActive(UseRandomPrayer && Core.Logic.Penitent.PrayerCast.IsUsingAbility);
            }
        }

        public void RandomizeNextPrayer()
        {
            ReadOnlyCollection<Prayer> allPrayers = Core.InventoryManager.GetAllPrayers();
            int index = Random.RandomRangeInt(0, allPrayers.Count);
            //NextPrayer = allPrayers[index];
            Core.InventoryManager.SetPrayerInSlot(0, allPrayers[index]); // Cant do this if prayer is currently active
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

        public Image PrayerImage { get; set; }

        private Dictionary<string, int> prayerCosts;
        private void StorePrayerCosts()
        {
            prayerCosts = new Dictionary<string, int>();
            foreach (Prayer prayer in Core.InventoryManager.GetAllPrayers())
                prayerCosts.Add(prayer.id, prayer.fervourNeeded);
        }
    }
}
