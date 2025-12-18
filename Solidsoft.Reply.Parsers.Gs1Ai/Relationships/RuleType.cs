namespace Solidsoft.Reply.Parsers.Gs1Ai.Relationships;

/// <summary>
/// The type of rule to apply when evaluating mandatory relationships.
/// </summary>
internal enum RuleType {
    /// <summary>
    /// Logical AND - all conditions must be met.
    /// </summary>
    And,

    /// <summary>
    /// Logical OR - at least one condition must be met.
    /// </summary>
    Or,

    /// <summary>
    /// Logical XOR - exactly one condition must be met.
    /// </summary>
    Xor
}