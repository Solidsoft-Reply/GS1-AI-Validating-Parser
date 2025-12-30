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
/// Delegate for processing resolved entities with minimal heap allocations.
/// </summary>
/// <param name="resolvedEntity">The resolved entity to process.</param>
public delegate void ResolvedEntityDelegate(scoped in ResolvedApplicationIdentifierRef resolvedEntity);
#endif

/// <summary>
/// Provides static methods for parsing GS1-encoded strings and processing resolved application identifiers (AIs)
/// according to GS1 standards.
/// </summary>
/// <remarks>The Parser class offers multiple overloads of the Parse method to support different input types and
/// performance requirements. It applies GS1 data relationship rules when requested, enabling validation and
/// relationship testing of AIs within a single physical entity. For high-performance scenarios, use the ParseEx method
/// with a delegate to minimize heap allocations. All parsing methods invoke a callback for each resolved entity,
/// allowing callers to process or validate parsed data as needed. GS1 data relationship rules are only applied if
/// explicitly requested via the appropriate parameter. Applying these rules may introduce additional overhead and is
/// not required for all use cases. The class is thread-safe for concurrent parsing operations.</remarks>
public static class Parser {
#if !NET6_0_OR_GREATER
    /// <summary>
    ///     Dictionary of AI values (the first two digits in an entity) of elements with a pre-defined length.
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
    ///     Parse the content of a GS1-encoded string.
    /// </summary>
    /// <param name="data">
    ///     The data to be parsed.
    /// </param>
    /// <param name="processResolvedEntity">
    ///     An action that is invoked to process each resolved entity.
    /// </param>
    /// <param name="initialPosition">
    ///     The initial character position.
    /// </param>
    /// <param name="relationshipTests">Optional control to collect resolved AIs for relationship testing.</param>
    /// <param name="semantics">AI semantics when data relationship tests are performed.</param>
    /// <remarks>
    /// <para>
    ///     GS1 data relationship rules are applied when requested via the <paramref name="relationshipTests"/>
    ///     parameter. Applying these rules intorduces additional overhead, and may not be necessary in many
    ///     scenarios.
    /// </para>
    /// <para>
    ///     GS1 data relationship rules apply to AI element strings present on a single physical entity. This
    ///     does not necessarily mean that the element strings need to appear in the same data carrier. For
    ///     example, multiple GS1-128 barcode symbols may be used in combination on a GS1 Logistic Label.
    /// </para>
    /// </remarks>
    public static void Parse(
        string? data,
        Action<IResolvedEntity> processResolvedEntity,
        int initialPosition = 0,
        DataRelationshipTests relationshipTests = DataRelationshipTests.None,
        Semantics semantics = default) {
#if NET7_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(processResolvedEntity);
#else
        if (processResolvedEntity is null) {
            throw new ArgumentNullException(nameof(processResolvedEntity));
        }
#endif

        // Is any data present?
        if (string.IsNullOrWhiteSpace(data)) {
            // Handle errors
            var errorEntity = new ResolvedApplicationIdentifier(
                    new ParserException(string.Empty, 2001, Resources.GS1_Error_001, true),
                    initialPosition);

            processResolvedEntity(errorEntity);
            return;
        }

#if NET7_0_OR_GREATER
        DoParse(data!.AsSpan(), processResolvedEntity, null, initialPosition, relationshipTests, semantics);
#else
        DoParse(data!.AsSpan(), processResolvedEntity, initialPosition, relationshipTests, semantics);
#endif
    }

#if NET7_0_OR_GREATER
    /// <summary>
    ///     Parse the content of a GS1-encoded string.
    /// </summary>
    /// <param name="data">
    ///     The data to be parsed.
    /// </param>
    /// <param name="processResolvedEntity">
    ///     A delegate that is invoked to process each resolved entity.  Use this overload to minmise heap allocations for greatest performance.
    /// </param>
    /// <param name="initialPosition">
    ///     The initial character position.
    /// </param>
    /// <param name="relationshipTests">Optional control to collect resolved AIs for relationship testing.</param>
    /// <remarks>
    /// Use this method as an alternative to Parse() for the very highest performance scenarios.  By using the ResolvedEntityDelegate delegate,
    /// you can avoid unecessary heap allocations.
    /// </remarks>
    /// <param name="semantics">AI semantics when data relationship tests are performed.</param>
    /// <remarks>
    /// <para>
    ///     GS1 data relationship rules are applied when requested via the <paramref name="relationshipTests"/>
    ///     parameter. Applying these rules intorduces additional overhead, and may not be necessary in many
    ///     scenarios.
    /// </para>
    /// <para>
    ///     GS1 data relationship rules apply to AI element strings present on a single physical entity. This
    ///     does not necessarily mean that the element strings need to appear in the same data carrier. For
    ///     example, multiple GS1-128 barcode symbols may be used in combination on a GS1 Logistic Label.
    /// </para>
    /// </remarks>
    public static void ParseEx(
        ReadOnlySpan<char> data,
        ResolvedEntityDelegate processResolvedEntity,
        int initialPosition = 0,
        DataRelationshipTests relationshipTests = DataRelationshipTests.None,
        Semantics semantics = default) {
        ArgumentNullException.ThrowIfNull(processResolvedEntity);

        // Is any data present?
        if (data.IsNullOrWhiteSpace()) {
            var entity = new ResolvedApplicationIdentifierRef(
                    new ParserException(string.Empty, 2001, Resources.GS1_Error_001, true),
                    initialPosition);

            // Handle errors
            processResolvedEntity(in entity);
            return;
        }

        DoParse(data, null, processResolvedEntity, initialPosition, relationshipTests, semantics);
    }
#endif

#if NET6_0_OR_GREATER
    /// <summary>
    ///     Parse the content of a GS1-encoded string.
    /// </summary>
    /// <param name="data">
    ///     The data to be parsed.
    /// </param>
    /// <param name="processResolvedEntity">
    ///     An action that is invoked to process each resolved entity.
    /// </param>
    /// <param name="initialPosition">
    ///     The initial character position.
    /// </param>
    /// <param name="relationshipTests">Optional control to collect resolved AIs for relationship testing.</param>
    /// <param name="semantics">AI semantics when data relationship tests are performed.</param>
    /// <remarks>
    /// <para>
    ///     GS1 data relationship rules are applied when requested via the <paramref name="relationshipTests"/>
    ///     parameter. Applying these rules intorduces additional overhead, and may not be necessary in many
    ///     scenarios.
    /// </para>
    /// <para>
    ///     GS1 data relationship rules apply to AI element strings present on a single physical entity. This
    ///     does not necessarily mean that the element strings need to appear in the same data carrier. For
    ///     example, multiple GS1-128 barcode symbols may be used in combination on a GS1 Logistic Label.
    /// </para>
    /// </remarks>
    public static void Parse(
        ReadOnlySpan<char> data,
        Action<IResolvedEntity> processResolvedEntity,
        int initialPosition = 0,
        DataRelationshipTests relationshipTests = DataRelationshipTests.None,
        Semantics semantics = default) {
#if NET7_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(processResolvedEntity);
#else
        if (processResolvedEntity is null) {
            throw new ArgumentNullException(nameof(processResolvedEntity));
        }
#endif

        // Is any data present?
        if (data.IsNullOrWhiteSpace()) {
            // Handle errors
            var errorEntity = new ResolvedApplicationIdentifier(
                    new ParserException(string.Empty, 2001, Resources.GS1_Error_001, true),
                    initialPosition);

            processResolvedEntity(errorEntity);
            return;
        }

#if NET7_0_OR_GREATER
        DoParse(data, processResolvedEntity, null, initialPosition, relationshipTests, semantics);
#else
        DoParse(data, processResolvedEntity, initialPosition, relationshipTests, semantics);
#endif
    }
#endif

#pragma warning disable CS1587 // XML comment is not placed on a valid language element
    /// <summary>
    ///     Perform the parsing of the data.
    /// </summary>
    /// <param name="characters">
    ///     The character buffer containing the data to be parsed.
    /// </param>
    /// <param name="processResolvedEntity">
    ///     An action that is invoked to process each resolved entity.
    /// </param>
#if NET7_0_OR_GREATER
    /// <param name="processResolvedEntityDelegate"></param>
    /// <remarks>
    /// If a delegate is provided, it is invoked to process each resolved entity.
    /// </remarks>
#endif
    /// <param name="currentPosition">
    /// The current character position.
    /// </param>
    /// <param name="relationshipTest">Indicates if resolved AIs will be collected for relationship testing.</param>
    /// <param name="semantics">The semantics of any GTIN (AI 01) when data relationship tests are performed.</param>
#pragma warning restore CS1587 // XML comment is not placed on a valid language element
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
    private static void DoParse(
        ReadOnlySpan<char> characters,
        Action<IResolvedEntity>? processResolvedEntity,
#if NET7_0_OR_GREATER
        ResolvedEntityDelegate? processResolvedEntityDelegate,
#endif
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
        int currentPosition,
        DataRelationshipTests relationshipTest,
        Semantics semantics) {
        int position = currentPosition;

        // Initialize/clear collected AIs only when relationship tests are requested
        if (relationshipTest != DataRelationshipTests.None && relationshipTest != DataRelationshipTests.None) {
            ResolvedAiList.Clear();
        }

        // Buffer callbacks when relationship tests are requested
        Dictionary<string, IResolvedEntity>? pendingByAi = (relationshipTest != DataRelationshipTests.None)
            ? new Dictionary<string, IResolvedEntity>(StringComparer.Ordinal)
            : null;

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
                        if (processResolvedEntityDelegate is not null) {
                            ResolvedApplicationIdentifierRef defaultEntity = new(
                                -1,
                                stackalloc char[4],
                                null,
                                null,
                                stackalloc char[ResolvedApplicationIdentifierRef.ValueMaxLength],
                                false,
                                stackalloc char[ResolvedApplicationIdentifierRef.DataTitleMaxLength],
                                stackalloc char[ResolvedApplicationIdentifierRef.DescriptionMaxLength],
                                0);
                            var entity = new ResolvedApplicationIdentifierRef(
                                new ParserException(string.Empty, 2004, Resources.GS1_Error_004, false, position + numberOfChars),
                                position,
                                workingBuffer.ResolveEx(defaultEntity, workingBuffer[..2], position));
                            if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                                ResolvedAiList.Add(entity.Identifier, entity.Value, position);
                            }

                            if (pendingByAi is null) {
                                processResolvedEntityDelegate(in entity);
                                continue;
                            }

                            pendingByAi[entity.Identifier.ToString()] = new ResolvedApplicationIdentifier(
                                entity.Entity,
                                entity.Identifier.ToString(),
                                entity.InverseExponent,
                                entity.Sequence,
                                entity.Value.ToString(),
                                entity.IsFixedWidth,
                                entity.DataTitle.ToString(),
                                entity.Description.ToString(),
                                entity.CharacterPosition);

                            continue;
                        } else
#endif
                        {
                            var resolved = new ResolvedApplicationIdentifier(
                                new ParserException(string.Empty, 2004, Resources.GS1_Error_004, false, position + numberOfChars),
                                position,
#if NET6_0_OR_GREATER
                                workingBuffer.ToString().Resolve(workingBuffer[..2].ToString(), position));
#else
                                workingBuffer.ToString().Resolve(workingBuffer.Slice(0, 2).ToString(), position));
#endif
                            if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                                ResolvedAiList.Add(resolved.Identifier, resolved.Value, position);
                            }

                            RecordResolvedEntity(resolved);
                            continue;
                        }
                    }

#if NET7_0_OR_GREATER
                    if (processResolvedEntityDelegate is not null) {
                        ResolvedApplicationIdentifierRef defaultEntity = new(
                            -1,
                            stackalloc char[4],
                            null,
                            null,
                            stackalloc char[ResolvedApplicationIdentifierRef.ValueMaxLength],
                            false,
                            stackalloc char[ResolvedApplicationIdentifierRef.DataTitleMaxLength],
                            stackalloc char[ResolvedApplicationIdentifierRef.DescriptionMaxLength],
                            0);
                        var entity = workingBuffer.ResolveEx(defaultEntity, workingBuffer[..2], position);
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(entity.Identifier, entity.Value, position);
                        }

                        if (pendingByAi is null) {
                            processResolvedEntityDelegate(in entity);
                            continue;
                        }

                        pendingByAi[entity.Identifier.ToString()] = new ResolvedApplicationIdentifier(
                            entity.Entity,
                            entity.Identifier.ToString(),
                            entity.InverseExponent,
                            entity.Sequence,
                            entity.Value.ToString(),
                            entity.IsFixedWidth,
                            entity.DataTitle.ToString(),
                            entity.Description.ToString(),
                            entity.CharacterPosition);
                    } else
#endif
                    {
                        var resolved =
#if NET6_0_OR_GREATER
                            workingBuffer.ToString().Resolve(workingBuffer[..2].ToString(), position);
#else
                            workingBuffer.ToString().Resolve(workingBuffer.Slice(0, 2).ToString(), position);
#endif
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(resolved.Identifier, resolved.Value, position);
                        }

                        RecordResolvedEntity(resolved);
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
                        if (processResolvedEntityDelegate is not null) {
                            var entity = new ResolvedApplicationIdentifierRef(
                                new ParserException(string.Empty, 2003, string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_002, normalisedCharacters.ToString()), true),
                                position);
                            if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                                ResolvedAiList.Add(entity.Identifier, entity.Value, position);
                            }

                            if (pendingByAi is null) {
                                processResolvedEntityDelegate(in entity);
                                continue;
                            }

                            pendingByAi[entity.Identifier.ToString()] = new ResolvedApplicationIdentifier(
                                entity.Entity,
                                entity.Identifier.ToString(),
                                entity.InverseExponent,
                                entity.Sequence,
                                entity.Value.ToString(),
                                entity.IsFixedWidth,
                                entity.DataTitle.ToString(),
                                entity.Description.ToString(),
                                entity.CharacterPosition);
                        }
                        else
#endif
                        {
                            var resolved = new ResolvedApplicationIdentifier(
                                new ParserException(string.Empty, 2003, string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_002, normalisedCharacters.ToString()), true),
                                position);
                            if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                                ResolvedAiList.Add(resolved.Identifier, resolved.Value, position);
                            }

                            RecordResolvedEntity(resolved);
                        }
                    }
                } else {
                    var workingBuffer = normalisedCharacters;
                    normalisedCharacters = [];

#if NET7_0_OR_GREATER
                    if (processResolvedEntityDelegate is not null) {
                        ResolvedApplicationIdentifierRef defaultEntity = new(
                            -1,
                            stackalloc char[4],
                            null,
                            null,
                            stackalloc char[ResolvedApplicationIdentifierRef.ValueMaxLength],
                            false,
                            stackalloc char[ResolvedApplicationIdentifierRef.DataTitleMaxLength],
                            stackalloc char[ResolvedApplicationIdentifierRef.DescriptionMaxLength],
                            0);
                        var entity = new ResolvedApplicationIdentifierRef(
                            new ParserException(string.Empty, 2005, Resources.GS1_Error_005, true, normalisedCharacters.Length - 1),
                            position,
                            workingBuffer.ResolveEx(defaultEntity, workingBuffer[..2], position));
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(entity.Identifier, entity.Value, position);
                        }

                        if (pendingByAi is null) {
                            processResolvedEntityDelegate(in entity);
                            continue;
                        }

                        pendingByAi[entity.Identifier.ToString()] = new ResolvedApplicationIdentifier(
                            entity.Entity,
                            entity.Identifier.ToString(),
                            entity.InverseExponent,
                            entity.Sequence,
                            entity.Value.ToString(),
                            entity.IsFixedWidth,
                            entity.DataTitle.ToString(),
                            entity.Description.ToString(),
                            entity.CharacterPosition);
                    }
                    else
#endif
                    {
                        var resolved = new ResolvedApplicationIdentifier(
                            new ParserException(string.Empty, 2005, Resources.GS1_Error_005, true, normalisedCharacters.Length - 1),
                            position,
#if NET6_0_OR_GREATER
                            workingBuffer.ToString().Resolve(workingBuffer[..2].ToString(), position));
#else
                            workingBuffer.ToString().Resolve(workingBuffer.Slice(0, 2).ToString(), position));
#endif
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(resolved.Identifier, resolved.Value, position);
                        }

                        RecordResolvedEntity(resolved);
                    }
                }
            } else {
                int gsIndex = normalisedCharacters.IndexOf(Convert.ToChar(29));
                if (gsIndex < 0) {
                    var workingBuffer = normalisedCharacters;
                    normalisedCharacters = [];

#if NET7_0_OR_GREATER
                    ResolvedApplicationIdentifierRef defaultEntity = new(
                        -1,
                        stackalloc char[4],
                        null,
                        null,
                        stackalloc char[ResolvedApplicationIdentifierRef.ValueMaxLength],
                        false,
                        stackalloc char[ResolvedApplicationIdentifierRef.DataTitleMaxLength],
                        stackalloc char[ResolvedApplicationIdentifierRef.DescriptionMaxLength],
                        0);
                    if (processResolvedEntityDelegate is not null) {
                        var entity = workingBuffer.ResolveEx(defaultEntity, workingBuffer[..2], position);
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(entity.Identifier, entity.Value, position);
                        }

                        if (pendingByAi is null) {
                            processResolvedEntityDelegate(in entity);
                            continue;
                        }

                        pendingByAi[entity.Identifier.ToString()] = new ResolvedApplicationIdentifier(
                            entity.Entity,
                            entity.Identifier.ToString(),
                            entity.InverseExponent,
                            entity.Sequence,
                            entity.Value.ToString(),
                            entity.IsFixedWidth,
                            entity.DataTitle.ToString(),
                            entity.Description.ToString(),
                            entity.CharacterPosition);
                    }
                    else
#endif
                    {
                        var resolved =
#if NET6_0_OR_GREATER
                            workingBuffer.ToString().Resolve(workingBuffer[..2].ToString(), position);
#else
                            workingBuffer.ToString().Resolve(workingBuffer.Slice(0, 2).ToString(), position);
#endif
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(resolved.Identifier, resolved.Value, position);
                        }

                        RecordResolvedEntity(resolved);
                    }
                } else {
#if NET6_0_OR_GREATER
                    var workingBuffer = normalisedCharacters[..gsIndex];
                    normalisedCharacters = normalisedCharacters[(gsIndex + 1) ..];
#else
                    var workingBuffer = normalisedCharacters.Slice(0, gsIndex);
                    normalisedCharacters = normalisedCharacters.Slice(gsIndex + 1);
#endif

#if NET7_0_OR_GREATER
                    if (workingBuffer.IsEmpty) {
                        break;
                    }

                    ResolvedApplicationIdentifierRef defaultEntity = new(
                        -1,
                        stackalloc char[4],
                        null,
                        null,
                        stackalloc char[ResolvedApplicationIdentifierRef.ValueMaxLength],
                        false,
                        stackalloc char[ResolvedApplicationIdentifierRef.DataTitleMaxLength],
                        stackalloc char[ResolvedApplicationIdentifierRef.DescriptionMaxLength],
                        0);
                    if (processResolvedEntityDelegate is not null) {
                        var entity = workingBuffer.ResolveEx(defaultEntity, workingBuffer[..2], position);
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(entity.Identifier, entity.Value, position);
                        }

                        if (pendingByAi is null) {
                            processResolvedEntityDelegate(in entity);
                            continue;
                        }

                        pendingByAi[entity.Identifier.ToString()] = new ResolvedApplicationIdentifier(
                            entity.Entity,
                            entity.Identifier.ToString(),
                            entity.InverseExponent,
                            entity.Sequence,
                            entity.Value.ToString(),
                            entity.IsFixedWidth,
                            entity.DataTitle.ToString(),
                            entity.Description.ToString(),
                            entity.CharacterPosition);
                    }
                    else
#endif
                    {
                        var resolved =
#if NET6_0_OR_GREATER
                            new string(workingBuffer.ToArray())
                                .Resolve(workingBuffer[.. (workingBuffer.Length >= 2 ? 2 : workingBuffer.Length)].ToString(), position);
#else
                            new string(workingBuffer.ToArray())
                                .Resolve(workingBuffer.Slice(0, workingBuffer.Length >= 2 ? 2 : workingBuffer.Length).ToString(), position);
#endif
                        if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                            ResolvedAiList.Add(resolved.Identifier, resolved.Value, position);
                        }

                        RecordResolvedEntity(resolved);
                    }

                    position += gsIndex - 1;
                    if (normalisedCharacters.Length > 1) {
                        continue;
                    }

                    if (normalisedCharacters.Length == 1) {
#if NET7_0_OR_GREATER
                        if (processResolvedEntityDelegate is not null) {
                            var entity = new ResolvedApplicationIdentifierRef(
                                new ParserException(string.Empty, 2003, string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_002, normalisedCharacters.ToString()), true),
                                position);
                            if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                                ResolvedAiList.Add(entity.Identifier, entity.Value, position);
                            }

                            if (pendingByAi is null) {
                                processResolvedEntityDelegate(in entity);
                                continue;
                            }

                            pendingByAi[entity.Identifier.ToString()] = new ResolvedApplicationIdentifier(
                                entity.Entity,
                                entity.Identifier.ToString(),
                                entity.InverseExponent,
                                entity.Sequence,
                                entity.Value.ToString(),
                                entity.IsFixedWidth,
                                entity.DataTitle.ToString(),
                                entity.Description.ToString(),
                                entity.CharacterPosition);
                        }
                        else
#endif
                        {
                            var resolved = new ResolvedApplicationIdentifier(
                                new ParserException(string.Empty, 2003, string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.GS1_Error_002, normalisedCharacters.ToString()), true),
                                position);
                            if (relationshipTest == DataRelationshipTests.All || relationshipTest == DataRelationshipTests.InvalidPairs) {
                                ResolvedAiList.Add(resolved.Identifier, resolved.Value, position);
                            }

                            RecordResolvedEntity(resolved);
                        }
                    }
                }
            }

            break;
        }

        // After parsing, run relationship tests and merge exceptions, then flush callbacks
        if (relationshipTest != DataRelationshipTests.None) {
            IEnumerable<(string ai, ParserException ex)> invalids = Array.Empty<(string, ParserException)>();
            IEnumerable<(string ai, ParserException ex)> mandated = Array.Empty<(string, ParserException)>();

            if (relationshipTest == DataRelationshipTests.InvalidPairs) {
                invalids = InvalidPairs.Test();
            } else if (relationshipTest == DataRelationshipTests.All) {
                invalids = InvalidPairs.Test();
                mandated = MandatedElements.Test(semantics);
            }

            // Attach exceptions to pending entities, or create new ones
            void AttachException(string ai, ParserException ex) {
                if (pendingByAi is null) return;
                if (pendingByAi.TryGetValue(ai, out var entity)) {
                    entity.AddException(ex);
                } else {
                    // Create a minimal error entity for the AI
                    var clsEntity = new ResolvedApplicationIdentifier(ex, position);
                    pendingByAi[ai] = clsEntity;
                }
            }

            foreach (var (ai, ex) in invalids) {
                AttachException(ai, ex);
            }

            foreach (var (ai, ex) in mandated) {
                AttachException(ai, ex);
            }

            // Flush buffered callbacks
            if (pendingByAi is not null) {
                foreach (var kvp in pendingByAi) {
                    var entity = kvp.Value;
                    processResolvedEntity?.Invoke(entity);
                }
            }
        }

        // Helper to record a resolved entity either immediately or into buffer
        void RecordResolvedEntity(IResolvedEntity entity) {
            if (pendingByAi is null) {
                processResolvedEntity?.Invoke(entity);
                return;
            }

            pendingByAi[entity.Identifier] = entity;
        }
    }
}