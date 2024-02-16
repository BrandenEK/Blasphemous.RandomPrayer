
namespace Blasphemous.RandomPrayer;

internal class PrayerConfig
{
    public bool OnlyShuffleOwnedPrayers { get; set; }
    public bool RemoveMirabras { get; set; }

    public PrayerConfig()
    {
        OnlyShuffleOwnedPrayers = false;
        RemoveMirabras = true;
    }
}
