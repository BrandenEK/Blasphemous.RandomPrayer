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
    // Dont allow equipping or unequipping prayers
    [HarmonyPatch(typeof(NewInventory_LayoutGrid), "EquipObject")]
    public class InventoryEquip_Patch
    {
        public static bool Prefix(BaseInventoryObject obj)
        {
            return !(Main.RandomPrayer.UseRandomPrayer && obj is Prayer);
        }
    }
    [HarmonyPatch(typeof(NewInventory_LayoutGrid), "UnEquipObject")]
    public class InventoryUnequip_Patch
    {
        public static bool Prefix(BaseInventoryObject obj)
        {
            return !(Main.RandomPrayer.UseRandomPrayer && obj is Prayer);
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

    // Set next random prayer when done using previous one
    [HarmonyPatch(typeof(PrayerUse), "EndUsingPrayer")]
    public class PrayerUseEnd_Patch
    {
        public static void Postfix()
        {
            Main.RandomPrayer.RandomizeNextPrayer();
        }
    }

    // Get random prayer instead of equipped one
    //[HarmonyPatch(typeof(PrayerUse), "GetEquippedPrayer")]
    //public class PrayerUseGet_Patch
    //{
    //    public static bool Prefix(ref Prayer __result)
    //    {
    //        if (Main.RandomPrayer.UseRandomPrayer)
    //        {
    //            __result = Main.RandomPrayer.NextPrayer;
    //            return false;
    //        }
    //        return true;
    //    }
    //}

    // On cast start - set box image
}
