using System.Collections.Generic;
using AutoPickupTent.Utilities;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace AutoPickupTent
{
    internal static class ModInfo
    {
        internal const string Guid = "omegaplatinum.elin.autopickuptent";
        internal const string Name = "Auto Pickup Tent";
        internal const string Version = "1.0.1.0";
    }

    [BepInPlugin(GUID: ModInfo.Guid, Name: ModInfo.Name, Version: ModInfo.Version)]
    internal class AutoPickupTent : BaseUnityPlugin
    {
        internal static AutoPickupTent Instance { get; private set; }

        private void Start()
        {
            Instance = this;
            
            AutoPickupTentConfig.LoadConfig(config: Config);

            Harmony.CreateAndPatchAll(type: typeof(Patcher), harmonyInstanceId: ModInfo.Guid);
        }
        
        private void Update()
        {
            if (AutoPickupTentConfig.FindTentsKey.Value.IsDown() == true)
            {
                if (EClass.core?.IsGameStarted == false)
                {
                    return;
                }
        
                Dictionary<Zone, List<Thing>> tentsByZone = new Dictionary<Zone, List<Thing>>();
                
                foreach (Zone zone in EClass.game?.spatials?.Zones)
                {
                    List<Thing> tentsInZone = TentUtils.FindTents(map: zone.map);

                    if (tentsInZone.Count > 0)
                    {
                        tentsByZone[key: zone] = tentsInZone;
                    }
                }

                if (tentsByZone.Count > 0)
                {
                    foreach (var entry in tentsByZone)
                    {
                        Zone zone = entry.Key;

                        Msg.SayPic(c: EClass.pc,
                            lang: TentUtils.__(jp: $"私は{zone.Name} {zone.RegionPos}にテントを置いてきました！ 現在地: {EClass.pc?.pos}",
                                en: $"I left tent(s) at {zone.Name} {zone.RegionPos}! Current location: {EClass.pc?.pos}",
                                cn: $"我把帐篷留在了{zone.Name} {zone.RegionPos}！ 当前所在地: {EClass.pc?.pos}"
                            )
                        );
                    }
                }
                else
                {
                    Msg.SayPic(c: EClass.pc,
                        lang: TentUtils.__(jp: "見つかったテントはありません。",
                            en: "No tents were found.",
                            cn: "未找到帐篷。"
                        )
                    );
                }
            }
            
            if (AutoPickupTentConfig.EmergencyPickupTentKey.Value.IsDown() == true)
            {
                if (EClass.core?.IsGameStarted == false)
                {
                    return;
                }

                if (EClass.pc?.currentZone is Zone_Tent == true)
                {
                    return;
                }
                
                List<Thing> tents = new List<Thing>();

                foreach (Zone zone in EClass.game?.spatials?.Zones)
                {
                    tents.AddRange(collection: TentUtils.FindTents(map: zone.map));
                }
                
                if (tents.Count > 0)
                {
                    TentUtils.PickUpTents(tents: tents);

                    Msg.SayPic(c: EClass.pc,
                        lang: TentUtils.__(jp: $"緊急テント回収が発動されました。",
                            en: $"Emergency tent pickup triggered.",
                            cn: $"触发了紧急帐篷拾取。"
                        )
                    );
                    AutoPickupTent.Log(payload: $"Emergency tent pickup triggered.");
                    tents.Clear();
                }
                else
                {
                    Msg.SayPic(c: EClass.pc,
                        lang: TentUtils.__(jp: "緊急テント回収が発動されましたが、回収可能なテントは見つかりませんでした。",
                            en: "Emergency tent pickup triggered, but no tents were found.",
                            cn: "触发了紧急帐篷拾取，但未找到可拾取的帐篷。"
                        )
                    );
                    AutoPickupTent.Log(payload: "No tents found for emergency pickup.");
                }
            }
        }

        public static void Log(object payload)
        {
            Instance?.Logger.LogInfo(data: payload);
        }
    }
}