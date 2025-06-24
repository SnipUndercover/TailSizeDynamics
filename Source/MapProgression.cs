using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using YamlDotNet.Serialization;

namespace Celeste.Mod.TailSizeDynamics;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature,
    Reason = "YamlDotNet requires a public parameterless constructor.")]
public class MapProgression
{
    [UsedImplicitly(Reason = "YamlDotNet requires that fields be public.")]
    public Dictionary<string, Data> ProgressionData = new();

    [YamlIgnore]
    public int TotalVisitedRooms
        => ProgressionData.Values.SelectMany(progression => progression.VisitedRooms).Count();

    [YamlIgnore]
    public float TotalMapsCompleted
        => ProgressionData.Values.Select(progression => progression.ProgressionPercentage).Sum();

    public void Clear()
        => ProgressionData.Clear();

    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature,
        Reason = "YamlDotNet requires a public parameterless constructor.")]
    public class Data
    {
        [UsedImplicitly(Reason = "YamlDotNet requires that fields be public.")]
        public HashSet<string> VisitedRooms = [];

        [UsedImplicitly(Reason = "YamlDotNet requires that fields be public.")]
        public int TotalRooms = 0;

        [YamlIgnore]
        public float ProgressionPercentage => TotalRooms > 0
            ? VisitedRooms.Count / (float)TotalRooms
            : 0;
    }

    public Data Get(AreaKey key)
    {
        string keyString = $"{key.Mode:D}:{key.SID}";

        return ProgressionData.TryGetValue(keyString, out Data? value)
            ? value
            : ProgressionData[keyString] = new Data();
    }
}
