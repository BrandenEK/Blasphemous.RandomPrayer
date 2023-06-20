using BepInEx;

namespace RandomPrayerUse
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.damocles.blasphemous.modding-api", "1.3.0")]
    [BepInProcess("Blasphemous.exe")]
    public class Main : BaseUnityPlugin
    {
        public static RandomPrayer RandomPrayer { get; private set; }

        private void Start()
        {
            RandomPrayer = new RandomPrayer(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION);
        }
    }
}