// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Parser.cs" company="Solidsoft Reply Ltd">
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
//  Parser for element strings encoded in any GS1 symbology that uses GS1 Application Identifiers, such as GS1-128,
// GS1 DataMatrix, GS1 DataBar, GS1 QR Code and GS1 Composite.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

[assembly: CLSCompliant(true)]

namespace Solidsoft.Reply.Parsers.Gs1Ai;

using Common;

using Properties;

using Solidsoft.Reply.Parsers.Gs1Ai.Relationships;

using System;
using System.Collections.Generic;

#if NET7_0_OR_GREATER
/// <summary>
/// Delegate for processing element data elements with minimal heap allocations.
/// </summary>
/// <param name="resolvedElement">The element element to process.</param>
public delegate void ResolvedElementDelegate(scoped in ResolvedApplicationIdentifierRef resolvedElement);
#endif

/// <summary>
/// Provides static methods for parsing GS1-encoded strings and processing element application identifiers (AIs)
/// according to GS1 standards.
/// </summary>
/// <remarks>The Parser class offers multiple overloads of the Parse method to support different input types and
/// performance requirements. It applies GS1 data relationship rules when requested, enabling validation and
/// relationship testing of AIs within a single physical element. For high-performance scenarios, use the ParseEx method
/// with a delegate to minimize heap allocations. All parsing methods invoke a callback for each element element,
/// allowing callers to process or validate parsed data as needed. GS1 data relationship rules are only applied if
/// explicitly requested via the appropriate parameter. Applying these rules may introduce additional overhead and is
/// not required for all use cases. The class is thread-safe for concurrent parsing operations.</remarks>
public static class Parser {
#if !NET6_0_OR_GREATER
    /// <summary>
    ///     Dictionary of AIs (the first two digits in an application idetifier) and values of elements with a pre-defined length.
    /// </summary>
    private static readonly Dictionary<string, int> FirstTwoDigitsTable = new () {
        { "00", 20 },
        { "01", 16 },
        { "02", 16 },
        { "03", 16 },
        { "04", 18 },
        { "11", 8 },
        { "12", 8 },
        { "13", 8 },
        { "14", 8 },
        { "15", 8 },
        { "16", 8 },
        { "17", 8 },
        { "18", 8 },
        { "19", 8 },
        { "20", 4 },
        { "31", 10 },
        { "32", 10 },
        { "33", 10 },
        { "34", 10 },
        { "35", 10 },
        { "36", 10 },
        { "41", 16 },
    };
#endif

    /// <summary>
    /// Contains the set of Application Identifiers (AIs) that are associated with direct Global Company Prefix (GCP)
    /// assignment according to GS1 specifications.
    /// </summary>
    /// <remarks>This set includes AIs for which the GCP is determined directly from the data element, rather
    /// than by parsing additional context. The comparison is performed using ordinal string comparison to ensure case
    /// sensitivity.</remarks>
    private static readonly HashSet<string> GcpDirectAis = new (StringComparer.Ordinal)
    {
        "00", "253", "255", "401", "402", "410", "411", "412", "413", "414", "415", "416", "417",
        "7023", "8004", "8010", "8013", "8014", "8017", "8018",
    };

    /// <summary>
    /// Contains the set of GS1 Global Company Prefixes (GCPs) that are associated with Application Identifiers (AIs)
    /// where the AI begins with a leading digit.
    /// </summary>
    /// <remarks>This set is used to identify GCPs that require special handling due to their associated AIs
    /// starting with a digit. The comparison is case-sensitive and uses ordinal string comparison.</remarks>
    private static readonly HashSet<string> GcpWithLeadingDigitAis = new (StringComparer.Ordinal)
    {
        "01", "02", "03", "8006", "8026",
    };

    /// <summary>
    ///     Parse the content of a GS1-encoded string.
    /// </summary>
    /// <param name="data">
    ///     The data to be parsed.
    /// </param>
    /// <param name="processResolvedElement">
    ///     An action that is invoked to process each element element.
    /// </param>
    /// <param name="initialPosition">
    ///     The initial character position.
    /// </param>
    /// <param name="relationshipTests">Optional control to collect element AIs for relationship testing.</param>
    /// <param name="semantics">AI semantics when data relationship tests are performed.</param>
    /// <param name="gcps">Optional list of Global Company Prefixes (GCPs) for additional validation of AIs.</param>
    /// <remarks>
    /// <para>
    ///     GS1 data relationship rules are applied when requested via the <paramref name="relationshipTests"/>
    ///     parameter. Applying these rules introduces additional overhead, and may not be necessary in many
    ///     scenarios.
    /// </para>
    /// <para>
    ///     GS1 data relationship rules apply to AI element strings present on a single physical element. This
    ///     does not necessarily mean that the element strings need to appear in the same data carrier. For
    ///     example, multiple GS1-128 barcode symbols may be used in combination on a GS1 Logistic Label.
    /// </para>
    /// </remarks>
    public static void Parse(
        string? data,
        Action<IResolvedEntity> processResolvedElement,
        int initialPosition = 0,
        DataRelationshipTests relationshipTests = DataRelationshipTests.None,
        Semantics semantics = default,
        IList<string>? gcps = null) {
#if NET7_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(processResolvedElement);
#else
        if (processResolvedElement is null) {
            throw new ArgumentNullException(nameof(processResolvedElement));
        }
#endif

        // Is any data present?
        if (string.IsNullOrWhiteSpace(data)) {
            // Handle errors
            var errorElement = new ResolvedApplicationIdentifier(
                    new ParserException(string.Empty, 2001, Resources.GS1_Error_001, true),
                    initialPosition);

            processResolvedElement(errorElement);
            return;
        }

#if NET7_0_OR_GREATER
        DoParse(data!.AsSpan(), processResolvedElement, null, initialPosition, relationshipTests, semantics, false, null, 0, gcps);
#else
        DoParse(data!.AsSpan(), processResolvedElement, initialPosition, relationshipTests, semantics, false, null, 0, gcps);
#endif
        ResolvedAiList.Clear();
    }

#if NET7_0_OR_GREATER
    /// <summary>
    ///     Parse the content of a GS1-encoded string.
    /// </summary>
    /// <param name="data">
    ///     The data to be parsed.
    /// </param>
    /// <param name="processResolvedElement">
    ///     A delegate that is invoked to process each element element.  Use this overload to minmise heap allocations for greatest performance.
    /// </param>
    /// <param name="initialPosition">
    ///     The initial character position.
    /// </param>
    /// <param name="relationshipTests">Optional control to collect element AIs for relationship testing.</param>
    /// <remarks>
    /// Use this method as an alternative to Parse() for the very highest performance scenarios.  By using the ResolvedElementDelegate delegate,
    /// you can avoid unecessary heap allocations.
    /// </remarks>
    /// <param name="semantics">AI semantics when data relationship tests are performed.</param>
    /// <param name="gcps">Optional list of Global Company Prefixes (GCPs) for additional validation of AIs.</param>
    /// <remarks>
    /// <para>
    ///     GS1 data relationship rules are applied when requested via the <paramref name="relationshipTests"/>
    ///     parameter. Applying these rules intorduces additional overhead, and may not be necessary in many
    ///     scenarios.
    /// </para>
    /// <para>
    ///     GS1 data relationship rules apply to AI element strings present on a single physical element. This
    ///     does not necessarily mean that the element strings need to appear in the same data carrier. For
    ///     example, multiple GS1-128 barcode symbols may be used in combination on a GS1 Logistic Label.
    /// </para>
    /// </remarks>
    public static void ParseEx(
        ReadOnlySpan<char> data,
        ResolvedElementDelegate processResolvedElement,
        int initialPosition = 0,
        DataRelationshipTests relationshipTests = DataRelationshipTests.None,
        Semantics semantics = default,
        IList<string>? gcps = null) {
        ArgumentNullException.ThrowIfNull(processResolvedElement);

        // Is any data present?
        if (data.IsNullOrWhiteSpace()) {
            // Handle errors
            var errorElement = new ResolvedApplicationIdentifierRef(
                    new ParserException(string.Empty, 2001, Resources.GS1_Error_001, true),
                    initialPosition);

            processResolvedElement(in errorElement);
            return;
        }

        DoParse(data, null, processResolvedElement, initialPosition, relationshipTests, semantics, false, null, 0, gcps);
        ResolvedAiList.Clear();
#if NET7_0_OR_GREATER
        ResolvedApplicationIdentifierRef.ClearExceptionsForCurrentThread();
#endif
    }
#endif

#if NET6_0_OR_GREATER
    /// <summary>
    ///     Parse the content of a GS1-encoded string.
    /// </summary>
    /// <param name="data">
    ///     The data to be parsed.
    /// </param>
    /// <param name="processResolvedElement">
    ///     An action that is invoked to process each element element.
    /// </param>
    /// <param name="initialPosition">
    ///     The initial character position.
    /// </param>
    /// <param name="relationshipTests">Optional control to collect element AIs for relationship testing.</param>
    /// <param name="semantics">AI semantics when data relationship tests are performed.</param>
    /// <param name="gcps">Optional list of Global Company Prefixes (GCPs) for additional validation of AIs.</param>
    /// <remarks>
    /// <para>
    ///     GS1 data relationship rules are applied when requested via the <paramref name="relationshipTests"/>
    ///     parameter. Applying these rules introduces additional overhead, and may not be necessary in many
    ///     scenarios.
    /// </para>
    /// <para>
    ///     GS1 data relationship rules apply to AI element strings present on a single physical element. This
    ///     does not necessarily mean that the element strings need to appear in the same data carrier. For
    ///     example, multiple GS1-128 barcode symbols may be used in combination on a GS1 Logistic Label.
    /// </para>
    /// </remarks>
    public static void Parse(
        ReadOnlySpan<char> data,
        Action<IResolvedEntity> processResolvedElement,
        int initialPosition = 0,
        DataRelationshipTests relationshipTests = DataRelationshipTests.None,
        Semantics semantics = default,
        IList<string>? gcps = null) {
#if NET7_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(processResolvedElement);
#else
        if (processResolvedElement is null) {
            throw new ArgumentNullException(nameof(processResolvedElement));
        }
#endif

        // Is any data present?
        if (data.IsNullOrWhiteSpace()) {
            // Handle errors
            var errorEntity = new ResolvedApplicationIdentifier(
                    new ParserException(string.Empty, 2001, Resources.GS1_Error_001, true),
                    initialPosition);

            processResolvedElement(errorEntity);
            return;
        }

#if NET7_0_OR_GREATER
        DoParse(data, processResolvedElement, null, initialPosition, relationshipTests, semantics, false, null, 0, gcps);
#else
        DoParse(data, processResolvedElement, initialPosition, relationshipTests, semantics, false, null, 0, gcps);
#endif
        ResolvedAiList.Clear();
    }
#endif

    /// <summary>
    /// Parse multiple GS1-encoded barcode contents for a single physical element, applying full data relationship rules across all inputs.
    /// </summary>
    /// <param name="barcodeContents">List of barcode content strings (each item is one barcode).</param>
    /// <param name="processResolvedElement">Callback invoked for each element element and any aggregated rule exceptions.</param>
    /// <param name="semantics">AI semantics for relationship evaluation (e.g., GTIN semantics).</param>
    /// <param name="scenario">
    /// The barcode parsing scenario with respect to the correspondence between barcodes and physical entities.
    /// </param>
    /// <param name="gcps">Optional list of Global Company Prefixes (GCPs) for additional validation of AIs.</param>
    /// <remarks>AI semantics for relationship evaluation (e.g., GTIN semantics).
    /// This method assumes that the barcode inputs provided in the barcode contents list are for a single physical
    /// element and performs data relationship tests accrdingly.
    /// </remarks>
    public static void Parse(
        IList<string> barcodeContents,
        Action<IResolvedEntity> processResolvedElement,
        Semantics semantics = default,
        Scenario scenario = Scenario.SinglePhysicalEntity,
        IList<string>? gcps = null)
    {
#if NET7_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(barcodeContents);
        ArgumentNullException.ThrowIfNull(processResolvedElement);
#else
        if (barcodeContents is null) throw new ArgumentNullException(nameof(barcodeContents));
        if (processResolvedElement is null) throw new ArgumentNullException(nameof(processResolvedElement));
#endif

        // Aggregate element AIs across all inputs
        var pendingByAi = new Dictionary<string, IResolvedEntity>(StringComparer.Ordinal);
        var index = 0;

        foreach (var content in barcodeContents) {
            if (string.IsNullOrWhiteSpace(content)) {
                // Emit an error element for empty content but continue processing others
                var errorElement = new ResolvedApplicationIdentifier(
                    new ParserException(string.Empty, 2001, Resources.GS1_Error_001, true),
                    0);
                processResolvedElement(errorElement);
                index++;
                continue;
            }
#pragma warning disable SA1118 // Parameter should not span multiple lines
#if NET7_0_OR_GREATER
            DoParse(
                content.AsSpan(),
                processResolvedElement,
                null,
                0,
                scenario switch {
                    Scenario.SinglePhysicalEntity => DataRelationshipTests.All,
                    Scenario.SinglePhysicalEntityPerSequence => DataRelationshipTests.All,
                    Scenario.Arbitrary => DataRelationshipTests.None,
                    _ => DataRelationshipTests.InvalidPairs
                },
                semantics,
                accumulate: scenario == Scenario.SinglePhysicalEntity,
                pendingShared: scenario == Scenario.SinglePhysicalEntity ? pendingByAi : null,
                index: index,
                gcps: gcps);
#else
            DoParse(
                content.AsSpan(),
                processResolvedElement,
                0,
                scenario switch {
                    Scenario.SinglePhysicalEntity => DataRelationshipTests.All,
                    Scenario.SinglePhysicalEntityPerSequence => DataRelationshipTests.All,
                    Scenario.Arbitrary => DataRelationshipTests.None,
                    _ => DataRelationshipTests.InvalidPairs
                },
                semantics,
                accumulate: scenario == Scenario.SinglePhysicalEntity,
                pendingShared: scenario == Scenario.SinglePhysicalEntity ? pendingByAi : null,
                index: index,
                gcps: gcps);
#endif
            index++;
#pragma warning restore SA1118 // Parameter should not span multiple lines
        }

        // After parsing all inputs, evaluate relationship rules once across the aggregated ResolvedAiList
        var invalids = InvalidPairs.Test();
        var mandated = MandatedElements.Test(semantics);

        void AttachException(string ai, ParserException ex) {
            if (pendingByAi.TryGetValue(ai, out var pendingElement)) {
                pendingElement.AddException(ex);
            } else {
                var errorElement = new ResolvedApplicationIdentifier(ex, 0);
                pendingByAi[ai] = errorElement;
            }
        }

        foreach (var (ai, ex) in invalids) AttachException(ai, ex);
        foreach (var (ai, ex) in mandated) AttachException(ai, ex);

        // General rule across all barcodes: same AI appearing more than once must have identical values
        var differingAis = new HashSet<string>(StringComparer.Ordinal);
        var seenValues = new Dictionary<string, string>(StringComparer.Ordinal);

        foreach (var entry in ResolvedAiList.Current) {
            if (!seenValues.TryGetValue(entry.Identifier, out var firstVal)) {
                seenValues[entry.Identifier] = entry.Value;
            } else if (!string.Equals(firstVal, entry.Value, StringComparison.Ordinal)) {
                differingAis.Add(entry.Identifier);
            }
        }

        foreach (var ai in differingAis) {
            var message = string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_202, ai);
            var ex = new ParserException(string.Empty, 2202, message, true);
            AttachException(ai, ex);
        }

        foreach (var kvp in pendingByAi) {
            processResolvedElement(kvp.Value);
        }

        ResolvedAiList.Clear();
    }

#pragma warning disable CS1587 // XML comment is not placed on a valid language element
    /// <summary>
    ///     Perform the parsing of the data.
    /// </summary>
    /// <param name="characters">
    ///     The character buffer containing the data to be parsed.
    /// </param>
    /// <param name="processResolvedElement">
    ///     An action that is invoked to process each element element.
    /// </param>
#if NET7_0_OR_GREATER
    /// <param name="processResolvedElementDelegate">
    ///     A delegate that is invoked to process each element element.
    /// </param>
    /// <remarks>
    /// If a delegate is provided, it is invoked to process each element element.
    /// </remarks>
#endif
    /// <param name="currentPosition">
    /// The current character position.
    /// </param>
    /// <param name="relationshipTest">Indicates if element AIs will be collected for relationship testing.</param>
    /// <param name="semantics">The semantics of any GTIN (AI 01) when data relationship tests are performed.</param>
    /// <param name="accumulate">Flag indicating if element AIs should be accumulated across multiple calls.</param>
    /// <param name="pendingShared">Buffered collection of element AIs used when accumuating results.</param>
    /// <param name="index">The index of the element string sequence. This is set when using ParseMulti().</param>
    /// <param name="gcps">Optional list of Global Company Prefixes (GCPs) for additional validation of AIs.</param>
#pragma warning restore CS1587 // XML comment is not placed on a valid language element
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
    private static void DoParse(
        ReadOnlySpan<char> characters,
        Action<IResolvedEntity>? processResolvedElement,
#if NET7_0_OR_GREATER
        ResolvedElementDelegate? processResolvedElementDelegate,
#endif
        int currentPosition,
        DataRelationshipTests relationshipTest,
        Semantics semantics,
        bool accumulate = false,
        Dictionary<string, IResolvedEntity>? pendingShared = null,
        int index = 0,
        IList<string>? gcps = null) {
        int position = currentPosition;

        // Buffer callbacks when relationship tests are requested
        Dictionary<string, IResolvedEntity>? pendingByAi = accumulate ? pendingShared : (relationshipTest != DataRelationshipTests.None ? new Dictionary<string, IResolvedEntity>(StringComparer.Ordinal) : null);

        // Convert GS1 element format to FNC1 format.
        var len = characters.Length;
        var destination = len <= 512 ? stackalloc char[len] : new char[len];
        var normalisedCharacters = characters.NormaliseData(destination);

        while (normalisedCharacters.Length >= 2) {
            // Use custom lookup to avoid string allocation
            bool hasPredefinedLength = normalisedCharacters.TryGetPredefinedLength(out int numberOfChars);
            if (hasPredefinedLength) {
                if (normalisedCharacters.Length >= numberOfChars) {
#if NET6_0_OR_GREATER
                    var workingBuffer = normalisedCharacters[..numberOfChars];
                    normalisedCharacters = normalisedCharacters[numberOfChars..];
#else
                    var workingBuffer = normalisedCharacters.Slice(0, numberOfChars);
                    normalisedCharacters = normalisedCharacters.Slice(numberOfChars);
#endif
                    if (normalisedCharacters.Length > 0 && normalisedCharacters[0] == Convert.ToChar(29)) {
#if NET7_0_OR_GREATER
                        if (processResolvedElementDelegate is not null) {
                            ResolvedApplicationIdentifierRef defaultElement = new (
                                -1,
                                stackalloc char[4],
                                null,
                                null,
                                stackalloc char[ResolvedApplicationIdentifierRef.ValueMaxLength],
                                false,
                                stackalloc char[ResolvedApplicationIdentifierRef.DataTitleMaxLength],
                                stackalloc char[ResolvedApplicationIdentifierRef.DescriptionMaxLength],
                                0,
                                index);
                            var element = new ResolvedApplicationIdentifierRef(
                                new ParserException(string.Empty, 2004, Resources.GS1_Error_004, false, position + numberOfChars),
                                position,
                                workingBuffer.ResolveEx(defaultElement, workingBuffer[..2], position));
                            ApplyGcpValidationForRef(element);
                            if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                                ResolvedAiList.Add(element.Identifier, element.Value, position);
                            }

                            if (pendingByAi is null) {
                                processResolvedElementDelegate(in element);
                                continue;
                            }

                            pendingByAi[element.Identifier.ToString().TrimEnd('\0')] = new ResolvedApplicationIdentifier(
                                element.Entity,
                                element.Identifier,
                                element.InverseExponent,
                                element.Sequence,
                                element.Value,
                                element.IsFixedWidth,
                                element.DataTitle,
                                element.Description,
                                element.CharacterPosition,
                                element.Index);

                            continue;
                        }
                        else
#endif
                        {
                            var element = new ResolvedApplicationIdentifier(
                                new ParserException(string.Empty, 2004, Resources.GS1_Error_004, false, position + numberOfChars),
                                position,
#if NET6_0_OR_GREATER
                                workingBuffer.ToString().Resolve(workingBuffer[..2].ToString(), position, index));
#else
                                workingBuffer.ToString().Resolve(workingBuffer.Slice(0, 2).ToString(), position, index));
#endif
                            ApplyGcpValidation(element);
                            if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                                ResolvedAiList.Add(element.Identifier, element.Value, position);
                            }

                            RecordResolvedElement(element);
                            continue;
                        }
                    }

#if NET7_0_OR_GREATER
                    if (processResolvedElementDelegate is not null) {
                        ResolvedApplicationIdentifierRef defaultElement = new (
                            -1,
                            stackalloc char[4],
                            null,
                            null,
                            stackalloc char[ResolvedApplicationIdentifierRef.ValueMaxLength],
                            false,
                            stackalloc char[ResolvedApplicationIdentifierRef.DataTitleMaxLength],
                            stackalloc char[ResolvedApplicationIdentifierRef.DescriptionMaxLength],
                            0,
                            index);
                        var element = workingBuffer.ResolveEx(defaultElement, workingBuffer[..2], position);
                        ApplyGcpValidationForRef(element);
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(element.Identifier, element.Value, position);
                        }

                        if (pendingByAi is null) {
                            processResolvedElementDelegate(in element);
                            continue;
                        }

                        pendingByAi[element.Identifier.ToString().TrimEnd('\0')] = new ResolvedApplicationIdentifier(
                            element.Entity,
                            element.Identifier,
                            element.InverseExponent,
                            element.Sequence,
                            element.Value,
                            element.IsFixedWidth,
                            element.DataTitle,
                            element.Description,
                            element.CharacterPosition,
                            element.Index);
                    }
                    else
#endif
                    {
                        var element =
#if NET6_0_OR_GREATER
                            workingBuffer.ToString().Resolve(workingBuffer[..2].ToString(), position, index);
#else
                            workingBuffer.ToString().Resolve(workingBuffer.Slice(0, 2).ToString(), position, index);
#endif
                        ApplyGcpValidation(element);
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(element.Identifier, element.Value, position);
                        }

                        RecordResolvedElement(element);
                    }

                    position += numberOfChars;
                    if (normalisedCharacters.Length > 0 && normalisedCharacters[0] == Convert.ToChar(29)) {
#if NET7_0_OR_GREATER
                        normalisedCharacters = normalisedCharacters[1..];
#else
                        normalisedCharacters = normalisedCharacters.Slice(1);
#endif
                        position++;
                    }

                    if (normalisedCharacters.Length > 1) {
                        continue;
                    }

                    if (normalisedCharacters.Length == 1) {
#if NET7_0_OR_GREATER
                        if (processResolvedElementDelegate is not null) {
                            var element = new ResolvedApplicationIdentifierRef(
                                new ParserException(string.Empty, 2003, string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_002, normalisedCharacters.ToString()), true),
                                position,
                                index);
                            ApplyGcpValidationForRef(element);
                            if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                                ResolvedAiList.Add(element.Identifier, element.Value, position);
                            }

                            if (pendingByAi is null) {
                                processResolvedElementDelegate(in element);
                                continue;
                            }

                            pendingByAi[element.Identifier.ToString().TrimEnd('\0')] = new ResolvedApplicationIdentifier(
                                element.Entity,
                                element.Identifier,
                                element.InverseExponent,
                                element.Sequence,
                                element.Value,
                                element.IsFixedWidth,
                                element.DataTitle,
                                element.Description,
                                element.CharacterPosition,
                                element.Index);
                        }
                        else
#endif
                        {
                            var element = new ResolvedApplicationIdentifier(
                                new ParserException(string.Empty, 2003, string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_002, normalisedCharacters.ToString()), true),
                                position,
                                index);
                            ApplyGcpValidation(element);
                            if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                                ResolvedAiList.Add(element.Identifier, element.Value, position);
                            }

                            RecordResolvedElement(element);
                        }
                    }
                }
                else {
                    var workingBuffer = normalisedCharacters;
                    normalisedCharacters = [];

#if NET7_0_OR_GREATER
                    if (processResolvedElementDelegate is not null) {
                        ResolvedApplicationIdentifierRef defaultElement = new (
                            -1,
                            stackalloc char[4],
                            null,
                            null,
                            stackalloc char[ResolvedApplicationIdentifierRef.ValueMaxLength],
                            false,
                            stackalloc char[ResolvedApplicationIdentifierRef.DataTitleMaxLength],
                            stackalloc char[ResolvedApplicationIdentifierRef.DescriptionMaxLength],
                            0,
                            index);
                        var element = new ResolvedApplicationIdentifierRef(
                            new ParserException(string.Empty, 2005, Resources.GS1_Error_005, true, normalisedCharacters.Length - 1),
                            position,
                            workingBuffer.ResolveEx(defaultElement, workingBuffer[..2], position));
                        ApplyGcpValidationForRef(element);
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(element.Identifier, element.Value, position);
                        }

                        if (pendingByAi is null) {
                            processResolvedElementDelegate(in element);
                            continue;
                        }

                        pendingByAi[element.Identifier.ToString().TrimEnd('\0')] = new ResolvedApplicationIdentifier(
                            element.Entity,
                            element.Identifier,
                            element.InverseExponent,
                            element.Sequence,
                            element.Value,
                            element.IsFixedWidth,
                            element.DataTitle,
                            element.Description,
                            element.CharacterPosition,
                            element.Index);
                    }
                    else
#endif
                    {
                        var element = new ResolvedApplicationIdentifier(
                            new ParserException(string.Empty, 2005, Resources.GS1_Error_005, true, normalisedCharacters.Length - 1),
                            position,
#if NET6_0_OR_GREATER
                            workingBuffer.ToString().Resolve(workingBuffer[..2].ToString(), position, index));
#else
                            workingBuffer.ToString().Resolve(workingBuffer.Slice(0, 2).ToString(), position, index));
#endif
                        ApplyGcpValidation(element);
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(element.Identifier, element.Value, position);
                        }

                        RecordResolvedElement(element);
                    }
                }
            } else {
                int gsIndex = normalisedCharacters.IndexOf(Convert.ToChar(29));
                if (gsIndex < 0) {
                    var workingBuffer = normalisedCharacters;
                    normalisedCharacters = [];

#if NET7_0_OR_GREATER
                    ResolvedApplicationIdentifierRef defaultElement = new (
                        -1,
                        stackalloc char[4],
                        null,
                        null,
                        stackalloc char[ResolvedApplicationIdentifierRef.ValueMaxLength],
                        false,
                        stackalloc char[ResolvedApplicationIdentifierRef.DataTitleMaxLength],
                        stackalloc char[ResolvedApplicationIdentifierRef.DescriptionMaxLength],
                        0,
                        index);
                    ResolvedApplicationIdentifierRef.ClearExceptionsForCurrentThread();
                    if (processResolvedElementDelegate is not null) {
                        var element = workingBuffer.ResolveEx(defaultElement, workingBuffer[..2], position);
                        ApplyGcpValidationForRef(element);
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(element.Identifier, element.Value, position);
                        }

                        if (pendingByAi is null) {
                            processResolvedElementDelegate(in element);
                            continue;
                        }

                        pendingByAi[element.Identifier.ToString().TrimEnd('\0')] = new ResolvedApplicationIdentifier(
                            element.Entity,
                            element.Identifier,
                            element.InverseExponent,
                            element.Sequence,
                            element.Value,
                            element.IsFixedWidth,
                            element.DataTitle,
                            element.Description,
                            element.CharacterPosition,
                            element.Index);
                    }
                    else
#endif
                    {
                        var element =
#if NET6_0_OR_GREATER
                            workingBuffer.ToString().Resolve(workingBuffer[..2].ToString(), position, index);
#else
                            workingBuffer.ToString().Resolve(workingBuffer.Slice(0, 2).ToString(), position, index);
#endif
                        ApplyGcpValidation(element);
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(element.Identifier, element.Value, position);
                        }

                        RecordResolvedElement(element);
                    }
                }
                else {
#if NET6_0_OR_GREATER
                    var workingBuffer = normalisedCharacters[..gsIndex];
                    normalisedCharacters = normalisedCharacters[(gsIndex + 1)..];
#else
                    var workingBuffer = normalisedCharacters.Slice(0, gsIndex);
                    normalisedCharacters = normalisedCharacters.Slice(gsIndex + 1);
#endif

#if NET7_0_OR_GREATER
                    if (workingBuffer.IsEmpty) {
                        break;
                    }

                    ResolvedApplicationIdentifierRef defaultElement = new (
                        -1,
                        stackalloc char[4],
                        null,
                        null,
                        stackalloc char[ResolvedApplicationIdentifierRef.ValueMaxLength],
                        false,
                        stackalloc char[ResolvedApplicationIdentifierRef.DataTitleMaxLength],
                        stackalloc char[ResolvedApplicationIdentifierRef.DescriptionMaxLength],
                        0,
                        index);
                    if (processResolvedElementDelegate is not null) {
                        var element = workingBuffer.ResolveEx(defaultElement, workingBuffer[..2], position);
                        ApplyGcpValidationForRef(element);
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(element.Identifier, element.Value, position);
                        }

                        if (pendingByAi is null) {
                            processResolvedElementDelegate(in element);
                            continue;
                        }

                        pendingByAi[element.Identifier.ToString().TrimEnd('\0')] = new ResolvedApplicationIdentifier(
                            element.Entity,
                            element.Identifier,
                            element.InverseExponent,
                            element.Sequence,
                            element.Value,
                            element.IsFixedWidth,
                            element.DataTitle,
                            element.Description,
                            element.CharacterPosition,
                            element.Index);
                    }
                    else
#endif
                    {
                        var element =
#if NET6_0_OR_GREATER
                            new string(workingBuffer.ToArray())
                                .Resolve(workingBuffer[.. (workingBuffer.Length >= 2 ? 2 : workingBuffer.Length)].ToString(), position, index);
#else
                            new string(workingBuffer.ToArray())
                                .Resolve(workingBuffer.Slice(0, workingBuffer.Length >= 2 ? 2 : workingBuffer.Length).ToString(), position, index);
#endif
                        ApplyGcpValidation(element);
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(element.Identifier, element.Value, position);
                        }

                        RecordResolvedElement(element);
                    }

                    position += gsIndex - 1;
                    if (normalisedCharacters.Length > 1) {
                        continue;
                    }

                    if (normalisedCharacters.Length == 1) {
#if NET7_0_OR_GREATER
                        if (processResolvedElementDelegate is not null) {
                            var element = new ResolvedApplicationIdentifierRef(
                                new ParserException(string.Empty, 2003, string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_002, normalisedCharacters.ToString()), true),
                                position,
                                index);
                            ApplyGcpValidationForRef(element);
                            if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                                ResolvedAiList.Add(element.Identifier, element.Value, position);
                            }

                            if (pendingByAi is null) {
                                processResolvedElementDelegate(in element);
                                continue;
                            }

                            pendingByAi[element.Identifier.ToString().TrimEnd('\0')] = new ResolvedApplicationIdentifier(
                                element.Entity,
                                element.Identifier,
                                element.InverseExponent,
                                element.Sequence,
                                element.Value,
                                element.IsFixedWidth,
                                element.DataTitle,
                                element.Description,
                                element.CharacterPosition,
                                element.Index);
                        }
                        else
#endif
                        {
                            var element = new ResolvedApplicationIdentifier(
                                new ParserException(string.Empty, 2003, string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_002, normalisedCharacters.ToString()), true),
                                position,
                                index);
                            ApplyGcpValidation(element);
                            if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                                ResolvedAiList.Add(element.Identifier, element.Value, position);
                            }

                            RecordResolvedElement(element);
                        }
                    }
                }
            }

            break;
        }

        // After parsing, run relationship tests and merge exceptions, then flush callbacks
        if (!accumulate && relationshipTest != DataRelationshipTests.None) {
            IEnumerable<(string ai, ParserException ex)> invalids = [];
            IEnumerable<(string ai, ParserException ex)> mandated = [];

            if (relationshipTest == DataRelationshipTests.InvalidPairs) {
                invalids = InvalidPairs.Test();
            }
            else if (relationshipTest == DataRelationshipTests.All) {
                invalids = InvalidPairs.Test();
                mandated = MandatedElements.Test(semantics);
            }

            // General rule: same AI appearing more than once must have identical values
            // Compute AIs with differing values using ResolvedAiList.Current
            var differingAis = new HashSet<string>(StringComparer.Ordinal);
            var seenValues = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var entry in ResolvedAiList.Current) {
                if (!seenValues.TryGetValue(entry.Identifier, out var firstVal)) {
                    seenValues[entry.Identifier] = entry.Value;
                }
                else if (!string.Equals(firstVal, entry.Value, StringComparison.Ordinal)) {
                    differingAis.Add(entry.Identifier);
                }
            }

            // Attach exceptions to pending elements, or create new ones
            void AttachException(string ai, ParserException ex) {
                if (pendingByAi is null) return;
                if (pendingByAi.TryGetValue(ai, out var element)) {
                    element.AddException(ex);
                }
                else {
                    // Create a minimal error element for the AI
                    var errorElement = new ResolvedApplicationIdentifier(ex, position, index);
                    pendingByAi[ai] = errorElement;
                }
            }

            foreach (var (ai, ex) in invalids) {
                AttachException(ai, ex);
            }

            foreach (var (ai, ex) in mandated) {
                AttachException(ai, ex);
            }

            // Emit duplicate-value errors
            foreach (var ai in differingAis) {
                var message = string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_202, ai);
                var ex = new ParserException(string.Empty, 2202, message, true);
                AttachException(ai, ex);
            }

            // Flush buffered callbacks
            if (pendingByAi is not null) {
                foreach (var kvp in pendingByAi) {
                    var element = (ResolvedApplicationIdentifier)kvp.Value;
#if NET7_0_OR_GREATER
                    if (processResolvedElementDelegate is not null) {
                       // Materialize readonly spans into writable stackalloc spans
                        var idLen = element.Identifier.Length;
                        var valLen = element.Value.Length;
                        var dtLen = element.DataTitle.Length;
                        var descLen = element.Description.Length;

                        var idSpan = idLen <= 128
                            ? stackalloc char[idLen]
                            : new char[idLen];
                        var valSpan = valLen <= ResolvedApplicationIdentifierRef.ValueMaxLength ? stackalloc char[valLen] : new char[valLen];
                        var dtSpan = dtLen <= ResolvedApplicationIdentifierRef.DataTitleMaxLength ? stackalloc char[dtLen] : new char[dtLen];
                        var descSpan = descLen <= ResolvedApplicationIdentifierRef.DescriptionMaxLength ? stackalloc char[descLen] : new char[descLen];

                        element.Identifier.AsSpan().CopyTo(idSpan);
                        element.Value.AsSpan().CopyTo(valSpan);
                        element.DataTitle.AsSpan().CopyTo(dtSpan);
                        element.Description.AsSpan().CopyTo(descSpan);

                        var resolvedAiRef =
                            new ResolvedApplicationIdentifierRef(
                            element.Entity,
                            idSpan,
                            element.InverseExponent,
                            element.Sequence,
                            valSpan,
                            element.IsFixedWidth,
                            dtSpan,
                            descSpan,
                            element.CharacterPosition,
                            element.Index);

                        foreach (var ex in element.Exceptions) {
                            resolvedAiRef.AddException(ex);
                        }

                        processResolvedElementDelegate(resolvedAiRef);
                    }
                    else {
                        processResolvedElement?.Invoke(element);
                    }
#else
                    processResolvedElement?.Invoke(element);
#endif
                }
            }
        }

        // Helper to record a element element either immediately or into buffer
        void RecordResolvedElement(IResolvedEntity element) {
            if (pendingByAi is null) {
                processResolvedElement?.Invoke(element);
                return;
            }

            pendingByAi[element.Identifier] = element;
        }

        static bool ValueStartsWithAny(ReadOnlySpan<char> value, IList<string> gcps) {
            foreach (var gcp in gcps) {
                if (!string.IsNullOrEmpty(gcp)) {
                    var g = gcp.AsSpan();
                    if (value.Length >= g.Length && value.StartsWith(g, StringComparison.Ordinal))
                        return true;
                }
            }

            return false;
        }

        void ApplyGcpValidation(IResolvedEntity element) {
            if (gcps is null || gcps.Count == 0) return;
            var ai = element.Identifier;
            if (string.IsNullOrEmpty(ai)) return;
            if (GcpDirectAis.Contains(ai)) {
                if (!ValueStartsWithAny(element.Value.AsSpan(), gcps)) element.AddException(new ParserException(ai, 2101, string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_101, ai), true));
                return;
            }

            if (GcpWithLeadingDigitAis.Contains(ai)) {
                var v = element.Value;
                if (v.Length < 2 || v[0] < '0' || v[0] > '9' || !ValueStartsWithAny(v.AsSpan(1), gcps)) element.AddException(new ParserException(ai, 2101, string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_101, ai), true));
                return;
            }

            if (ai == "8003") {
                var v = element.Value;
                if (v.Length < 2 || v[0] != '0' || !ValueStartsWithAny(v.AsSpan(1), gcps)) element.AddException(new ParserException(ai, 2101, string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_101, ai), true));
            }
        }
#if NET7_0_OR_GREATER
        void ApplyGcpValidationForRef(scoped in ResolvedApplicationIdentifierRef elementRef) {
            if (gcps is null || gcps.Count == 0) return;
            var ai = elementRef.Identifier.TrimEnd('\0').ToString();
            if (string.IsNullOrEmpty(ai)) return;
            if (GcpDirectAis.Contains(ai)) {
                if (!ValueStartsWithAny(elementRef.Value, gcps)) elementRef.AddException(new ParserException(ai, 2101, string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_101, ai), true));
                return;
            }

            if (GcpWithLeadingDigitAis.Contains(ai)) {
                var v = elementRef.Value.TrimEnd('\0');
                if (v.Length < 2 || v[0] < '0' || v[0] > '9' || !ValueStartsWithAny(v[1..], gcps)) elementRef.AddException(new ParserException(ai, 2101, string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_101, ai), true));
                return;
            }

            if (ai == "8003") {
                var v = elementRef.Value.TrimEnd('\0');
                if (v.Length < 2 || v[0] != '0' || !ValueStartsWithAny(v[1..], gcps)) elementRef.AddException(new ParserException(ai, 2101, string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_101, ai), true));
            }
        }
#endif
    }
}