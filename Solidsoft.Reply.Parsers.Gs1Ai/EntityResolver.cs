// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityResolver.cs" company="Solidsoft Reply Ltd.">
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
// Resolves GS1 entities, validating the entity value and providing descriptors.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CommentTypo
// ReSharper disable BadListLineBreaks

#pragma warning disable S3358

namespace Solidsoft.Reply.Parsers.Gs1Ai;

using Descriptors;

using Properties;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

using Common;

/// <summary>
///     Resolves GS1 entities, validating the entity value and providing descriptors.
/// </summary>
#if NET7_0_OR_GREATER
internal static partial class EntityResolver {

    /// <summary>
    ///     Returns a regular expression for matching a Serial Shipping Container Code.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{18}$", RegexOptions.None, "en-US")]
    private static partial Regex SsccRegex();

    /// <summary>
    ///     Returns a regular expression for matching a Global Trade Item Number.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{14}$", RegexOptions.None, "en-US")]
    private static partial Regex GtinRegex();

    /// <summary>
    ///     Returns a regular expression for matching a Global Document Type Identifier.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^\d{13}([-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,17})?$""", RegexOptions.None, "en-US")]
    private static partial Regex GdtiRegex();

    /// <summary>
    ///     Returns a regular expression for matching a Global Coupon Number.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{13}(\d{1,12})?$", RegexOptions.None, "en-US")]
    private static partial Regex GcnRegex();

    /// <summary>
    ///     Returns a regular expression for matching a Global Returnable Asset Identifier (GRAI).
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^0\d{13}([-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,16})?$""", RegexOptions.None, "en-US")]
    private static partial Regex GraiRegex();

    /// <summary>
    ///     Returns a regular expression for an identification of an individual trade item piece (ITIP).
    /// </summary>
    [GeneratedRegex(@"^\d{14}\d{2}\d{2}$", RegexOptions.None, "en-US")]
    private static partial Regex ItipRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-2 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,2}$""", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8202CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 2-2 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{2,2}$""", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet820202CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-3 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,3}$""", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8203CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-10 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,10}$""", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8210CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-12 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,12}$""", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8212CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-20 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,20}$""", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8220CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-25 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,25}$""", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8225CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-28 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,28}$""", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8228CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 3-30 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{3,30}$""", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet820330CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-30 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,30}$""", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8230CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-30 character value of characters taken from Character Set 39.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^[#-/0-9A-Z]{1,30}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet3930CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-34 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,34}$""", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8234CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-35 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,35}$""", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8235CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-50 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,50}$""", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8250CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-70 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,70}$""", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8270CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-90 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,90}$""", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8290CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a processor with an ISO country code.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^\d{3}[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,27}$""", RegexOptions.None, "en-US")]
    private static partial Regex ProcessorWithIsoCountryCodeRegex();

    /// <summary>
    ///     Returns a regular expression for matching a processor with an ISO country code.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^\d{3}[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,9}$""", RegexOptions.None, "en-US")]
    private static partial Regex PostalCodeWithIsoCountryCodeRegex();

    /// <summary>
    ///     Returns a regular expression for matching a GS1 UIC with Extension 1 and Imported index.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("""^\d[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{3,3}$""", RegexOptions.None, "en-US")]
    private static partial Regex Gs1UicWithExtension1AndImportedIndexRegex();

    /// <summary>
    ///     Returns a regular expression for six-digit date representation - YYMMDD.
    ///     If it is not necessary to specify the day, the day field can be filled with two zeros.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^(((\d{2})(0[13578]|1[02])(0[0-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[0-9]|[12]\d|30))|((\d{2})02(0[0-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))$", RegexOptions.None, "en-US")]
    private static partial Regex DatePatternZerosRegex();

    /// <summary>
    ///     Returns a regular expression for matching 6-digit trade measures.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{6}$", RegexOptions.None, "en-US")]
    private static partial Regex SixDigitTradeMeasureRegex();

    /// <summary>
    ///     Returns a regular expression for matching 6-digit logistics measures.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{6}$", RegexOptions.None, "en-US")]
    private static partial Regex SixDigitLogisticsMeasureRegex();

    /// <summary>
    ///     Returns a regular expression for matching 6-digit monetary values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{6}$", RegexOptions.None, "en-US")]
    private static partial Regex SixDigitMonetaryValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching 2-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{2}$", RegexOptions.None, "en-US")]
    private static partial Regex TwoDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching 3-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{3}$", RegexOptions.None, "en-US")]
    private static partial Regex ThreeDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching 4-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{4}$", RegexOptions.None, "en-US")]
    private static partial Regex FourDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching 6-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{6}$", RegexOptions.None, "en-US")]
    private static partial Regex SixDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching 13-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{13}$", RegexOptions.None, "en-US")]
    private static partial Regex ThirteenDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching 14-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{14}$", RegexOptions.None, "en-US")]
    private static partial Regex FourteenDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching 17-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{17}$", RegexOptions.None, "en-US")]
    private static partial Regex SeventeenDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching 18-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{18}$", RegexOptions.None, "en-US")]
    private static partial Regex EighteenDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching up to 4-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{1,4}$", RegexOptions.None, "en-US")]
    private static partial Regex MaxFourDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching up to 6-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{1,6}$", RegexOptions.None, "en-US")]
    private static partial Regex MaxSixDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching up to 8-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{1,8}$", RegexOptions.None, "en-US")]
    private static partial Regex MaxEightDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching up to 10-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{1,10}$", RegexOptions.None, "en-US")]
    private static partial Regex MaxTenDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching up to 12-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{1,12}$", RegexOptions.None, "en-US")]
    private static partial Regex MaxTwelveDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching up to 15-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{1,15}$", RegexOptions.None, "en-US")]
    private static partial Regex MaxFifteenDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching up to 4-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{4,20}$", RegexOptions.None, "en-US")]
    private static partial Regex MinFourMaxTwentyDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching up to 15-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{3}\d{1,15}$", RegexOptions.None, "en -US")]
    private static partial Regex MaxFifteenDigitAmountWithIsoCodeRegex();

    /// <summary>
    ///     Returns a regular expression for matching up to 15-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^(\d{3}){1,5}$", RegexOptions.None, "en -US")]
    private static partial Regex MaxFiveIsoCountryCodesRegex();

    /// <summary>
    ///     Returns a regular expression for matching a binary flag (1 or 0).
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^[01]$", RegexOptions.None, "en-US")]
    private static partial Regex BinaryFlagRegex();

    /// <summary>
    ///     A regular expression for six-digit date representation - YYMMDD.
    /// </summary>
    [GeneratedRegex(@"(((\d{2})(0[13578]|1[02])(0[1-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[1-9]|[12]\d|30))|((\d{2})02(0[1-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))", RegexOptions.None, "en-US")]
    private static partial Regex DatePatternRegex();

    /// <summary>
    ///     Returns a regular expression for ten-digit date and time representation - YYMMDDHHMM.
    /// </summary>
    [GeneratedRegex(@"(((\d{2})(0[13578]|1[02])(0[1-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[1-9]|[12]\d|30))|((\d{2})02(0[1-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))((0\d|1\d|2[0-3])([0-5]\d))", RegexOptions.None, "en-US")]
    private static partial Regex DateTimePatternRegex();

    /// <summary>
    ///     Returns a regular expression for ten-digit date and time representation - YYMMDDHHMM.
    ///     The time copmponent is optional.
    /// </summary>
    [GeneratedRegex(@"(((\d{2})(0[13578]|1[02])(0[1-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[1-9]|[12]\d|30))|((\d{2})02(0[1-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))((0\d|1\d|2[0-3])([0-5]\d))?", RegexOptions.None, "en-US")]
    private static partial Regex DateOptionalTimePatternRegex();

    /// <summary>
    ///     Returns a regular expression for the harvest date.
    ///     The second (end) date is optional.
    /// </summary>
    [GeneratedRegex(@"((0\d|1[012])([0-5]\d))((0\d|1[012])([0-5]\d))?", RegexOptions.None, "en-US")]
    private static partial Regex HarvestDateRegex();

    /// <summary>
    ///     Returns a regular expression for ten-digit date and time representation - YYMMDDHHMM.
    ///     If it is not necessary to specify the day, the day field can be filled with two zeros.
    ///     If it is not necessary to specify a time, the hour and minutes are filled with 9s.
    /// </summary>
    [GeneratedRegex(@"(((\d{2})(0[13578]|1[02])(0[0-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[0-9]|[12]\d|30))|((\d{2})02(0[0-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))(((0\d|1\d|2[0-3])([0-5]\d))|9999)", RegexOptions.None, "en-US")]
    private static partial Regex DateTimePatternZerosAnd9SRegex();

    /// <summary>
    ///     Returns a regular expression for the date and time of production:.
    ///     If it is not necessary to specify the minutes or the seconds.
    /// </summary>
    [GeneratedRegex(@"(((\d{2})(0[13578]|1[02])(0[0-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[0-9]|[12]\d|30))|((\d{2})02(0[0-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))(((0\d|1\d|2[0-3])([0-5]\d)?([0-5]\d)?))", RegexOptions.None, "en-US")]
    private static partial Regex DateAndTimeOfProductionRegex();
#else
internal static class EntityResolver {
    
    /// <summary>
    ///     Returns a regular expression for matching a Serial Shipping Container Code.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex SsccRegex = new(@"^\d{18}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a Global Trade Item Number.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex GtinRegex= new(@"^\d{14}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a Global Document Type Identifier.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex GdtiRegex= new("""^\d{13}([-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,17})?$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a Global Coupon Number.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex GcnRegex= new(@"^\d{13}(\d{1,12})?$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a Global Returnable Asset Identifier (GRAI).
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex GraiRegex= new("""^0\d{13}([-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,16})?$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for an identification of an individual trade item piece (ITIP).
    /// </summary>
    private static readonly Regex ItipRegex= new(@"^\d{14}\d{2}\d{2}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-2 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet8202CharsRegex= new("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,2}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 2-2 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet820202CharsRegex= new("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{2,2}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-3 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet8203CharsRegex= new("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,3}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-10 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet8210CharsRegex= new("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,10}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-12 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet8212CharsRegex= new("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,12}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-20 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet8220CharsRegex= new("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,20}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-25 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet8225CharsRegex= new("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,25}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-28 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet8228CharsRegex= new("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,28}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 3-30 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet820330CharsRegex= new("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{3,30}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-30 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet8230CharsRegex= new("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,30}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-30 character value of characters taken from Character Set 39.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet3930CharsRegex= new(@"^[#-/0-9A-Z]{1,30}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-34 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet8234CharsRegex= new("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,34}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-35 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet8235CharsRegex= new("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,35}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-50 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet8250CharsRegex= new("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,50}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-70 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet8270CharsRegex= new("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,70}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-90 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex CharacterSet8290CharsRegex= new("""^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,90}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a processor with an ISO country code.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex ProcessorWithIsoCountryCodeRegex= new("""^\d{3}[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,27}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a processor with an ISO country code.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex PostalCodeWithIsoCountryCodeRegex= new("""^\d{3}[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,9}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a GS1 UIC with Extension 1 and Imported index.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex Gs1UicWithExtension1AndImportedIndexRegex= new("""^\d[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{3,3}$""", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for six-digit date representation - YYMMDD.
    ///     If it is not necessary to specify the day, the day field can be filled with two zeros.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex DatePatternZerosRegex= new(@"^(((\d{2})(0[13578]|1[02])(0[0-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[0-9]|[12]\d|30))|((\d{2})02(0[0-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 6-digit trade measures.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex SixDigitTradeMeasureRegex= new(@"^\d{6}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 6-digit logistics measures.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex SixDigitLogisticsMeasureRegex= new(@"^\d{6}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 6-digit monetary values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex SixDigitMonetaryValueRegex= new(@"^\d{6}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 2-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex TwoDigitValueRegex= new(@"^\d{2}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 3-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex ThreeDigitValueRegex= new(@"^\d{3}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 4-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex FourDigitValueRegex= new(@"^\d{4}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 6-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex SixDigitValueRegex= new(@"^\d{6}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 13-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex ThirteenDigitValueRegex= new(@"^\d{13}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 14-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex FourteenDigitValueRegex= new(@"^\d{14}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 17-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex SeventeenDigitValueRegex= new(@"^\d{17}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 18-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex EighteenDigitValueRegex= new(@"^\d{18}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 4-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex MaxFourDigitValueRegex= new(@"^\d{1,4}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 6-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex MaxSixDigitValueRegex= new(@"^\d{1,6}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 8-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex MaxEightDigitValueRegex= new(@"^\d{1,8}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 10-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex MaxTenDigitValueRegex= new(@"^\d{1,10}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 12-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex MaxTwelveDigitValueRegex= new(@"^\d{1,12}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 15-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex MaxFifteenDigitValueRegex= new(@"^\d{1,15}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 4-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex MinFourMaxTwentyDigitValueRegex= new(@"^\d{4,20}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 15-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex MaxFifteenDigitAmountWithIsoCodeRegex= new(@"^\d{3}\d{1,15}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 15-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex MaxFiveIsoCountryCodesRegex= new(@"^(\d{3}){1,5}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a binary flag (1 or 0).
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex BinaryFlagRegex= new(@"^[01]$", RegexOptions.None);

    /// <summary>
    ///     A regular expression for six-digit date representation - YYMMDD.
    /// </summary>
    private static readonly Regex DatePatternRegex= new(@"(((\d{2})(0[13578]|1[02])(0[1-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[1-9]|[12]\d|30))|((\d{2})02(0[1-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for ten-digit date and time representation - YYMMDDHHMM.
    /// </summary>
    private static readonly Regex DateTimePatternRegex= new(@"(((\d{2})(0[13578]|1[02])(0[1-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[1-9]|[12]\d|30))|((\d{2})02(0[1-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))((0\d|1\d|2[0-3])([0-5]\d))", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for ten-digit date and time representation - YYMMDDHHMM.
    ///     The time copmponent is optional.
    /// </summary>
    private static readonly Regex DateOptionalTimePatternRegex= new(@"(((\d{2})(0[13578]|1[02])(0[1-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[1-9]|[12]\d|30))|((\d{2})02(0[1-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))((0\d|1\d|2[0-3])([0-5]\d))?", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for the harvest date.
    ///     The second (end) date is optional.
    /// </summary>
    private static readonly Regex HarvestDateRegex= new(@"((0\d|1[012])([0-5]\d))((0\d|1[012])([0-5]\d))?", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for ten-digit date and time representation - YYMMDDHHMM.
    ///     If it is not necessary to specify the day, the day field can be filled with two zeros.
    ///     If it is not necessary to specify a time, the hour and minutes are filled with 9s.
    /// </summary>
    private static readonly Regex DateTimePatternZerosAnd9SRegex= new(@"(((\d{2})(0[13578]|1[02])(0[0-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[0-9]|[12]\d|30))|((\d{2})02(0[0-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))(((0\d|1\d|2[0-3])([0-5]\d))|9999)", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for the date and time of production:.
    ///     If it is not necessary to specify the minutes or the seconds.
    /// </summary>
    private static readonly Regex DateAndTimeOfProductionRegex= new(@"(((\d{2})(0[13578]|1[02])(0[0-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[0-9]|[12]\d|30))|((\d{2})02(0[0-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))(((0\d|1\d|2[0-3])([0-5]\d)?([0-5]\d)?))", RegexOptions.None);
#endif

    /// <summary>
    ///     A dictionary of application identifier descriptors.
    /// </summary>
    private static readonly IDictionary<int, EntityDescriptor> Descriptors =
        new Dictionary<int, EntityDescriptor>
        {
            {
                0, new IdentifierWithFinalChecksumDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "SSCC",
                    "Identification of a logistic unit (SSCC)",
#if NET7_0_OR_GREATER
                    SsccRegex(),
#else 
                    SsccRegex,
#endif
                    true)
            },
            {
                1,
                new IdentifierWithFinalChecksumDescriptor(
                    "GTIN",
                    "Identification of a trade item (GTIN)",
#if NET7_0_OR_GREATER
                    GtinRegex(),
#else 
                    GtinRegex,
#endif
                    true)
            },
            {
                2, new IdentifierWithFinalChecksumDescriptor(
                    "CONTENT",
                    "Identification of trade items contained in a logistic unit",
#if NET7_0_OR_GREATER
                    GtinRegex(),
#else 
                    GtinRegex,
#endif
                    true)
            },
            {
                10, new EntityDescriptor(
                    "BATCH/LOT",
                    "Batch or lot number",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                11, new EntityDescriptor(
                    "PROD DATE",

                    // ReSharper disable once StringLiteralTypo
                    "Production date",
#if NET7_0_OR_GREATER
                    DatePatternZerosRegex(),
#else 
                    DatePatternZerosRegex,
#endif
                    true)
            },
            {
                12, new EntityDescriptor(
                    "DUE DATE",

                    // ReSharper disable once StringLiteralTypo
                    "Due date for amount on payment slip",
#if NET7_0_OR_GREATER
                    DatePatternZerosRegex(),
#else 
                    DatePatternZerosRegex,
#endif
                    true)
            },
            {
                13, new EntityDescriptor(
                    "PACK DATE",

                    // ReSharper disable once StringLiteralTypo
                    "Packaging date",
#if NET7_0_OR_GREATER
                    DatePatternZerosRegex(),
#else 
                    DatePatternZerosRegex,
#endif
                    true)
            },
            {
                15, new EntityDescriptor(
                    "BEST BEFORE or BEST BY",

                    // ReSharper disable once StringLiteralTypo
                    "Best before date",
#if NET7_0_OR_GREATER
                    DatePatternZerosRegex(),
#else 
                    DatePatternZerosRegex,
#endif
                    true)
            },
            {
                16, new EntityDescriptor(
                    "SELL BY",

                    // ReSharper disable once StringLiteralTypo
                    "Sell by date",
#if NET7_0_OR_GREATER
                    DatePatternZerosRegex(),
#else 
                    DatePatternZerosRegex,
#endif
                    true)
            },
            {
                17, new EntityDescriptor(
                    "USE BY OR EXPIRY",

                    // ReSharper disable once StringLiteralTypo
                    "Expiration date",
#if NET7_0_OR_GREATER
                    DatePatternZerosRegex(),
#else 
                    DatePatternZerosRegex,
#endif
                    true)
            },
            {
                20,
                new EntityDescriptor(
                    "VARIANT",
                    "Internal product variant",
#if NET7_0_OR_GREATER
                    TwoDigitValueRegex(),
#else 
                    TwoDigitValueRegex,
#endif
                    true)
            },
            {
                21,
                new EntityDescriptor(
                    "SERIAL",
                    "Serial number",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                22, new EntityDescriptor(
                    "CPV",
                    "Consumer product variant",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                235, new EntityDescriptor(
                    "TPX",
                    "Third Party Controlled, Serialised Extension of Global Trade Item Number (GTIN) (TPX)",
#if NET7_0_OR_GREATER
                    CharacterSet8228CharsRegex(),
#else 
                    CharacterSet8228CharsRegex,
#endif
                    false)
            },
            {
                240, new EntityDescriptor(
                    "ADDITIONAL ID",
                    "Additional product identification assigned by the manufacturer",
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else 
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                241, new EntityDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "CUST. PART No.",
                    "Customer part number",
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else 
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                242,
                new EntityDescriptor(
                    "MTO VARIANT",
                    "Made-to-Order variation number",
#if NET7_0_OR_GREATER
                    MaxSixDigitValueRegex(),
#else 
                    MaxSixDigitValueRegex,
#endif
                    false)
            },
            {
                243, new EntityDescriptor(
                    "PCN",
                    "Packaging component number",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                250, new EntityDescriptor(
                    "SECONDARY SERIAL",
                    "Secondary serial number",
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else 
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                251, new EntityDescriptor(
                    "REF. TO SOURCE",
                    "Reference to source entity",
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else 
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                253, new IdentifierWithPos13ChecksumDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "GDTI",
                    "Global Document Type Identifier (GDTI)",
#if NET7_0_OR_GREATER
                    GdtiRegex(),
#else 
                    GdtiRegex,
#endif
                    false)
            },
            {
                254, new EntityDescriptor(
                    "GLN EXTENSION COMPONENT",
                    "Global Location Number (GLN) extension component",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                255, new IdentifierWithPos13ChecksumDescriptor(
                    "GCN",
                    "Global Coupon Number (GCN)",
#if NET7_0_OR_GREATER
                    GcnRegex(),
#else 
                    GcnRegex,
#endif
                    false)
            },
            {
                30, new EntityDescriptor(
                    "VAR. COUNT",
                    "Variable count of items",
#if NET7_0_OR_GREATER
                    MaxEightDigitValueRegex(),
#else 
                    MaxEightDigitValueRegex,
#endif
                    false)
            },
            {
                310, new EntityDescriptor(
                    "NET WEIGHT (kg)",
                    "Net weight, kilograms (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                311, new EntityDescriptor(
                    "LENGTH (m)",
                    "Length or first dimension, metres (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                312, new EntityDescriptor(
                    "WIDTH (m)",
                    "Width, diameter, or second dimension, metres (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                313, new EntityDescriptor(
                    "HEIGHT (m)",
                    "Depth, thickness, height, or third dimension, metres (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                314, new EntityDescriptor(
                    "AREA (m²)",
                    "Area, square metres (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                315, new EntityDescriptor(
                    "NET VOLUME (l)",
                    "Net volume, litres (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                316, new EntityDescriptor(
                    "NET VOLUME (m³)",
                    "Net volume, cubic metres (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                320, new EntityDescriptor(
                    "NET WEIGHT (lb)",
                    "Net weight, pounds (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                321, new EntityDescriptor(
                    "LENGTH (i)",
                    "Length or first dimension, inches (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                322, new EntityDescriptor(
                    "LENGTH (f)",
                    "Length or first dimension, feet (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                323, new EntityDescriptor(
                    "LENGTH (y)",
                    "Length or first dimension, yards (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                324, new EntityDescriptor(
                    "WIDTH (i)",
                    "Width, diameter, or second dimension, inches (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                325, new EntityDescriptor(
                    "WIDTH (f)",
                    "Width, diameter, or second dimension, feet (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                326, new EntityDescriptor(
                    "WIDTH (y)",
                    "Width, diameter, or second dimension, yards (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                327, new EntityDescriptor(
                    "HEIGHT (i)",
                    "Depth, thickness, height, or third dimension, inches (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                328, new EntityDescriptor(
                    "HEIGHT (f)",
                    "Depth, thickness, height, or third dimension, feet (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                329, new EntityDescriptor(
                    "HEIGHT (y)",
                    "Depth, thickness, height, or third dimension, yards (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                330,
                new EntityDescriptor(
                    "GROSS WEIGHT (kg)",
                    "Logistic weight, kilograms",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                331,
                new EntityDescriptor(
                    "LENGTH (m), log",
                    "Length or first dimension, metres",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                332, new EntityDescriptor(
                    "WIDTH (m), log",
                    "Width, diameter, or second dimension, metres",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                333, new EntityDescriptor(
                    "HEIGHT (m), log",
                    "Depth, thickness, height, or third dimension, metres",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                334,
                new EntityDescriptor(
                    "AREA (m²), log",
                    "Area, square metres",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                335,
                new EntityDescriptor(
                    "VOLUME (l), log",
                    "Logistic volume, litres",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                336,
                new EntityDescriptor(
                    "VOLUME (m³), log",
                    "Logistic volume, cubic metres",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                337,
                new EntityDescriptor(
                    "KG PER m²",
                    "Kilograms per square metre",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                340,
                new EntityDescriptor(
                    "GROSS WEIGHT (lb)",
                    "Logistic weight, pounds",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                341,
                new EntityDescriptor(
                    "LENGTH (i), log",
                    "Length or first dimension, inches",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                342,
                new EntityDescriptor(
                    "LENGTH (f), log",
                    "Length or first dimension, feet",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                343,
                new EntityDescriptor(
                    "LENGTH (y), log",
                    "Length or first dimension, yards",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                344, new EntityDescriptor(
                    "WIDTH (i), log",
                    "Width, diameter, or second dimension, inches",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                345, new EntityDescriptor(
                    "WIDTH (f), log",
                    "Width, diameter, or second dimension, feet",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                346, new EntityDescriptor(
                    "WIDTH (y), log",
                    "Width, diameter, or second dimension, yard",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                347, new EntityDescriptor(
                    "HEIGHT (i), log",
                    "Depth, thickness, height, or third dimension, inches",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                348, new EntityDescriptor(
                    "HEIGHT (f), log",
                    "Depth, thickness, height, or third dimension, feet",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                349, new EntityDescriptor(
                    "HEIGHT (y), log",
                    "Depth, thickness, height, or third dimension, yards",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                350, new EntityDescriptor(
                    "AREA (i²)",
                    "Area, square inches (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                351, new EntityDescriptor(
                    "AREA (f²)",
                    "Area, square feet (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                352, new EntityDescriptor(
                    "AREA (y²)",
                    "Area, square yards (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                353,
                new EntityDescriptor(
                    "AREA (i²), log",
                    "Area, square inches",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                354,
                new EntityDescriptor(
                    "AREA (f²), log",
                    "Area, square feet",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                355,
                new EntityDescriptor(
                    "AREA (y²), log",
                    "Area, square yards",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                356,
                new EntityDescriptor(
                    "NET WEIGHT (t)",
                    "Net weight, troy ounces (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                357,
                new EntityDescriptor(
                    "NET VOLUME (oz)",
                    "Net weight (or volume), ounces (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                360,
                new EntityDescriptor(
                    "NET VOLUME (q)",
                    "Net volume, quarts (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                361,
                new EntityDescriptor(
                    "NET VOLUME (g)",
                    "Net volume, gallons U.S. (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                362,
                new EntityDescriptor(
                    "VOLUME (q), log",
                    "Logistic volume, quarts",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                363,
                new EntityDescriptor(
                    "VOLUME (g), log",
                    "Logistic volume, gallons U.S.",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                364, new EntityDescriptor(
                    "VOLUME (i³)",
                    "Net volume, cubic inches (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                365, new EntityDescriptor(
                    "VOLUME (f³)",
                    "Net volume, cubic feet (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                366, new EntityDescriptor(
                    "VOLUME (y³)",
                    "Net volume, cubic yards (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else 
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                367,
                new EntityDescriptor(
                    "VOLUME (i³), log",
                    "Logistic volume, cubic inches",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                368,
                new EntityDescriptor(
                    "VOLUME (f³), log",
                    "Logistic volume, cubic feet",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                369,
                new EntityDescriptor(
                    "VOLUME (y³), log",
                    "Logistic volume, cubic yards",
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else 
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                37,
                new EntityDescriptor(
                    "COUNT",
                    "Count of trade items or trade item pieces contained in a logistic unit",
#if NET7_0_OR_GREATER
                    MaxEightDigitValueRegex(),
#else 
                    MaxEightDigitValueRegex,
#endif
                    false)
            },
            {
                390,
                new EntityDescriptor(
                    "AMOUNT",
                    "Amount payable or coupon value - Single monetary area",
#if NET7_0_OR_GREATER
                    MaxFifteenDigitValueRegex(),
#else 
                    MaxFifteenDigitValueRegex,
#endif
                    false)
            },
            {
                391,
                new EntityDescriptor(
                    "AMOUNT",
                    "Amount payable and ISO currency code",
#if NET7_0_OR_GREATER
                    MaxFifteenDigitAmountWithIsoCodeRegex(),
#else 
                    MaxFifteenDigitAmountWithIsoCodeRegex,
#endif
                    false)
            },
            {
                392,
                new EntityDescriptor(
                    "PRICE",
                    "Amount payable for a variable measure trade item – Single monetary area",
#if NET7_0_OR_GREATER
                    MaxFifteenDigitValueRegex(),
#else 
                    MaxFifteenDigitValueRegex,
#endif
                    false)
            },
            {
                393,
                new EntityDescriptor(
                    "PRICE",
                    "Amount payable for a variable measure trade item and ISO currency code",
#if NET7_0_OR_GREATER
                    MaxFifteenDigitAmountWithIsoCodeRegex(),
#else 
                    MaxFifteenDigitAmountWithIsoCodeRegex,
#endif
                    false)
            },
            {
                394,
                new EntityDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "PRCNT OFF",
                    "Percentage discount of a coupon",
#if NET7_0_OR_GREATER
                    FourDigitValueRegex(),
#else 
                    FourDigitValueRegex,
#endif
                    true)
            },
            {
                395,
                new EntityDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "PRICE/UoM",
                    "Amount payable per unit of measure single monetary area (variable measure trade item)",
#if NET7_0_OR_GREATER
                    SixDigitMonetaryValueRegex(),
#else 
                    SixDigitMonetaryValueRegex,
#endif
                    true)
            },
            {
                400,
                new EntityDescriptor(
                    "ORDER NUMBER",
                    "Customer’s purchase order number",
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else 
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                401,
                new EntityDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "GINC",

                    // ReSharper disable once StringLiteralTypo
                    "Global Identification Number for Consignment (GINC)",
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else 
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                402,
                new EntityDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "GSIN",

                    // ReSharper disable once StringLiteralTypo
                    "Global Shipment Identification Number (GSIN)",
#if NET7_0_OR_GREATER
                    SeventeenDigitValueRegex(),
#else 
                    SeventeenDigitValueRegex,
#endif
                    true)
            },
            {
                403,
                new EntityDescriptor(
                    "ROUTE",
                    "Routing code",
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else 
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                410, new IdentifierWithFinalChecksumDescriptor(
                    "SHIP TO LOC",
                    "Ship to - Deliver to Global Location Number (GLN)",
#if NET7_0_OR_GREATER
                    ThirteenDigitValueRegex(),
#else 
                    ThirteenDigitValueRegex,
#endif
                    true)
            },
            {
                411, new IdentifierWithFinalChecksumDescriptor(
                    "BILL TO",
                    "Bill to - Invoice to Global Location Number (GLN)",
#if NET7_0_OR_GREATER
                    ThirteenDigitValueRegex(),
#else 
                    ThirteenDigitValueRegex,
#endif
                    true)
            },
            {
                412, new IdentifierWithFinalChecksumDescriptor(
                    "PURCHASE FROM",
                    "Purchased from Global Location Number (GLN)",
#if NET7_0_OR_GREATER
                    ThirteenDigitValueRegex(),
#else 
                    ThirteenDigitValueRegex,
#endif
                    true)
            },
            {
                413, new IdentifierWithFinalChecksumDescriptor(
                    "SHIP FOR LOC",
                    "Ship for - Deliver for - Forward to Global Location Number (GLN)",
#if NET7_0_OR_GREATER
                    ThirteenDigitValueRegex(),
#else 
                    ThirteenDigitValueRegex,
#endif
                    true)
            },
            {
                414, new IdentifierWithFinalChecksumDescriptor(
                    "LOC No.",
                    "Identification of a physical location - Global Location Number (GLN)",
#if NET7_0_OR_GREATER
                    ThirteenDigitValueRegex(),
#else 
                    ThirteenDigitValueRegex,
#endif
                    true)
            },
            {
                415, new IdentifierWithFinalChecksumDescriptor(
                    "PAY TO",
                    "Global Location Number (GLN) of the invoicing party",
#if NET7_0_OR_GREATER
                    ThirteenDigitValueRegex(),
#else 
                    ThirteenDigitValueRegex,
#endif
                    true)
            },
            {
                416, new IdentifierWithFinalChecksumDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "PROD/SERV LOC",
                    "Global Location Number (GLN) of the production or service location",
#if NET7_0_OR_GREATER
                    ThirteenDigitValueRegex(),
#else 
                    ThirteenDigitValueRegex,
#endif
                    true)
            },
            {
                417, new IdentifierWithFinalChecksumDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "PARTY",
                    "Party Global Location Number (GLN)",
#if NET7_0_OR_GREATER
                    ThirteenDigitValueRegex(),
#else 
                    ThirteenDigitValueRegex,
#endif
                    true)
            },
            {
                420, new EntityDescriptor(
                    "SHIP TO POST",
                    "Ship-to / Deliver-to postal code within a single postal authority",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                421, new EntityDescriptor(
                    "SHIP TO POST",
                    "Ship-to / Deliver-to postal code with three-digit ISO country code",
#if NET7_0_OR_GREATER
                    PostalCodeWithIsoCountryCodeRegex(),
#else 
                    PostalCodeWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                422,
                new EntityDescriptor(
                    "ORIGIN",
                    "Country of origin of a trade item",
#if NET7_0_OR_GREATER
                    ThreeDigitValueRegex(),
#else 
                    ThreeDigitValueRegex,
#endif
                    true)
            },
            {
                423, new EntityDescriptor(
                    "COUNTRY - INITIAL PROCESS",
                    "Country of initial processing",
#if NET7_0_OR_GREATER
                    MaxFiveIsoCountryCodesRegex(),
#else 
                    MaxFiveIsoCountryCodesRegex,
#endif
                    false)
            },
            {
                424,
                new EntityDescriptor(
                    "COUNTRY - PROCESS",
                    "Country of processing",
#if NET7_0_OR_GREATER
                    ThreeDigitValueRegex(),
#else 
                    ThreeDigitValueRegex,
#endif
                    true)
            },
            {
                425, new EntityDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "COUNTRY - DISASSEMBLY",

                    // ReSharper disable once StringLiteralTypo
                    "Country of disassembly",
#if NET7_0_OR_GREATER
                    MaxFiveIsoCountryCodesRegex(),
#else 
                    MaxFiveIsoCountryCodesRegex,
#endif
                    false)
            },
            {
                426, new EntityDescriptor(
                    "COUNTRY – FULL PROCESS",
                    "Country covering full process chain",
#if NET7_0_OR_GREATER
                    ThreeDigitValueRegex(),
#else 
                    ThreeDigitValueRegex,
#endif
                    true)
            },
            {
                427, new EntityDescriptor(
                    "ORIGIN SUBDIVISION",
                    "Country subdivision of origin code for a trade item",
#if NET7_0_OR_GREATER
                    CharacterSet8203CharsRegex(),
#else 
                    CharacterSet8203CharsRegex,
#endif
                    false)
            },
            {
                4300,
                new EntityDescriptor(
                    "SHIP TO COMP",
                    "Ship-to / Deliver-to Company name",
#if NET7_0_OR_GREATER
                    CharacterSet8235CharsRegex(),
#else 
                    CharacterSet8235CharsRegex,
#endif
                    false)
            },
            {
                4301,
                new EntityDescriptor("SHIP TO NAME",
                    "Ship-to / Deliver-to contact name",
#if NET7_0_OR_GREATER
                    CharacterSet8235CharsRegex(),
#else 
                    CharacterSet8235CharsRegex,
#endif
                    false)
            },
            {
                4302,
                new EntityDescriptor("SHIP TO ADD1",
                    "Ship-to / Deliver-to address line 1",
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else 
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                4303,
                new EntityDescriptor("SHIP TO ADD2",
                    "Ship-to / Deliver-to address line 2",
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else 
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                4304,
                new EntityDescriptor("SHIP TO SUB",
                    "Ship-to / Deliver-to suburb",
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else 
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                4305,
                new EntityDescriptor("SHIP TO LOC",
                    "Ship-to / Deliver-to locality",
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else 
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                4306,
                new EntityDescriptor("SHIP TO REG",
                    "Ship-to / Deliver-to region",
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else 
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                4307,
                new EntityDescriptor("SHIP TO COUNTRY",
                    "Ship-to / Deliver-to country code",
#if NET7_0_OR_GREATER
                    CharacterSet820202CharsRegex(),
#else 
                    CharacterSet820202CharsRegex,
#endif
                    true)
            },
            {
                4308,
                new EntityDescriptor("SHIP TO PHONE",
                    "Ship-to / Deliver-to telephone number",
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else 
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                4309,
                new EntityDescriptor("SHIP TO GEO",
                    "Ship-to / Deliver-to GEO location",
#if NET7_0_OR_GREATER
                    MinFourMaxTwentyDigitValueRegex(),
#else 
                    MinFourMaxTwentyDigitValueRegex,
#endif
                    true)
            },
            {
                4310,
                new EntityDescriptor("RTN TO COMP",
                    "Return-to company name",
#if NET7_0_OR_GREATER
                    CharacterSet8235CharsRegex(),
#else 
                    CharacterSet8235CharsRegex,
#endif
                    false)
            },
            {
                4311,
                new EntityDescriptor("RTN TO NAME",
                    "Return-to contact name",
#if NET7_0_OR_GREATER
                    CharacterSet8235CharsRegex(),
#else 
                    CharacterSet8235CharsRegex,
#endif
                    false)
            },
            {
                4312,
                new EntityDescriptor("RTN TO ADD1",
                    "Return-to address line 1",
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else 
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                4313,
                new EntityDescriptor("RTN TO ADD2",
                    "Return-to address line 2",
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else 
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                4314,
                new EntityDescriptor("RTN TO SUB",
                    "Return-to suburb",
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else 
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                4315,
                new EntityDescriptor("RTN TO LOC",
                    "Return-to locality",
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else 
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                4316,
                new EntityDescriptor("RTN TO REG",
                    "Return-to region",
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else 
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                4317,
                new EntityDescriptor("RTN TO COUNTRY",
                    "Return-to country code",
#if NET7_0_OR_GREATER
                    CharacterSet820202CharsRegex(),
#else 
                    CharacterSet820202CharsRegex,
#endif
                    true)
            },
            {
                4318,
                new EntityDescriptor("RTN TO POST",
                    "Return-to postal code",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                4319,
                new EntityDescriptor("RTN TO PHONE",
                    "Return-to telephone number",
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else 
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                4320,
                new EntityDescriptor("SRV DESCRIPTION",
                    "Service code description",
#if NET7_0_OR_GREATER
                    CharacterSet8235CharsRegex(),
#else 
                    CharacterSet8235CharsRegex,
#endif
                    false)
            },
            {
                4321,
                new EntityDescriptor("DANGEROUS GOODS",
                    "Dangerous goods flag",
#if NET7_0_OR_GREATER
                    BinaryFlagRegex(),
#else 
                    BinaryFlagRegex,
#endif
                    true)
            },
            {
                4322,
                new EntityDescriptor("AUTH LEAVE",
                    "Authority to leave flag",
#if NET7_0_OR_GREATER
                    BinaryFlagRegex(),
#else 
                    BinaryFlagRegex,
#endif
                    true)
            },
            {
                4323,
                new EntityDescriptor("SIG REQUIRED",
                    "Signature required flag",
#if NET7_0_OR_GREATER
                    BinaryFlagRegex(),
#else 
                    BinaryFlagRegex,
#endif
                    true)
            },
            {
                4324,
                new EntityDescriptor("NBEF DEL DT",
                    "Not before delivery date/time",
#if NET7_0_OR_GREATER
                    DateTimePatternZerosAnd9SRegex(),
#else 
                    DateTimePatternZerosAnd9SRegex,
#endif
                    true)
            },
            {
                4325,
                new EntityDescriptor(
                    "NAFT DEL DT",
                    "Not after delivery date/time",
#if NET7_0_OR_GREATER
                    DateTimePatternZerosAnd9SRegex(),
#else 
                    DateTimePatternZerosAnd9SRegex,
#endif
                    true)
            },
            {
                4326,
                new EntityDescriptor(
                    "REL DATE",
                    "Release date",
#if NET7_0_OR_GREATER
                    DatePatternRegex(),
#else 
                    DatePatternRegex,
#endif
                    true)
            },
            {
                7001,
                new EntityDescriptor(
                    "NSN",
                    "NATO Stock Number (NSN)",
#if NET7_0_OR_GREATER
                    ThirteenDigitValueRegex(),
#else 
                    ThirteenDigitValueRegex,
#endif
                    true)
            },
            {
                7002, new EntityDescriptor(
                    "MEAT CUT",
                    "UN/ECE meat carcasses and cuts classification",
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else 
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                7003,
                new EntityDescriptor(
                    "EXPIRY TIME",
                    "Expiration date and time",
#if NET7_0_OR_GREATER
                    DateTimePatternRegex(),
#else 
                    DateTimePatternRegex,
#endif
                    true) },
            {
                7004,
                new EntityDescriptor(
                    "ACTIVE POTENCY",
                    "Active potency",
#if NET7_0_OR_GREATER
                    MaxFourDigitValueRegex(),
#else 
                    MaxFourDigitValueRegex,
#endif
                    false) },
            {
                7005,
                new EntityDescriptor(
                    "CATCH AREA",
                    "Catch area",
#if NET7_0_OR_GREATER
                    CharacterSet8212CharsRegex(),
#else 
                    CharacterSet8212CharsRegex,
#endif
                    false) },
            {
                7006,
                new EntityDescriptor(
                    "FIRST FREEZE DATE",
                    "First freeze date",
#if NET7_0_OR_GREATER
                    DatePatternRegex(),
#else 
                    DatePatternRegex,
#endif
                    true) },
            {
                7007,
                new EntityDescriptor(
                    "HARVEST DATE",
                    "Harvest date",
#if NET7_0_OR_GREATER
                    HarvestDateRegex(),
#else 
                    HarvestDateRegex,
#endif
                    false)
            },
            {
                7008,
                new EntityDescriptor(
                    "AQUATIC SPECIES",
                    "Species for fishery purposes",
#if NET7_0_OR_GREATER
                    CharacterSet8203CharsRegex(),
#else 
                    CharacterSet8203CharsRegex,
#endif
                    false)
            },
            {
                7009,
                new EntityDescriptor(
                    "FISHING GEAR TYPE",
                    "Fishing gear type",
#if NET7_0_OR_GREATER
                    CharacterSet8210CharsRegex(),
#else 
                    CharacterSet8210CharsRegex,
#endif
                    false)
            },
            {
                7010,
                new EntityDescriptor(
                    "PROD METHOD",
                    "Production method",
#if NET7_0_OR_GREATER
                    CharacterSet8202CharsRegex(),
#else 
                    CharacterSet8202CharsRegex,
#endif
                    false)
            },
            {
                7011,
                new EntityDescriptor(
                    "TEST BY DATE",
                    "Test by date",
#if NET7_0_OR_GREATER
                    DateOptionalTimePatternRegex(),
#else 
                    DateOptionalTimePatternRegex,
#endif
                    false)
            },
            {
                7020,
                new EntityDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "REFURB LOT",
                    "Refurbishment lot ID",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                7021,
                new EntityDescriptor(
                    "FUNC STAT",
                    "Functional status",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                7022,
                new EntityDescriptor(
                    "REV STAT",
                    "Revision status",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                7023,
                new EntityDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "GIAI – ASSEMBLY",

                    // ReSharper disable once StringLiteralTypo
                    "Global Individual Asset Identifier of an assembly",
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else 
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                703,
                new EntityDescriptor(
                    "PROCESSOR # s",
                    "Number of processor with three-digit ISO country code",
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else 
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7031,
                new EntityDescriptor(
                    "PROCESSOR # 1",
                    "Number of processor with ISO Country Code",
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else 
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7032,
                new EntityDescriptor(
                    "PROCESSOR # 2",
                    "Number of processor with ISO Country Code",
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else 
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7033,
                new EntityDescriptor(
                    "PROCESSOR # 3",
                    "Number of processor with ISO Country Code",
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else 
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7034,
                new EntityDescriptor(
                    "PROCESSOR # 4",
                    "Number of processor with ISO Country Code",
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else 
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7035,
                new EntityDescriptor(
                    "PROCESSOR # 5",
                    "Number of processor with ISO Country Code",
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else 
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7036,
                new EntityDescriptor(
                    "PROCESSOR # 6",
                    "Number of processor with ISO Country Code",
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else 
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7037,
                new EntityDescriptor(
                    "PROCESSOR # 7",
                    "Number of processor with ISO Country Code",
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else 
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7038,
                new EntityDescriptor(
                    "PROCESSOR # 8",
                    "Number of processor with ISO Country Code",
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else 
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7039,
                new EntityDescriptor(
                    "PROCESSOR # 9",
                    "Number of processor with ISO Country Code",
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else 
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7040,
                new EntityDescriptor(
                    "UIC+EXT",
                    "GS1 UIC with Extension 1 and Importer index",
#if NET7_0_OR_GREATER
                    Gs1UicWithExtension1AndImportedIndexRegex(),
#else 
                    Gs1UicWithExtension1AndImportedIndexRegex,
#endif
                    true)
            },
            {
                710,
                new EntityDescriptor(
                    "NHRN PZN",
                    "National Healthcare Reimbursement Number (NHRN) – Germany PZN",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                711,
                new EntityDescriptor(
                    "NHRN CIP",
                    "National Healthcare Reimbursement Number (NHRN) – France CIP",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                712,
                new EntityDescriptor(
                    "NHRN CN",
                    "National Healthcare Reimbursement Number (NHRN) – Spain CN",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                713,
                new EntityDescriptor(
                    "NHRN DRN",

                    // ReSharper disable once StringLiteralTypo
                    "National Healthcare Reimbursement Number (NHRN) – Brasil DRN",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                714,
                new EntityDescriptor(
                    "NHRN AIM",
                    "National Healthcare Reimbursement Number (NHRN) – Portugal AIM",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                715,
                new EntityDescriptor(
                    "NHRN NDC",
                    "National Healthcare Reimbursement Number (NHRN) – United States of America NDC",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                723,
                new EntityDescriptor(
                    "CERT # s",
                    "Certification reference",
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else 
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7231,
                new EntityDescriptor(
                    "CERT # 1",
                    "Certification reference",
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else 
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7232,
                new EntityDescriptor(
                    "CERT # 2",
                    "Certification reference",
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else 
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7233,
                new EntityDescriptor(
                    "CERT # 3",
                    "Certification reference",
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else 
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7234,
                new EntityDescriptor(
                    "CERT # 4",
                    "Certification reference",
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else 
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7235,
                new EntityDescriptor(
                    "CERT # 5",
                    "Certification reference",
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else 
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7236,
                new EntityDescriptor(
                    "CERT # 6",
                    "Certification reference",
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else 
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7237,
                new EntityDescriptor(
                    "CERT # 7",
                    "Certification reference",
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else 
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7238,
                new EntityDescriptor(
                    "CERT # 8",
                    "Certification reference",
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else 
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7239,
                new EntityDescriptor(
                    "CERT # 9",
                    "Certification reference",
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else 
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7240,
                new EntityDescriptor(
                    "PROTOCOL",
                    "Protocol ID",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                8001,
                new EntityDescriptor(
                    "DIMENSIONS",
                    "Roll products - width, length, core diameter, direction, splices",
#if NET7_0_OR_GREATER
                    FourteenDigitValueRegex(),
#else 
                    FourteenDigitValueRegex,
#endif
                    true)
            },
            {
                8002,
                new EntityDescriptor(
                    "CMT No.",
                    "Cellular mobile telephone identifier",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                8003,
                new IdentifierWithPos14ChecksumDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "GRAI",

                    // ReSharper disable once StringLiteralTypo
                    "Global Returnable Asset Identifier (GRAI)",
#if NET7_0_OR_GREATER
                    GraiRegex(),
#else 
                    GraiRegex,
#endif
                    false)
            },
            {
                8004,
                new EntityDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "GIAI",

                    // ReSharper disable once StringLiteralTypo
                    "Global Individual Asset Identifier (GIAI)",
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else 
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                8005,
                new EntityDescriptor(
                    "PRICE PER UNIT",
                    "Price per unit of measure",
#if NET7_0_OR_GREATER
                    SixDigitValueRegex(),
#else 
                    SixDigitValueRegex,
#endif
                    true)
            },
            {
                8006,
                new EntityDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "ITIP",
                    "Identification of an individual trade item (ITIP) piece",
#if NET7_0_OR_GREATER
                    ItipRegex(),
#else 
                    ItipRegex,
#endif
                    true)
            },
            {
                8007,
                new EntityDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "IBAN",

                    // ReSharper disable once StringLiteralTypo
                    "International Bank Account Number (IBAN)",
#if NET7_0_OR_GREATER
                    CharacterSet8234CharsRegex(),
#else 
                    CharacterSet8234CharsRegex,
#endif
                    false)
            },
            {
                8008,
                new EntityDescriptor(
                    "PROD TIME",
                    "Date and time of production",
#if NET7_0_OR_GREATER
                    DateAndTimeOfProductionRegex(),
#else 
                    DateAndTimeOfProductionRegex,
#endif
                    false) },
            {
                8009,
                new EntityDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "OPTSEN",
                    "Optically readable sensor indicator",
#if NET7_0_OR_GREATER
                    CharacterSet8250CharsRegex(),
#else 
                    CharacterSet8250CharsRegex,
#endif
                    false)
            },
            {
                8010,
                new EntityDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "CPID",
                    "Component/Part Identifier (CPID)",
#if NET7_0_OR_GREATER
                    CharacterSet3930CharsRegex(),
#else 
                    CharacterSet3930CharsRegex,
#endif
                    false)
            },
            {
                8011,
                new EntityDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "CPID SERIAL",

                    // ReSharper disable once StringLiteralTypo
                    "Component/Part Identifier serial number",
#if NET7_0_OR_GREATER
                    MaxTwelveDigitValueRegex(),
#else 
                    MaxTwelveDigitValueRegex,
#endif
                    false)
            },
            {
                8012,
                new EntityDescriptor(
                    "VERSION",
                    "Software version",
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else 
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                8013,
                new EntityDescriptor(
                    "GMN",
                    "Global Model Number (GMN)",
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else 
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                8017,
                new IdentifierWithFinalChecksumDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "GSRN - PROVIDER",
                    "Global Service Relation Number (GSRN) to identify the relationship between an organisation offering services and the provider of services",
#if NET7_0_OR_GREATER
                    EighteenDigitValueRegex(),
#else 
                    EighteenDigitValueRegex,
#endif
                    true)
            },
            {
                8018,
                new IdentifierWithFinalChecksumDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "GSRN - RECIPIENT",
                    "Global Service Relation Number (GSRN) to identify the relationship between an organisation offering services and the recipient of services",
#if NET7_0_OR_GREATER
                    EighteenDigitValueRegex(),
#else 
                    EighteenDigitValueRegex,
#endif
                    true)
            },
            {
                8019,
                new EntityDescriptor(

                    // ReSharper disable once StringLiteralTypo
                    "SRIN",

                    // ReSharper disable once StringLiteralTypo
                    "Service Relation Instance Number (SRIN)",
#if NET7_0_OR_GREATER
                    MaxTenDigitValueRegex(),
#else 
                    MaxTenDigitValueRegex,
#endif
                    false)
            },
            {
                8020,
                new EntityDescriptor(
                    "REF No.",
                    "Payment slip reference number",
#if NET7_0_OR_GREATER
                    CharacterSet8225CharsRegex(),
#else 
                    CharacterSet8225CharsRegex,
#endif
                    false)
            },
            {
                8026,
                new EntityDescriptor(
                    "ITIP CONTENT",
                    "Identification of pieces of a trade item (ITIP) contained in a logistic unit",
#if NET7_0_OR_GREATER
                    EighteenDigitValueRegex(),
#else 
                    EighteenDigitValueRegex,
#endif
                    true)
            },
            {
                8110,
                new EntityDescriptor(
                    "-",
                    "Coupon code identification for use in North America",
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else 
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                8111,
                new EntityDescriptor(
                    "POINTS",
                    "Loyalty points of a coupon",
#if NET7_0_OR_GREATER
                    FourDigitValueRegex(),
#else 
                    FourDigitValueRegex,
#endif
                    true)
            },
            {
                8112,
                new EntityDescriptor(
                    "-",
                    "Positive offer file coupon code identification for use in North America",
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else 
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                8200,
                new EntityDescriptor(
                    "PRODUCT URL",
                    "Extended packaging URL",
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else 
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                90,
                new EntityDescriptor(
                    "INTERNAL",
                    "Information mutually agreed between trading partners",
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else 
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                91,
                new EntityDescriptor(
                    "INTERNAL",
                    "Company internal information",
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else 
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                92,
                new EntityDescriptor(
                    "INTERNAL",
                    "Company internal information",
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else 
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                93,
                new EntityDescriptor(
                    "INTERNAL",
                    "Company internal information",
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else 
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                94,
                new EntityDescriptor(
                    "INTERNAL",
                    "Company internal information",
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else 
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                95,
                new EntityDescriptor(
                    "INTERNAL",
                    "Company internal information",
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else 
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                96,
                new EntityDescriptor(
                    "INTERNAL",
                    "Company internal information",
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else 
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                97,
                new EntityDescriptor(
                    "INTERNAL",
                    "Company internal information",
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else 
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                98,
                new EntityDescriptor(
                    "INTERNAL",
                    "Company internal information",
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else 
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                99,
                new EntityDescriptor(
                    "INTERNAL",
                    "Company internal information",
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else 
                    CharacterSet8290CharsRegex,
#endif
                    false)
            }
        };

    /// <summary>
    ///     Resolve a first two digits of the application identifier into an entity.
    /// </summary>
    /// <param name="data">
    ///     The data buffer.
    /// </param>
    /// <param name="firstTwoDigits">
    ///     The first two digits of the AI.
    /// </param>
    /// <param name="currentPosition">
    ///     The position of the application identifier for the current field.
    /// </param>
    /// <param name="includeDescriptors">
    ///     Indicates whether the descriptors should be included in the resolved identifier.
    /// </param>
    /// <returns>
    ///     An entity.
    /// </returns>
    public static ResolvedApplicationIdentifier Resolve(
        this string data,
        string firstTwoDigits,
        int currentPosition,
        bool includeDescriptors = true) {
        if (data is null) {
            throw new ArgumentNullException(nameof(data));
        }

        ApplicationIdentifier entity;
        string? identifier = null;
        string value;
        int? numberOfDecimalPlaces = null;
        int? sequenceNumber = null;

        switch (firstTwoDigits) {
            case "01":
            case "10":
            case "17":
            case "21":
            case "00":
            case "02":
            case "11":
            case "12":
            case "13":
            case "15":
            case "16":
            case "20":
            case "22":
            case "30":
            case "37":
            case "90":
            case "91":
            case "92":
            case "93":
            case "94":
            case "95":
            case "96":
            case "97":
            case "98":
            case "99":
                entity = data.GetEntity2();
                value = data.GetValue(2);
                break;
            case "23":
                // 5
                entity = data.IsNumberEqual(2, 1, 5) ? data.GetEntity3() : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    value = data.GetValue(2);
                }
                else {
                    value = data.GetValue(3);
                    currentPosition++;
                }

                break;
            case "25":
                // 0..1, 3..5
                entity = data.IsDigitInRange(2, 1) || data.IsNumberInRange(2, 1, 3, 5)
                             ? data.GetEntity3()
                             : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    value = data.GetValue(2);
                }
                else {
                    value = data.GetValue(3);
                    currentPosition++;
                }

                break;
            case "24":
            case "40":
                // 0..3
                entity = data.IsDigitInRange(2, 3) ? data.GetEntity3() : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    value = data.GetValue(2);
                }
                else {
                    value = data.GetValue(3);
                    currentPosition++;
                }

                break;
            case "71":
                // 0..5
                entity = data.IsNumberInRange(2, 1, 0, 5)
                             ? data.GetEntity3()
                             : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    value = data.GetValue(2);
                }
                else {
                    value = data.GetValue(3);
                    currentPosition++;
                }

                break;
            case "41":
            case "42":
                // 0..7
                entity = data.IsDigitInRange(2, 7) ? data.GetEntity3() : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    value = data.GetValue(2);
                }
                else {
                    value = data.GetValue(3);
                    currentPosition++;
                }

                break;
            case "39":
                // 0n..5n
                entity = data.IsDigitInRange(2, 5) ? data.GetEntity3() : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    value = data.GetValue(2);
                }
                else {
                    identifier = ((int)data.GetEntity4()).ToInvariantString("##00");
                    value = data.GetValue(4);
                    numberOfDecimalPlaces = data.GetInverseExponent(3);
                    currentPosition += 2;
                }

                break;
            case "31":
                // 0n..6n
                entity = data.IsDigitInRange(2, 6) ? data.GetEntity3() : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    value = data.GetValue(2);
                }
                else {
                    identifier = ((int)data.GetEntity4()).ToInvariantString("##00");
                    value = data.GetValue(4);
                    numberOfDecimalPlaces = data.GetInverseExponent(3);
                    currentPosition += 2;
                }

                break;
            case "33":
            case "35":
                // 0n..7n
                entity = data.IsDigitInRange(2, 7) ? data.GetEntity3() : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    value = data.GetValue(2);
                }
                else {
                    identifier = ((int)data.GetEntity4()).ToInvariantString("##00");
                    value = data.GetValue(4);
                    numberOfDecimalPlaces = data.GetInverseExponent(3);
                    currentPosition += 2;
                }

                break;
            case "32":
            case "34":
            case "36":
                // 0n..9n
                entity = data.IsDigitInRange(2, 9) ? data.GetEntity3() : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    value = data.GetValue(2);
                }
                else {
                    identifier = ((int)data.GetEntity4()).ToInvariantString("##00");
                    value = data.GetValue(4);
                    numberOfDecimalPlaces = data.GetInverseExponent(3);
                    currentPosition += 2;
                }

                break;
            case "43":
                //00..26
                entity = data.IsNumberInRange(2, 2, 0, 26)
                    ? data.GetEntity4()
                    : ApplicationIdentifier.Unrecognised;

                if (entity == ApplicationIdentifier.Unrecognised) {
                    value = data.GetValue(2);
                }
                else {
                    value = data.GetValue(4);
                    currentPosition += 2;
                }

                break;
            case "70":
                // 01..11, 20..23, 3s, 40
                entity = data.IsNumberInRange(2, 2, 1, 11) || data.IsNumberInRange(2, 2, 20, 23)
                                                           || data.IsNumberEqual(2, 1, 3)
                                                           || data.IsNumberEqual(2, 2, 40)
                             ? data[2] == '3' ? data.GetEntity3() : data.GetEntity4()
                             : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    value = data.GetValue(2);
                }
                else {
                    value = data.GetValue(4);

                    if (entity == ApplicationIdentifier.NumberOfProcessorWithIsoCountryCode) {
                        identifier = ((int)data.GetEntity4()).ToInvariantString("##00");
                        sequenceNumber = data.GetSequenceNumber(3);
                    }

                    currentPosition += 2;
                }

                break;
            case "72":
                // 3s, 40
                entity = data.IsNumberEqual(2, 1, 3) || data.IsNumberEqual(2, 2, 40)
                             ? data[2] == '3' ? data.GetEntity3() : data.GetEntity4()
                             : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    value = data.GetValue(2);
                }
                else {
                    value = data.GetValue(4);

                    if (entity == ApplicationIdentifier.CertificationReference) {
                        identifier = ((int)data.GetEntity4()).ToInvariantString("##00");
                        sequenceNumber = data.GetSequenceNumber(3);
                    }

                    currentPosition += 2;
                }

                break;
            case "80":
                // 01..09, 10..13, 17..20, 26
                entity = data.IsNumberInRange(2, 2, 1, 9) || data.IsNumberInRange(2, 2, 10, 13)
                                                          || data.IsNumberInRange(2, 2, 17, 20)
                                                          || data.IsNumberEqual(2, 2, 26)
                             ? data.GetEntity4()
                             : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    value = data.GetValue(2);
                }
                else {
                    value = data.GetValue(4);
                    currentPosition += 2;
                }

                break;
            case "81":
                // 10..12
                entity = data.IsNumberInRange(2, 2, 10, 12)
                             ? data.GetEntity4()
                             : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    value = data.GetValue(2);
                }
                else {
                    value = data.GetValue(4);
                    currentPosition += 2;
                }

                break;
            case "82":
                // 00
                entity = data.IsNumberEqual(2, 2, 0) ? data.GetEntity4() : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    value = data.GetValue(2);
                }
                else {
                    value = data.GetValue(4);
                    currentPosition += 2;
                }

                break;
            default:
                entity = ApplicationIdentifier.Unrecognised;
                value = data.GetValue(2);
                break;
        }

        if (!includeDescriptors) {
            return Validate(
                new ResolvedApplicationIdentifier(
                    (int)entity,
                    identifier ?? ((int)entity).ToInvariantString("##00"),
                    numberOfDecimalPlaces,
                    sequenceNumber,
                    value,
                    false,
                    string.Empty,
                    string.Empty,
                    currentPosition));
        }

        if (entity == ApplicationIdentifier.Unrecognised) {
            return new ResolvedApplicationIdentifier(
                new ParserException(
                    2002,
                    string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_007, firstTwoDigits),
                    true),
                currentPosition,
                new ResolvedApplicationIdentifier(
                    -1,
                    firstTwoDigits,
                    null,
                    null,
                    value,
                    false,
                    string.Empty,
                    string.Empty,
                    currentPosition));
        }

        var descriptors = entity.GetDescriptors();

        return Validate(
            new ResolvedApplicationIdentifier(
                (int)entity,
                identifier ?? ((int)entity).ToInvariantString("##00"),
                numberOfDecimalPlaces,
                sequenceNumber,
                value,
                descriptors.IsFixedWidth,
                descriptors.DataTitle,
                descriptors.Description,
                currentPosition));
    }

    /// <summary>
    ///     Returns the descriptors tuple for the given application identifier.
    /// </summary>
    /// <param name="entity">The application identifier entity.</param>
    /// <returns>The descriptors tuple for the given application identifier.</returns>
    private static EntityDescriptor GetDescriptors(this ApplicationIdentifier entity) {
        return entity == ApplicationIdentifier.Unrecognised
                   ? new EntityDescriptor(null, null, null, false)
                   : Descriptors[(int)entity];
    }

    /// <summary>
    ///     Returns an entity from the given data based on a given number of characters.
    /// </summary>
    /// <param name="data">
    ///     The data containing the entity identifier.
    /// </param>
    /// <param name="length">
    ///     The length of the entity identifier.
    /// </param>
    /// <returns>
    ///     An entity.
    /// </returns>
    private static ApplicationIdentifier GetEntity(this string data, int length) {
        return !string.IsNullOrEmpty(data) && data.Length >= length && Enum.TryParse(
#if NET6_0_OR_GREATER
                    data[..length],
#else
                    data.Substring(length),
#endif
                    true,
                    out ApplicationIdentifier applicationIdentifier)
                    ? applicationIdentifier
                    : ApplicationIdentifier.Unrecognised;
    }

    /// <summary>
    ///     Returns an entity from the given data based on the first two characters.
    /// </summary>
    /// <param name="data">The data containing the entity identifier.</param>
    /// <returns>An entity.</returns>
    private static ApplicationIdentifier GetEntity2(this string data) {
        return GetEntity(data, 2);
    }

    /// <summary>
    ///     Returns an entity from the given data based on the first three characters.
    /// </summary>
    /// <param name="data">The data containing the entity identifier.</param>
    /// <returns>An entity.</returns>
    private static ApplicationIdentifier GetEntity3(this string data) {
        return GetEntity(data, 3);
    }

    /// <summary>
    ///     Returns an entity from the given data based on the first four characters.
    /// </summary>
    /// <param name="data">The data containing the entity identifier.</param>
    /// <returns>An entity.</returns>
    private static ApplicationIdentifier GetEntity4(this string data) {
        return GetEntity(data, 4);
    }

    /// <summary>
    ///     Returns the number of decimal places.
    /// </summary>
    /// <param name="data">The data buffer.</param>
    /// <param name="inverseExponentIndex">The index of the implied decimal point position digit.</param>
    /// <returns>The number of decimal places.</returns>
    private static int GetInverseExponent(this string data, int inverseExponentIndex) {
        var numberOfDecimalPlacesAsString =
            inverseExponentIndex < data.Length
                ? data[inverseExponentIndex].ToInvariantString()
                : "0";

        return int.TryParse(
                   numberOfDecimalPlacesAsString,
                   out var numberOfDecimalPlaces)
                   ? numberOfDecimalPlaces
                   : -99;
    }

    /// <summary>
    ///     Returns the sequence number.
    /// </summary>
    /// <param name="data">The data buffer.</param>
    /// <param name="sequenceNumberIndex">The index of the sequence number digit.</param>
    /// <returns>The sequence number.</returns>
    private static int GetSequenceNumber(this string data, int sequenceNumberIndex) {
        var sequenceNumberAsString =
            sequenceNumberIndex < data.Length
                ? data[sequenceNumberIndex].ToInvariantString()
                : "0";

        return int.TryParse(
            sequenceNumberAsString,
            out var sequenceNumber)
            ? sequenceNumber
            : -99;
    }

    /// <summary>
    ///     Returns the value associated with the application identifier.
    /// </summary>
    /// <param name="data">The data buffer.</param>
    /// <param name="applicationIdentifierLength">The length of the application identifier.</param>
    /// <returns>The value associated wit the application identifier.</returns>
    private static string GetValue(this string data, int applicationIdentifierLength) {
        return applicationIdentifierLength >= data.Length 
            ? string.Empty
#if NET6_0_OR_GREATER
            : data[applicationIdentifierLength..];
#else
            : data.Substring(applicationIdentifierLength);
#endif
    }

    /// <summary>
    ///     Tests if a character in the data is a digit within a given range.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="index">The index of the character.</param>
    /// <param name="upperBound">The upper bound of the range.  The lower bound is 0.</param>
    /// <returns>True, if the character is a digit and is within range; otherwise false.</returns>
    private static bool IsDigitInRange(this string data, int index, int upperBound) {
        return IsNumberInRange(data, index, 1, 0, upperBound);
    }

    /// <summary>
    ///     Tests if a string in the data is a number equal to a given value.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="startIndex">The index of the start of the number.</param>
    /// <param name="length">The length of the number.</param>
    /// <param name="value">The value to be tested.</param>
    /// <returns>True, if the string is a number and is equal to the value; otherwise false.</returns>
    private static bool IsNumberEqual(this string data, int startIndex, int length, int value) {
        return IsNumberInRange(data, startIndex, length, value, value);
    }

    /// <summary>
    ///     Tests if a string in the data is a number within a given range.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="startIndex">The index of the start of the number.</param>
    /// <param name="length">The length of the number.</param>
    /// <param name="lowerBound">The lower bound of the range.</param>
    /// <param name="upperBound">The upper bound of the range.</param>
    /// <returns>True, if the string is a number and is within range; otherwise false.</returns>
    private static bool IsNumberInRange(
        this string data,
        int startIndex,
        int length,
        int lowerBound,
        int upperBound) {
        return data.Length >= startIndex + length
            && int.TryParse(
#if NET6_0_OR_GREATER
                data[startIndex..(length + startIndex)],
#else
                data.Substring(startIndex, length + startIndex - 1),
#endif
                out var number)
            && number.IsNumberInRange(lowerBound, upperBound);
    }

    /// <summary>
    ///     Tests if a number is within a given range.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <param name="lowerBound">The lower bound of the range.</param>
    /// <param name="upperBound">The upper bound of the range.</param>
    /// <returns>True, if the number is within range; otherwise false.</returns>
    private static bool IsNumberInRange(this int number, int lowerBound, int upperBound) {
        return number >= lowerBound && number <= upperBound;
    }

    /// <summary>
    ///     Validates a resolved entity.
    /// </summary>
    /// <param name="resolvedEntity">The resolved entity to be validated.</param>
    /// <returns>A resolved entity object. If the value is invalid, the object records the error.</returns>
    private static ResolvedApplicationIdentifier Validate(ResolvedApplicationIdentifier resolvedEntity) {
        try {
            var valueString = resolvedEntity.Value.Length > 0
                                  ? " " + resolvedEntity.Value
                                  : string.Empty;

            var identifier =
                Descriptors[resolvedEntity.Entity].IsValid(
                    resolvedEntity.Value,
                    out var validationErrors)
                    ? resolvedEntity
                    : new ResolvedApplicationIdentifier(
                        new ParserException(
                            2005,
                            string.Format(
                                CultureInfo.CurrentCulture,
                                Resources.GS1_Error_006,
                                valueString,
                                resolvedEntity.Identifier.Trim()),
                            true),
                        resolvedEntity.CharacterPosition,
                        resolvedEntity);

            // Add additional resolver exceptions to the collection
            foreach (var gs1ParserException in validationErrors) {
                identifier.AddException(gs1ParserException);
            }

            return identifier;
        }
        catch (ArgumentNullException) {
            return new ResolvedApplicationIdentifier(
                new ParserException(
                    2006,
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.GS1_Error_003,
                        resolvedEntity.Identifier.Trim()),
                    true),
                resolvedEntity.CharacterPosition,
                resolvedEntity);
        }
        catch (RegexMatchTimeoutException) {
            return new ResolvedApplicationIdentifier(
                new ParserException(
                    2007,
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.GS1_Error_002,
                        resolvedEntity.Identifier.Trim()),
                    true),
                resolvedEntity.CharacterPosition,
                resolvedEntity);
        }
    }
}