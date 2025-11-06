using Celeste.Mod.TailSizeDynamics.Config;
using Celeste.Mod.TailSizeDynamics.StatisticProviders;
using JetBrains.Annotations;

namespace Celeste.Mod.TailSizeDynamics;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature,
    Reason = "YamlDotNet requires a public parameterless constructor.")]
public class TailScaleSession : EverestModuleSession
{
    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public SessionStatisticProvider Statistics { get; set; } = new();

    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public TailScaleSessionConfig? Configuration { get; set; }
}
