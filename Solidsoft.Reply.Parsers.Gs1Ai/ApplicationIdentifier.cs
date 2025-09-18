// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationIdentifier.cs" company="Solidsoft Reply Ltd">
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
// GS1 Identification identifiers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable UnusedMember.Global
// ReSharper disable CommentTypo

// ReSharper disable GrammarMistakeInComment
namespace Solidsoft.Reply.Parsers.Gs1Ai;

/// <summary>
///     GS1 Identification identifiers.
/// </summary>
public enum ApplicationIdentifier {
    /// <summary>
    ///     Unrecognised entity.
    /// </summary>
    Unrecognised = -1,

    /// <summary>
    ///     Identification of a logistic unit (SSCC).
    /// </summary>
    /// <remarks>Format: N2+N18</remarks>
    SerialShippingContainerCode = 0,

    /// <summary>
    ///     Global Trade Item Number (GTIN).
    /// </summary>
    /// <remarks>Format: N2+N14</remarks>
    GlobalTradeItemNumber = 1,

    /// <summary>
    ///     GTIN of contained trade items (CONTENT).
    /// </summary>
    /// <remarks>Format: N2+N14</remarks>
    GtinOfContainedTradeItems = 2,

    /// <summary>
    ///     Identification of a Made-to-Order trade item (MTO GTIN).
    /// </summary>
    /// <remarks>Format: N2+N14</remarks>
    MadeToOrderTradeItemGtin = 3,

    /// <summary>
    ///     Batch or lot number (BATCH/LOT).
    /// </summary>
    /// <remarks>Format: N2+X..20 (FNC1)</remarks>
    BatchOrLotNumber = 10,

    /// <summary>
    ///     Production date (YYMMDD) (PROD DATE).
    /// </summary>
    /// <remarks>Format: N2+N6</remarks>
    ProductionDate = 11,

    /// <summary>
    ///     Due date (YYMMDD) (DUE DATE).
    /// </summary>
    /// <remarks>Format: N2+N6</remarks>
    DueDate = 12,

    /// <summary>
    ///     Packaging date (YYMMDD) (PACK DATE).
    /// </summary>
    /// <remarks>Format: N2+N6</remarks>
    PackagingDate = 13,

    /// <summary>
    ///     Best before date (YYMMDD) (BEST BEFORE or BEST BY).
    /// </summary>
    /// <remarks>Format: N2+N6</remarks>
    BestBeforeDate = 15,

    /// <summary>
    ///     Sell by date (YYMMDD) (SELL BY).
    /// </summary>
    /// <remarks>Format: N2+N6</remarks>
    SellByDate = 16,

    /// <summary>
    ///     Expiration date (YYMMDD) (USE BY OR EXPIRY).
    /// </summary>
    /// <remarks>Format: N2+N6</remarks>
    ExpirationDate = 17,

    /// <summary>
    ///     Internal product variant (VARIANT).
    /// </summary>
    /// <remarks>Format: N2+N2</remarks>
    InternalProductVariant = 20,

    /// <summary>
    ///     Serial number (SERIAL).
    /// </summary>
    /// <remarks>Format: N2+X..20 (FNC1)</remarks>
    SerialNumber = 21,

    /// <summary>
    ///     Consumer product variant (CPV).
    /// </summary>
    /// <remarks>Format: N2+X..20 (FNC1)</remarks>
    ConsumerProductVariant = 22,

    /// <summary>
    ///     Third Party Controlled, Serialized Extension of GTIN (TPX).
    /// </summary>
    /// <remarks>Format: N3+X..28 (FNC1)</remarks>
    ThirdPartyGtinExtension = 235,

    /// <summary>
    ///     Additional product identification assigned by the manufacturer (ADDITIONAL ID).
    /// </summary>
    /// <remarks>Format: N3+X..30 (FNC1)</remarks>
    AdditionalItemIdentification = 240,

    /// <summary>
    ///     Customer part number (CUST. PART NO.).
    /// </summary>
    /// <remarks>Format: N3+X..30 (FNC1)</remarks>
    CustomerPartNumber = 241,

    /// <summary>
    ///     Made-to-Order variation number (MTO VARIANT).
    /// </summary>
    /// <remarks>Format: N3+N..6 (FNC1)</remarks>
    MadeToOrderVariationNumber = 242,

    /// <summary>
    ///     Packaging component number (PCN).
    /// </summary>
    /// <remarks>Format: N3+X..20 (FNC1)</remarks>
    PackagingComponentNumber = 243,

    /// <summary>
    ///     Secondary serial number (SECONDARY SERIAL).
    /// </summary>
    /// <remarks>Format: N3+X..30 (FNC1)</remarks>
    SecondarySerialNumber = 250,

    /// <summary>
    ///     Reference to source entity (REF. TO SOURCE).
    /// </summary>
    /// <remarks>Format: N3+X..30 (FNC1)</remarks>
    ReferenceToSourceEntity = 251,

    /// <summary>
    ///     Global Document Type Identifier (GDTI).
    /// </summary>
    /// <remarks>Format: N3+N13+X..17 (FNC1)</remarks>
    GlobalDocumentTypeIdentifier = 253,

    /// <summary>
    ///     GLN extension component (GLN EXTENSION COMPONENT).
    /// </summary>
    /// <remarks>Format: N3+X..20 (FNC1)</remarks>
    GlnExtensionComponent = 254,

    /// <summary>
    ///     Global Coupon Number (GCN).
    /// </summary>
    /// <remarks>Format:  N3+N13+N..12 (FNC1)</remarks>
    GlobalCouponNumber = 255,

    /// <summary>
    ///     Variable count of items (variable measure trade item) (VAR. COUNT).
    /// </summary>
    /// <remarks>Format: N2+N..8 (FNC1)</remarks>
    VariableCountOfItemsVariableMeasureTradeItem = 30,

    /// <summary>
    ///     Net weight, kilograms (variable measure trade item) (NET WEIGHT (kg)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    NetWeightKilogramsVariableMeasureTradeItem = 310,

    /// <summary>
    ///     Length or first dimension, metres (variable measure trade item) (LENGTH (m)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LengthOrFirstDimensionMetresVariableMeasureTradeItem = 311,

    /// <summary>
    ///     Width, diameter, or second dimension, metres (variable measure trade item) (WIDTH (m)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    WidthDiameterOrSecondDimensionMetresVariableMeasureTradeItem = 312,

    /// <summary>
    ///     Depth, thickness, height, or third dimension, metres (variable measure trade item) (HEIGHT (m)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    DepthThicknessHeightOrThirdDimensionMetresVariableMeasureTradeItem = 313,

    /// <summary>
    ///     Area, square metres (variable measure trade item) (AREA (m²)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    AreaSquareMetresVariableMeasureTradeItem = 314,

    /// <summary>
    ///     Net volume, litres (variable measure trade item) (NET VOLUME (l)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    NetVolumeLitresVariableMeasureTradeItem = 315,

    /// <summary>
    ///     Net volume, cubic metres (variable measure trade item) (NET VOLUME (m³)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    NetVolumeCubicMetresVariableMeasureTradeItem = 316,

    /// <summary>
    ///     Net weight, pounds (variable measure trade item) (NET WEIGHT (lb)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    NetWeightPoundsVariableMeasureTradeItem = 320,

    /// <summary>
    ///     Length or first dimension, inches (variable measure trade item) (LENGTH (i)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LengthOrFirstDimensionInchesVariableMeasureTradeItem = 321,

    /// <summary>
    ///     Length or first dimension, feet (variable measure trade item) (LENGTH (f)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LengthOrFirstDimensionFeetVariableMeasureTradeItem = 322,

    /// <summary>
    ///     Length or first dimension, yards (variable measure trade item) (LENGTH (y)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LengthOrFirstDimensionYardsVariableMeasureTradeItem = 323,

    /// <summary>
    ///     Width, diameter, or second dimension, inches (variable measure trade item) (WIDTH (i)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    WidthDiameterOrSecondDimensionInchesVariableMeasureTradeItem = 324,

    /// <summary>
    ///     Width, diameter, or second dimension, feet (variable measure trade item) (WIDTH (f)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    WidthDiameterOrSecondDimensionFeetVariableMeasureTradeItem = 325,

    /// <summary>
    ///     Width, diameter, or second dimension, yards (variable measure trade item) (WIDTH (y)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    WidthDiameterOrSecondDimensionYardsVariableMeasureTradeItem = 326,

    /// <summary>
    ///     Depth, thickness, height, or third dimension, inches (variable measure trade item) (HEIGHT (i)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    DepthThicknessHeightOrThirdDimensionInchesVariableMeasureTradeItem = 327,

    /// <summary>
    ///     Depth, thickness, height, or third dimension, feet (variable measure trade item) (HEIGHT (f)).
    /// </summary>
    /// <remarks>`Format: N4+N6</remarks>
    DepthThicknessHeightOrThirdDimensionFeetVariableMeasureTradeItem = 328,

    /// <summary>
    ///     Depth, thickness, height, or third dimension, yards (variable measure trade item) (HEIGHT (y)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    DepthThicknessHeightOrThirdDimensionYardsVariableMeasureTradeItem = 329,

    /// <summary>
    ///     Logistic weight, kilograms (GROSS WEIGHT (kg)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LogisticWeightKilograms = 330,

    /// <summary>
    ///     Length or first dimension, metres (LENGTH (m), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LengthOrFirstDimensionMetres = 331,

    /// <summary>
    ///     Width, diameter, or second dimension, metres (WIDTH (m), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    WidthDiameterOrSecondDimensionMetres = 332,

    /// <summary>
    ///     Depth, thickness, height, or third dimension, metres (HEIGHT (m), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    DepthThicknessHeightOrThirdDimensionMetres = 333,

    /// <summary>
    ///     Area, square metres (AREA (m²), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    AreaSquareMetres = 334,

    /// <summary>
    ///     Logistic volume, litres (VOLUME (l), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LogisticVolumeLitres = 335,

    /// <summary>
    ///     Logistic volume, cubic metres (VOLUME (m³), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LogisticVolumeCubicMetres = 336,

    /// <summary>
    ///     Kilograms per square metre (KG PER m²).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    KilogramPerSquareMetre = 337,

    /// <summary>
    ///     Logistic weight, pounds (GROSS WEIGHT (lb)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LogisticWeightPounds = 340,

    /// <summary>
    ///     Length or first dimension, inches (LENGTH (i), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LengthOrFirstDimensionInches = 341,

    /// <summary>
    ///     Length or first dimension, feet (LENGTH (f), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LengthOrFirstDimensionFeet = 342,

    /// <summary>
    ///     Length or first dimension, yards (LENGTH (y), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LengthOrFirstDimensionYards = 343,

    /// <summary>
    ///     Width, diameter, or second dimension, inches (WIDTH (i), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    WidthDiameterOrSecondDimensionInches = 344,

    /// <summary>
    ///     Width, diameter, or second dimension, feet (WIDTH (f), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    WidthDiameterOrSecondDimensionFeet = 345,

    /// <summary>
    ///     Width, diameter, or second dimension, yard (WIDTH (y), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    WidthDiameterOrSecondDimensionYard = 346,

    /// <summary>
    ///     Depth, thickness, height, or third dimension, inches (HEIGHT (i), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    DepthThicknessHeightOrThirdDimensionInches = 347,

    /// <summary>
    ///     Depth, thickness, height, or third dimension, feet (HEIGHT (f), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    DepthThicknessHeightOrThirdDimensionFeet = 348,

    /// <summary>
    ///     Depth, thickness, height, or third dimension, yards (HEIGHT (y), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    DepthThicknessHeightOrThirdDimensionYards = 349,

    /// <summary>
    ///     Area, square inches (variable measure trade item) (AREA (i²)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    AreaSquareInchesVariableMeasureTradeItem = 350,

    /// <summary>
    ///     Area, square feet (variable measure trade item) (AREA (f²)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    AreaSquareFeetVariableMeasureTradeItem = 351,

    /// <summary>
    ///     Area, square yards (variable measure trade item) (AREA (y²)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    AreaSquareYardsVariableMeasureTradeItem = 352,

    /// <summary>
    ///     Area, square inches (AREA (i²), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    AreaSquareInches = 353,

    /// <summary>
    ///     Area, square feet (AREA (f²), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    AreaSquareFeet = 354,

    /// <summary>
    ///     Area, square yards (AREA (y²), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    AreaSquareYards = 355,

    /// <summary>
    ///     Net weight, troy ounces (variable measure trade item) (NET WEIGHT (t)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    NetWeightTroyOuncesVariableMeasureTradeItem = 356,

    /// <summary>
    ///     Net weight (or volume), ounces (variable measure trade item) (NET VOLUME (oz)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    NetWeightOrVolumeOuncesVariableMeasureTradeItem = 357,

    /// <summary>
    ///     Net volume, quarts (variable measure trade item) (NET VOLUME (q)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    NetVolumeQuartsVariableMeasureTradeItem = 360,

    /// <summary>
    ///     Net volume, gallons U.S. (variable measure trade item) (NET VOLUME (g)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    NetVolumeGallonsUsVariableMeasureTradeItem = 361,

    /// <summary>
    ///     Logistic volume, quarts (VOLUME (q), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LogisticsVolumeQuarts = 362,

    /// <summary>
    ///     Logistic volume, gallons U.S. (VOLUME (g), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LogisticsVolumeGallonsUs = 363,

    /// <summary>
    ///     Net volume, cubic inches (variable measure trade item) (VOLUME (i³)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    NetVolumeCubicInchesVariableMeasureTradeItem = 364,

    /// <summary>
    ///     Net volume, cubic feet (variable measure trade item) (VOLUME (f³)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    NetVolumeCubicFeetVariableMeasureTradeItem = 365,

    /// <summary>
    ///     Net volume, cubic yards (variable measure trade item) (VOLUME (y³)).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    NetVolumeCubicYardsVariableMeasureTradeItem = 366,

    /// <summary>
    ///     Logistic volume, cubic inches (VOLUME (i³), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LogisticsVolumeCubicInches = 367,

    /// <summary>
    ///     Logistic volume, cubic feet (VOLUME (f³), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LogisticsVolumeCubicFeet = 368,

    /// <summary>
    ///     Logistic volume, cubic yards (VOLUME (y³), log).
    /// </summary>
    /// <remarks>Format: N4+N6</remarks>
    LogisticsVolumeCubicYards = 369,

    /// <summary>
    ///     Count of trade items (COUNT).
    /// </summary>
    /// <remarks>Format: N2+N..8 (FNC1)</remarks>
    CountOfTradeItems = 37,

    /// <summary>
    ///     Applicable amount payable or Coupon value, local currency (AMOUNT).
    /// </summary>
    /// <remarks>Format: N4+N..15 (FNC1)</remarks>
    ApplicableAmountPayableOrCouponValueLocalCurrency = 390,

    /// <summary>
    ///     Applicable amount payable with ISO currency code (AMOUNT).
    /// </summary>
    /// <remarks>Format: N4+N3+N..15 (FNC1)</remarks>
    ApplicableAmountPayableWithIsoCurrencyCode = 391,

    /// <summary>
    ///     Applicable amount payable, single monetary area (variable measure trade item) (PRICE).
    /// </summary>
    /// <remarks>Format: N4+N..15 (FNC1)</remarks>
    ApplicableAmountPayableSingleMonetaryArea = 392,

    /// <summary>
    ///     Applicable amount payable with ISO currency code (variable measure trade item) (PRICE).
    /// </summary>
    /// <remarks>Format: N4+N3+N..15 (FNC1)</remarks>
    ApplicableAmountPayableWithWithIsoCurrencyCode = 393,

    /// <summary>
    ///     Percentage discount of a coupon (PRCNT OFF).
    /// </summary>
    /// <remarks>Format: N4+N4 (FNC1)</remarks>
    PercentageDiscountOfACoupon = 394,

    /// <summary>
    ///     Amount payable per unit of measure single monetary area (variable measure trade item) (PRICE/UoM).
    /// </summary>
    /// <remarks>Format: N4+N6 (FNC1)</remarks>
    AmountPayablePerUnitOfMeasureSingleMonetaryArea = 395,

    /// <summary>
    ///     Customer's purchase order number (ORDER NUMBER).
    /// </summary>
    /// <remarks>Format: N3+X..30 (FNC1)</remarks>
    CustomersPurchaseOrderNumber = 400,

    /// <summary>
    ///     Global Identification Number for Consignment (GINC) (GINC).
    /// </summary>
    /// <remarks>Format: N3+X..30 (FNC1)</remarks>
    GlobalIdentificationNumberForConsignment = 401,

    /// <summary>
    ///     Global Shipment Identification Number (GSIN) (GSIN).
    /// </summary>
    /// <remarks>Format: N3+N17 (FNC1)</remarks>
    GlobalShipmentIdentificationNumber = 402,

    /// <summary>
    ///     Routing code (ROUTE).
    /// </summary>
    /// <remarks>Format: N3+X..30 (FNC1)</remarks>
    RoutingCode = 403,

    /// <summary>
    ///     Ship to - Deliver to Global Location Number (SHIP TO LOC).
    /// </summary>
    /// <remarks>Format: N3+N13</remarks>
    ShipToDeliverToGlobalLocationNumber = 410,

    /// <summary>
    ///     Bill to - Invoice to Global Location Number  (BILL TO).
    /// </summary>
    /// <remarks>Format: N3+N13</remarks>
    BillToInvoiceToGlobalLocationNumber = 411,

    /// <summary>
    ///     Purchased from Global Location Number (PURCHASE FROM).
    /// </summary>
    /// <remarks>Format: N3+N13</remarks>
    PurchasedFromGlobalLocationNumber = 412,

    /// <summary>
    ///     Ship for - Deliver for - Forward to Global Location Number (SHIP FOR LOC).
    /// </summary>
    /// <remarks>Format: N3+N13</remarks>
    ShipForDeliverForForwardToGlobalLocationNumber = 413,

    /// <summary>
    ///     Identification of a physical location - Global Location Number (LOC No).
    /// </summary>
    /// <remarks>Format: N3+N13</remarks>
    IdentificationOfAPhysicalLocationGlobalLocationNumber = 414,

    /// <summary>
    ///     Global Location Number of the invoicing party (PAY TO).
    /// </summary>
    /// <remarks>Format: N3+N13</remarks>
    GlobalLocationNumberOfTheInvoicingParty = 415,

    /// <summary>
    ///     GLN of the production or service location (PROD/SERV LOC).
    /// </summary>
    /// <remarks>Format: N3+N13</remarks>
    GlnOfTheProductionOrServiceLocation = 416,

    /// <summary>
    ///     Party GLN (PARTY).
    /// </summary>
    /// <remarks>Format: N3+N13</remarks>
    PartyGln = 417,

    /// <summary>
    ///     Ship to - Deliver to postal code within a single postal authority (SHIP TO POST).
    /// </summary>
    /// <remarks>Format: N3+X..20 (FNC1)</remarks>
    ShipToDeliverToPostalCodeWithinASinglePostalAuthority = 420,

    /// <summary>
    ///     Ship to - Deliver to postal code with ISO country code (SHIP TO POST).
    /// </summary>
    /// <remarks>Format: N3+N3+X..9 (FNC1)</remarks>
    ShipToDeliverToPostalCodeWithIsoCountryCode = 421,

    /// <summary>
    ///     Country of origin of a trade item (ORIGIN).
    /// </summary>
    /// <remarks>Format: N3+N3 (FNC1)</remarks>
    CountryOfOriginOfATradeItem = 422,

    /// <summary>
    ///     Country of initial processing (COUNTRY - INITIAL PROCESS).
    /// </summary>
    /// <remarks>Format: N3+N3+N..12 (FNC1)</remarks>
    CountryOfInitialProcessing = 423,

    /// <summary>
    ///     Country of processing (COUNTRY - PROCESS).
    /// </summary>
    /// <remarks>Format: N3+N3 (FNC1)</remarks>
    CountryOfProcessing = 424,

    /// <summary>
    ///     Country of disassembly (COUNTRY - DISASSEMBLY).
    /// </summary>
    /// <remarks>Format: N3+N3+N..12 (FNC1)</remarks>
    CountryOfDisassembly = 425,

    /// <summary>
    ///     Country covering full process chain (COUNTRY – FULL PROCESS).
    /// </summary>
    /// <remarks>Format: N3+N3 (FNC1)</remarks>
    CountryCoveringFullProcessChain = 426,

    /// <summary>
    ///     Country subdivision Of origin (ORIGIN SUBDIVISION).
    /// </summary>
    /// <remarks>Format: N3+X..3 (FNC1)</remarks>
    CountrySubdivisionOfOrigin = 427,

    /// <summary>
    ///     Ship-to / Deliver-to Company name (SHIP TO COMP).
    /// </summary>
    /// <remarks>Format: N4+X..35 (FNC1)</remarks>
    ShipToDeliverToCompanyName = 4300,

    /// <summary>
    ///     Ship-to / Deliver-to contact name (SHIP TO NAME).
    /// </summary>
    /// <remarks>Format: N4+X..35 (FNC1)</remarks>
    ShipToDeliverToContactName = 4301,

    /// <summary>
    ///     Ship-to / Deliver-to address line 1 (SHIP TO ADD1).
    /// </summary>
    /// <remarks>Format: N4+X..70 (FNC1)</remarks>
    ShipToDeliverToAddressLine1 = 4302,

    /// <summary>
    ///     Ship-to / Deliver-to address line 2 (SHIP TO ADD2).
    /// </summary>
    /// <remarks>Format: N4+X..70 (FNC1)</remarks>
    ShipToDeliverToAddressLine2 = 4303,

    /// <summary>
    ///     Ship-to / Deliver-to suburb (SHIP TO SUB).
    /// </summary>
    /// <remarks>Format: N4+X..70 (FNC1)</remarks>
    ShipToDeliverToSuburb = 4304,

    /// <summary>
    ///     Ship-to / Deliver-to locality (SHIP TO LOC).
    /// </summary>
    /// <remarks>Format: N4+X..70 (FNC1)</remarks>
    ShipToDeliverToLocality = 4305,

    /// <summary>
    ///     Ship-to / Deliver-to region (SHIP TO REG).
    /// </summary>
    /// <remarks>Format: N4+X..70 (FNC1)</remarks>
    ShipToDeliverToRegion = 4306,

    /// <summary>
    ///     Ship-to / Deliver-to country code (SHIP TO COUNTRY).
    /// </summary>
    /// <remarks>Format: N4+X2 (FNC1)</remarks>
    ShipToDeliverToCountryCode = 4307,

    /// <summary>
    ///     Ship-to / Deliver-to telephone number (SHIP TO PHONE).
    /// </summary>
    /// <remarks>Format: N4+X..30 (FNC1)</remarks>
    ShipToDeliverToTelephoneNumber = 4308,

    /// <summary>
    ///     Ship-to / Deliver-to GEO location (SHIP TO GEO).
    /// </summary>
    /// <remarks>Format: N4+N20 (FNC1)</remarks>
    ShipToDeliverToGeoLocation = 4309,

    /// <summary>
    ///     Return-to company name (RTN TO COMP).
    /// </summary>
    /// <remarks>Format: N4+X..35 (FNC1)</remarks>
    ReturnToCompanyName = 4310,

    /// <summary>
    ///     Return-to contact name (RTN TO NAME).
    /// </summary>
    /// <remarks>Format: N4+X..35 (FNC1)</remarks>
    ReturnToContactName = 4311,

    /// <summary>
    ///     Return-to address line 1 (RTN TO ADD1).
    /// </summary>
    /// <remarks>Format: N4+X..70 (FNC1)</remarks>
    ReturnToAddressLine1 = 4312,

    /// <summary>
    ///     Return-to address line 2 (RTN TO ADD2).
    /// </summary>
    /// <remarks>Format: N4+X..70 (FNC1)</remarks>
    ReturnToAddressLine2 = 4313,

    /// <summary>
    ///     Return-to suburb (RTN TO SUB).
    /// </summary>
    /// <remarks>Format: N4+X..70 (FNC1)</remarks>
    ReturnToSuburb = 4314,

    /// <summary>
    ///     Return-to locality (RTN TO LOC).
    /// </summary>
    /// <remarks>Format: N4+X..70 (FNC1)</remarks>
    ReturnToLocality = 4315,

    /// <summary>
    ///     Return-to region (RTN TO REG).
    /// </summary>
    /// <remarks>Format: N4+X..70 (FNC1)</remarks>
    ReturnToRegion = 4316,

    /// <summary>
    ///     Return-to country code (RTN TO COUNTRY).
    /// </summary>
    /// <remarks>Format: N4+X2 (FNC1)</remarks>
    ReturnToCountryCode = 4317,

    /// <summary>
    ///     Return-to postal code (RTN TO POST).
    /// </summary>
    /// <remarks>Format: N4+X..20 (FNC1)</remarks>
    ReturnToPostalCode = 4318,

    /// <summary>
    ///     Return-to telephone number (RTN TO PHONE).
    /// </summary>
    /// <remarks>Format: N4+X..30 (FNC1)</remarks>
    ReturnToTelephoneNumber = 4319,

    /// <summary>
    ///     Service code description (SRV DESCRIPTION).
    /// </summary>
    /// <remarks>Format: N4+X..35 (FNC1)</remarks>
    ServiceCodeDescription = 4320,

    /// <summary>
    ///     Dangerous goods flag (DANGEROUS GOODS).
    /// </summary>
    /// <remarks>Format: N4+N1 (FNC1)</remarks>
    DangerousGoodsFlag = 4321,

    /// <summary>
    ///     Authority to leave flag (AUTH LEAV).
    /// </summary>
    /// <remarks>Format: N4+N1 (FNC1) (FNC1)</remarks>
    AuthorityToLeaveFlag = 4322,

    /// <summary>
    ///     Signature required flag (SIG REQUIRED).
    /// </summary>
    /// <remarks>Format: N4+N1 (FNC1)</remarks>
    SignatureRequiredFlag = 4323,

    /// <summary>
    ///     Not before delivery date/time (NBEF DEL DT).
    /// </summary>
    /// <remarks>Format: N4+N10 (FNC1)</remarks>
    NotBeforeDeliveryDateTime = 4324,

    /// <summary>
    ///     Not after delivery date/time (NAFT DEL DT).
    /// </summary>
    /// <remarks>Format: N4+N10 (FNC1)</remarks>
    NotAfterDeliveryDateTime = 4325,

    /// <summary>
    ///     Release date (REL DATE).
    /// </summary>
    /// <remarks>Format: N4+N6 (FNC1)</remarks>
    ReleaseDate = 4326,

    /// <summary>
    ///     NATO Stock Number (NSN) (NSN).
    /// </summary>
    /// <remarks>Format: N4+N13 (FNC1)</remarks>
    NatoStockNumber = 7001,

    /// <summary>
    ///     UN/ECE meat carcasses and cuts classification (MEAT CUT).
    /// </summary>
    /// <remarks>Format: N4+X..30 (FNC1)</remarks>
    UnEceMeatCarcassesAndCutsClassification = 7002,

    /// <summary>
    ///     Expiration date and time (EXPIRY TIME).
    /// </summary>
    /// <remarks>Format: N4+N10 (FNC1)</remarks>
    ExpirationDateAndTime = 7003,

    /// <summary>
    ///     Active potency (ACTIVE POTENCY).
    /// </summary>
    /// <remarks>Format: N4+N..4 (FNC1)</remarks>
    ActivePotency = 7004,

    /// <summary>
    ///     Catch area (CATCH AREA).
    /// </summary>
    /// <remarks>Format: N4+X..12 (FNC1)</remarks>
    CatchArea = 7005,

    /// <summary>
    ///     First freeze date (FIRST FREEZE DATE).
    /// </summary>
    /// <remarks>Format: N4+N6 (FNC1)</remarks>
    FirstFreezeDate = 7006,

    /// <summary>
    ///     Harvest date (HARVEST DATE).
    /// </summary>
    /// <remarks>Format: N4+N6..12 (FNC1)</remarks>
    HarvestDate = 7007,

    /// <summary>
    ///     Species for fishery purposes (AQUATIC SPECIES).
    /// </summary>
    /// <remarks>Format: N4+X..3 (FNC1)</remarks>
    SpeciesForFisheryPurposes = 7008,

    /// <summary>
    ///     Fishing gear type (FISHING GEAR TYPE).
    /// </summary>
    /// <remarks>Format: N4+X..10 (FNC1)</remarks>
    FishingGearType = 7009,

    /// <summary>
    ///     Test by date (PROD METHOD).
    /// </summary>
    /// <remarks>Format: N4+N6+[N4] (FNC1)</remarks>
    ProductionMethod = 7010,

    /// <summary>
    ///     Production method (TEST BY DATE).
    /// </summary>
    /// <remarks>Format: N4+X..2 (FNC1)</remarks>
    TestByDate = 7011,

    /// <summary>
    ///     Refurbishment lot ID (REFURB LOT).
    /// </summary>
    /// <remarks>Format: N4+X..20 (FNC1)</remarks>
    RefurbishmentLotId = 7020,

    /// <summary>
    ///     Functional status (FUNC STAT).
    /// </summary>
    /// <remarks>Format: N4+X..20 (FNC1)</remarks>
    FunctionalStatus = 7021,

    /// <summary>
    ///     Revision status (REV STAT).
    /// </summary>
    /// <remarks>Format: N4+X..20 (FNC1)</remarks>
    RevisionStatus = 7022,

    /// <summary>
    ///     Global Individual Asset Identifier (GIAI) of an assembly (GIAI – ASSEMBLY).
    /// </summary>
    /// <remarks>Format: N4+X..30 (FNC1)</remarks>
    GlobalIndividualAssetIdentifierOfAnAssembly = 7023,

    /// <summary>
    ///     Number of processor with ISO Country Code (PROCESSOR # s).
    /// </summary>
    /// <remarks>Format: N4+N3+X..27 (FNC1)</remarks>
    NumberOfProcessorWithIsoCountryCode = 703,

    /// <summary>
    ///     GS1 UIC with Extension 1 and Importer index (UIC+EXT).
    /// </summary>
    /// <remarks>Format: N4+N1+X3 (FNC1)</remarks>
    Gs1UicWithExtensionOneAndImporterIndex = 7040,

    /// <summary>
    ///     UN/CEFACT freight unit type (UFRGT UNIT TYPE).
    /// </summary>
    /// <remarks>Format: N4+X1..X4 (FNC1)</remarks>
    UnCefactFreightUnitType = 7041,

    /// <summary>
    ///     National Healthcare Reimbursement Number (NHRN) – Germany PZN (NHRN PZN).
    /// </summary>
    /// <remarks>Format: N3+X..20 (FNC1)</remarks>
    NationalHealthcareReimbursementNumberGermanyPzn = 710,

    /// <summary>
    ///     National Healthcare Reimbursement Number (NHRN) – France CIP (NHRN CIP).
    /// </summary>
    /// <remarks>Format: N3+X..20 (FNC1)</remarks>
    NationalHealthcareReimbursementNumberFranceCip = 711,

    /// <summary>
    ///     National Healthcare Reimbursement Number (NHRN) – Spain CN (NHRN CN).
    /// </summary>
    /// <remarks>Format: N3+X..20 (FNC1)</remarks>
    NationalHealthcareReimbursementNumberSpainCn = 712,

    /// <summary>
    ///     National Healthcare Reimbursement Number (NHRN) – Brazil DRN (NHRN DRN).
    /// </summary>
    /// <remarks>Format: N3+X..20 (FNC1)</remarks>
    NationalHealthcareReimbursementNumberBrazilDrn = 713,

    /// <summary>
    ///     National Healthcare Reimbursement Number (NHRN) – Portugal AIM (NHRN AIM).
    /// </summary>
    /// <remarks>Format: N3+X..20 (FNC1)</remarks>
    NationalHealthcareReimbursementNumberPortugalAim = 714,

    /// <summary>
    ///     National Healthcare Reimbursement Number (NHRN) – United States of America NDC (NHRN NDC).
    /// </summary>
    /// <remarks>Format: N3+X..20 (FNC1)</remarks>
    NationalHealthcareReimbursementNumberUnitedStatesNdc = 715,

    /// <summary>
    ///     National Healthcare Reimbursement Number (NHRN) – Italy AIC (NHRN AIC).
    /// </summary>
    /// <remarks>Format: N3+X..20 (FNC1)</remarks>
    NationalHealthcareReimbursementNumberItalyAic = 716,

    // ... (*****) National Healthcare Reimbursement Number (NHRN) – Country “A” NHRN N3+X..20 (FNC1) NHRN ResolveEntity

    /// <summary>
    ///     Certification reference (CERT # s).
    /// </summary>
    /// <remarks>Format: N4+X2+X..28 (FNC1)</remarks>
    CertificationReference = 723,

    /// <summary>
    ///     Protocol ID (PROTOCOL).
    /// </summary>
    /// <remarks>Format: N4+X..20 (FNC1)</remarks>
    ProtocolId = 7240,

    /// <summary>
    ///     Date of birth (DOB).
    /// </summary>
    /// <remarks>Format: N4+N8 (FNC1)</remarks>
    DateOfBirth = 7250,

    /// <summary>
    ///     Date and time of birth (DOB TIME).
    /// </summary>
    /// <remarks>Format: N4+N12 (FNC1)</remarks>
    DateAndTimeOfBirth = 7251,

    /// <summary>
    ///     Biological sex (BIO SEX).
    /// </summary>
    /// <remarks>Format: N4+N1 (FNC1)</remarks>
    BiologicalSex = 7252,

    /// <summary>
    ///     Family name of person (FAMILY NAME).
    /// </summary>
    /// <remarks>Format: N4+X..40 (FNC1)</remarks>
    FamilyNameOfPerson = 7253,

    /// <summary>
    ///     Given name of person (GIVEN NAME).
    /// </summary>
    /// <remarks>Format: N4+X..40 (FNC1)</remarks>
    GivenNameOfPerson = 7254,

    /// <summary>
    ///     Name suffix of person (SUFFIX).
    /// </summary>
    /// <remarks>Format: N4+X..10 (FNC1)</remarks>
    NameSuffixOfPerson = 7255,

    /// <summary>
    ///     Full name of person (FULL NAME).
    /// </summary>
    /// <remarks>Format: N4+X..90 (FNC1)</remarks>
    FullNameOfPerson = 7256,

    /// <summary>
    ///     Address of person (PERSON ADDR).
    /// </summary>
    /// <remarks>Format: N4+X..70 (FNC1)</remarks>
    AddressOfPerson = 7257,

    /// <summary>
    ///     Baby birth sequence indicator (BIRTH SEQUENCE).
    /// </summary>
    /// <remarks>Format: N4+N1+X1+N1 (FNC1)</remarks>
    BabyBirthSequenceIndicator = 7258,

    /// <summary>
    ///     Baby of family name (BABY).
    /// </summary>
    /// <remarks>Format: N4+X..40 (FNC1)</remarks>
    BabyOfFamilyName = 7259,

    /// <summary>
    ///     Roll products (width, length, core diameter, direction, splices) (DIMENSIONS).
    /// </summary>
    /// <remarks>Format: N4+N14 (FNC1)</remarks>
    RollProductsWidthLengthCorDiameterDirectionSplices = 8001,

    /// <summary>
    ///     Cellular mobile telephone identifier (CMT No).
    /// </summary>
    /// <remarks>Format: N4+X..20 (FNC1)</remarks>
    CellularMobileTelephoneIdentifier = 8002,

    /// <summary>
    ///     Global Returnable Asset Identifier (GRAI) (GRAI).
    /// </summary>
    /// <remarks>Format: N4+N14+X..16 (FNC1)</remarks>
    GlobalReturnableAssetIdentifier = 8003,

    /// <summary>
    ///     Global Individual Asset Identifier (GIAI) (GIAI).
    /// </summary>
    /// <remarks>Format: N4+X..30 (FNC1)</remarks>
    GlobalIndividualAssetIdentifier = 8004,

    /// <summary>
    ///     Price per unit of measure (PRICE PER UNIT),
    /// </summary>
    /// <remarks>Format: N4+N6 (FNC1)</remarks>
    PricePerUnitOfMeasure = 8005,

    /// <summary>
    ///     Identification of an individual trade item piece (ITIP or GCTIN).
    /// </summary>
    /// <remarks>Format: N4+N6N4+N14+N2+N2 (FNC1)</remarks>
    IdentificationOfAnIndividualTradeItemPiece = 8006,

    /// <summary>
    ///     International Bank Account Number (IBAN) (IBAN).
    /// </summary>
    /// <remarks>Format: N4+X..34 (FNC1)</remarks>
    InternationalBankAccountNumber = 8007,

    /// <summary>
    ///     Date and time of production (PROD TIME).
    /// </summary>
    /// <remarks>Format: N4+N8+N..4 (FNC1)</remarks>
    DateAndTimeOfProduction = 8008,

    /// <summary>
    ///     Optically readable sensor indicator (OPTSEN).
    /// </summary>
    /// <remarks>Format: N4+X..50 (FNC1)</remarks>
    OpticallyReadableSensorIndicator = 8009,

    /// <summary>
    ///     Component / Part Identifier (CPID) (CPID).
    /// </summary>
    /// <remarks>Format: N4 + X..30 (FNC1)</remarks>
    ComponentPartIdentifier = 8010,

    /// <summary>
    ///     Component / Part Identifier serial number (CPID SERIAL) (CPID SERIAL).
    /// </summary>
    /// <remarks>Format: N4 + N..12 (FNC1)</remarks>
    ComponentPartIdentifierSerialNumber = 8011,

    /// <summary>
    ///     Software version (VERSION).
    /// </summary>
    /// <remarks>Format: N4 + X..20 (FNC1)</remarks>
    SoftwareVersion = 8012,

    /// <summary>
    ///     Global Model Number (GMN).
    /// </summary>
    /// <remarks>Format: N4 + X..30 (FNC1)</remarks>
    GlobalModelNumber = 8013,

    /// <summary>
    ///    Highly Individualised Device Registration Identifier (MUDI).
    /// </summary>
    /// <remarks>Format: N4+X..25 (FNC1)</remarks>
    HighlyIndividualisedDeviceRegistrationIdentifier = 8014,

    /// <summary>
    ///     Global Service Relation Number to identify the relationship between an organisation offering services and the
    ///     provider of services (GSRN - PROVIDER).
    /// </summary>
    /// <remarks>Format: N4+N18 (FNC1)</remarks>
    GlobalServiceRelationNumberToIdentifyTheRelationshipBetweenAnOrganisationOfferingServicesAndTheProviderOfServices
    = 8017,

    /// <summary>
    ///     Global Service Relation Number to identify the relationship between an organisation offering services and the
    ///     recipient of services (GSRN - RECIPIENT).
    /// </summary>
    /// 8017
    /// <remarks>Format: N4+N18 (FNC1)</remarks>
    GlobalServiceRelationNumberToIdentifyTheRelationshipBetweenAnOrganisationOfferingServicesAndTheRecipientOfServices
    = 8018,

    /// <summary>
    ///     Service Relation Instance Number (SRIN) (SRIN).
    /// </summary>
    /// <remarks>Format: N4+N..10 (FNC1)</remarks>
    ServiceRelationInstanceNumber = 8019,

    /// <summary>
    ///     Payment slip reference number (REF No).
    /// </summary>
    /// <remarks>Format: N4+X..25 (FNC1)</remarks>
    PaymentSlipReferenceNumber = 8020,

    /// <summary>
    ///     Identification of pieces of a trade item (ITIP) contained in a logistic unit (ITIP CONTENT).
    /// </summary>
    /// <remarks>Format: N4+N14+N2+N2 (FNC1)</remarks>
    IdentificationOfTradeItemPieces = 8026,

    /// <summary>
    ///     Coupon code identification for use in North America (-).
    /// </summary>
    /// <remarks>Format: N4+X..70 (FNC1)</remarks>
    CouponCodeIdentificationForUseInNorthAmerica = 8110,

    /// <summary>
    ///     Loyalty points of a coupon (POINTS).
    /// </summary>
    /// <remarks>Format: N4+N4 (FNC1)</remarks>
    LoyaltyPointsOfACoupon = 8111,

    /// <summary>
    ///     Paperless coupon code identification for use in North America (AI 8112) (-).
    /// </summary>
    /// <remarks>Format: N4+X..70 (FNC1)</remarks>
    PaperlessCouponCodeIdentificationForUseInNorthAmerica = 8112,

    /// <summary>
    ///     Extended Packaging URL (PRODUCT URL)
    /// </summary>
    /// <remarks>Format: N4+X..70 (FNC1)</remarks>
    ExtendedPackagingUrl = 8200,

    /// <summary>
    ///     Information mutually agreed between trading partners (INTERNAL).
    /// </summary>
    /// <remarks>Format: N2+X..30 (FNC1)</remarks>
    InformationMutuallyAgreedBetweenTradingPartners = 90,

    /// <summary>
    ///     Company internal information (INTERNAL).
    /// </summary>
    /// <remarks>Format: N2+X..90 (FNC1)</remarks>
    CompanyInternalInformation1 = 91,

    /// <summary>
    ///     Company internal information (INTERNAL).
    /// </summary>
    /// <remarks>Format: N2+X..90 (FNC1)</remarks>
    CompanyInternalInformation2 = 92,

    /// <summary>
    ///     Company internal information (INTERNAL).
    /// </summary>
    /// <remarks>Format: N2+X..90 (FNC1)</remarks>
    CompanyInternalInformation3 = 93,

    /// <summary>
    ///     Company internal information (INTERNAL).
    /// </summary>
    /// <remarks>Format: N2+X..90 (FNC1)</remarks>
    CompanyInternalInformation4 = 94,

    /// <summary>
    ///     Company internal information (INTERNAL).
    /// </summary>
    /// <remarks>Format: N2+X..90 (FNC1)</remarks>
    CompanyInternalInformation5 = 95,

    /// <summary>
    ///     Company internal information (INTERNAL).
    /// </summary>
    /// <remarks>Format: N2+X..90 (FNC1)</remarks>
    CompanyInternalInformation6 = 96,

    /// <summary>
    ///     Company internal information (INTERNAL).
    /// </summary>
    /// <remarks>Format: N2+X..90 (FNC1)</remarks>
    CompanyInternalInformation7 = 97,

    /// <summary>
    ///     Company internal information (INTERNAL).
    /// </summary>
    /// <remarks>Format: N2+X..90 (FNC1)</remarks>
    CompanyInternalInformation8 = 98,

    /// <summary>
    ///     Company internal information (INTERNAL).
    /// </summary>
    /// <remarks>Format: N2+X..90 (FNC1)</remarks>
    CompanyInternalInformation9 = 99,
}