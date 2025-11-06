using Celeste.Mod.TailSizeDynamics.Config;
using Celeste.Mod.TailSizeDynamics.StatisticProviders;

namespace Celeste.Mod.TailSizeDynamics.ScaleMethods;

public interface IUpdateableScaleMethod : IScaleMethod
{
    public void UpdateScale(IStatisticProvider provider);

    float IScaleMethod.GetCurrentScale(IStatisticProvider provider)
        => provider.AccumulatedScale + TailScaleConfig.EffectiveConfig.BaseScale;
}
