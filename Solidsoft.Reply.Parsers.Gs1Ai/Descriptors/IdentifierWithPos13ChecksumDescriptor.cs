// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentifierWithPos13ChecksumDescriptor.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018-2023 Solidsoft Reply Ltd.  All rights reserved.
// </copyright>
// <license>
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
// </license>
// <summary>
// A descriptor for a GS1 identifiers with a checksum at position 13 followed by optional text.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai.Descriptors;

using Properties;

using Common;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Globalization;

/// <summary>
///     A descriptor for a GS1 identifiers with a checksum at position 13 followed by optional text.
/// </summary>
internal class IdentifierWithPos13ChecksumDescriptor : EntityDescriptor {
    /// <summary>
    ///     Initializes a new instance of the <see cref="IdentifierWithPos13ChecksumDescriptor" /> class.
    /// </summary>
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
    public IdentifierWithPos13ChecksumDescriptor(
        string dataTitle,
        string description,
        Regex pattern,
        bool isFixedWidth)
        : base(dataTitle, description, pattern, isFixedWidth) {
    }

    /// <summary>
    ///     Validate data against the descriptor.
    /// </summary>
    /// <param name="value">The GS1 data element value to be validated.</param>
    /// <param name="validationErrors">A list of validation errors.</param>
    /// <returns>True, if valid.  Otherwise false.</returns>

    // ReSharper disable once CommentTypo
    // ReSharper disable once InheritdocConsiderUsage
    public override bool IsValid(string value, out IList<ParserException> validationErrors) {
        var result = base.IsValid(value, out validationErrors);

        if (string.IsNullOrEmpty(value)) {
            return result;
        }

        // Check the length is at least 13 characters
        if (value.Length < 13) {
            return result;
        }

        // Get first 13 characters
        if (value[..13].Gs1ChecksumIsValid()) {
            return result;
        }

        var valueString = value.Length > 0 ? " " + value : string.Empty;
        var offset = valueString.Length > 0 ? valueString.Trim().Length - 1 : 0;

        // ReSharper disable once StringLiteralTypo
        validationErrors.Add(
            new ParserException(
                2009,
                string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_008, valueString),
                false,
                offset));
        return false;
    }
}