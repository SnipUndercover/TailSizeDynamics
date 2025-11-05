namespace Celeste.Mod.TailSizeDynamics.Enums;

/// <summary>
///   Determines the unit of time to be considered.
/// </summary>
public enum TimeUnit
{
    /// <summary>
    ///   The time spent in a map should be counted in frames.
    /// </summary>
    Frames = 1,

    /// <summary>
    ///   The time spent in a map should be counted in minutes.
    /// </summary>
    Seconds = 60,
}
