using AutoPickupTent.Patches;
using HarmonyLib;

namespace AutoPickupTent
{
    [HarmonyPatch]
    public class Patcher
    {
        [HarmonyPrefix]
        [HarmonyPatch(declaringType: typeof(Dialog), methodName: nameof(Dialog.YesNo))]
        public static void DialogYesNo(string langDetail)
        {
            DialogPatch.YesNoPrefix(langDetail: langDetail);
        }
        
        [HarmonyPostfix]
        [HarmonyPatch(declaringType: typeof(Player), methodName: nameof(Player.MoveZone))]
        public static void PlayerMoveZonePostfix(Zone z)
        {
            PlayerPatch.MoveZonePostfix(z: z);
        }
        
        [HarmonyPrefix]
        [HarmonyPatch(declaringType: typeof(TraitNewZone), methodName: nameof(TraitNewZone.OnStepped))]
        public static void TraitNewZoneOnStepped(TraitNewZone __instance, Chara c)
        {
            TraitNewZonePatch.OnSteppedPrefix(__instance: __instance, c: c);
        }
        
        [HarmonyPrefix]
        [HarmonyPatch(declaringType: typeof(Zone), methodName: nameof(Zone.Destroy))]
        public static bool ZoneDestroy(Zone __instance)
        {
            return ZonePatch.DestroyPrefix(__instance: __instance);
        }
        
        [HarmonyPrefix]
        [HarmonyPatch(declaringType: typeof(Zone), methodName: nameof(Zone.UnloadMap))]
        public static bool ZoneUnloadMap(Zone __instance)
        {
            return ZonePatch.UnloadMapPrefix(__instance: __instance);
        }
    }
}