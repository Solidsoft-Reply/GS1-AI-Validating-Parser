// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityDescriptor.cs" company="Solidsoft Reply Ltd.">
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
// A descriptor for a GS1 entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai.Descriptors;

using Properties;

using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

using Common;

/// <summary>
///     A descriptor for a GS1 entity.
/// </summary>
internal class EntityDescriptor {
    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityDescriptor" /> class.
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
    /// <param name="validator">
    ///     An optional validator expression for additional validation of value.
    /// </param>
    public EntityDescriptor(string? dataTitle, string? description, Regex? pattern, bool isFixedWidth, Regex? validator = null) {
        DataTitle = dataTitle;
        Description = description;
        Pattern = pattern;
        IsFixedWidth = isFixedWidth;
        Validator = validator;
    }

    /// <summary>
    ///     Gets the data title of the entity.
    /// </summary>
    public string? DataTitle { get; }

    /// <summary>
    ///     Gets the description of the entity.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    ///     Gets a value indicating whether the value associated with the Application Identifier is fixed-width.
    /// </summary>
    public bool IsFixedWidth { get; }

    /// <summary>
    ///     Gets the compiled regular expression object for validating the entity pattern.
    /// </summary>
    public Regex? Pattern { get; }

    /// <summary>
    ///     Gets the compiled regular expression object for validating the entity pattern.
    /// </summary>
    public Regex? Validator { get; }

    /// <summary>
    ///     Validate data against the descriptor.
    /// </summary>
    /// <param name="value">The data to be validated.</param>
    /// <param name="validationErrors">A list of validation errors.</param>
    /// <returns>True, if valid.  Otherwise, false.</returns>
    public virtual bool IsValid(string value, out IList<ParserException> validationErrors) {
        validationErrors = new List<ParserException>();

        if (string.IsNullOrWhiteSpace(value)) {
            throw new ArgumentNullException(nameof(value));
        }

        if (Pattern == null) {
            return true;
        }


        var result = Pattern.IsMatch(value);

        if (result) {
            return true;
        }

        var valueString = value.Length > 0 ? " " + value : string.Empty;
        validationErrors.Add(
            new ParserException(
                2100,
                string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_009, valueString),
                false));
        return false;
    }
}