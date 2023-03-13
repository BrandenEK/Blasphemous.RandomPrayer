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
    //[HarmonyPatch(typeof(InventoryManager), "SetPrayerInSlot", typeof(int), typeof(Prayer))]
    //public class InventoryManager_Patch
    //{
    //    public static bool Prefix()
    //    {
    //        return false;
    //    }
    //}

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
        public static bool Prefix(Prayer __result)
        {
            ReadOnlyCollection<Prayer> allPrayers = Core.InventoryManager.GetAllPrayers();
            int index = Main.RandomPrayer.rng.Next(allPrayers.Count);
            return allPrayers[index];
        }
    }

    // On cast start - set box image
}
