using System.Collections;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.TailSizeDynamics.StatisticProviders;

internal static class StatisticManager
{
    private static SessionStatisticProvider SessionStatistics => TailScaleModule.Session.Statistics;
    private static SaveDataStatisticProvider SaveDataStatistics => TailScaleModule.SaveData.Statistics;

    internal static void Load()
    {
        Everest.Events.LevelLoader.OnLoadingThread += CreateMapProgression;
        Everest.Events.Player.OnSpawn += AddSpawnRoomToVisitedRooms;
        Everest.Events.Level.OnTransitionTo += AddTransitionRoomToVisitedRooms;

        On.Celeste.HeartGem.Collect += AddHeartGem;
        On.Celeste.SummitGem.SmashRoutine += AddSummitGem;
        On.Celeste.Cassette.OnPlayer += AddCassette;
        On.Celeste.Strawberry.OnCollect += AddStrawberry;
        On.Celeste.SaveData.AddDeath += AddDeath;
        On.Celeste.Player.CallDashEvents += AddDash;
        On.Celeste.Player.Jump += AddJump;
        On.Celeste.Player.SuperJump += AddSuperJump;
        On.Celeste.Player.WallJump += AddWallJump;
        On.Celeste.Player.SuperWallJump += AddSuperWallJump;
        On.Celeste.Level.UpdateTime += AddFrame;
    }

    internal static void Unload()
    {
        Everest.Events.LevelLoader.OnLoadingThread -= CreateMapProgression;
        Everest.Events.Player.OnSpawn -= AddSpawnRoomToVisitedRooms;
        Everest.Events.Level.OnTransitionTo -= AddTransitionRoomToVisitedRooms;

        On.Celeste.HeartGem.Collect -= AddHeartGem;
        On.Celeste.SummitGem.SmashRoutine -= AddSummitGem;
        On.Celeste.Cassette.OnPlayer -= AddCassette;
        On.Celeste.Strawberry.OnCollect -= AddStrawberry;
        On.Celeste.SaveData.AddDeath -= AddDeath;
        On.Celeste.Player.CallDashEvents -= AddDash;
        On.Celeste.Player.Jump -= AddJump;
        On.Celeste.Player.SuperJump -= AddSuperJump;
        On.Celeste.Player.WallJump -= AddWallJump;
        On.Celeste.Player.SuperWallJump -= AddSuperWallJump;
        On.Celeste.Level.UpdateTime -= AddFrame;
    }

    private static void CreateMapProgression(Level level)
    {
        int roomCount = level.Session.MapData.Levels.Where(static room => room.Spawns.Count > 0).Count();

        SessionStatistics.MapProgression.Get(level.Session.Area).TotalRooms = roomCount;
        SaveDataStatistics.MapProgression.Get(level.Session.Area).TotalRooms = roomCount;
    }

    private static void AddSpawnRoomToVisitedRooms(Player player)
    {
        if (player.Scene is not Level level)
            return;

        SessionStatistics.MapProgression.Get(level.Session.Area).VisitedRooms.Add(level.Session.Level);
        SaveDataStatistics.MapProgression.Get(level.Session.Area).VisitedRooms.Add(level.Session.Level);
    }

    private static void AddTransitionRoomToVisitedRooms(Level level, LevelData next, Vector2 direction)
    {
        SessionStatistics.MapProgression.Get(level.Session.Area).VisitedRooms.Add(next.Name);
        SaveDataStatistics.MapProgression.Get(level.Session.Area).VisitedRooms.Add(next.Name);
    }

    private static void AddHeartGem(On.Celeste.HeartGem.orig_Collect orig, HeartGem self, Player player)
    {
        orig(self, player);

        SessionStatistics.HasCrystalHeart = true;
        SaveDataStatistics.TotalCrystalHearts++;
    }

    private static IEnumerator AddSummitGem(
        On.Celeste.SummitGem.orig_SmashRoutine orig,
        SummitGem self,
        Player player,
        Level level)
    {
        SessionStatistics.TotalSummitGems++;
        SaveDataStatistics.TotalSummitGems++;

        return orig(self, player, level);
    }

    private static void AddCassette(On.Celeste.Cassette.orig_OnPlayer orig, Cassette self, Player player)
    {
        orig(self, player);

        SessionStatistics.HasCassette = true;
        SaveDataStatistics.TotalCassettes++;
    }

    private static void AddStrawberry(On.Celeste.Strawberry.orig_OnCollect orig, Strawberry self)
    {
        orig(self);

        if (self.Golden)
        {
            if (self.Winged)
            {
                SessionStatistics.HasWingedGolden = true;
                SaveDataStatistics.TotalWingedGoldens++;
            }
            else
            {
                SessionStatistics.HasGoldenStrawberry = true;
                SaveDataStatistics.TotalGoldenStrawberries++;
            }
        }
        else if (self.Moon)
        {
            SessionStatistics.HasMoonberry = true;
            SaveDataStatistics.TotalMoonberries++;
        }
        else
        {
            SessionStatistics.TotalStrawberries++;
            SaveDataStatistics.TotalStrawberries++;
        }
    }

    private static void AddDeath(On.Celeste.SaveData.orig_AddDeath orig, SaveData self, AreaKey area)
    {
        orig(self, area);

        SessionStatistics.TotalDeaths++;
        SaveDataStatistics.TotalDeaths++;
    }

    private static void AddDash(On.Celeste.Player.orig_CallDashEvents orig, Player self)
    {
        if (!self.calledDashEvents)
        {
            SessionStatistics.TotalDashes++;
            SaveDataStatistics.TotalDashes++;
        }
        orig(self);
    }

    private static void AddJump(On.Celeste.Player.orig_Jump orig, Player self, bool particles, bool playSfx)
    {
        // also handles climbjumps
        orig(self, particles, playSfx);

        SessionStatistics.TotalJumps++;
        SaveDataStatistics.TotalJumps++;
    }

    private static void AddSuperJump(On.Celeste.Player.orig_SuperJump orig, Player self)
    {
        orig(self);

        SessionStatistics.TotalJumps++;
        SaveDataStatistics.TotalJumps++;
    }

    private static void AddWallJump(On.Celeste.Player.orig_WallJump orig, Player self, int dir)
    {
        orig(self, dir);

        SessionStatistics.TotalJumps++;
        SaveDataStatistics.TotalJumps++;
    }

    private static void AddSuperWallJump(On.Celeste.Player.orig_SuperWallJump orig, Player self, int dir)
    {
        orig(self, dir);

        SessionStatistics.TotalJumps++;
        SaveDataStatistics.TotalJumps++;
    }

    private static void AddFrame(On.Celeste.Level.orig_UpdateTime orig, Level self)
    {
        orig(self);

        if (self.Paused)
            return;

        SessionStatistics.TotalFrames++;
        SaveDataStatistics.TotalFrames++;
    }
}
