using BepInEx;

namespace Blasphemous.RandomPrayer;

[BepInPlugin(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_VERSION)]
[BepInDependency("Blasphemous.ModdingAPI", "0.1.0")]
public class Main : BaseUnityPlugin
{
    public static RandomPrayer RandomPrayer { get; private set; }

    private void Start()
    {
        RandomPrayer = new RandomPrayer();
    }
}
