// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElementDescriptors.cs" company="Solidsoft Reply Ltd">
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
// A descriptor for a GS1 data element.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai.Descriptors;

using Solidsoft.Reply.Parsers.Gs1Ai.Properties;
using System.Globalization;
using System.Text.RegularExpressions;

using Solidsoft.Reply.Parsers.Common;

/// <summary>
///     A descriptor for a GS1 data element.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="ElementDescriptors" /> class.
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
/// <param name="validator">
///     An optional validator expression for additional validation of value.
/// </param>
internal class ElementDescriptors(
    string? dataTitle,
    string? description,
    Regex? pattern,
    bool isFixedWidth,
    Regex? validator = null) {
    /// <summary>
    ///     Gets the data title of the element.
    /// </summary>
    public string? DataTitle { get; } = dataTitle;

    /// <summary>
    ///     Gets the description of the element.
    /// </summary>
    public string? Description { get; } = description;

    /// <summary>
    ///     Gets a value indicating whether the value associated with the Application Identifier is fixed-width.
    /// </summary>
    public bool IsFixedWidth { get; } = isFixedWidth;

    /// <summary>
    ///     Gets the compiled regular expression object for validating the element pattern.
    /// </summary>
    public Regex? Pattern { get; } = pattern;

    /// <summary>
    ///     Gets a compiled regular expression object for validating the element pattern.
    /// </summary>
    public Regex? Validator { get; } = validator;

#if NET7_0_OR_GREATER
    /// <summary>
    ///     Validate data against the descriptor.
    /// </summary>
    /// <param name="resolvedElement">The resolved application identifier to be validated.</param>
    /// <param name="validationErrors">A list of validation errors.</param>
    /// <returns>True, if valid.  Otherwise, false.</returns>
    public virtual bool IsValid(ResolvedApplicationIdentifierRef resolvedElement, out IList<ParserException>? validationErrors) {
        validationErrors = null;
        var value = resolvedElement.Value;

        if (value.IsNullOrWhiteSpace()) {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
            throw new ArgumentNullException(nameof(value));
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
        }

        if (Pattern == null) {
            return true;
        }

        value = value.TrimEnd('\0');
        var result = Pattern.IsMatch(value);

        if (result) {
            return true;
        }

        validationErrors = [];

        var valueString = value.Length > 0 ? " " + value.ToString() : string.Empty;
        var offset = valueString.Length > 0 ? resolvedElement.Identifier.TrimEnd('\0').Length + valueString.Length - 1 : 0;
        validationErrors.Add(
            new ParserException(
                string.Empty,
                2100,
                string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_100, valueString),
                true,
                offset));
        return false;
    }
#endif

    /// <summary>
    ///     Validate data against the descriptor.
    /// </summary>
    /// <param name="resolvedElement">The resolved application identifier to be validated.</param>
    /// <param name="validationErrors">A list of validation errors.</param>
    /// <returns>True, if valid.  Otherwise, false.</returns>
    public virtual bool IsValid(ResolvedApplicationIdentifier resolvedElement, out IList<ParserException>? validationErrors) {
        validationErrors = null;
        var value = resolvedElement.Value;

        if (string.IsNullOrWhiteSpace(value)) {
            throw new ArgumentNullException(nameof(value));
        }

        if (Pattern == null) return true;

        var result = Pattern.IsMatch(value);

        if (result) {
            return true;
        }

#pragma warning disable IDE0028 // Simplify collection initialization
        validationErrors = new List<ParserException>();
#pragma warning restore IDE0028 // Simplify collection initialization

        var valueString = value.Length > 0 ? " " + value : string.Empty;
        var offset = valueString.Length > 0 ? resolvedElement.Identifier.Trim().Length + valueString.Trim().Length - 1 : 0;
        validationErrors.Add(
            new ParserException(
                string.Empty,
                2100,
                string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_100, valueString),
                true,
                offset));
        return false;
    }
}