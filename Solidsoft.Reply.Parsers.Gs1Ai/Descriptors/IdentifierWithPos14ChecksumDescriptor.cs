// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentifierWithPos14ChecksumDescriptor.cs" company="Solidsoft Reply Ltd">
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
// A descriptor for a GS1 identifiers ith a checksum at position 14 followed by optional text.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai.Descriptors;

using Properties;

using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

using Common;

/// <summary>
///     A descriptor for a GS1 identifiers with a checksum at position 14 followed by optional text.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="IdentifierWithPos14ChecksumDescriptor" /> class.
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
internal class IdentifierWithPos14ChecksumDescriptor(
        string dataTitle,
        string description,
        Regex? pattern,
        bool isFixedWidth)
    : EntityDescriptors(dataTitle, description, pattern, isFixedWidth) {
    /// <summary>
    ///     Validate data against the descriptor.
    /// </summary>
    /// <param name="value">The GS1 data element value to be validated.</param>
    /// <param name="validationErrors">A list of validation errors.</param>
    /// <returns>True, if valid.  Otherwise, false.</returns>
    // ReSharper disable once CommentTypo
    // ReSharper disable once InheritdocConsiderUsage
#if NET7_0_OR_GREATER
    public override bool IsValid(ReadOnlySpan<char> value, out IList<ParserException>? validationErrors) {
        var result = base.IsValid(value, out validationErrors);

        if (value.IsNull() || value.IsEmpty) {
            return result;
        }

        // Check the length is at least 14 characters
        if (value.Length < 14) {
            return result;
        }

        // Get first 14 characters
        if (value[..14].Gs1ChecksumIsValid()) {
            return result;
        }

        value = value.TrimEnd('\0');
        var valueString = value.Length > 0 ? " " + value.ToString() : string.Empty;
        var offset = valueString.Length > 0 ? valueString.Trim().Length - 1 : 0;

        // ReSharper disable once StringLiteralTypo
        validationErrors ??= [];
        validationErrors.Add(
            new ParserException(
                2010,
                string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_008, valueString),
                false,
                offset));
        return false;
    }
#else
    public override bool IsValid(string value, out IList<ParserException>? validationErrors) {
        var result = base.IsValid(value, out validationErrors);

        if (string.IsNullOrEmpty(value)) {
            return result;
        }

        // Check the length is at least 14 characters
        if (value.Length < 14) {
            return result;
        }

        // Get first 14 characters
        if (
#if NET6_0_OR_GREATER
            value.AsSpan()[..14]
#else
            value.Substring(0, 14)
#endif
        .Gs1ChecksumIsValid()) {
            return result;
        }

        var valueString = value.Length > 0 ? " " + value : string.Empty;
        var offset = valueString.Length > 0 ? valueString.Trim().Length - 1 : 0;
        validationErrors ??= [];

        // ReSharper disable once StringLiteralTypo
        validationErrors.Add(
            new ParserException(
                2010,
                string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_008, valueString),
                false,
                offset));
        return false;
    }
#endif
}