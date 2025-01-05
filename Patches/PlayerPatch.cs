using System.Collections.Generic;
using AutoPickupTent.Utilities;

namespace AutoPickupTent.Patches
{
    public static class PlayerPatch
    {
        public static void MoveZonePostfix(Zone z)
        {
            if (AutoPickupTentConfig.EnableAutoPickup?.Value == false &&
                AutoPickupTentConfig.EnableDialog?.Value == false)
            {
                return;
            }
            
            if (z is Zone_Tent == true)
            {
                return;
            }

            List<Thing> tents = TentUtils.FindTents(map: z.map);

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