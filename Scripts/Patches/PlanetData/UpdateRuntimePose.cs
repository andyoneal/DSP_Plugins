using HarmonyLib;
using System;
using System.Diagnostics;
using UnityEngine;

namespace GalacticScale
{
    public partial class PatchOnPlanetData
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlanetData), "UpdateRuntimePose")]
        public static bool UpdateRuntimePose(Double time, PlanetData __instance)
        {
            if (GS2.IsMenuDemo || GS2.Vanilla) return true;

            var gsPlanet = GS2.GetGSPlanet(__instance.id);
            if (gsPlanet == null)
            {
                //GS2.Log($"Couldn't find GSPlanet for id:{__instance.id}. Reverting to slow path.");
                return true;
            }

            var dbgplanet = __instance.id == 81905;
            Stopwatch highStopwatch = new Stopwatch();
            long[] ts = new long[11];
            if (dbgplanet)
            {
                highStopwatch.Start();
            }


            var orbitIncRotateFactors = gsPlanet.orbitIncRotateFactors;
            if (dbgplanet) ts[0] = highStopwatch.ElapsedTicks; //2.2
            if (dbgplanet) highStopwatch.Restart();

            double num = time / __instance.orbitalPeriod + __instance.orbitPhase / 360.0;
            int num2 = (int)(num + 0.1);
            num -= num2;
            __instance.runtimeOrbitPhase = (float)num * 360f;
            num *= Math.PI * 2.0;
            if (dbgplanet) ts[1] = highStopwatch.ElapsedTicks; //1.0
            if (dbgplanet) highStopwatch.Restart();
            double num3 = time / __instance.rotationPeriod + __instance.rotationPhase / 360.0;
            int num4 = (int)(num3 + 0.1);
            num3 = (num3 - num4) * 360.0;
            if (dbgplanet) ts[2] = highStopwatch.ElapsedTicks; //0.5
            if (dbgplanet) highStopwatch.Restart();
            var num_sin = Mathf.Sin((float)num);
            var num_cos = Mathf.Cos((float)num);
            if (dbgplanet) ts[3] = highStopwatch.ElapsedTicks; //1.5
            if (dbgplanet) highStopwatch.Restart();

            VectorLF3 vectorLF;
            vectorLF.x = num_cos * orbitIncRotateFactors[0] + num_sin * orbitIncRotateFactors[1];
            vectorLF.y = num_cos * orbitIncRotateFactors[2];
            vectorLF.z = num_sin * orbitIncRotateFactors[3] - num_cos * orbitIncRotateFactors[4];
            if (dbgplanet) ts[4] = highStopwatch.ElapsedTicks; //0.6
            if (dbgplanet) highStopwatch.Restart();
            //VectorLF3 vectorLF = Maths.QRotateLF(__instance.runtimeOrbitRotation, new VectorLF3((float)Math.Cos(num) * __instance.orbitRadius, 0f, (float)Math.Sin(num) * __instance.orbitRadius));
            if (__instance.orbitAroundPlanet != null)
            {
                vectorLF.x += __instance.orbitAroundPlanet.runtimePosition.x;
                vectorLF.y += __instance.orbitAroundPlanet.runtimePosition.y;
                vectorLF.z += __instance.orbitAroundPlanet.runtimePosition.z;
            }
            __instance.runtimePosition = vectorLF;
            if (dbgplanet) ts[5] = highStopwatch.ElapsedTicks; //0.7
            if (dbgplanet) highStopwatch.Restart();
            __instance.runtimeRotation = __instance.runtimeSystemRotation * Quaternion.AngleAxis((float)num3, Vector3.down);
            if (dbgplanet) ts[6] = highStopwatch.ElapsedTicks; //1.3
            if (dbgplanet) highStopwatch.Restart();
            __instance.uPosition.x = __instance.star.uPosition.x + vectorLF.x * 40000.0;
            __instance.uPosition.y = __instance.star.uPosition.y + vectorLF.y * 40000.0;
            __instance.uPosition.z = __instance.star.uPosition.z + vectorLF.z * 40000.0;
            if (dbgplanet) ts[7] = highStopwatch.ElapsedTicks; //0.5
            if (dbgplanet) highStopwatch.Restart();
            __instance.runtimeLocalSunDirection = Maths.QInvRotate(__instance.runtimeRotation, -vectorLF);
            if (dbgplanet) ts[8] = highStopwatch.ElapsedTicks; //1.8
            if (dbgplanet) highStopwatch.Restart();
            double num5 = time + 1.0 / 60.0;
            double num6 = num5 / __instance.orbitalPeriod + __instance.orbitPhase / 360.0;
            int num7 = (int)(num6 + 0.1);
            num6 -= num7;
            num6 *= Math.PI * 2.0;
            double num8 = num5 / __instance.rotationPeriod + __instance.rotationPhase / 360.0;
            int num9 = (int)(num8 + 0.1);
            num8 = (num8 - num9) * 360.0;

            var num6_sin = Mathf.Sin((float)num6);
            var num6_cos = Mathf.Cos((float)num6);

            VectorLF3 vectorLF2;
            vectorLF2.x = num6_cos * orbitIncRotateFactors[0] + num6_sin * orbitIncRotateFactors[1];
            vectorLF2.y = num6_cos * orbitIncRotateFactors[2];
            vectorLF2.z = num6_sin * orbitIncRotateFactors[3] - num6_cos * orbitIncRotateFactors[4];

            //VectorLF3 vectorLF2 = Maths.QRotateLF(__instance.runtimeOrbitRotation, new VectorLF3((float)Math.Cos(num6) * __instance.orbitRadius, 0f, (float)Math.Sin(num6) * __instance.orbitRadius));
            if (__instance.orbitAroundPlanet != null)
            {
                vectorLF2.x += __instance.orbitAroundPlanet.runtimePositionNext.x;
                vectorLF2.y += __instance.orbitAroundPlanet.runtimePositionNext.y;
                vectorLF2.z += __instance.orbitAroundPlanet.runtimePositionNext.z;
            }
            __instance.runtimePositionNext = vectorLF2;
            __instance.runtimeRotationNext = __instance.runtimeSystemRotation * Quaternion.AngleAxis((float)num8, Vector3.down);
            __instance.uPositionNext.x = __instance.star.uPosition.x + vectorLF2.x * 40000.0;
            __instance.uPositionNext.y = __instance.star.uPosition.y + vectorLF2.y * 40000.0;
            __instance.uPositionNext.z = __instance.star.uPosition.z + vectorLF2.z * 40000.0;
            if (dbgplanet) ts[9] = highStopwatch.ElapsedTicks; //3.0
            if (dbgplanet) highStopwatch.Restart();
            __instance.galaxy.astrosData[__instance.id].uPos = __instance.uPosition;
            __instance.galaxy.astrosData[__instance.id].uRot = __instance.runtimeRotation;
            __instance.galaxy.astrosData[__instance.id].uPosNext = __instance.uPositionNext;
            __instance.galaxy.astrosData[__instance.id].uRotNext = __instance.runtimeRotationNext;
            if (dbgplanet) ts[10] = highStopwatch.ElapsedTicks; //0.8
            if (dbgplanet) highStopwatch.Stop();
            if (dbgplanet)
            {
                GS2.Log("frame");
                GS2.Log($"getfactors: {ts[0]}");
                GS2.Log($"num: {ts[1]}");
                GS2.Log($"num3: {ts[2]}");
                GS2.Log($"num_sin: {ts[3]}");
                GS2.Log($"rot: {ts[4]}");
                GS2.Log($"runtimepos: {ts[5]}");
                GS2.Log($"runtimerot: {ts[6]}");
                GS2.Log($"upos: {ts[7]}");
                GS2.Log($"localsun: {ts[8]}");
                GS2.Log($"next: {ts[9]}");
                GS2.Log($"end: {ts[10]}");
            }

            return false;
        }
    }
}