namespace Celeste.Mod.TailSizeDynamics;

public partial class TailScaleSettings
{
    public enum TailScaleMethod
    {
        Off,
        Additive,
        Accumulative,
    }

    public enum StatisticPersistenceMode
    {
        Session,
        SaveData,
    }

    public enum MapProgressionMode
    {
        VisitedRooms,
        CompletedMaps,
    }

    public enum TimeUnit
    {
        Frames = 1,
        Seconds = 60,
    }

    public enum ResetMode
    {
        ReturnToMap,
        RestartChapter,
    }
}
