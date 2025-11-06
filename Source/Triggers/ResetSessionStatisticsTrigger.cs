using System;
using Celeste.Mod.Entities;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.TailSizeDynamics.Triggers;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature,
    Reason = "Instantiated by Everest")]
[CustomEntity($"{nameof(TailSizeDynamics)}/{nameof(ResetSessionStatisticsTrigger)}")]
public class ResetSessionStatisticsTrigger : Trigger
{
    private readonly bool ResetCrystalHearts;
    private readonly bool ResetCassettes;
    private readonly bool ResetSummitGems;
    private readonly bool ResetStrawberries;
    private readonly bool ResetGoldenStrawberries;
    private readonly bool ResetWingedGoldens;
    private readonly bool ResetMoonberries;
    private readonly bool ResetDeaths;
    private readonly bool ResetDashes;
    private readonly bool ResetJumps;
    private readonly bool ResetTime;
    private readonly bool ResetMapProgression;
    private readonly bool ResetAccumulatedScale;

    public ResetSessionStatisticsTrigger(EntityData data, Vector2 offset) : base(data, offset)
    {
        const string VersionKey = "_version";
        if (!data.Has(VersionKey))
            throw new InvalidOperationException($"Missing \"{VersionKey}\" in {nameof(EntityData)}.");

        // TODO: make version check somehow
        int version = data.Int(VersionKey);

        ResetCrystalHearts = data.Bool("crystalHearts");
        ResetCassettes = data.Bool("cassettes");
        ResetSummitGems = data.Bool("summitGems");
        ResetStrawberries = data.Bool("strawberries");
        ResetGoldenStrawberries = data.Bool("goldenStrawberries");
        ResetWingedGoldens = data.Bool("wingedGoldens");
        ResetMoonberries = data.Bool("moonberries");
        ResetDeaths = data.Bool("deaths");
        ResetDashes = data.Bool("dashes");
        ResetJumps = data.Bool("jumps");
        ResetTime = data.Bool("time");
        ResetMapProgression = data.Bool("mapProgression");
        ResetAccumulatedScale = data.Bool("accumulatedScale");
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
        }
    }

    public override void OnEnter(Player player)
    {
        base.OnEnter(player);
        TailScaleModule.Session.Statistics.ResetStats(
            ResetCrystalHearts,
            ResetCassettes,
            ResetSummitGems,
            ResetStrawberries,
            ResetGoldenStrawberries,
            ResetWingedGoldens,
            ResetMoonberries,
            ResetDeaths,
            ResetDashes,
            ResetJumps,
            ResetTime,
            ResetMapProgression,
            ResetAccumulatedScale);
    }
    
}
