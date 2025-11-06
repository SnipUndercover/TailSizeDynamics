using System;
using Celeste.Mod.TailSizeDynamics.Enums;
using Celeste.Mod.TailSizeDynamics.ScaleMethods;
using Celeste.Mod.TailSizeDynamics.StatisticProviders;
using JetBrains.Annotations;

namespace Celeste.Mod.TailSizeDynamics.Config;

/// <summary>
///   An instance of TailSizeDynamics configuration.
///   User configuration (settings) and mapper configuration (session) are instances of <see cref="TailScaleConfig"/>.
///   <br/>
///   For effective configuration, see <see cref="EffectiveConfig"/>.
/// </summary>
public class TailScaleConfig
{
    /// <summary>
    ///   Digits of precision to use when calculating multipliers in <see cref="MenuItems.LogarithmicScale"/> and when
    ///   displaying floating-point values in the UI.
    /// </summary>
    public const int DigitsOfPrecision = 4;

    /// <summary>
    ///   The format specifier to format floats with <see cref="DigitsOfPrecision"/>.
    /// </summary>
    public static readonly string ScaleFormatSpecifier = $"F{DigitsOfPrecision}";

    /// <summary>
    ///  The base tail size in Foxeline.
    /// </summary>
    public const int BaseTailScale = 100;

    /// <summary>
    ///   The TailSizeDynamics settings as configured by the end user.
    /// </summary>
    [PublicAPI]
    public static TailScaleConfig SettingsConfig => TailScaleModule.Settings.Configuration;

    /// <summary>
    ///   The TailSizeDynamics settings as configured by the mapper, if any.
    ///   Takes precedence over <see cref="SettingsConfig"/> if not <c>null</c>.
    /// </summary>
    [PublicAPI]
    public static TailScaleConfig? SessionConfig => TailScaleModule.Session.Configuration;

    /// <summary>
    ///   The effective TailSizeDynamics configuration.
    /// </summary>
    [PublicAPI]
    public static TailScaleEffectiveConfig EffectiveConfig { get; private set; } = new();

    #region Constructors

    internal TailScaleConfig(
        TailScaleMethod defaultScaleMethod,
        StatisticPersistenceMode defaultStatisticPersistence,
        float defaultCrystalHeartScaleMultiplier,
        float defaultCassetteScaleMultiplier,
        float defaultSummitGemScaleMultiplier,
        float defaultStrawberryScaleMultiplier,
        float defaultGoldenStrawberryScaleMultiplier,
        float defaultWingedGoldenScaleMultiplier,
        float defaultMoonberryScaleMultiplier,
        float defaultDeathScaleMultiplier,
        float defaultDashScaleMultiplier,
        float defaultJumpScaleMultiplier,
        float defaultTimeScaleMultiplier,
        TimeUnit defaultTimeScaleUnit,
        float defaultMapProgressionScaleMultiplier,
        MapProgressionMode defaultMapProgressionScaleMode,
        float defaultBaseScale,
        bool defaultMinigameMode,
        MinigameResetMode defaultMinigameResetMode)
    {
        ScaleMethod = defaultScaleMethod;
        StatisticPersistence = defaultStatisticPersistence;
        CrystalHeartScaleMultiplier = defaultCrystalHeartScaleMultiplier;
        CassetteScaleMultiplier = defaultCassetteScaleMultiplier;
        SummitGemScaleMultiplier = defaultSummitGemScaleMultiplier;
        StrawberryScaleMultiplier = defaultStrawberryScaleMultiplier;
        GoldenStrawberryScaleMultiplier = defaultGoldenStrawberryScaleMultiplier;
        WingedGoldenScaleMultiplier = defaultWingedGoldenScaleMultiplier;
        MoonberryScaleMultiplier = defaultMoonberryScaleMultiplier;
        DeathScaleMultiplier = defaultDeathScaleMultiplier;
        DashScaleMultiplier = defaultDashScaleMultiplier;
        JumpScaleMultiplier = defaultJumpScaleMultiplier;
        TimeScaleMultiplier = defaultTimeScaleMultiplier;
        TimeScaleUnit = defaultTimeScaleUnit;
        MapProgressionScaleMultiplier = defaultMapProgressionScaleMultiplier;
        MapProgressionScaleMode = defaultMapProgressionScaleMode;
        BaseScale = defaultBaseScale;
        MinigameMode = defaultMinigameMode;
        MinigameResetMode = defaultMinigameResetMode;
    }

    [UsedImplicitly(Reason = "YamlDotNet requires a public parameterless constructor.")]
    public TailScaleConfig() : this(
        defaultScaleMethod: TailScaleMethod.Off,
        defaultStatisticPersistence: StatisticPersistenceMode.Session,
        defaultCrystalHeartScaleMultiplier: 0f,
        defaultCassetteScaleMultiplier: 0f,
        defaultSummitGemScaleMultiplier: 0f,
        defaultStrawberryScaleMultiplier: 0f,
        defaultGoldenStrawberryScaleMultiplier: 0f,
        defaultWingedGoldenScaleMultiplier: 0f,
        defaultMoonberryScaleMultiplier: 0f,
        defaultDeathScaleMultiplier: 0f,
        defaultDashScaleMultiplier: 0f,
        defaultJumpScaleMultiplier: 0f,
        defaultTimeScaleMultiplier: 0f,
        defaultTimeScaleUnit: TimeUnit.Seconds,
        defaultMapProgressionScaleMultiplier: 0f,
        defaultMapProgressionScaleMode: MapProgressionMode.VisitedRooms,
        defaultBaseScale: 1f,
        defaultMinigameMode: false,
        defaultMinigameResetMode: MinigameResetMode.RestartChapter)
    {
    }

    #endregion

    #region Properties

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public TailScaleMethod ScaleMethod { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public StatisticPersistenceMode StatisticPersistence { get; set; }

    #region Collectables

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float CrystalHeartScaleMultiplier { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float CassetteScaleMultiplier { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float SummitGemScaleMultiplier { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float StrawberryScaleMultiplier { get; set; }

    #region Special Strawberries

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float GoldenStrawberryScaleMultiplier { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float WingedGoldenScaleMultiplier { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float MoonberryScaleMultiplier { get; set; }

    #endregion

    #endregion

    #region Statistics

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float DeathScaleMultiplier { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float DashScaleMultiplier { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float JumpScaleMultiplier { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float TimeScaleMultiplier { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public TimeUnit TimeScaleUnit { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float MapProgressionScaleMultiplier { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public MapProgressionMode MapProgressionScaleMode { get; set; }

    #endregion

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float BaseScale { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public bool MinigameMode { get; set; }

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public MinigameResetMode MinigameResetMode { get; set; }

    #endregion

    public static IStatisticProvider GetStatisticProvider(StatisticPersistenceMode statisticPersistence)
        => statisticPersistence switch {
            StatisticPersistenceMode.Session => TailScaleModule.Session.Statistics,
            StatisticPersistenceMode.SaveData => TailScaleModule.SaveData.Statistics,
            _ => throw new ArgumentOutOfRangeException(
                nameof(statisticPersistence),
                statisticPersistence,
                $"{nameof(StatisticPersistenceMode)} value out of range: "),
        };


    public static IScaleMethod GetScaleMethod(TailScaleMethod scaleMethod)
        => scaleMethod switch {
            TailScaleMethod.Off => DisabledScaleMethod.Instance,
            TailScaleMethod.Additive => AdditiveScaleMethod.Instance,
            TailScaleMethod.Accumulative => AccumulativeScaleMethod.Instance,
            _ => DisabledScaleMethod.Instance,
        };

    public TailScaleConfig Clone(
        TailScaleMethod? overrideScaleMethod = null,
        StatisticPersistenceMode? overrideStatisticPersistence = null,
        float? overrideCrystalHeartScaleMultiplier = null,
        float? overrideCassetteScaleMultiplier = null,
        float? overrideSummitGemScaleMultiplier = null,
        float? overrideStrawberryScaleMultiplier = null,
        float? overrideGoldenStrawberryScaleMultiplier = null,
        float? overrideWingedGoldenScaleMultiplier = null,
        float? overrideMoonberryScaleMultiplier = null,
        float? overrideDeathScaleMultiplier = null,
        float? overrideDashScaleMultiplier = null,
        float? overrideJumpScaleMultiplier = null,
        float? overrideTimeScaleMultiplier = null,
        TimeUnit? overrideTimeScaleUnit = null,
        float? overrideMapProgressionScaleMultiplier = null,
        MapProgressionMode? overrideMapProgressionScaleMode = null,
        float? overrideBaseScale = null,
        bool? overrideMinigameMode = null,
        MinigameResetMode? overrideMinigameResetMode = null)
        => new(
            overrideScaleMethod ?? ScaleMethod,
            overrideStatisticPersistence ?? StatisticPersistence,
            overrideCrystalHeartScaleMultiplier ?? CrystalHeartScaleMultiplier,
            overrideCassetteScaleMultiplier ?? CassetteScaleMultiplier,
            overrideSummitGemScaleMultiplier ?? SummitGemScaleMultiplier,
            overrideStrawberryScaleMultiplier ?? StrawberryScaleMultiplier,
            overrideGoldenStrawberryScaleMultiplier ?? GoldenStrawberryScaleMultiplier,
            overrideWingedGoldenScaleMultiplier ?? WingedGoldenScaleMultiplier,
            overrideMoonberryScaleMultiplier ?? MoonberryScaleMultiplier,
            overrideDeathScaleMultiplier ?? DeathScaleMultiplier,
            overrideDashScaleMultiplier ?? DashScaleMultiplier,
            overrideJumpScaleMultiplier ?? JumpScaleMultiplier,
            overrideTimeScaleMultiplier ?? TimeScaleMultiplier,
            overrideTimeScaleUnit ?? TimeScaleUnit,
            overrideMapProgressionScaleMultiplier ?? MapProgressionScaleMultiplier,
            overrideMapProgressionScaleMode ?? MapProgressionScaleMode,
            overrideBaseScale ?? BaseScale,
            overrideMinigameMode ?? MinigameMode,
            overrideMinigameResetMode ?? MinigameResetMode);
}
