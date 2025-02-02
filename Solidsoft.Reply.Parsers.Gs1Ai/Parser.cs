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
using System.Collections.Generic;
using System.Linq;

/// <summary>
///     Parser for element strings encoded in any GS1 symbology that uses GS1 Application Identifiers,
///     such as GS1-128, GS1 DataMatrix, GS1 DataBar, GS1 QR Code and GS1 Composite.
/// </summary>
public static class Parser {
    /// <summary>
    ///     Dictionary of AI values (the first two digits in an entity) of elements with a pre-defined length.
    /// </summary>
    private static readonly Dictionary<string, int> FirstTwoDigitsTable = new ()
                                                                          {
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

        DoParse(data!.ToCharArray(), processResolvedEntity, initialPosition);
    }

    /// <summary>
    ///     Perform the parsing of the data.
    /// </summary>
    /// <param name="characters">
    ///     The character buffer containing the data to be parsed.
    /// </param>
    /// <param name="processResolvedEntity">
    ///     An action that is invoked to process each resolved entity.
    /// </param>
    /// <param name="currentPosition">
    ///     The current character position.
    /// </param>
    private static void DoParse(
        IEnumerable<char> characters,
        Action<IResolvedEntity> processResolvedEntity,
        int currentPosition) {
        while (true) {
            var characterBuffer = characters as char[] ?? characters.ToArray();
            char[] workingBuffer;
            var firstTwoDigits = new string(characterBuffer.Take(2).ToArray());
            currentPosition += 2;

            // Are first two digits in table of element strings with predefined length?
            if (FirstTwoDigitsTable.TryGetValue(firstTwoDigits, out var numberOfChars)) {
                // Does string contain at least the correct number of characters?
                if (characterBuffer.Length >= numberOfChars) {
                    // Move pre-defined number of characters to buffer
                    workingBuffer = characterBuffer.Take(numberOfChars).ToArray();
                    characterBuffer = characterBuffer.Skip(numberOfChars).ToArray();

                    // Does the buffer contain <GS>?
                    if (workingBuffer.Contains(Convert.ToChar(29))) {
                        var bufferContents = workingBuffer.ToString() ?? string.Empty;

                        // Handle errors
#pragma warning disable SA1118 // Parameter should not span multiple lines
                        processResolvedEntity?.Invoke(
                            new ResolvedApplicationIdentifier(
                                new ParserException(2003, Resources.GS1_Error_004, false),
                                currentPosition + bufferContents.IndexOf(
                                    Convert.ToChar(29).ToInvariantString(),
                                    StringComparison.Ordinal),
                                new string(workingBuffer).Resolve(firstTwoDigits, currentPosition)));
#pragma warning restore SA1118 // Parameter should not span multiple lines
                        return;
                    }

                    // Transmit data in buffer for further processing.
                    processResolvedEntity?.Invoke(
                        new string(workingBuffer).Resolve(firstTwoDigits, currentPosition));
                    currentPosition += numberOfChars - 2;

                    // If this next character is <GS>, move past it.
                    if (characterBuffer.Take(1).Contains(Convert.ToChar(29))) {
                        characterBuffer = characterBuffer.Skip(1).ToArray();
                        currentPosition++;
                    }

                    // Is any data present?
                    if (characterBuffer.Length > 0) {
                        characters = characterBuffer;
                        continue;
                    }
                }
                else {
                    // Move pre-defined number of characters to buffer
                    workingBuffer = characterBuffer.Take(characterBuffer.Length).ToArray();

                    // Handle errors
                    processResolvedEntity?.Invoke(
                        new ResolvedApplicationIdentifier(
                            new ParserException(2004, Resources.GS1_Error_005, false),
                            currentPosition + numberOfChars,
                            new string(workingBuffer).Resolve(firstTwoDigits, currentPosition)));
                }
            }
            else {
                // Does string contain <GS>?
                // ReSharper disable once IdentifierTypo
                var gscharacterPosition = characterBuffer.ToList().FindIndex(c => c == Convert.ToChar(29));
                if (gscharacterPosition < 0) {
                    // Move remaining data string to buffer.
                    workingBuffer = characterBuffer;

                    // Transmit data in buffer for further processing.
                    processResolvedEntity?.Invoke(
                        new string(workingBuffer).Resolve(firstTwoDigits, currentPosition));
                }
                else {
                    // Move characters up to <GS> to buffer
                    workingBuffer = characterBuffer.Take(gscharacterPosition).ToArray();

                    // Transmit data in buffer for further processing.
                    processResolvedEntity?.Invoke(
                        new string(workingBuffer).Resolve(firstTwoDigits, currentPosition));

                    // Move past <GS>
                    characterBuffer = characterBuffer.Skip(gscharacterPosition + 1).ToArray();
                    currentPosition += gscharacterPosition - 1;

                    // Is any data present?
                    if (characterBuffer.Length > 0) {
                        characters = characterBuffer;
                        continue;
                    }
                }
            }

            break;
        }
    }
}