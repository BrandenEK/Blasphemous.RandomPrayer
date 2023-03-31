using BepInEx;

namespace RandomPrayerUse
{
    [BepInPlugin(MOD_ID, MOD_NAME, MOD_VERSION)]
    [BepInDependency("com.damocles.blasphemous.modding-api", "1.3.0")]
    [BepInProcess("Blasphemous.exe")]
    public class Main : BaseUnityPlugin
    {
        public const string MOD_ID = "com.author.blasphemous.random-prayer-use";
        public const string MOD_NAME = "Random Prayer";
        public const string MOD_VERSION = "1.0.1";

        public static RandomPrayer RandomPrayer { get; private set; }

        private void Start()
        {
            RandomPrayer = new RandomPrayer(MOD_ID, MOD_NAME, MOD_VERSION);
        }
    }
}