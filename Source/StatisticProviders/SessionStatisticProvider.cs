using JetBrains.Annotations;
using YamlDotNet.Serialization;

namespace Celeste.Mod.TailSizeDynamics.StatisticProviders;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature,
    Reason = "YamlDotNet requires a public parameterless constructor.")]
public class SessionStatisticProvider : IStatisticProvider
{
    #region Collectables

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public bool HasCrystalHeart { get; set; }

    [YamlIgnore]
    public int TotalCrystalHearts => HasCrystalHeart.ToInt();

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public bool HasCassette { get; set; }

    [YamlIgnore]
    public int TotalCassettes => HasCassette.ToInt();

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public int TotalSummitGems { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public int TotalStrawberries { get; set; }

    #region Special Strawberries

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public bool HasGoldenStrawberry { get; set; }

    [YamlIgnore]
    public int TotalGoldenStrawberries => HasGoldenStrawberry.ToInt();

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public bool HasWingedGolden { get; set; }

    [YamlIgnore]
    public int TotalWingedGoldens => HasWingedGolden.ToInt();

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public bool HasMoonberry { get; set; }

    [YamlIgnore]
    public int TotalMoonberries => HasMoonberry.ToInt();

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
            HasCrystalHeart = false;
        if (cassettes)
            HasCassette = false;
        if (summitGems)
            TotalSummitGems = 0;

        if (strawberries)
            TotalStrawberries = 0;
        if (goldenStrawberries)
            HasGoldenStrawberry = false;
        if (wingedGoldens)
            HasWingedGolden = false;
        if (moonberries)
            HasMoonberry = false;

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
