using System;
using Celeste.Mod.Entities;
using Celeste.Mod.TailSizeDynamics.Config;
using Celeste.Mod.TailSizeDynamics.Enums;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.TailSizeDynamics.Triggers;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature,
    Reason = "Instantiated by Everest")]
[CustomEntity($"{nameof(TailSizeDynamics)}/{nameof(DisableTailSizeDynamicsTrigger)}")]
public class DisableTailSizeDynamicsTrigger : Trigger
{
    // TODO: support returning to previous config on death?

    public DisableTailSizeDynamicsTrigger(EntityData data, Vector2 offset) : base(data, offset)
    {
        ReadEntityData(data);
    }

    private static void ReadEntityData(EntityData data)
    {
        const string VersionKey = "_version";
        if (!data.Has(VersionKey))
            throw new InvalidOperationException($"Missing \"{VersionKey}\" in {nameof(EntityData)}.");

        int version = data.Int(VersionKey);
        switch (version) {
            case 1:
                // nop
                break;
            default:
                throw new NotSupportedException(
                    $"Version {version} is not supported. Maybe update {nameof(TailSizeDynamics)}?");
        };
    }

    public override void OnEnter(Player player)
    {
        base.OnEnter(player);
        TailScaleModule.Session.Configuration = null;
    }
}
