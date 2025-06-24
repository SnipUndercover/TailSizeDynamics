using Celeste.Mod.Foxeline;
using Celeste.Mod.TailSizeDynamics.StatisticProviders;

namespace Celeste.Mod.TailSizeDynamics.ScaleMethods;

public class DisabledScaleMethod : IScaleMethod
{
    public static DisabledScaleMethod Instance { get; private set; } = null!;

    internal static void CreateInstance()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (Instance is not null)
            return;

        Instance = new DisabledScaleMethod(FoxelineModule.Settings.TailScale);
    }

    public readonly int DefaultScale;

    private DisabledScaleMethod(int defaultScale)
        => DefaultScale = defaultScale;

    public float GetCurrentScale(IStatisticProvider provider)
        => DefaultScale;
}
