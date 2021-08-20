using IPA.Config.Stores.Attributes;
using Hive.Versioning;
using SiraUtil.Converters;

namespace HitScoreBloomReviver
{
    public class PluginConfig
    {
        [UseConverter(typeof(HiveVersionConverter))] public virtual Version Version { get; set; } = new Version("0.0.0");

        public virtual bool Enabled { get; set; } = true;
    }
}
