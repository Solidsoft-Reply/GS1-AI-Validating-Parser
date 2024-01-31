// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompanyPrefix.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018-2024 Solidsoft Reply Ltd.  All rights reserved.
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
public enum CountryCode {
    /// <summary>
    ///     Unknown country.
    /// </summary>
    [LocalisedDescription(nameof(Unknown))]
    Unknown = -1,

    /// <summary>
    ///     UPC-A compatible (GTIN 8).
    /// </summary>
    [LocalisedDescription(nameof(UpcACompatibleGtin8))]
    UpcACompatibleGtin8 = 0,

    /// <summary>
    ///     UPC-A compatible - United States and Canada.
    /// </summary>
    [LocalisedDescription(nameof(UpcACompatibleUnitedStatesAndCanada))]
    UpcACompatibleUnitedStatesAndCanada = 19,

    /// <summary>
    ///     UPC-A compatible - Used to issue restricted circulation numbers within a geographic region.
    /// </summary>
    [LocalisedDescription(nameof(UpcACompatibleRestrictedCirculation))]
    UpcACompatibleRestrictedCirculation = 20,

    /// <summary>
    ///     UPC-A compatible -  United States drugs (National Drug Code).
    /// </summary>
    [LocalisedDescription(nameof(UpcACompatibleUnitedStatesDrugs))]
    UpcACompatibleUnitedStatesDrugs = 30,

    /// <summary>
    ///     UPC-A compatible - GS1 US reserved for future use .
    /// </summary>
    [LocalisedDescription(nameof(UpcACompatibleUnitedStatesReserved))]
    UpcACompatibleUnitedStatesReserved = 50,

    /// <summary>
    ///     United States of America
    /// </summary>
    [LocalisedDescription(nameof(UnitedStates))]
    UnitedStates = 100,

    /// <summary>
    ///     Used to issue GS1 restricted circulation number within a geographic region
    /// </summary>
    [LocalisedDescription(nameof(RestrictedCirculation))]
    RestrictedCirculation = 200,

    /// <summary>
    ///     France and Monaco.
    /// </summary>
    [LocalisedDescription(nameof(FranceAndMonaco))]
    FranceAndMonaco = 300,

    /// <summary>
    ///     Bulgaria.
    /// </summary>
    [LocalisedDescription(nameof(Bulgaria))]
    Bulgaria = 380,

    /// <summary>
    ///     Slovenia.
    /// </summary>
    [LocalisedDescription(nameof(Slovenia))]
    Slovenia = 383,

    /// <summary>
    ///     Croatia.
    /// </summary>
    [LocalisedDescription(nameof(Croatia))]
    Croatia = 385,

    /// <summary>
    ///     Bosnia and Herzegovina
    /// </summary>
    [LocalisedDescription(nameof(BosniaAndHerzegovina))]
    BosniaAndHerzegovina = 387,

    /// <summary>
    ///     Montenegro.
    /// </summary>
    [LocalisedDescription(nameof(Montenegro))]
    Montenegro = 389,

    /// <summary>
    ///     Kosovo.
    /// </summary>
    [LocalisedDescription(nameof(Kosovo))]
    Kosovo = 390,

    /// <summary>
    ///     Germany.
    /// </summary>
    [LocalisedDescription(nameof(Germany))]
    Germany = 400,

    /// <summary>
    ///     Japan.
    /// </summary>
    [LocalisedDescription(nameof(Japan))]
    Japan = 450,

    /// <summary>
    ///     Russia.
    /// </summary>
    [LocalisedDescription(nameof(Russia))]
    Russia = 460,

    /// <summary>
    ///     Kyrgyzstan.
    /// </summary>
    [LocalisedDescription(nameof(Kyrgyzstan))]
    Kyrgyzstan = 470,

    /// <summary>
    ///     Taiwan.
    /// </summary>
    [LocalisedDescription(nameof(Taiwan))]
    Taiwan = 471,

    /// <summary>
    ///     Estonia.
    /// </summary>
    [LocalisedDescription(nameof(Estonia))]
    Estonia = 474,

    /// <summary>
    ///     Latvia.
    /// </summary>
    [LocalisedDescription(nameof(Latvia))]
    Latvia = 475,

    /// <summary>
    ///     Azerbaijan.
    /// </summary>
    [LocalisedDescription(nameof(Azerbaijan))]
    Azerbaijan = 476,

    /// <summary>
    ///     Lithuania.
    /// </summary>
    [LocalisedDescription(nameof(Lithuania))]
    Lithuania = 477,

    /// <summary>
    ///     Uzbekistan.
    /// </summary>
    [LocalisedDescription(nameof(Uzbekistan))]
    Uzbekistan = 478,

    /// <summary>
    ///     Sri Lanka.
    /// </summary>
    [LocalisedDescription(nameof(SriLanka))]
    SriLanka = 479,

    /// <summary>
    ///     Philippines.
    /// </summary>
    [LocalisedDescription(nameof(Philippines))]
    Philippines = 480,

    /// <summary>
    ///     Belarus.
    /// </summary>
    [LocalisedDescription(nameof(Belarus))]
    Belarus = 481,

    /// <summary>
    ///     Ukraine.
    /// </summary>
    [LocalisedDescription(nameof(Ukraine))]
    Ukraine = 482,

    /// <summary>
    ///     Turkmenistan.
    /// </summary>
    [LocalisedDescription(nameof(Turkmenistan))]
    Turkmenistan = 483,

    /// <summary>
    ///     Moldova.
    /// </summary>
    [LocalisedDescription(nameof(Moldova))]
    Moldova = 484,

    /// <summary>
    ///     Armenia.
    /// </summary>
    [LocalisedDescription(nameof(Armenia))]
    Armenia = 485,

    /// <summary>
    ///     Georgia.
    /// </summary>
    [LocalisedDescription(nameof(Georgia))]
    Georgia = 486,

    /// <summary>
    ///     Kazakhstan.
    /// </summary>
    [LocalisedDescription(nameof(Kazakhstan))]
    Kazakhstan = 487,

    /// <summary>
    ///     Tajikistan.
    /// </summary>
    [LocalisedDescription(nameof(Tajikistan))]
    Tajikistan = 488,

    /// <summary>
    ///     Hong Kong.
    /// </summary>
    [LocalisedDescription(nameof(HongKong))]
    HongKong = 489,

    /// <summary>
    ///     Japan.
    /// </summary>
    [LocalisedDescription(nameof(JapanOriginalJan))]
    JapanOriginalJan = 490,

    /// <summary>
    ///     United Kingdom.
    /// </summary>
    [LocalisedDescription(nameof(UnitedKingdom))]
    UnitedKingdom = 500,

    /// <summary>
    ///     Greece.
    /// </summary>
    [LocalisedDescription(nameof(Greece))]
    Greece = 520,

    /// <summary>
    ///     Lebanon.
    /// </summary>
    [LocalisedDescription(nameof(Lebanon))]
    Lebanon = 528,

    /// <summary>
    ///     Cyprus.
    /// </summary>
    [LocalisedDescription(nameof(Cyprus))]
    Cyprus = 529,

    /// <summary>
    ///     Albania.
    /// </summary>
    [LocalisedDescription(nameof(Albania))]
    Albania = 530,

    /// <summary>
    ///     Macedonia.
    /// </summary>
    [LocalisedDescription(nameof(NorthMacedonia))]
    NorthMacedonia = 531,

    /// <summary>
    ///     Malta.
    /// </summary>
    [LocalisedDescription(nameof(Malta))]
    Malta = 535,

    /// <summary>
    ///     Ireland.
    /// </summary>
    [LocalisedDescription(nameof(Ireland))]
    Ireland = 539,

    /// <summary>
    ///     Belgium And Luxembourg.
    /// </summary>
    [LocalisedDescription(nameof(BelgiumAndLuxembourg))]
    BelgiumAndLuxembourg = 540,

    /// <summary>
    ///     Portugal.
    /// </summary>
    [LocalisedDescription(nameof(Portugal))]
    Portugal = 560,

    /// <summary>
    ///     Iceland.
    /// </summary>
    [LocalisedDescription(nameof(Iceland))]
    Iceland = 569,

    /// <summary>
    ///     Denmark, Faroe Islands and Greenland.
    /// </summary>
    [LocalisedDescription(nameof(DenmarkFaroeIslandsAndGreenland))]
    DenmarkFaroeIslandsAndGreenland = 570,

    /// <summary>
    ///     Poland.
    /// </summary>
    [LocalisedDescription(nameof(Poland))]
    Poland = 590,

    /// <summary>
    ///     Romania.
    /// </summary>
    [LocalisedDescription(nameof(Romania))]
    Romania = 594,

    /// <summary>
    ///     Hungary.
    /// </summary>
    [LocalisedDescription(nameof(Hungary))]
    Hungary = 599,

    /// <summary>
    ///     South Africa.
    /// </summary>
    [LocalisedDescription(nameof(SouthAfrica))]
    SouthAfrica = 600,

    /// <summary>
    ///     Ghana.
    /// </summary>
    [LocalisedDescription(nameof(Ghana))]
    Ghana = 603,

    /// <summary>
    ///     Senegal.
    /// </summary>
    [LocalisedDescription(nameof(Senegal))]
    Senegal = 604,

    /// <summary>
    ///     Bahrain.
    /// </summary>
    [LocalisedDescription(nameof(Bahrain))]
    Bahrain = 608,

    /// <summary>
    ///     Mauritius.
    /// </summary>
    [LocalisedDescription(nameof(Mauritius))]
    Mauritius = 609,

    /// <summary>
    ///     Morocco.
    /// </summary>
    [LocalisedDescription(nameof(Morocco))]
    Morocco = 611,

    /// <summary>
    ///     Algeria.
    /// </summary>
    [LocalisedDescription(nameof(Algeria))]
    Algeria = 613,

    /// <summary>
    ///     Nigeria.
    /// </summary>
    [LocalisedDescription(nameof(Nigeria))]
    Nigeria = 615,

    /// <summary>
    ///     Kenya.
    /// </summary>
    [LocalisedDescription(nameof(Kenya))]
    Kenya = 616,

    /// <summary>
    ///     Ivory Coast.
    /// </summary>
    [LocalisedDescription(nameof(IvoryCoast))]
    IvoryCoast = 618,

    /// <summary>
    ///     Tunisia.
    /// </summary>
    [LocalisedDescription(nameof(Tunisia))]
    Tunisia = 619,

    /// <summary>
    ///     Tanzania.
    /// </summary>
    [LocalisedDescription(nameof(Tanzania))]
    Tanzania = 620,

    /// <summary>
    ///     Syria.
    /// </summary>
    [LocalisedDescription(nameof(Syria))]
    Syria = 621,

    /// <summary>
    ///     Egypt.
    /// </summary>
    [LocalisedDescription(nameof(Egypt))]
    Egypt = 622,

    /// <summary>
    ///     Brunei.
    /// </summary>
    [LocalisedDescription(nameof(Brunei))]
    Brunei = 623,

    /// <summary>
    ///     Libya.
    /// </summary>
    [LocalisedDescription(nameof(Libya))]
    Libya = 624,

    /// <summary>
    ///     Jordan.
    /// </summary>
    [LocalisedDescription(nameof(Jordan))]
    Jordan = 625,

    /// <summary>
    ///     Iran.
    /// </summary>
    [LocalisedDescription(nameof(Iran))]
    Iran = 626,

    /// <summary>
    ///     Kuwait.
    /// </summary>
    [LocalisedDescription(nameof(Kuwait))]
    Kuwait = 627,

    /// <summary>
    ///     Saudi Arabia.
    /// </summary>
    [LocalisedDescription(nameof(SaudiArabia))]
    SaudiArabia = 628,

    /// <summary>
    ///     United Arab Emirates.
    /// </summary>
    [LocalisedDescription(nameof(UnitedArabEmirates))]
    UnitedArabEmirates = 629,

    /// <summary>
    ///     Finland.
    /// </summary>
    [LocalisedDescription(nameof(Finland))]
    Finland = 640,

    /// <summary>
    ///     China.
    /// </summary>
    [LocalisedDescription(nameof(PeoplesRepublicOfChina))]
    PeoplesRepublicOfChina = 690,

    /// <summary>
    ///     Norway.
    /// </summary>
    [LocalisedDescription(nameof(Norway))]
    Norway = 700,

    /// <summary>
    ///     Israel.
    /// </summary>
    [LocalisedDescription(nameof(Israel))]
    Israel = 729,

    /// <summary>
    ///     Sweden.
    /// </summary>
    [LocalisedDescription(nameof(Sweden))]
    Sweden = 730,

    /// <summary>
    ///     Guatemala.
    /// </summary>
    [LocalisedDescription(nameof(Guatemala))]
    Guatemala = 740,

    /// <summary>
    ///     El Salvador.
    /// </summary>
    // ReSharper disable once IdentifierTypo
    [LocalisedDescription(nameof(ElSalvador))]
    ElSalvador = 741,

    /// <summary>
    ///     Honduras.
    /// </summary>
    [LocalisedDescription(nameof(Honduras))]
    Honduras = 742,

    /// <summary>
    ///     Nicaragua.
    /// </summary>
    [LocalisedDescription(nameof(Nicaragua))]
    Nicaragua = 743,

    /// <summary>
    ///     Costa Rica.
    /// </summary>
    // ReSharper disable once IdentifierTypo
    [LocalisedDescription(nameof(CostaRica))]
    CostaRica = 744,

    /// <summary>
    ///     Panama.
    /// </summary>
    [LocalisedDescription(nameof(Panama))]
    Panama = 745,

    /// <summary>
    ///     Dominican Republic.
    /// </summary>
    [LocalisedDescription(nameof(DominicanRepublic))]
    DominicanRepublic = 746,

    /// <summary>
    ///     Mexico.
    /// </summary>
    [LocalisedDescription(nameof(Mexico))]
    Mexico = 750,

    /// <summary>
    ///     Canada.
    /// </summary>
    [LocalisedDescription(nameof(Canada))]
    Canada = 754,

    /// <summary>
    ///     Venezuela.
    /// </summary>
    [LocalisedDescription(nameof(Venezuela))]
    Venezuela = 759,

    /// <summary>
    ///     Switzerland and Liechtenstein.
    /// </summary>
    [LocalisedDescription(nameof(SwitzerlandAndLiechtenstein))]
    SwitzerlandAndLiechtenstein = 760,

    /// <summary>
    ///     Colombia.
    /// </summary>
    [LocalisedDescription(nameof(Colombia))]
    Colombia = 770,

    /// <summary>
    ///     Uruguay.
    /// </summary>
    [LocalisedDescription(nameof(Uruguay))]
    Uruguay = 773,

    /// <summary>
    ///     Peru.
    /// </summary>
    [LocalisedDescription(nameof(Peru))]
    Peru = 775,

    /// <summary>
    ///     Bolivia.
    /// </summary>
    [LocalisedDescription(nameof(Bolivia))]
    Bolivia = 777,

    /// <summary>
    ///     Argentina.
    /// </summary>
    [LocalisedDescription(nameof(Argentina))]
    Argentina = 778,

    /// <summary>
    ///     Chile.
    /// </summary>
    [LocalisedDescription(nameof(Chile))]
    Chile = 780,

    /// <summary>
    ///     Paraguay.
    /// </summary>
    [LocalisedDescription(nameof(Paraguay))]
    Paraguay = 784,

    /// <summary>
    ///     Ecuador.
    /// </summary>
    [LocalisedDescription(nameof(Ecuador))]
    Ecuador = 786,

    /// <summary>
    ///     Brazil.
    /// </summary>
    [LocalisedDescription(nameof(Brazil))]
    Brazil = 789,

    /// <summary>
    ///     Italy, San Marino and Vatican City.
    /// </summary>
    [LocalisedDescription(nameof(ItalySanMarinoAndVaticanCity))]
    ItalySanMarinoAndVaticanCity = 800,

    /// <summary>
    ///     Spain and Andorra.
    /// </summary>
    [LocalisedDescription(nameof(SpainAndAndorra))]
    SpainAndAndorra = 840,

    /// <summary>
    ///     Cuba.
    /// </summary>
    [LocalisedDescription(nameof(Cuba))]
    Cuba = 850,

    /// <summary>
    ///     Slovakia.
    /// </summary>
    [LocalisedDescription(nameof(Slovakia))]
    Slovakia = 858,

    /// <summary>
    ///     Czech Republic.
    /// </summary>
    [LocalisedDescription(nameof(CzechRepublic))]
    CzechRepublic = 859,

    /// <summary>
    ///     Serbia.
    /// </summary>
    [LocalisedDescription(nameof(Serbia))]
    Serbia = 860,

    /// <summary>
    ///     Mongolia.
    /// </summary>
    [LocalisedDescription(nameof(Mongolia))]
    Mongolia = 865,

    /// <summary>
    ///     North Korea.
    /// </summary>
    [LocalisedDescription(nameof(NorthKorea))]
    NorthKorea = 867,

    /// <summary>
    ///     Turkey.
    /// </summary>
    [LocalisedDescription(nameof(Turkey))]
    Turkey = 868,

    /// <summary>
    ///     Netherlands.
    /// </summary>
    [LocalisedDescription(nameof(Netherlands))]
    Netherlands = 870,

    /// <summary>
    ///     South Korea.
    /// </summary>
    [LocalisedDescription(nameof(SouthKorea))]
    SouthKorea = 880,

    /// <summary>
    ///     Cambodia.
    /// </summary>
    [LocalisedDescription(nameof(Cambodia))]
    Cambodia = 884,

    /// <summary>
    ///     Thailand.
    /// </summary>
    [LocalisedDescription(nameof(Thailand))]
    Thailand = 885,

    /// <summary>
    ///     Singapore.
    /// </summary>
    [LocalisedDescription(nameof(Singapore))]
    Singapore = 888,

    /// <summary>
    ///     India.
    /// </summary>
    [LocalisedDescription(nameof(India))]
    India = 890,

    /// <summary>
    ///     Vietnam.
    /// </summary>
    [LocalisedDescription(nameof(Vietnam))]
    Vietnam = 893,

    /// <summary>
    ///     Bangladesh.
    /// </summary>
    [LocalisedDescription(nameof(Bangladesh))]
    Bangladesh = 894,

    /// <summary>
    ///     Pakistan.
    /// </summary>
    [LocalisedDescription(nameof(Pakistan))]
    Pakistan = 896,

    /// <summary>
    ///     Indonesia.
    /// </summary>
    [LocalisedDescription(nameof(Indonesia))]
    Indonesia = 899,

    /// <summary>
    ///     Austria.
    /// </summary>
    [LocalisedDescription(nameof(Austria))]
    Austria = 900,

    /// <summary>
    ///     Australia.
    /// </summary>
    [LocalisedDescription(nameof(Australia))]
    Australia = 930,

    /// <summary>
    ///     New Zealand.
    /// </summary>
    [LocalisedDescription(nameof(NewZealand))]
    NewZealand = 940,

    /// <summary>
    ///     Global Office - special application.
    /// </summary>
    [LocalisedDescription(nameof(GlobalOffice))]
    GlobalOffice = 950,

    /// <summary>
    ///     General Manager Numbers for the EPC General Identifier (GID) scheme as defined by the EPC Tag Data Standard.
    /// </summary>
    [LocalisedDescription(nameof(GeneralManagerNumber))]
    GeneralManagerNumber = 951,

    /// <summary>
    ///     Malaysia.
    /// </summary>
    [LocalisedDescription(nameof(Malaysia))]
    Malaysia = 955,

    /// <summary>
    ///     Macau.
    /// </summary>
    [LocalisedDescription(nameof(Macau))]
    Macau = 958,

    /// <summary>
    ///     GS1 UK Office: GTIN-8 allocation.
    /// </summary>
    [LocalisedDescription(nameof(UnitedKingdomOfficeGtin8Allocation))]
    UnitedKingdomOfficeGtin8Allocation = 960,

    /// <summary>
    ///     GS1 Global Office: GTIN-8 allocation.
    /// </summary>
    [LocalisedDescription(nameof(GlobalOfficeGtin8Allocation))]
    GlobalOfficeGtin8Allocation = 962,

    /// <summary>
    ///     Serial publications (ISSN).
    /// </summary>
    [LocalisedDescription(nameof(SerialPublicationIssn))]
    SerialPublicationIssn = 977,

    /// <summary>
    ///     Bookland (ISBN)
    /// </summary>
    [SuppressMessage(
        "StyleCop.CSharp.DocumentationRules",
        "SA1650:ElementDocumentationMustBeSpelledCorrectly",
        Justification = "Reviewed. Suppression is OK here.")]
    [LocalisedDescription(nameof(BooklandIsbn))]
    BooklandIsbn = 978,

    /// <summary>
    ///     Bookland (ISBN) - sheet music (ISMN-13).
    /// </summary>
    [SuppressMessage(
        "StyleCop.CSharp.DocumentationRules",
        "SA1650:ElementDocumentationMustBeSpelledCorrectly",
        Justification = "Reviewed. Suppression is OK here.")]
    [LocalisedDescription(nameof(BooklandIsbnIsmn))]
    BooklandIsbnIsmn = 979,

    /// <summary>
    ///     Refund Receipt.
    /// </summary>
    [LocalisedDescription(nameof(RefundReceipt))]
    RefundReceipt = 980,

    /// <summary>
    ///     GS1 coupon identification for common currency areas.
    /// </summary>
    [LocalisedDescription(nameof(CouponIdentificationForCommonCurrencyArea))]
    CouponIdentificationForCommonCurrencyArea = 981,

    /// <summary>
    ///     Coupon identification.
    /// </summary>
    [LocalisedDescription(nameof(CouponIdentification))]
    CouponIdentification = 990
}