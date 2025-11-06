using Celeste.Mod.TailSizeDynamics.Enums;
using JetBrains.Annotations;

namespace Celeste.Mod.TailSizeDynamics.Config;

/// <summary>
///   An implementation of <see cref="TailScaleConfig"/> that forces a <see cref="StatisticPersistenceMode.Session"/>
///   statistic persistence mode.
/// </summary>
public class TailScaleSessionConfig : TailScaleConfig
{
    public TailScaleSessionConfig(
        TailScaleMethod scaleMethod,
        float crystalHeartScaleMultiplier,
        float cassetteScaleMultiplier,
        float summitGemScaleMultiplier,
        float strawberryScaleMultiplier,
        float goldenStrawberryScaleMultiplier,
        float wingedGoldenScaleMultiplier,
        float moonberryScaleMultiplier,
        float deathScaleMultiplier,
        float dashScaleMultiplier,
        float jumpScaleMultiplier,
        float timeScaleMultiplier,
        TimeUnit timeScaleUnit,
        float mapProgressionScaleMultiplier,
        MapProgressionMode mapProgressionScaleMode,
        float baseScale,
        bool minigameMode,
        MinigameResetMode minigameResetMode)
    : base(
        scaleMethod,
        StatisticPersistenceMode.Session,
        crystalHeartScaleMultiplier,
        cassetteScaleMultiplier,
        summitGemScaleMultiplier,
        strawberryScaleMultiplier,
        goldenStrawberryScaleMultiplier,
        wingedGoldenScaleMultiplier,
        moonberryScaleMultiplier,
        deathScaleMultiplier,
        dashScaleMultiplier,
        jumpScaleMultiplier,
        timeScaleMultiplier,
        timeScaleUnit,
        mapProgressionScaleMultiplier,
        mapProgressionScaleMode,
        baseScale,
        minigameMode,
        minigameResetMode)
    {
    }


    [UsedImplicitly(Reason = "YamlDotNet requires a public parameterless constructor.")]
    public TailScaleSessionConfig()
    {
    }
}
