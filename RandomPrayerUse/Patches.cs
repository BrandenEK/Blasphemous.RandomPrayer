using HarmonyLib;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Framework.Managers;
using Framework.Inventory;
using Gameplay.UI.Others.MenuLogic;
using Gameplay.GameControllers.Penitent.Abilities;

namespace RandomPrayerUse
{
    // Dont allow equipping prayers
    [HarmonyPatch(typeof(InventoryManager), "SetPrayerInSlot", typeof(int), typeof(Prayer))]
    public class InventoryManager_Patch
    {
        public static bool Prefix()
        {
            return !Main.RandomPrayer.UseRandomPrayer;
        }
    }

    // Always return same fervour cost
    //[HarmonyPatch(typeof(InventoryManager), "")]

    // Load images for prayer background
    [HarmonyPatch(typeof(NewInventory_GridItem), "Awake")]
    public class InvGridItem_Patch
    {
        public static void Postfix(Sprite ___frameSelected, Sprite ___backEquipped)
        {
            Main.RandomPrayer.FrameImage = ___frameSelected;
            Main.RandomPrayer.BackImage = ___backEquipped;
        }
    }

    // Get random prayer instead of equipped one
    [HarmonyPatch(typeof(PrayerUse), "GetEquippedPrayer")]
    public class PrayerUseGet_Patch
    {
        public static bool Prefix(ref Prayer __result)
        {
            ReadOnlyCollection<Prayer> allPrayers = Core.InventoryManager.GetAllPrayers();
            int index = Random.RandomRangeInt(0, allPrayers.Count);
            __result = allPrayers[index];
            return false;
        }
    }

    // On cast start - set box image
}
