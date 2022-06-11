using HarmonyLib;

namespace GalacticScale
{
    public static partial class PatchOnGameAbnormalityData
    {
        [HarmonyPrefix, HarmonyPatch(typeof(ABN.GameAbnormalityData_0925), "NothingAbnormal")]
        public static bool IsGameNormal(ref bool __result)
        {
            __result = true;
            return false;
        }
    }
}