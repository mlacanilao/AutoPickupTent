using System.Collections.Generic;
using System.Linq;

namespace AutoPickupTent.Utilities
{
    public static class TentUtils
    {
        public static void PickUpTents(List<Thing> tents)
        {
            if (tents == null)
            {
                return;
            }
            
            foreach (Thing tent in tents)
            {
                tent.SetPlaceState(newState: 0);
                tent.SetFreePos(point: EClass.pc?.pos);
                EClass.pc?.Pick(t: tent);
            }
            
            HashSet<int> uniqueUids = new HashSet<int>();
            List<Thing> duplicates = new List<Thing>();
            var allTents = EClass.pc?.things.Where(predicate: t => t.trait is TraitTent).ToList();
            if (allTents != null)
            {
                foreach (Thing tent in allTents)
                {
                    if (tent == null)
                    {
                        continue;
                    }

                    if (!uniqueUids.Add(item: tent.uid))
                    {
                        duplicates.Add(item: tent);
                    }
                }

                foreach (Thing duplicate in duplicates)
                {
                    duplicate.Destroy();
                }
            }
        }
        
        public static List<Thing> FindTents(Map map)
        {
            List<Thing> tents = new List<Thing>();

            if (map == null || map.things == null)
            {
                return tents;
            }
            
            if (AutoPickupTentConfig.IgnorePCFactionZones?.Value == true &&
                map.zone?.IsPCFaction == true)
            {
                return tents;
            }

            foreach (Thing thing in map.things)
            {
                if (thing?.trait is TraitContainer == true)
                {
                    foreach (Thing thingContainer in thing?.things)
                    {
                        if (thingContainer?.trait is TraitTent == true)
                        {
                            tents.Add(item: thingContainer);
                        }
                    }
                    continue;
                }

                if (thing?.trait is TraitTent == true && thing?.isNPCProperty == false)
                {
                    tents.Add(item: thing);
                }
            }

            return tents;
        }
        
        public static string __(string jp = "", string en = "", string cn = "")
        {
            if (Lang.langCode == "JP")
            {
                return jp ?? en;
            }

            if (Lang.langCode == "CN")
            {
                return cn ?? en;
            }

            return en;
        }
    }
}