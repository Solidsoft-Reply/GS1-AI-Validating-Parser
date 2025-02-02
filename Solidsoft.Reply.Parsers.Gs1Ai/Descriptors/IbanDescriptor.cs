// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IbanDescriptor.cs" company="Solidsoft Reply Ltd">
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
// A descriptor for IBANs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai.Descriptors;

using Properties;

using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

using Common;

/// <summary>
///     A descriptor for IBANs.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="IbanDescriptor" /> class.
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
    class IbanDescriptor(
        string dataTitle,
        string description,
        Regex pattern,
        bool isFixedWidth)
    : EntityDescriptor(dataTitle, description, pattern, isFixedWidth) {
#if !NET7_0_OR_GREATER

    /// <summary>
    /// Regular expression for IBAN.
    /// </summary>
    private static readonly Regex IbanRegex = new (@"^[A-Z]{2}\d{2}[A-Za-z0-9]{1,30}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Albanian IBAN.
    /// </summary>
    private static readonly Regex AlbaniaIbanRegex = new (@"^AL\d{2}\d{8}[A-Za-z0-9]{16}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Andorra IBAN.
    /// </summary>
    private static readonly Regex AndorraIbanRegex = new (@"^AD\d{2}\d{8}[A-Za-z0-9]{12}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Austria IBAN.
    /// </summary>
    private static readonly Regex AustriaIbanRegex = new (@"^AT\d{2}[A-Za-z0-9]{16}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Azerbaijan IBAN.
    /// </summary>
    private static readonly Regex AzerbaijanIbanRegex = new (@"^AZ\d{2}[A-Z]{4}[A-Za-z0-9]{20}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Bahrain IBAN.
    /// </summary>
    private static readonly Regex BahrainIbanRegex = new (@"^BH\d{2}[A-Z]{4}[A-Za-z0-9]{14}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Belarus IBAN.
    /// </summary>
    private static readonly Regex BelarusIbanRegex = new (@"^BY\d{2}[A-Za-z0-9]{4}\d{4}[A-Za-z0-9]{16}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Belgium IBAN.
    /// </summary>
    private static readonly Regex BelgiumIbanRegex = new (@"^BE\d{2}\d{12}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Bosnia and Herzegovina IBAN.
    /// </summary>
    private static readonly Regex BosniaAndHerzegovinaIbanRegex = new (@"^BE\d{2}\d{16}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Brazil IBAN.
    /// </summary>
    private static readonly Regex BrazilIbanRegex = new (@"^BR\d{2}\d{23}[A-Z][A-Za-z0-9]$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Bulgaria IBAN.
    /// </summary>
    private static readonly Regex BulgariaIbanRegex = new (@"^BG\d{2}[A-Z]{4}\d{6}[A-Za-z0-9]{8}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Costa Rica IBAN.
    /// </summary>
    private static readonly Regex CostaRicaIbanRegex = new (@"^CR\d{2}\d{18}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Croatia IBAN.
    /// </summary>
    private static readonly Regex CroatiaIbanRegex = new (@"^HR\d{2}\d{17}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Cyprus IBAN.
    /// </summary>
    private static readonly Regex CyprusIbanRegex = new (@"^CY\d{2}\d{8}[A-Za-z0-9]{16}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Czech Republic IBAN.
    /// </summary>
    private static readonly Regex CzechRepublicIbanRegex = new (@"^CZ\d{2}\d{20}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Denmark IBAN.
    /// </summary>
    private static readonly Regex DenmarkIbanRegex = new (@"^DK\d{2}\d{14}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Dominican Republic IBAN.
    /// </summary>
    private static readonly Regex DominicanRepublicIbanRegex = new (@"^DO\d{2}[A-Za-z0-9]{4}\d{20}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for East Timor IBAN.
    /// </summary>
    private static readonly Regex EastTimorIbanRegex = new (@"^TL\d{2}\d{19}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Egypt IBAN.
    /// </summary>
    private static readonly Regex EgyptIbanRegex = new (@"^EG\d{2}\d{25}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for El Salvador IBAN.
    /// </summary>
    private static readonly Regex ElSalvadorIbanRegex = new (@"^SV\d{2}[A-Z]{4}\d{20}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Estonia IBAN.
    /// </summary>
    private static readonly Regex EstoniaIbanRegex = new (@"^EE\d{2}\d{16}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Faroe Islands IBAN.
    /// </summary>
    private static readonly Regex FaroeIslandsIbanRegex = new (@"^FO\d{2}\d{14}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Finland IBAN.
    /// </summary>
    private static readonly Regex FinlandIbanRegex = new (@"^FI\d{2}\d{14}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for France IBAN.
    /// </summary>
    private static readonly Regex FranceIbanRegex = new (@"^FR\d{2}\d{10}[A-Za-z0-9]{11}\d{2}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Georgia IBAN.
    /// </summary>
    private static readonly Regex GeorgiaIbanRegex = new (@"^GE\d{2}[A-Z]{2}\d{16}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Germany IBAN.
    /// </summary>
    private static readonly Regex GermanyIbanRegex = new (@"^DE\d{2}\d{18}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Gibraltar IBAN.
    /// </summary>
    private static readonly Regex GibraltarIbanRegex = new (@"^GI\d{2}[A-Z]{4}[A-Za-z0-9]{15}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Greece IBAN.
    /// </summary>
    private static readonly Regex GreeceIbanRegex = new (@"^GR\d{2}\d{7}[A-Za-z0-9]{16}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Greenland IBAN.
    /// </summary>
    private static readonly Regex GreenlandIbanRegex = new (@"^GL\d{2}\d{14}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Guatemala IBAN.
    /// </summary>
    private static readonly Regex GuatemalaIbanRegex = new (@"^GT\d{2}[A-Za-z0-9]{24}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Hungary IBAN.
    /// </summary>
    private static readonly Regex HungaryIbanRegex = new (@"^HU\d{2}\d{24}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Iceland IBAN.
    /// </summary>
    private static readonly Regex IcelandIbanRegex = new (@"^IS\d{2}\d{22}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Iraq IBAN.
    /// </summary>
    private static readonly Regex IraqIbanRegex = new (@"^IQ\d{2}[A-Z]{4}\d{15}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Ireland IBAN.
    /// </summary>
    private static readonly Regex IrelandIbanRegex = new (@"^IE\d{2}[A-Z]{4}\d{14}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Israel IBAN.
    /// </summary>
    private static readonly Regex IsraelIbanRegex = new (@"^IL\d{2}\d{19}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Italy IBAN.
    /// </summary>
    private static readonly Regex ItalyIbanRegex = new (@"^IT\d{2}[A-Z]{1}\d{10}[A-Za-z0-9]{12}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Jordan IBAN.
    /// </summary>
    private static readonly Regex JordanIbanRegex = new (@"^JO\d{2}[A-Z]{4}\d{4}[A-Za-z0-9]{18}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Kazakhstan IBAN.
    /// </summary>
    private static readonly Regex KazakhstanIbanRegex = new (@"^KZ\d{2}\d{3}[A-Za-z0-9]{13}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Kosovo IBAN.
    /// </summary>
    private static readonly Regex KosovoIbanRegex = new (@"^XK\d{2}\d{4}\d{12}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Kuwait IBAN.
    /// </summary>
    private static readonly Regex KuwaitIbanRegex = new (@"^KW\d{2}[A-Z]{4}[A-Za-z0-9]{22}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Latvia IBAN.
    /// </summary>
    private static readonly Regex LatviaIbanRegex = new (@"^LV\d{2}[A-Z]{4}[A-Za-z0-9]{13}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Lebanon IBAN.
    /// </summary>
    private static readonly Regex LebanonIbanRegex = new (@"^LB\d{2}\d{4}[A-Za-z0-9]{20}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Libya IBAN.
    /// </summary>
    private static readonly Regex LibyaIbanRegex = new (@"^LY\d{2}\d{21}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Liechtenstein IBAN.
    /// </summary>
    private static readonly Regex LiechtensteinIbanRegex = new (@"^LI\d{2}\d{5}[A-Za-z0-9]{12}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Lithuania IBAN.
    /// </summary>
    private static readonly Regex LithuaniaIbanRegex = new (@"^LT\d{2}\d{16}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Luxembourg IBAN.
    /// </summary>
    private static readonly Regex LuxembourgIbanRegex = new (@"^LU\d{2}\d{3}[A-Za-z0-9]{13}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Malta IBAN.
    /// </summary>
    private static readonly Regex MaltaIbanRegex = new (@"^MT\d{2}[A-Z]{4}\d{5}[A-Za-z0-9]{18}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Mauritania IBAN.
    /// </summary>
    private static readonly Regex MauritaniaIbanRegex = new (@"^MR\d{2}\d{23}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Mauritius IBAN.
    /// </summary>
    private static readonly Regex MauritiusIbanRegex = new (@"^MU\d{2}[A-Z]{4}\d{19}[A-Z]{3}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Monaco IBAN.
    /// </summary>
    private static readonly Regex MonacoIbanRegex = new (@"^MC\d{2}\d{10}[A-Za-z0-9]{11}\d{2}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Moldova IBAN.
    /// </summary>
    private static readonly Regex MoldovaIbanRegex = new (@"^MD\d{2}[A-Za-z0-9]{2}[A-Za-z0-9]{18}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Montenegro IBAN.
    /// </summary>
    private static readonly Regex MontenegroIbanRegex = new (@"^ME\d{2}\d{18}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Netherlands IBAN.
    /// </summary>
    private static readonly Regex NetherlandsIbanRegex = new (@"^NL\d{2}[A-Z]{4}\d{10}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for North Macedonia IBAN.
    /// </summary>
    private static readonly Regex NorthMacedoniaIbanRegex = new (@"^MK\d{2}\d{3}[A-Za-z0-9]{10}\d{2}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Norway IBAN.
    /// </summary>
    private static readonly Regex NorwayIbanRegex = new (@"^NO\d{2}\d{11}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Pakistan IBAN.
    /// </summary>
    private static readonly Regex PakistanIbanRegex = new (@"^PK\d{2}[A-Z]{4}[A-Za-z0-9]{16}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Palestinian Territories IBAN.
    /// </summary>
    private static readonly Regex PalestinianterritoriesIbanRegex = new (@"^PS\d{2}[A-Z]{4}[A-Za-z0-9]{21}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Poland IBAN.
    /// </summary>
    private static readonly Regex PolandIbanRegex = new (@"^PL\d{2}\d{24}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Portugal IBAN.
    /// </summary>
    private static readonly Regex PortugalIbanRegex = new (@"^PT\d{2}\d{21}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Qatar IBAN.
    /// </summary>
    private static readonly Regex QatarIbanRegex = new (@"^QA\d{2}[A-Z]{4}[A-Za-z0-9]{21}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Romania IBAN.
    /// </summary>
    private static readonly Regex RomaniaIbanRegex = new (@"^RO\d{2}[A-Z]{4}[A-Za-z0-9]{16}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Russia IBAN.
    /// </summary>
    private static readonly Regex RussiaIbanRegex = new (@"^RU\d{2}\d{14}[A-Za-z0-9]{15}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Saint Lucia IBAN.
    /// </summary>
    private static readonly Regex SaintLuciaIbanRegex = new (@"^LC\d{2}[A-Z]{4}[A-Za-z0-9]{24}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for San Marino IBAN.
    /// </summary>
    private static readonly Regex SanMarinoIbanRegex = new (@"^SM\d{2}[A-Z]{1}\d{10}[A-Za-z0-9]{12}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for São Tomé and Príncipe IBAN.
    /// </summary>
    private static readonly Regex SãoToméandPríncipeIbanRegex = new (@"^ST\d{2}\d{21}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Saudi Arabia IBAN.
    /// </summary>
    private static readonly Regex SaudiArabiaIbanRegex = new (@"^SA\d{2}\d{2}[A-Za-z0-9]{18}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Serbia IBAN.
    /// </summary>
    private static readonly Regex SerbiaIbanRegex = new (@"^RS\d{2}\d{18}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Seychelles IBAN.
    /// </summary>
    private static readonly Regex SeychellesIbanRegex = new (@"^SC\d{2}[A-Z]{4}\d{20}[A-Z]{3}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Slovakia IBAN.
    /// </summary>
    private static readonly Regex SlovakiaIbanRegex = new (@"^SK\d{2}\d{20}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Slovenia IBAN.
    /// </summary>
    private static readonly Regex SloveniaIbanRegex = new (@"^SI\d{2}\d{15}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Spain IBAN.
    /// </summary>
    private static readonly Regex SpainIbanRegex = new (@"^ES\d{2}\d{20}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Sudan IBAN.
    /// </summary>
    private static readonly Regex SudanIbanRegex = new (@"^SD\d{2}\d{14}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Sweden IBAN.
    /// </summary>
    private static readonly Regex SwedenIbanRegex = new (@"^SE\d{2}\d{20}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Switzerland IBAN.
    /// </summary>
    private static readonly Regex SwitzerlandIbanRegex = new (@"^CH\d{2}\d{5}[A-Za-z0-9]{12}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Tunisia IBAN.
    /// </summary>
    private static readonly Regex TunisiaIbanRegex = new (@"^TN\d{2}\d{20}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Turkey IBAN.
    /// </summary>
    private static readonly Regex TurkeyIbanRegex = new (@"^TR\d{2}\d{6}[A-Za-z0-9]{16}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Ukraine IBAN.
    /// </summary>
    private static readonly Regex UkraineIbanRegex = new (@"^UA\d{2}\d{6}[A-Za-z0-9]{ 1}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for United Arab Emirates IBAN.
    /// </summary>
    private static readonly Regex UnitedArabEmiratesIbanRegex = new (@"^AE\d{2}\d{3}\d{16}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for United Kingdom IBAN.
    /// </summary>
    private static readonly Regex UnitedKingdomIbanRegex = new (@"^GB\d{2}[A-Z]{4}\d{14}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Vatican City IBAN.
    /// </summary>
    private static readonly Regex VaticanCityIbanRegex = new (@"^VA\d{2}\d{3}\d{15}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for British Virgin Islands.
    /// </summary>
    private static readonly Regex BritishVirginIslandsIbanRegex = new (@"^VG\d{2}[A-Z]{4}\d{16}$", RegexOptions.None);

    /*
     * Aspirational
     */

    /// <summary>
    /// Regular expression for Algeria - aspirational.
    /// </summary>
    private static readonly Regex AlgeriaIbanRegex = new (@"^DZ\d{2}\d{22}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Angola - aspirational.
    /// </summary>
    private static readonly Regex AngolaIbanRegex = new (@"^AO\d{2}\d{21}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Benin - aspirational.
    /// </summary>
    private static readonly Regex BeninIbanRegex = new (@"^BJ\d{2}[A-Za-z0-9]{2}\d{22}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Burkina Faso - aspirational.
    /// </summary>
    private static readonly Regex BurkinaFasoIbanRegex = new (@"^BF\d{2}[A-Za-z0-9]{2}\d{22}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Burundi - aspirational.
    /// </summary>
    private static readonly Regex BurundiIbanRegex = new (@"^BI\d{2}\d{23}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Cabo - aspirational.
    /// </summary>
    private static readonly Regex CaboVerdeIbanRegex = new (@"^CV\d{2}\d{21}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Cameroon - aspirational.
    /// </summary>
    private static readonly Regex CameroonIbanRegex = new (@"^CM\d{2}\d{23}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for the Central African Republic - aspirational.
    /// </summary>
    private static readonly Regex CentralAfricanRepublicIbanRegex = new (@"^CF\d{2}\d{23}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Chad - aspirational.
    /// </summary>
    private static readonly Regex ChadIbanRegex = new (@"^TD\d{2}\d{23}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Comoros - aspirational.
    /// </summary>
    private static readonly Regex ComorosIbanRegex = new (@"^KM\d{2}\d{23}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Republic of the Congo - aspirational.
    /// </summary>
    private static readonly Regex RepublicOfTheCongoIbanRegex = new (@"^CG\d{2}\d{23}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Côte d'Ivoire - aspirational.
    /// </summary>
    private static readonly Regex CôtedIvoireIbanRegex = new (@"^CI\d{2}[A-Z]{1}\d{23}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Djibouti - aspirational.
    /// </summary>
    private static readonly Regex DjiboutiIbanRegex = new (@"^DJ\d{2}\d{23}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Equatorial Guinea - aspirational.
    /// </summary>
    private static readonly Regex EquatorialGuineaIbanRegex = new (@"^GQ\d{2}\d{23}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Gabon - aspirational.
    /// </summary>
    private static readonly Regex GabonIbanRegex = new (@"^GA\d{2}\d{23}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Guinea Bissau - aspirational.
    /// </summary>
    private static readonly Regex GuineaBissauIbanRegex = new (@"^GW\d{2}[A-Za-z0-9]{2}\d{19}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Honduras - aspirational.
    /// </summary>
    private static readonly Regex HondurasIbanRegex = new (@"^HN\d{2}[A-Z]{4}\d{20}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Iran - aspirational.
    /// </summary>
    private static readonly Regex IranIbanRegex = new (@"^IR\d{2}\d{22}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Madagascar - aspirational.
    /// </summary>
    private static readonly Regex MadagascarIbanRegex = new (@"^MG\d{2}\d{23}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Mali - aspirational.
    /// </summary>
    private static readonly Regex MaliIbanRegex = new (@"^ML\d{2}[A-Za-z0-9]{2}\d{22}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Morocco - aspirational.
    /// </summary>
    private static readonly Regex MoroccoIbanRegex = new (@"^MA\d{2}\d{24}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Mozambique - aspirational.
    /// </summary>
    private static readonly Regex MozambiqueIbanRegex = new (@"^MZ\d{2}\d{21}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Nicaragua - aspirational.
    /// </summary>
    private static readonly Regex NicaraguaIbanRegex = new (@"^NI\d{2}[A-Z]{4}\d{24}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Niger - aspirational.
    /// </summary>
    private static readonly Regex NigerIbanRegex = new (@"^NE\d{2}[A-Z]{2}\d{22}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Senegal - aspirational.
    /// </summary>
    private static readonly Regex SenegalIbanRegex = new (@"^SN\d{2}[A-Z]{2}\d{22}$", RegexOptions.None);

    /// <summary>
    /// Regular expression for Togo - aspirational.
    /// </summary>
    private static readonly Regex TogoIbanRegex = new (@"^TG\d{2}[A-Z]{2}\d{22}$", RegexOptions.None);
#endif

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
        if (!IbanRegex().IsMatch(value)) {
#else
        if (!IbanRegex.IsMatch(value)) {
#endif
            validationErrors.Add(AddException(2012, Resources.GS1_Error_011));
            return false;
        }

#if NET6_0_OR_GREATER
        var countryCode = value[..2];
#else
        var countryCode = value.Substring(0, 2);
#endif

        var countrySpecificRegex = countryCode switch {
#if NET7_0_OR_GREATER
            "AL" => AlbaniaIbanRegex(),
            "AD" => AndorraIbanRegex(),
            "AT" => AustriaIbanRegex(),
            "AZ" => AzerbaijanIbanRegex(),
            "BH" => BahrainIbanRegex(),
            "BY" => BelarusIbanRegex(),
            "BE" => BelgiumIbanRegex(),
            "BA" => BosniaAndHerzegovinaIbanRegex(),
            "BR" => BrazilIbanRegex(),
            "BG" => BulgariaIbanRegex(),
            "CR" => CostaRicaIbanRegex(),
            "HR" => CroatiaIbanRegex(),
            "CY" => CyprusIbanRegex(),
            "CZ" => CzechRepublicIbanRegex(),
            "DK" => DenmarkIbanRegex(),
            "DO" => DominicanRepublicIbanRegex(),
            "TL" => EastTimorIbanRegex(),
            "EG" => EgyptIbanRegex(),
            "SV" => ElSalvadorIbanRegex(),
            "EE" => EstoniaIbanRegex(),
            "FO" => FaroeIslandsIbanRegex(),
            "FI" => FinlandIbanRegex(),
            "FR" => FranceIbanRegex(),
            "GE" => GeorgiaIbanRegex(),
            "DE" => GermanyIbanRegex(),
            "GI" => GibraltarIbanRegex(),
            "GR" => GreeceIbanRegex(),
            "GL" => GreenlandIbanRegex(),
            "GT" => GuatemalaIbanRegex(),
            "HU" => HungaryIbanRegex(),
            "IS" => IcelandIbanRegex(),
            "IQ" => IraqIbanRegex(),
            "IE" => IrelandIbanRegex(),
            "IL" => IsraelIbanRegex(),
            "IT" => ItalyIbanRegex(),
            "JO" => JordanIbanRegex(),
            "KZ" => KazakhstanIbanRegex(),
            "XK" => KosovoIbanRegex(),
            "KW" => KuwaitIbanRegex(),
            "LV" => LatviaIbanRegex(),
            "LB" => LebanonIbanRegex(),
            "LY" => LibyaIbanRegex(),
            "LI" => LiechtensteinIbanRegex(),
            "LT" => LithuaniaIbanRegex(),
            "LU" => LuxembourgIbanRegex(),
            "MT" => MaltaIbanRegex(),
            "MR" => MauritaniaIbanRegex(),
            "MU" => MauritiusIbanRegex(),
            "MC" => MonacoIbanRegex(),
            "MD" => MoldovaIbanRegex(),
            "ME" => MontenegroIbanRegex(),
            "NL" => NetherlandsIbanRegex(),
            "MK" => NorthMacedoniaIbanRegex(),
            "NO" => NorwayIbanRegex(),
            "PK" => PakistanIbanRegex(),
            "PS" => PalestinianTerritoriesIbanRegex(),
            "PL" => PolandIbanRegex(),
            "PT" => PortugalIbanRegex(),
            "QA" => QatarIbanRegex(),
            "RO" => RomaniaIbanRegex(),
            "RU" => RussiaIbanRegex(),
            "LC" => SaintLuciaIbanRegex(),
            "SM" => SanMarinoIbanRegex(),
            "ST" => SãoToméAndPríncipeIbanRegex(),
            "SA" => SaudiArabiaIbanRegex(),
            "RS" => SerbiaIbanRegex(),
            "SC" => SeychellesIbanRegex(),
            "SK" => SlovakiaIbanRegex(),
            "SI" => SloveniaIbanRegex(),
            "ES" => SpainIbanRegex(),
            "SD" => SudanIbanRegex(),
            "SE" => SwedenIbanRegex(),
            "CH" => SwitzerlandIbanRegex(),
            "TN" => TunisiaIbanRegex(),
            "TR" => TurkeyIbanRegex(),
            "UA" => UkraineIbanRegex(),
            "AE" => UnitedArabEmiratesIbanRegex(),
            "GB" => UnitedKingdomIbanRegex(),
            "VA" => VaticanCityIbanRegex(),
            "VG" => BritishVirginIslandsIbanRegex(),
            _ => null
#else
            "AL" => AlbaniaIbanRegex,
            "AD" => AndorraIbanRegex,
            "AT" => AustriaIbanRegex,
            "AZ" => AzerbaijanIbanRegex,
            "BH" => BahrainIbanRegex,
            "BY" => BelarusIbanRegex,
            "BE" => BelgiumIbanRegex,
            "BA" => BosniaAndHerzegovinaIbanRegex,
            "BR" => BrazilIbanRegex,
            "BG" => BulgariaIbanRegex,
            "CR" => CostaRicaIbanRegex,
            "HR" => CroatiaIbanRegex,
            "CY" => CyprusIbanRegex,
            "CZ" => CzechRepublicIbanRegex,
            "DK" => DenmarkIbanRegex,
            "DO" => DominicanRepublicIbanRegex,
            "TL" => EastTimorIbanRegex,
            "EG" => EgyptIbanRegex,
            "SV" => ElSalvadorIbanRegex,
            "EE" => EstoniaIbanRegex,
            "FO" => FaroeIslandsIbanRegex,
            "FI" => FinlandIbanRegex,
            "FR" => FranceIbanRegex,
            "GE" => GeorgiaIbanRegex,
            "DE" => GermanyIbanRegex,
            "GI" => GibraltarIbanRegex,
            "GR" => GreeceIbanRegex,
            "GL" => GreenlandIbanRegex,
            "GT" => GuatemalaIbanRegex,
            "HU" => HungaryIbanRegex,
            "IS" => IcelandIbanRegex,
            "IQ" => IraqIbanRegex,
            "IE" => IrelandIbanRegex,
            "IL" => IsraelIbanRegex,
            "IT" => ItalyIbanRegex,
            "JO" => JordanIbanRegex,
            "KZ" => KazakhstanIbanRegex,
            "XK" => KosovoIbanRegex,
            "KW" => KuwaitIbanRegex,
            "LV" => LatviaIbanRegex,
            "LB" => LebanonIbanRegex,
            "LY" => LibyaIbanRegex,
            "LI" => LiechtensteinIbanRegex,
            "LT" => LithuaniaIbanRegex,
            "LU" => LuxembourgIbanRegex,
            "MT" => MaltaIbanRegex,
            "MR" => MauritaniaIbanRegex,
            "MU" => MauritiusIbanRegex,
            "MC" => MonacoIbanRegex,
            "MD" => MoldovaIbanRegex,
            "ME" => MontenegroIbanRegex,
            "NL" => NetherlandsIbanRegex,
            "MK" => NorthMacedoniaIbanRegex,
            "NO" => NorwayIbanRegex,
            "PK" => PakistanIbanRegex,
            "PS" => PalestinianterritoriesIbanRegex,
            "PL" => PolandIbanRegex,
            "PT" => PortugalIbanRegex,
            "QA" => QatarIbanRegex,
            "RO" => RomaniaIbanRegex,
            "RU" => RussiaIbanRegex,
            "LC" => SaintLuciaIbanRegex,
            "SM" => SanMarinoIbanRegex,
            "ST" => SãoToméandPríncipeIbanRegex,
            "SA" => SaudiArabiaIbanRegex,
            "RS" => SerbiaIbanRegex,
            "SC" => SeychellesIbanRegex,
            "SK" => SlovakiaIbanRegex,
            "SI" => SloveniaIbanRegex,
            "ES" => SpainIbanRegex,
            "SD" => SudanIbanRegex,
            "SE" => SwedenIbanRegex,
            "CH" => SwitzerlandIbanRegex,
            "TN" => TunisiaIbanRegex,
            "TR" => TurkeyIbanRegex,
            "UA" => UkraineIbanRegex,
            "AE" => UnitedArabEmiratesIbanRegex,
            "GB" => UnitedKingdomIbanRegex,
            "VA" => VaticanCityIbanRegex,
            "VG" => BritishVirginIslandsIbanRegex,
            _ => null
#endif
        };

        if (countrySpecificRegex is null) {
            var aspirationalCountryRegEx = CheckAspirational();

            if (aspirationalCountryRegEx?.IsMatch(value) ?? false) {
                validationErrors.Add(AddException(2014, Resources.GS1_Error_013, countryCode));
                return false;
            }
        }
        else if (countrySpecificRegex.IsMatch(value)) {
            if (ValidateCheckDigits()) {
                return result;
            }

            validationErrors.Add(AddException(2015, Resources.GS1_Error_014, countryCode));
        }

        validationErrors.Add(AddException(2013, Resources.GS1_Error_012, countryCode));
        return false;

        bool ValidateCheckDigits() {
#if NET7_0_OR_GREATER
            var normalisedValue = value[4..] + value[..4];
#else
            var normalisedValue = value.Substring(4) + value.Substring(0, 4);
#endif

            var builder = new StringBuilder();

#if NETCOREAPP3_0_OR_GREATER
            foreach (var c in normalisedValue.AsSpan()) {
#else
            foreach (var c in normalisedValue) {
#endif
                builder.Append(c switch {
                    _ when c >= 65 && c <= 90 => (c - 55).ToString("D2"),
                    _ => c
                });
            }

            return (BigInteger.Parse(builder.ToString(), CultureInfo.InvariantCulture) % 97) == 1;
        }

        Regex? CheckAspirational() =>
            countryCode switch {
#if NET7_0_OR_GREATER
                "DZ" => AlgeriaIbanRegex(),
                "AO" => AngolaIbanRegex(),
                "BJ" => BeninIbanRegex(),
                "BF" => BurkinaFasoIbanRegex(),
                "BI" => BurundiIbanRegex(),
                "CV" => CaboVerdeIbanRegex(),
                "CM" => CameroonIbanRegex(),
                "CF" => CentralAfricanRepublicIbanRegex(),
                "TD" => ChadIbanRegex(),
                "KM" => ComorosIbanRegex(),
                "CG" => RepublicOfTheCongoIbanRegex(),
                "CI" => CôtedIvoireIbanRegex(),
                "DJ" => DjiboutiIbanRegex(),
                "GQ" => EquatorialGuineaIbanRegex(),
                "GA" => GabonIbanRegex(),
                "GW" => GuineaBissauIbanRegex(),
                "HN" => HondurasIbanRegex(),
                "IR" => IranIbanRegex(),
                "MG" => MadagascarIbanRegex(),
                "ML" => MaliIbanRegex(),
                "MA" => MoroccoIbanRegex(),
                "MZ" => MozambiqueIbanRegex(),
                "NI" => NicaraguaIbanRegex(),
                "NE" => NigerIbanRegex(),
                "SN" => SenegalIbanRegex(),
                "TG" => TogoIbanRegex(),
                _ => null
#else
                "DZ" => AlgeriaIbanRegex,
                "AO" => AngolaIbanRegex,
                "BJ" => BeninIbanRegex,
                "BF" => BurkinaFasoIbanRegex,
                "BI" => BurundiIbanRegex,
                "CV" => CaboVerdeIbanRegex,
                "CM" => CameroonIbanRegex,
                "CF" => CentralAfricanRepublicIbanRegex,
                "TD" => ChadIbanRegex,
                "KM" => ComorosIbanRegex,
                "CG" => RepublicOfTheCongoIbanRegex,
                "CI" => CôtedIvoireIbanRegex,
                "DJ" => DjiboutiIbanRegex,
                "GQ" => EquatorialGuineaIbanRegex,
                "GA" => GabonIbanRegex,
                "GW" => GuineaBissauIbanRegex,
                "HN" => HondurasIbanRegex,
                "IR" => IranIbanRegex,
                "MG" => MadagascarIbanRegex,
                "ML" => MaliIbanRegex,
                "MA" => MoroccoIbanRegex,
                "MZ" => MozambiqueIbanRegex,
                "NI" => NicaraguaIbanRegex,
                "NE" => NigerIbanRegex,
                "SN" => SenegalIbanRegex,
                "TG" => TogoIbanRegex,
                _ => null
#endif
            };

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
    /// Regular expression for IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^[A-Z]{2}\d{2}[A-Za-z0-9]{1,30}$", RegexOptions.None, "en-US")]
    private static partial Regex IbanRegex();

    /// <summary>
    /// Regular expression for Albanian IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^AL\d{2}\d{8}[A-Za-z0-9]{16}$", RegexOptions.None, "en-US")]
    private static partial Regex AlbaniaIbanRegex();

    /// <summary>
    /// Regular expression for Andorra IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^AD\d{2}\d{8}[A-Za-z0-9]{12}$", RegexOptions.None, "en-US")]
    private static partial Regex AndorraIbanRegex();

    /// <summary>
    /// Regular expression for Austria IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^AT\d{2}[A-Za-z0-9]{16}$", RegexOptions.None, "en-US")]
    private static partial Regex AustriaIbanRegex();

    /// <summary>
    /// Regular expression for Azerbaijan IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^AZ\d{2}[A-Z]{4}[A-Za-z0-9]{20}$", RegexOptions.None, "en-US")]
    private static partial Regex AzerbaijanIbanRegex();

    /// <summary>
    /// Regular expression for Bahrain IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^BH\d{2}[A-Z]{4}[A-Za-z0-9]{14}$", RegexOptions.None, "en-US")]
    private static partial Regex BahrainIbanRegex();

    /// <summary>
    /// Regular expression for Belarus IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^BY\d{2}[A-Za-z0-9]{4}\d{4}[A-Za-z0-9]{16}$", RegexOptions.None, "en-US")]
    private static partial Regex BelarusIbanRegex();

    /// <summary>
    /// Regular expression for Belgium IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^BE\d{2}\d{12}$", RegexOptions.None, "en-US")]
    private static partial Regex BelgiumIbanRegex();

    /// <summary>
    /// Regular expression for Bosnia and Herzegovina IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^BA\d{2}\d{16}$", RegexOptions.None, "en-US")]
    private static partial Regex BosniaAndHerzegovinaIbanRegex();

    /// <summary>
    /// Regular expression for Brazil IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^BR\d{2}\d{23}[A-Z][A-Za-z0-9]$", RegexOptions.None, "en-US")]
    private static partial Regex BrazilIbanRegex();

    /// <summary>
    /// Regular expression for Bulgaria IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^BG\d{2}[A-Z]{4}\d{6}[A-Za-z0-9]{8}$", RegexOptions.None, "en-US")]
    private static partial Regex BulgariaIbanRegex();

    /// <summary>
    /// Regular expression for Costa Rica IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^CR\d{2}\d{18}$", RegexOptions.None, "en-US")]
    private static partial Regex CostaRicaIbanRegex();

    /// <summary>
    /// Regular expression for Croatia IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^HR\d{2}\d{17}$", RegexOptions.None, "en-US")]
    private static partial Regex CroatiaIbanRegex();

    /// <summary>
    /// Regular expression for Cyprus IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^CY\d{2}\d{8}[A-Za-z0-9]{16}$", RegexOptions.None, "en-US")]
    private static partial Regex CyprusIbanRegex();

    /// <summary>
    /// Regular expression for Czech Republic IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^CZ\d{2}\d{20}$", RegexOptions.None, "en-US")]
    private static partial Regex CzechRepublicIbanRegex();

    /// <summary>
    /// Regular expression for Denmark IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^DK\d{2}\d{14}$", RegexOptions.None, "en-US")]
    private static partial Regex DenmarkIbanRegex();

    /// <summary>
    /// Regular expression for Dominican Republic IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^DO\d{2}[A-Za-z0-9]{4}\d{20}$", RegexOptions.None, "en-US")]
    private static partial Regex DominicanRepublicIbanRegex();

    /// <summary>
    /// Regular expression for East Timor IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^TL\d{2}\d{19}$", RegexOptions.None, "en-US")]
    private static partial Regex EastTimorIbanRegex();

    /// <summary>
    /// Regular expression for Egypt IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^EG\d{2}\d{25}$", RegexOptions.None, "en-US")]
    private static partial Regex EgyptIbanRegex();

    /// <summary>
    /// Regular expression for El Salvador IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^SV\d{2}[A-Z]{4}\d{20}$", RegexOptions.None, "en-US")]
    private static partial Regex ElSalvadorIbanRegex();

    /// <summary>
    /// Regular expression for Estonia IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^EE\d{2}\d{16}$", RegexOptions.None, "en-US")]
    private static partial Regex EstoniaIbanRegex();

    /// <summary>
    /// Regular expression for Faroe Islands IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^FO\d{2}\d{14}$", RegexOptions.None, "en-US")]
    private static partial Regex FaroeIslandsIbanRegex();

    /// <summary>
    /// Regular expression for Finland IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^FI\d{2}\d{14}$", RegexOptions.None, "en-US")]
    private static partial Regex FinlandIbanRegex();

    /// <summary>
    /// Regular expression for France IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^FR\d{2}\d{10}[A-Za-z0-9]{11}\d{2}$", RegexOptions.None, "en-US")]
    private static partial Regex FranceIbanRegex();

    /// <summary>
    /// Regular expression for Georgia IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^GE\d{2}[A-Z]{2}\d{16}$", RegexOptions.None, "en-US")]
    private static partial Regex GeorgiaIbanRegex();

    /// <summary>
    /// Regular expression for Germany IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^DE\d{2}\d{18}$", RegexOptions.None, "en-US")]
    private static partial Regex GermanyIbanRegex();

    /// <summary>
    /// Regular expression for Gibraltar IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^GI\d{2}[A-Z]{4}[A-Za-z0-9]{15}$", RegexOptions.None, "en-US")]
    private static partial Regex GibraltarIbanRegex();

    /// <summary>
    /// Regular expression for Greece IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^GR\d{2}\d{7}[A-Za-z0-9]{16}$", RegexOptions.None, "en-US")]
    private static partial Regex GreeceIbanRegex();

    /// <summary>
    /// Regular expression for Greenland IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^GL\d{2}\d{14}$", RegexOptions.None, "en-US")]
    private static partial Regex GreenlandIbanRegex();

    /// <summary>
    /// Regular expression for Guatemala IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^GT\d{2}[A-Za-z0-9]{24}$", RegexOptions.None, "en-US")]
    private static partial Regex GuatemalaIbanRegex();

    /// <summary>
    /// Regular expression for Hungary IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^HU\d{2}\d{24}$", RegexOptions.None, "en-US")]
    private static partial Regex HungaryIbanRegex();

    /// <summary>
    /// Regular expression for Iceland IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^IS\d{2}\d{22}$", RegexOptions.None, "en-US")]
    private static partial Regex IcelandIbanRegex();

    /// <summary>
    /// Regular expression for Iraq IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^IQ\d{2}[A-Z]{4}\d{15}$", RegexOptions.None, "en-US")]
    private static partial Regex IraqIbanRegex();

    /// <summary>
    /// Regular expression for Ireland IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^IE\d{2}[A-Z]{4}\d{14}$", RegexOptions.None, "en-US")]
    private static partial Regex IrelandIbanRegex();

    /// <summary>
    /// Regular expression for Israel IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^IL\d{2}\d{19}$", RegexOptions.None, "en-US")]
    private static partial Regex IsraelIbanRegex();

    /// <summary>
    /// Regular expression for Italy IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^IT\d{2}[A-Z]{1}\d{10}[A-Za-z0-9]{12}$", RegexOptions.None, "en-US")]
    private static partial Regex ItalyIbanRegex();

    /// <summary>
    /// Regular expression for Jordan IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^JO\d{2}[A-Z]{4}\d{4}[A-Za-z0-9]{18}$", RegexOptions.None, "en-US")]
    private static partial Regex JordanIbanRegex();

    /// <summary>
    /// Regular expression for Kazakhstan IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^KZ\d{2}\d{3}[A-Za-z0-9]{13}$", RegexOptions.None, "en-US")]
    private static partial Regex KazakhstanIbanRegex();

    /// <summary>
    /// Regular expression for Kosovo IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^XK\d{2}\d{4}\d{12}$", RegexOptions.None, "en-US")]
    private static partial Regex KosovoIbanRegex();

    /// <summary>
    /// Regular expression for Kuwait IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^KW\d{2}[A-Z]{4}[A-Za-z0-9]{22}$", RegexOptions.None, "en-US")]
    private static partial Regex KuwaitIbanRegex();

    /// <summary>
    /// Regular expression for Latvia IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^LV\d{2}[A-Z]{4}[A-Za-z0-9]{13}$", RegexOptions.None, "en-US")]
    private static partial Regex LatviaIbanRegex();

    /// <summary>
    /// Regular expression for Lebanon IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^LB\d{2}\d{4}[A-Za-z0-9]{20}$", RegexOptions.None, "en-US")]
    private static partial Regex LebanonIbanRegex();

    /// <summary>
    /// Regular expression for Libya IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^LY\d{2}\d{21}$", RegexOptions.None, "en-US")]
    private static partial Regex LibyaIbanRegex();

    /// <summary>
    /// Regular expression for Liechtenstein IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^LI\d{2}\d{5}[A-Za-z0-9]{12}$", RegexOptions.None, "en-US")]
    private static partial Regex LiechtensteinIbanRegex();

    /// <summary>
    /// Regular expression for Lithuania IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^LT\d{2}\d{16}$", RegexOptions.None, "en-US")]
    private static partial Regex LithuaniaIbanRegex();

    /// <summary>
    /// Regular expression for Luxembourg IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^LU\d{2}\d{3}[A-Za-z0-9]{13}$", RegexOptions.None, "en-US")]
    private static partial Regex LuxembourgIbanRegex();

    /// <summary>
    /// Regular expression for Malta IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^MT\d{2}[A-Z]{4}\d{5}[A-Za-z0-9]{18}$", RegexOptions.None, "en-US")]
    private static partial Regex MaltaIbanRegex();

    /// <summary>
    /// Regular expression for Mauritania IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^MR\d{2}\d{23}$", RegexOptions.None, "en-US")]
    private static partial Regex MauritaniaIbanRegex();

    /// <summary>
    /// Regular expression for Mauritius IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^MU\d{2}[A-Z]{4}\d{19}[A-Z]{3}$", RegexOptions.None, "en-US")]
    private static partial Regex MauritiusIbanRegex();

    /// <summary>
    /// Regular expression for Monaco IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^MC\d{2}\d{10}[A-Za-z0-9]{11}\d{2}$", RegexOptions.None, "en-US")]
    private static partial Regex MonacoIbanRegex();

    /// <summary>
    /// Regular expression for Moldova IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^MD\d{2}[A-Za-z0-9]{2}[A-Za-z0-9]{18}$", RegexOptions.None, "en-US")]
    private static partial Regex MoldovaIbanRegex();

    /// <summary>
    /// Regular expression for Montenegro IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^ME\d{2}\d{18}$", RegexOptions.None, "en-US")]
    private static partial Regex MontenegroIbanRegex();

    /// <summary>
    /// Regular expression for Netherlands IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^NL\d{2}[A-Z]{4}\d{10}$", RegexOptions.None, "en-US")]
    private static partial Regex NetherlandsIbanRegex();

    /// <summary>
    /// Regular expression for North Macedonia IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^MK\d{2}\d{3}[A-Za-z0-9]{10}\d{2}$", RegexOptions.None, "en-US")]
    private static partial Regex NorthMacedoniaIbanRegex();

    /// <summary>
    /// Regular expression for Norway IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^NO\d{2}\d{11}$", RegexOptions.None, "en-US")]
    private static partial Regex NorwayIbanRegex();

    /// <summary>
    /// Regular expression for Pakistan IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^PK\d{2}[A-Z]{4}[A-Za-z0-9]{16}$", RegexOptions.None, "en-US")]
    private static partial Regex PakistanIbanRegex();

    /// <summary>
    /// Regular expression for Palestinian Territories IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^PS\d{2}[A-Z]{4}[A-Za-z0-9]{21}$", RegexOptions.None, "en-US")]
    private static partial Regex PalestinianTerritoriesIbanRegex();

    /// <summary>
    /// Regular expression for Poland IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^PL\d{2}\d{24}$", RegexOptions.None, "en-US")]
    private static partial Regex PolandIbanRegex();

    /// <summary>
    /// Regular expression for Portugal IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^PT\d{2}\d{21}$", RegexOptions.None, "en-US")]
    private static partial Regex PortugalIbanRegex();

    /// <summary>
    /// Regular expression for Qatar IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^QA\d{2}[A-Z]{4}[A-Za-z0-9]{21}$", RegexOptions.None, "en-US")]
    private static partial Regex QatarIbanRegex();

    /// <summary>
    /// Regular expression for Romania IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^RO\d{2}[A-Z]{4}[A-Za-z0-9]{16}$", RegexOptions.None, "en-US")]
    private static partial Regex RomaniaIbanRegex();

    /// <summary>
    /// Regular expression for Russia IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^RU\d{2}\d{14}[A-Za-z0-9]{15}$", RegexOptions.None, "en-US")]
    private static partial Regex RussiaIbanRegex();

    /// <summary>
    /// Regular expression for Saint Lucia IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^LC\d{2}[A-Z]{4}[A-Za-z0-9]{24}$", RegexOptions.None, "en-US")]
    private static partial Regex SaintLuciaIbanRegex();

    /// <summary>
    /// Regular expression for San Marino IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^SM\d{2}[A-Z]{1}\d{10}[A-Za-z0-9]{12}$", RegexOptions.None, "en-US")]
    private static partial Regex SanMarinoIbanRegex();

    /// <summary>
    /// Regular expression for São Tomé and Príncipe IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^ST\d{2}\d{21}$", RegexOptions.None, "en-US")]
    private static partial Regex SãoToméAndPríncipeIbanRegex();

    /// <summary>
    /// Regular expression for Saudi Arabia IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^SA\d{2}\d{2}[A-Za-z0-9]{18}$", RegexOptions.None, "en-US")]
    private static partial Regex SaudiArabiaIbanRegex();

    /// <summary>
    /// Regular expression for Serbia IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^RS\d{2}\d{18}$", RegexOptions.None, "en-US")]
    private static partial Regex SerbiaIbanRegex();

    /// <summary>
    /// Regular expression for Seychelles IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^SC\d{2}[A-Z]{4}\d{20}[A-Z]{3}$", RegexOptions.None, "en-US")]
    private static partial Regex SeychellesIbanRegex();

    /// <summary>
    /// Regular expression for Slovakia IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^SK\d{2}\d{20}$", RegexOptions.None, "en-US")]
    private static partial Regex SlovakiaIbanRegex();

    /// <summary>
    /// Regular expression for Slovenia IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^SI\d{2}\d{15}$", RegexOptions.None, "en-US")]
    private static partial Regex SloveniaIbanRegex();

    /// <summary>
    /// Regular expression for Spain IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^ES\d{2}\d{20}$", RegexOptions.None, "en-US")]
    private static partial Regex SpainIbanRegex();

    /// <summary>
    /// Regular expression for Sudan IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^SD\d{2}\d{14}$", RegexOptions.None, "en-US")]
    private static partial Regex SudanIbanRegex();

    /// <summary>
    /// Regular expression for Sweden IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^SE\d{2}\d{20}$", RegexOptions.None, "en-US")]
    private static partial Regex SwedenIbanRegex();

    /// <summary>
    /// Regular expression for Switzerland IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^CH\d{2}\d{5}[A-Za-z0-9]{12}$", RegexOptions.None, "en-US")]
    private static partial Regex SwitzerlandIbanRegex();

    /// <summary>
    /// Regular expression for Tunisia IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^TN\d{2}\d{20}$", RegexOptions.None, "en-US")]
    private static partial Regex TunisiaIbanRegex();

    /// <summary>
    /// Regular expression for Turkey IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^TR\d{2}\d{6}[A-Za-z0-9]{16}$", RegexOptions.None, "en-US")]
    private static partial Regex TurkeyIbanRegex();

    /// <summary>
    /// Regular expression for Ukraine IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^UA\d{2}\d{6}[A-Za-z0-9]{ 1}$", RegexOptions.None, "en-US")]
    private static partial Regex UkraineIbanRegex();

    /// <summary>
    /// Regular expression for the United Arab Emirates IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^AE\d{2}\d{3}\d{16}$", RegexOptions.None, "en-US")]
    private static partial Regex UnitedArabEmiratesIbanRegex();

    /// <summary>
    /// Regular expression for United Kingdom IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^GB\d{2}[A-Z]{4}\d{14}$", RegexOptions.None, "en-US")]
    private static partial Regex UnitedKingdomIbanRegex();

    /// <summary>
    /// Regular expression for Vatican City IBAN.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^VA\d{2}\d{3}\d{15}$", RegexOptions.None, "en-US")]
    private static partial Regex VaticanCityIbanRegex();

    /// <summary>
    /// Regular expression for British Virgin Islands.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^VG\d{2}[A-Z]{4}\d{16}$", RegexOptions.None, "en-US")]
    private static partial Regex BritishVirginIslandsIbanRegex();

    /*
     * Aspirational
     */

    /// <summary>
    /// Regular expression for Algeria - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^DZ\d{2}\d{22}$", RegexOptions.None, "en-US")]
    private static partial Regex AlgeriaIbanRegex();

    /// <summary>
    /// Regular expression for Angola - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^AO\d{2}\d{21}$", RegexOptions.None, "en-US")]
    private static partial Regex AngolaIbanRegex();

    /// <summary>
    /// Regular expression for Benin - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^BJ\d{2}[A-Za-z0-9]{2}\d{22}$", RegexOptions.None, "en-US")]
    private static partial Regex BeninIbanRegex();

    /// <summary>
    /// Regular expression for Burkina Faso - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^BF\d{2}[A-Za-z0-9]{2}\d{22}$", RegexOptions.None, "en-US")]
    private static partial Regex BurkinaFasoIbanRegex();

    /// <summary>
    /// Regular expression for Burundi - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^BI\d{2}\d{23}$", RegexOptions.None, "en-US")]
    private static partial Regex BurundiIbanRegex();

    /// <summary>
    /// Regular expression for Cabo - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^CV\d{2}\d{21}$", RegexOptions.None, "en-US")]
    private static partial Regex CaboVerdeIbanRegex();

    /// <summary>
    /// Regular expression for Cameroon - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^CM\d{2}\d{23}$", RegexOptions.None, "en-US")]
    private static partial Regex CameroonIbanRegex();

    /// <summary>
    /// Regular expression for the Central African Republic - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^CF\d{2}\d{23}$", RegexOptions.None, "en-US")]
    private static partial Regex CentralAfricanRepublicIbanRegex();

    /// <summary>
    /// Regular expression for Chad - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^TD\d{2}\d{23}$", RegexOptions.None, "en-US")]
    private static partial Regex ChadIbanRegex();

    /// <summary>
    /// Regular expression for Comoros - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^KM\d{2}\d{23}$", RegexOptions.None, "en-US")]
    private static partial Regex ComorosIbanRegex();

    /// <summary>
    /// Regular expression for Republic of the Congo - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^CG\d{2}\d{23}$", RegexOptions.None, "en-US")]
    private static partial Regex RepublicOfTheCongoIbanRegex();

    /// <summary>
    /// Regular expression for Côte d'Ivoire - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^CI\d{2}[A-Z]{1}\d{23}$", RegexOptions.None, "en-US")]
    private static partial Regex CôtedIvoireIbanRegex();

    /// <summary>
    /// Regular expression for Djibouti - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^DJ\d{2}\d{23}$", RegexOptions.None, "en-US")]
    private static partial Regex DjiboutiIbanRegex();

    /// <summary>
    /// Regular expression for Equatorial Guinea - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^GQ\d{2}\d{23}$", RegexOptions.None, "en-US")]
    private static partial Regex EquatorialGuineaIbanRegex();

    /// <summary>
    /// Regular expression for Gabon - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^GA\d{2}\d{23}$", RegexOptions.None, "en-US")]
    private static partial Regex GabonIbanRegex();

    /// <summary>
    /// Regular expression for Guinea Bissau - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^GW\d{2}[A-Za-z0-9]{2}\d{19}$", RegexOptions.None, "en-US")]
    private static partial Regex GuineaBissauIbanRegex();

    /// <summary>
    /// Regular expression for Honduras - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^HN\d{2}[A-Z]{4}\d{20}$", RegexOptions.None, "en-US")]
    private static partial Regex HondurasIbanRegex();

    /// <summary>
    /// Regular expression for Iran - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^IR\d{2}\d{22}$", RegexOptions.None, "en-US")]
    private static partial Regex IranIbanRegex();

    /// <summary>
    /// Regular expression for Madagascar - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^MG\d{2}\d{23}$", RegexOptions.None, "en-US")]
    private static partial Regex MadagascarIbanRegex();

    /// <summary>
    /// Regular expression for Mali - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^ML\d{2}[A-Za-z0-9]{2}\d{22}$", RegexOptions.None, "en-US")]
    private static partial Regex MaliIbanRegex();

    /// <summary>
    /// Regular expression for Morocco - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^MA\d{2}\d{24}$", RegexOptions.None, "en-US")]
    private static partial Regex MoroccoIbanRegex();

    /// <summary>
    /// Regular expression for Mozambique - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^MZ\d{2}\d{21}$", RegexOptions.None, "en-US")]
    private static partial Regex MozambiqueIbanRegex();

    /// <summary>
    /// Regular expression for Nicaragua - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^NI\d{2}[A-Z]{4}\d{24}$", RegexOptions.None, "en-US")]
    private static partial Regex NicaraguaIbanRegex();

    /// <summary>
    /// Regular expression for Niger - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^NE\d{2}[A-Z]{2}\d{22}$", RegexOptions.None, "en-US")]
    private static partial Regex NigerIbanRegex();

    /// <summary>
    /// Regular expression for Senegal - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^SN\d{2}[A-Z]{2}\d{22}$", RegexOptions.None, "en-US")]
    private static partial Regex SenegalIbanRegex();

    /// <summary>
    /// Regular expression for Togo - aspirational.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"^TG\d{2}[A-Z]{2}\d{22}$", RegexOptions.None, "en-US")]
    private static partial Regex TogoIbanRegex();

#endif
}