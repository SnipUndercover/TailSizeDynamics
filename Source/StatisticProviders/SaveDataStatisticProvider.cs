using JetBrains.Annotations;

namespace Celeste.Mod.TailSizeDynamics.StatisticProviders;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature,
    Reason = "YamlDotNet requires a public parameterless constructor.")]
public class SaveDataStatisticProvider : IStatisticProvider
{
    #region Collectables

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public int TotalCrystalHearts { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public int TotalCassettes { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public int TotalSummitGems { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public int TotalStrawberries { get; set; }

    #region Special Strawberries

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public int TotalGoldenStrawberries { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public int TotalWingedGoldens { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public int TotalMoonberries { get; set; }

    #endregion

    #endregion

    #region Statistics

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public int TotalDeaths { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public int TotalDashes { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public int TotalJumps { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public uint TotalFrames { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public MapProgression MapProgression { get; set; } = new();

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
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

        bool accumulatedScale)
    {
        if (crystalHearts)
            TotalCrystalHearts = 0;
        if (cassettes)
            TotalCassettes = 0;
        if (summitGems)
            TotalSummitGems = 0;

        if (strawberries)
            TotalStrawberries = 0;
        if (goldenStrawberries)
            TotalGoldenStrawberries = 0;
        if (wingedGoldens)
            TotalWingedGoldens = 0;
        if (moonberries)
            TotalMoonberries = 0;

        if (deaths)
            TotalDeaths = 0;
        if (dashes)
            TotalDashes = 0;
        if (jumps)
            TotalJumps = 0;
        if (time)
            TotalFrames = 0;
        if (mapProgression)
            MapProgression.Clear();

        if (accumulatedScale)
            AccumulatedScale = 0;
    }
}
