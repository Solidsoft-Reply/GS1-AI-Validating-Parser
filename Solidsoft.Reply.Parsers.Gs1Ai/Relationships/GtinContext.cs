namespace Solidsoft.Reply.Parsers.Gs1Ai.Relationships;

/// <summary>
/// The context in which to interpret the GTIN (AI 01).
/// </summary>
internal enum GtinContext {
    /// <summary>
    /// No context.
    /// </summary>
    None,

    /// <summary>
    /// A variable measure trade item scanned at POS.
    /// </summary>
    VariableMeasure,

    /// <summary>
    /// A custom trade item.
    /// </summary>
    Custom,
}