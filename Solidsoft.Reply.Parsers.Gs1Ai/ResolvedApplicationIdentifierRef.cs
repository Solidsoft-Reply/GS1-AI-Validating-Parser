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

namespace Solidsoft.Reply.Parsers.Gs1Ai;

using Solidsoft.Reply.Parsers.Gs1Ai.Properties;
using Solidsoft.Reply.Parsers.Common;

#if NET7_0_OR_GREATER
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
#pragma warning disable SA1311 // Static readonly fields should begin with upper-case letter
    private static readonly Dictionary<int, List<ParserException>> exceptions = [];
#pragma warning restore SA1311 // Static readonly fields should begin with upper-case letter

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
    /// <param name="characterPosition">
    ///     The position of the application identifier within the data.
    /// </param>
    /// <param name="index">
    ///     The index of the element string sequence. This is set when using ParseMulti().
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
        int characterPosition,
        int index) {
            (Entity, InverseExponent, Sequence, IsFixedWidth, CharacterPosition, Index)
                = (entity, inverseExponent, sequence, isFixedWidth, characterPosition, index);
            Identifier = identifier;
            Value = value.Length > ValueMaxLength ? value[..ValueMaxLength] : value;
            DataTitle = dataTitle.Length > DataTitleMaxLength ? dataTitle[..DataTitleMaxLength] : dataTitle;
            Description = description.Length > DescriptionMaxLength ? description[..DescriptionMaxLength] : description;

            if (inverseExponent < -1) {
                AddException(new ParserException(identifier.ToString(), 2010, Resources.GS1_Error_010, true, 4));
            }
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ResolvedApplicationIdentifierRef" /> struct.
    /// </summary>
    /// <param name="exception">The resolver exception.</param>
    /// <param name="characterPosition">
    ///     he current character position at which parsing has occurred.
    /// </param>
    /// <param name="index">
    ///     The index of the element string sequence. This is always 0 when parsing a single sequence of
    ///     element strings, but indicates the index of the sequence when using ParseMulti().
    /// </param>
    public ResolvedApplicationIdentifierRef(ParserException exception, int characterPosition, int index = 0) {
        (Entity, InverseExponent, Sequence, IsFixedWidth, CharacterPosition, Index)
            = (-1, null, null, false, characterPosition, index);
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
        (Entity, IsFixedWidth, CharacterPosition) = (ai.Entity, ai.IsFixedWidth, characterPosition);
        Identifier = ai.Identifier;
        Value = ai.Value;
        DataTitle = ai.DataTitle;
        Description = ai.Description;
        Index = ai.Index;

        // Snapshot the existing exceptions to avoid modifying the collection during enumeration
        // (AddException adds to the same per-thread list returned by Exceptions).
        var existing = exceptions.TryGetValue(Environment.CurrentManagedThreadId, out var list)
            ? list.ToArray()
            : null;

        if (existing != null) {
            foreach (var e in existing) {
                AddException(e);
            }
        }

        AddException(new ParserException(
            ai.Identifier.ToString(),
            exception.ErrorNumber,
            exception.Message,
            exception.IsFatal,
            exception.Offset));
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
    ///     Gets the exceptions raised during attempted element resolution.
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
    public readonly bool IsError {
        get => exceptions.TryGetValue(Environment.CurrentManagedThreadId, out var list) && list.Count > 0;
    }

    /// <summary>
    ///     Gets a value indicating whether an error is fatal (further parsing was aborted).
    /// </summary>
    public readonly bool IsFatal {
        get => exceptions.TryGetValue(Environment.CurrentManagedThreadId, out var list)
            && list.Exists(exception => exception.IsFatal);
    }

    /// <summary>
    ///     Gets a value indicating whether the application identifier is a fixed-width field,.
    ///     This includes fixed width values for AIs that do not have a pre-defined length.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public bool IsFixedWidth { get; internal set; }

    /// <summary>
    ///     Gets the value associated with the application identifier.
    /// </summary>
    public readonly Span<char> Value { get; }

    /// <summary>
    ///     Gets the index of the element string sequence. This is always 0 unless using ParseMulti().
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// Clears resolver exceptions for the current thread to prevent unbounded growth during repeated parses.
    /// </summary>
    public static void ClearExceptionsForCurrentThread() {
        if (exceptions.TryGetValue(Environment.CurrentManagedThreadId, out var list)) {
            list.Clear();
        }
    }

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