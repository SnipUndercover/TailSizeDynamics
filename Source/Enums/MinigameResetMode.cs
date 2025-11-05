namespace Celeste.Mod.TailSizeDynamics.Enums;

/// <summary>
///   How should the map be failed in Minigame Mode. (when the tail reaches 0x size while on ground)
/// </summary>
public enum MinigameResetMode
{
    /// <summary>
    ///   The player will return to map.
    /// </summary>
    ReturnToMap,

    /// <summary>
    ///   The player will restart the chapter.
    /// </summary>
    RestartChapter,
}
