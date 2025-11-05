namespace Celeste.Mod.TailSizeDynamics.Enums;

/// <summary>
///   Determines the way the tail should change size.
/// </summary>
public enum TailScaleMethod
{
    /// <summary>
    ///   The tail size remains unchanged.
    /// </summary>
    Off,

    /// <summary>
    ///   The sum of statistics determines the final tail size.
    /// </summary>
    Additive,

    /// <summary>
    ///   The sum of statistics determines the rate by which the tail changes size per second.
    /// </summary>
    Accumulative,
}
