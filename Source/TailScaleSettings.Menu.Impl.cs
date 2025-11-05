using Celeste.Mod.TailSizeDynamics.Enums;
using Celeste.Mod.TailSizeDynamics.MenuItems;
using Celeste.Mod.TailSizeDynamics.StatisticProviders;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.TailSizeDynamics;

public partial class TailScaleSettings
{
    private static readonly Color WarningHighlightA = Calc.HexToColor("FF5959");
    private static readonly Color WarningHighlightB = Calc.HexToColor("FFA159");
    private const string DummyHeaderId = $"{nameof(TailScaleComponent)}_DummyHeader";

    private class StatisticsSubmenuImpl : TextMenuExt.SubMenu
    {
        private const string DialogId = TailScaleSettings.DialogRoot + "StatisticsSubmenu";
        // we want to override the parent DialogRoot
        // ReSharper disable once MemberHidesStaticFromOuterClass
        private const string DialogRoot = DialogId + "_";

        private readonly TailScaleSettings Settings;

        public StatisticsSubmenuImpl(TailScaleSettings settings)
            : base(GetLabel(settings), false)
        {
            Settings = settings;

            Add(new TextMenu.SubHeader(Dialog.Clean(DummyHeaderId), false));
        }

        public override void ConfirmPressed()
        {
            Clear();

            Label = GetLabel(Settings);

            IStatisticProvider provider = Settings.StatisticProvider;

            CreateHelpText();
            CreateCrystalHeartCountSlider(provider);
            CreateCassetteCountSlider(provider);
            CreateSummitGemCountSlider(provider);
            CreateStrawberryCountSlider(provider);
            CreateGoldenStrawberryCountSlider(provider);
            CreateWingedGoldenCountSlider(provider);
            CreateMoonberryCountSlider(provider);
            CreateDeathCountSlider(provider);
            CreateDashCountSlider(provider);
            CreateJumpCountSlider(provider);
            CreateTimeCountSlider(provider);
            CreateVisitedRoomCountSlider(provider);
            CreateMapCompletionCountSlider(provider);

            base.ConfirmPressed();
        }

        #region Help Text

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.SubHeader? HelpText;

        private void CreateHelpText()
        {
            const string DescriptionId = DialogId + DescriptionSuffix;
            Add(HelpText = new TextMenu.SubHeader(
                Dialog.Clean(DescriptionId),
                topPadding: false));
        }

        #endregion
        #region Crystal Hearts

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.Option<int>? CrystalHeartCount;

        private void CreateCrystalHeartCountSlider(IStatisticProvider provider)
        {
            CrystalHeartCount = AddStatistic(
                Dialog.Clean(DialogRoot + "CrystalHearts"),
                provider.TotalCrystalHearts,
                Settings.CrystalHeartScaleMultiplier);
        }

        #endregion
        #region Cassettes

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.Option<int>? CassetteCount;

        private void CreateCassetteCountSlider(IStatisticProvider provider)
        {
            CassetteCount = AddStatistic(
                Dialog.Clean(DialogRoot + "Cassettes"),
                provider.TotalCassettes,
                Settings.CassetteScaleMultiplier);
        }

        #endregion
        #region Summit Gems

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.Option<int>? SummitGemCount;

        private void CreateSummitGemCountSlider(IStatisticProvider provider)
        {
            SummitGemCount = AddStatistic(
                Dialog.Clean(DialogRoot + "SummitGems"),
                provider.TotalSummitGems,
                Settings.SummitGemScaleMultiplier);
        }

        #endregion
        #region Strawberries

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.Option<int>? StrawberryCount;

        private void CreateStrawberryCountSlider(IStatisticProvider provider)
        {
            StrawberryCount = AddStatistic(
                Dialog.Clean(DialogRoot + "Strawberries"),
                provider.TotalStrawberries,
                Settings.StrawberryScaleMultiplier);
        }

        #endregion
        #region Golden Strawberries

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.Option<int>? GoldenStrawberryCount;

        private void CreateGoldenStrawberryCountSlider(IStatisticProvider provider)
        {
            GoldenStrawberryCount = AddStatistic(
                Dialog.Clean(DialogRoot + "GoldenStrawberries"),
                provider.TotalGoldenStrawberries,
                Settings.GoldenStrawberryScaleMultiplier);
        }

        #endregion
        #region Winged Goldens

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.Option<int>? WingedGoldenCount;

        private void CreateWingedGoldenCountSlider(IStatisticProvider provider)
        {
            WingedGoldenCount = AddStatistic(
                Dialog.Clean(DialogRoot + "WingedGoldens"),
                provider.TotalWingedGoldens,
                Settings.WingedGoldenScaleMultiplier);
        }

        #endregion
        #region Moonberries

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.Option<int>? MoonberryCount;

        private void CreateMoonberryCountSlider(IStatisticProvider provider)
        {
            MoonberryCount = AddStatistic(
                Dialog.Clean(DialogRoot + "Moonberries"),
                provider.TotalMoonberries,
                Settings.MoonberryScaleMultiplier);
        }

        #endregion
        #region Deaths

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.Option<int>? DeathCount;

        private void CreateDeathCountSlider(IStatisticProvider provider)
        {
            DeathCount = AddStatistic(
                Dialog.Clean(DialogRoot + "Deaths"),
                provider.TotalDeaths,
                Settings.DeathScaleMultiplier);
        }

        #endregion
        #region Dashes

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.Option<int>? DashCount;

        private void CreateDashCountSlider(IStatisticProvider provider)
        {
            DashCount = AddStatistic(
                Dialog.Clean(DialogRoot + "Dashes"),
                provider.TotalDashes,
                Settings.DashScaleMultiplier);
        }

        #endregion
        #region Jumps

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.Option<int>? JumpCount;

        private void CreateJumpCountSlider(IStatisticProvider provider)
        {
            JumpCount = AddStatistic(
                Dialog.Clean(DialogRoot + "Jumps"),
                provider.TotalJumps,
                Settings.JumpScaleMultiplier);
        }

        #endregion
        #region Time

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.Option<int>? TimeCount;

        private void CreateTimeCountSlider(IStatisticProvider provider)
        {
            TimeCount = AddStatistic(
                Dialog.Clean(DialogRoot + "Time"),
                provider.TotalFrames / (int)Settings.TimeScaleUnit,
                Settings.TimeScaleMultiplier);
        }

        #endregion
        #region Visited Rooms

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.Option<int>? VisitedRoomCount;

        private void CreateVisitedRoomCountSlider(IStatisticProvider provider)
        {
            VisitedRoomCount = AddStatistic(
                Dialog.Clean(DialogRoot + "VisitedRooms"),
                provider.MapProgression.TotalVisitedRooms,
                Settings.MapProgressionScaleMultiplier);
        }

        #endregion
        #region Map Completions

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.Option<int>? MapCompletionCount;

        private void CreateMapCompletionCountSlider(IStatisticProvider provider)
        {
            MapCompletionCount = AddStatistic(
                Dialog.Clean(DialogRoot + "MapCompletions"),
                provider.MapProgression.TotalMapsCompleted,
                Settings.MapProgressionScaleMultiplier);
        }

        #endregion

        private TextMenu.Option<int> AddStatistic(string label, long statistic, float multiplier)
        {
            TextMenu.Option<int> slider = new TextMenu.Option<int>($"{label} ({multiplier}x)")
                .Add(FormatSizeContribution(statistic * multiplier), 0)
                .Add(statistic.ToString(), 1);
            Add(slider);
            return slider;
        }

        private TextMenu.Option<int> AddStatistic(string label, float statistic, float multiplier)
        {
            TextMenu.Option<int> slider = new TextMenu.Option<int>($"{label} ({multiplier}x)")
                .Add(FormatSizeContribution(statistic * multiplier), 0)
                .Add(FormatStatistic(statistic), 1);
            Add(slider);
            return slider;
        }

        private static string FormatStatistic(float value)
            => TailScaleModule.FormatDecimal(value);

        private static string FormatSizeContribution(float value)
            => TailScaleModule.FormatScale(value, withPlusSign: true);

        private static string GetLabel(TailScaleSettings settings)
        {
            string totalText = Dialog.Clean(DialogRoot + "Total");
            string scaleText = TailScaleModule.FormatScale(
                settings.ScaleMethodImpl.GetCurrentScale(settings.StatisticProvider) / BaseTailScale);
            // de-tails. heheheh.
            string detailsText = Dialog.Clean(DialogRoot + "Details");

            return $"{totalText}: {scaleText} | {detailsText}";
        }
    }

    private class ResetStatsSubmenuImpl : RecolorableSubmenu
    {
        private const string DialogId = TailScaleSettings.DialogRoot + "ResetStatisticsSubmenu";
        // we want to override the parent DialogRoot
        // ReSharper disable once MemberHidesStaticFromOuterClass
        private const string DialogRoot = DialogId + "_";

        private readonly TailScaleSettings Settings;
        private readonly bool InGame;

        private StatisticPersistenceMode StatisticPersistence;

        private bool ResetAllStats = true;

        private bool ResetCrystalHearts;
        private bool ResetCassettes;
        private bool ResetSummitGems;
        private bool ResetStrawberries;
        private bool ResetGoldenStrawberries;
        private bool ResetWingedGoldens;
        private bool ResetMoonberries;
        private bool ResetDeaths;
        private bool ResetDashes;
        private bool ResetJumps;
        private bool ResetTime;
        private bool ResetMapProgression;
        private bool ResetAccumulatedScale;

        public ResetStatsSubmenuImpl(TailScaleSettings settings, bool inGame)
            : base(Dialog.Clean(DialogId), enterOnSelect: false)
        {
            Settings = settings;
            InGame = inGame;

            Add(new TextMenu.SubHeader(DummyHeaderId, false));

            HighlightColorA = WarningHighlightA;
            HighlightColorB = WarningHighlightB;
        }

        // TODO: change highlighting colors?

        public override void ConfirmPressed()
        {
            StatisticPersistence = StatisticPersistence switch {
                < StatisticPersistenceMode.Session => StatisticPersistenceMode.Session,
                > StatisticPersistenceMode.SaveData => StatisticPersistenceMode.SaveData,
                StatisticPersistenceMode.Session when !InGame => StatisticPersistenceMode.SaveData,
                _ => Settings.StatisticPersistence,
            };

            ResetAllStats = true;
            ResetCrystalHearts = false;
            ResetCassettes = false;
            ResetSummitGems = false;
            ResetStrawberries = false;
            ResetGoldenStrawberries = false;
            ResetWingedGoldens = false;
            ResetMoonberries = false;
            ResetDeaths = false;
            ResetDashes = false;
            ResetJumps = false;
            ResetTime = false;
            ResetMapProgression = false;
            ResetAccumulatedScale = false;

            Clear();

            CreateScaleBehaviorStatsSlider();
            CreateResetAllStatsToggle();
            CreateResetCrystalHeartsToggle();
            CreateResetCassettesToggle();
            CreateResetSummitGemsToggle();
            CreateResetStrawberriesToggle();
            CreateResetGoldenStrawberriesToggle();
            CreateResetWingedGoldensToggle();
            CreateResetMoonberriesToggle();
            CreateResetDeathsToggle();
            CreateResetDashesToggle();
            CreateResetJumpsToggle();
            CreateResetTimeToggle();
            CreateResetMapProgressionToggle();
            CreateResetAccumulatedScaleToggle();
            CreateResetStatsButton();

            base.ConfirmPressed();
        }

        #region Scale Behavior Stats Slider

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.Option<StatisticPersistenceMode>? ScaleBehaviorStatsSlider;

        private void CreateScaleBehaviorStatsSlider()
        {
            const string EnumParentId = TailScaleSettings.DialogRoot + "StatisticPersistence" + EnumValueInfix;

            // @formatter:off
            Add(ScaleBehaviorStatsSlider = new TextMenu.Option<StatisticPersistenceMode>(
                Dialog.Clean(DialogRoot + "StatisticsToReset")));
            // @formatter:on

            ScaleBehaviorStatsSlider.Add(
                Dialog.Clean(EnumParentId + nameof(StatisticPersistenceMode.Session)),
                StatisticPersistenceMode.Session,
                StatisticPersistence == StatisticPersistenceMode.Session);
            ScaleBehaviorStatsSlider.Add(
                Dialog.Clean(EnumParentId + nameof(StatisticPersistenceMode.SaveData)),
                StatisticPersistenceMode.SaveData,
                StatisticPersistence == StatisticPersistenceMode.SaveData);

            ScaleBehaviorStatsSlider.Change(newValue => StatisticPersistence = newValue);
        }

        #endregion
        #region Reset All Stats

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.OnOff? ResetAllStatsToggle;

        private void CreateResetAllStatsToggle()
        {
            // @formatter:off
            Add(ResetAllStatsToggle = new TextMenu.OnOff(
                Dialog.Clean(DialogRoot + "All"),
                true));
            // @formatter:on
            ResetAllStatsToggle.Change(UpdateSliders);
        }

        #endregion
        #region Reset Crystal Hearts

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.OnOff? ResetCrystalHeartsToggle;

        private void CreateResetCrystalHeartsToggle()
        {
            // @formatter:off
            Add(ResetCrystalHeartsToggle = new TextMenu.OnOff(
                Dialog.Clean(DialogRoot + "CrystalHearts"),
                false));
            // @formatter:on
            ResetCrystalHeartsToggle.Change(newValue => ResetCrystalHearts = newValue);
            ResetCrystalHeartsToggle.Visible = false;
        }

        #endregion
        #region Reset Cassettes

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.OnOff? ResetCassettesToggle;

        private void CreateResetCassettesToggle()
        {
            // @formatter:off
            Add(ResetCassettesToggle = new TextMenu.OnOff(
                Dialog.Clean(DialogRoot + "Cassettes"),
                false));
            // @formatter:on
            ResetCassettesToggle.Change(newValue => ResetCassettes = newValue);
            ResetCassettesToggle.Visible = false;
        }

        #endregion
        #region Reset Summit Gems

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.OnOff? ResetSummitGemsToggle;

        private void CreateResetSummitGemsToggle()
        {
            // @formatter:off
            Add(ResetSummitGemsToggle = new TextMenu.OnOff(
                Dialog.Clean(DialogRoot + "SummitGems"),
                false));
            // @formatter:on
            ResetSummitGemsToggle.Change(newValue => ResetSummitGems = newValue);
            ResetSummitGemsToggle.Visible = false;
        }

        #endregion
        #region Reset Strawberries

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.OnOff? ResetStrawberriesToggle;

        private void CreateResetStrawberriesToggle()
        {
            // @formatter:off
            Add(ResetStrawberriesToggle = new TextMenu.OnOff(
                Dialog.Clean(DialogRoot + "Strawberries"),
                false));
            // @formatter:on
            ResetStrawberriesToggle.Change(newValue => ResetStrawberries = newValue);
            ResetStrawberriesToggle.Visible = false;
        }

        #endregion
        #region Reset Golden Strawberries

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.OnOff? ResetGoldenStrawberriesToggle;

        private void CreateResetGoldenStrawberriesToggle()
        {
            // @formatter:off
            Add(ResetGoldenStrawberriesToggle = new TextMenu.OnOff(
                Dialog.Clean(DialogRoot + "GoldenStrawberries"),
                false));
            // @formatter:on
            ResetGoldenStrawberriesToggle.Change(newValue => ResetGoldenStrawberries = newValue);
            ResetGoldenStrawberriesToggle.Visible = false;
        }

        #endregion
        #region Reset Winged Goldens

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.OnOff? ResetWingedGoldensToggle;

        private void CreateResetWingedGoldensToggle()
        {
            // @formatter:off
            Add(ResetWingedGoldensToggle = new TextMenu.OnOff(
                Dialog.Clean(DialogRoot + "Strawberries"),
                false));
            // @formatter:on
            ResetWingedGoldensToggle.Change(newValue => ResetWingedGoldens = newValue);
            ResetWingedGoldensToggle.Visible = false;
        }

        #endregion
        #region Reset Moonberries

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.OnOff? ResetMoonberriesToggle;

        private void CreateResetMoonberriesToggle()
        {
            // @formatter:off
            Add(ResetMoonberriesToggle = new TextMenu.OnOff(
                Dialog.Clean(DialogRoot + "Moonberries"),
                false));
            // @formatter:on
            ResetMoonberriesToggle.Change(newValue => ResetMoonberries = newValue);
            ResetMoonberriesToggle.Visible = false;
        }

        #endregion
        #region Reset Deaths

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.OnOff? ResetDeathsToggle;

        private void CreateResetDeathsToggle()
        {
            // @formatter:off
            Add(ResetDeathsToggle = new TextMenu.OnOff(
                Dialog.Clean(DialogRoot + "Deaths"),
                false));
            // @formatter:on
            ResetDeathsToggle.Change(newValue => ResetDeaths = newValue);
            ResetDeathsToggle.Visible = false;
        }

        #endregion
        #region Reset Dashes

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.OnOff? ResetDashesToggle;

        private void CreateResetDashesToggle()
        {
            // @formatter:off
            Add(ResetDashesToggle = new TextMenu.OnOff(
                Dialog.Clean(DialogRoot + "Dashes"),
                false));
            // @formatter:on
            ResetDashesToggle.Change(newValue => ResetDashes = newValue);
            ResetDashesToggle.Visible = false;
        }

        #endregion
        #region Reset Jumps

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.OnOff? ResetJumpsToggle;

        private void CreateResetJumpsToggle()
        {
            // @formatter:off
            Add(ResetJumpsToggle = new TextMenu.OnOff(
                Dialog.Clean(DialogRoot + "Jumps"),
                false));
            // @formatter:on
            ResetJumpsToggle.Change(newValue => ResetJumps = newValue);
            ResetJumpsToggle.Visible = false;
        }

        #endregion
        #region Reset Time

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.OnOff? ResetTimeToggle;

        private void CreateResetTimeToggle()
        {
            // @formatter:off
            Add(ResetTimeToggle = new TextMenu.OnOff(
                Dialog.Clean(DialogRoot + "Time"),
                false));
            // @formatter:on
            ResetTimeToggle.Change(newValue => ResetTime = newValue);
            ResetTimeToggle.Visible = false;
        }

        #endregion
        #region Reset Map Progression

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.OnOff? ResetMapProgressionToggle;

        private void CreateResetMapProgressionToggle()
        {
            // @formatter:off
            Add(ResetMapProgressionToggle = new TextMenu.OnOff(
                Dialog.Clean(DialogRoot + "MapProgression"),
                false));
            // @formatter:on
            ResetMapProgressionToggle.Change(newValue => ResetMapProgression = newValue);
            ResetMapProgressionToggle.Visible = false;
        }

        #endregion
        #region Reset Accumulated Scale

        // ReSharper disable once NotAccessedField.Local
        private TextMenu.OnOff? ResetAccumulatedScaleToggle;

        private void CreateResetAccumulatedScaleToggle()
        {
            // @formatter:off
            Add(ResetAccumulatedScaleToggle = new TextMenu.OnOff(
                Dialog.Clean(DialogRoot + "AccumulatedScale"),
                false));
            // @formatter:on
            ResetAccumulatedScaleToggle.Change(newValue => ResetAccumulatedScale = newValue);
            ResetAccumulatedScaleToggle.Visible = false;
        }

        #endregion
        #region Reset Stats Button

        // ReSharper disable once NotAccessedField.Local
        private RecolorableButton? ResetStatsButton;

        private void CreateResetStatsButton()
        {
            Add(ResetStatsButton = new RecolorableButton(Dialog.Clean(DialogRoot + "ResetStats")) {
                HighlightColorA = WarningHighlightA,
                HighlightColorB = WarningHighlightB,
            });
            ResetStatsButton.OnPressed = ResetStats;
        }

        #endregion

        private void UpdateSliders(bool resetAllStats)
        {
            if (ResetAllStats == resetAllStats)
                return;

            ResetAllStats = resetAllStats;

            bool visible = !resetAllStats;
            ResetCrystalHeartsToggle!.Visible = visible;
            ResetCassettesToggle!.Visible = visible;
            ResetSummitGemsToggle!.Visible = visible;
            ResetStrawberriesToggle!.Visible = visible;
            ResetGoldenStrawberriesToggle!.Visible = visible;
            ResetWingedGoldensToggle!.Visible = visible;
            ResetMoonberriesToggle!.Visible = visible;
            ResetDeathsToggle!.Visible = visible;
            ResetDashesToggle!.Visible = visible;
            ResetJumpsToggle!.Visible = visible;
            ResetTimeToggle!.Visible = visible;
            ResetMapProgressionToggle!.Visible = visible;
            ResetAccumulatedScaleToggle!.Visible = visible;
        }

        private void ResetStats()
        {
            IStatisticProvider statisticProvider = Settings.GetStatisticProvider(StatisticPersistence);

            statisticProvider.ResetStats(
                ResetAllStats || ResetCrystalHearts,
                ResetAllStats || ResetCassettes,
                ResetAllStats || ResetSummitGems,
                ResetAllStats || ResetStrawberries,
                ResetAllStats || ResetGoldenStrawberries,
                ResetAllStats || ResetWingedGoldens,
                ResetAllStats || ResetMoonberries,
                ResetAllStats || ResetDeaths,
                ResetAllStats || ResetDashes,
                ResetAllStats || ResetJumps,
                ResetAllStats || ResetTime,
                ResetAllStats || ResetMapProgression,
                ResetAllStats || ResetAccumulatedScale);

            Exit();
        }
    }
}
