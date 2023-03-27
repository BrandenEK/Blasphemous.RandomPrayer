using ModdingAPI;
using UnityEngine;
using UnityEngine.UI;
using Framework.Managers;
using Framework.Inventory;
using System.Collections.Generic;

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
        public bool DisplayPrayerBox { get; set; }
        public Image PrayerImage { get; set; }
        private PrayerConfig Config { get; set; }

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

        private Dictionary<string, int> prayerCosts;
        private void StorePrayerCosts()
        {
            prayerCosts = new Dictionary<string, int>();
            foreach (Prayer prayer in Core.InventoryManager.GetAllPrayers())
                prayerCosts.Add(prayer.id, prayer.fervourNeeded);
        }

        protected override void Initialize()
        {
            RegisterPenitence(new PenitenceRandomPrayer());
            RegisterItem(new BeadRandomPrayer().AddEffect<RandomPrayerBeadEffect>());
            DisableFileLogging = true;

            Config = FileUtil.loadConfig<PrayerConfig>();
        }

        protected override void LevelLoaded(string oldLevel, string newLevel)
        {
            if (prayerCosts == null)
                StorePrayerCosts();
            if (newLevel == "MainMenu")
            {
                UseRandomPrayer = false;
                DecreasedFervourCost = false;
            }
            else if (UseRandomPrayer)
                RandomizeNextPrayer();
        }

        protected override void Update()
        {
            if (PrayerImage != null && Core.Logic.Penitent != null)
            {
                bool usingPrayer = Core.Logic.Penitent.PrayerCast.IsUsingAbility;
                PrayerImage.transform.parent.gameObject.SetActive(usingPrayer && DisplayPrayerBox);
                if (DisplayPrayerBox && !usingPrayer)
                    DisplayPrayerBox = false;
            }
        }

        public void RandomizeNextPrayer()
        {
            // If currently using a prayer, dont set new one
            if (Core.Logic.Penitent.PrayerCast.IsUsingAbility)
                return;

            // Get list of possible prayers based on config
            List<Prayer> possiblePrayers = new List<Prayer>();
            if (Config.OnlyShuffleOwnedPrayers)
                possiblePrayers.AddRange(Core.InventoryManager.GetPrayersOwned());
            else
                possiblePrayers.AddRange(Core.InventoryManager.GetAllPrayers());
            if (Config.RemoveMirabras)
            {
                for (int i = 0; i < possiblePrayers.Count; i++)
                {
                    if (possiblePrayers[i].id == "PR202")
                    {
                        possiblePrayers.RemoveAt(i);
                        i--;
                    }
                }
            }
            
            //LogWarning("Getting random prayer from " + possiblePrayers.Count + " options");
            Prayer prayer = possiblePrayers.Count == 0 ? null : possiblePrayers[Random.RandomRangeInt(0, possiblePrayers.Count)];
            Core.InventoryManager.SetPrayerInSlot(0, prayer);
        }
    }
}
