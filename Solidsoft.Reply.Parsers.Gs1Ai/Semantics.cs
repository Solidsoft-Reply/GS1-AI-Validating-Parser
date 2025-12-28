namespace Solidsoft.Reply.Parsers.Gs1Ai;

using Solidsoft.Reply.Parsers.Gs1Ai.Relationships;

/// <summary>
/// Represents the semantics for GS1 Application Identifiers, including GTIN, expiry date, and amount payable semantics.
/// </summary>
#if NET5_0_OR_GREATER
/// <param name="GtinSemantics">The semantics for the Global Trade Item Number (GTIN).</param>
/// <param name="ExpiryDateSemantics">The semantics for the expiry date.</param>
/// <param name="AmountPayableSemantics">The semantics for the amount payable.</param>
public record struct Semantics(
    GtinSemantics GtinSemantics = GtinSemantics.General,
    ExpiryDateSemantics ExpiryDateSemantics = ExpiryDateSemantics.TradeItem,
    AmountPayableSemantics AmountPayableSemantics = AmountPayableSemantics.Invoice) {
    }
#else
public readonly struct Semantics() {
    /// <summary>
    /// Gets the semantics for the Global Trade Item Number (GTIN).
    /// </summary>
    public GtinSemantics GtinSemantics { get; } = GtinSemantics.General;

    /// <summary>
    /// Gets the semantics for the expiry date.
    /// </summary>
    public ExpiryDateSemantics ExpiryDateSemantics { get; } = ExpiryDateSemantics.TradeItem;

    /// <summary>
    /// Gets the semantics for the amount payable.
    /// </summary>
    public AmountPayableSemantics AmountPayableSemantics { get; } = AmountPayableSemantics.Invoice;
}
#endif