using BeatSaberMarkupLanguage.GameplaySetup;
using HarmonyLib;
using HitScoreBloomReviver.Settings;
using IPA;
using IPA.Config.Stores;
using IPA.Loader;
using Logger = IPA.Logging.Logger;
using System.Reflection;

namespace HitScoreBloomReviver
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Harmony harmonyId;
        internal static Logger Logger;
        internal static PluginConfig Config;

        [Init] public Plugin(IPA.Config.Config conf, Logger log, PluginMetadata data)
        {
            Config = conf.Generated<PluginConfig>();
            Logger = log;
            Config.Version = data.HVersion;
        }

        [OnEnable] public void Enable()
        {
            if (harmonyId is null) harmonyId = new Harmony("bs.Exomanz.hsbr");
            harmonyId.PatchAll(Assembly.GetExecutingAssembly());
            GameplaySetup.instance.AddTab("HitScoreBloom", "HitScoreBloomReviver.Settings.settingsView.bsml", SettingsUI.instance);
        }

        [OnDisable] public void Disable()
        {
            GameplaySetup.instance.RemoveTab("HitScoreBloom");
            harmonyId.UnpatchAll(harmonyId.Id);
            harmonyId = null;
        }
    }
}
