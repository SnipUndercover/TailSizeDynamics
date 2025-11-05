namespace Celeste.Mod.TailSizeDynamics.Enums;

/// <summary>
///   Determines which statistic to consider as map progression.
/// </summary>
public enum MapProgressionMode
{
    /// <summary>
    ///   The total number of visited rooms should be considered as map progression.
    /// </summary>
    VisitedRooms,

    /// <summary>
    ///   The total percentage of visited rooms per map should be considered as map progression.
    /// </summary>
    CompletedMaps,
}
