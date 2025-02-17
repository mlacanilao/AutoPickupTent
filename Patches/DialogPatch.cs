using System.Collections.Generic;
using AutoPickupTent.Utilities;

namespace AutoPickupTent.Patches
{
    public static class DialogPatch
    {
        public static void YesNoPrefix(string langDetail)
        {
            if (AutoPickupTentConfig.EnableAutoPickup?.Value == false)
            {
                return;
            }

            if (EClass.core?.IsGameStarted == false)
            {
                return;
            }
            
            if (langDetail?.Contains(value: "ExitZoneEscort".lang(ref1: EClass._zone.Name, ref2: null, ref3: null, ref4: null, ref5: null)) == false &&
                langDetail?.Contains(value: "ExitZone".lang(ref1: EClass._zone.Name, ref2: null, ref3: null, ref4: null, ref5: null)) == false)
            {
                return;
            }
            
            List<Thing> tents = TentUtils.FindTents(map: EClass._map);

            if (tents.Count == 0)
            {
                return;
            }
            
            TentUtils.PickUpTents(tents: tents);
        }
    }
}