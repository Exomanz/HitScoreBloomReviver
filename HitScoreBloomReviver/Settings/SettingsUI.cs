using BeatSaberMarkupLanguage.Attributes;

namespace HitScoreBloomReviver.Settings
{
    public class SettingsUI : PersistentSingleton<SettingsUI>
    {
        PluginConfig Config => Plugin.Config;

        [UIValue("Enabled")]
        protected bool Enabled
        {
            get => Config.Enabled;
            set => Config.Enabled = value;
        }
    }
}
