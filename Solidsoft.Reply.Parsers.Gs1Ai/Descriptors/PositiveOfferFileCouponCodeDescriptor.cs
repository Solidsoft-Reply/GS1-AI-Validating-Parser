// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PositiveOfferFileCouponCodeDescriptor.cs" company="Solidsoft Reply Ltd">
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
// A descriptor for North American Positive Offer File coupon codes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai.Descriptors;

using Properties;

using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

using Common;

/// <summary>
///     A descriptor for North American Positive Offer File coupon codes.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="PositiveOfferFileCouponCodeDescriptor" /> class.
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
    class PositiveOfferFileCouponCodeDescriptor(
        string dataTitle,
        string description,
        Regex pattern,
        bool isFixedWidth)
    : EntityDescriptor(dataTitle, description, pattern, isFixedWidth) {
    /// <summary>
    ///     Validate data against the descriptor.
    /// </summary>
    /// <param name="value">The GS1 identifier to be validated.</param>
    /// <param name="validationErrors">A list of validation errors.</param>
    /// <returns>True, if valid.  Otherwise, false.</returns>
    // ReSharper disable once CommentTypo
    // ReSharper disable once InheritdocConsiderUsage
    public override bool IsValid(string value, out IList<ParserException> validationErrors) {
        var result = base.IsValid(value, out validationErrors);

        if (string.IsNullOrEmpty(value)) {
            return result;
        }

#if NET7_0_OR_GREATER
        if (PositiveOfferFileCouponCodeRegex().IsMatch(value)) {
#else
        if (PositiveOfferFileCouponCodeRegex.IsMatch(value)) {
#endif
            return true;
        }

        validationErrors.Add(AddException(2017, Resources.GS1_Error_016));
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
#if NET7_0_OR_GREATER
    /// <summary>
    ///     A regular expression for North American positive offer file coupon codes.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(
        @"^[01][0-6]\d{6,12}\d{6}[0-9]\d{6,15}$",
        RegexOptions.None,
        "en-US")]
    private static partial Regex PositiveOfferFileCouponCodeRegex();
#else

    /// <summary>
    ///     A regular expression for North American positive offer file coupon codes.
    /// </summary>
    private static readonly Regex PositiveOfferFileCouponCodeRegex = new (@"^[01][0-6]\d{6,12}\d{6}[0-9]\d{6,15}$");

#endif
}