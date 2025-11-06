using Celeste.Mod.TailSizeDynamics.StatisticProviders;
using JetBrains.Annotations;

namespace Celeste.Mod.TailSizeDynamics;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature,
    Reason = "YamlDotNet requires a public parameterless constructor.")]
public class TailScaleSaveData : EverestModuleSaveData
{
    [UsedImplicitly(Reason = "YamlDotNet requires that accessors be public.")]
    public SaveDataStatisticProvider Statistics { get; set; } = new();
}
