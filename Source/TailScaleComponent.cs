using System;
using System.Collections;
using Celeste.Mod.Foxeline;
using Celeste.Mod.TailSizeDynamics.Config;
using Celeste.Mod.TailSizeDynamics.Enums;
using Celeste.Mod.TailSizeDynamics.ScaleMethods;
using Celeste.Mod.TailSizeDynamics.StatisticProviders;
using Monocle;

namespace Celeste.Mod.TailSizeDynamics;

public class TailScaleComponent() : Component(active: true, visible: false)
{
    private const string LogId = $"{nameof(TailSizeDynamics)}/{nameof(TailScaleComponent)}";

    internal static void Load()
    {
        Everest.Events.Player.OnSpawn += Spawn;
    }

    internal static void Unload()
    {
        Everest.Events.Player.OnSpawn -= Spawn;
    }

    private static void Spawn(Player player)
    {
        if (player.Get<TailScaleComponent>() is null)
            player.Add(new TailScaleComponent());
    }


    private static TailScaleEffectiveConfig EffectiveConfig => TailScaleConfig.EffectiveConfig;

    private static IStatisticProvider StatisticProvider => EffectiveConfig.StatisticProvider;
    private static IScaleMethod ScaleMethod => EffectiveConfig.ScaleMethodImpl;

    public static float TargetScale => MathF.Max(ScaleMethod.GetCurrentScale(StatisticProvider), 0);

    public int CurrentScale = (int)TargetScale;

    public bool TailGone { get; private set; }

    public bool Disabled { get; private set; }

    public override void Update()
    {
        if (Entity is not Player player)
        {
            Logger.Warn(LogId, $"Attempted to add a {nameof(TailScaleComponent)} to a non-{nameof(Player)} entity.");
            RemoveSelf();
            return;
        }

        if (ScaleMethod is not DisabledScaleMethod)
            // we aren't disabled, so reset to false
            Disabled = false;
        else if (!Disabled)
            // we still have to scale down to the initial size, only then disable
            Disabled = CurrentScale == (int)TargetScale;

        if (Disabled)
            return;

        if (TailGone)
            return;

        (ScaleMethod as IUpdateableScaleMethod)?.UpdateScale(StatisticProvider);

        // try scaling based on delta
        int adjustment = (int)MathF.Max(MathF.Abs(TargetScale - CurrentScale) / 100, 1);
        CurrentScale = (int)Calc.Approach(CurrentScale, TargetScale, adjustment);
        FoxelineModule.Settings.TailScale = (int)MathF.Min(CurrentScale, int.MaxValue);

        if (!EffectiveConfig.MinigameMode)
            return;

        if (!(CurrentScale <= 0 && player.OnGround()))
            // wait till the scale is 0 and the player is on ground
            return;

        foreach (Follower follower in player.Leader.Followers)
        {
            if (follower.Entity is Strawberry { Golden: false } or Strawberry { Golden: true, Winged: true })
                // we have a regular berry or a winged golden to collect to pawsibly save ourselves
                return;
        }

        if (player.IsIntroState)
            // don't get sad until after the intro states
            return;

        TailGone = true;
        player.Add(new Coroutine(TailGoneRoutine(player)));
    }

    private IEnumerator TailGoneRoutine(Player player)
    {
        if (Scene is not Level level)
            throw new InvalidOperationException($"{nameof(Scene)} is not {nameof(Level)}!");

        level.PauseLock = true;
        player.StateMachine.State = Player.StDummy;
        player.DummyAutoAnimate = false;
        player.ForceCameraUpdate = true;
        player.Sprite.Play("sleep");

        // there's no animation id when the anim ends
        while (player.Sprite.CurrentAnimationID != "")
            yield return null;

        yield return 0.5f;

        SpotlightWipe.FocusPoint = player.Center - level.Camera.Position;
        _ = new SpotlightWipe(Scene, wipeIn: false, Reset) { Duration = 2f };
    }

    // taken from Celeste.UnlockEverythingThingy.UnlockEverything(Level)
    private void Reset()
        => Engine.Scene = new LevelExit(
            EffectiveConfig.MinigameResetMode switch {
                MinigameResetMode.ReturnToMap => LevelExit.Mode.GiveUp,
                MinigameResetMode.RestartChapter => LevelExit.Mode.Restart,
                _ => throw new InvalidOperationException(
                    $"Invalid {nameof(MinigameResetMode)} value: {EffectiveConfig.MinigameResetMode}"),
            },
            SceneAs<Level>().Session);
}
