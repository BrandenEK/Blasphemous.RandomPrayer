using Framework.Inventory;
using Framework.Managers;
using Gameplay.GameControllers.Penitent.Abilities;
using Gameplay.UI.Others.MenuLogic;
using Gameplay.UI.Others.UIGameLogic;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Blasphemous.RandomPrayer;

// Dont allow equipping or unequipping prayers
[HarmonyPatch(typeof(NewInventory_LayoutGrid), "EquipObject")]
class InventoryEquip_Patch
{
    public static bool Prefix(BaseInventoryObject obj)
    {
        return !(Main.RandomPrayer.UseRandomPrayer && obj is Prayer);
    }
}
[HarmonyPatch(typeof(NewInventory_LayoutGrid), "UnEquipObject")]
class InventoryUnequip_Patch
{
    public static bool Prefix(BaseInventoryObject obj)
    {
        return !(Main.RandomPrayer.UseRandomPrayer && obj is Prayer);
    }
}

// Allow Miriam portal prayer to stay activated
[HarmonyPatch(typeof(InventoryManager), "IsPrayerEquipped", typeof(string))]
class InventoryPrayer_Patch
{
    public static bool Prefix(string idPrayer, ref bool __result)
    {
        if (Main.RandomPrayer.UseRandomPrayer && idPrayer == "PR201")
        {
            __result = true;
            return false;
        }
        return true;
    }
}

// Allow library room to think you have no prayers equipped
[HarmonyPatch(typeof(InventoryManager), "IsAnyPrayerEquipped")]
class InventoryAnyPrayer_Patch
{
    public static void Postfix(ref bool __result)
    {
        if (Main.RandomPrayer.UseRandomPrayer)
            __result = false;
    }
}

// Recalculate next prayer when obtaining new one
[HarmonyPatch(typeof(InventoryManager), "AddPrayer", typeof(Prayer))]
class InventoryAdd_Patch
{
    public static void Postfix()
    {
        if (Main.RandomPrayer.UseRandomPrayer)
            Main.RandomPrayer.RandomizeNextPrayer();
    }
}

// Load images for prayer background
[HarmonyPatch(typeof(NewInventory_GridItem), "Awake")]
class InvGridItemLoad_Patch
{
    public static void Postfix(Sprite ___frameSelected, Sprite ___backEquipped)
    {
        Main.RandomPrayer.FrameImage = ___frameSelected;
        Main.RandomPrayer.BackImage = ___backEquipped;
    }
}

// Hide the next prayer to use
[HarmonyPatch(typeof(NewInventory_LayoutGrid), "UpdateEquipped")]
class InvLayoutUpdate_Patch
{
    public static void Prefix(InventoryManager.ItemType itemType, List<NewInventory_GridItem> ___cachedEquipped)
    {
        if (___cachedEquipped.Count > 0 && ___cachedEquipped[0] != null)
            ___cachedEquipped[0].gameObject.SetActive(!Main.RandomPrayer.UseRandomPrayer || itemType != InventoryManager.ItemType.Prayer);
    }
}
[HarmonyPatch(typeof(NewInventory_LayoutGrid), "IsEquipped")]
class InvLayoutEquip_Patch
{
    public static bool Prefix(BaseInventoryObject obj, ref bool __result)
    {
        if (Main.RandomPrayer.UseRandomPrayer && obj != null && obj is Prayer)
        {
            __result = false;
            return false;
        }
        return true;
    }
}

// Set image for ui box when starting prayer
[HarmonyPatch(typeof(PrayerUse), "OnCastStart")]
class PrayerUseStart_Patch
{
    public static void Postfix()
    {
        Prayer prayer = Core.InventoryManager.GetPrayerInSlot(0);
        if (Main.RandomPrayer.UseRandomPrayer && Main.RandomPrayer.PrayerImage != null)
        {
            Main.RandomPrayer.PrayerImage.sprite = prayer.picture;
            Main.RandomPrayer.DisplayPrayerBox = true;
        }
    }
}

// Set next random prayer when done using previous one
[HarmonyPatch(typeof(PrayerUse), "EndUsingPrayer")]
class PrayerUseEnd_Patch
{
    public static void Postfix()
    {
        if (Main.RandomPrayer.UseRandomPrayer)
            Main.RandomPrayer.RandomizeNextPrayer();
    }
}

// Create ui box to display current prayer
[HarmonyPatch(typeof(PlayerFervour), "OnLevelLoaded")]
class PlayerFervourCreate_Patch
{
    public static void Postfix(PlayerFervour __instance, GameObject ___normalPrayerInUse)
    {
        if (Main.RandomPrayer.PrayerImage != null || Main.RandomPrayer.FrameImage == null || Main.RandomPrayer.BackImage == null)
            return;
        Main.RandomPrayer.Log("Creating new prayer use image");

        GameObject frameObject = Object.Instantiate(___normalPrayerInUse, __instance.transform);
        RectTransform frameRect = frameObject.transform as RectTransform;
        frameRect.anchorMin = new Vector2(0f, 1f);
        frameRect.anchorMax = new Vector2(0f, 1f);
        frameRect.pivot = new Vector2(0f, 1f);
        frameRect.anchoredPosition = new Vector2(40f, -60f);
        frameRect.sizeDelta = new Vector2(28f, 28f);
        frameObject.GetComponent<Image>().sprite = Main.RandomPrayer.BackImage;
        frameObject.SetActive(false);

        GameObject imageObject = Object.Instantiate(___normalPrayerInUse, frameRect);
        RectTransform imageRect = imageObject.transform as RectTransform;
        imageRect.anchorMin = Vector2.zero;
        imageRect.anchorMax = Vector2.one;
        imageRect.pivot = new Vector2(0.5f, 0.5f);
        imageRect.anchoredPosition = Vector2.zero;
        imageRect.sizeDelta = Vector2.zero;
        imageObject.SetActive(true);

        Main.RandomPrayer.PrayerImage = imageObject.GetComponent<Image>();
        Main.RandomPrayer.PrayerImage.sprite = null;
    }
}
