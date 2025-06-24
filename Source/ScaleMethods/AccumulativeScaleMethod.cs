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
        TailScaleSettings settings = TailScaleModule.Settings;

        float crystalHeartScale = provider.TotalCrystalHearts * settings.CrystalHeartScaleMultiplier;
        float summitGemScale = provider.TotalSummitGems * settings.SummitGemScaleMultiplier;
        float cassetteScale = provider.TotalCassettes * settings.CassetteScaleMultiplier;
        float strawberryScale = provider.TotalStrawberries * settings.StrawberryScaleMultiplier;
        float goldenStrawberryScale = provider.TotalGoldenStrawberries * settings.GoldenStrawberryScaleMultiplier;
        float wingedGoldenScale = provider.TotalWingedGoldens * settings.WingedGoldenScaleMultiplier;
        float moonberryScale = provider.TotalMoonberries * settings.MoonberryScaleMultiplier;
        float deathScale = provider.TotalDeaths * settings.DeathScaleMultiplier;
        float dashScale = provider.TotalDashes * settings.DashScaleMultiplier;
        float jumpScale = provider.TotalJumps * settings.JumpScaleMultiplier;
        float timeScale = provider.TotalFrames * (settings.TimeScaleMultiplier / (int)settings.TimeScaleUnit);
        float mapProgressionScale = settings.MapProgressionScaleMode switch {
            TailScaleSettings.MapProgressionMode.VisitedRooms =>
                provider.MapProgression.TotalVisitedRooms,
            TailScaleSettings.MapProgressionMode.CompletedMaps =>
                provider.MapProgression.TotalMapsCompleted,
            _ => 0,
        } * settings.MapProgressionScaleMultiplier;

        provider.AccumulatedScale += (crystalHeartScale + summitGemScale + cassetteScale + strawberryScale
            + goldenStrawberryScale + wingedGoldenScale + moonberryScale + deathScale + dashScale + jumpScale
            + timeScale + mapProgressionScale) * TailScaleSettings.BaseTailScale * Engine.DeltaTime;
    }
}
