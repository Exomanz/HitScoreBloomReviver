using HarmonyLib;
using IPA.Utilities;
using System.Linq;
using TMPro;
using UnityEngine;

namespace HitScoreBloomReviver.HarmonyPatches
{
    [HarmonyPatch(typeof(EffectPoolsManualInstaller), nameof(EffectPoolsManualInstaller.ManualInstallBindings), MethodType.Normal)]
    public class FlyingScoreEffectMaterialPatch
    {
        private static TMP_FontAsset customFontAsset;

        [HarmonyPrefix]
        internal static void Prefix(FlyingScoreEffect ____flyingScoreEffectPrefab)
        {
            if (!Plugin.Config.Enabled) return;

            var fseb = ____flyingScoreEffectPrefab;
            var text = fseb.GetField<TextMeshPro, FlyingScoreEffect>("_text");

            // I have to make a separate font since if I adjust the materials shader, everything 
            // using the original FontAsset would have bloom applied to it.
            customFontAsset = TMP_FontAsset.CreateFontAsset(Resources.FindObjectsOfTypeAll<TMP_FontAsset>().First(x => x.name.Contains(
                "Teko-Medium SDF")).sourceFontFile);
            customFontAsset.name = "Teko-Medium SDF Bloom";

            // Also can't use Mobile shaders since they lack the underlay that makes them glow.
            customFontAsset.material.shader = Resources.FindObjectsOfTypeAll<Shader>().First(x => x.name.Contains(
                "TextMeshPro/Distance Field"));

            text.font = customFontAsset;
        }
    }
}