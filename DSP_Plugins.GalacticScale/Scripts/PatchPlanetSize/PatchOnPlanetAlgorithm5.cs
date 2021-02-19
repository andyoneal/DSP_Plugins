﻿using System;
using BepInEx.Logging;
using GalacticScale.Scripts.PatchStarSystemGeneration;
using HarmonyLib;
using UnityEngine;
using Patch = GalacticScale.Scripts.PatchPlanetSize.PatchForPlanetSize;
using Random = System.Random;

namespace GalacticScale.Scripts.PatchPlanetSize {
    [HarmonyPatch(typeof(PlanetAlgorithm5))]
    public class PatchOnPlanetAlgorithm5 {
        [HarmonyPrefix]
        [HarmonyPatch("GenerateTerrain")]
        public static bool PatchGenerateTerrain(ref PlanetAlgorithm1 __instance, ref PlanetData ___planet,
            double modX, double modY) {

            Patch.Debug("GenerateTerrain" + ___planet.radius + " for : " + ___planet.name, LogLevel.Debug,
                Patch.DebugPlanetAlgorithm5);
            return true;
        }
        [HarmonyPrefix]
        [HarmonyPatch("GenerateVegetables")]
        public static bool PatchGenerateVegetables(ref PlanetData ___planet) {
            Patch.Debug("GenerateVegetables 5:  " + ___planet.radius + " for : " + ___planet.name,
                LogLevel.Debug, Patch.DebugPlanetAlgorithm5);
            ThemeProto themeProto = LDB.themes.Select(___planet.theme);
            if (themeProto == null)
                return false;
            int[] vegetables0 = themeProto.Vegetables0;
            int[] vegetables1 = themeProto.Vegetables1;
            int[] vegetables2 = themeProto.Vegetables2;
            int[] vegetables3 = themeProto.Vegetables3;
            float num1 = 1.3f;
            float num2 = -0.2f;
            float num3 = 2.5f;
            float num4 = 4f;
            float num5 = 0.5f;
            float num6 = 1f;
            float num7 = 2f;
            float num8 = -0.2f;
            float num9 = 1.4f;
            Random random1 = new Random(___planet.seed);
            random1.Next();
            random1.Next();
            random1.Next();
            Random random2 = new Random(random1.Next());
            SimplexNoise simplexNoise1 = new SimplexNoise(random2.Next());
            SimplexNoise simplexNoise2 = new SimplexNoise(random2.Next());
            PlanetRawData data = ___planet.data;
            int stride = data.stride;
            int num10 = stride / 2;
            float num11 =
                (float) (___planet.radius * 3.1415901184082 * 2.0 / (data.precision * 4.0));
            VegeData vege = new VegeData();
            VegeProto[] vegeProtos = PlanetModelingManager.vegeProtos;
            Vector4[] vegeScaleRanges = PlanetModelingManager.vegeScaleRanges;
            short[] vegeHps = PlanetModelingManager.vegeHps;
            for (int index1 = 0; index1 < data.dataLength; ++index1) {
                int num12 = index1 % stride;
                int num13 = index1 / stride;
                if (num12 > num10)
                    --num12;
                if (num13 > num10)
                    --num13;
                if (num12 % 2 == 1 && num13 % 2 == 1) {
                    Vector3 vertex = data.vertices[index1];
                    double num14 = data.vertices[index1].x * (double) ___planet.radius;
                    double num15 = data.vertices[index1].y * (double) ___planet.radius;
                    double num16 = data.vertices[index1].z * (double) ___planet.radius;
                    float a = data.heightData[index1] * 0.01f;
                    float b1 = data.heightData[index1 + 1 + stride] * 0.01f;
                    float b2 = data.heightData[index1 - 1 + stride] * 0.01f;
                    float b3 = data.heightData[index1 + 1 - stride] * 0.01f;
                    float b4 = data.heightData[index1 - 1 - stride] * 0.01f;
                    float num17 = data.heightData[index1 + 1] * 0.01f;
                    float num18 = data.heightData[index1 - 1] * 0.01f;
                    float num19 = data.heightData[index1 + stride] * 0.01f;
                    float num20 = data.heightData[index1 - stride] * 0.01f;
                    float num21 = data.biomoData[index1] * 0.01f;
                    if (a >= (double) ___planet.radius && b1 >= (double) ___planet.radius &&
                        (b2 >= (double) ___planet.radius && b3 >= (double) ___planet.radius) &&
                        (b4 >= (double) ___planet.radius && num17 >= (double) ___planet.radius &&
                         (num18 >= (double) ___planet.radius &&
                          num19 >= (double) ___planet.radius)) &&
                        num20 >= (double) ___planet.radius) {
                        bool flag = true;
                        if (diff(a, b1) > 0.200000002980232) {
                            flag = false;
                        }
                        if (diff(a, b2) > 0.200000002980232) {
                            flag = false;
                        }
                        if (diff(a, b3) > 0.200000002980232) {
                            flag = false;
                        }
                        if (diff(a, b4) > 0.200000002980232) {
                            flag = false;
                        }
                        double num22 = random2.NextDouble();
                        double num23 = num22 * num22;
                        double num24 = random2.NextDouble();
                        float num25 = (float) random2.NextDouble() - 0.5f;
                        float num26 = (float) random2.NextDouble() - 0.5f;
                        float num27 = (float) Math.Sqrt(random2.NextDouble());
                        float angle1 = (float) random2.NextDouble() * 360f;
                        float num28 = (float) random2.NextDouble();
                        float num29 = (float) random2.NextDouble();
                        int[] numArray1;
                        float num30;
                        float num31;
                        float num32;
                        if (num21 < 0.800000011920929) {
                            numArray1 = vegetables0;
                            num30 = num1;
                            num31 = num2;
                            num32 = num3;
                        }
                        else {
                            numArray1 = vegetables1;
                            num30 = num4;
                            num31 = num5;
                            num32 = num6;
                        }

                        double num33 = simplexNoise1.Noise(num14 * 0.07, num15 * 0.07, num16 * 0.07) * num30 +
                                       num31 + 0.5;
                        double num34 = simplexNoise2.Noise(num14 * 0.4, num15 * 0.4, num16 * 0.4) * num7 +
                                       num8 +
                                       0.5;
                        double num35 = num34 - 0.2;
                        int[] numArray2;
                        double num36;
                        int num37;
                        if (num21 > 1.0) {
                            numArray2 = vegetables2;
                            num36 = num34;
                            num37 = 4;
                        }
                        else {
                            numArray2 = vegetables3;
                            num36 = num35;
                            num37 = 1;
                        }

                        if (flag && num24 < num33 && (numArray1 != null && numArray1.Length > 0)) {
                            vege.protoId = (short) numArray1[(int) (num23 * numArray1.Length)];
                            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, vertex);
                            Vector3 vector3_1 = rotation * Vector3.forward;
                            Vector3 vector3_2 = rotation * Vector3.right;
                            Vector4 vector4 = vegeScaleRanges[vege.protoId];
                            Vector3 vector3_3 = vertex * a;
                            Vector3 vector3_4 = (vector3_2 * num25 + vector3_1 * num26).normalized *
                                                (num27 * num32 * num11);
                            float y = (float) (num29 * (vector4.x + (double) vector4.y) +
                                               (1.0 - vector4.x));
                            float num38 =
                                (float) (num28 * (vector4.z + (double) vector4.w) +
                                         (1.0 - vector4.z)) * y;
                            vege.pos = (vector3_3 + vector3_4).normalized;
                            a = data.QueryHeight(vege.pos);
                              vege.pos *= a *  ___planet.GetScaleFactored();
                            vege.rot = Quaternion.FromToRotation(Vector3.up, vege.pos.normalized) *
                                       Quaternion.AngleAxis(angle1, Vector3.up);
                            vege.scl = new Vector3(num38, y, num38);
                            vege.modelIndex = (short) vegeProtos[vege.protoId].ModelIndex;
                            vege.hp = vegeHps[vege.protoId];
                            int num39 = data.AddVegeData(vege);
                            data.vegeIds[index1] = (ushort) num39;
                        }

                        if (num24 < num36 && numArray2 != null && numArray2.Length > 0) {
                            vege.protoId = (short) numArray2[(int) (num23 * numArray2.Length)];
                            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, vertex);
                            Vector3 vector3_1 = rotation * Vector3.forward;
                            Vector3 vector3_2 = rotation * Vector3.right;
                            Vector4 vector4 = vegeScaleRanges[vege.protoId];
                            for (int index2 = 0; index2 < num37; ++index2) {
                                float num38 = (float) random2.NextDouble() - 0.5f;
                                float num39 = (float) random2.NextDouble() - 0.5f;
                                float num40 = (float) Math.Sqrt(random2.NextDouble());
                                float angle2 = (float) random2.NextDouble() * 360f;
                                float num41 = (float) random2.NextDouble();
                                float num42 = (float) random2.NextDouble();
                                Vector3 vector3_3 = vertex * a;
                                Vector3 vector3_4 = (vector3_2 * num38 + vector3_1 * num39).normalized *
                                                    (num40 * num9 * num11);
                                float y =
                                    (float) (num42 * (vector4.x + (double) vector4.y) +
                                             (1.0 - vector4.x));
                                float num43 = (float) (num41 * (vector4.z + (double) vector4.w) +
                                                       (1.0 - vector4.z)) * y;
                                vege.pos = (vector3_3 + vector3_4).normalized;
                                a = data.QueryHeight(vege.pos);
                                  vege.pos *= a *  ___planet.GetScaleFactored();
                                vege.rot = Quaternion.FromToRotation(Vector3.up, vege.pos.normalized) *
                                           Quaternion.AngleAxis(angle2, Vector3.up);
                                vege.scl = new Vector3(num43, y, num43);
                                vege.modelIndex = (short) vegeProtos[vege.protoId].ModelIndex;
                                vege.hp = 1;
                                int num44 = data.AddVegeData(vege);
                                data.vegeIds[index1] = (ushort) num44;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private static float diff(float a, float b) => (double) a > (double) b ? a - b : b - a;
    }
}