using System.Collections.Generic;
using AutoPickupTent.Utilities;

namespace AutoPickupTent.Patches
{
    public static class TraitNewZonePatch
    {
        public static void OnSteppedPrefix(TraitNewZone __instance, Chara c)
        {
            if (AutoPickupTentConfig.EnableAutoPickup?.Value == false &&
                AutoPickupTentConfig.EnableDialog?.Value == false)
            {
                return;
            }
            
            if (__instance.AutoEnter && 
                c.IsPC && 
                __instance.owner.IsInstalled && 
                c.IsAliveInCurrentZone &&
                (__instance.ForceEnter || EClass.core.config.game.disableAutoStairs))
            {
                List<Thing> tents = new List<Thing>();

                foreach (Thing thing in EClass._map.things)
                {
                    if (thing.trait is TraitTent == false || thing.isNPCProperty == true)
                    {
                        continue;
                    }

                    tents.Add(item: thing);
                }

                if (tents.Count == 0)
                {
                    return;
                }

                if (AutoPickupTentConfig.EnableDialog?.Value == true)
                {
                    Dialog.YesNo(
                        langDetail: TentUtils.__(
                            jp: "テントを回収しますか？",
                            en: "Pick up tent(s)?",
                            cn: "拾取帐篷？"
                        ),
                        actionYes: delegate { TentUtils.PickUpTents(tents: tents); },
                        actionNo: delegate { },
                        langYes: "yes",
                        langNo: "no"
                    );
                }
                else if (AutoPickupTentConfig.EnableAutoPickup?.Value == true)
                {
                    TentUtils.PickUpTents(tents: tents);
                }
            }
        }
    }
}