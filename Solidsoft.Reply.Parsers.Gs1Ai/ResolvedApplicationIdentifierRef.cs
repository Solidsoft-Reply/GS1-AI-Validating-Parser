// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResolvedApplicationIdentifierRef.cs" company="Solidsoft Reply Ltd">
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

#if NET7_0_OR_GREATER
namespace Solidsoft.Reply.Parsers.Gs1Ai;

using System.Collections.Generic;
using Properties;
using Common;

/// <summary>
///     Represents a resolved GS1 application identifier and its associated data.
/// </summary>
public ref struct ResolvedApplicationIdentifierRef : IResolvedEntityRef {
    /// <summary>
    /// Represents the maximum length for a value.
    /// </summary>
    public const int ValueMaxLength = 90;

    /// <summary>
    /// Represents the maximum allowable length for a name.
    /// </summary>
    public const int DataTitleMaxLength = 50;

    /// <summary>
    /// Represents the maximum allowable length for a description.
    /// </summary>
    public const int DescriptionMaxLength = 200;

    /// <summary>
    ///     A disctionary of per-thread resolver exceptions.
    /// </summary>
    private static readonly Dictionary<int, List<ParserException>> exceptions = [];

    /// <summary>
    ///     Initializes a new instance of the <see cref="ResolvedApplicationIdentifierRef" /> struct.
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
    /// <param name="characterPosition">
    ///     The position of the application identifier for the current field.
    /// </param>
    public ResolvedApplicationIdentifierRef(
        int entity,
        Span<char> identifier,
        int? inverseExponent,
        int? sequence,
        Span<char> value,
        bool isFixedWidth,
        Span<char> dataTitle,
        Span<char> description,
        int characterPosition) {
            (Entity, InverseExponent, Sequence, IsFixedWidth, CharacterPosition)
                = (entity, inverseExponent, sequence, isFixedWidth, characterPosition);
            Identifier = identifier;
            Value = value.Length > ValueMaxLength ? value[..ValueMaxLength] : value;
            DataTitle = dataTitle.Length > DataTitleMaxLength ? dataTitle[..DataTitleMaxLength] : dataTitle;
            Description = description.Length > DescriptionMaxLength ? description[..DescriptionMaxLength] : description;

            if (inverseExponent < -1) AddException(new ParserException(2011, Resources.GS1_Error_010, false));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ResolvedApplicationIdentifierRef" /> struct.
    /// </summary>
    /// <param name="exception">The resolver exception.</param>
    /// <param name="characterPosition">
    ///     he current character position at which parsing has occurred.
    /// </param>
    public ResolvedApplicationIdentifierRef(ParserException exception, int characterPosition) {
        (Entity, InverseExponent, Sequence, IsFixedWidth, CharacterPosition)
            = (-1, null, null, false, characterPosition);
        Identifier = [];
        Value = [];
        DataTitle = [];
        Description = [];
        AddException(exception);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ResolvedApplicationIdentifierRef" /> struct.
    /// </summary>
    /// <param name="exception">The resolver exception.</param>
    /// <param name="characterPosition">
    ///     he current character position at which parsing has occurred.
    /// </param>
    /// <param name="ai">
    ///     The GS1 application identifier.
    /// </param>
    public ResolvedApplicationIdentifierRef(
        ParserException exception,
        int characterPosition,
        ResolvedApplicationIdentifierRef ai) {
        (Entity, IsFixedWidth, CharacterPosition) = (-1, ai.IsFixedWidth, characterPosition);
        Identifier = ai.Identifier;
        Value = ai.Value;
        DataTitle = ai.DataTitle;
        Description = ai.Description;

        foreach (var e in ai.Exceptions)
            AddException(e);

        AddException(exception);
    }

    /// <summary>
    ///     Gets the character position where the error occurred.
    /// </summary>
    public int CharacterPosition { get; internal set; }

    /// <summary>
    ///     Gets the application identifier data title.
    /// </summary>
    public readonly Span<char> DataTitle { get; }

    /// <summary>
    ///     Gets the description of the application identifier.
    /// </summary>
    public readonly Span<char> Description { get; }

    /// <summary>
    ///     Gets the application identifier entity.
    /// </summary>
    public int Entity { get; internal set; }

    /// <summary>
    ///     Gets the implied decimal point position in the value.
    /// </summary>
    public int? InverseExponent { get; internal set; }

    /// <summary>
    ///     Gets the sequence number.
    /// </summary>
    public int? Sequence { get; internal set; }

    /// <summary>
    ///     Gets the exceptions raised during attempted entity resolution.
    /// </summary>
    public readonly IEnumerable<ParserException> Exceptions {
        get {
            return (!exceptions.TryGetValue(Environment.CurrentManagedThreadId, out var list))
                ? NotFound()
                : list;

            static IEnumerable<ParserException> NotFound() {
                exceptions[Environment.CurrentManagedThreadId] = [];
                return exceptions[Environment.CurrentManagedThreadId];
            }
        }

        internal set {

            if (!exceptions.TryGetValue(Environment.CurrentManagedThreadId, out var list))
                exceptions[Environment.CurrentManagedThreadId] = [];
            else
                list.Clear();

            foreach (var exception in value)
                list?.Add(exception);
        }
    }

    /// <summary>
    ///     Gets the application identifier.
    /// </summary>
    public readonly Span<char> Identifier { get; }

    /// <summary>
    ///     Gets a value indicating whether resolution resulted in an error.
    /// </summary>
    public readonly bool IsError => exceptions.Count > 0;

    /// <summary>
    ///     Gets a value indicating whether an error is fatal (further parsing was aborted).
    /// </summary>
    public readonly bool IsFatal {
        get => exceptions.TryGetValue(Environment.CurrentManagedThreadId, out var list)
            && list.Exists(exception => exception.IsFatal);
    }

    /// <summary>
    ///     Gets a value indicating whether the application identifier is a fixed-width field,.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public bool IsFixedWidth { get; internal set; }

    /// <summary>
    ///     Gets the value associated with the application identifier.
    /// </summary>
    public readonly Span<char> Value { get; }

    /// <summary>
    ///     Adds a resolver exception.
    /// </summary>
    /// <param name="parserException">The resolver exception to be added.</param>
    public readonly void AddException(ParserException? parserException) {
        if (parserException is not null) {
            if (!exceptions.TryGetValue(Environment.CurrentManagedThreadId, out var list)) {
                exceptions[Environment.CurrentManagedThreadId] = [parserException];
                return;
            }

            list.Add(parserException);
        }
    }
}
#endif