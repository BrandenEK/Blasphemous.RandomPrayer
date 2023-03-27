using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using Framework.Managers;
using Framework.Inventory;
using Gameplay.UI.Others.MenuLogic;
using Gameplay.UI.Others.UIGameLogic;
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

    // Set image for ui box when starting prayer
    [HarmonyPatch(typeof(PrayerUse), "OnCastStart")]
    public class PrayerUseStart_Patch
    {
        public static void Postfix()
        {
            if (Main.RandomPrayer.UseRandomPrayer && Main.RandomPrayer.PrayerImage != null)
                Main.RandomPrayer.PrayerImage.sprite = Core.InventoryManager.GetPrayerInSlot(0).picture;
        }
    }

    // Set next random prayer when done using previous one
    [HarmonyPatch(typeof(PrayerUse), "EndUsingPrayer")]
    public class PrayerUseEnd_Patch
    {
        public static void Postfix()
        {
            if (Main.RandomPrayer.UseRandomPrayer)
                Main.RandomPrayer.RandomizeNextPrayer();
        }
    }

    // Create ui box to display current prayer
    [HarmonyPatch(typeof(PlayerFervour), "OnLevelLoaded")]
    public class PlayerFervourCreate_Patch
    {
        public static void Postfix(PlayerFervour __instance, GameObject ___normalPrayerInUse)
        {
            if (Main.RandomPrayer.PrayerImage != null || Main.RandomPrayer.FrameImage == null || Main.RandomPrayer.BackImage == null) return;
            Main.RandomPrayer.Log("Creating new prayer use image");

            GameObject frameObject = Object.Instantiate(___normalPrayerInUse, __instance.transform);
            RectTransform frameRect = frameObject.transform as RectTransform;
            frameRect.anchorMin = new Vector2(0f, 1f);
            frameRect.anchorMax = new Vector2(0f, 1f);
            frameRect.pivot = new Vector2(0f, 1f);
            frameRect.anchoredPosition = new Vector2(40f, -45f);
            frameRect.sizeDelta = new Vector2(30f, 30f);
            frameObject.GetComponent<Image>().sprite = Main.RandomPrayer.BackImage;
            frameObject.SetActive(false);

            GameObject imageObject = Object.Instantiate(___normalPrayerInUse, frameRect);
            RectTransform imageRect = imageObject.transform as RectTransform;
            imageRect.anchorMin = Vector2.zero;
            imageRect.anchorMax = Vector2.one;
            imageRect.pivot = new Vector2(0.5f, 0.5f);
            imageRect.sizeDelta = Vector2.zero;
            imageObject.SetActive(true);

            Main.RandomPrayer.PrayerImage = imageObject.GetComponent<Image>();
            Main.RandomPrayer.PrayerImage.sprite = null;
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
