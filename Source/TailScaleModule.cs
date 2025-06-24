using System;
using System.Collections;
using System.Linq;
using System.Text;
using Celeste.Mod.Foxeline;
using Celeste.Mod.TailSizeDynamics.ScaleMethods;
using Celeste.Mod.TailSizeDynamics.StatisticProviders;
using FMOD.Studio;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.TailSizeDynamics;

public class TailScaleModule : EverestModule
{
    // TODO: finish documentation

    // samah, forgive me for putting attributes on the same line as the properties...
    [PublicAPI] public static TailScaleModule Instance { get; private set; } = null!;
    [PublicAPI] public static TailScaleSettings Settings => (TailScaleSettings)Instance._Settings;
    [PublicAPI] public static SessionStatisticProvider Session => (SessionStatisticProvider)Instance._Session;
    [PublicAPI] public static SaveDataStatisticProvider SaveData => (SaveDataStatisticProvider)Instance._SaveData;

    public override Type SettingsType => typeof(TailScaleSettings);
    public override Type SessionType => typeof(SessionStatisticProvider);
    public override Type SaveDataType => typeof(SaveDataStatisticProvider);

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
        Everest.Events.Player.OnSpawn += Spawn;
        Everest.Events.Celeste.OnExiting += RestoreTailScaleOnExit;
        Everest.Events.LevelLoader.OnLoadingThread += CreateMapProgression;
        Everest.Events.Player.OnSpawn += AddSpawnRoomToVisitedRooms;
        Everest.Events.Level.OnTransitionTo += AddTransitionRoomToVisitedRooms;

        On.Celeste.HeartGem.Collect += AddHeartGem;
        On.Celeste.SummitGem.SmashRoutine += AddSummitGem;
        On.Celeste.Cassette.OnPlayer += AddCassette;
        On.Celeste.Strawberry.OnCollect += AddStrawberry;
        On.Celeste.SaveData.AddDeath += AddDeath;
        On.Celeste.Player.CallDashEvents += AddDash;
        On.Celeste.Player.Jump += AddJump;
        On.Celeste.Player.SuperJump += AddSuperJump;
        On.Celeste.Player.WallJump += AddWallJump;
        On.Celeste.Player.SuperWallJump += AddSuperWallJump;
        On.Celeste.Level.UpdateTime += AddFrame;
    }

    public override void Unload()
    {
        RestoreTailScale(saveSettings: false);
        Everest.Events.Player.OnSpawn -= Spawn;
        Everest.Events.Celeste.OnExiting -= RestoreTailScaleOnExit;
        Everest.Events.LevelLoader.OnLoadingThread -= CreateMapProgression;
        Everest.Events.Player.OnSpawn -= AddSpawnRoomToVisitedRooms;
        Everest.Events.Level.OnTransitionTo += AddTransitionRoomToVisitedRooms;

        On.Celeste.HeartGem.Collect -= AddHeartGem;
        On.Celeste.SummitGem.SmashRoutine -= AddSummitGem;
        On.Celeste.Cassette.OnPlayer -= AddCassette;
        On.Celeste.Strawberry.OnCollect -= AddStrawberry;
        On.Celeste.SaveData.AddDeath -= AddDeath;
        On.Celeste.Player.CallDashEvents -= AddDash;
        On.Celeste.Player.Jump -= AddJump;
        On.Celeste.Player.SuperJump -= AddSuperJump;
        On.Celeste.Player.WallJump -= AddWallJump;
        On.Celeste.Player.SuperWallJump -= AddSuperWallJump;
        On.Celeste.Level.UpdateTime -= AddFrame;
    }

    private static void RestoreTailScale(bool saveSettings)
    {
        FoxelineModule.Settings.TailScale = DisabledScaleMethod.Instance.DefaultScale;
        if (saveSettings)
            FoxelineModule.Instance.SaveSettings();
    }

    private static void RestoreTailScaleOnExit()
        => RestoreTailScale(saveSettings: true);

    private static void CreateMapProgression(Level level)
    {
        int roomCount = level.Session.MapData.Levels.Where(static room => room.Spawns.Count > 0).Count();

        Session.MapProgression.Get(level.Session.Area).TotalRooms = roomCount;
        SaveData.MapProgression.Get(level.Session.Area).TotalRooms = roomCount;
    }

    private static void AddSpawnRoomToVisitedRooms(Player player)
    {
        if (player.Scene is not Level level)
            return;

        Session.MapProgression.Get(level.Session.Area).VisitedRooms.Add(level.Session.Level);
        SaveData.MapProgression.Get(level.Session.Area).VisitedRooms.Add(level.Session.Level);
    }

    private static void AddTransitionRoomToVisitedRooms(Level level, LevelData next, Vector2 direction)
    {
       Session.MapProgression.Get(level.Session.Area).VisitedRooms.Add(next.Name);
       SaveData.MapProgression.Get(level.Session.Area).VisitedRooms.Add(next.Name);
    }

    private static void Spawn(Player player)
    {
        if (Settings.ScaleMethod != TailScaleSettings.TailScaleMethod.Off
            && player.Get<TailScaleComponent>() is null)
            player.Add(new TailScaleComponent());
    }

    private static void AddHeartGem(On.Celeste.HeartGem.orig_Collect orig, HeartGem self, Player player)
    {
        orig(self, player);

        Session.HasCrystalHeart = true;
        SaveData.TotalCrystalHearts++;
    }

    private static IEnumerator AddSummitGem(
        On.Celeste.SummitGem.orig_SmashRoutine orig,
        SummitGem self,
        Player player,
        Level level)
    {
        Session.TotalSummitGems++;
        SaveData.TotalSummitGems++;

        return orig(self, player, level);
    }

    private static void AddCassette(On.Celeste.Cassette.orig_OnPlayer orig, Cassette self, Player player)
    {
        orig(self, player);

        Session.HasCassette = true;
        SaveData.TotalCassettes++;
    }

    private static void AddStrawberry(On.Celeste.Strawberry.orig_OnCollect orig, Strawberry self)
    {
        orig(self);

        if (self.Golden)
        {
            if (self.Winged)
            {
                Session.HasWingedGolden = true;
                SaveData.TotalWingedGoldens++;
            }
            else
            {
                Session.HasGoldenStrawberry = true;
                SaveData.TotalGoldenStrawberries++;
            }
        }
        else if (self.Moon)
        {
            Session.HasMoonberry = true;
            SaveData.TotalMoonberries++;
        }
        else
        {
            Session.TotalStrawberries++;
            SaveData.TotalStrawberries++;
        }
    }

    private static void AddDeath(On.Celeste.SaveData.orig_AddDeath orig, SaveData self, AreaKey area)
    {
        orig(self, area);

        Session.TotalDeaths++;
        SaveData.TotalDeaths++;
    }

    private static void AddDash(On.Celeste.Player.orig_CallDashEvents orig, Player self)
    {
        if (!self.calledDashEvents)
        {
            Session.TotalDashes++;
            SaveData.TotalDashes++;
        }
        orig(self);
    }

    private static void AddJump(On.Celeste.Player.orig_Jump orig, Player self, bool particles, bool playSfx)
    {
        // also handles climbjumps
        orig(self, particles, playSfx);

        Session.TotalJumps++;
        SaveData.TotalJumps++;
    }

    private static void AddSuperJump(On.Celeste.Player.orig_SuperJump orig, Player self)
    {
        orig(self);

        Session.TotalJumps++;
        SaveData.TotalJumps++;
    }

    private static void AddWallJump(On.Celeste.Player.orig_WallJump orig, Player self, int dir)
    {
        orig(self, dir);

        Session.TotalJumps++;
        SaveData.TotalJumps++;
    }

    private static void AddSuperWallJump(On.Celeste.Player.orig_SuperWallJump orig, Player self, int dir)
    {
        orig(self, dir);

        Session.TotalJumps++;
        SaveData.TotalJumps++;
    }

    private static void AddFrame(On.Celeste.Level.orig_UpdateTime orig, Level self)
    {
        orig(self);

        if (self.Paused)
            return;

        Session.TotalFrames++;
        SaveData.TotalFrames++;
    }

    #endregion

    // everest would normally use reflection a lot to instantiate mod options,
    // but i'd rather handle it myself; it'll be more performant than reflection too
    public override void CreateModMenuSection(TextMenu menu, bool inGame, EventInstance snapshot)
    {
        CreateModMenuSectionHeader(menu, inGame, snapshot);
        Settings.CreateModMenuSection(menu, inGame, snapshot);
    }

    /// <summary>
    ///   Formats a scale with <see cref="TailScaleSettings.DigitsOfPrecision"/> decimal digits,
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
    ///   Formats a decimal value with <see cref="TailScaleSettings.DigitsOfPrecision"/> decimal digits,
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
        result.Append(value.ToString(TailScaleSettings.ScaleFormatSpecifier));

        for (int i = 0; i < TailScaleSettings.DigitsOfPrecision; i++)
        {
            if (result[^(i + 1)] == '0')
                continue;

            return result.ToString(0, result.Length - i);
        }

        return result.ToString(0, result.Length - (TailScaleSettings.DigitsOfPrecision + 1));
    }
}
