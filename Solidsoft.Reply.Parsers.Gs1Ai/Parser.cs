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

using Properties;

using Common;

using System;

#if NET7_0_OR_GREATER
    /// <summary>
    ///     Represents a method that is called when an entity reference has been resolved.
    /// </summary>
    /// <param name="resolvedEntity">The resolved entity reference that is passed to the delegate. Cannot be null.</param>
    /// <remarks>
    ///     Use this delegate in high-performance scenarios. Its use avoids unecessary heap allocations.
    /// </remarks>
    public delegate void ResolvedEntityDelegate(scoped in ResolvedApplicationIdentifierRef resolvedEntity);
#endif

/// <summary>
///     Parser for element strings encoded in any GS1 symbology that uses GS1 Application Identifiers,
///     such as GS1-128, GS1 DataMatrix, GS1 DataBar, GS1 QR Code and GS1 Composite.
/// </summary>
public static class Parser {
#if !NET6_0_OR_GREATER
    /// <summary>
    ///     Dictionary of AI values (the first two digits in an entity) of elements with a pre-defined length.
    /// </summary>
    private static readonly Dictionary<string, int> FirstTwoDigitsTable = new() {
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
    public static void Parse(string? data, Action<IResolvedEntity> processResolvedEntity, int initialPosition = 0) {
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
            processResolvedEntity(
                new ResolvedApplicationIdentifier(
                    new ParserException(2001, Resources.GS1_Error_001, true),
                    initialPosition));
            return;
        }

#if NET6_0_OR_GREATER
#if NET7_0_OR_GREATER
        DoParse(data!.AsSpan(), processResolvedEntity, null, initialPosition);
#else
        DoParse(data!.AsSpan(), processResolvedEntity, initialPosition);
#endif
#else
        DoParse(data!.ToCharArray(), processResolvedEntity, initialPosition);
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
    /// <remarks>
    /// Use this method as an alternative to Parse() for the very highest performance scenarios.  By using the ResolvedEntityDelegate delegate,
    /// you can avoid unecessary heap allocations.
    /// </remarks>
    public static void ParseEx(ReadOnlySpan<char> data, ResolvedEntityDelegate processResolvedEntity, int initialPosition = 0) {
        ArgumentNullException.ThrowIfNull(processResolvedEntity);

        // Is any data present?
        if (data.IsNullOrWhiteSpace()) {
            var entity = new ResolvedApplicationIdentifierRef(
                    new ParserException(2001, Resources.GS1_Error_001, true),
                    initialPosition);

            // Handle errors
            processResolvedEntity(in entity);
            return;
        }

        DoParse(data, null, processResolvedEntity, initialPosition);
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
    public static void Parse(ReadOnlySpan<char> data, Action<IResolvedEntity> processResolvedEntity, int initialPosition = 0) {
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
            processResolvedEntity(
                new ResolvedApplicationIdentifier(
                    new ParserException(2001, Resources.GS1_Error_001, true),
                    initialPosition));
            return;
        }

#if NET7_0_OR_GREATER
        DoParse(data, processResolvedEntity, null, initialPosition);
#else
        DoParse(data, processResolvedEntity, initialPosition);
#endif
    }
#endif

#if NET6_0_OR_GREATER

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
    /// <remarks>
    /// If a delegate is provided, it is invoked to process each resolved entity.
    /// </remarks>
#endif
    /// <param name="currentPosition">
    /// The current character position.
    /// </param>
#pragma warning restore CS1587 // XML comment is not placed on a valid language element
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
    private static void DoParse(
        ReadOnlySpan<char> characters,
        Action<IResolvedEntity>? processResolvedEntity,
#if NET7_0_OR_GREATER
        ResolvedEntityDelegate? processResolvedEntityDelegate,
#endif
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
        int currentPosition) {
        int position = currentPosition;
        while (characters.Length >= 2) {
            // Use custom lookup to avoid string allocation
            bool hasPredefinedLength = TryGetPredefinedLength(characters, out int numberOfChars);
            position += 2;
            if (hasPredefinedLength) {
                if (characters.Length >= numberOfChars) {
                    var workingBuffer = characters[..numberOfChars];
                    characters = characters[numberOfChars..];
                    int gsIndex = workingBuffer.IndexOf(Convert.ToChar(29));
                    if (gsIndex >= 0) {
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
                                new ParserException(2003, Resources.GS1_Error_004, false),
                                position + gsIndex,
                                workingBuffer.ResolveEx(defaultEntity, workingBuffer[..2], position));
                            processResolvedEntityDelegate(in entity);
                        }
                        else
#endif
                        {
                            processResolvedEntity?.Invoke(
                                new ResolvedApplicationIdentifier(
                                    new ParserException(2003, Resources.GS1_Error_004, false),
                                    position + gsIndex,
                                    workingBuffer.ToString().Resolve(workingBuffer[..2].ToString(), position)));
                        }

                        return;
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
                        processResolvedEntityDelegate(in entity);
                    }
                    else
#endif
                    {
                        processResolvedEntity?.Invoke(
                            workingBuffer.ToString().Resolve(workingBuffer[..2].ToString(), position));
                    }

                    position += numberOfChars - 2;
                    if (characters.Length > 0 && characters[0] == Convert.ToChar(29)) {
                        characters = characters[1..];
                        position++;
                    }

                    if (characters.Length > 1) {
                        continue;
                    }

                    if (characters.Length == 1) {
#if NET7_0_OR_GREATER
                        if (processResolvedEntityDelegate is not null) {
                            var entity = new ResolvedApplicationIdentifierRef(
                                new ParserException(2018, Resources.GS1_Error_007, true),
                                position + 1);
                            processResolvedEntityDelegate(in entity);
                        }
                        else
#endif
                        {
                            processResolvedEntity?.Invoke(
                            new ResolvedApplicationIdentifier(
                                new ParserException(2018, Resources.GS1_Error_007, true),
                                position + 1));
                        }
                    }
                } else {
                    var workingBuffer = characters;
#if NET7_0_OR_GREATER
                    if (processResolvedEntityDelegate is not null) {
                        ResolvedApplicationIdentifierRef defaultEntity =
                            new(-1,
                                stackalloc char[4],
                                null,
                                null,
                                stackalloc char[ResolvedApplicationIdentifierRef.ValueMaxLength],
                                false,
                                stackalloc char[ResolvedApplicationIdentifierRef.DataTitleMaxLength],
                                stackalloc char[ResolvedApplicationIdentifierRef.DescriptionMaxLength],
                                0);
                        var entity = new ResolvedApplicationIdentifierRef(
                            new ParserException(2004, Resources.GS1_Error_005, false),
                            position + numberOfChars,
                            workingBuffer.ResolveEx(defaultEntity, workingBuffer[..2], position));
                        processResolvedEntityDelegate(in entity);
                    }
                    else
#endif
                    {
                        processResolvedEntity?.Invoke(
                        new ResolvedApplicationIdentifier(
                            new ParserException(2004, Resources.GS1_Error_005, false),
                            position + numberOfChars,
                            workingBuffer.ToString().Resolve(workingBuffer[..2].ToString(), position)));
                    }
                }
            } else {
                int gsIndex = characters.IndexOf(Convert.ToChar(29));
                if (gsIndex < 0) {
                    var workingBuffer = characters;
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
                        processResolvedEntityDelegate(in entity);
                    }
                    else
#endif
                    {
                        processResolvedEntity?.Invoke(
                        workingBuffer.ToString().Resolve(workingBuffer[..2].ToString(), position));
                    }
                } else {
                    var workingBuffer = characters[..gsIndex];
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
                        processResolvedEntityDelegate(in entity);
                    }
                    else
#endif
                    {
                        processResolvedEntity?.Invoke(
                        new string(workingBuffer.ToArray()).Resolve(workingBuffer[..2].ToString(), position));
                    }

                    characters = characters[(gsIndex + 1)..];
                    position += gsIndex - 1;
                    if (characters.Length > 1) {
                        continue;
                    }

                    if (characters.Length == 1) {
#if NET7_0_OR_GREATER
                        if (processResolvedEntityDelegate is not null) {
                            var entity = new ResolvedApplicationIdentifierRef(
                                new ParserException(2019, Resources.GS1_Error_007, true),
                                position + 1);
                            processResolvedEntityDelegate(in entity);
                        }
                        else
#endif
                        {
                            processResolvedEntity?.Invoke(
                            new ResolvedApplicationIdentifier(
                                new ParserException(2019, Resources.GS1_Error_007, true),
                                position + 1));
                        }
                    }
                }
            }

            break;
        }
    }
#else
    private static void DoParse(
    IEnumerable<char> characters,
    Action<IResolvedEntity> processResolvedEntity,
    int currentPosition) {
        var characterBuffer = characters as char[] ?? characters.ToArray();
        int position = currentPosition;
        while (characterBuffer.Length >= 2) {
            var firstTwoDigits = new string(characterBuffer.Take(2).ToArray());
            position += 2;
            if (FirstTwoDigitsTable.TryGetValue(firstTwoDigits, out var numberOfChars)) {
                if (characterBuffer.Length >= numberOfChars) {
                    var workingBuffer = characterBuffer.Take(numberOfChars).ToArray();
                    characterBuffer = characterBuffer.Skip(numberOfChars).ToArray();
                    int gsIndex = Array.IndexOf(workingBuffer, Convert.ToChar(29));
                    if (gsIndex >= 0) {
                        processResolvedEntity?.Invoke(
                            new ResolvedApplicationIdentifier(
                                new ParserException(2003, Resources.GS1_Error_004, false),
                                position + gsIndex,
                                new string(workingBuffer).Resolve(firstTwoDigits, position)));
                        return;
                    }

                    processResolvedEntity?.Invoke(
                        new string(workingBuffer).Resolve(firstTwoDigits, position));
                    position += numberOfChars - 2;
                    if (characterBuffer.Length > 0 && characterBuffer[0] == Convert.ToChar(29)) {
                        characterBuffer = characterBuffer.Skip(1).ToArray();
                        position++;
                    }

                    if (characterBuffer.Length > 1) {
                        continue;
                    }

                    if (characterBuffer.Length == 1) {
                        processResolvedEntity?.Invoke(
                            new ResolvedApplicationIdentifier(
                                new ParserException(2018, Resources.GS1_Error_007, true),
                                position + 1));
                    }
                } else {
                    var workingBuffer = characterBuffer;
                    processResolvedEntity?.Invoke(
                        new ResolvedApplicationIdentifier(
                            new ParserException(2004, Resources.GS1_Error_005, false),
                            position + numberOfChars,
                            new string(workingBuffer).Resolve(firstTwoDigits, position)));
                }
            } else {
                int gsIndex = Array.IndexOf(characterBuffer, Convert.ToChar(29));
                if (gsIndex < 0) {
                    var workingBuffer = characterBuffer;
                    processResolvedEntity?.Invoke(
                        new string(workingBuffer).Resolve(firstTwoDigits, position));
                } else {
                    var workingBuffer = characterBuffer.Take(gsIndex).ToArray();
                    processResolvedEntity?.Invoke(
                        new string(workingBuffer).Resolve(firstTwoDigits, position));
                    characterBuffer = characterBuffer.Skip(gsIndex + 1).ToArray();
                    position += gsIndex - 1;
                    if (characterBuffer.Length > 1) {
                        continue;
                    }

                    if (characterBuffer.Length == 1) {
                        processResolvedEntity?.Invoke(
                            new ResolvedApplicationIdentifier(
                                new ParserException(2019, Resources.GS1_Error_007, true),
                                position + 1));
                    }
                }
            }

            break;
        }
    }
#endif

#if NET6_0_OR_GREATER
    /// <summary>
    ///     Try to get the predefined length for the first two digits without allocating a string.
    /// </summary>
    /// <param name="span">A span containing at least two characters.</param>
    /// <param name="length">The predefined length if found.</param>
    /// <returns>True if found, otherwise false.</returns>
    private static bool TryGetPredefinedLength(ReadOnlySpan<char> span, out int length) {
        length = 0;
        if (span.Length < 2) return false;
        switch (span[0]) {
            case '0':
                switch (span[1]) {
                    case '0': length = 20; return true;
                    case '1': length = 16; return true;
                    case '2': length = 16; return true;
                    case '3': length = 16; return true;
                    case '4': length = 18; return true;
                }

                break;
            case '1':
                switch (span[1]) {
                    case '1': length = 8; return true;
                    case '2': length = 8; return true;
                    case '3': length = 8; return true;
                    case '4': length = 8; return true;
                    case '5': length = 8; return true;
                    case '6': length = 8; return true;
                    case '7': length = 8; return true;
                    case '8': length = 8; return true;
                    case '9': length = 8; return true;
                }

                break;
            case '2':
                switch (span[1]) {
                    case '0': length = 4; return true;
                }

                break;
            case '3':
                switch (span[1]) {
                    case '1': length = 10; return true;
                    case '2': length = 10; return true;
                    case '3': length = 10; return true;
                    case '4': length = 10; return true;
                    case '5': length = 10; return true;
                    case '6': length = 10; return true;
                }

                break;
            case '4':
                if (span[1] == '1') {
                    length = 16;
                    return true;
                }

                break;
        }

        return false;
    }
#endif
}