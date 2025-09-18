// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnCefactFreightUnitTypeDescriptor.cs" company="Solidsoft Reply Ltd">
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
// A descriptor for UN/CEFACT freight unit types
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai.Descriptors;

using System.Globalization;
using System.Text.RegularExpressions;
using Solidsoft.Reply.Parsers.Common;
using Solidsoft.Reply.Parsers.Gs1Ai.Properties;

/// <summary>
///     A descriptor for UN/CEFACT freight unit types.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="UnCefactFreightUnitTypeDescriptor" /> class.
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
/// <remarks>
/// <para>Ascertaining the valid codes for AI 7041 is problematical based on the available
/// documentation. GS1 maintains a package type code list which they state is based on the
/// UN/ECE Recommendation 21 codes with some additional GS1-defined codes. In reality, the
/// GS1 list appears to be based on the unece:PackageTypeCodeList category of UN/CEFACT
/// alphanumeric codes which is, for the most part, a superset of the UN/ECE Recommendation
/// 21 codes. Three alphabetic codes in UNECE Recommendation 21 (AE, BA and VT) are not
/// included in the GS1 list. One of these (VT) is not included in the
/// unece:PackageTypeCodeList category. To further complicate matters, UNECE Recommendation
/// 21 codes defines both alphabetic and numeric codes. In some cases, numeric codes in
/// UNECE Recommendation 21 conflict with codes in the unece:PackageTypeCodeList category
/// and the GS1 list.</para>
/// <para>We have adopted the following approach to validation. We accept the
/// de-duplicated union of the following:</para>
/// <list type="number">
/// <item>
/// <description>All codes in the unece:PackageTypeCodeList category.</description>
/// </item>
/// <item>
/// <description>All codes in the GS1 PackageTypeCode code list.</description>
/// </item>
/// <item>
/// <description>All alphabetic codes in UNECE Recommendation 21.</description>
/// </item>
/// </list>
/// </remarks>
internal
#if NET7_0_OR_GREATER
partial
#endif
class UnCefactFreightUnitTypeDescriptor(
    string dataTitle,
    string description,
    Regex pattern,
    bool isFixedWidth)
: EntityDescriptor(dataTitle, description, pattern, isFixedWidth) {

    private static Dictionary<string, (string, string)> _freightUnitTypes = new () {
        { "8", ("Oneway pallet (GS1 Code)", "Pallet need not be returned to the point of expedition") },
        { "9", ("Returnable pallet (GS1 Code)", "Pallet must be returned to the point of expedition.") },
        { "43", ("Bag, super bulk", "A cloth plastic or paper based bag having the dimensions of the pallet on which it is constructed.") },
        { "44", ("Bag, polybag", "A type of plastic bag, typically used to wrap promotional pieces, publications, product samples, and/or catalogues.") },
        { "200", ("Pallet ISO 0 - 1/2 EURO Pallet (GS1 Code)", "Standard pallet with dimensions 80 X 60 cm.") },
        { "201", ("Pallet ISO 1 - 1/1 EURO Pallet (GS1 Code)", "Standard pallet with dimensions 80 X 120 cm.") },
        { "202", ("Pallet ISO 2 – 2/1 EURO Pallet (GS1 Code)", "Standard pallet with dimensions 100 X 120 cm.") },
        { "203", ("1/4 EURO Pallet (GS1 Code)", "Standard pallet with dimensions 60 X 40 cm.") },
        { "204", ("1/8 EURO Pallet (GS1 Code)", "Standard pallet with dimensions 40 X 30 cm.") },
        { "205", ("Synthetic pallet ISO 1 (GS1 Code)", "A standard pallet with standard dimensions 80*120cm made of a synthetic material for hygienic reasons.") },
        { "206", ("Synthetic pallet ISO 2 (GS1 Code)", "A standard pallet with standard dimensions 100*120cm made of a synthetic material for hygienic reasons.") },
        { "210", ("Wholesaler pallet (GS1 Code)", "Pallet provided by the wholesaler.") },
        { "211", ("Pallet 80 X 100 cm (GS1 Code)", "Pallet with dimensions 80 X 100 cm.") },
        { "212", ("Pallet 60 X 100 cm (GS1 Code)", "Pallet with dimensions 60 X 100 cm.") },
        { "1A", ("Drum, steel", string.Empty) },
        { "1B", ("Drum, aluminium", string.Empty) },
        { "1D", ("Drum, plywood", string.Empty) },
        { "1F", ("Container, flexible", "A packaging container of flexible construction.") },
        { "1G", ("Drum, fibre", string.Empty) },
        { "1W", ("Drum, wooden", string.Empty) },
        { "2C", ("Barrel, wooden", string.Empty) },
        { "3A", ("Jerrican, steel", string.Empty) },
        { "3H", ("Jerrican, plastic", string.Empty) },
        { "4A", ("Box, steel", string.Empty) },
        { "4B", ("Box, aluminium", string.Empty) },
        { "4C", ("Box, natural wood", string.Empty) },
        { "4D", ("Box, plywood", string.Empty) },
        { "4F", ("Box, reconstituted wood", string.Empty) },
        { "4G", ("Box, fibreboard", string.Empty) },
        { "4H", ("Box, plastic", string.Empty) },
        { "5H", ("Bag, woven plastic", string.Empty) },
        { "5L", ("Bag, textile", string.Empty) },
        { "5M", ("Bag, paper", string.Empty) },
        { "6H", ("Composite packaging, plastic receptacle", string.Empty) },
        { "6P", ("Composite packaging, glass receptacle", string.Empty) },
        { "7A", ("Case, car", "A type of portable container designed to store equipment for carriage in an automobile.") },
        { "7B", ("Case, wooden", "A case made of wood for retaining substances or articles.") },
        { "8A", ("Pallet, wooden", "A platform or open-ended box, made of wood, on which goods are retained for ease of mechanical handling during transport and storage.") },
        { "8B", ("Crate, wooden", "A receptacle, made of wood, on which goods are retained for ease of mechanical handling during transport and storage.") },
        { "8C", ("Bundle, wooden", "Loose or unpacked pieces of wood tied or wrapped together.") },
        { "AA", ("Intermediate bulk container, rigid plastic", string.Empty) },
        { "AB", ("Receptacle, fibre", "Containment vessel made of fibre used for retaining substances or articles.") },
        { "AC", ("Receptacle, paper", "Containment vessel made of paper for retaining substances or articles.") },
        { "AD", ("Receptacle, wooden", "Containment vessel made of wood for retaining substances or articles.") },
        { "AE", ("Aerosol", string.Empty) },
        { "AF", ("Pallet, modular, collars 80cms * 60cms", "Standard sized pallet of dimensions 80 centimeters by 60 centimeters (cms).") },
        { "AG", ("Pallet, shrinkwrapped", "Pallet load secured with transparent plastic film that has been wrapped around and then shrunk tightly.") },
        { "AH", ("Pallet, 100cms * 110cms", "Standard sized pallet of dimensions 100centimeters by 110 centimeters (cms).") },
        { "AI", ("Clamshell", "GS1 Description: A package with a base and top that are hinged together. E.g. video cassette case.") },
        { "AJ", ("Cone", "Container used in the transport of linear material such as yarn.") },
        { "AL", ("Ball", "A spherical containment vessel for retaining substances or articles.") },
        { "AM", ("Ampoule, non-protected", string.Empty) },
        { "AP", ("Ampoule, protected", string.Empty) },
        { "APE", ("Aluminium packed (GS1 Code)", "Packaging using thin sheets of aluminium.") },
        { "AT", ("Atomizer", string.Empty) },
        { "AV", ("Capsule", string.Empty) },
        { "B4", ("Belt", "A band use to retain multiple articles together.") },
        { "BA", ("Barrel", string.Empty) },
        { "BB", ("Bobbin", string.Empty) },
        { "BC", ("Bottlecrate / bottlerack", string.Empty) },
        { "BD", ("Board", string.Empty) },
        { "BE", ("Bundle", string.Empty) },
        { "BF", ("Balloon, non-protected", string.Empty) },
        { "BG", ("Bag", "A receptacle made of flexible material with an open or closed top.") },
        { "BGE", ("Large bag, pallet sized (GS1 Code)", "A non-rigid container made of fabric, paper, plastic, etc, with an opening at the top which can be closed and which is suitable for use on pallets.") },
        { "BH", ("Bunch", string.Empty) },
        { "BI", ("Bin", string.Empty) },
        { "BJ", ("Bucket", string.Empty) },
        { "BK", ("Basket", string.Empty) },
        { "BL", ("Bale, compressed", string.Empty) },
        { "BM", ("Basin", string.Empty) },
        { "BME", ("Blister pack (GS1 Code)", "A transparent strip package of pressable plastic which allows the product to be displayed while remaining protected.") },
        { "BN", ("Bale, non-compressed", string.Empty) },
        { "BO", ("Bottle, non-protected, cylindrical", "A narrow-necked cylindrical shaped vessel without external protective packing material.") },
        { "BP", ("Balloon, protected", string.Empty) },
        { "BQ", ("Bottle, protected cylindrical", "A narrow-necked cylindrical shaped vessel with external protective packing material.") },
        { "BR", ("Bar", string.Empty) },
        { "BRI", ("Brick (GS1 Code)", "A box made of a cardboard, plastic or metal, used for liquids.") },
        { "BS", ("Bottle, non-protected, bulbous", "A narrow-necked bulb shaped vessel without external protective packing material.") },
        { "BT", ("Bolt", string.Empty) },
        { "BU", ("Butt", string.Empty) },
        { "BV", ("Bottle, protected bulbous", "A narrow-necked bulb shaped vessel with external protective packing material.") },
        { "BW", ("Box, for liquids", string.Empty) },
        { "BX", ("Box", string.Empty) },
        { "BY", ("Board, in bundle/bunch/truss", string.Empty) },
        { "BZ", ("Bars, in bundle/bunch/truss", string.Empty) },
        { "CA", ("Can, rectangular", string.Empty) },
        { "CB", ("Crate, beer", string.Empty) },
        { "CBL", ("Container bottle like (GS1 Code)", "A non-protected, non-cylindrical, container with a narrow neck made usually of glass or plastic which is especially used for liquids, e.g. perfume bottle.") },
        { "CC", ("Churn", string.Empty) },
        { "CCE", ("Cardboard carrier (GS1 Code)", "A package made of cardboard.") },
        { "CD", ("Can, with handle and spout", "GS1 Description: A can with a handle and spout which allows the lifting and pouring of liquids contained within the can") },
        { "CE", ("Creel", string.Empty) },
        { "CF", ("Coffer", string.Empty) },
        { "CG", ("Cage", string.Empty) },
        { "CH", ("Chest", string.Empty) },
        { "CI", ("Canister", string.Empty) },
        { "CJ", ("Coffin", string.Empty) },
        { "CK", ("Cask", string.Empty) },
        { "CL", ("Coil", string.Empty) },
        { "CM", ("Card", "A flat package usually made of fibreboard from/to which product is often hung or attached.") },
        { "CN", ("Container, not otherwise specified as transport equipment", "GS1 Description: A receptacle in which something is kept and/or transported.") },
        { "CO", ("Carboy, non-protected", string.Empty) },
        { "CP", ("Carboy, protected", string.Empty) },
        { "CQ", ("Cartridge", "Package containing a charge such as propelling explosive for firearms or ink toner for a printer.") },
        { "CR", ("Crate", string.Empty) },
        { "CS", ("Case", string.Empty) },
        { "CT", ("Carton", string.Empty) },
        { "CU", ("Cup", string.Empty) },
        { "CV", ("Cover", string.Empty) },
        { "CW", ("Cage, roll", string.Empty) },
        { "CX", ("Can, cylindrical", string.Empty) },
        { "CY", ("Cylinder", string.Empty) },
        { "CZ", ("Canvas", string.Empty) },
        { "DA", ("Crate, multiple layer, plastic", "GS1 Description: Plastic crate which contains multiple layers.") },
        { "DB", ("Crate, multiple layer, wooden", "GS1 Description: Wooden crate which contains multiple layers.") },
        { "DC", ("Crate, multiple layer, cardboard", string.Empty) },
        { "DG", ("Cage, Commonwealth Handling Equipment Pool (CHEP)", string.Empty) },
        { "DH", ("Box, Commonwealth Handling Equipment Pool (CHEP), Eurobox", "A box mounted on a pallet base under the control of CHEP.") },
        { "DI", ("Drum, iron", string.Empty) },
        { "DJ", ("Demijohn, non-protected", string.Empty) },
        { "DK", ("Crate, bulk, cardboard", string.Empty) },
        { "DL", ("Crate, bulk, plastic", string.Empty) },
        { "DM", ("Crate, bulk, wooden", string.Empty) },
        { "DN", ("Dispenser", string.Empty) },
        { "DP", ("Demijohn, protected", string.Empty) },
        { "DPE", ("Display package (GS1 Code)", "A package used for the display of goods, usually during a promotion.") },
        { "DR", ("Drum", string.Empty) },
        { "DS", ("Tray, one layer no cover, plastic", string.Empty) },
        { "DT", ("Tray, one layer no cover, wooden", string.Empty) },
        { "DU", ("Tray, one layer no cover, polystyrene", string.Empty) },
        { "DV", ("Tray, one layer no cover, cardboard", string.Empty) },
        { "DW", ("Tray, two layers no cover, plastic tray", string.Empty) },
        { "DX", ("Tray, two layers no cover, wooden", string.Empty) },
        { "DY", ("Tray, two layers no cover, cardboard", string.Empty) },
        { "E1", ("Performance meat container E1", "Standard performance meat container with dimensions 60 X 40 X 12,5 cm.") },
        { "E2", ("Performance meat container E2", "Standard performance meat container with dimensions 60 X 40 X 20 cm.") },
        { "E3", ("Performance meat container E3", "Standard performance meat container with dimensions 60 X 40 X 30 cm.") },
        { "EC", ("Bag, plastic", string.Empty) },
        { "ED", ("Case, with pallet base", string.Empty) },
        { "EE", ("Case, with pallet base, wooden", string.Empty) },
        { "EF", ("Case, with pallet base, cardboard", string.Empty) },
        { "EG", ("Case, with pallet base, plastic", string.Empty) },
        { "EH", ("Case, with pallet base, metal", string.Empty) },
        { "EI", ("Case, isothermic", string.Empty) },
        { "EN", ("Envelope", string.Empty) },
        { "FB", ("Flexibag", "A flexible containment bag made of plastic, typically for the transportation bulk non-hazardous cargoes using standard size shipping containers.") },
        { "FC", ("Crate, fruit", string.Empty) },
        { "FD", ("Crate, framed", string.Empty) },
        { "FE", ("Flexitank", "A flexible containment tank made of plastic, typically for the transportation bulk non-hazardous cargoes using standard size shipping containers.") },
        { "FI", ("Firkin", string.Empty) },
        { "FL", ("Flask", string.Empty) },
        { "FO", ("Footlocker", string.Empty) },
        { "FOB", ("Folding box (GS1 Code)", "Folded cardboard box e.g. for products like frozen vegetables, paper clips.") },
        { "FP", ("Filmpack", string.Empty) },
        { "FPE", ("Foil packed (GS1 Code)", "Packaging using a metallic foil.") },
        { "FR", ("Frame", string.Empty) },
        { "FT", ("Foodtainer", string.Empty) },
        { "FW", ("Cart, flatbed", "Wheeled flat bedded device on which trays or other regular shaped items are packed for transportation purposes.") },
        { "FX", ("Bag, flexible container", string.Empty) },
        { "GB", ("Bottle, gas", "A narrow-necked metal cylinder for retention of liquefied or compressed gas.") },
        { "GI", ("Girder", string.Empty) },
        { "GL", ("Container, gallon", "A container with a capacity of one gallon.") },
        { "GR", ("Receptacle, glass", "Containment vessel made of glass for retaining substances or articles.") },
        { "GU", ("Tray, containing horizontally stacked flat items", "Tray containing flat items stacked on top of one another.") },
        { "GY", ("Bag, gunny", "A sack made of gunny or burlap, used for transporting coarse commodities, such as grains, potatoes, and other agricultural products.") },
        { "GZ", ("Girders, in bundle/bunch/truss", string.Empty) },
        { "HA", ("Basket, with handle, plastic", string.Empty) },
        { "HB", ("Basket, with handle, wooden", string.Empty) },
        { "HC", ("Basket, with handle, cardboard", string.Empty) },
        { "HG", ("Hogshead", string.Empty) },
        { "HN", ("Hanger", "A purpose shaped device with a hook at the top for hanging items from a rail.") },
        { "HR", ("Hamper", string.Empty) },
        { "IA", ("Package, display, wooden", string.Empty) },
        { "IB", ("Package, display, cardboard", string.Empty) },
        { "IC", ("Package, display, plastic", string.Empty) },
        { "ID", ("Package, display, metal", string.Empty) },
        { "IE", ("Package, show", string.Empty) },
        { "IF", ("Package, flow", "A flexible tubular package or skin, possibly transparent, often used for containment of foodstuffs (e.g. salami sausage).") },
        { "IG", ("Package, paper wrapped", string.Empty) },
        { "IH", ("Drum, plastic", string.Empty) },
        { "IK", ("Package, cardboard, with bottle grip-holes", "Packaging material made out of cardboard that facilitates the separation of individual glass or plastic bottles.") },
        { "IL", ("Tray, rigid, lidded stackable (CEN TS 14482:2002)", "Lidded stackable rigid tray compliant with CEN TS 14482:2002.") },
        { "IN", ("Ingot", string.Empty) },
        { "IZ", ("Ingots, in bundle/bunch/truss", string.Empty) },
        { "JB", ("Bag, jumbo", "A flexible containment bag, widely used for storage, transportation and handling of powder, flake or granular materials. Typically constructed from woven polypropylene (PP) fabric in the form of cubic bags.") },
        { "JC", ("Jerrican, rectangular", string.Empty) },
        { "JG", ("Jug", string.Empty) },
        { "JR", ("Jar", string.Empty) },
        { "JT", ("Jutebag", string.Empty) },
        { "JY", ("Jerrican, cylindrical", string.Empty) },
        { "KG", ("Keg", string.Empty) },
        { "KI", ("Kit", "A set of articles or implements used for a specific purpose.") },
        { "LAB", ("Labeled package (GS1 Code)", "The package is labeled. Usually the label identifies the name, brand or description of the product within the package.") },
        { "LE", ("Luggage", "A collection of bags, cases and/or containers which hold personal belongings for a journey.") },
        { "LG", ("Log", string.Empty) },
        { "LT", ("Lot", string.Empty) },
        { "LU", ("Lug", "A wooden box for the transportation and storage of fruit or vegetables.") },
        { "LV", ("Liftvan", "A wooden or metal container used for packing household goods and personal effects.") },
        { "LZ", ("Logs, in bundle/bunch/truss", string.Empty) },
        { "MA", ("Crate, metal", "Containment box made of metal for retaining substances or articles.") },
        { "MB", ("Bag, multiply", string.Empty) },
        { "MC", ("Crate, milk", string.Empty) },
        { "ME", ("Container, metal", "A type of containment box made of metal for retaining substances or articles, not otherwise specified as transport equipment.") },
        { "MPE", ("Multipack (GS1 Code)", "A container for the merchandising of multiple units of the same product.") },
        { "MR", ("Receptacle, metal", "Containment vessel made of metal for retaining substances or articles.") },
        { "MS", ("Sack, multi-wall", string.Empty) },
        { "MT", ("Mat", string.Empty) },
        { "MW", ("Receptacle, plastic wrapped", "Containment vessel wrapped with plastic for retaining substances or articles.") },
        { "MX", ("Matchbox", string.Empty) },
        { "NA", ("Not available", string.Empty) },
        { "NE", ("Unpacked or unpackaged", string.Empty) },
        { "NF", ("Unpacked or unpackaged, single unit", string.Empty) },
        { "NG", ("Unpacked or unpackaged, multiple units", string.Empty) },
        { "NS", ("Nest", string.Empty) },
        { "NT", ("Net", string.Empty) },
        { "NU", ("Net, tube, plastic", string.Empty) },
        { "NV", ("Net, tube, textile", string.Empty) },
        { "O1", ("Two sided cage on wheels with fixing strap", string.Empty) },
        { "O2", ("Trolley", string.Empty) },
        { "O3", ("Oneway pallet ISO 0 - 1/2 EURO Pallet", string.Empty) },
        { "O4", ("Oneway pallet ISO 1 - 1/1 EURO Pallet", string.Empty) },
        { "O5", ("Oneway pallet ISO 2 - 2/1 EURO Pallet", string.Empty) },
        { "O6", ("Pallet with exceptional dimensions", string.Empty) },
        { "O7", ("Wooden pallet 40 cm x 80 cm", string.Empty) },
        { "O8", ("Plastic pallet SRS 60 cm x 80 cm", string.Empty) },
        { "O9", ("Plastic pallet SRS 80 cm x 120 cm", string.Empty) },
        { "OA", ("Pallet, CHEP 40 cm x 60 cm", "Commonwealth Handling Equipment Pool (CHEP) standard pallet of dimensions 40 centimeters x 60 centimeters.") },
        { "OB", ("Pallet, CHEP 80 cm x 120 cm", "Commonwealth Handling Equipment Pool (CHEP) standard pallet of dimensions 80 centimeters x 120 centimeters.") },
        { "OC", ("Pallet, CHEP 100 cm x 120 cm", "Commonwealth Handling Equipment Pool (CHEP) standard pallet of dimensions 100 centimeters x 120 centimeters.") },
        { "OD", ("Pallet, AS 4068-1993", "Australian standard pallet of dimensions 115.5 centimeters x 116.5 centimeters.") },
        { "OE", ("Pallet, ISO T11", "ISO standard pallet of dimensions 110 centimeters x 110 centimeters, prevalent in Asia - Pacific region.") },
        { "OF", ("Platform, unspecified weight or dimension", "A pallet equivalent shipping platform of unknown dimensions or unknown weight.") },
        { "OG", ("Pallet ISO 0 - 1/2 EURO Pallet", string.Empty) },
        { "OH", ("Pallet ISO 1 - 1/1 EURO Pallet", string.Empty) },
        { "OI", ("Pallet ISO 2 - 2/1 EURO Pallet", string.Empty) },
        { "OJ", ("1/4 EURO Pallet", string.Empty) },
        { "OK", ("Block", "A solid piece of a hard substance, such as granite, having one or more flat sides.") },
        { "OL", ("1/8 EURO Pallet", string.Empty) },
        { "OM", ("Synthetic pallet ISO 1", string.Empty) },
        { "ON", ("Synthetic pallet ISO 2", string.Empty) },
        { "OP", ("Wholesaler pallet", string.Empty) },
        { "OPE", ("Oxygen packed (GS1 Code)", "A package with oxygen added for storage purposes.") },
        { "OQ", ("Pallet 80 X 100 cm", string.Empty) },
        { "OR", ("Pallet 60 X 100 cm", string.Empty) },
        { "OS", ("Oneway pallet", string.Empty) },
        { "OT", ("Octabin", "A standard cardboard container of large dimensions for storing for example vegetables, granules of plastics or other dry products.") },
        { "OU", ("Container, outer", "A type of containment box that serves as the outer shipping container, not otherwise specified as transport equipment.") },
        { "OV", ("Returnable pallet", string.Empty) },
        { "OW", ("Large bag, pallet sized", string.Empty) },
        { "OX", ("A wheeled pallet with raised rim (81 x 67 x 135)", string.Empty) },
        { "OY", ("A wheeled pallet with raised rim (81 x 72 x 135)", string.Empty) },
        { "OZ", ("A wheeled pallet with raised rim (81 x 60 x 16)", string.Empty) },
        { "P1", ("CHEP pallet 60 cm x 80 cm", string.Empty) },
        { "P2", ("Pan", "A shallow, wide, open container, usually of metal.") },
        { "P3", ("LPR pallet 60 cm x 80 cm", string.Empty) },
        { "P4", ("LPR pallet 80 cm x 120 cm", string.Empty) },
        { "PA", ("Packet", "Small package.") },
        { "PAE", ("Paper (GS1 Code)", "An indication that the item(s) is packed in paper.") },
        { "PB", ("Pallet, box Combined open-ended box and pallet", string.Empty) },
        { "PC", ("Parcel", string.Empty) },
        { "PD", ("Pallet, modular, collars 80cms * 100cms", "Standard sized pallet of dimensions 80 centimeters by 100 centimeters (cms).") },
        { "PE", ("Pallet, modular, collars 80cms * 120cms", "Standard sized pallet of dimensions 80 centimeters by 120 centimeters (cms).") },
        { "PF", ("Pen", "A small open top enclosure for retaining animals.") },
        { "PG", ("Plate", string.Empty) },
        { "PH", ("Pitcher", string.Empty) },
        { "PI", ("Pipe", string.Empty) },
        { "PJ", ("Punnet", "GS1 Description: A small shallow basket usually made of plastic.") },
        { "PK", ("Package", "Standard packaging unit.") },
        { "PL", ("Pail", string.Empty) },
        { "PLP", ("Peel pack (GS1 Code)", "A package used for sterile products which may be torn open without touching the product inside.") },
        { "PN", ("Plank", string.Empty) },
        { "PO", ("Pouch", string.Empty) },
        { "POP", ("Cone shaped paper wrapper (GS1 Code)", "Cone shaped paper wrapping e.g. for an individually packed ice cream cone.") },
        { "PP", ("Piece", "A loose or unpacked article.") },
        { "PPE", ("Polypropylene bag (GS1 Code)", "A bag made from polypropylene.") },
        { "PR", ("Receptacle, plastic", "Containment vessel made of plastic for retaining substances or articles.") },
        { "PT", ("Pot", string.Empty) },
        { "PU", ("Tray", string.Empty) },
        { "PUE", ("Tray packed in plastic (GS1 Code)", "A board with a ring packed in plastic carrying for small articles.") },
        { "PV", ("Pipes, in bundle/bunch/truss", string.Empty) },
        { "PX", ("Pallet", "Platform or open-ended box, usually made of wood, on which goods are retained for ease of mechanical handling during transport and storage.") },
        { "PY", ("Plates, in bundle/bunch/truss", string.Empty) },
        { "PZ", ("Planks, in bundle/bunch/truss", string.Empty) },
        { "QA", ("Drum, steel, non-removable head", string.Empty) },
        { "QB", ("Drum, steel, removable head", string.Empty) },
        { "QC", ("Drum, aluminium, non-removable head", string.Empty) },
        { "QD", ("Drum, aluminium, removable head", string.Empty) },
        { "QF", ("Drum, plastic, non-removable head", string.Empty) },
        { "QG", ("Drum, plastic, removable head", string.Empty) },
        { "QH", ("Barrel, wooden, bung type", string.Empty) },
        { "QJ", ("Barrel, wooden, removable head", string.Empty) },
        { "QK", ("Jerrican, steel, non-removable head", string.Empty) },
        { "QL", ("Jerrican, steel, removable head", string.Empty) },
        { "QM", ("Jerrican, plastic, non-removable head", string.Empty) },
        { "QN", ("Jerrican, plastic, removable head", string.Empty) },
        { "QP", ("Box, wooden, natural wood, ordinary", string.Empty) },
        { "QQ", ("Box, wooden, natural wood, with sift proof walls", string.Empty) },
        { "QR", ("Box, plastic, expanded", string.Empty) },
        { "QS", ("Box, plastic, solid", string.Empty) },
        { "RB1", ("A wheeled pallet with raised rim (GS1 Code). 81 x 67 x 135 cm (length x width x height).", "A wheeled pallet with raised rim for the storing and transporting of loads.Dimensions: 81 x 67 x 135 cm (length x width x height).") },
        { "RB2", ("A Wheeled pallet with raised rim (GS1 Code). 81 x 72 x 135 cm (length x width x height).", "A wheeled pallet with raised rim for the storing and transporting of loads.Dimensions: 81 x 72 x 135 cm (length x width x height).") },
        { "RB3", ("Wheeled pallet with raised rim. 81 x 60 x 16 cm (length x width x height). (GS1 Code)", "A wheeled pallet with raised rim for the storing and transporting of loads. Dimensions: 81 x 60 x 16 cm (length x width x height).") },
        { "RCB", ("Two sided cage on wheels with fixing strap (GS1 Code) 900 x 770 x 1513 cm (length x width x height)", "A two sided cage mounted on wheels with fixing strap.Dimensions: 900 x 770 x 1513 cm (length x width x height).") },
        { "RD", ("Rod", string.Empty) },
        { "RG", ("Ring", string.Empty) },
        { "RJ", ("Rack, clothing hanger", string.Empty) },
        { "RK", ("Rack", string.Empty) },
        { "RL", ("Reel", "Cylindrical rotatory device with a rim at each end on which materials are wound.") },
        { "RO", ("Roll", string.Empty) },
        { "RT", ("Rednet", "Containment material made of red mesh netting for retaining articles (e.g. trees).") },
        { "RZ", ("Rods, in bundle/bunch/truss", string.Empty) },
        { "S1", ("GS1 SMART-Box Type “E”", "Standard reusable crate with dimensions 60 x 40 x 21,1 cm") },
        { "SA", ("Sack", string.Empty) },
        { "SB", ("Slab", string.Empty) },
        { "SC", ("Crate, shallow", string.Empty) },
        { "SD", ("Spindle", string.Empty) },
        { "SE", ("Sea-chest", string.Empty) },
        { "SEC", ("Article Surveillance (GS1 Code)", "Equipped with article surveillance.") },
        { "SH", ("Sachet", string.Empty) },
        { "SI", ("Skid", "A low movable platform or pallet to facilitate the handling and transport of goods.") },
        { "SK", ("Case, skeleton", string.Empty) },
        { "SL", ("Slipsheet", "Hard plastic sheeting primarily used as the base on which to stack goods to optimise the space within a container. May be used as an alternative to a palletized packaging.") },
        { "SM", ("Sheetmetal", string.Empty) },
        { "SO", ("Spool", "A packaging container used in the transport of such items as wire, cable, tape and yarn.") },
        { "SP", ("Sheet, plastic wrapping", string.Empty) },
        { "SS", ("Case, steel", string.Empty) },
        { "ST", ("Sheet", string.Empty) },
        { "STL", ("Stick (GS1 Code)", "A container for dispensing solid substances, e.g. glue, deodorant.") },
        { "SU", ("Suitcase", string.Empty) },
        { "SV", ("Envelope, steel", string.Empty) },
        { "SW", ("Shrinkwrapped", "Goods retained in a transparent plastic film that has been wrapped around and then shrunk tightly on to the goods.") },
        { "SX", ("Set", string.Empty) },
        { "SY", ("Sleeve", "GS1 Description: A non-rigid container made of paper, cardboard or plastic that is open-ended and is slid over the contents for protection or presentation.") },
        { "SZ", ("Sheets, in bundle/bunch/truss", string.Empty) },
        { "T1", ("Tablet", "A loose or unpacked article in the form of a bar, block or piece. GS1 Description: A small rectangular package of aluminium foil or paper, e.g. a tablet of chocolate.") },
        { "TB", ("Tub", string.Empty) },
        { "TC", ("Tea-chest", string.Empty) },
        { "TD", ("Tube, collapsible", string.Empty) },
        { "TE", ("Tyre", "A ring made of rubber and/or metal surrounding a wheel.") },
        { "TEV", ("Tamper evident package (GS1 Code)", "A type of package giving easy or immediate recognition that the package has been tampered with after it has been sealed.") },
        { "TG", ("Tank container, generic", "A specially constructed container for transporting liquids and gases in bulk.") },
        { "THE", ("Three pack (GS1 Code)", "A package containing three products.") },
        { "TI", ("Tierce", string.Empty) },
        { "TK", ("Tank, rectangular", string.Empty) },
        { "TL", ("Tub, with lid", string.Empty) },
        { "TN", ("Tin", string.Empty) },
        { "TO", ("Tun", string.Empty) },
        { "TR", ("Trunk", string.Empty) },
        { "TRE", ("Trolley (GS1 Code)", "A low cart for the transportation and storage of groceries, milk, etc.") },
        { "TS", ("Truss", string.Empty) },
        { "TT", ("Bag, tote", "A capacious bag or basket.") },
        { "TTE", ("Tube, standing (GS1 Code)", "A screw-topped pliable cylinder capable of standing and suitable for holding pastes or semi-liquids, e.g. a tube of toothpaste.") },
        { "TU", ("Tube", string.Empty) },
        { "TV", ("Tube, with nozzle", "A tube made of plastic, metal or cardboard fitted with a nozzle, containing a liquid or semi-liquid product, e.g. silicon.") },
        { "TW", ("Pallet, triwall", "A lightweight pallet made from heavy duty corrugated board.") },
        { "TWE", ("Two pack (GS1 Code)", "A package containing two products") },
        { "TY", ("Tank, cylindrical", string.Empty) },
        { "TZ", ("Tubes, in bundle/bunch/truss", string.Empty) },
        { "UC", ("Uncaged", string.Empty) },
        { "UN", ("Unit", "A type of package composed of a single item or object, not otherwise specified as a unit of transport equipment.") },
        { "UUE", ("Tube net (GS1 Code)", "A plastic or textile tube suitable for carrying loose products, e.g. fruit.") },
        { "VA", ("Vat", string.Empty) },
        { "VG", ("Bulk, gas (at 1031 mbar and 15°C)", string.Empty) },
        { "VI", ("Vial", string.Empty) },
        { "VK", ("Vanpack", "A type of wooden crate.") },
        { "VL", ("Bulk, liquid", string.Empty) },
        { "VN", ("Vehicle", "A self-propelled means of conveyance.") },
        { "VO", ("Bulk, solid, large particles (“nodules”)", string.Empty) },
        { "VP", ("Vacuum-packed", string.Empty) },
        { "VQ", ("Bulk, liquefied gas (at abnormal temperature/pressure)", string.Empty) },
        { "VR", ("Bulk, solid, granular particles (“grains”)", string.Empty) },
        { "VS", ("Bulk, scrap metal", "Loose or unpacked scrap metal transported in bulk form.") },
        { "VT", ("Vial", string.Empty) },
        { "VY", ("Bulk, solid, fine particles (“powders”)", string.Empty) },
        { "WA", ("Intermediate bulk container", "A reusable container made of metal, plastic, textile, wood or composite materials used to facilitate transportation of bulk solids and liquids in manageable volumes.") },
        { "WB", ("Wickerbottle", string.Empty) },
        { "WC", ("Intermediate bulk container, steel", string.Empty) },
        { "WD", ("Intermediate bulk container, aluminium", string.Empty) },
        { "WF", ("Intermediate bulk container, metal", string.Empty) },
        { "WG", ("Intermediate bulk container, steel, pressurised > 10 kpa", string.Empty) },
        { "WH", ("Intermediate bulk container, aluminium, pressurised > 10 kpa", string.Empty) },
        { "WJ", ("Intermediate bulk container, metal, pressure 10 kpa", string.Empty) },
        { "WK", ("Intermediate bulk container, steel, liquid", string.Empty) },
        { "WL", ("Intermediate bulk container, aluminium, liquid", string.Empty) },
        { "WM", ("Intermediate bulk container, metal, liquid", string.Empty) },
        { "WN", ("Intermediate bulk container, woven plastic, without coat/liner", string.Empty) },
        { "WP", ("Intermediate bulk container, woven plastic, coated", string.Empty) },
        { "WQ", ("Intermediate bulk container, woven plastic, with liner", string.Empty) },
        { "WR", ("Intermediate bulk container, woven plastic, coated and liner", string.Empty) },
        { "WRP", ("Wrapper (GS1 Code)", "Wrapping e.g. for an individually packed ice cream.") },
        { "WS", ("Intermediate bulk container, plastic film", string.Empty) },
        { "WT", ("Intermediate bulk container, textile without coat/liner", string.Empty) },
        { "WU", ("Intermediate bulk container, natural wood, with inner liner", string.Empty) },
        { "WV", ("Intermediate bulk container, textile, coated", string.Empty) },
        { "WW", ("Intermediate bulk container, textile, with liner", string.Empty) },
        { "WX", ("Intermediate bulk container, textile, coated and liner", string.Empty) },
        { "WY", ("Intermediate bulk container, plywood, with inner liner", string.Empty) },
        { "WZ", ("Intermediate bulk container, reconstituted wood, with inner liner", string.Empty) },
        { "X11", ("Banded package (GS1 Code)", "A package with bands, usually metal or nylon, round it to hold the products together.") },
        { "X12", ("Cardboard package with grip holes for bottles (GS1 Code)", "Cardboard package with a number of holes. Each hole is to be gripped tightly around the neck of a bottle.") },
        { "X15", ("Oneway pallet ISO 0 - 1/2 EURO Pallet (GS1 temporary Code)", "Oneway pallet with dimensions 80 X 60 cm.") },
        { "X16", ("Oneway pallet ISO 1 - 1/1 EURO Pallet (GS1 temporary Code)", "Oneway pallet with dimensions 80 X 120 cm.") },
        { "X17", ("Oneway pallet ISO 2 - 2/1 EURO Pallet (GS1 temporary Code)", "Oneway pallet with dimensions 100 X 120 cm.") },
        { "X18", ("Pallet with exceptional dimensions (GS1 temporary Code)", "Pallet with non-standard dimensions") },
        { "X19", ("Parcel with exceptional dimensions (GS1 temporary Code)", "Parcel with non-standard dimensions") },
        { "X20", ("Wooden pallet (120x120 cm) (GS1 temporary code)", "Reusable wooden pallet with dimensions 120x120 cm.") },
        { "X3", ("Standard stack of stones (GS1 Code)", "Standard stack of stones.") },
        { "XA", ("Bag, woven plastic, without inner coat/liner", string.Empty) },
        { "XB", ("Bag, woven plastic, sift proof", string.Empty) },
        { "XC", ("Bag, woven plastic, water resistant", string.Empty) },
        { "XD", ("Bag, plastics film", string.Empty) },
        { "XF", ("Bag, textile, without inner coat/liner", string.Empty) },
        { "XG", ("Bag, textile, sift proof", string.Empty) },
        { "XH", ("Bag, textile, water resistant", string.Empty) },
        { "XJ", ("Bag, paper, multi-wall", string.Empty) },
        { "XK", ("Bag, paper, multi-wall, water resistant", string.Empty) },
        { "YA", ("Composite packaging, plastic receptacle in steel drum", string.Empty) },
        { "YB", ("Composite packaging, plastic receptacle in steel crate box", string.Empty) },
        { "YC", ("Composite packaging, plastic receptacle in aluminium drum", string.Empty) },
        { "YD", ("Composite packaging, plastic receptacle in aluminium crate", string.Empty) },
        { "YF", ("Composite packaging, plastic receptacle in wooden box", string.Empty) },
        { "YG", ("Composite packaging, plastic receptacle in plywood drum", string.Empty) },
        { "YH", ("Composite packaging, plastic receptacle in plywood box", string.Empty) },
        { "YJ", ("Composite packaging, plastic receptacle in fibre drum", string.Empty) },
        { "YK", ("Composite packaging, plastic receptacle in fibreboard box", string.Empty) },
        { "YL", ("Composite packaging, plastic receptacle in plastic drum", string.Empty) },
        { "YM", ("Composite packaging, plastic receptacle in solid plastic box", string.Empty) },
        { "YN", ("Composite packaging, glass receptacle in steel drum", string.Empty) },
        { "YP", ("Composite packaging, glass receptacle in steel crate box", string.Empty) },
        { "YQ", ("Composite packaging, glass receptacle in aluminium drum", string.Empty) },
        { "YR", ("Composite packaging, glass receptacle in aluminium crate", string.Empty) },
        { "YS", ("Composite packaging, glass receptacle in wooden box", string.Empty) },
        { "YT", ("Composite packaging, glass receptacle in plywood drum", string.Empty) },
        { "YV", ("Composite packaging, glass receptacle in wickerwork hamper", string.Empty) },
        { "YW", ("Composite packaging, glass receptacle in fibre drum", string.Empty) },
        { "YX", ("Composite packaging, glass receptacle in fibreboard box", string.Empty) },
        { "YY", ("Composite packaging, glass receptacle in expandable plastic pack", string.Empty) },
        { "YZ", ("Composite packaging, glass receptacle in solid plastic pack", string.Empty) },
        { "ZA", ("Intermediate bulk container, paper, multi-wall", string.Empty) },
        { "ZB", ("Bag, large", "GS1 Description: A non-rigid container made of fabric, paper, plastic, etc, with an opening at the top which can be closed and which is suitable for use on pallets.") },
        { "ZC", ("Intermediate bulk container, paper, multi-wall, water resistant", string.Empty) },
        { "ZD", ("Intermediate bulk container, rigid plastic, with structural equipment, solids", string.Empty) },
        { "ZF", ("Intermediate bulk container, rigid plastic, freestanding, solids", string.Empty) },
        { "ZG", ("Intermediate bulk container, rigid plastic, with structural equipment, pressurised", string.Empty) },
        { "ZH", ("Intermediate bulk container, rigid plastic, freestanding, pressurised", string.Empty) },
        { "ZJ", ("Intermediate bulk container, rigid plastic, with structural equipment, liquids", string.Empty) },
        { "ZK", ("Intermediate bulk container, rigid plastic, freestanding, liquids", string.Empty) },
        { "ZL", ("Intermediate bulk container, composite, rigid plastic, solids", string.Empty) },
        { "ZM", ("Intermediate bulk container, composite, flexible plastic, solids", string.Empty) },
        { "ZN", ("Intermediate bulk container, composite, rigid plastic, pressurised", string.Empty) },
        { "ZP", ("Intermediate bulk container, composite, flexible plastic, pressurised", string.Empty) },
        { "ZQ", ("Intermediate bulk container, composite, rigid plastic, liquids", string.Empty) },
        { "ZR", ("Intermediate bulk container, composite, flexible plastic, liquids", string.Empty) },
        { "ZS", ("Intermediate bulk container, composite", string.Empty) },
        { "ZT", ("Intermediate bulk container, fibreboard", string.Empty) },
        { "ZU", ("Intermediate bulk container, flexible", string.Empty) },
        { "ZV", ("Intermediate bulk container, metal, other than steel", string.Empty) },
        { "ZW", ("Intermediate bulk container, natural wood", string.Empty) },
        { "ZX", ("Intermediate bulk container, plywood", string.Empty) },
        { "ZY", ("Intermediate bulk container, reconstituted wood", string.Empty) },
        { "ZZ", ("Mutually defined", string.Empty) },

    };

    /// <summary>
    ///     Validate data against the descriptor.
    /// </summary>
    /// <param name="value">The GS1 identifier to be validated.</param>
    /// <param name="validationErrors">A list of validation errors.</param>
    /// <returns>True, if valid.  Otherwise, false.</returns>
    /// <remarks>The exception is marked as a warning, rather than an error,
    /// due to a degree of uncertainty regarding the standard.  See remarks
    /// for the <see cref="UnCefactFreightUnitTypeDescriptor"/> class.</remarks>
    // ReSharper disable once CommentTypo
    // ReSharper disable once InheritdocConsiderUsage
#if NET7_0_OR_GREATER
    public override bool IsValid(ReadOnlySpan<char> value, out IList<ParserException>? validationErrors) {
        if (!_freightUnitTypes.ContainsKey(value.ToString())) {
            validationErrors = [
                new (
                    2017,
                    string.Format(CultureInfo.CurrentCulture, Resources.GS1_Warning_001, value.ToString()),
                    false),
            ];

            return false;
        }

        validationErrors = null;
        return true;
    }
#else
    public override bool IsValid(string value, out IList<ParserException>? validationErrors) {
        if (!_freightUnitTypes.ContainsKey(value)) {
            validationErrors = [
                new (
                    2017,
                    string.Format(CultureInfo.CurrentCulture, Resources.GS1_Warning_001, value),
                    false),
            ];

            return false;
        }

        validationErrors = null;
        return true;
    }
#endif
}