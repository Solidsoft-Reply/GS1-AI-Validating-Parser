// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Semantics.cs" company="Solidsoft Reply Ltd">
// Copyright (c) 2018-2025 Solidsoft Reply Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <summary>
// Represents the semantics for GS1 Application Identifiers, including GTIN, expiry date, and amount payable semantics.
// </summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
    GtinSemantics GtinSemantics = GtinSemantics.General,
    ExpiryDateSemantics ExpiryDateSemantics = ExpiryDateSemantics.TradeItem,
    AmountPayableSemantics AmountPayableSemantics = AmountPayableSemantics.Invoice) {
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    }
#else
public readonly struct Semantics() {
    /// <summary>
    /// Initializes a new instance of the <see cref="Semantics"/> struct.
    /// </summary>
    /// <param name="gtinSemantics">The semantics for the Global Trade Item Number (GTIN).</param>
    /// <param name="expiryDateSemantics">The semantics for the expiry date.</param>
    /// <param name="amountPayableSemantics">The semantics for the amount payable.</param>
    public Semantics(
        GtinSemantics gtinSemantics,
        ExpiryDateSemantics expiryDateSemantics,
        AmountPayableSemantics amountPayableSemantics)
        : this() {
        GtinSemantics = gtinSemantics;
        ExpiryDateSemantics = expiryDateSemantics;
        AmountPayableSemantics = amountPayableSemantics;
    }

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