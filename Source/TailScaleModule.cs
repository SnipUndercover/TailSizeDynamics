using System;
using System.Text;
using Celeste.Mod.Foxeline;
using Celeste.Mod.TailSizeDynamics.Config;
using Celeste.Mod.TailSizeDynamics.Enums;
using Celeste.Mod.TailSizeDynamics.ScaleMethods;
using Celeste.Mod.TailSizeDynamics.StatisticProviders;
using FMOD.Studio;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.TailSizeDynamics;

public class TailScaleModule : EverestModule
{
    // TODO: finish documentation

    [PublicAPI]
    public static TailScaleModule Instance { get; private set; } = null!;

    [PublicAPI]
    public static TailScaleSettings Settings => (TailScaleSettings)Instance._Settings;

    [PublicAPI]
    public static TailScaleSession Session => (TailScaleSession)Instance._Session;

    [PublicAPI]
    public static TailScaleSaveData SaveData => (TailScaleSaveData)Instance._SaveData;

    public override Type SettingsType => typeof(TailScaleSettings);
    public override Type SessionType => typeof(TailScaleSession);
    public override Type SaveDataType => typeof(TailScaleSaveData);

    internal static readonly Vector2 JustifyCenter = new(0.5f, 0.5f);
    internal static readonly Vector2 JustifyCenterLeft = new(0f, 0.5f);
    internal static readonly Vector2 JustifyCenterRight = new(1f, 0.5f);

    [UsedImplicitly(Reason = "Instantiated by Everest")]
    public TailScaleModule()
    {
        Instance = this;
    #if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(TailScaleModule), LogLevel.Verbose);
    #else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(TailScaleModule), LogLevel.Info);
    #endif
    }

    #region Hooks

    public override void Load()
    {
        IScaleMethod.Initialize();
        TailScaleComponent.Load();
        Everest.Events.Celeste.OnExiting += RestoreTailScaleOnExit;
        StatisticManager.Load();
    }

    public override void Unload()
    {
        RestoreTailScale(saveSettings: false);
        TailScaleComponent.Unload();
        Everest.Events.Celeste.OnExiting -= RestoreTailScaleOnExit;
        StatisticManager.Unload();
    }

    private static void RestoreTailScale(bool saveSettings)
    {
        FoxelineModule.Settings.TailScale = DisabledScaleMethod.Instance.DefaultScale;
        if (saveSettings)
            FoxelineModule.Instance.SaveSettings();
    }

    private static void RestoreTailScaleOnExit()
        => RestoreTailScale(saveSettings: true);

    #endregion

    // everest would normally use reflection a lot to instantiate mod options,
    // but i'd rather handle it myself; it'll be more performant than reflection too
    public override void CreateModMenuSection(TextMenu menu, bool inGame, EventInstance snapshot)
    {
        CreateModMenuSectionHeader(menu, inGame, snapshot);
        Settings.CreateModMenuSection(menu, inGame, snapshot);
    }

    /// <summary>
    ///   Formats a scale with <see cref="Config.TailScaleConfig.DigitsOfPrecision"/> decimal digits,
    ///   trimming trailing zeroes and appending an <c>x</c> symbol at the end.
    /// </summary>
    /// <param name="scale">
    ///   The value to format.
    /// </param>
    /// <param name="withPlusSign">
    ///   Whether to include a plus sign (<c>+</c>) in front if the value is positive.
    /// </param>
    [Pure]
    internal static string FormatScale(float scale, bool withPlusSign = false)
        => $"{FormatDecimal(scale, withPlusSign: withPlusSign)}x";

    /// <summary>
    ///   Formats a decimal value with <see cref="Config.TailScaleConfig.DigitsOfPrecision"/> decimal digits,
    ///   trimming trailing zeroes.
    /// </summary>
    /// <param name="value">
    ///   The value to format.
    /// </param>
    /// <param name="withPlusSign">
    ///   Whether to include a plus sign (<c>+</c>) in front if the value is positive.
    /// </param>
    [Pure]
    internal static string FormatDecimal(
        float value,
        bool withPlusSign = false)
    {
        StringBuilder result = new();
        if (withPlusSign && value >= 0)
            result.Append('+');
        result.Append(value.ToString(Config.TailScaleConfig.ScaleFormatSpecifier));

        for (int i = 0; i < Config.TailScaleConfig.DigitsOfPrecision; i++)
        {
            if (result[^(i + 1)] == '0')
                continue;

            return result.ToString(0, result.Length - i);
        }

        return result.ToString(0, result.Length - (Config.TailScaleConfig.DigitsOfPrecision + 1));
    }
}
