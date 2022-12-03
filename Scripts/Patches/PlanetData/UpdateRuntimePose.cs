using HarmonyLib;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

namespace GalacticScale
{
    public class PatchOnPlanetDataRunPose
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlanetData), "UpdateRuntimePose")]
        public static bool UpdateRuntimePose(Double time, PlanetData __instance)
        {

            if (GS2.IsMenuDemo || GS2.Vanilla) return true;

            var starUPos = __instance.star.uPosition;

            double num = time / __instance.orbitalPeriod + __instance.orbitPhase / 360.0;
            int num2 = (int)(num + 0.1);
            num -= num2;
            __instance.runtimeOrbitPhase = (float)num * 360f;
            num *= Math.PI * 2.0;
            VectorLF3 vectorLF = Maths.QRotateLF(__instance.runtimeOrbitRotation, new VectorLF3(Math.Cos(num) * __instance.orbitRadius, 0f, (float)Math.Sin(num) * __instance.orbitRadius));
            if (__instance.orbitAroundPlanet != null)
            {
                vectorLF += __instance.orbitAroundPlanet.runtimePosition;
                //vectorLF.y += __instance.orbitAroundPlanet.runtimePosition.y;
                //vectorLF.z += __instance.orbitAroundPlanet.runtimePosition.z;
            }
            __instance.runtimePosition = vectorLF;

            double num3 = time / __instance.rotationPeriod + __instance.rotationPhase / 360.0;
            int num4 = (int)(num3 + 0.1);
            num3 = (num3 - num4) * 360.0;
            //__instance.runtimeRotationPhase = (float)num3;
            __instance.runtimeRotation = __instance.runtimeSystemRotation * Quaternion.AngleAxis((float)num3, Vector3.down);
            __instance.uPosition = starUPos + vectorLF * 40000.0;
            //__instance.uPosition.y = starUPos.y + vectorLF.y;
            //__instance.uPosition.z = starUPos.z + vectorLF.z;
            if (__instance.factory != null) __instance.runtimeLocalSunDirection = Maths.QInvRotate(__instance.runtimeRotation, -vectorLF);
            double num5 = time + 1.0 / 60.0;
            double num6 = num5 / __instance.orbitalPeriod + __instance.orbitPhase / 360.0;
            int num7 = (int)(num6 + 0.1);
            num6 -= num7;
            num6 *= Math.PI * 2.0;
            double num8 = num5 / __instance.rotationPeriod + __instance.rotationPhase / 360.0;
            int num9 = (int)(num8 + 0.1);
            num8 = (num8 - num9) * 360.0;
            VectorLF3 vectorLF2 = Maths.QRotateLF(__instance.runtimeOrbitRotation, new VectorLF3((float)Math.Cos(num6) * __instance.orbitRadius, 0f, (float)Math.Sin(num6) * __instance.orbitRadius));
            if (__instance.orbitAroundPlanet != null)
            {
                vectorLF2 += __instance.orbitAroundPlanet.runtimePositionNext;
                //vectorLF2.y += __instance.orbitAroundPlanet.runtimePositionNext.y;
                //vectorLF2.z += __instance.orbitAroundPlanet.runtimePositionNext.z;
            }
            __instance.runtimePositionNext = vectorLF2;
            __instance.runtimeRotationNext = __instance.runtimeSystemRotation * Quaternion.AngleAxis((float)num8, Vector3.down);
            __instance.uPositionNext = starUPos + vectorLF2 * 40000.0;
            //__instance.uPositionNext.y = starUPos.y + vectorLF2.y * 40000.0;
            //__instance.uPositionNext.z = starUPos.z + vectorLF2.z * 40000.0;
            var astroData = __instance.galaxy.astrosData[__instance.id];
            astroData.uPos = __instance.uPosition;
            astroData.uRot = __instance.runtimeRotation;
            astroData.uPosNext = __instance.uPositionNext;
            astroData.uRotNext = __instance.runtimeRotationNext;

            return false;
        }
    }
}