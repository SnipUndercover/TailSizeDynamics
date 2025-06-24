using System;
using Celeste.Mod.TailSizeDynamics.ScaleMethods;
using Celeste.Mod.TailSizeDynamics.StatisticProviders;
using JetBrains.Annotations;
using YamlDotNet.Serialization;

namespace Celeste.Mod.TailSizeDynamics;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature,
    Reason = "YamlDotNet requires a public parameterless constructor.")]
public partial class TailScaleSettings : EverestModuleSettings
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

    #region Properties

    // ReSharper disable RedundantDefaultMemberInitializer
    // i'd rather explicitly state the defaults

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public TailScaleMethod ScaleMethod { get; set; } = TailScaleMethod.Off;

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public StatisticPersistenceMode StatisticPersistence { get; set; } = StatisticPersistenceMode.Session;

    #region Collectables

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float CrystalHeartScaleMultiplier { get; set; } = 2.0f;

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float CassetteScaleMultiplier { get; set; } = 1.0f;

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float SummitGemScaleMultiplier { get; set; } = 0.5f;

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float StrawberryScaleMultiplier { get; set; } = 0.2f;

    #region Special Strawberries

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float GoldenStrawberryScaleMultiplier { get; set; } = 5.0f;

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float WingedGoldenScaleMultiplier { get; set; } = 5.0f;

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float MoonberryScaleMultiplier { get; set; } = 3.0f;

    #endregion

    #endregion

    #region Statistics

    public float DeathScaleMultiplier { get; set; } = 0f;

    public float DashScaleMultiplier { get; set; } = 0f;

    public float JumpScaleMultiplier { get; set; } = 0f;

    public float TimeScaleMultiplier { get; set; } = 0f;

    public TimeUnit TimeScaleUnit { get; set; } = TimeUnit.Seconds;

    public float MapProgressionScaleMultiplier { get; set; } = 0.02f;

    public MapProgressionMode MapProgressionScaleMode { get; set; } = MapProgressionMode.VisitedRooms;

    #endregion

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public float BaseScale { get; set; } = 1f;

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public bool MinigameMode { get; set; } = false;

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public ResetMode MinigameResetMode { get; set; } = ResetMode.RestartChapter;

    // ReSharper restore RedundantDefaultMemberInitializer

    #endregion

    [YamlIgnore]
    public IStatisticProvider StatisticProvider => GetStatisticProvider(StatisticPersistence);

    public IStatisticProvider GetStatisticProvider(StatisticPersistenceMode statisticPersistence)
        => statisticPersistence switch {
            StatisticPersistenceMode.Session => TailScaleModule.Session,
            StatisticPersistenceMode.SaveData => TailScaleModule.SaveData,
            _ => throw new ArgumentOutOfRangeException(
                nameof(statisticPersistence),
                statisticPersistence,
                $"{nameof(StatisticPersistenceMode)} value out of range: "),
        };

    [YamlIgnore]
    public IScaleMethod ScaleMethodImpl => GetScaleMethod(ScaleMethod);

    public IScaleMethod GetScaleMethod(TailScaleMethod scaleMethod)
        => scaleMethod switch {
            TailScaleMethod.Off => DisabledScaleMethod.Instance,
            TailScaleMethod.Additive => AdditiveScaleMethod.Instance,
            TailScaleMethod.Accumulative => AccumulativeScaleMethod.Instance,
            _ => DisabledScaleMethod.Instance,
        };
}
