using Celeste.Mod.TailSizeDynamics.Enums;
using Celeste.Mod.TailSizeDynamics.ScaleMethods;
using Celeste.Mod.TailSizeDynamics.StatisticProviders;

namespace Celeste.Mod.TailSizeDynamics.Config;

/// <summary>
///   The effective TailSizeDynamics configuration, merging session and settings configuration.
/// </summary>
public class TailScaleEffectiveConfig
{
    internal TailScaleEffectiveConfig()
    {
    }

    #region Properties

    public TailScaleMethod ScaleMethod
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).ScaleMethod;

    public StatisticPersistenceMode StatisticPersistence
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).StatisticPersistence;

    #region Collectables

    public float CrystalHeartScaleMultiplier
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).CrystalHeartScaleMultiplier;

    public float CassetteScaleMultiplier
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).CassetteScaleMultiplier;

    public float SummitGemScaleMultiplier
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).SummitGemScaleMultiplier;

    public float StrawberryScaleMultiplier
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).StrawberryScaleMultiplier;

    #region Special Strawberries

    public float GoldenStrawberryScaleMultiplier
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).GoldenStrawberryScaleMultiplier;

    public float WingedGoldenScaleMultiplier
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).WingedGoldenScaleMultiplier;

    public float MoonberryScaleMultiplier
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).MoonberryScaleMultiplier;

    #endregion

    #endregion

    #region Statistics

    public float DeathScaleMultiplier
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).DeathScaleMultiplier;

    public float DashScaleMultiplier
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).DashScaleMultiplier;

    public float JumpScaleMultiplier
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).JumpScaleMultiplier;

    public float TimeScaleMultiplier
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).TimeScaleMultiplier;

    public TimeUnit TimeScaleUnit
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).TimeScaleUnit;

    public float MapProgressionScaleMultiplier
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).MapProgressionScaleMultiplier;

    public MapProgressionMode MapProgressionScaleMode
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).MapProgressionScaleMode;

    #endregion

    public float BaseScale
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).BaseScale;
    public bool MinigameMode
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).MinigameMode;

    public MinigameResetMode MinigameResetMode
        => (TailScaleConfig.SessionConfig ?? TailScaleConfig.SettingsConfig).MinigameResetMode;

    #endregion

    public IStatisticProvider StatisticProvider => TailScaleConfig.GetStatisticProvider(StatisticPersistence);

    public IScaleMethod ScaleMethodImpl => TailScaleConfig.GetScaleMethod(ScaleMethod);
}
