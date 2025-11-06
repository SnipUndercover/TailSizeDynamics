using Celeste.Mod.TailSizeDynamics.Enums;
using JetBrains.Annotations;

namespace Celeste.Mod.TailSizeDynamics.Config;

/// <summary>
///   An implementation of <see cref="TailScaleConfig"/> that provides default setting values.
/// </summary>
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature,
    Reason = "YamlDotNet requires a public parameterless constructor.")]
public sealed class TailScaleSettingsConfig() : TailScaleConfig(
    defaultScaleMethod: TailScaleMethod.Off,
    defaultStatisticPersistence: StatisticPersistenceMode.Session,
    defaultCrystalHeartScaleMultiplier: 2.0f,
    defaultCassetteScaleMultiplier: 1.0f,
    defaultSummitGemScaleMultiplier: 0.5f,
    defaultStrawberryScaleMultiplier: 0.2f,
    defaultGoldenStrawberryScaleMultiplier: 5.0f,
    defaultWingedGoldenScaleMultiplier: 5.0f,
    defaultMoonberryScaleMultiplier: 3.0f,
    defaultDeathScaleMultiplier: 0f,
    defaultDashScaleMultiplier: 0f,
    defaultJumpScaleMultiplier: 0f,
    defaultTimeScaleMultiplier: 0f,
    defaultTimeScaleUnit: TimeUnit.Seconds,
    defaultMapProgressionScaleMultiplier: 0.02f,
    defaultMapProgressionScaleMode: MapProgressionMode.VisitedRooms,
    defaultBaseScale: 1f,
    defaultMinigameMode: false,
    defaultMinigameResetMode: MinigameResetMode.RestartChapter);
