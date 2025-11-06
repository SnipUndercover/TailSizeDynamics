using Celeste.Mod.TailSizeDynamics.Config;
using Celeste.Mod.TailSizeDynamics.Enums;
using Celeste.Mod.TailSizeDynamics.StatisticProviders;
using Monocle;

namespace Celeste.Mod.TailSizeDynamics.ScaleMethods;

public class AccumulativeScaleMethod : IUpdateableScaleMethod
{
    public static AccumulativeScaleMethod Instance { get; private set; } = null!;

    internal static void CreateInstance()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (Instance is not null)
            return;

        Instance = new AccumulativeScaleMethod();
    }

    public void UpdateScale(IStatisticProvider provider)
    {
        TailScaleEffectiveConfig config = TailScaleConfig.EffectiveConfig;

        float crystalHeartScale = provider.TotalCrystalHearts * config.CrystalHeartScaleMultiplier;
        float summitGemScale = provider.TotalSummitGems * config.SummitGemScaleMultiplier;
        float cassetteScale = provider.TotalCassettes * config.CassetteScaleMultiplier;
        float strawberryScale = provider.TotalStrawberries * config.StrawberryScaleMultiplier;
        float goldenStrawberryScale = provider.TotalGoldenStrawberries * config.GoldenStrawberryScaleMultiplier;
        float wingedGoldenScale = provider.TotalWingedGoldens * config.WingedGoldenScaleMultiplier;
        float moonberryScale = provider.TotalMoonberries * config.MoonberryScaleMultiplier;
        float deathScale = provider.TotalDeaths * config.DeathScaleMultiplier;
        float dashScale = provider.TotalDashes * config.DashScaleMultiplier;
        float jumpScale = provider.TotalJumps * config.JumpScaleMultiplier;
        float timeScale = provider.TotalFrames * (config.TimeScaleMultiplier / (int)config.TimeScaleUnit);
        float mapProgressionScale = config.MapProgressionScaleMode switch {
            MapProgressionMode.VisitedRooms =>
                provider.MapProgression.TotalVisitedRooms,
            MapProgressionMode.CompletedMaps =>
                provider.MapProgression.TotalMapsCompleted,
            _ => 0,
        } * config.MapProgressionScaleMultiplier;

        provider.AccumulatedScale += (crystalHeartScale + summitGemScale + cassetteScale + strawberryScale
            + goldenStrawberryScale + wingedGoldenScale + moonberryScale + deathScale + dashScale + jumpScale
            + timeScale + mapProgressionScale) * TailScaleConfig.BaseTailScale * Engine.DeltaTime;
    }
}
