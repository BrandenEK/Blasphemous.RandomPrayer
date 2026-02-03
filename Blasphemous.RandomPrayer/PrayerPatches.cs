using Blasphemous.ModdingAPI;
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

/// <summary>
/// Dont allow equipping or unequipping prayers
/// </summary>
[HarmonyPatch(typeof(NewInventory_LayoutGrid), nameof(NewInventory_LayoutGrid.EquipObject))]
class NewInventory_LayoutGrid_EquipObject_Patch
{
    public static bool Prefix(BaseInventoryObject obj)
    {
        return !(Main.RandomPrayer.UseRandomPrayer && obj is Prayer);
    }
}
[HarmonyPatch(typeof(NewInventory_LayoutGrid), nameof(NewInventory_LayoutGrid.UnEquipObject))]
class NewInventory_LayoutGrid_UnEquipObject_Patch
{
    public static bool Prefix(BaseInventoryObject obj)
    {
        return !(Main.RandomPrayer.UseRandomPrayer && obj is Prayer);
    }
}

/// <summary>
/// Allow Miriam portal prayer to stay activated
/// </summary>
[HarmonyPatch(typeof(InventoryManager), nameof(InventoryManager.IsPrayerEquipped), typeof(string))]
class InventoryManager_IsPrayerEquipped_Patch
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

/// <summary>
/// Allow library room to think you have no prayers equipped
/// </summary>
[HarmonyPatch(typeof(InventoryManager), nameof(InventoryManager.IsAnyPrayerEquipped))]
class InventoryManager_IsAnyPrayerEquipped_Patch
{
    public static void Postfix(ref bool __result)
    {
        if (Main.RandomPrayer.UseRandomPrayer)
            __result = false;
    }
}

/// <summary>
/// Recalculate next prayer when obtaining new one
/// </summary>
[HarmonyPatch(typeof(InventoryManager), nameof(InventoryManager.AddPrayer), typeof(Prayer))]
class InventoryManager_AddPrayer_Patch
{
    public static void Postfix()
    {
        if (Main.RandomPrayer.UseRandomPrayer)
            Main.RandomPrayer.RandomizeNextPrayer();
    }
}

/// <summary>
/// Load images for prayer background
/// </summary>
[HarmonyPatch(typeof(NewInventory_GridItem), nameof(NewInventory_GridItem.Awake))]
class NewInventory_GridItem_Awake_Patch
{
    public static void Postfix(Sprite ___frameSelected, Sprite ___backEquipped)
    {
        Main.RandomPrayer.FrameImage = ___frameSelected;
        Main.RandomPrayer.BackImage = ___backEquipped;
    }
}

/// <summary>
/// Hide the next prayer to use
/// </summary>
[HarmonyPatch(typeof(NewInventory_LayoutGrid), nameof(NewInventory_LayoutGrid.UpdateEquipped))]
class NewInventory_LayoutGrid_UpdateEquipped_Patch
{
    public static void Prefix(InventoryManager.ItemType itemType, List<NewInventory_GridItem> ___cachedEquipped)
    {
        if (___cachedEquipped.Count > 0 && ___cachedEquipped[0] != null)
            ___cachedEquipped[0].gameObject.SetActive(!Main.RandomPrayer.UseRandomPrayer || itemType != InventoryManager.ItemType.Prayer);
    }
}
[HarmonyPatch(typeof(NewInventory_LayoutGrid), nameof(NewInventory_LayoutGrid.IsEquipped))]
class NewInventory_LayoutGrid_IsEquipped_Patch
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

/// <summary>
/// Set image for ui box when starting prayer
/// </summary>
[HarmonyPatch(typeof(PrayerUse), nameof(PrayerUse.OnCastStart))]
class PrayerUse_OnCastStart_Patch
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

/// <summary>
/// Set next random prayer when done using previous one
/// </summary>
[HarmonyPatch(typeof(PrayerUse), nameof(PrayerUse.EndUsingPrayer))]
class PrayerUse_EndUsingPrayer_Patch
{
    public static void Postfix()
    {
        if (Main.RandomPrayer.UseRandomPrayer)
            Main.RandomPrayer.RandomizeNextPrayer();
    }
}

/// <summary>
/// Create ui box to display current prayer
/// </summary>
[HarmonyPatch(typeof(PlayerFervour), nameof(PlayerFervour.OnLevelLoaded))]
class PlayerFervour_OnLevelLoaded_Patch
{
    public static void Postfix(PlayerFervour __instance, GameObject ___normalPrayerInUse)
    {
        if (Main.RandomPrayer.PrayerImage != null || Main.RandomPrayer.FrameImage == null || Main.RandomPrayer.BackImage == null)
            return;

        ModLog.Info("Creating new prayer use image");

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
