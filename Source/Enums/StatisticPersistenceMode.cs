namespace Celeste.Mod.TailSizeDynamics.Enums;

/// <summary>
///   Determines statistic persistence.
/// </summary>
public enum StatisticPersistenceMode
{
    /// <summary>
    ///   Use statistics recorded in the current map session.
    /// </summary>
    Session,

    /// <summary>
    ///   Use statistics recorded in the current save file.
    /// </summary>
    SaveData,
}
