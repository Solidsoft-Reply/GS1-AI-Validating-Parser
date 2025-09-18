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

using Common;

using Properties;

using System.Collections.Generic;

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
    /// <param name="isFixedWidth">Indicates whether the value associated with the application identifier is fixed width.</param>
    /// <param name="dataTitle">
    ///     The application identifier data title.
    /// </param>
    /// <param name="description">
    ///     The description of the application identifier.
    /// </param>
    /// <param name="currentPosition">
    ///     The position of the application identifier for the current field.
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
        int currentPosition) {
        (Entity, Identifier, InverseExponent, Sequence, Value, IsFixedWidth, DataTitle, Description, CharacterPosition)
            = (entity,
               identifier,
               inverseExponent,
               sequence,
               value,
               isFixedWidth,
               dataTitle ?? string.Empty,
               description ?? string.Empty,
               currentPosition);

        if (inverseExponent < 0) {
            AddException(new ParserException(2011, Resources.GS1_Error_010, false));
        }
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ResolvedApplicationIdentifier" /> class.
    /// </summary>
    /// <param name="exception">The resolver exception.</param>
    /// <param name="currentPosition">
    ///     The current character position at which parsing has occurred.
    /// </param>
    public ResolvedApplicationIdentifier(ParserException exception, int currentPosition) {
        (Entity, Identifier, InverseExponent, Sequence, Value, IsFixedWidth, DataTitle, Description, CharacterPosition)
            = (-1,
               string.Empty,
               null,
               null,
               string.Empty,
               false,
               string.Empty,
               string.Empty,
               currentPosition);
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
        (Entity, Identifier, Value, IsFixedWidth, DataTitle, Description, CharacterPosition)
            = (-1,
               ai.Identifier,
               ai.Value,
               ai.IsFixedWidth,
               ai.DataTitle,
               ai.Description,
               currentPosition);

        foreach (var e in ai.Exceptions) {
            AddException(e);
        }

        AddException(exception);
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
    ///     Gets the exceptions raised during attempted entity resolution.
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
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public bool IsFixedWidth { get; }

    /// <summary>
    ///     Gets the value associated with the application identifier.
    /// </summary>
    public string Value { get; }

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