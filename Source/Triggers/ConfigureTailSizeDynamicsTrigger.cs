using System;
using Celeste.Mod.Entities;
using Celeste.Mod.TailSizeDynamics.Config;
using Celeste.Mod.TailSizeDynamics.Enums;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.TailSizeDynamics.Triggers;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature,
    Reason = "Instantiated by Everest")]
[CustomEntity($"{nameof(TailSizeDynamics)}/{nameof(ConfigureTailSizeDynamicsTrigger)}")]
public class ConfigureTailSizeDynamicsTrigger : Trigger
{
    // TODO: support returning to previous config on death?

    private readonly TailScaleSessionConfig Configuration;

    public ConfigureTailSizeDynamicsTrigger(EntityData data, Vector2 offset) : base(data, offset)
    {
        Configuration = ReadConfigFromEntityData(data);
    }

    private static TailScaleSessionConfig ReadConfigFromEntityData(EntityData data)
    {
        const string VersionKey = "_version";
        if (!data.Has(VersionKey))
            throw new InvalidOperationException($"Missing \"{VersionKey}\" in {nameof(EntityData)}.");

        int version = data.Int(VersionKey);
        return version switch {
            1 => ReadConfigFromEntityData_V1(data),
            _ => throw new NotSupportedException(
                $"Version {version} is not supported. Maybe update {nameof(TailSizeDynamics)}?"),
        };
    }

    /// <inheritdoc />
    public override void OnEnter(Player player)
    {
        base.OnEnter(player);
        TailScaleModule.Session.Configuration = Configuration;
    }

    #region Backwards Compatibility

    private static TailScaleSessionConfig ReadConfigFromEntityData_V1(EntityData data) => new(
        data.Enum("scaleMethod", TailScaleMethod.Off),
        data.Float("crystalHeartScaleMultiplier"),
        data.Float("cassetteScaleMultiplier"),
        data.Float("summitGemScaleMultiplier"),
        data.Float("strawberryScaleMultiplier"),
        data.Float("goldenStrawberryScaleMultiplier"),
        data.Float("wingedGoldenScaleMultiplier"),
        data.Float("moonberryScaleMultiplier"),
        data.Float("deathScaleMultiplier"),
        data.Float("dashScaleMultiplier"),
        data.Float("jumpScaleMultiplier"),
        data.Float("timeScaleMultiplier"),
        data.Enum("timeScaleUnit", TimeUnit.Seconds),
        data.Float("mapProgressionScaleMultiplier"),
        data.Enum("mapProgressionScaleMode", MapProgressionMode.VisitedRooms),
        data.Float("baseScale"),
        data.Bool("minigameMode"),
        data.Enum("minigameResetMode", MinigameResetMode.RestartChapter));

    #endregion
}
