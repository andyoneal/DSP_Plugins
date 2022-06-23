using System;
using System.IO;
using HarmonyLib;

namespace GalacticScale
{
   
    public partial class PatchOnGameAchievementData
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(GameAchievementData), "Import")]
        public static bool Import(GameAchievementData __instance, BinaryReader r)
        {
            r.ReadInt32();
            int num = r.ReadInt32();
            GS2.Warn($"save RuntimeDatasCount {num}");
            for (int i = 0; i < num; i++)
            {
                int num2 = r.ReadInt32();
                if (__instance.runtimeDatas.ContainsKey(num2) && __instance.runtimeDatas[num2] != null)
                {
                    GS2.Warn($"If True {num2}");
                    __instance.runtimeDatas[num2].Import(r);
                }
                else
                {
                    GS2.Warn($"If False {num2}");
                    if (!LDB.achievements.Exist(num2)) return false;
                    AchievementProto achievementProto = LDB.achievements.Select(num2);
                    if (!String.IsNullOrEmpty(achievementProto.RuntimeDataName)) 
                        (__instance.runtimeAsm.CreateInstance(achievementProto.RuntimeDataName) as AchievementRuntimeData)?.Import(r);
                }
            }

            return false;
        }
    }
}