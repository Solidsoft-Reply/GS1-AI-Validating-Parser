// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResolvedApplicationIdentifier.cs" company="Solidsoft Reply Ltd">
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
// Represents a resolved GS1 application identifier and its associated data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai;

using Solidsoft.Reply.Parsers.Common;

using Solidsoft.Reply.Parsers.Gs1Ai.Properties;

using System.Globalization;

/// <summary>
///     Represents a resolved GS1 application identifier and its associated data.
/// </summary>
public record ResolvedApplicationIdentifier : IResolvedEntity {
    /// <summary>
    ///     A list  of resolver exceptions.
    /// </summary>
    private readonly List<ParserException> _exceptions = [];

    /// <summary>
    ///     Initializes a new instance of the <see cref="ResolvedApplicationIdentifier" /> class.
    /// </summary>
    /// <param name="entity">
    ///     The identifier entity.
    /// </param>
    /// <param name="identifier">
    ///     The application identifier.
    /// </param>
    /// <param name="inverseExponent">
    ///     The implied decimal point position in the identifier.
    /// </param>
    /// <param name="sequence">
    ///     The sequence number.
    /// </param>
    /// <param name="value">
    ///     The value associated with the application identifier.
    /// </param>
    /// <param name="isFixedWidth">
    ///     Indicates whether the value associated with the application identifier is fixed width.
    ///     This includes fixed width values for AIs that do not have a pre-defined length.
    /// </param>
    /// <param name="dataTitle">
    ///     The application identifier data title.
    /// </param>
    /// <param name="description">
    ///     The description of the application identifier.
    /// </param>
    /// <param name="currentPosition">
    ///     The position of the application identifier within the data.
    /// </param>
    /// <param name="index">
    ///     The index of the element string sequence. This is always 0 when parsing a single sequence of
    ///     element strings, but indicates the index of the sequence when using ParseMulti().
    /// </param>
    public ResolvedApplicationIdentifier(
    int entity,
    string identifier,
    int? inverseExponent,
    int? sequence,
    string value,
    bool isFixedWidth,
    string? dataTitle,
    string? description,
    int currentPosition,
    int index) {
        (Entity, Identifier, InverseExponent, Sequence, Value, IsFixedWidth, DataTitle, Description, CharacterPosition, Index)
            = (entity,
               identifier,
               inverseExponent,
               sequence,
               value,
               isFixedWidth,
               dataTitle ?? string.Empty,
               description ?? string.Empty,
               currentPosition,
               index);

        if (inverseExponent < 0) {
            AddException(new ParserException(identifier, 2010, string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_010, identifier, 4), true));
        }
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ResolvedApplicationIdentifier" /> class.
    /// </summary>
    /// <param name="entity">
    ///     The identifier entity.
    /// </param>
    /// <param name="identifier">
    ///     The application identifier.
    /// </param>
    /// <param name="inverseExponent">
    ///     The implied decimal point position in the identifier.
    /// </param>
    /// <param name="sequence">
    ///     The sequence number.
    /// </param>
    /// <param name="value">
    ///     The value associated with the application identifier.
    /// </param>
    /// <param name="isFixedWidth">
    ///     Indicates whether the value associated with the application identifier is fixed width.
    ///     This includes fixed width values for AIs that do not have a pre-defined length.
    /// </param>
    /// <param name="dataTitle">
    ///     The application identifier data title.
    /// </param>
    /// <param name="description">
    ///     The description of the application identifier.
    /// </param>
    /// <param name="currentPosition">
    ///     The position of the application identifier within the data.
    /// </param>
    /// <param name="index">
    ///     The index of the element string sequence. This is always 0 when parsing a single sequence of
    ///     element strings, but indicates the index of the sequence when using ParseMulti().
    /// </param>
    public ResolvedApplicationIdentifier(
        int entity,
        Span<char> identifier,
        int? inverseExponent,
        int? sequence,
        Span<char> value,
        bool isFixedWidth,
        Span<char> dataTitle,
        Span<char> description,
        int currentPosition,
        int index) {
        (Entity, Identifier, InverseExponent, Sequence, Value, IsFixedWidth, DataTitle, Description, CharacterPosition, Index)
            = (entity,
               identifier.ToString().TrimEnd('\0'),
               inverseExponent,
               sequence,
               value.ToString().TrimEnd('\0'),
               isFixedWidth,
               dataTitle.ToString().TrimEnd('\0') ?? string.Empty,
               description.ToString().TrimEnd('\0') ?? string.Empty,
               currentPosition,
               index);

        if (inverseExponent < 0) {
            var id = identifier.ToString().TrimEnd('\0');
            AddException(new ParserException(id, 2010, string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_010, id, 4), true));
        }
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ResolvedApplicationIdentifier" /> class.
    /// </summary>
    /// <param name="exception">The resolver exception.</param>
    /// <param name="currentPosition">
    ///     The current character position at which parsing has occurred.
    /// </param>
    /// <param name="index">
    ///     The index of the element string sequence. This is always 1 when parsing a single sequence of
    ///     element strings, but indicates the index of the sequence when using ParseMulti().
    /// </param>
    public ResolvedApplicationIdentifier(ParserException exception, int currentPosition, int index = 0) {
        (Entity, Identifier, InverseExponent, Sequence, Value, IsFixedWidth, DataTitle, Description, CharacterPosition, Index)
            = (-1,
               string.Empty,
               null,
               null,
               string.Empty,
               false,
               string.Empty,
               string.Empty,
               currentPosition,
               index);
        AddException(exception);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ResolvedApplicationIdentifier" /> class.
    /// </summary>
    /// <param name="exception">The resolver exception.</param>
    /// <param name="currentPosition">
    ///     he current character position at which parsing has occurred.
    /// </param>
    /// <param name="ai">
    ///     The GS1 application identifier.
    /// </param>
    public ResolvedApplicationIdentifier(
        ParserException exception,
        int currentPosition,
        ResolvedApplicationIdentifier ai) {
        (Entity, Identifier, Value, IsFixedWidth, DataTitle, Description, CharacterPosition, Index)
            = (ai.Entity,
               ai.Identifier,
               ai.Value,
               ai.IsFixedWidth,
               ai.DataTitle,
               ai.Description,
               currentPosition,
               ai.Index);

        foreach (var e in ai.Exceptions) {
            AddException(e);
        }

        AddException(new ParserException(
            ai.Identifier,
            exception.ErrorNumber,
            exception.Message,
            exception.IsFatal,
            exception.Offset));
    }

    /// <summary>
    ///     Gets the character position where the error occurred.
    /// </summary>
    public int CharacterPosition { get; }

    /// <summary>
    ///     Gets the application identifier data title.
    /// </summary>
    public string DataTitle { get; }

    /// <summary>
    ///     Gets the description of the application identifier.
    /// </summary>
    public string Description { get; }

    /// <summary>
    ///     Gets the application identifier entity.
    /// </summary>
    public int Entity { get; }

    /// <summary>
    ///     Gets the implied decimal point position in the value.
    /// </summary>
    public int? InverseExponent { get; }

    /// <summary>
    ///     Gets the sequence number.
    /// </summary>
    public int? Sequence { get; }

    /// <summary>
    ///     Gets the exceptions raised during attempted element resolution.
    /// </summary>
    public IEnumerable<ParserException> Exceptions => _exceptions;

    /// <summary>
    ///     Gets the application identifier.
    /// </summary>
    public string Identifier { get; }

    /// <summary>
    ///     Gets a value indicating whether resolution resulted in an error.
    /// </summary>
    public bool IsError => _exceptions.Count > 0;

    /// <summary>
    ///     Gets a value indicating whether an error is fatal (further parsing was aborted).
    /// </summary>
    public bool IsFatal {
        get {
            return _exceptions.Exists(exception => exception.IsFatal);
        }
    }

    /// <summary>
    ///     Gets a value indicating whether the application identifier is a fixed-width field,.
    ///     This includes fixed width values for AIs that do not have a pre-defined length.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public bool IsFixedWidth { get; }

    /// <summary>
    ///     Gets the value associated with the application identifier.
    /// </summary>
    public string Value { get; }

    /// <summary>
    ///     Gets the index of the element string sequence. This is always 0 unless using ParseMulti().
    /// </summary>
    public int Index { get; }

    /// <summary>
    ///     Adds a resolver exception.
    /// </summary>
    /// <param name="parserException">The resolver exception to be added.</param>
    public void AddException(ParserException? parserException) {
        if (parserException is not null) {
            _exceptions.Add(parserException);
        }
    }
}