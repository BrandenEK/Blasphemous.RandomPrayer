using BepInEx;

namespace Blasphemous.RandomPrayer;

[BepInPlugin(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_VERSION)]
[BepInDependency("Blasphemous.ModdingAPI", "2.1.0")]
[BepInDependency("Blasphemous.Framework.Items", "0.1.0")]
[BepInDependency("Blasphemous.Framework.Penitence", "0.2.0")]
internal class Main : BaseUnityPlugin
{
    public static RandomPrayer RandomPrayer { get; private set; }

    private void Start()
    {
        RandomPrayer = new RandomPrayer();
    }
}
