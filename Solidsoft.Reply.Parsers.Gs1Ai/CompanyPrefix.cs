// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompanyPrefix.cs" company="Solidsoft Reply Ltd.">
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
// List of countries for which GS1 assign country codes.   These codes are used as the prefix to GS1
// GTINs, but cannot be used as dependable identifiers of the country of origin of the pharmaceutical
// product.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable UnusedMember.Global

namespace Solidsoft.Reply.Parsers.Gs1Ai;

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

/// <summary>
///     List of countries for which GS1 assign country codes.   These codes are used as the prefix to GS1
///     GTINs, but cannot be used as dependable identifiers of the country of origin of the pharmaceutical
///     product.
/// </summary>
[SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1630:DocumentationTextMustContainWhitespace",
    Justification = "Reviewed. Suppression is OK here.")]
public enum CompanyPrefix {
    /// <summary>
    ///     Unknown country.
    /// </summary>
    [Description("Reserved by GS1 Global Office for allocations in non-member countries and for future use")]
    Unknown = -1,

    /// <summary>
    ///     UPC-A compatible (GTIN 8).
    /// </summary>
    [Description("UPC-A compatible (GTIN 8)")]
    UpcACompatibleGtin8 = 0,

    /// <summary>
    ///     UPC-A compatible - United States and Canada.
    /// </summary>
    [Description("UPC-A compatible - United States and Canada")]
    UpcACompatibleUnitedStatesAndCanada = 19,

    /// <summary>
    ///     UPC-A compatible - Used to issue restricted circulation numbers within a geographic region.
    /// </summary>
    [Description("UPC-A compatible - Used to issue restricted circulation numbers within a geographic region")]
    UpcACompatibleRestrictedCirculation = 20,

    /// <summary>
    ///     UPC-A compatible -  United States drugs (National Drug Code).
    /// </summary>
    [Description("UPC-A compatible -  United States drugs (National Drug Code)")]
    UpcACompatibleUnitedStatesDrugs = 30,

    /// <summary>
    ///     UPC-A compatible - GS1 US reserved for future use .
    /// </summary>
    [Description("UPC-A compatible - United States reserved for future use ")]
    UpcACompatibleUnitedStatesReserved = 50,

    /// <summary>
    ///     United States of America
    /// </summary>
    [Description("United States")]
    UnitedStates = 100,

    /// <summary>
    ///     Used to issue GS1 restricted circulation number within a geographic region
    /// </summary>
    [Description("Used to issue GS1 restricted circulation number within a geographic region")]
    RestrictedCirculation = 200,

    /// <summary>
    ///     France and Monaco.
    /// </summary>
    [Description("France and Monaco")]
    FranceAndMonaco = 300,

    /// <summary>
    ///     Bulgaria.
    /// </summary>
    [Description("Bulgaria")]
    Bulgaria = 380,

    /// <summary>
    ///     Slovenia.
    /// </summary>
    [Description("Slovenia")]
    Slovenia = 383,

    /// <summary>
    ///     Croatia.
    /// </summary>
    [Description("Croatia")]
    Croatia = 385,

    /// <summary>
    ///     Bosnia and Herzegovina
    /// </summary>
    [Description("Bosnia and Herzegovina")]
    BosniaAndHerzegovina = 387,

    /// <summary>
    ///     Montenegro.
    /// </summary>
    [Description("Montenegro")]
    Montenegro = 389,

    /// <summary>
    ///     Kosovo.
    /// </summary>
    [Description("Kosovo")]
    Kosovo = 390,

    /// <summary>
    ///     Germany.
    /// </summary>
    [Description("Germany")]
    Germany = 400,

    /// <summary>
    ///     Japan.
    /// </summary>
    [Description("Japan (new Japanese Article Number range)")]
    Japan = 450,

    /// <summary>
    ///     Russia.
    /// </summary>
    [Description("Russia")]
    Russia = 460,

    /// <summary>
    ///     Kyrgyzstan.
    /// </summary>
    [Description("Kyrgyzstan")]
    Kyrgyzstan = 470,

    /// <summary>
    ///     Taiwan.
    /// </summary>
    [Description("Republic of China (Taiwan)")]
    Taiwan = 471,

    /// <summary>
    ///     Estonia.
    /// </summary>
    [Description("Estonia")]
    Estonia = 474,

    /// <summary>
    ///     Latvia.
    /// </summary>
    [Description("Latvia")]
    Latvia = 475,

    /// <summary>
    ///     Azerbaijan.
    /// </summary>
    [Description("Azerbaijan")]
    Azerbaijan = 476,

    /// <summary>
    ///     Lithuania.
    /// </summary>
    [Description("Lithuania")]
    Lithuania = 477,

    /// <summary>
    ///     Uzbekistan.
    /// </summary>
    [Description("Uzbekistan")]
    Uzbekistan = 478,

    /// <summary>
    ///     Sri Lanka.
    /// </summary>
    [Description("Sri Lanka")]
    SriLanka = 479,

    /// <summary>
    ///     Philippines.
    /// </summary>
    [Description("Philippines")]
    Philippines = 480,

    /// <summary>
    ///     Belarus.
    /// </summary>
    [Description("Belarus")]
    Belarus = 481,

    /// <summary>
    ///     Ukraine.
    /// </summary>
    [Description("Ukraine")]
    Ukraine = 482,

    /// <summary>
    ///     Turkmenistan.
    /// </summary>
    [Description("Turkmenistan")]
    Turkmenistan = 483,

    /// <summary>
    ///     Moldova.
    /// </summary>
    [Description("Moldova")]
    Moldova = 484,

    /// <summary>
    ///     Armenia.
    /// </summary>
    [Description("Armenia")]
    Armenia = 485,

    /// <summary>
    ///     Georgia.
    /// </summary>
    [Description("Georgia")]
    Georgia = 486,

    /// <summary>
    ///     Kazakhstan.
    /// </summary>
    [Description("Kazakhstan")]
    Kazakhstan = 487,

    /// <summary>
    ///     Tajikistan.
    /// </summary>
    [Description("Tajikistan")]
    Tajikistan = 488,

    /// <summary>
    ///     Hong Kong.
    /// </summary>
    [Description("HongKong")]
    HongKong = 489,

    /// <summary>
    ///     Japan.
    /// </summary>
    [Description("Japan (original Japanese Article Number range)")]
    JapanOriginalJan = 490,

    /// <summary>
    ///     United Kingdom.
    /// </summary>
    [Description("United Kingdom")]
    UnitedKingdom = 500,

    /// <summary>
    ///     Greece.
    /// </summary>
    [Description("Greece")]
    Greece = 520,

    /// <summary>
    ///     Lebanon.
    /// </summary>
    [Description("Lebanon")]
    Lebanon = 528,

    /// <summary>
    ///     Cyprus.
    /// </summary>
    [Description("Cyprus")]
    Cyprus = 529,

    /// <summary>
    ///     Albania.
    /// </summary>
    [Description("Albania")]
    Albania = 530,

    /// <summary>
    ///     Macedonia.
    /// </summary>
    [Description("Macedonia")]
    NorthMacedonia = 531,

    /// <summary>
    ///     Malta.
    /// </summary>
    [Description("Malta")]
    Malta = 535,

    /// <summary>
    ///     Ireland.
    /// </summary>
    [Description("Ireland")]
    Ireland = 539,

    /// <summary>
    ///     Belgium And Luxembourg.
    /// </summary>
    [Description("Belgium And Luxembourg")]
    BelgiumAndLuxembourg = 540,

    /// <summary>
    ///     Portugal.
    /// </summary>
    [Description("Portugal")]
    Portugal = 560,

    /// <summary>
    ///     Iceland.
    /// </summary>
    [Description("Iceland")]
    Iceland = 569,

    /// <summary>
    ///     Denmark, Faroe Islands and Greenland.
    /// </summary>
    [Description("Denmark, Faroe Islands and Greenland")]
    DenmarkFaroeIslandsAndGreenland = 570,

    /// <summary>
    ///     Poland.
    /// </summary>
    [Description("Poland")]
    Poland = 590,

    /// <summary>
    ///     Romania.
    /// </summary>
    [Description("Romania")]
    Romania = 594,

    /// <summary>
    ///     Hungary.
    /// </summary>
    [Description("Hungary")]
    Hungary = 599,

    /// <summary>
    ///     South Africa.
    /// </summary>
    [Description("South Africa")]
    SouthAfrica = 600,

    /// <summary>
    ///     Ghana.
    /// </summary>
    [Description("Ghana")]
    Ghana = 603,

    /// <summary>
    ///     Senegal.
    /// </summary>
    [Description("Senegal")]
    Senegal = 604,

    /// <summary>
    ///     Bahrain.
    /// </summary>
    [Description("Bahrain")]
    Bahrain = 608,

    /// <summary>
    ///     Mauritius.
    /// </summary>
    [Description("Mauritius")]
    Mauritius = 609,

    /// <summary>
    ///     Morocco.
    /// </summary>
    [Description("Morocco")]
    Morocco = 611,

    /// <summary>
    ///     Algeria.
    /// </summary>
    [Description("Algeria")]
    Algeria = 613,

    /// <summary>
    ///     Nigeria.
    /// </summary>
    [Description("Nigeria")]
    Nigeria = 615,

    /// <summary>
    ///     Kenya.
    /// </summary>
    [Description("Kenya")]
    Kenya = 616,

    /// <summary>
    ///     Ivory Coast.
    /// </summary>
    [Description("Ivory Coast")]
    IvoryCoast = 618,

    /// <summary>
    ///     Tunisia.
    /// </summary>
    [Description("Tunisia")]
    Tunisia = 619,

    /// <summary>
    ///     Tanzania.
    /// </summary>
    [Description("Tanzania")]
    Tanzania = 620,

    /// <summary>
    ///     Syria.
    /// </summary>
    [Description("Syria")]
    Syria = 621,

    /// <summary>
    ///     Egypt.
    /// </summary>
    [Description("Egypt")]
    Egypt = 622,

    /// <summary>
    ///     Brunei.
    /// </summary>
    [Description("Brunei")]
    Brunei = 623,

    /// <summary>
    ///     Libya.
    /// </summary>
    [Description("Libya")]
    Libya = 624,

    /// <summary>
    ///     Jordan.
    /// </summary>
    [Description("Jordan")]
    Jordan = 625,

    /// <summary>
    ///     Iran.
    /// </summary>
    [Description("Iran")]
    Iran = 626,

    /// <summary>
    ///     Kuwait.
    /// </summary>
    [Description("Kuwait")]
    Kuwait = 627,

    /// <summary>
    ///     Saudi Arabia.
    /// </summary>
    [Description("Saudi Arabia")]
    SaudiArabia = 628,

    /// <summary>
    ///     United Arab Emirates.
    /// </summary>
    [Description("United Arab Emirates")]
    UnitedArabEmirates = 629,

    /// <summary>
    ///     Finland.
    /// </summary>
    [Description("Finland")]
    Finland = 640,

    /// <summary>
    ///     China.
    /// </summary>
    [Description("Peoples Republic of China")]
    PeoplesRepublicOfChina = 690,

    /// <summary>
    ///     Norway.
    /// </summary>
    [Description("Norway")]
    Norway = 700,

    /// <summary>
    ///     Israel.
    /// </summary>
    [Description("Israel")]
    Israel = 729,

    /// <summary>
    ///     Sweden.
    /// </summary>
    [Description("Sweden")]
    Sweden = 730,

    /// <summary>
    ///     Guatemala.
    /// </summary>
    [Description("Guatemala")]
    Guatemala = 740,

    /// <summary>
    ///     El Salvador.
    /// </summary>
    // ReSharper disable once IdentifierTypo
    [Description("El Salvador")]
    ElSalvador = 741,

    /// <summary>
    ///     Honduras.
    /// </summary>
    [Description("Kosovo")]
    Honduras = 742,

    /// <summary>
    ///     Nicaragua.
    /// </summary>
    [Description("Nicaragua")]
    Nicaragua = 743,

    /// <summary>
    ///     Costa Rica.
    /// </summary>
    // ReSharper disable once IdentifierTypo
    [Description("Costa Rica")]
    CostaRica = 744,

    /// <summary>
    ///     Panama.
    /// </summary>
    [Description("Panama")]
    Panama = 745,

    /// <summary>
    ///     Dominican Republic.
    /// </summary>
    [Description("Dominican Republic")]
    DominicanRepublic = 746,

    /// <summary>
    ///     Mexico.
    /// </summary>
    [Description("Mexico")]
    Mexico = 750,

    /// <summary>
    ///     Canada.
    /// </summary>
    [Description("Canada")]
    Canada = 754,

    /// <summary>
    ///     Venezuela.
    /// </summary>
    [Description("Venezuela")]
    Venezuela = 759,

    /// <summary>
    ///     Switzerland and Liechtenstein.
    /// </summary>
    [Description("Switzerland and Liechtenstein")]
    SwitzerlandAndLiechtenstein = 760,

    /// <summary>
    ///     Colombia.
    /// </summary>
    [Description("Colombia")]
    Colombia = 770,

    /// <summary>
    ///     Uruguay.
    /// </summary>
    [Description("Uruguay")]
    Uruguay = 773,

    /// <summary>
    ///     Peru.
    /// </summary>
    [Description("Peru")]
    Peru = 775,

    /// <summary>
    ///     Bolivia.
    /// </summary>
    [Description("Bolivia")]
    Bolivia = 777,

    /// <summary>
    ///     Argentina.
    /// </summary>
    [Description("Argentina")]
    Argentina = 778,

    /// <summary>
    ///     Chile.
    /// </summary>
    [Description("Chile")]
    Chile = 780,

    /// <summary>
    ///     Paraguay.
    /// </summary>
    [Description("Paraguay")]
    Paraguay = 784,

    /// <summary>
    ///     Ecuador.
    /// </summary>
    [Description("Ecuador")]
    Ecuador = 786,

    /// <summary>
    ///     Brazil.
    /// </summary>
    [Description("Brazil")]
    Brazil = 789,

    /// <summary>
    ///     Italy, San Marino and Vatican City.
    /// </summary>
    [Description("Italy, San Marino and Vatican City")]
    ItalySanMarinoAndVaticanCity = 800,

    /// <summary>
    ///     Spain and Andorra.
    /// </summary>
    [Description("Spain and Andorra")]
    SpainAndAndorra = 840,

    /// <summary>
    ///     Cuba.
    /// </summary>
    [Description("Cuba")]
    Cuba = 850,

    /// <summary>
    ///     Slovakia.
    /// </summary>
    [Description("Slovakia")]
    Slovakia = 858,

    /// <summary>
    ///     Czech Republic.
    /// </summary>
    [Description("Czech Republic")]
    CzechRepublic = 859,

    /// <summary>
    ///     Serbia.
    /// </summary>
    [Description("Serbia")]
    Serbia = 860,

    /// <summary>
    ///     Mongolia.
    /// </summary>
    [Description("Mongolia")]
    Mongolia = 865,

    /// <summary>
    ///     North Korea.
    /// </summary>
    [Description("North Korea")]
    NorthKorea = 867,

    /// <summary>
    ///     Turkey.
    /// </summary>
    [Description("Turkey")]
    Turkey = 868,

    /// <summary>
    ///     Netherlands.
    /// </summary>
    [Description("Netherlands")]
    Netherlands = 870,

    /// <summary>
    ///     South Korea.
    /// </summary>
    [Description("South Korea")]
    SouthKorea = 880,

    /// <summary>
    ///     Cambodia.
    /// </summary>
    [Description("Cambodia")]
    Cambodia = 884,

    /// <summary>
    ///     Thailand.
    /// </summary>
    [Description("Thailand")]
    Thailand = 885,

    /// <summary>
    ///     Singapore.
    /// </summary>
    [Description("Singapore")]
    Singapore = 888,

    /// <summary>
    ///     India.
    /// </summary>
    [Description("India")]
    India = 890,

    /// <summary>
    ///     Vietnam.
    /// </summary>
    [Description("Vietnam")]
    Vietnam = 893,

    /// <summary>
    ///     Bangladesh.
    /// </summary>
    [Description("Bangladesh")]
    Bangladesh = 894,

    /// <summary>
    ///     Pakistan.
    /// </summary>
    [Description("Pakistan")]
    Pakistan = 896,

    /// <summary>
    ///     Indonesia.
    /// </summary>
    [Description("Indonesia")]
    Indonesia = 899,

    /// <summary>
    ///     Austria.
    /// </summary>
    [Description("Austria")]
    Austria = 900,

    /// <summary>
    ///     Australia.
    /// </summary>
    [Description("Australia")]
    Australia = 930,

    /// <summary>
    ///     New Zealand.
    /// </summary>
    [Description("New Zealand")]
    NewZealand = 940,

    /// <summary>
    ///     Global Office - special application.
    /// </summary>
    [Description("Global Office - special application")]
    GlobalOffice = 950,

    /// <summary>
    ///     General Manager Numbers for the EPC General Identifier (GID) scheme as defined by the EPC Tag Data Standard.
    /// </summary>
    [Description(
        "General Manager Numbers for the EPC General Identifier (GID) scheme as defined by the EPC Tag Data Standard")]
    GeneralManagerNumber = 951,

    /// <summary>
    ///     Malaysia.
    /// </summary>
    [Description("Malaysia")]
    Malaysia = 955,

    /// <summary>
    ///     Macau.
    /// </summary>
    [Description("Macau")]
    Macau = 958,

    /// <summary>
    ///     GS1 UK Office: GTIN-8 allocation.
    /// </summary>
    [Description("GS1 UK Office: GTIN-8 allocation")]
    UnitedKingdomOfficeGtin8Allocation = 960,

    /// <summary>
    ///     GS1 Global Office: GTIN-8 allocation.
    /// </summary>
    [Description("GS1 Global Office: GTIN-8 allocation")]
    GlobalOfficeGtin8Allocation = 962,

    /// <summary>
    ///     Serial publications (ISSN).
    /// </summary>
    [Description("Serial publications (ISSN)")]
    SerialPublicationIssn = 977,

    /// <summary>
    ///     Bookland (ISBN)
    /// </summary>
    [SuppressMessage(
        "StyleCop.CSharp.DocumentationRules",
        "SA1650:ElementDocumentationMustBeSpelledCorrectly",
        Justification = "Reviewed. Suppression is OK here.")]
    [Description("Bookland (ISBN)")]
    BooklandIsbn = 978,

    /// <summary>
    ///     Bookland (ISBN) - sheet music (ISMN-13).
    /// </summary>
    [SuppressMessage(
        "StyleCop.CSharp.DocumentationRules",
        "SA1650:ElementDocumentationMustBeSpelledCorrectly",
        Justification = "Reviewed. Suppression is OK here.")]
    [Description("Bookland (ISBN) - sheet music (ISMN-13)")]
    BooklandIsbnIsmn = 979,

    /// <summary>
    ///     Refund Receipt.
    /// </summary>
    [Description("Refund Receipt")]
    RefundReceipt = 980,

    /// <summary>
    ///     GS1 coupon identification for common currency areas.
    /// </summary>
    [Description("GS1 coupon identification for common currency areas")]
    CouponIdentificationForCommonCurrencyArea = 981,

    /// <summary>
    ///     Coupon identification.
    /// </summary>
    [Description("Coupon identification")]
    CouponIdentification = 990
}