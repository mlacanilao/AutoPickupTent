using System.Collections.Generic;
using AutoPickupTent.Utilities;
using B83.Win32;

namespace AutoPickupTent.Patches
{
    public static class ZonePatch
    {
        public static bool DestroyPrefix(Zone __instance)
        {
            if (__instance is Zone_Tent == true)
            {
                return false;
            }
            
            if (__instance?.map == null)
            {
                return true;
            }
            
            List<Thing> tents = TentUtils.FindTents(map: __instance.map);
            
            if (tents.Count == 0)
            {
                return true;
            }
            
            TentUtils.PickUpTents(tents: tents);

            return true;
        }
        
        public static bool UnloadMapPrefix(Zone __instance)
        {
            if (__instance?.map == null)
            {
                return true;
            }

            if (__instance is Zone_Tent == true)
            {
                return false;
            }
            
            List<Thing> tents = TentUtils.FindTents(map: __instance.map);
            
            if (tents.Count == 0)
            {
                return true;
            }

            if (AutoPickupTentConfig.EnableNotifications?.Value == true)
            {
                Msg.SayPic(c: EClass.pc,
                    lang: TentUtils.__(jp: $"私は{__instance.Name} {__instance.RegionPos}にテントを置いてきました！ 現在地: {EClass.pc?.pos}",
                        en: $"I left my tent(s) at {__instance.Name} {__instance.RegionPos}! Current location: {EClass.pc?.pos}",
                        cn: $"我把帐篷留在了{__instance.Name} {__instance.RegionPos}！ 当前所在地: {EClass.pc?.pos}"
                    )
                );
            }

            return false;
        }
    }
}