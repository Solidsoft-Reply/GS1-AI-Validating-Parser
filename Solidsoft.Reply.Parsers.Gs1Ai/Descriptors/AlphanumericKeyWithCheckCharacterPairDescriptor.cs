// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlphanumericKeyWithCheckCharacterPairDescriptor.cs" company="Solidsoft Reply Ltd">
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
// A descriptor for a GS1 identifiers whose last two characters are check characters.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai.Descriptors;

using Solidsoft.Reply.Parsers.Gs1Ai.Properties;
using System.Globalization;
using System.Text.RegularExpressions;

using Solidsoft.Reply.Parsers.Common;

/// <summary>
///     A descriptor for a GS1 identifiers whose last two characters are check characters.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="AlphanumericKeyWithCheckCharacterPairDescriptor" /> class.
/// </remarks>
/// <param name="dataTitle">
///     The data title.
/// </param>
/// <param name="description">
///     The description.
/// </param>
/// <param name="pattern">
///     The pattern.
/// </param>
/// <param name="isFixedWidth">
///     Indicates whether the value associated with the Application Identifier is fixed-width.
/// </param>
internal class AlphanumericKeyWithCheckCharacterPairDescriptor(
        string dataTitle,
        string description,
        Regex pattern,
        bool isFixedWidth)
    : EntityDescriptors(dataTitle, description, pattern, isFixedWidth) {
    /// <summary>
    ///     Validate data against the descriptor.
    /// </summary>
    /// <param name="resolvedEntity">The resolved entity to be validated.</param>
    /// <param name="validationErrors">A list of validation errors.</param>
    /// <returns>True, if valid.  Otherwise, false.</returns>
    // ReSharper disable once CommentTypo
    // ReSharper disable once InheritdocConsiderUsage
#if NET7_0_OR_GREATER
    public override bool IsValid(ResolvedApplicationIdentifierRef resolvedEntity, out IList<ParserException>? validationErrors) {
        var result = base.IsValid(resolvedEntity, out validationErrors);
        var value = resolvedEntity.Value;

        if (value.IsNull() || value.IsEmpty) {
            return result;
        }

        if (value.Gs1CheckCharactersPairIsValid()) {
            return result;
        }

        var valueString = value.Length > 0 ? " " + value.ToString() : string.Empty;
        var offset = valueString.Length > 0 ? resolvedEntity.Identifier.TrimEnd('\0').Length + valueString.Length - 1 : 0;
        validationErrors ??= [];
        validationErrors.Add(
            new ParserException(
                string.Empty,
                2009,
                string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_009, valueString, this.DataTitle),
                true,
                offset));

        return false;
    }
#endif

    /// <summary>
    /// Determines whether the specified resolved application identifier is valid according to GS1 check character
    /// rules.
    /// </summary>
    /// <remarks>This method extends base validation by checking the GS1 check character pair for correctness.
    /// If validation fails, detailed error information is provided in <paramref name="validationErrors"/>.</remarks>
    /// <param name="resolvedEntity">The resolved application identifier to validate. Must not be null.</param>
    /// <param name="validationErrors">When the method returns <see langword="false"/>, contains a list of <see cref="ParserException"/> instances
    /// describing validation errors; otherwise, contains an empty list.</param>
    /// <returns>true if the resolved application identifier is valid; otherwise, false.</returns>
    public override bool IsValid(ResolvedApplicationIdentifier resolvedEntity, out IList<ParserException>? validationErrors) {
        var result = base.IsValid(resolvedEntity, out validationErrors);
        var value = resolvedEntity.Value;

        if (string.IsNullOrEmpty(value)) {
            return result;
        }

        if (value.Gs1CheckCharactersPairIsValid()) {
            return result;
        }

        var valueString = value.Length > 0 ? " " + value : string.Empty;
        var offset = valueString.Length > 0 ? resolvedEntity.Identifier.Trim().Length + valueString.Trim().Length - 1 : 0;
        validationErrors ??= [];
        validationErrors.Add(
            new ParserException(
                string.Empty,
                2009,
                string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_009, valueString, this.DataTitle),
                true,
                offset));

        return false;
    }
}