using Celeste.Mod.TailSizeDynamics.Enums;
using Celeste.Mod.TailSizeDynamics.MenuItems;
using FMOD.Studio;

namespace Celeste.Mod.TailSizeDynamics;

public partial class TailScaleSettings
{
    private const string DialogRoot = $"{nameof(TailSizeDynamics)}_{nameof(TailScaleSettings)}_";
    private const string DescriptionSuffix = "_Description";
    private const string EnumValueInfix = "_Value_";

    internal void CreateModMenuSection(TextMenu menu, bool inGame, EventInstance _)
    {
        CreateStatisticsSubmenu(menu, inGame);

        // configuration
        CreateTailScaleMethodSlider(menu);
        CreateStatisticPersistenceSlider(menu);
        CreateBaseScaleMultiplierSlider(menu);

        CreateCollectablesHeader(menu);
        CreateSignToggleHint(menu);

        CreateCrystalHeartScaleMultiplierSlider(menu);
        CreateCassetteScaleMultiplierSlider(menu);
        CreateSummitGemScaleMultiplierSlider(menu);
        CreateStrawberryScaleMultiplierSlider(menu);
        CreateSpecialStrawberrySubmenu(menu);

        CreateStatisticsHeader(menu);

        CreateDeathScaleMultiplierSlider(menu);
        CreateDashScaleMultiplierSlider(menu);
        CreateJumpScaleMultiplierSlider(menu);
        CreateTimeScaleMultiplierSlider(menu);
        CreateTimeScaleUnitSlider(menu);
        CreateMapProgressionScaleMultiplierSlider(menu);
        CreateMapProgressionScaleModeSlider(menu);

        CreateResetHeader(menu);

        // reset statistics
        CreateResetStatisticsSubmenu(menu, inGame);

        // minigame mode
        CreateMinigameModeHeader(menu);
        CreateMinigameModeToggle(menu);
        CreateMinigameResetModeSlider(menu);
    }

    #region Statistics Submenu

    // ReSharper disable once NotAccessedField.Local
    private StatisticsSubmenuImpl? StatisticsSubmenu;

    private void CreateStatisticsSubmenu(TextMenu menu, bool inGame)
    {
        if (inGame)
            menu.Add(StatisticsSubmenu = new StatisticsSubmenuImpl());
    }

    #endregion
    #region Tail Scale Method

    // ReSharper disable once NotAccessedField.Local
    private TextMenu.Option<TailScaleMethod>? ScaleMethodSlider;

    private void CreateTailScaleMethodSlider(TextMenu menu)
    {
        const string DialogId = DialogRoot + "TailScaleMethod";
        const string DescriptionId = DialogId + DescriptionSuffix;
        const string EnumParentId = DialogId + EnumValueInfix;

        // @formatter:off
        menu.Add(ScaleMethodSlider = new TextMenu.Option<TailScaleMethod>(
            Dialog.Clean(DialogId)));
        // @formatter:on

        ScaleMethodSlider.Add(
            Dialog.Clean(EnumParentId + nameof(TailScaleMethod.Off)),
            TailScaleMethod.Off,
            Configuration.ScaleMethod == TailScaleMethod.Off);
        ScaleMethodSlider.Add(
            Dialog.Clean(EnumParentId + nameof(TailScaleMethod.Additive)),
            TailScaleMethod.Additive,
            Configuration.ScaleMethod == TailScaleMethod.Additive);
        ScaleMethodSlider.Add(
            Dialog.Clean(EnumParentId + nameof(TailScaleMethod.Accumulative)),
            TailScaleMethod.Accumulative,
            Configuration.ScaleMethod == TailScaleMethod.Accumulative);

        ScaleMethodSlider.Change(newValue => Configuration.ScaleMethod = newValue);

        ScaleMethodSlider.AddDescription(menu, Dialog.Clean(DescriptionId + "4"));
        ScaleMethodSlider.AddDescription(menu, Dialog.Clean(DescriptionId + "3"));
        ScaleMethodSlider.AddDescription(menu, Dialog.Clean(DescriptionId + "2"));
        ScaleMethodSlider.AddDescription(menu, Dialog.Clean(DescriptionId + "1"));
        if (menu.Current == ScaleMethodSlider)
            ScaleMethodSlider.OnEnter?.Invoke();
    }

    #endregion
    #region Statistic Persistence

    // ReSharper disable once NotAccessedField.Local
    private TextMenu.Option<StatisticPersistenceMode>? StatisticPersistenceModeSlider;

    private void CreateStatisticPersistenceSlider(TextMenu menu)
    {
        const string DialogId = DialogRoot + "StatisticPersistence";
        const string DescriptionId = DialogId + DescriptionSuffix;
        const string EnumParentId = DialogId + EnumValueInfix;

        // @formatter:off
        menu.Add(StatisticPersistenceModeSlider = new TextMenu.Option<StatisticPersistenceMode>(
            Dialog.Clean(DialogId)));
        // @formatter:on

        StatisticPersistenceModeSlider.Add(
            Dialog.Clean(EnumParentId + nameof(StatisticPersistenceMode.Session)),
            StatisticPersistenceMode.Session,
            Configuration.StatisticPersistence == StatisticPersistenceMode.Session);
        StatisticPersistenceModeSlider.Add(
            Dialog.Clean(EnumParentId + nameof(StatisticPersistenceMode.SaveData)),
            StatisticPersistenceMode.SaveData,
            Configuration.StatisticPersistence == StatisticPersistenceMode.SaveData);

        StatisticPersistenceModeSlider.Change(newValue => Configuration.StatisticPersistence = newValue);

        StatisticPersistenceModeSlider.AddDescription(menu, Dialog.Clean(DescriptionId + "2"));
        StatisticPersistenceModeSlider.AddDescription(menu, Dialog.Clean(DescriptionId + "1"));
    }

    #endregion
    #region Base Scale

    // ReSharper disable once NotAccessedField.Local
    private LogarithmicScale? BaseScaleSlider;

    private void CreateBaseScaleMultiplierSlider(TextMenu menu)
    {
        // TODO: disable decimals for base scale slider
        // @formatter:off
        menu.Add(BaseScaleSlider = new LogarithmicScale(
            Dialog.Clean(DialogRoot + "BaseScale"),
            Configuration.BaseScale,
            allowNegatives: false));
        // @formatter:on
        BaseScaleSlider.OnValueChange +=
            newValue => Configuration.BaseScale = newValue;
    }

    #endregion
    #region Collectables Header

    // ReSharper disable once NotAccessedField.Local
    private TextMenu.SubHeader? CollectablesHeader;

    private void CreateCollectablesHeader(TextMenu menu)
    {
        // @formatter:off
        menu.Add(CollectablesHeader = new TextMenu.SubHeader(
            Dialog.Clean(DialogRoot + "CollectablesHeader"),
            topPadding: false));
        // @formatter:on
    }

    #endregion
    #region Sign Toggle Hint

    // ReSharper disable once NotAccessedField.Local
    private TextMenu.SubHeader? SignToggleHint;

    private void CreateSignToggleHint(TextMenu menu)
    {
        // @formatter:off
        menu.Add(SignToggleHint = new TextMenu.SubHeader(
            Dialog.Clean(DialogRoot + "SignToggleHint"),
            topPadding: false));
        // @formatter:on
    }

    #endregion
    #region Crystal Heart Scale

    // ReSharper disable once NotAccessedField.Local
    private LogarithmicScale? CrystalHeartScaleMultiplierSlider;

    private void CreateCrystalHeartScaleMultiplierSlider(TextMenu menu)
    {
        // @formatter:off
        menu.Add(CrystalHeartScaleMultiplierSlider = new LogarithmicScale(
            Dialog.Clean(DialogRoot + "CrystalHeartMult"),
            Configuration.CrystalHeartScaleMultiplier));
        // @formatter:on
        CrystalHeartScaleMultiplierSlider.OnValueChange +=
            newValue => Configuration.CrystalHeartScaleMultiplier = newValue;
    }

    #endregion
    #region Cassette Scale

    // ReSharper disable once NotAccessedField.Local
    private LogarithmicScale? CassetteScaleMultiplierSlider;

    private void CreateCassetteScaleMultiplierSlider(TextMenu menu)
    {
        // @formatter:off
        menu.Add(CassetteScaleMultiplierSlider = new LogarithmicScale(
            Dialog.Clean(DialogRoot + "CassetteMult"),
            Configuration.CassetteScaleMultiplier));
        // @formatter:on
        CassetteScaleMultiplierSlider.OnValueChange +=
            newValue => Configuration.CassetteScaleMultiplier = newValue;
    }

    #endregion
    #region Summit Gem Scale

    // ReSharper disable once NotAccessedField.Local
    private LogarithmicScale? SummitGemScaleMultiplierSlider;

    private void CreateSummitGemScaleMultiplierSlider(TextMenu menu)
    {
        // @formatter:off
        menu.Add(SummitGemScaleMultiplierSlider = new LogarithmicScale(
            Dialog.Clean(DialogRoot + "SummitGemMult"),
            Configuration.SummitGemScaleMultiplier));
        // @formatter:on
        SummitGemScaleMultiplierSlider.OnValueChange +=
            newValue => Configuration.SummitGemScaleMultiplier = newValue;
    }

    #endregion
    #region Strawberry Scale

    // ReSharper disable once NotAccessedField.Local
    private LogarithmicScale? StrawberryScaleMultiplierSlider;

    private void CreateStrawberryScaleMultiplierSlider(TextMenu menu)
    {
        // @formatter:off
        menu.Add(StrawberryScaleMultiplierSlider = new LogarithmicScale(
            Dialog.Clean(DialogRoot + "StrawberryMult"),
            Configuration.StrawberryScaleMultiplier));
        // @formatter:on
        StrawberryScaleMultiplierSlider.OnValueChange +=
            newValue => Configuration.StrawberryScaleMultiplier = newValue;
    }

    #endregion
    #region Special Strawberries Submenu

    // ReSharper disable once NotAccessedField.Local
    private TextMenuExt.SubMenu? SpecialStrawberriesSubmenu;

    private void CreateSpecialStrawberrySubmenu(TextMenu menu)
    {
        // @formatter:off
        SpecialStrawberriesSubmenu = new TextMenuExt.SubMenu(
            Dialog.Clean(DialogRoot + "SpecialStrawberriesSubmenu"),
            // enterOnSelect is buggy
            enterOnSelect: false);
        // @formatter:on

        CreateGoldenStrawberryScaleMultiplierSlider(SpecialStrawberriesSubmenu);
        CreateWingedGoldenScaleMultiplierSlider(SpecialStrawberriesSubmenu);
        CreateMoonberryScaleMultiplierSlider(SpecialStrawberriesSubmenu);

        menu.Add(SpecialStrawberriesSubmenu);
    }

    #endregion
    #region Golden Strawberry Scale

    // ReSharper disable once NotAccessedField.Local
    private LogarithmicScale? GoldenStrawberryScaleMultiplierSlider;

    private void CreateGoldenStrawberryScaleMultiplierSlider(TextMenuExt.SubMenu subMenu)
    {
        // @formatter:off
        subMenu.Add(GoldenStrawberryScaleMultiplierSlider = new LogarithmicScale(
            Dialog.Clean(DialogRoot + "GoldenStrawberryMult"),
            Configuration.GoldenStrawberryScaleMultiplier));
        // @formatter:on
        GoldenStrawberryScaleMultiplierSlider.OnValueChange +=
            newValue => Configuration.GoldenStrawberryScaleMultiplier = newValue;
    }

    #endregion
    #region Winged Golden Scale

    // ReSharper disable once NotAccessedField.Local
    private LogarithmicScale? WingedGoldenScaleMultiplierSlider;

    private void CreateWingedGoldenScaleMultiplierSlider(TextMenuExt.SubMenu subMenu)
    {
        // @formatter:off
        subMenu.Add(WingedGoldenScaleMultiplierSlider = new LogarithmicScale(
            Dialog.Clean(DialogRoot + "WingedGoldenMult"),
            Configuration.WingedGoldenScaleMultiplier));
        // @formatter:on
        WingedGoldenScaleMultiplierSlider.OnValueChange +=
            newValue => Configuration.WingedGoldenScaleMultiplier = newValue;
    }

    #endregion
    #region Moonberry Scale

    // ReSharper disable once NotAccessedField.Local
    private LogarithmicScale? MoonberryScaleMultiplierSlider;

    private void CreateMoonberryScaleMultiplierSlider(TextMenuExt.SubMenu subMenu)
    {
        // @formatter:off
        subMenu.Add(MoonberryScaleMultiplierSlider = new LogarithmicScale(
            Dialog.Clean(DialogRoot + "MoonberryMult"),
            Configuration.MoonberryScaleMultiplier));
        // @formatter:on
        MoonberryScaleMultiplierSlider.OnValueChange +=
            newValue => Configuration.MoonberryScaleMultiplier = newValue;
    }

    #endregion
    #region Statistics Header

    // ReSharper disable once NotAccessedField.Local
    private TextMenu.SubHeader? OtherStatisticsHeader;

    private void CreateStatisticsHeader(TextMenu menu)
    {
        // @formatter:off
        menu.Add(OtherStatisticsHeader = new TextMenu.SubHeader(
            Dialog.Clean(DialogRoot + "StatisticsHeader"),
            topPadding: false));
        // @formatter:on
    }

    #endregion
    #region Death Scale

    // ReSharper disable once NotAccessedField.Local
    private LogarithmicScale? DeathScaleMultiplierSlider;

    private void CreateDeathScaleMultiplierSlider(TextMenu menu)
    {
        // @formatter:off
        menu.Add(DeathScaleMultiplierSlider = new LogarithmicScale(
            Dialog.Clean(DialogRoot + "DeathMult"),
            Configuration.DeathScaleMultiplier));
        // @formatter:on
        DeathScaleMultiplierSlider.OnValueChange +=
            newValue => Configuration.DeathScaleMultiplier = newValue;
    }

    #endregion
    #region Dash Scale

    // ReSharper disable once NotAccessedField.Local
    private LogarithmicScale? DashScaleMultiplierSlider;

    private void CreateDashScaleMultiplierSlider(TextMenu menu)
    {
        // @formatter:off
        menu.Add(DashScaleMultiplierSlider = new LogarithmicScale(
            Dialog.Clean(DialogRoot + "DashMult"),
            Configuration.DashScaleMultiplier));
        // @formatter:on
        DashScaleMultiplierSlider.OnValueChange +=
            newValue => Configuration.DashScaleMultiplier = newValue;
    }

    #endregion
    #region Jump Scale

    // ReSharper disable once NotAccessedField.Local
    private LogarithmicScale? JumpScaleMultiplierSlider;

    private void CreateJumpScaleMultiplierSlider(TextMenu menu)
    {
        // @formatter:off
        menu.Add(JumpScaleMultiplierSlider = new LogarithmicScale(
            Dialog.Clean(DialogRoot + "JumpMult"),
            Configuration.JumpScaleMultiplier));
        // @formatter:on
        JumpScaleMultiplierSlider.OnValueChange +=
            newValue => Configuration.JumpScaleMultiplier = newValue;
    }

    #endregion
    #region Time Scale

    // ReSharper disable once NotAccessedField.Local
    private LogarithmicScale? TimeScaleMultiplierSlider;

    private void CreateTimeScaleMultiplierSlider(TextMenu menu)
    {
        // @formatter:off
        menu.Add(TimeScaleMultiplierSlider = new LogarithmicScale(
            Dialog.Clean(DialogRoot + "TimeMult"),
            Configuration.TimeScaleMultiplier));
        // @formatter:on
        TimeScaleMultiplierSlider.OnValueChange +=
            newValue => Configuration.TimeScaleMultiplier = newValue;
    }

    #endregion
    #region Time Scale Unit

    // ReSharper disable once NotAccessedField.Local
    private TextMenu.Option<TimeUnit>? TimeScaleUnitSlider;

    private void CreateTimeScaleUnitSlider(TextMenu menu)
    {
        const string DialogId = DialogRoot + "TimeUnit";
        const string EnumParentId = DialogId + EnumValueInfix;

        // @formatter:off
        menu.Add(TimeScaleUnitSlider = new TextMenu.Option<TimeUnit>(
            Dialog.Clean(DialogId)));
        // @formatter:on

        TimeScaleUnitSlider.Add(
            Dialog.Clean(EnumParentId + nameof(TimeUnit.Frames)),
            TimeUnit.Frames,
            Configuration.TimeScaleUnit == TimeUnit.Frames);
        TimeScaleUnitSlider.Add(
            Dialog.Clean(EnumParentId + nameof(TimeUnit.Seconds)),
            TimeUnit.Seconds,
            Configuration.TimeScaleUnit == TimeUnit.Seconds);

        TimeScaleUnitSlider.Change(newValue => Configuration.TimeScaleUnit = newValue);
    }

    #endregion
    #region Map Progression Scale

    // ReSharper disable once NotAccessedField.Local
    private LogarithmicScale? MapProgressionScaleMultiplierSlider;

    private void CreateMapProgressionScaleMultiplierSlider(TextMenu menu)
    {
        // @formatter:off
        menu.Add(MapProgressionScaleMultiplierSlider = new LogarithmicScale(
            Dialog.Clean(DialogRoot + "MapProgressionMult"),
            Configuration.MapProgressionScaleMultiplier));
        // @formatter:on
        MapProgressionScaleMultiplierSlider.OnValueChange +=
            newValue => Configuration.MapProgressionScaleMultiplier = newValue;
    }

    #endregion
    #region Map Progression Mode

    // ReSharper disable once NotAccessedField.Local
    private TextMenu.Option<MapProgressionMode>? MapProgressionScaleModeSlider;

    private void CreateMapProgressionScaleModeSlider(TextMenu menu)
    {
        const string DialogId = DialogRoot + "MapProgressionMode";
        const string EnumParentId = DialogId + EnumValueInfix;

        // @formatter:off
        menu.Add(MapProgressionScaleModeSlider = new TextMenu.Option<MapProgressionMode>(
            Dialog.Clean(DialogId)));
        // @formatter:on

        MapProgressionScaleModeSlider.Add(
            Dialog.Clean(EnumParentId + nameof(MapProgressionMode.VisitedRooms)),
            MapProgressionMode.VisitedRooms,
            Configuration.MapProgressionScaleMode == MapProgressionMode.VisitedRooms);
        MapProgressionScaleModeSlider.Add(
            Dialog.Clean(EnumParentId + nameof(MapProgressionMode.CompletedMaps)),
            MapProgressionMode.CompletedMaps,
            Configuration.MapProgressionScaleMode == MapProgressionMode.CompletedMaps);

        MapProgressionScaleModeSlider.Change(newValue => Configuration.MapProgressionScaleMode = newValue);
    }

    #endregion
    #region Reset Header

    // ReSharper disable once NotAccessedField.Local
    private TextMenu.SubHeader? ResetHeader;

    private void CreateResetHeader(TextMenu menu)
    {
        // @formatter:off
        menu.Add(ResetHeader = new TextMenu.SubHeader(
            Dialog.Clean(DialogRoot + "ResetStatisticsHeader"),
            topPadding: false));
        // @formatter:on
    }

    #endregion
    #region Reset Statistics Submenu

    // ReSharper disable once NotAccessedField.Local
    private ResetStatsSubmenuImpl? ResetStatsSubmenu;

    private void CreateResetStatisticsSubmenu(TextMenu menu, bool inGame)
    {
        menu.Add(ResetStatsSubmenu = new ResetStatsSubmenuImpl(inGame));
        ResetStatsSubmenu.Disabled = !inGame;
    }

    #endregion
    #region Minigame Mode Header

    // ReSharper disable once NotAccessedField.Local
    private TextMenu.SubHeader? MinigameModeHeader;

    private void CreateMinigameModeHeader(TextMenu menu)
    {
        // @formatter:off
        menu.Add(MinigameModeHeader = new TextMenu.SubHeader(
            Dialog.Clean(DialogRoot + "MinigameModeHeader"),
            topPadding: false));
        // @formatter:on
    }

    #endregion
    #region Minigame Mode

    // ReSharper disable once NotAccessedField.Local
    private TextMenu.OnOff? MinigameModeToggle;

    private void CreateMinigameModeToggle(TextMenu menu)
    {
        const string DialogId = DialogRoot + "MinigameMode";
        const string DescriptionId = DialogId + DescriptionSuffix;

        // @formatter:off
        menu.Add(MinigameModeToggle = new TextMenu.OnOff(
            Dialog.Clean(DialogId),
            Configuration.MinigameMode));
        // @formatter:on
        MinigameModeToggle.AddDescription(menu, Dialog.Clean(DescriptionId));

        MinigameModeToggle.Change(newValue => Configuration.MinigameMode = newValue);
    }

    #endregion
    #region Minigame Reset Mode

    private TextMenu.Option<MinigameResetMode>? MinigameResetModeToggle;

    private void CreateMinigameResetModeSlider(TextMenu menu)
    {
        const string DialogId = DialogRoot + "MinigameResetMode";
        const string EnumParentId = DialogId + EnumValueInfix;

        // @formatter:off
        menu.Add(MinigameResetModeToggle = new TextMenu.Option<MinigameResetMode>(
            Dialog.Clean(DialogId)));
        // @formatter:on

        MinigameResetModeToggle.Add(
            Dialog.Clean(EnumParentId + nameof(MinigameResetMode.ReturnToMap)),
            MinigameResetMode.ReturnToMap,
            Configuration.MinigameResetMode == MinigameResetMode.ReturnToMap);
        MinigameResetModeToggle.Add(
            Dialog.Clean(EnumParentId + nameof(MinigameResetMode.RestartChapter)),
            MinigameResetMode.RestartChapter,
            Configuration.MinigameResetMode == MinigameResetMode.RestartChapter);

        MinigameResetModeToggle.Change(newValue => Configuration.MinigameResetMode = newValue);
    }

    #endregion
}
