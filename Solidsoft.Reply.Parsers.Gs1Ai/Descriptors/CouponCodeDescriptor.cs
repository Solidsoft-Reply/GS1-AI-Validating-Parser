// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouponCodeDescriptor.cs" company="Solidsoft Reply Ltd">
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
// A descriptor for North American coupon codes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai.Descriptors;

using Properties;

using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

using Common;

/// <summary>
///     A descriptor for North American coupon codes.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="CouponCodeDescriptor" /> class.
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
internal
#if NET7_0_OR_GREATER
    partial
#endif
    class CouponCodeDescriptor(
        string dataTitle,
        string description,
        Regex pattern,
        bool isFixedWidth)
    : EntityDescriptors(dataTitle, description, pattern, isFixedWidth) {
#if !NET7_0_OR_GREATER
    /// <summary>
    ///     A regular expression for North American coupon codes.
    /// </summary>
    private static readonly Regex CouponCodeRegex = new (@"^[0-6]\d{6,12}\d{6}[1-5]\d{1,5}[1-5]\d{1,5}[0-49]\d{3}(1[0-3][1-5]\d{1,5}[0-49]\d{3}[0-6]\d{6,12}(2[1-5]\d{1,5}[0-49]\d{3}[0-6]\d{6,12})?)?" + $"(3{DatePattern})?(4{DatePattern})?" + @"(5[0-9]\d{6,15})?(6[1-7]\d{7,13})?(9[0-256][0-2]\d[01])?$");
#endif

    /// <summary>
    ///     A regular expression for six-digit date representation - YYMMDD.
    /// </summary>
    private const string DatePattern = @"(((\d{2})(0[13578]|1[02])(0[1-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[1-9]|[12]\d|30))|((\d{2})02(0[1-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))";

    /// <summary>
    ///     Validate data against the descriptor.
    /// </summary>
    /// <param name="value">The GS1 identifier to be validated.</param>
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

        if (CouponCodeRegex().IsMatch(value)) {
            return true;
        }

        value = value.TrimEnd('\0');
        validationErrors ??= [];
        validationErrors.Add(AddException(value, 2016, Resources.GS1_Error_015));
        return false;

        ParserException AddException(ReadOnlySpan<char> value, int errorNumber, string message, string country = "") {
            var valueString = value.Length > 0 ? " " + value.ToString() : string.Empty;
            var offset = valueString.Length > 0 ? valueString.Trim().Length - 1 : 0;
            return new ParserException(
                errorNumber,
                string.Format(CultureInfo.CurrentCulture, message, valueString, country),
                false,
                offset);
        }
    }

    // ReSharper disable once InvalidXmlDocComment
    /// <summary>
    ///     A regular expression for North American coupon codes.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(
        @"^[0-6]\d{6,12}\d{6}[1-5]\d{1,5}[1-5]\d{1,5}[0-49]\d{3}(1[0-3][1-5]\d{1,5}[0-49]\d{3}[0-6](\d{6,12})?(2[1-5]\d{1,5}[0-49]\d{3}[0-6](\d{6,12})?)?)?" + $"(3{DatePattern})?(4{DatePattern})?" + @"(5[0-9]\d{6,15})?(6[1-7]\d{7,13})?(9[0-256][0-2]\d[01])?$",
        RegexOptions.None,
        "en-US")]
    private static partial Regex CouponCodeRegex();
#else
    public override bool IsValid(string value, out IList<ParserException>? validationErrors) {
        var result = base.IsValid(value, out validationErrors);

        if (string.IsNullOrEmpty(value)) {
            return result;
        }

        if (CouponCodeRegex.IsMatch(value)) {
            return true;
        }

        validationErrors ??= [];
        validationErrors.Add(AddException(2016, Resources.GS1_Error_015));
        return false;

        ParserException AddException(int errorNumber, string message, string country = "") {
            var valueString = value.Length > 0 ? " " + value : string.Empty;
            var offset = valueString.Length > 0 ? valueString.Trim().Length - 1 : 0;
            return new ParserException(
                errorNumber,
                string.Format(CultureInfo.CurrentCulture, message, valueString, country),
                false,
                offset);
        }
    }
#endif
}