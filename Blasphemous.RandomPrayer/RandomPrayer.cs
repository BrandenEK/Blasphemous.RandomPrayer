using Blasphemous.ModdingAPI;
using Blasphemous.ModdingAPI.Items;
using Blasphemous.ModdingAPI.Penitence;
using Framework.Inventory;
using Framework.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Blasphemous.RandomPrayer;

public class RandomPrayer : BlasMod
{
    public RandomPrayer() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    private const int NORMAL_FERVOUR_COST = 35;
    private const int REDUCED_FERVOUR_COST = 25;

    private bool m_UseRandomPrayer;
    public bool UseRandomPrayer
    {
        get => m_UseRandomPrayer;
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

    private Dictionary<string, int> prayerCosts;

    private Sprite m_FrameImage;
    public Sprite FrameImage
    {
        get => m_FrameImage;
        set
        {
            if (m_FrameImage == null)
                m_FrameImage = value;
        }
    }

    private Sprite m_BackImage;
    public Sprite BackImage
    {
        get => m_BackImage;
        set
        {
            if (m_BackImage == null)
                m_BackImage = value;
        }
    }

    protected override void OnInitialize()
    {
        LocalizationHandler.RegisterDefaultLanguage("en");
        Config = ConfigHandler.Load<PrayerConfig>();
    }

    protected override void OnRegisterServices(ModServiceProvider provider)
    {
        provider.RegisterPenitence(new PrayerPenitence());
        provider.RegisterItem(new PrayerBead().AddEffect(new PrayerBeadEffect()));
    }

    protected override void OnLevelLoaded(string oldLevel, string newLevel)
    {
        prayerCosts ??= Core.InventoryManager.GetAllPrayers().ToDictionary(prayer => prayer.id, prayer => prayer.fervourNeeded);

        if (newLevel == "MainMenu")
        {
            UseRandomPrayer = false;
            DecreasedFervourCost = false;
        }
        else if (UseRandomPrayer)
        {
            RandomizeNextPrayer();
        }
    }

    protected override void OnUpdate()
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
        List<Prayer> possiblePrayers = new(Config.OnlyShuffleOwnedPrayers
            ? Core.InventoryManager.GetPrayersOwned()
            : Core.InventoryManager.GetAllPrayers());

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
        Prayer prayer = possiblePrayers.Count == 0 ? null : possiblePrayers[new System.Random().Next(0, possiblePrayers.Count)];
        Core.InventoryManager.SetPrayerInSlot(0, prayer);
    }
}
