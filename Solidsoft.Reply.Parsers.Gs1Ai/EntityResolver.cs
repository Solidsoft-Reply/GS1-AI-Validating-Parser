// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityResolver.cs" company="Solidsoft Reply Ltd">
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
// Resolves GS1 entities, validating the entity value and providing descriptors.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CommentTypo
// ReSharper disable BadListLineBreaks
namespace Solidsoft.Reply.Parsers.Gs1Ai;

using Common;

using Descriptors;

using Properties;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

/// <summary>
///     Resolves GS1 entities, validating the entity value and providing descriptors.
/// </summary>
#if NET7_0_OR_GREATER
internal static partial class EntityResolver {
#else
internal static class EntityResolver {
#endif

    /// <summary>
    /// ISO 3166 numeric country codes.
    /// Last checked January 2024.
    /// </summary>
    private const string Iso3166CountryCodes =
    "(004|008|010|012|016|020|024|028|031|032|036|040|" +
    "044|048|050|051|052|056|060|064|068|070|072|074|" +
    "076|084|086|090|092|096|100|104|108|112|116|120|" +
    "124|132|136|140|144|148|152|156|158|162|166|170|" +
    "174|175|178|180|184|188|191|192|196|203|204|208|" +
    "212|214|218|222|226|231|232|233|234|238|239|242|" +
    "246|248|250|254|258|260|262|266|268|270|275|276|" +
    "288|292|296|300|304|308|312|316|320|324|328|332|" +
    "334|336|340|344|348|352|356|360|364|368|372|376|" +
    "380|384|388|392|398|400|404|408|410|414|417|418|" +
    "422|426|428|430|434|438|440|442|446|450|454|458|" +
    "462|466|470|474|478|480|484|492|496|498|499|500|" +
    "504|508|512|516|520|524|528|531|533|534|535|540|" +
    "548|554|558|562|566|570|574|578|580|581|583|584|" +
    "585|586|591|598|600|604|608|612|616|620|624|626|" +
    "630|634|638|642|643|646|652|654|659|660|662|663|" +
    "666|670|674|678|682|686|688|690|694|702|703|704|" +
    "705|706|710|716|724|728|729|732|740|744|748|752|" +
    "756|760|762|764|768|772|776|780|784|788|792|795|" +
    "796|798|800|804|807|818|826|831|832|833|834|840|" +
    "850|854|858|860|862|876|882|887|894)";

    /// <summary>
    /// ISO 3166 alpha-2 country codes.
    /// Last checked January 2024.
    /// </summary>
    private const string Iso3166Alpha2CountryCodes =
        "(AD|AE|AF|AG|AI|AL|AM|AO|AQ|AR|AS|AT|" +
        "AU|AW|AX|AZ|BA|BB|BD|BE|BF|BG|BH|BI|" +
        "BJ|BL|BM|BN|BO|BQ|BR|BS|BT|BV|BW|BY|" +
        "BZ|CA|CC|CD|CF|CG|CH|CI|CK|CL|CM|CN|" +
        "CO|CR|CU|CV|CW|CX|CY|CZ|DE|DJ|DK|DM|" +
        "DO|DZ|EC|EE|EG|EH|ER|ES|ET|FI|FJ|FK|" +
        "FM|FO|FR|GA|GB|GD|GE|GF|GG|GH|GI|GL|" +
        "GM|GN|GP|GQ|GR|GS|GT|GU|GW|GY|HK|HM|" +
        "HN|HR|HT|HU|ID|IE|IL|IM|IN|IO|IQ|IR|" +
        "IS|IT|JE|JM|JO|JP|KE|KG|KH|KI|KM|KN|" +
        "KP|KR|KW|KY|KZ|LA|LB|LC|LI|LK|LR|LS|" +
        "LT|LU|LV|LY|MA|MC|MD|ME|MF|MG|MH|MK|" +
        "ML|MM|MN|MO|MP|MQ|MR|MS|MT|MU|MV|MW|" +
        "MX|MY|MZ|NA|NC|NE|NF|NG|NI|NL|NO|NP|" +
        "NR|NU|NZ|OM|PA|PE|PF|PG|PH|PK|PL|PM|" +
        "PN|PR|PS|PT|PW|PY|QA|RE|RO|RS|RU|RW|" +
        "SA|SB|SC|SD|SE|SG|SH|SI|SJ|SK|SL|SM|" +
        "SN|SO|SR|SS|ST|SV|SX|SY|SZ|TC|TD|TF|" +
        "TG|TH|TJ|TK|TL|TM|TN|TO|TR|TT|TV|TW|" +
        "TZ|UA|UG|UM|US|UY|UZ|VA|VC|VE|VG|VI|" +
        "VN|VU|WF|WS|YE|YT|ZA|ZM|ZW)";

    /// <summary>
    /// ISO 4217 numeric currency codes.
    /// </summary>
    private const string Iso4217CurrencyCodes =
        "(008|012|032|036|044|048|050|051|052|060|064|068|" +
        "072|084|090|096|104|108|116|124|132|136|144|152|" +
        "156|170|174|188|191|192|203|208|214|222|230|232|" +
        "238|242|262|270|292|320|324|328|332|340|344|348|" +
        "352|356|360|364|368|376|388|392|398|400|404|408|" +
        "410|414|417|418|422|426|430|434|446|454|458|462|" +
        "480|484|496|498|504|512|516|524|532|533|548|554|" +
        "558|566|578|586|590|598|600|604|608|634|643|646|" +
        "654|682|690|694|702|704|706|710|728|748|752|756|" +
        "760|764|776|780|784|788|800|807|818|826|834|840|" +
        "858|860|882|886|901|925|927|928|929|930|931|932|" +
        "933|934|936|938|940|941|943|944|946|947|948|949|" +
        "950|951|952|953|955|956|957|958|959|960|961|962|" +
        "963|964|965|967|968|969|970|971|972|973|975|976|" +
        "977|978|979|980|981|984|985|986|990|994|997|999)";

    /// <summary>
    /// GS1 Package Type Codes. These are based on UN/CEFACT alphanumeric codes
    /// extended with additional GS1 codes.
    /// Last checked August 2024.
    /// </summary>
    private const string PackageTypeCodes =
        "(1A|1B|1D|1F|1G|1W|200|201|202|203|204|205|206|" +
        "210|211|212|2C|3A|3H|43|44|4A|4B|4C|4D|4F|4G|4H|" +
        "5H|5L|5M|6H|6P|7A|7B|8|8A|8B|8C|9|AA|AB|AC|AD|AF|" +
        "AG|AH|AI|AJ|AL|AM|AP|APE|AT|AV|B4|BB|BC|BD|BE|BF|" +
        "BG|BGE|BH|BI|BJ|BK|BL|BM|BME|BN|BO|BP|BQ|BR|BRI|" +
        "BS|BT|BU|BV|BW|BX|BY|BZ|CA|CB|CBL|CC|CCE|CD|CE|" +
        "CF|CG|CH|CI|CJ|CK|CL|CM|CN|CO|CP|CQ|CR|CS|CT|CU|" +
        "CV|CW|CX|CY|CZ|DA|DB|DC|DG|DH|DI|DJ|DK|DL|DM|DN|" +
        "DP|DPE|DR|DS|DT|DU|DV|DW|DX|DY|E1|E2|E3|EC|ED|EE|" +
        "EF|EG|EH|EI|EN|FB|FC|FD|FE|FI|FL|FO|FOB|FP|FPE|" +
        "FR|FT|FW|FX|GB|GI|GL|GR|GU|GY|GZ|HA|HB|HC|HG|HN|" +
        "HR|IA|IB|IC|ID|IE|IF|IG|IH|IK|IL|IN|IZ|JB|JC|JG|" +
        "JR|JT|JY|KG|KI|LAB|LE|LG|LT|LU|LV|LZ|MA|MB|MC|ME|" +
        "MPE|MR|MS|MT|MW|MX|NA|NE|NF|NG|NS|NT|NU|NV|OA|OB|" +
        "OC|OD|OE|OF|OK|OPE|OT|OU|P2|PA|PAE|PB|PC|PD|PE|" +
        "PF|PG|PH|PI|PJ|PK|PL|PLP|PN|PO|POP|PP|PPE|PR|PT|" +
        "PU|PUE|PV|PX|PY|PZ|QA|QB|QC|QD|QF|QG|QH|QJ|QK|QL|" +
        "QM|QN|QP|QQ|QR|QS|RB1|RB2|RB3|RCB|RD|RG|RJ|RK|RL|" +
        "RO|RT|RZ|S1|SA|SB|SC|SD|SE|SEC|SH|SI|SK|SL|SM|SO|" +
        "SP|SS|ST|STL|SU|SV|SW|SX|SY|SZ|T1|TB|TC|TD|TE|" +
        "TEV|TG|THE|TI|TK|TL|TN|TO|TR|TRE|TS|TT|TTE|TU|TV|" +
        "TW|TWE|TY|TZ|UC|UN|UUE|VA|VG|VI|VK|VL|VN|VO|VP|" +
        "VQ|VR|VS|VY|WA|WB|WC|WD|WF|WG|WH|WJ|WK|WL|WM|WN|" +
        "WP|WQ|WR|WRP|WS|WT|WU|WV|WW|WX|WY|WZ|X11|X12|X15|" +
        "X16|X17|X18|X19|X20|X3|XA|XB|XC|XD|XF|XG|XH|XJ|" +
        "XK|YA|YB|YC|YD|YF|YG|YH|YJ|YK|YL|YM|YN|YP|YQ|YR|" +
        "YS|YT|YV|YW|YX|YY|YZ|ZA|ZB|ZC|ZD|ZF|ZG|ZH|ZJ|ZK|" +
        "ZL|ZM|ZN|ZP|ZQ|ZR|ZS|ZT|ZU|ZV|ZW|ZX|ZY|ZZ|)";

    /// <summary>
    /// “ISO/IEC 5218 Sex Codes.
    /// Last checked August 2024.
    /// </summary>
    private const string BiologicalSexCode =
        "[0129]";

    /// <summary>
    /// Character Set for check characters for alphanumeric values.
    /// </summary>
    private const string AlphanumericCheckCharacterSet = "[2-9A-Z]";

    /// <summary>
    /// Character Set 39 (file-safe / URI-safe base64).
    /// </summary>
    private const string CharacterSet39 = "[#-/0-9A-Z]";

    /// <summary>
    /// Character Set 64 (file-safe / URI-safe base64).
    /// </summary>
    private const string CharacterSet64 = "[A-Za-z0-9-_=]";

    /// <summary>
    /// Character Set 82 (Invariants).
    /// </summary>
    private const string CharacterSet82 = "[!\"%&'()*+,-./0-9:;<=>?A-Z_a-z]";

    /// <summary>
    /// Character Set for EU 2018/574 IDs.
    /// </summary>
    private const string Eu2018574ImporterIndexCharacterSet = "[A-Za-z0-9-_]";

#if !NET7_0_OR_GREATER
    /// <summary>
    ///     Returns a regular expression for matching a Serial Shipping Container Code.
    /// </summary>
    private static readonly Regex SsccRegex = new (@"^\d{18}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a Global Trade Item Number.
    /// </summary>
    private static readonly Regex GtinRegex = new (@"^\d{14}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a Global Document Type Identifier.
    /// </summary>
    private static readonly Regex GdtiRegex = new (@"^\d{13}(" + CharacterSet82 + @"{1,17})?$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a Global Coupon Number.
    /// </summary>
    private static readonly Regex GcnRegex = new (@"^\d{13}(\d{1,12})?$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a Global Returnable Asset Identifier (GRAI).
    /// </summary>
    private static readonly Regex GraiRegex = new (@"^0\d{13}(" + CharacterSet82 + @"{1,16})?$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for an identification of an individual trade item piece (ITIP).
    /// </summary>
    private static readonly Regex ItipRegex = new (@"^\d{14}\d{2}\d{2}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-2 character value of characters taken from Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8202CharsRegex = new ("^" + CharacterSet82 + @"{1,2}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching an Alpha-2 country code value.
    /// </summary>
    private static readonly Regex Alpha2CountryCodesRegex = new ($"^{Iso3166Alpha2CountryCodes}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a GS1 Package Type Code value.
    /// </summary>
    private static readonly Regex PackageTypeCodesRegex = new ($"^{PackageTypeCodes}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-3 character value of characters taken from Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8203CharsRegex = new ("^" + CharacterSet82 + @"{1,3}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-10 character value of characters taken from Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8210CharsRegex = new ("^" + CharacterSet82 + @"{1,10}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-12 character value of characters taken from Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8212CharsRegex = new ("^" + CharacterSet82 + @"{1,12}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-20 character value of characters taken from Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8220CharsRegex = new ("^" + CharacterSet82 + @"{1,20}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a GCP with 1-19 character value of characters taken from
    ///     Character Set 82 and a check character pair.
    /// </summary>
    private static readonly Regex GcpWithCharacterSet8219CharsAndCheckCharacterPairRegex = new (@"^[0-9]{4}" +
        CharacterSet82 + @"{1,19}" + $"{AlphanumericCheckCharacterSet}" + @"{2}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-25 character value of characters taken from Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8225CharsRegex = new ("^" + CharacterSet82 + @"{1,25}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-28 character value of characters taken from Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8228CharsRegex = new ("^" + CharacterSet82 + @"{1,28}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 3-30 character value of characters taken from Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet820330CharsRegex = new ("^" + CharacterSet82 + @"{3,30}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-30 character value of characters taken from Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8230CharsRegex = new ("^" + CharacterSet82 + @"{1,30}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-30 character value of characters taken from Character Set 39.
    /// </summary>
    private static readonly Regex CharacterSet3930CharsRegex = new ($"^{CharacterSet39}" + @"{1,30}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-34 character value of characters taken from Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8234CharsRegex = new ("^" + CharacterSet82 + @"{1,34}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-35 character value of characters taken from Character Set 82.
    ///     The strings use percent encoding (hexadecimal octets) to encode characters that are not included in Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8235CharsPercEncRegex = new ($"^({CharacterSet82}" + @"|%(?=[0-9a-fA-F]{2})){1,35}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-40 character value of characters taken from Character Set 82.
    ///     The strings use percent encoding (hexadecimal octets) to encode characters that are not included in Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8240CharsPercEncRegex = new ($"^({CharacterSet82}" + @"|%(?=[0-9a-fA-F]{2})){1,40}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-50 character value of characters taken from Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8250CharsRegex = new ("^" + CharacterSet82 + @"{1,50}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-70 character value of characters taken from Character Set 82.
    ///     The strings use percent encoding (hexadecimal octets) to encode characters that are not included in Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8270CharsPercEncRegex = new (@"^(" + CharacterSet82 + @"|%(?=[0-9a-fA-F]{2})){1,70}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-70 character value of characters taken from Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8270CharsRegex = new ("^" + CharacterSet82 + @"{1,70}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-90 character value of characters taken from Character Set 82.
    ///     The strings use percent encoding (hexadecimal octets) to encode characters that are not included in Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8290CharsRegex = new ("^" + CharacterSet82 + @"{1,90}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-90 character value of characters taken from Character Set 82.
    ///     The strings use percent encoding (hexadecimal octets) to encode characters that are not included in Character Set 82.
    /// </summary>
    private static readonly Regex CharacterSet8290CharsPercEncRegex = new (@"^(" + CharacterSet82 + @"|%(?=[0-9a-fA-F]{2})){1,90}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a processor with an ISO country code.
    /// </summary>
    private static readonly Regex ProcessorWithIsoCountryCodeRegex = new ($"^{Iso3166CountryCodes}{CharacterSet82}" + @"{1,27}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a processor with an ISO country code.
    /// </summary>
    private static readonly Regex PostalCodeWithIsoCountryCodeRegex = new (@"^\d{3}" + CharacterSet82 + @"{1,9}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a GS1 UIC with Extension 1 and Imported index.
    /// </summary>
    private static readonly Regex Gs1UicWithExtension1AndImportedIndexRegex = new (@"^\d" + CharacterSet82 + @"{2}" + $"{Eu2018574ImporterIndexCharacterSet}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for six-digit date representation - YYMMDD.
    ///     If it is not necessary to specify the day, the day field can be filled with two zeros.
    /// </summary>
    private static readonly Regex DatePatternZerosRegex = new (@"^(((\d{2})(0[13578]|1[02])(0[0-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[0-9]|[12]\d|30))|((\d{2})02(0[0-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 6-digit trade measures.
    /// </summary>
    private static readonly Regex SixDigitTradeMeasureRegex = new (@"^\d{6}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 6-digit logistics measures.
    /// </summary>
    private static readonly Regex SixDigitLogisticsMeasureRegex = new (@"^\d{6}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 6-digit monetary values.
    /// </summary>
    private static readonly Regex SixDigitMonetaryValueRegex = new (@"^\d{6}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 1-digit ISO/IEC 5218 biological sex codes.
    /// </summary>
    private static readonly Regex IsoIec5218SexCodeRegex = new ($"^{BiologicalSexCode}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 2-digit values.
    /// </summary>
    private static readonly Regex TwoDigitValueRegex = new (@"^\d{2}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 3-digit values.
    /// </summary>
    private static readonly Regex ThreeDigitValueRegex = new ($"^{Iso3166CountryCodes}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 4-digit values.
    /// </summary>
    private static readonly Regex FourDigitValueRegex = new (@"^\d{4}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 6-digit values.
    /// </summary>
    private static readonly Regex SixDigitValueRegex = new (@"^\d{6}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 8-digit values.
    /// </summary>
    private static readonly Regex EightDigitValueRegex = new (@"^\d{8}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 12-digit values.
    /// </summary>
    private static readonly Regex TwelveDigitValueRegex = new (@"^\d{12}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 13-digit values.
    /// </summary>
    private static readonly Regex ThirteenDigitValueRegex = new (@"^\d{13}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 14-digit roll product values.
    /// </summary>
    private static readonly Regex FourteenDigitRollProductValueRegex = new (@"^\d{12}[019]\d$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 17-digit values.
    /// </summary>
    private static readonly Regex SeventeenDigitValueRegex = new (@"^\d{17}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 18-digit values.
    /// </summary>
    private static readonly Regex EighteenDigitValueRegex = new (@"^\d{18}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 4-digit values.
    /// </summary>
    private static readonly Regex MaxFourDigitValueRegex = new (@"^\d{1,4}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 6-digit values.
    /// </summary>
    private static readonly Regex MaxSixDigitValueRegex = new (@"^\d{1,6}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 8-digit values.
    /// </summary>
    private static readonly Regex MaxEightDigitValueRegex = new (@"^\d{1,8}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 10-digit values.
    /// </summary>
    private static readonly Regex MaxTenDigitValueRegex = new (@"^\d{1,10}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 12-digit values.
    ///     The first digit must be non-zero unless the value is a single 0.
    /// </summary>
    private static readonly Regex MaxTwelveDigitValueZeroOrNonZeroFirstRegex = new (@"^(0|[^0]\d{0,11})$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 15-digit values.
    /// </summary>
    private static readonly Regex MaxFifteenDigitValueRegex = new (@"^\d{1,15}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching geographic locations as 20-digit values.
    /// </summary>
    private static readonly Regex TwentyDigitGeoLocationValueRegex = new (@"^((0\d{9}|1[0-7]\d{8}|180{8})([0-2]\d{9}|3[0-5]\d{8}|360{8}))$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 15-digit values with ISO 4217 code.
    /// </summary>
    private static readonly Regex MaxFifteenDigitAmountWithIso4217CodeRegex = new ($"^{Iso4217CurrencyCodes}" + @"\d{1,15}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching up to 15-digit values.
    /// </summary>
    private static readonly Regex MaxFiveIsoCountryCodesRegex = new ($"^{Iso3166CountryCodes}{{1,5}}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a binary flag (1 or 0).
    /// </summary>
    private static readonly Regex BinaryFlagRegex = new (@"^[01]$", RegexOptions.None);

    /// <summary>
    ///     A regular expression for six-digit date representation - YYMMDD.
    /// </summary>
    private static readonly Regex DatePatternRegex = new (@"(((\d{2})(0[13578]|1[02])(0[1-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[1-9]|[12]\d|30))|((\d{2})02(0[1-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for ten-digit date and time representation - YYMMDDHHMM.
    /// </summary>
    private static readonly Regex DateTimePatternRegex = new (@"(((\d{2})(0[13578]|1[02])(0[1-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[1-9]|[12]\d|30))|((\d{2})02(0[1-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))((0\d|1\d|2[0-3])([0-5]\d))", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for ten-digit date and time representation - YYMMDDHHMM.
    ///     The time copmponent is optional.
    /// </summary>
    private static readonly Regex DateOptionalTimePatternRegex = new (@"(((\d{2})(0[13578]|1[02])(0[1-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[1-9]|[12]\d|30))|((\d{2})02(0[1-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))((0\d|1\d|2[0-3])([0-5]\d))?", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for the harvest date.
    ///     The second (end) date is optional.
    /// </summary>
    private static readonly Regex HarvestDateRegex = new (@"((0\d|1[012])([0-5]\d))((0\d|1[012])([0-5]\d))?", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for ten-digit date and time representation - YYMMDDHHMM.
    ///     If it is not necessary to specify the day, the day field can be filled with two zeros.
    ///     If it is not necessary to specify a time, the hour and minutes are filled with 9s.
    /// </summary>
    private static readonly Regex DateTimePatternZerosAnd9SRegex = new (@"(((\d{2})(0[13578]|1[02])(0[0-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[0-9]|[12]\d|30))|((\d{2})02(0[0-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))(((0\d|1\d|2[0-3])([0-5]\d))|9999)", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for the date and time of production:.
    ///     If it is not necessary to specify the minutes or the seconds.
    /// </summary>
    private static readonly Regex DateAndTimeOfProductionRegex = new (@"(((\d{2})(0[13578]|1[02])(0[0-9]|[12]\d|3[01]))|((\d{2})(0[469]|11)(0[0-9]|[12]\d|30))|((\d{2})02(0[0-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))(((0\d|1\d|2[0-3])([0-5]\d)?([0-5]\d)?))", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for the Scan4Transport temperature
    ///     requirement. If there is a final -, this represents a negative
    ///     temerature value.
    /// </summary>
    private static readonly Regex Scan4TransportTemperatureRegex = new (@"^\d{6}-?$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching 2-digit AIDC media types.
    /// </summary>
    private static readonly Regex TwoDigitAidcMediaTypeRegex = new (@"^(0[1-9]|10|[89][0-9])$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for matching a 1-90 character value of
    ///     characters taken from Character Set 64 (RFC 4648 Section 5).
    /// </summary>
    private static readonly Regex CharacterSet6490CharsRegex = new ($"^{CharacterSet64}" + @"{1,90}$", RegexOptions.None);

    /// <summary>
    ///     Returns a regular expression for a sequence of digit-solidus-digit (e.g., 3/6).
    /// </summary>
    /// <returns>A regular expression.</returns>
    private static readonly Regex SequenceRegex = new ($@"^\d/\d$", RegexOptions.None);
#endif

    /// <summary>
    ///     A dictionary of application identifier descriptors.
    /// </summary>
    private static readonly IDictionary<int, EntityDescriptors> Descriptors =
        new Dictionary<int, EntityDescriptors>
        {
            {
                0, new IdentifierWithFinalChecksumDescriptor(
                    "SSCC",
                    Gs1ApplicationIdentifier.ai00,
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
                    Gs1ApplicationIdentifier.ai01,
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
                    Gs1ApplicationIdentifier.ai02,
#if NET7_0_OR_GREATER
                    GtinRegex(),
#else
                    GtinRegex,
#endif
                    true)
            },
            {
                3, new IdentifierWithFinalChecksumDescriptor(
                    "MTO GTIN",
                    Gs1ApplicationIdentifier.ai03,
#if NET7_0_OR_GREATER
                    GtinRegex(),
#else
                    GtinRegex,
#endif
                    true)
            },
            {
                10, new EntityDescriptors(
                    "BATCH/LOT",
                    Gs1ApplicationIdentifier.ai10,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                11, new EntityDescriptors(
                    "PROD DATE",
                    Gs1ApplicationIdentifier.ai11,
#if NET7_0_OR_GREATER
                    DatePatternZerosRegex(),
#else
                    DatePatternZerosRegex,
#endif
                    true)
            },
            {
                12, new EntityDescriptors(
                    "DUE DATE",

                    // ReSharper disable once StringLiteralTypo
#pragma warning disable SA1115 // Parameter should follow comma
                    Gs1ApplicationIdentifier.ai12,
#pragma warning restore SA1115 // Parameter should follow comma
#if NET7_0_OR_GREATER
                    DatePatternZerosRegex(),
#else
                    DatePatternZerosRegex,
#endif
                    true)
            },
            {
                13, new EntityDescriptors(
                    "PACK DATE",

                    // ReSharper disable once StringLiteralTypo
#pragma warning disable SA1115 // Parameter should follow comma
                    Gs1ApplicationIdentifier.ai13,
#pragma warning restore SA1115 // Parameter should follow comma
#if NET7_0_OR_GREATER
                    DatePatternZerosRegex(),
#else
                    DatePatternZerosRegex,
#endif
                    true)
            },
            {
                15, new EntityDescriptors(
                    "BEST BEFORE or BEST BY",

                    // ReSharper disable once StringLiteralTypo
#pragma warning disable SA1115 // Parameter should follow comma
                    Gs1ApplicationIdentifier.ai15,
#pragma warning restore SA1115 // Parameter should follow comma
#if NET7_0_OR_GREATER
                    DatePatternZerosRegex(),
#else
                    DatePatternZerosRegex,
#endif
                    true)
            },
            {
                16, new EntityDescriptors(
                    "SELL BY",

                    // ReSharper disable once StringLiteralTypo
#pragma warning disable SA1115 // Parameter should follow comma
                    Gs1ApplicationIdentifier.ai16,
#pragma warning restore SA1115 // Parameter should follow comma
#if NET7_0_OR_GREATER
                    DatePatternZerosRegex(),
#else
                    DatePatternZerosRegex,
#endif
                    true)
            },
            {
                17, new EntityDescriptors(
                    "USE BY OR EXPIRY",
                    Gs1ApplicationIdentifier.ai17,
#if NET7_0_OR_GREATER
                    DatePatternZerosRegex(),
#else
                    DatePatternZerosRegex,
#endif
                    true)
            },
            {
                20,
                new EntityDescriptors(
                    "VARIANT",
                    Gs1ApplicationIdentifier.ai20,
#if NET7_0_OR_GREATER
                    TwoDigitValueRegex(),
#else
                    TwoDigitValueRegex,
#endif
                    true)
            },
            {
                21,
                new EntityDescriptors(
                    "SERIAL",
                    Gs1ApplicationIdentifier.ai21,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                22, new EntityDescriptors(
                    "CPV",
                    Gs1ApplicationIdentifier.ai22,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                235, new EntityDescriptors(
                    "TPX",
                    Gs1ApplicationIdentifier.ai235,
#if NET7_0_OR_GREATER
                    CharacterSet8228CharsRegex(),
#else
                    CharacterSet8228CharsRegex,
#endif
                    false)
            },
            {
                240, new EntityDescriptors(
                    "ADDITIONAL ID",
                    Gs1ApplicationIdentifier.ai240,
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                241, new EntityDescriptors(
                    "CUST. PART No.",
                    Gs1ApplicationIdentifier.ai241,
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                242,
                new EntityDescriptors(
                    "MTO VARIANT",
                    Gs1ApplicationIdentifier.ai242,
#if NET7_0_OR_GREATER
                    MaxSixDigitValueRegex(),
#else
                    MaxSixDigitValueRegex,
#endif
                    false)
            },
            {
                243, new EntityDescriptors(
                    "PCN",
                    Gs1ApplicationIdentifier.ai243,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                250, new EntityDescriptors(
                    "SECONDARY SERIAL",
                    Gs1ApplicationIdentifier.ai250,
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                251, new EntityDescriptors(
                    "REF. TO SOURCE",
                    Gs1ApplicationIdentifier.ai251,
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                253, new IdentifierWithPos13ChecksumDescriptor(
                    "GDTI",
                    Gs1ApplicationIdentifier.ai253,
#if NET7_0_OR_GREATER
                    GdtiRegex(),
#else
                    GdtiRegex,
#endif
                    false)
            },
            {
                254, new EntityDescriptors(
                    "GLN EXTENSION COMPONENT",
                    Gs1ApplicationIdentifier.ai254,
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
                    Gs1ApplicationIdentifier.ai255,
#if NET7_0_OR_GREATER
                    GcnRegex(),
#else
                    GcnRegex,
#endif
                    false)
            },
            {
                30, new EntityDescriptors(
                    "VAR. COUNT",
                    Gs1ApplicationIdentifier.ai30,
#if NET7_0_OR_GREATER
                    MaxEightDigitValueRegex(),
#else
                    MaxEightDigitValueRegex,
#endif
                    false)
            },
            {
                310, new EntityDescriptors(
                    "NET WEIGHT (kg)",
                    Gs1ApplicationIdentifier.ai310n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                311, new EntityDescriptors(
                    "LENGTH (m)",
                    Gs1ApplicationIdentifier.ai311n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                312, new EntityDescriptors(
                    "WIDTH (m)",
                    Gs1ApplicationIdentifier.ai312n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                313, new EntityDescriptors(
                    "HEIGHT (m)",
                    Gs1ApplicationIdentifier.ai313n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                314, new EntityDescriptors(
                    "AREA (m²)",
                    Gs1ApplicationIdentifier.ai314n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                315, new EntityDescriptors(
                    "NET VOLUME (l)",
                    Gs1ApplicationIdentifier.ai315n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                316, new EntityDescriptors(
                    "NET VOLUME (m³)",
                    Gs1ApplicationIdentifier.ai316n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                320, new EntityDescriptors(
                    "NET WEIGHT (lb)",
                    Gs1ApplicationIdentifier.ai320n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                321, new EntityDescriptors(
                    "LENGTH (i)",
                    Gs1ApplicationIdentifier.ai321n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                322, new EntityDescriptors(
                    "LENGTH (f)",
                    Gs1ApplicationIdentifier.ai322n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                323, new EntityDescriptors(
                    "LENGTH (y)",
                    Gs1ApplicationIdentifier.ai323n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                324, new EntityDescriptors(
                    "WIDTH (i)",
                    Gs1ApplicationIdentifier.ai324n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                325, new EntityDescriptors(
                    "WIDTH (f)",
                    Gs1ApplicationIdentifier.ai325n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                326, new EntityDescriptors(
                    "WIDTH (y)",
                    Gs1ApplicationIdentifier.ai326n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                327, new EntityDescriptors(
                    "HEIGHT (i)",
                    Gs1ApplicationIdentifier.ai327n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                328, new EntityDescriptors(
                    "HEIGHT (f)",
                    Gs1ApplicationIdentifier.ai328n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                329, new EntityDescriptors(
                    "HEIGHT (y)",
                    Gs1ApplicationIdentifier.ai329n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                330,
                new EntityDescriptors(
                    "GROSS WEIGHT (kg)",
                    Gs1ApplicationIdentifier.ai330n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                331,
                new EntityDescriptors(
                    "LENGTH (m), log",
                    Gs1ApplicationIdentifier.ai331n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                332, new EntityDescriptors(
                    "WIDTH (m), log",
                    Gs1ApplicationIdentifier.ai332n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                333, new EntityDescriptors(
                    "HEIGHT (m), log",
                    Gs1ApplicationIdentifier.ai333n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                334,
                new EntityDescriptors(
                    "AREA (m²), log",
                    Gs1ApplicationIdentifier.ai334n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                335,
                new EntityDescriptors(
                    "VOLUME (l), log",
                    Gs1ApplicationIdentifier.ai335n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                336,
                new EntityDescriptors(
                    "VOLUME (m³), log",
                    Gs1ApplicationIdentifier.ai336n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                337,
                new EntityDescriptors(
                    "KG PER m²",
                    Gs1ApplicationIdentifier.ai337n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                340,
                new EntityDescriptors(
                    "GROSS WEIGHT (lb)",
                    Gs1ApplicationIdentifier.ai340n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                341,
                new EntityDescriptors(
                    "LENGTH (i), log",
                    Gs1ApplicationIdentifier.ai341n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                342,
                new EntityDescriptors(
                    "LENGTH (f), log",
                    Gs1ApplicationIdentifier.ai342n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                343,
                new EntityDescriptors(
                    "LENGTH (y), log",
                    Gs1ApplicationIdentifier.ai343n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                344, new EntityDescriptors(
                    "WIDTH (i), log",
                    Gs1ApplicationIdentifier.ai344n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                345, new EntityDescriptors(
                    "WIDTH (f), log",
                    Gs1ApplicationIdentifier.ai345n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                346, new EntityDescriptors(
                    "WIDTH (y), log",
                    Gs1ApplicationIdentifier.ai346n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                347, new EntityDescriptors(
                    "HEIGHT (i), log",
                    Gs1ApplicationIdentifier.ai347n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                348, new EntityDescriptors(
                    "HEIGHT (f), log",
                    Gs1ApplicationIdentifier.ai348n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                349, new EntityDescriptors(
                    "HEIGHT (y), log",
                    Gs1ApplicationIdentifier.ai349n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                350, new EntityDescriptors(
                    "AREA (i²)",
                    Gs1ApplicationIdentifier.ai350n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                351, new EntityDescriptors(
                    "AREA (f²)",
                    Gs1ApplicationIdentifier.ai351n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                352, new EntityDescriptors(
                    "AREA (y²)",
                    Gs1ApplicationIdentifier.ai352n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                353,
                new EntityDescriptors(
                    "AREA (i²), log",
                    Gs1ApplicationIdentifier.ai353n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                354,
                new EntityDescriptors(
                    "AREA (f²), log",
                    Gs1ApplicationIdentifier.ai354n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                355,
                new EntityDescriptors(
                    "AREA (y²), log",
                    Gs1ApplicationIdentifier.ai355n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                356,
                new EntityDescriptors(
                    "NET WEIGHT (t)",
                    Gs1ApplicationIdentifier.ai356n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                357,
                new EntityDescriptors(
                    "NET VOLUME (oz)",
                    Gs1ApplicationIdentifier.ai357n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                360,
                new EntityDescriptors(
                    "NET VOLUME (q)",
                    Gs1ApplicationIdentifier.ai360n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                361,
                new EntityDescriptors(
                    "NET VOLUME (g)",
                    Gs1ApplicationIdentifier.ai361n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                362,
                new EntityDescriptors(
                    "VOLUME (q), log",
                    Gs1ApplicationIdentifier.ai362n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                363,
                new EntityDescriptors(
                    "VOLUME (g), log",
                    Gs1ApplicationIdentifier.ai363n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                364, new EntityDescriptors(
                    "VOLUME (i³)",
                    Gs1ApplicationIdentifier.ai364n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                365, new EntityDescriptors(
                    "VOLUME (f³)",
                    Gs1ApplicationIdentifier.ai365n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                366, new EntityDescriptors(
                    "VOLUME (y³)",
                    Gs1ApplicationIdentifier.ai366n,
#if NET7_0_OR_GREATER
                    SixDigitTradeMeasureRegex(),
#else
                    SixDigitTradeMeasureRegex,
#endif
                    true)
            },
            {
                367,
                new EntityDescriptors(
                    "VOLUME (i³), log",
                    Gs1ApplicationIdentifier.ai367n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                368,
                new EntityDescriptors(
                    "VOLUME (f³), log",
                    Gs1ApplicationIdentifier.ai368n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                369,
                new EntityDescriptors(
                    "VOLUME (y³), log",
                    Gs1ApplicationIdentifier.ai369n,
#if NET7_0_OR_GREATER
                    SixDigitLogisticsMeasureRegex(),
#else
                    SixDigitLogisticsMeasureRegex,
#endif
                    true)
            },
            {
                37,
                new EntityDescriptors(
                    "COUNT",
                    Gs1ApplicationIdentifier.ai37,
#if NET7_0_OR_GREATER
                    MaxEightDigitValueRegex(),
#else
                    MaxEightDigitValueRegex,
#endif
                    false)
            },
            {
                390,
                new EntityDescriptors(
                    "AMOUNT",
                    Gs1ApplicationIdentifier.ai390n,
#if NET7_0_OR_GREATER
                    MaxFifteenDigitValueRegex(),
#else
                    MaxFifteenDigitValueRegex,
#endif
                    false)
            },
            {
                391,
                new EntityDescriptors(
                    "AMOUNT",
                    Gs1ApplicationIdentifier.ai391n,
#if NET7_0_OR_GREATER
                    MaxFifteenDigitAmountWithIso4217CodeRegex(),
#else
                    MaxFifteenDigitAmountWithIso4217CodeRegex,
#endif
                    false)
            },
            {
                392,
                new EntityDescriptors(
                    "PRICE",
                    Gs1ApplicationIdentifier.ai392n,
#if NET7_0_OR_GREATER
                    MaxFifteenDigitValueRegex(),
#else
                    MaxFifteenDigitValueRegex,
#endif
                    false)
            },
            {
                393,
                new EntityDescriptors(
                    "PRICE",
                    Gs1ApplicationIdentifier.ai393n,
#if NET7_0_OR_GREATER
                    MaxFifteenDigitAmountWithIso4217CodeRegex(),
#else
                    MaxFifteenDigitAmountWithIso4217CodeRegex,
#endif
                    false)
            },
            {
                394,
                new EntityDescriptors(
                    "PRCNT OFF",
                    Gs1ApplicationIdentifier.ai394n,
#if NET7_0_OR_GREATER
                    FourDigitValueRegex(),
#else
                    FourDigitValueRegex,
#endif
                    true)
            },
            {
                395,
                new EntityDescriptors(
                    "PRICE/UoM",
                    Gs1ApplicationIdentifier.ai395n,
#if NET7_0_OR_GREATER
                    SixDigitMonetaryValueRegex(),
#else
                    SixDigitMonetaryValueRegex,
#endif
                    true)
            },
            {
                400,
                new EntityDescriptors(
                    "ORDER NUMBER",
                    Gs1ApplicationIdentifier.ai400,
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                401,
                new EntityDescriptors(
                    "GINC",
                    Gs1ApplicationIdentifier.ai401,
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                402,
                new IdentifierWithFinalChecksumDescriptor(
                    "GSIN",
                    Gs1ApplicationIdentifier.ai402,
#if NET7_0_OR_GREATER
                    SeventeenDigitValueRegex(),
#else
                    SeventeenDigitValueRegex,
#endif
                    true)
            },
            {
                403,
                new EntityDescriptors(
                    "ROUTE",
                    Gs1ApplicationIdentifier.ai403,
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
                    Gs1ApplicationIdentifier.ai410,
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
                    Gs1ApplicationIdentifier.ai411,
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
                    Gs1ApplicationIdentifier.ai412,
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
                    Gs1ApplicationIdentifier.ai413,
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
                    Gs1ApplicationIdentifier.ai414,
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
                    Gs1ApplicationIdentifier.ai415,
#if NET7_0_OR_GREATER
                    ThirteenDigitValueRegex(),
#else
                    ThirteenDigitValueRegex,
#endif
                    true)
            },
            {
                416, new IdentifierWithFinalChecksumDescriptor(
                    "PROD/SERV LOC",
                    Gs1ApplicationIdentifier.ai416,
#if NET7_0_OR_GREATER
                    ThirteenDigitValueRegex(),
#else
                    ThirteenDigitValueRegex,
#endif
                    true)
            },
            {
                417, new IdentifierWithFinalChecksumDescriptor(
                    "PARTY",
                    Gs1ApplicationIdentifier.ai417,
#if NET7_0_OR_GREATER
                    ThirteenDigitValueRegex(),
#else
                    ThirteenDigitValueRegex,
#endif
                    true)
            },
            {
                420, new EntityDescriptors(
                    "SHIP TO POST",
                    Gs1ApplicationIdentifier.ai420,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                421, new EntityDescriptors(
                    "SHIP TO POST",
                    Gs1ApplicationIdentifier.ai421,
#if NET7_0_OR_GREATER
                    PostalCodeWithIsoCountryCodeRegex(),
#else
                    PostalCodeWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                422,
                new EntityDescriptors(
                    "ORIGIN",
                    Gs1ApplicationIdentifier.ai422,
#if NET7_0_OR_GREATER
                    ThreeDigitValueRegex(),
#else
                    ThreeDigitValueRegex,
#endif
                    true)
            },
            {
                423, new EntityDescriptors(
                    "COUNTRY - INITIAL PROCESS",
                    Gs1ApplicationIdentifier.ai423,
#if NET7_0_OR_GREATER
                    MaxFiveIsoCountryCodesRegex(),
#else
                    MaxFiveIsoCountryCodesRegex,
#endif
                    false)
            },
            {
                424,
                new EntityDescriptors(
                    "COUNTRY - PROCESS",
                    Gs1ApplicationIdentifier.ai424,
#if NET7_0_OR_GREATER
                    ThreeDigitValueRegex(),
#else
                    ThreeDigitValueRegex,
#endif
                    true)
            },
            {
                425, new EntityDescriptors(
                    "COUNTRY - DISASSEMBLY",
                    Gs1ApplicationIdentifier.ai425,
#if NET7_0_OR_GREATER
                    MaxFiveIsoCountryCodesRegex(),
#else
                    MaxFiveIsoCountryCodesRegex,
#endif
                    false)
            },
            {
                426, new EntityDescriptors(
                    "COUNTRY – FULL PROCESS",
                    Gs1ApplicationIdentifier.ai426,
#if NET7_0_OR_GREATER
                    ThreeDigitValueRegex(),
#else
                    ThreeDigitValueRegex,
#endif
                    true)
            },
            {
                427, new EntityDescriptors(
                    "ORIGIN SUBDIVISION",
                    Gs1ApplicationIdentifier.ai427,
#if NET7_0_OR_GREATER
                    CharacterSet8203CharsRegex(),
#else
                    CharacterSet8203CharsRegex,
#endif
                    false)
            },
            {
                4300,
                new EntityDescriptors(
                    "SHIP TO COMP",
                    Gs1ApplicationIdentifier.ai4300,
#if NET7_0_OR_GREATER
                    CharacterSet8235CharsPercEncRegex(),
#else
                    CharacterSet8235CharsPercEncRegex,
#endif
                    false)
            },
            {
                4301,
                new EntityDescriptors(
                    "SHIP TO NAME",
                    Gs1ApplicationIdentifier.ai4301,
#if NET7_0_OR_GREATER
                    CharacterSet8235CharsPercEncRegex(),
#else
                    CharacterSet8235CharsPercEncRegex,
#endif
                    false)
            },
            {
                4302,
                new EntityDescriptors(
                    "SHIP TO ADD1",
                    Gs1ApplicationIdentifier.ai4302,
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsPercEncRegex(),
#else
                    CharacterSet8270CharsPercEncRegex,
#endif
                    false)
            },
            {
                4303,
                new EntityDescriptors(
                    "SHIP TO ADD2",
                    Gs1ApplicationIdentifier.ai4303,
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsPercEncRegex(),
#else
                    CharacterSet8270CharsPercEncRegex,
#endif
                    false)
            },
            {
                4304,
                new EntityDescriptors(
                    "SHIP TO SUB",
                    Gs1ApplicationIdentifier.ai4304,
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsPercEncRegex(),
#else
                    CharacterSet8270CharsPercEncRegex,
#endif
                    false)
            },
            {
                4305,
                new EntityDescriptors(
                    "SHIP TO LOC",
                    Gs1ApplicationIdentifier.ai4305,
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsPercEncRegex(),
#else
                    CharacterSet8270CharsPercEncRegex,
#endif
                    false)
            },
            {
                4306,
                new EntityDescriptors(
                    "SHIP TO REG",
                    Gs1ApplicationIdentifier.ai4306,
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsPercEncRegex(),
#else
                    CharacterSet8270CharsPercEncRegex,
#endif
                    false)
            },
            {
                4307,
                new EntityDescriptors(
                    "SHIP TO COUNTRY",
                    Gs1ApplicationIdentifier.ai4307,
#if NET7_0_OR_GREATER
                    Alpha2CountryCodesRegex(),
#else
                    Alpha2CountryCodesRegex,
#endif
                    true)
            },
            {
                4308,
                new EntityDescriptors(
                    "SHIP TO PHONE",
                    Gs1ApplicationIdentifier.ai4308,
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                4309,
                new EntityDescriptors(
                    "SHIP TO GEO",
                    Gs1ApplicationIdentifier.ai4309,
#if NET7_0_OR_GREATER
                    TwentyDigitGeoLocationValueRegex(),
#else
                    TwentyDigitGeoLocationValueRegex,
#endif
                    true)
            },
            {
                4310,
                new EntityDescriptors(
                    "RTN TO COMP",
                    Gs1ApplicationIdentifier.ai4310,
#if NET7_0_OR_GREATER
                    CharacterSet8235CharsPercEncRegex(),
#else
                    CharacterSet8235CharsPercEncRegex,
#endif
                    false)
            },
            {
                4311,
                new EntityDescriptors(
                    "RTN TO NAME",
                    Gs1ApplicationIdentifier.ai4311,
#if NET7_0_OR_GREATER
                    CharacterSet8235CharsPercEncRegex(),
#else
                    CharacterSet8235CharsPercEncRegex,
#endif
                    false)
            },
            {
                4312,
                new EntityDescriptors(
                    "RTN TO ADD1",
                    Gs1ApplicationIdentifier.ai4312,
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsPercEncRegex(),
#else
                    CharacterSet8270CharsPercEncRegex,
#endif
                    false)
            },
            {
                4313,
                new EntityDescriptors(
                    "RTN TO ADD2",
                    Gs1ApplicationIdentifier.ai4313,
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsPercEncRegex(),
#else
                    CharacterSet8270CharsPercEncRegex,
#endif
                    false)
            },
            {
                4314,
                new EntityDescriptors(
                    "RTN TO SUB",
                    Gs1ApplicationIdentifier.ai4314,
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsPercEncRegex(),
#else
                    CharacterSet8270CharsPercEncRegex,
#endif
                    false)
            },
            {
                4315,
                new EntityDescriptors(
                    "RTN TO LOC",
                    Gs1ApplicationIdentifier.ai4315,
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsPercEncRegex(),
#else
                    CharacterSet8270CharsPercEncRegex,
#endif
                    false)
            },
            {
                4316,
                new EntityDescriptors(
                    "RTN TO REG",
                    Gs1ApplicationIdentifier.ai4316,
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsPercEncRegex(),
#else
                    CharacterSet8270CharsPercEncRegex,
#endif
                    false)
            },
            {
                4317,
                new EntityDescriptors(
                    "RTN TO COUNTRY",
                    Gs1ApplicationIdentifier.ai4317,
#if NET7_0_OR_GREATER
                    Alpha2CountryCodesRegex(),
#else
                    Alpha2CountryCodesRegex,
#endif
                    true)
            },
            {
                4318,
                new EntityDescriptors(
                    "RTN TO POST",
                    Gs1ApplicationIdentifier.ai4318,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                4319,
                new EntityDescriptors(
                    "RTN TO PHONE",
                    Gs1ApplicationIdentifier.ai4319,
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                4320,
                new EntityDescriptors(
                    "SRV DESCRIPTION",
                    Gs1ApplicationIdentifier.ai4320,
#if NET7_0_OR_GREATER
                    CharacterSet8235CharsPercEncRegex(),
#else
                    CharacterSet8235CharsPercEncRegex,
#endif
                    false)
            },
            {
                4321,
                new EntityDescriptors(
                    "DANGEROUS GOODS",
                    Gs1ApplicationIdentifier.ai4321,
#if NET7_0_OR_GREATER
                    BinaryFlagRegex(),
#else
                    BinaryFlagRegex,
#endif
                    true)
            },
            {
                4322,
                new EntityDescriptors(
                    "AUTH LEAVE",
                    Gs1ApplicationIdentifier.ai4322,
#if NET7_0_OR_GREATER
                    BinaryFlagRegex(),
#else
                    BinaryFlagRegex,
#endif
                    true)
            },
            {
                4323,
                new EntityDescriptors(
                    "SIG REQUIRED",
                    Gs1ApplicationIdentifier.ai4323,
#if NET7_0_OR_GREATER
                    BinaryFlagRegex(),
#else
                    BinaryFlagRegex,
#endif
                    true)
            },
            {
                4324,
                new EntityDescriptors(
                    "NBEF DEL DT",
                    Gs1ApplicationIdentifier.ai4324,
#if NET7_0_OR_GREATER
                    DateTimePatternZerosAnd9SRegex(),
#else
                    DateTimePatternZerosAnd9SRegex,
#endif
                    true)
            },
            {
                4325,
                new EntityDescriptors(
                    "NAFT DEL DT",
                    Gs1ApplicationIdentifier.ai4325,
#if NET7_0_OR_GREATER
                    DateTimePatternZerosAnd9SRegex(),
#else
                    DateTimePatternZerosAnd9SRegex,
#endif
                    true)
            },
            {
                4326,
                new EntityDescriptors(
                    "REL DATE",
                    Gs1ApplicationIdentifier.ai4326,
#if NET7_0_OR_GREATER
                    DatePatternRegex(),
#else
                    DatePatternRegex,
#endif
                    true)
            },
            {
                4330,
                new EntityDescriptors(
                    "MAX TEMP F",
                    Gs1ApplicationIdentifier.ai4330,
#if NET7_0_OR_GREATER
                    Scan4TransportTemperatureRegex(),
#else
                    Scan4TransportTemperatureRegex,
#endif
                    false)
            },
            {
                4331,
                new EntityDescriptors(
                    "MAX TEMP C",
                    Gs1ApplicationIdentifier.ai4331,
#if NET7_0_OR_GREATER
                    Scan4TransportTemperatureRegex(),
#else
                    Scan4TransportTemperatureRegex,
#endif
                    false)
            },
            {
                4332,
                new EntityDescriptors(
                    "MIN TEMP F",
                    Gs1ApplicationIdentifier.ai4332,
#if NET7_0_OR_GREATER
                    Scan4TransportTemperatureRegex(),
#else
                    Scan4TransportTemperatureRegex,
#endif
                    false)
            },
            {
                4333,
                new EntityDescriptors(
                    "MIN TEMP C",
                    Gs1ApplicationIdentifier.ai4333,
#if NET7_0_OR_GREATER
                    Scan4TransportTemperatureRegex(),
#else
                    Scan4TransportTemperatureRegex,
#endif
                    false)
            },
            {
                7001,
                new EntityDescriptors(
                    "NSN",
                    Gs1ApplicationIdentifier.ai7001,
#if NET7_0_OR_GREATER
                    ThirteenDigitValueRegex(),
#else
                    ThirteenDigitValueRegex,
#endif
                    true)
            },
            {
                7002, new EntityDescriptors(
                    "MEAT CUT",
                    Gs1ApplicationIdentifier.ai7002,
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                7003,
                new EntityDescriptors(
                    "EXPIRY TIME",
                    Gs1ApplicationIdentifier.ai7003,
#if NET7_0_OR_GREATER
                    DateTimePatternRegex(),
#else
                    DateTimePatternRegex,
#endif
                    true)
            },
            {
                7004,
                new EntityDescriptors(
                    "ACTIVE POTENCY",
                    Gs1ApplicationIdentifier.ai7004,
#if NET7_0_OR_GREATER
                    MaxFourDigitValueRegex(),
#else
                    MaxFourDigitValueRegex,
#endif
                    false)
            },
            {
                7005,
                new EntityDescriptors(
                    "CATCH AREA",
                    Gs1ApplicationIdentifier.ai7005,
#if NET7_0_OR_GREATER
                    CharacterSet8212CharsRegex(),
#else
                    CharacterSet8212CharsRegex,
#endif
                    false)
            },
            {
                7006,
                new EntityDescriptors(
                    "FIRST FREEZE DATE",
                    Gs1ApplicationIdentifier.ai7006,
#if NET7_0_OR_GREATER
                    DatePatternRegex(),
#else
                    DatePatternRegex,
#endif
                    true)
            },
            {
                7007,
                new EntityDescriptors(
                    "HARVEST DATE",
                    Gs1ApplicationIdentifier.ai7007,
#if NET7_0_OR_GREATER
                    HarvestDateRegex(),
#else
                    HarvestDateRegex,
#endif
                    false)
            },
            {
                7008,
                new EntityDescriptors(
                    "AQUATIC SPECIES",
                    Gs1ApplicationIdentifier.ai7008,
#if NET7_0_OR_GREATER
                    CharacterSet8203CharsRegex(),
#else
                    CharacterSet8203CharsRegex,
#endif
                    false)
            },
            {
                7009,
                new EntityDescriptors(
                    "FISHING GEAR TYPE",
                    Gs1ApplicationIdentifier.ai7009,
#if NET7_0_OR_GREATER
                    CharacterSet8210CharsRegex(),
#else
                    CharacterSet8210CharsRegex,
#endif
                    false)
            },
            {
                7010,
                new EntityDescriptors(
                    "PROD METHOD",
                    Gs1ApplicationIdentifier.ai7010,
#if NET7_0_OR_GREATER
                    CharacterSet8202CharsRegex(),
#else
                    CharacterSet8202CharsRegex,
#endif
                    false)
            },
            {
                7011,
                new EntityDescriptors(
                    "TEST BY DATE",
                    Gs1ApplicationIdentifier.ai7011,
#if NET7_0_OR_GREATER
                    DateOptionalTimePatternRegex(),
#else
                    DateOptionalTimePatternRegex,
#endif
                    false)
            },
            {
                7020,
                new EntityDescriptors(
                    "REFURB LOT",
                    Gs1ApplicationIdentifier.ai7020,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                7021,
                new EntityDescriptors(
                    "FUNC STAT",
                    Gs1ApplicationIdentifier.ai7021,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                7022,
                new EntityDescriptors(
                    "REV STAT",
                    Gs1ApplicationIdentifier.ai7022,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                7023,
                new EntityDescriptors(
                    "GIAI – ASSEMBLY",
                    Gs1ApplicationIdentifier.ai7023,
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                703,
                new EntityDescriptors(
                    "PROCESSOR # s",
                    Gs1ApplicationIdentifier.ai703s,
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7031,
                new EntityDescriptors(
                    "PROCESSOR # 1",
                    Gs1ApplicationIdentifier.ai703s,
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7032,
                new EntityDescriptors(
                    "PROCESSOR # 2",
                    Gs1ApplicationIdentifier.ai703s,
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7033,
                new EntityDescriptors(
                    "PROCESSOR # 3",
                    Gs1ApplicationIdentifier.ai703s,
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7034,
                new EntityDescriptors(
                    "PROCESSOR # 4",
                    Gs1ApplicationIdentifier.ai703s,
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7035,
                new EntityDescriptors(
                    "PROCESSOR # 5",
                    Gs1ApplicationIdentifier.ai703s,
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7036,
                new EntityDescriptors(
                    "PROCESSOR # 6",
                    Gs1ApplicationIdentifier.ai703s,
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7037,
                new EntityDescriptors(
                    "PROCESSOR # 7",
                    Gs1ApplicationIdentifier.ai703s,
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7038,
                new EntityDescriptors(
                    "PROCESSOR # 8",
                    Gs1ApplicationIdentifier.ai703s,
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7039,
                new EntityDescriptors(
                    "PROCESSOR # 9",
                    Gs1ApplicationIdentifier.ai703s,
#if NET7_0_OR_GREATER
                    ProcessorWithIsoCountryCodeRegex(),
#else
                    ProcessorWithIsoCountryCodeRegex,
#endif
                    false)
            },
            {
                7040,
                new EntityDescriptors(
                    "UIC+EXT",
                    Gs1ApplicationIdentifier.ai7040,
#if NET7_0_OR_GREATER
                    Gs1UicWithExtension1AndImportedIndexRegex(),
#else
                    Gs1UicWithExtension1AndImportedIndexRegex,
#endif
                    true)
            },
            {
                7041,
                new UnCefactFreightUnitTypeDescriptor(
                    "UFRGT UNIT TYPE",
                    Gs1ApplicationIdentifier.ai7041,
#if NET7_0_OR_GREATER
                    PackageTypeCodesRegex(),
#else
                    PackageTypeCodesRegex,
#endif
                    false)
            },
            {
                710,
                new EntityDescriptors(
                    "NHRN PZN",
                    Gs1ApplicationIdentifier.ai710,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                711,
                new EntityDescriptors(
                    "NHRN CIP",
                    Gs1ApplicationIdentifier.ai711,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                712,
                new EntityDescriptors(
                    "NHRN CN",
                    Gs1ApplicationIdentifier.ai712,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                713,
                new EntityDescriptors(
                    "NHRN DRN",
                    Gs1ApplicationIdentifier.ai713,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                714,
                new EntityDescriptors(
                    "NHRN AIM",
                    Gs1ApplicationIdentifier.ai714,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                715,
                new EntityDescriptors(
                    "NHRN NDC",
                    Gs1ApplicationIdentifier.ai715,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                716,
                new EntityDescriptors(
                    "NHRN AIC",
                    Gs1ApplicationIdentifier.ai716,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                723,
                new EntityDescriptors(
                    "CERT # s",
                    Gs1ApplicationIdentifier.ai723s,
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7231,
                new EntityDescriptors(
                    "CERT # 1",
                    Gs1ApplicationIdentifier.ai723s,
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7232,
                new EntityDescriptors(
                    "CERT # 2",
                    Gs1ApplicationIdentifier.ai723s,
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7233,
                new EntityDescriptors(
                    "CERT # 3",
                    Gs1ApplicationIdentifier.ai723s,
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7234,
                new EntityDescriptors(
                    "CERT # 4",
                    Gs1ApplicationIdentifier.ai723s,
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7235,
                new EntityDescriptors(
                    "CERT # 5",
                    Gs1ApplicationIdentifier.ai723s,
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7236,
                new EntityDescriptors(
                    "CERT # 6",
                    Gs1ApplicationIdentifier.ai723s,
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7237,
                new EntityDescriptors(
                    "CERT # 7",
                    Gs1ApplicationIdentifier.ai723s,
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7238,
                new EntityDescriptors(
                    "CERT # 8",
                    Gs1ApplicationIdentifier.ai723s,
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7239,
                new EntityDescriptors(
                    "CERT # 9",
                    Gs1ApplicationIdentifier.ai723s,
#if NET7_0_OR_GREATER
                    CharacterSet820330CharsRegex(),
#else
                    CharacterSet820330CharsRegex,
#endif
                    false)
            },
            {
                7240,
                new EntityDescriptors(
                    "PROTOCOL",
                    Gs1ApplicationIdentifier.ai7240,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                7241,
                new EntityDescriptors(
                    "AIDC MEDIA TYPE",
                    Gs1ApplicationIdentifier.ai7241,
#if NET7_0_OR_GREATER
                    TwoDigitAidcMediaTypeRegex(),
#else
                    TwoDigitAidcMediaTypeRegex,
#endif
                    true)
            },
            {
                7242,
                new EntityDescriptors(
                    "VCN",
                    Gs1ApplicationIdentifier.ai7242,
#if NET7_0_OR_GREATER
                    CharacterSet8225CharsRegex(),
#else
                    CharacterSet8225CharsRegex,
#endif
                    false)
            },
            {
                7250,
                new EntityDescriptors(
                    "DOB",
                    Gs1ApplicationIdentifier.ai7250,
#if NET7_0_OR_GREATER
                    EightDigitValueRegex(),
#else
                    EightDigitValueRegex,
#endif
                    true)
            },
            {
                7251,
                new EntityDescriptors(
                    "DOB TIME",
                    Gs1ApplicationIdentifier.ai7251,
#if NET7_0_OR_GREATER
                    TwelveDigitValueRegex(),
#else
                    TwelveDigitValueRegex,
#endif
                    true)
            },
            {
                7252,
                new EntityDescriptors(
                    "BIO SEX",
                    Gs1ApplicationIdentifier.ai7252,
#if NET7_0_OR_GREATER
                    IsoIec5218SexCodeRegex(),
#else
                    IsoIec5218SexCodeRegex,
#endif
                    true)
            },
            {
                7253,
                new EntityDescriptors(
                    "FAMILY NAME",
                    Gs1ApplicationIdentifier.ai7253,
#if NET7_0_OR_GREATER
                    CharacterSet8240CharsPercEncRegex(),
#else
                    CharacterSet8240CharsPercEncRegex,
#endif
                    false)
            },
            {
                7254,
                new EntityDescriptors(
                    "GIVEN NAME",
                    Gs1ApplicationIdentifier.ai7254,
#if NET7_0_OR_GREATER
                    CharacterSet8240CharsPercEncRegex(),
#else
                    CharacterSet8240CharsPercEncRegex,
#endif
                    false)
            },
            {
                7255,
                new EntityDescriptors(
                    "SUFFIX",
                    Gs1ApplicationIdentifier.ai7255,
#if NET7_0_OR_GREATER
                    CharacterSet8210CharsRegex(),
#else
                    CharacterSet8210CharsRegex,
#endif
                    false)
            },
            {
                7256,
                new EntityDescriptors(
                    "FULL NAME",
                    Gs1ApplicationIdentifier.ai7256,
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsPercEncRegex(),
#else
                    CharacterSet8290CharsPercEncRegex,
#endif
                    false)
            },
            {
                7257,
                new EntityDescriptors(
                    "PERSON ADDR",
                    Gs1ApplicationIdentifier.ai7257,
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsPercEncRegex(),
#else
                    CharacterSet8270CharsPercEncRegex,
#endif
                    false)
            },
            {
                7258,
                new EntityDescriptors(
                    "BIRTH SEQUENCE",
                    Gs1ApplicationIdentifier.ai7258,
#if NET7_0_OR_GREATER
                    SequenceRegex(),
#else
                    SequenceRegex,
#endif
                    true)
            },
            {
                7259,
                new EntityDescriptors(
                    "BABY",
                    Gs1ApplicationIdentifier.ai7259,
#if NET7_0_OR_GREATER
                    CharacterSet8240CharsPercEncRegex(),
#else
                    CharacterSet8240CharsPercEncRegex,
#endif
                    false)
            },
            {
                8001,
                new EntityDescriptors(
                    "DIMENSIONS",
                    Gs1ApplicationIdentifier.ai8001,
#if NET7_0_OR_GREATER
                    FourteenDigitRollProductValueRegex(),
#else
                    FourteenDigitRollProductValueRegex,
#endif
                    true)
            },
            {
                8002,
                new EntityDescriptors(
                    "CMT No.",
                    Gs1ApplicationIdentifier.ai8002,
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
                    "GRAI",
                    Gs1ApplicationIdentifier.ai8003,
#if NET7_0_OR_GREATER
                    GraiRegex(),
#else
                    GraiRegex,
#endif
                    false)
            },
            {
                8004,
                new EntityDescriptors(
                    "GIAI",
                    Gs1ApplicationIdentifier.ai8004,
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                8005,
                new EntityDescriptors(
                    "PRICE PER UNIT",
                    Gs1ApplicationIdentifier.ai8005,
#if NET7_0_OR_GREATER
                    SixDigitValueRegex(),
#else
                    SixDigitValueRegex,
#endif
                    true)
            },
            {
                8006,
                new IdentifierWithPos14ChecksumDescriptor(
                    "ITIP",
                    Gs1ApplicationIdentifier.ai8006,
#if NET7_0_OR_GREATER
                    ItipRegex(),
#else
                    ItipRegex,
#endif
                    true)
            },
            {
                8007,
                new IbanDescriptor(
                    "IBAN",
                    Gs1ApplicationIdentifier.ai8007,
#if NET7_0_OR_GREATER
                    CharacterSet8234CharsRegex(),
#else
                    CharacterSet8234CharsRegex,
#endif
                    false)
            },
            {
                8008,
                new EntityDescriptors(
                    "PROD TIME",
                    Gs1ApplicationIdentifier.ai8008,
#if NET7_0_OR_GREATER
                    DateAndTimeOfProductionRegex(),
#else
                    DateAndTimeOfProductionRegex,
#endif
                    false)
            },
            {
                8009,
                new EntityDescriptors(
                    "OPTSEN",
                    Gs1ApplicationIdentifier.ai8009,
#if NET7_0_OR_GREATER
                    CharacterSet8250CharsRegex(),
#else
                    CharacterSet8250CharsRegex,
#endif
                    false)
            },
            {
                8010,
                new EntityDescriptors(
                    "CPID",
                    Gs1ApplicationIdentifier.ai8010,
#if NET7_0_OR_GREATER
                    CharacterSet3930CharsRegex(),
#else
                    CharacterSet3930CharsRegex,
#endif
                    false)
            },
            {
                8011,
                new EntityDescriptors(
                    "CPID SERIAL",
                    Gs1ApplicationIdentifier.ai8011,
#if NET7_0_OR_GREATER
                    MaxTwelveDigitValueZeroOrNonZeroFirstRegex(),
#else
                    MaxTwelveDigitValueZeroOrNonZeroFirstRegex,
#endif
                    false)
            },
            {
                8012,
                new EntityDescriptors(
                    "VERSION",
                    Gs1ApplicationIdentifier.ai8012,
#if NET7_0_OR_GREATER
                    CharacterSet8220CharsRegex(),
#else
                    CharacterSet8220CharsRegex,
#endif
                    false)
            },
            {
                8013,
                new AlphanumericKeyWithCheckCharacterPairDescriptor(
                    "GMN",
                    Gs1ApplicationIdentifier.ai8013,
#if NET7_0_OR_GREATER
                    GcpWithCharacterSet8219CharsAndCheckCharacterPairRegex(),
#else
                    GcpWithCharacterSet8219CharsAndCheckCharacterPairRegex,
#endif
                    false)
            },
            {
                8014, new AlphanumericKeyWithCheckCharacterPairDescriptor(
                    "MUDI",
                    Gs1ApplicationIdentifier.ai8014,
#if NET7_0_OR_GREATER
                    GcpWithCharacterSet8219CharsAndCheckCharacterPairRegex(),
#else
                    GcpWithCharacterSet8219CharsAndCheckCharacterPairRegex,
#endif
                    false)
            },
            {
                8017,
                new IdentifierWithFinalChecksumDescriptor(
                    "GSRN - PROVIDER",
                    Gs1ApplicationIdentifier.ai8017,
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
                    "GSRN - RECIPIENT",
                    Gs1ApplicationIdentifier.ai8018,
#if NET7_0_OR_GREATER
                    EighteenDigitValueRegex(),
#else
                    EighteenDigitValueRegex,
#endif
                    true)
            },
            {
                8019,
                new EntityDescriptors(
                    "SRIN",
                    Gs1ApplicationIdentifier.ai8019,
#if NET7_0_OR_GREATER
                    MaxTenDigitValueRegex(),
#else
                    MaxTenDigitValueRegex,
#endif
                    false)
            },
            {
                8020,
                new EntityDescriptors(
                    "REF No.",
                    Gs1ApplicationIdentifier.ai8020,
#if NET7_0_OR_GREATER
                    CharacterSet8225CharsRegex(),
#else
                    CharacterSet8225CharsRegex,
#endif
                    false)
            },
            {
                8026,
                new IdentifierWithPos14ChecksumDescriptor(
                    "ITIP CONTENT",
                    Gs1ApplicationIdentifier.ai8026,
#if NET7_0_OR_GREATER
                    EighteenDigitValueRegex(),
#else
                    EighteenDigitValueRegex,
#endif
                    true)
            },
            {
                8030,
                new EntityDescriptors(
                    "DIGSIG",
                    Gs1ApplicationIdentifier.ai8030,
#if NET7_0_OR_GREATER
                    CharacterSet6490CharsRegex(),
#else
                    CharacterSet6490CharsRegex,
#endif
                    false)
            },
            {
                8110,
                new CouponCodeDescriptor(
                    "-",
                    Gs1ApplicationIdentifier.ai8110,
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                8111,
                new EntityDescriptors(
                    "POINTS",
                    Gs1ApplicationIdentifier.ai8111,
#if NET7_0_OR_GREATER
                    FourDigitValueRegex(),
#else
                    FourDigitValueRegex,
#endif
                    true)
            },
            {
                8112,
                new EntityDescriptors(
                    "-",
                    Gs1ApplicationIdentifier.ai8112,
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                8200,
                new EntityDescriptors(
                    "PRODUCT URL",
                    Gs1ApplicationIdentifier.ai8200,
#if NET7_0_OR_GREATER
                    CharacterSet8270CharsRegex(),
#else
                    CharacterSet8270CharsRegex,
#endif
                    false)
            },
            {
                90,
                new EntityDescriptors(
                    "INTERNAL",
                    Gs1ApplicationIdentifier.ai90,
#if NET7_0_OR_GREATER
                    CharacterSet8230CharsRegex(),
#else
                    CharacterSet8230CharsRegex,
#endif
                    false)
            },
            {
                91,
                new EntityDescriptors(
                    "INTERNAL",
                    Gs1ApplicationIdentifier.ai91,
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                92,
                new EntityDescriptors(
                    "INTERNAL",
                    Gs1ApplicationIdentifier.ai92,
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                93,
                new EntityDescriptors(
                    "INTERNAL",
                    Gs1ApplicationIdentifier.ai93,
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                94,
                new EntityDescriptors(
                    "INTERNAL",
                    Gs1ApplicationIdentifier.ai94,
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                95,
                new EntityDescriptors(
                    "INTERNAL",
                    Gs1ApplicationIdentifier.ai95,
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                96,
                new EntityDescriptors(
                    "INTERNAL",
                    Gs1ApplicationIdentifier.ai96,
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                97,
                new EntityDescriptors(
                    "INTERNAL",
                    Gs1ApplicationIdentifier.ai97,
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                98,
                new EntityDescriptors(
                    "INTERNAL",
                    Gs1ApplicationIdentifier.ai98,
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
            {
                99,
                new EntityDescriptors(
                    "INTERNAL",
                    Gs1ApplicationIdentifier.ai99,
#if NET7_0_OR_GREATER
                    CharacterSet8290CharsRegex(),
#else
                    CharacterSet8290CharsRegex,
#endif
                    false)
            },
        };

#if NET7_0_OR_GREATER
    /// <summary>
    ///     Resolve a first two digits of the application identifier into an entity.
    /// </summary>
    /// <param name="data">
    ///     The data buffer.
    /// </param>
    /// <param name="aiRef">
    ///     An existing resolved application identifier reference to populate.
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
    public static ResolvedApplicationIdentifierRef ResolveEx(
        this ReadOnlySpan<char> data,
        ResolvedApplicationIdentifierRef aiRef,
        ReadOnlySpan<char> firstTwoDigits,
        int currentPosition,
        bool includeDescriptors = true) {
        if (data.IsEmpty || data.IsWhiteSpace()) {
            throw new ArgumentException(Resources.GS1_Error_001, nameof(data));
        }

        Span<char> dataCopy = stackalloc char[data.Length];
        data.CopyTo(dataCopy);
        Span<char> identifier = stackalloc char[4];
        Span<char> value = stackalloc char[ResolvedApplicationIdentifierRef.ValueMaxLength];
        Span<int> parameters = stackalloc int[3];

        ResolveEntity(dataCopy, firstTwoDigits, currentPosition, identifier, value, parameters);
        value = value.TrimEnd('\0');
        ApplicationIdentifier entity = (ApplicationIdentifier)parameters[0];
        int numberOfDecimalPlaces = parameters[1];
        int sequenceNumber = parameters[2];

        if (!includeDescriptors) {
            aiRef.Entity = (int)entity;
            identifier.GetIdentifierIfNull((int)entity).CopyTo(aiRef.Identifier);
            aiRef.InverseExponent = numberOfDecimalPlaces == -1 ? null : numberOfDecimalPlaces;
            aiRef.Sequence = sequenceNumber == -1 ? null : sequenceNumber;
            value.CopyTo(aiRef.Value);
            aiRef.IsFixedWidth = false;
            Span<char>.Empty.CopyTo(aiRef.DataTitle);
            Span<char>.Empty.CopyTo(aiRef.Description);
            aiRef.CharacterPosition = currentPosition;
            return Validate(aiRef);
        }

        var descriptors = entity.GetDescriptors();

        entity = descriptors.DataTitle == string.Empty
            ? ApplicationIdentifier.Unrecognised
            : entity;

        if (entity == ApplicationIdentifier.Unrecognised) {
            aiRef.Entity = -1;
            firstTwoDigits.CopyTo(aiRef.Identifier);
            aiRef.InverseExponent = null;
            aiRef.Sequence = null;
            value.CopyTo(aiRef.Value);
            aiRef.IsFixedWidth = false;
            Span<char>.Empty.CopyTo(aiRef.DataTitle);
            Span<char>.Empty.CopyTo(aiRef.Description);
            aiRef.CharacterPosition = currentPosition;
            return new ResolvedApplicationIdentifierRef(
                new ParserException(
                    2002,
                    string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_007, firstTwoDigits.ToString()),
                    true),
                currentPosition,
                aiRef);
        }

        aiRef.Entity = (int)entity;
        identifier.GetIdentifierIfNull((int)entity).CopyTo(aiRef.Identifier);
        aiRef.InverseExponent = numberOfDecimalPlaces == -1 ? null : numberOfDecimalPlaces;
        aiRef.Sequence = sequenceNumber == -1 ? null : sequenceNumber;
        value.CopyTo(aiRef.Value);
        aiRef.IsFixedWidth = descriptors.IsFixedWidth;

        if (descriptors.DataTitle?.Length > ResolvedApplicationIdentifierRef.DataTitleMaxLength) {
            descriptors.DataTitle.AsSpan()[..ResolvedApplicationIdentifierRef.DataTitleMaxLength].CopyTo(aiRef.DataTitle);
        }
        else {
            descriptors.Description?.CopyTo(aiRef.Description);
        }

        if (descriptors.Description?.Length > ResolvedApplicationIdentifierRef.DescriptionMaxLength) {
            descriptors.Description.AsSpan()[..ResolvedApplicationIdentifierRef.DescriptionMaxLength].CopyTo(aiRef.Description);
        }
        else {
            descriptors.Description?.CopyTo(aiRef.Description);
        }

        aiRef.CharacterPosition = currentPosition;
        return Validate(aiRef);
    }
#endif

//#if NET6_0_OR_GREATER
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
        bool includeDescriptors = true) =>
            Resolve(data.AsSpan(), firstTwoDigits.AsSpan(), currentPosition, includeDescriptors);

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
        this ReadOnlySpan<char> data,
        ReadOnlySpan<char> firstTwoDigits,
        int currentPosition,
        bool includeDescriptors = true) {
        if (data.IsEmpty || data.IsWhiteSpace()) {
            throw new ArgumentException(Resources.GS1_Error_001, nameof(data));
        }

        Span<char> dataCopy = stackalloc char[data.Length];
        data.CopyTo(dataCopy);
        Span<char> identifier = stackalloc char[4];
        Span<char> value = stackalloc char[90];
        Span<int> parameters = stackalloc int[3];

        ResolveEntity(dataCopy, firstTwoDigits, currentPosition, identifier, value, parameters);
        ApplicationIdentifier entity = (ApplicationIdentifier)parameters[0];
        int numberOfDecimalPlaces = parameters[1];
        int sequenceNumber = parameters[2];

        if (!includeDescriptors) {
            return Validate(
                new ResolvedApplicationIdentifier(
                    (int)entity,
                    identifier.GetIdentifierIfNull((int)entity).ToString(),
                    numberOfDecimalPlaces == -1 ? null : numberOfDecimalPlaces,
                    sequenceNumber == -1 ? null : sequenceNumber,
                    value.TrimEnd('\0').ToString(),
                    false,
                    string.Empty,
                    string.Empty,
                    currentPosition));
        }

        var descriptors = entity.GetDescriptors();

        entity = descriptors.DataTitle == string.Empty
            ? ApplicationIdentifier.Unrecognised
            : entity;

        if (entity == ApplicationIdentifier.Unrecognised) {
            return new ResolvedApplicationIdentifier(
                new ParserException(
                    2002,
                    string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_007, firstTwoDigits.ToString()),
                    true),
                currentPosition,
                new ResolvedApplicationIdentifier(
                    -1,
                    firstTwoDigits.ToString(),
                    null,
                    null,
                    value.TrimEnd('\0').ToString(),
                    false,
                    string.Empty,
                    string.Empty,
                    currentPosition));
        }

        return Validate(
            new ResolvedApplicationIdentifier(
                (int)entity,
                identifier.GetIdentifierIfNull((int)entity).ToString(),
                numberOfDecimalPlaces == -1 ? null : numberOfDecimalPlaces,
                sequenceNumber == -1 ? null : sequenceNumber,
                value.TrimEnd('\0').ToString(),
                descriptors.IsFixedWidth,
                descriptors.DataTitle,
                descriptors.Description,
                currentPosition));
    }

    private static void ResolveEntity(
        Span<char> data,
        ReadOnlySpan<char> firstTwoDigits,
        int currentPosition,
        Span<char> identifier,
        Span<char> value,
        Span<int> parameters) {
        ApplicationIdentifier entity;
        int numberOfDecimalPlaces = -1;
        int sequenceNumber = -1;

        Span<char> extractedValue = stackalloc char[value.Length];

        switch (firstTwoDigits) {
            case "01":
            case "10":
            case "17":
            case "21":
            case "00":
            case "02":
            case "03":
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
                extractedValue = data.GetValue(2);
                break;
            case "23":
                // 5
                entity = data.IsNumberEqual(2, 1, 5) ? data.GetEntity3() : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    extractedValue = data.GetValue(2);
                }
                else {
                    extractedValue = data.GetValue(3);
                    currentPosition++;
                }

                break;
            case "25":
                // 0..1, 3..5
                entity = data.IsDigitInRange(2, 1) || data.IsNumberInRange(2, 1, 3, 5)
                             ? data.GetEntity3()
                             : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    extractedValue = data.GetValue(2);
                }
                else {
                    extractedValue = data.GetValue(3);
                    currentPosition++;
                }

                break;
            case "24":
            case "40":
                // 0..3
                entity = data.IsDigitInRange(2, 3) ? data.GetEntity3() : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    extractedValue = data.GetValue(2);
                }
                else {
                    extractedValue = data.GetValue(3);
                    currentPosition++;
                }

                break;
            case "71":
                // 0..6
                entity = data.IsNumberInRange(2, 1, 0, 6)
                             ? data.GetEntity3()
                             : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    extractedValue = data.GetValue(2);
                }
                else {
                    extractedValue = data.GetValue(3);
                    currentPosition++;
                }

                break;
            case "41":
            case "42":
                // 0..7
                entity = data.IsDigitInRange(2, 7) ? data.GetEntity3() : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    extractedValue = data.GetValue(2);
                }
                else {
                    extractedValue = data.GetValue(3);
                    currentPosition++;
                }

                break;
            case "39":
                // 0n..5n
                entity = data.IsDigitInRange(2, 5) ? data.GetEntity3() : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    extractedValue = data.GetValue(2);
                }
                else {
                    identifier = ((int)data.GetEntity4()).ToInvariantSpan(identifier, "##00");
                    extractedValue = data.GetValue(4);
                    numberOfDecimalPlaces = data.GetInverseExponent(3);
                    currentPosition += 2;
                }

                break;
            case "31":
                // 0n..6n
                entity = data.IsDigitInRange(2, 6) ? data.GetEntity3() : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    extractedValue = data.GetValue(2);
                }
                else {
                    identifier = ((int)data.GetEntity4()).ToInvariantSpan(identifier, "##00");
                    extractedValue = data.GetValue(4);
                    numberOfDecimalPlaces = data.GetInverseExponent(3);
                    currentPosition += 2;
                }

                break;
            case "33":
            case "35":
                // 0n..7n
                entity = data.IsDigitInRange(2, 7) ? data.GetEntity3() : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    extractedValue = data.GetValue(2);
                }
                else {
                    identifier = ((int)data.GetEntity4()).ToInvariantSpan(identifier, "##00");
                    extractedValue = data.GetValue(4);
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
                    extractedValue = data.GetValue(2);
                }
                else {
                    identifier = ((int)data.GetEntity4()).ToInvariantSpan(identifier, "##00");
                    extractedValue = data.GetValue(4);
                    numberOfDecimalPlaces = data.GetInverseExponent(3);
                    currentPosition += 2;
                }

                break;

            case "43":
                // 00..26, 30..33
                entity = data.IsNumberInRange(2, 2, 0, 26) || data.IsNumberInRange(2, 2, 30, 33)
                    ? data.GetEntity4()
                    : ApplicationIdentifier.Unrecognised;

                if (entity == ApplicationIdentifier.Unrecognised) {
                    extractedValue = data.GetValue(2);
                }
                else {
                    extractedValue = data.GetValue(4);
                    numberOfDecimalPlaces = data.IsNumberInRange(2, 2, 30, 33) ? 2 : -1;
                    currentPosition += 2;
                }

                break;
            case "70":
                // 01..11, 20..23, 3s, 40..41
                entity = data.IsNumberInRange(2, 2, 1, 11)
                         || data.IsNumberInRange(2, 2, 20, 23)
                         || data.IsNumberEqual(2, 1, 3)
                         || data.IsNumberInRange(2, 2, 40, 41)
                             ? data[2] == '3' ? data.GetEntity3() : data.GetEntity4()
                             : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    extractedValue = data.GetValue(2);
                }
                else {
                    extractedValue = data.GetValue(4);

                    if (entity == ApplicationIdentifier.NumberOfProcessorWithIsoCountryCode) {
                        identifier = ((int)data.GetEntity4()).ToInvariantSpan(identifier, "##00");
                        sequenceNumber = data.GetSequenceNumber(3);
                    }

                    currentPosition += 2;
                }

                break;
            case "72":
                // 3s, 40, 50-59
                entity = data.IsNumberEqual(2, 1, 3)
                         || data.IsNumberInRange(2, 2, 40, 42)
                         || data.IsNumberInRange(2, 2, 50, 59)
                             ? data[2] == '3' ? data.GetEntity3() : data.GetEntity4()
                             : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    extractedValue = data.GetValue(2);
                }
                else {
                    extractedValue = data.GetValue(4);

                    if (entity == ApplicationIdentifier.CertificationReference) {
                        identifier = ((int)data.GetEntity4()).ToInvariantSpan(identifier, "##00");
                        sequenceNumber = data.GetSequenceNumber(3);
                    }

                    currentPosition += 2;
                }

                break;
            case "80":
                // 01..09, 10..14, 17..20, 26, 30
                entity = data.IsNumberInRange(2, 2, 1, 9) || data.IsNumberInRange(2, 2, 10, 14)
                                                          || data.IsNumberInRange(2, 2, 17, 20)
                                                          || data.IsNumberEqual(2, 2, 26)
                                                          || data.IsNumberEqual(2, 2, 30)
                             ? data.GetEntity4()
                             : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    extractedValue = data.GetValue(2);
                }
                else {
                    extractedValue = data.GetValue(4);
                    currentPosition += 2;
                }

                break;
            case "81":
                // 10..12
                entity = data.IsNumberInRange(2, 2, 10, 12)
                             ? data.GetEntity4()
                             : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    extractedValue = data.GetValue(2);
                }
                else {
                    extractedValue = data.GetValue(4);
                    currentPosition += 2;
                }

                break;
            case "82":
                // 00
                entity = data.IsNumberEqual(2, 2, 0) ? data.GetEntity4() : ApplicationIdentifier.Unrecognised;
                if (entity == ApplicationIdentifier.Unrecognised) {
                    extractedValue = data.GetValue(2);
                }
                else {
                    extractedValue = data.GetValue(4);
                    currentPosition += 2;
                }

                break;
            default:
                entity = ApplicationIdentifier.Unrecognised;
                extractedValue = data.GetValue(2);
                break;
        }

        extractedValue.TrimEnd('\0').CopyTo(value);

        parameters[0] = (int)entity;
        parameters[1] = numberOfDecimalPlaces;
        parameters[2] = sequenceNumber;
    }

#if NET7_0_OR_GREATER
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
    [GeneratedRegex(@"^\d{13}(" + CharacterSet82 + @"{1,17})?$", RegexOptions.None, "en-US")]
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
    [GeneratedRegex(@"^0\d{13}(" + CharacterSet82 + @"{1,16})?$", RegexOptions.None, "en-US")]
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
    [GeneratedRegex("^" + CharacterSet82 + @"{1,2}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8202CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching an Alpha-2 country code value.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex($"^{Iso3166Alpha2CountryCodes}$", RegexOptions.None, "en-US")]
    private static partial Regex Alpha2CountryCodesRegex();

    /// <summary>
    ///     Returns a regular expression for matching a Package Type Code value.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex($"^{PackageTypeCodes}$", RegexOptions.None, "en-US")]
    private static partial Regex PackageTypeCodesRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-3 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex($"^{CharacterSet82}" + @"{1,3}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8203CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-10 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^" + CharacterSet82 + @"{1,10}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8210CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-12 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^" + CharacterSet82 + @"{1,12}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8212CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-20 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^" + CharacterSet82 + @"{1,20}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8220CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a GCP with 1-19 character value of characters taken from
    ///     Character Set 82 and a check character pair.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^[0-9]{4}" + CharacterSet82 + @"{1,19}" + $"{AlphanumericCheckCharacterSet}" + @"{2}$", RegexOptions.None, "en-US")]
    private static partial Regex GcpWithCharacterSet8219CharsAndCheckCharacterPairRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-25 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^" + CharacterSet82 + @"{1,25}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8225CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-28 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^" + CharacterSet82 + @"{1,28}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8228CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 3-30 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^" + CharacterSet82 + @"{3,30}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet820330CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-30 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^" + CharacterSet82 + @"{1,30}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8230CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-30 character value of characters taken from Character Set 39.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex($"^{CharacterSet39}" + @"{1,30}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet3930CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-34 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^" + CharacterSet82 + @"{1,34}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8234CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-35 character value of characters taken from Character Set 82.
    ///     The strings use percent encoding (hexadecimal octets) to encode characters that are not included in Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex($"^({CharacterSet82}" + @"|%(?=[0-9a-fA-F]{2})){1,35}$", RegexOptions.None, "en-US")]

    // ReSharper disable once IdentifierTypo
    private static partial Regex CharacterSet8235CharsPercEncRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-40 character value of characters taken from Character Set 82.
    ///     The strings use percent encoding (hexadecimal octets) to encode characters that are not included in Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex($"^({CharacterSet82}" + @"|%(?=[0-9a-fA-F]{2})){1,40}$", RegexOptions.None, "en-US")]

    // ReSharper disable once IdentifierTypo
    private static partial Regex CharacterSet8240CharsPercEncRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-50 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^" + CharacterSet82 + @"{1,50}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8250CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-70 character value of characters taken from Character Set 82.
    ///     The strings use percent encoding (hexadecimal octets) to encode characters that are not included in Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^(" + CharacterSet82 + @"|%(?=[0-9a-fA-F]{2})){1,70}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8270CharsPercEncRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-70 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^" + CharacterSet82 + @"{1,70}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8270CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-90 character value of characters taken from Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^" + CharacterSet82 + @"{1,90}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8290CharsRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-90 character value of characters taken from Character Set 82.
    ///     The strings use percent encoding (hexadecimal octets) to encode characters that are not included in Character Set 82.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^(" + CharacterSet82 + @"|%(?=[0-9a-fA-F]{2})){1,90}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet8290CharsPercEncRegex();

    /// <summary>
    ///     Returns a regular expression for matching a processor with an ISO country code.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex($"^{Iso3166CountryCodes}{CharacterSet82}" + @"{1,27}$", RegexOptions.None, "en-US")]
    private static partial Regex ProcessorWithIsoCountryCodeRegex();

    /// <summary>
    ///     Returns a regular expression for matching a processor with an ISO country code.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex($"^{Iso3166CountryCodes}{CharacterSet82}" + @"{1,9}$", RegexOptions.None, "en-US")]
    private static partial Regex PostalCodeWithIsoCountryCodeRegex();

    /// <summary>
    ///     Returns a regular expression for matching a GS1 UIC with Extension 1 and Importer index.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d" + CharacterSet82 + @"{2}" + $"{Eu2018574ImporterIndexCharacterSet}$", RegexOptions.None, "en-US")]
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
    ///     Returns a regular expression for matching 1-digit ISO/IEC 5218 biological sex codes.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex($"^{BiologicalSexCode}$", RegexOptions.None, "en-US")]
    private static partial Regex IsoIec5218SexCodeRegex();

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
    [GeneratedRegex($"^{Iso3166CountryCodes}$", RegexOptions.None, "en-US")]
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
    ///     Returns a regular expression for matching 8-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{8}$", RegexOptions.None, "en-US")]
    private static partial Regex EightDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching 12-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{12}$", RegexOptions.None, "en-US")]
    private static partial Regex TwelveDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching 13-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{13}$", RegexOptions.None, "en-US")]
    private static partial Regex ThirteenDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching 14-digit roll products.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{12}[019]\d$", RegexOptions.None, "en-US")]
    private static partial Regex FourteenDigitRollProductValueRegex();

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
    ///     The first digit must be non-zero unless the value is a single 0.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^(0|[^0]\d{0,11})$", RegexOptions.None, "en-US")]
    private static partial Regex MaxTwelveDigitValueZeroOrNonZeroFirstRegex();

    /// <summary>
    ///     Returns a regular expression for matching up to 15-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d{1,15}$", RegexOptions.None, "en-US")]
    private static partial Regex MaxFifteenDigitValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching geographic locations as 20-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^((0\d{9}|1[0-7]\d{8}|180{8})([0-2]\d{9}|3[0-5]\d{8}|360{8}))$", RegexOptions.None, "en-US")]
    private static partial Regex TwentyDigitGeoLocationValueRegex();

    /// <summary>
    ///     Returns a regular expression for matching up to 15-digit values with ISO 4217 code.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex($"^{Iso4217CurrencyCodes}" + @"\d{1,15}$", RegexOptions.None, "en -US")]
    private static partial Regex MaxFifteenDigitAmountWithIso4217CodeRegex();

    /// <summary>
    ///     Returns a regular expression for matching up to 15-digit values.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex($"^{Iso3166CountryCodes}{{1,5}}$", RegexOptions.None, "en -US")]
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

    /// <summary>
    ///     Returns a regular expression for the Scan4Transport temperature
    ///     requirement. If there is a final -, this represents a negative
    ///     temerature value.
    /// </summary>
    [GeneratedRegex(@"^\d{6}-?$", RegexOptions.None, "en-US")]
    private static partial Regex Scan4TransportTemperatureRegex();

    /// <summary>
    ///     Returns a regular expression for matching 2-digit AIDC media types.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^(0[1-9]|10|[89][0-9])$", RegexOptions.None, "en-US")]
    private static partial Regex TwoDigitAidcMediaTypeRegex();

    /// <summary>
    ///     Returns a regular expression for matching a 1-90 character value of
    ///     characters taken from Character Set 64 (RFC 4648 Section 5).
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex($"^{CharacterSet64}" + @"{1,90}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet6490CharsRegex();

    /// <summary>
    ///     Returns a regular expression for a sequence of digit-solidus-digit (e.g., 3/6).
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^\d/\d$", RegexOptions.None, "en-US")]
    private static partial Regex SequenceRegex();

#endif

    /// <summary>
    ///     Returns the descriptors tuple for the given application identifier.
    /// </summary>
    /// <param name="entity">The application identifier entity.</param>
    /// <returns>The descriptors tuple for the given application identifier.</returns>
    private static EntityDescriptors GetDescriptors(this ApplicationIdentifier entity) {
        return entity == ApplicationIdentifier.Unrecognised
                   ? new EntityDescriptors(null, null, null, false)
                   : GetDescriptor();

        // In highly unusual circumstances, it is possible to obtain an invalid entity.
        EntityDescriptors GetDescriptor() =>
            Descriptors.TryGetValue((int)entity, out var descriptor)
                ? descriptor
                : new EntityDescriptors(string.Empty, null, null, false);
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
    private static ApplicationIdentifier GetEntity(this ReadOnlySpan<char> data, int length) {
#pragma warning disable SA1114 // Parameter list should follow declaration
        return !data.IsNull() && data.Length >= length && Enum.TryParse(
#if NET6_0_OR_GREATER
            data[..length],
#else
            data.Slice(0, length).ToString(),
#endif
            true,
            out ApplicationIdentifier applicationIdentifier)
                ? applicationIdentifier
                : ApplicationIdentifier.Unrecognised;
#pragma warning restore SA1114 // Parameter list should follow declaration
    }

    /// <summary>
    ///     Returns an entity from the given data based on the first two characters.
    /// </summary>
    /// <param name="data">The data containing the entity identifier.</param>
    /// <returns>An entity.</returns>
    private static ApplicationIdentifier GetEntity2(this ReadOnlySpan<char> data) {
        return GetEntity(data, 2);
    }

    /// <summary>
    ///     Returns an entity from the given data based on the first three characters.
    /// </summary>
    /// <param name="data">The data containing the entity identifier.</param>
    /// <returns>An entity.</returns>
    private static ApplicationIdentifier GetEntity3(this ReadOnlySpan<char> data) {
        return GetEntity(data, 3);
    }

    /// <summary>
    ///     Returns an entity from the given data based on the first four characters.
    /// </summary>
    /// <param name="data">The data containing the entity identifier.</param>
    /// <returns>An entity.</returns>
    private static ApplicationIdentifier GetEntity4(this ReadOnlySpan<char> data) {
        return GetEntity(data, 4);
    }

    /// <summary>
    ///     Returns the value associated with the application identifier.
    /// </summary>
    /// <param name="data">The data buffer.</param>
    /// <param name="applicationIdentifierLength">The length of the application identifier.</param>
    /// <returns>The value associated wit the application identifier.</returns>
    private static Span<char> GetValue(this Span<char> data, int applicationIdentifierLength) {
        return applicationIdentifierLength >= data.Length
            ? []
#if NET6_0_OR_GREATER
            : data[applicationIdentifierLength..];
#else
            : data.Slice(applicationIdentifierLength);
#endif
    }

    /// <summary>
    ///     Tests if a character in the data is a digit within a given range.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="index">The index of the character.</param>
    /// <param name="upperBound">The upper bound of the range.  The lower bound is 0.</param>
    /// <returns>True, if the character is a digit and is within range; otherwise false.</returns>
    private static bool IsDigitInRange(this ReadOnlySpan<char> data, int index, int upperBound) {
        return IsNumberInRange(data, index, 1, 0, upperBound);
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
        this ReadOnlySpan<char> data,
        int startIndex,
        int length,
        int lowerBound,
        int upperBound) {
#pragma warning disable SA1114 // Parameter list should follow declaration
        return data.Length >= startIndex + length
            && int.TryParse(
#if NET6_0_OR_GREATER
                data[startIndex..(length + startIndex)],
#else
                data.Slice(startIndex, length).ToString(),
#endif
                out var number)
            && number.IsNumberInRange(lowerBound, upperBound);
#pragma warning restore SA1114 // Parameter list should follow declaration
    }

    /// <summary>
    ///     Tests if a string in the data is a number equal to a given value.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="startIndex">The index of the start of the number.</param>
    /// <param name="length">The length of the number.</param>
    /// <param name="value">The value to be tested.</param>
    /// <returns>True, if the string is a number and is equal to the value; otherwise false.</returns>
    private static bool IsNumberEqual(this ReadOnlySpan<char> data, int startIndex, int length, int value) {
        return IsNumberInRange(data, startIndex, length, value, value);
    }

    /// <summary>
    ///     Returns the number of decimal places.
    /// </summary>
    /// <param name="data">The data buffer.</param>
    /// <param name="inverseExponentIndex">The index of the implied decimal point position digit.</param>
    /// <returns>The number of decimal places.</returns>
    private static int GetInverseExponent(this ReadOnlySpan<char> data, int inverseExponentIndex) {
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
    private static int GetSequenceNumber(this ReadOnlySpan<char> data, int sequenceNumberIndex) {
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
    /// Formats the specified integer value into the provided span using the given format string and invariant culture.
    /// </summary>
    /// <remarks>The formatted value uses the invariant culture regardless of the current culture settings. If
    /// formatting fails, the returned span will be cleared and contain only zeros.</remarks>
    /// <param name="value">The integer value to format.</param>
    /// <param name="span4">A span of at least 4 characters that receives the formatted value, padded with leading zeros if required.</param>
    /// <param name="format">A standard or custom numeric format string that defines how the value is formatted.</param>
    /// <returns>A slice of the input span containing the formatted value, padded with leading zeros if the formatted result is
    /// shorter than 4 characters.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="span4"/> is less than 4 characters in length.</exception>
    private static Span<char> ToInvariantSpan(this int value, Span<char> span4, string format) {
        if (span4.Length < 4)
            throw new ArgumentException("Span must be at least 4 characters long.", nameof(span4));

        span4.Clear();
        Span<char> buffer = stackalloc char[4];
#if NET6_0_OR_GREATER
        if (value.TryFormat(buffer, out int charsWritten, format, CultureInfo.InvariantCulture)) {
            var startIndex = 4 - charsWritten;

            for (var idx = 0; idx < startIndex; idx++)
                span4[idx] = '0';

            for (var idx = startIndex; idx < 4; idx++)
                span4[idx] = buffer[idx - startIndex];

            return span4[startIndex..];
        }
#else
        try {
            var formattedString = value.ToString(format, CultureInfo.InvariantCulture);
            var charsWritten = formattedString.Length;

            var startIndex = 4 - charsWritten;

            for (var idx = 0; idx < startIndex; idx++)
                span4[idx] = '0';

            for (var idx = startIndex; idx < 4; idx++)
                span4[idx] = formattedString[idx - startIndex];

            return span4.Slice(startIndex);
        }
        catch {
        }
#endif

        return span4;
    }

    private static Span<char> GetIdentifierIfNull(this Span<char> identifier, int entity) =>
        identifier.IsNull()
            ? entity.ToInvariantSpan(identifier, "##00")
            : identifier;

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
                                resolvedEntity.Value.Length > 0 ? " " + resolvedEntity.Value : string.Empty,
                                resolvedEntity.Identifier.Trim()),
                            true),
                        resolvedEntity.CharacterPosition,
                        resolvedEntity);

            // Add additional resolver exceptions to the collection
            if (validationErrors == null || validationErrors.Count == 0) return identifier;

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

#if NET7_0_OR_GREATER
    /// <summary>
    ///     Validates a resolved entity.
    /// </summary>
    /// <param name="resolvedEntity">The resolved entity to be validated.</param>
    /// <returns>A resolved entity object. If the value is invalid, the object records the error.</returns>
    private static ResolvedApplicationIdentifierRef Validate(ResolvedApplicationIdentifierRef resolvedEntity) {
        try {
            var identifier =
                Descriptors[resolvedEntity.Entity].IsValid(
                    resolvedEntity.Value,
                    out var validationErrors)
                    ? resolvedEntity
                    : new ResolvedApplicationIdentifierRef(
                        new ParserException(
                            2005,
                            string.Format(
                                CultureInfo.CurrentCulture,
                                Resources.GS1_Error_006,
                                resolvedEntity.Value.Length > 0 ? " " + resolvedEntity.Value.ToString() : string.Empty,
                                resolvedEntity.Identifier.ToString().Trim()),
                            true),
                        resolvedEntity.CharacterPosition,
                        resolvedEntity);

            // Add additional resolver exceptions to the collection
            if (validationErrors == null || validationErrors.Count == 0) return identifier;

            foreach (var gs1ParserException in validationErrors) {
                System.Diagnostics.Trace.WriteLine(gs1ParserException.Message);
                System.Console.WriteLine(gs1ParserException.Message);
                identifier.AddException(gs1ParserException);
            }

            return identifier;
        }
        catch (ArgumentNullException) {
            return new ResolvedApplicationIdentifierRef(
                new ParserException(
                    2006,
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.GS1_Error_003,
                        resolvedEntity.Identifier.ToString().Trim()),
                    true),
                resolvedEntity.CharacterPosition,
                resolvedEntity);
        }
        catch (RegexMatchTimeoutException) {
            return new ResolvedApplicationIdentifierRef(
                new ParserException(
                    2007,
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.GS1_Error_002,
                        resolvedEntity.Identifier.ToString().Trim()),
                    true),
                resolvedEntity.CharacterPosition,
                resolvedEntity);
        }
    }
#endif
}