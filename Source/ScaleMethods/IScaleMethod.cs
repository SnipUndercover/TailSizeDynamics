using Celeste.Mod.TailSizeDynamics.StatisticProviders;

namespace Celeste.Mod.TailSizeDynamics.ScaleMethods;

public interface IScaleMethod
{
    public float GetCurrentScale(IStatisticProvider provider);

    internal static void Initialize()
    {
        DisabledScaleMethod.CreateInstance();
        AdditiveScaleMethod.CreateInstance();
        AccumulativeScaleMethod.CreateInstance();
    }
}
