namespace Celeste.Mod.TailSizeDynamics.StatisticProviders;

public interface IStatisticProvider
{
    #region Collectables

    public int TotalCrystalHearts { get; }
    public int TotalCassettes { get; }
    public int TotalSummitGems { get; }
    public int TotalStrawberries { get; }

    #region Special Strawberries

    public int TotalGoldenStrawberries { get; }
    public int TotalWingedGoldens { get; }
    public int TotalMoonberries { get; }

    #endregion

    #endregion

    #region Statistics

    public int TotalDeaths { get; }
    public int TotalDashes { get; }
    public int TotalJumps { get; }
    public uint TotalFrames { get; }

    public MapProgression MapProgression { get; }

    public float AccumulatedScale { get; set; }

    #endregion

    public void ResetStats(
        bool crystalHearts,
        bool cassettes,
        bool summitGems,

        bool strawberries,
        bool goldenStrawberries,
        bool wingedGoldens,
        bool moonberries,

        bool deaths,
        bool dashes,
        bool jumps,
        bool time,
        bool mapProgression,

        bool accumulatedScale);

    public void ResetAllStats() => ResetStats(
        crystalHearts: true,
        cassettes: true,
        summitGems: true,

        strawberries: true,
        goldenStrawberries: true,
        wingedGoldens: true,
        moonberries: true,

        deaths: true,
        dashes: true,
        jumps: true,
        time: true,
        mapProgression: true,

        accumulatedScale: true);
}
