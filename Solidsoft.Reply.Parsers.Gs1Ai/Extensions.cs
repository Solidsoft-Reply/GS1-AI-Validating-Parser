// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Solidsoft Reply Ltd.">
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
// Extension methods.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable BadListLineBreaks

namespace Solidsoft.Reply.Parsers.Gs1Ai;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary>
///     Extension methods.
/// </summary>
public static partial class Extensions {

    /// <summary>
    ///   UPC-A Compatible regular expression.
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex("^\\d*$")]
    private static partial Regex UpcACompatibleRegex();

    /// <summary>
    ///   UPC-A Compatible UnitedStates and Canada regular expression.
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex("^\\d((0000[1-9])|(000[1-9])|(0[01][0-9]))\\d*$")]
    private static partial Regex UpcaCompatibleUnitedStatesAndCanadaRegex();

    /// <summary>
    ///     A dictionary of GS1 company prefixes.
    /// </summary>
    private static readonly Dictionary<int, CountryCode> CompanyPrefixes =
        new()
        {
            { 100, CountryCode.UnitedStates },
            { 101, CountryCode.UnitedStates },
            { 102, CountryCode.UnitedStates },
            { 103, CountryCode.UnitedStates },
            { 104, CountryCode.UnitedStates },
            { 105, CountryCode.UnitedStates },
            { 106, CountryCode.UnitedStates },
            { 107, CountryCode.UnitedStates },
            { 108, CountryCode.UnitedStates },
            { 109, CountryCode.UnitedStates },
            { 110, CountryCode.UnitedStates },
            { 111, CountryCode.UnitedStates },
            { 112, CountryCode.UnitedStates },
            { 113, CountryCode.UnitedStates },
            { 114, CountryCode.UnitedStates },
            { 115, CountryCode.UnitedStates },
            { 116, CountryCode.UnitedStates },
            { 117, CountryCode.UnitedStates },
            { 118, CountryCode.UnitedStates },
            { 119, CountryCode.UnitedStates },
            { 120, CountryCode.UnitedStates },
            { 121, CountryCode.UnitedStates },
            { 122, CountryCode.UnitedStates },
            { 123, CountryCode.UnitedStates },
            { 124, CountryCode.UnitedStates },
            { 125, CountryCode.UnitedStates },
            { 126, CountryCode.UnitedStates },
            { 127, CountryCode.UnitedStates },
            { 128, CountryCode.UnitedStates },
            { 129, CountryCode.UnitedStates },
            { 130, CountryCode.UnitedStates },
            { 131, CountryCode.UnitedStates },
            { 132, CountryCode.UnitedStates },
            { 133, CountryCode.UnitedStates },
            { 134, CountryCode.UnitedStates },
            { 135, CountryCode.UnitedStates },
            { 136, CountryCode.UnitedStates },
            { 137, CountryCode.UnitedStates },
            { 138, CountryCode.UnitedStates },
            { 139, CountryCode.UnitedStates },
            { 200, CountryCode.RestrictedCirculation },
            { 201, CountryCode.RestrictedCirculation },
            { 202, CountryCode.RestrictedCirculation },
            { 203, CountryCode.RestrictedCirculation },
            { 204, CountryCode.RestrictedCirculation },
            { 205, CountryCode.RestrictedCirculation },
            { 206, CountryCode.RestrictedCirculation },
            { 207, CountryCode.RestrictedCirculation },
            { 208, CountryCode.RestrictedCirculation },
            { 209, CountryCode.RestrictedCirculation },
            { 210, CountryCode.RestrictedCirculation },
            { 211, CountryCode.RestrictedCirculation },
            { 212, CountryCode.RestrictedCirculation },
            { 213, CountryCode.RestrictedCirculation },
            { 214, CountryCode.RestrictedCirculation },
            { 215, CountryCode.RestrictedCirculation },
            { 216, CountryCode.RestrictedCirculation },
            { 217, CountryCode.RestrictedCirculation },
            { 218, CountryCode.RestrictedCirculation },
            { 219, CountryCode.RestrictedCirculation },
            { 220, CountryCode.RestrictedCirculation },
            { 221, CountryCode.RestrictedCirculation },
            { 222, CountryCode.RestrictedCirculation },
            { 223, CountryCode.RestrictedCirculation },
            { 224, CountryCode.RestrictedCirculation },
            { 225, CountryCode.RestrictedCirculation },
            { 226, CountryCode.RestrictedCirculation },
            { 227, CountryCode.RestrictedCirculation },
            { 228, CountryCode.RestrictedCirculation },
            { 229, CountryCode.RestrictedCirculation },
            { 230, CountryCode.RestrictedCirculation },
            { 231, CountryCode.RestrictedCirculation },
            { 232, CountryCode.RestrictedCirculation },
            { 233, CountryCode.RestrictedCirculation },
            { 234, CountryCode.RestrictedCirculation },
            { 235, CountryCode.RestrictedCirculation },
            { 236, CountryCode.RestrictedCirculation },
            { 237, CountryCode.RestrictedCirculation },
            { 238, CountryCode.RestrictedCirculation },
            { 239, CountryCode.RestrictedCirculation },
            { 240, CountryCode.RestrictedCirculation },
            { 241, CountryCode.RestrictedCirculation },
            { 242, CountryCode.RestrictedCirculation },
            { 243, CountryCode.RestrictedCirculation },
            { 244, CountryCode.RestrictedCirculation },
            { 245, CountryCode.RestrictedCirculation },
            { 246, CountryCode.RestrictedCirculation },
            { 247, CountryCode.RestrictedCirculation },
            { 248, CountryCode.RestrictedCirculation },
            { 249, CountryCode.RestrictedCirculation },
            { 250, CountryCode.RestrictedCirculation },
            { 251, CountryCode.RestrictedCirculation },
            { 252, CountryCode.RestrictedCirculation },
            { 253, CountryCode.RestrictedCirculation },
            { 254, CountryCode.RestrictedCirculation },
            { 255, CountryCode.RestrictedCirculation },
            { 256, CountryCode.RestrictedCirculation },
            { 257, CountryCode.RestrictedCirculation },
            { 258, CountryCode.RestrictedCirculation },
            { 259, CountryCode.RestrictedCirculation },
            { 260, CountryCode.RestrictedCirculation },
            { 261, CountryCode.RestrictedCirculation },
            { 262, CountryCode.RestrictedCirculation },
            { 263, CountryCode.RestrictedCirculation },
            { 264, CountryCode.RestrictedCirculation },
            { 265, CountryCode.RestrictedCirculation },
            { 266, CountryCode.RestrictedCirculation },
            { 267, CountryCode.RestrictedCirculation },
            { 268, CountryCode.RestrictedCirculation },
            { 269, CountryCode.RestrictedCirculation },
            { 270, CountryCode.RestrictedCirculation },
            { 271, CountryCode.RestrictedCirculation },
            { 272, CountryCode.RestrictedCirculation },
            { 273, CountryCode.RestrictedCirculation },
            { 274, CountryCode.RestrictedCirculation },
            { 275, CountryCode.RestrictedCirculation },
            { 276, CountryCode.RestrictedCirculation },
            { 277, CountryCode.RestrictedCirculation },
            { 278, CountryCode.RestrictedCirculation },
            { 279, CountryCode.RestrictedCirculation },
            { 280, CountryCode.RestrictedCirculation },
            { 281, CountryCode.RestrictedCirculation },
            { 282, CountryCode.RestrictedCirculation },
            { 283, CountryCode.RestrictedCirculation },
            { 284, CountryCode.RestrictedCirculation },
            { 285, CountryCode.RestrictedCirculation },
            { 286, CountryCode.RestrictedCirculation },
            { 287, CountryCode.RestrictedCirculation },
            { 288, CountryCode.RestrictedCirculation },
            { 289, CountryCode.RestrictedCirculation },
            { 290, CountryCode.RestrictedCirculation },
            { 291, CountryCode.RestrictedCirculation },
            { 292, CountryCode.RestrictedCirculation },
            { 293, CountryCode.RestrictedCirculation },
            { 294, CountryCode.RestrictedCirculation },
            { 295, CountryCode.RestrictedCirculation },
            { 296, CountryCode.RestrictedCirculation },
            { 297, CountryCode.RestrictedCirculation },
            { 298, CountryCode.RestrictedCirculation },
            { 299, CountryCode.RestrictedCirculation },
            { 300, CountryCode.FranceAndMonaco },
            { 301, CountryCode.FranceAndMonaco },
            { 302, CountryCode.FranceAndMonaco },
            { 303, CountryCode.FranceAndMonaco },
            { 304, CountryCode.FranceAndMonaco },
            { 305, CountryCode.FranceAndMonaco },
            { 306, CountryCode.FranceAndMonaco },
            { 307, CountryCode.FranceAndMonaco },
            { 308, CountryCode.FranceAndMonaco },
            { 309, CountryCode.FranceAndMonaco },
            { 310, CountryCode.FranceAndMonaco },
            { 311, CountryCode.FranceAndMonaco },
            { 312, CountryCode.FranceAndMonaco },
            { 313, CountryCode.FranceAndMonaco },
            { 314, CountryCode.FranceAndMonaco },
            { 315, CountryCode.FranceAndMonaco },
            { 316, CountryCode.FranceAndMonaco },
            { 317, CountryCode.FranceAndMonaco },
            { 318, CountryCode.FranceAndMonaco },
            { 319, CountryCode.FranceAndMonaco },
            { 320, CountryCode.FranceAndMonaco },
            { 321, CountryCode.FranceAndMonaco },
            { 322, CountryCode.FranceAndMonaco },
            { 323, CountryCode.FranceAndMonaco },
            { 324, CountryCode.FranceAndMonaco },
            { 325, CountryCode.FranceAndMonaco },
            { 326, CountryCode.FranceAndMonaco },
            { 327, CountryCode.FranceAndMonaco },
            { 328, CountryCode.FranceAndMonaco },
            { 329, CountryCode.FranceAndMonaco },
            { 330, CountryCode.FranceAndMonaco },
            { 331, CountryCode.FranceAndMonaco },
            { 332, CountryCode.FranceAndMonaco },
            { 333, CountryCode.FranceAndMonaco },
            { 334, CountryCode.FranceAndMonaco },
            { 335, CountryCode.FranceAndMonaco },
            { 336, CountryCode.FranceAndMonaco },
            { 337, CountryCode.FranceAndMonaco },
            { 338, CountryCode.FranceAndMonaco },
            { 339, CountryCode.FranceAndMonaco },
            { 340, CountryCode.FranceAndMonaco },
            { 341, CountryCode.FranceAndMonaco },
            { 342, CountryCode.FranceAndMonaco },
            { 343, CountryCode.FranceAndMonaco },
            { 344, CountryCode.FranceAndMonaco },
            { 345, CountryCode.FranceAndMonaco },
            { 346, CountryCode.FranceAndMonaco },
            { 347, CountryCode.FranceAndMonaco },
            { 348, CountryCode.FranceAndMonaco },
            { 349, CountryCode.FranceAndMonaco },
            { 350, CountryCode.FranceAndMonaco },
            { 351, CountryCode.FranceAndMonaco },
            { 352, CountryCode.FranceAndMonaco },
            { 353, CountryCode.FranceAndMonaco },
            { 354, CountryCode.FranceAndMonaco },
            { 355, CountryCode.FranceAndMonaco },
            { 356, CountryCode.FranceAndMonaco },
            { 357, CountryCode.FranceAndMonaco },
            { 358, CountryCode.FranceAndMonaco },
            { 359, CountryCode.FranceAndMonaco },
            { 360, CountryCode.FranceAndMonaco },
            { 361, CountryCode.FranceAndMonaco },
            { 362, CountryCode.FranceAndMonaco },
            { 363, CountryCode.FranceAndMonaco },
            { 364, CountryCode.FranceAndMonaco },
            { 365, CountryCode.FranceAndMonaco },
            { 366, CountryCode.FranceAndMonaco },
            { 367, CountryCode.FranceAndMonaco },
            { 368, CountryCode.FranceAndMonaco },
            { 369, CountryCode.FranceAndMonaco },
            { 370, CountryCode.FranceAndMonaco },
            { 371, CountryCode.FranceAndMonaco },
            { 372, CountryCode.FranceAndMonaco },
            { 373, CountryCode.FranceAndMonaco },
            { 374, CountryCode.FranceAndMonaco },
            { 375, CountryCode.FranceAndMonaco },
            { 376, CountryCode.FranceAndMonaco },
            { 377, CountryCode.FranceAndMonaco },
            { 378, CountryCode.FranceAndMonaco },
            { 379, CountryCode.FranceAndMonaco },
            { 380, CountryCode.Bulgaria },
            { 383, CountryCode.Slovenia },
            { 385, CountryCode.Croatia },
            { 387, CountryCode.BosniaAndHerzegovina },
            { 389, CountryCode.Montenegro },
            { 400, CountryCode.Germany },
            { 401, CountryCode.Germany },
            { 402, CountryCode.Germany },
            { 403, CountryCode.Germany },
            { 404, CountryCode.Germany },
            { 405, CountryCode.Germany },
            { 406, CountryCode.Germany },
            { 407, CountryCode.Germany },
            { 408, CountryCode.Germany },
            { 409, CountryCode.Germany },
            { 410, CountryCode.Germany },
            { 411, CountryCode.Germany },
            { 412, CountryCode.Germany },
            { 413, CountryCode.Germany },
            { 414, CountryCode.Germany },
            { 415, CountryCode.Germany },
            { 416, CountryCode.Germany },
            { 417, CountryCode.Germany },
            { 418, CountryCode.Germany },
            { 419, CountryCode.Germany },
            { 420, CountryCode.Germany },
            { 421, CountryCode.Germany },
            { 422, CountryCode.Germany },
            { 423, CountryCode.Germany },
            { 424, CountryCode.Germany },
            { 425, CountryCode.Germany },
            { 426, CountryCode.Germany },
            { 427, CountryCode.Germany },
            { 428, CountryCode.Germany },
            { 429, CountryCode.Germany },
            { 430, CountryCode.Germany },
            { 431, CountryCode.Germany },
            { 432, CountryCode.Germany },
            { 433, CountryCode.Germany },
            { 434, CountryCode.Germany },
            { 435, CountryCode.Germany },
            { 436, CountryCode.Germany },
            { 437, CountryCode.Germany },
            { 438, CountryCode.Germany },
            { 439, CountryCode.Germany },
            { 440, CountryCode.Germany },
            { 450, CountryCode.Japan },
            { 451, CountryCode.Japan },
            { 452, CountryCode.Japan },
            { 453, CountryCode.Japan },
            { 454, CountryCode.Japan },
            { 455, CountryCode.Japan },
            { 456, CountryCode.Japan },
            { 457, CountryCode.Japan },
            { 458, CountryCode.Japan },
            { 459, CountryCode.Japan },
            { 460, CountryCode.Russia },
            { 461, CountryCode.Russia },
            { 462, CountryCode.Russia },
            { 463, CountryCode.Russia },
            { 464, CountryCode.Russia },
            { 465, CountryCode.Russia },
            { 466, CountryCode.Russia },
            { 467, CountryCode.Russia },
            { 468, CountryCode.Russia },
            { 469, CountryCode.Russia },
            { 470, CountryCode.Kyrgyzstan },
            { 471, CountryCode.Taiwan },
            { 474, CountryCode.Estonia },
            { 475, CountryCode.Latvia },
            { 476, CountryCode.Azerbaijan },
            { 477, CountryCode.Lithuania },
            { 478, CountryCode.Uzbekistan },
            { 479, CountryCode.SriLanka },
            { 480, CountryCode.Philippines },
            { 481, CountryCode.Belarus },
            { 482, CountryCode.Ukraine },
            { 483, CountryCode.Turkmenistan },
            { 484, CountryCode.Moldova },
            { 485, CountryCode.Armenia },
            { 486, CountryCode.Georgia },
            { 487, CountryCode.Kazakhstan },
            { 488, CountryCode.Tajikistan },
            { 489, CountryCode.HongKong },
            { 490, CountryCode.JapanOriginalJan },
            { 491, CountryCode.JapanOriginalJan },
            { 492, CountryCode.JapanOriginalJan },
            { 493, CountryCode.JapanOriginalJan },
            { 494, CountryCode.JapanOriginalJan },
            { 495, CountryCode.JapanOriginalJan },
            { 496, CountryCode.JapanOriginalJan },
            { 497, CountryCode.JapanOriginalJan },
            { 498, CountryCode.JapanOriginalJan },
            { 499, CountryCode.JapanOriginalJan },
            { 500, CountryCode.UnitedKingdom },
            { 501, CountryCode.UnitedKingdom },
            { 502, CountryCode.UnitedKingdom },
            { 503, CountryCode.UnitedKingdom },
            { 504, CountryCode.UnitedKingdom },
            { 505, CountryCode.UnitedKingdom },
            { 506, CountryCode.UnitedKingdom },
            { 507, CountryCode.UnitedKingdom },
            { 508, CountryCode.UnitedKingdom },
            { 509, CountryCode.UnitedKingdom },
            { 520, CountryCode.Greece },
            { 521, CountryCode.Greece },
            { 528, CountryCode.Lebanon },
            { 529, CountryCode.Cyprus },
            { 530, CountryCode.Albania },
            { 531, CountryCode.NorthMacedonia },
            { 535, CountryCode.Malta },
            { 539, CountryCode.Ireland },
            { 540, CountryCode.BelgiumAndLuxembourg },
            { 541, CountryCode.BelgiumAndLuxembourg },
            { 542, CountryCode.BelgiumAndLuxembourg },
            { 543, CountryCode.BelgiumAndLuxembourg },
            { 544, CountryCode.BelgiumAndLuxembourg },
            { 545, CountryCode.BelgiumAndLuxembourg },
            { 546, CountryCode.BelgiumAndLuxembourg },
            { 547, CountryCode.BelgiumAndLuxembourg },
            { 548, CountryCode.BelgiumAndLuxembourg },
            { 549, CountryCode.BelgiumAndLuxembourg },
            { 560, CountryCode.Portugal },
            { 569, CountryCode.Iceland },
            { 570, CountryCode.DenmarkFaroeIslandsAndGreenland },
            { 571, CountryCode.DenmarkFaroeIslandsAndGreenland },
            { 572, CountryCode.DenmarkFaroeIslandsAndGreenland },
            { 573, CountryCode.DenmarkFaroeIslandsAndGreenland },
            { 574, CountryCode.DenmarkFaroeIslandsAndGreenland },
            { 575, CountryCode.DenmarkFaroeIslandsAndGreenland },
            { 576, CountryCode.DenmarkFaroeIslandsAndGreenland },
            { 577, CountryCode.DenmarkFaroeIslandsAndGreenland },
            { 578, CountryCode.DenmarkFaroeIslandsAndGreenland },
            { 579, CountryCode.DenmarkFaroeIslandsAndGreenland },
            { 590, CountryCode.Poland },
            { 594, CountryCode.Romania },
            { 599, CountryCode.Hungary },
            { 600, CountryCode.SouthAfrica },
            { 601, CountryCode.SouthAfrica },
            { 603, CountryCode.Ghana },
            { 604, CountryCode.Senegal },
            { 608, CountryCode.Bahrain },
            { 609, CountryCode.Mauritius },
            { 611, CountryCode.Morocco },
            { 613, CountryCode.Algeria },
            { 615, CountryCode.Nigeria },
            { 616, CountryCode.Kenya },
            { 618, CountryCode.IvoryCoast },
            { 619, CountryCode.Tunisia },
            { 620, CountryCode.Tanzania },
            { 621, CountryCode.Syria },
            { 622, CountryCode.Egypt },
            { 623, CountryCode.Brunei },
            { 624, CountryCode.Libya },
            { 625, CountryCode.Jordan },
            { 626, CountryCode.Iran },
            { 627, CountryCode.Kuwait },
            { 628, CountryCode.SaudiArabia },
            { 629, CountryCode.UnitedArabEmirates },
            { 640, CountryCode.Finland },
            { 641, CountryCode.Finland },
            { 642, CountryCode.Finland },
            { 643, CountryCode.Finland },
            { 644, CountryCode.Finland },
            { 645, CountryCode.Finland },
            { 646, CountryCode.Finland },
            { 647, CountryCode.Finland },
            { 648, CountryCode.Finland },
            { 649, CountryCode.Finland },
            { 690, CountryCode.PeoplesRepublicOfChina },
            { 691, CountryCode.PeoplesRepublicOfChina },
            { 692, CountryCode.PeoplesRepublicOfChina },
            { 693, CountryCode.PeoplesRepublicOfChina },
            { 694, CountryCode.PeoplesRepublicOfChina },
            { 695, CountryCode.PeoplesRepublicOfChina },
            { 696, CountryCode.PeoplesRepublicOfChina },
            { 697, CountryCode.PeoplesRepublicOfChina },
            { 698, CountryCode.PeoplesRepublicOfChina },
            { 699, CountryCode.PeoplesRepublicOfChina },
            { 700, CountryCode.Norway },
            { 701, CountryCode.Norway },
            { 702, CountryCode.Norway },
            { 703, CountryCode.Norway },
            { 704, CountryCode.Norway },
            { 705, CountryCode.Norway },
            { 706, CountryCode.Norway },
            { 707, CountryCode.Norway },
            { 708, CountryCode.Norway },
            { 709, CountryCode.Norway },
            { 729, CountryCode.Israel },
            { 730, CountryCode.Sweden },
            { 731, CountryCode.Sweden },
            { 732, CountryCode.Sweden },
            { 733, CountryCode.Sweden },
            { 734, CountryCode.Sweden },
            { 735, CountryCode.Sweden },
            { 736, CountryCode.Sweden },
            { 737, CountryCode.Sweden },
            { 738, CountryCode.Sweden },
            { 739, CountryCode.Sweden },
            { 740, CountryCode.Guatemala },
            { 741, CountryCode.ElSalvador },
            { 742, CountryCode.Honduras },
            { 743, CountryCode.Nicaragua },
            { 744, CountryCode.CostaRica },
            { 745, CountryCode.Panama },
            { 746, CountryCode.DominicanRepublic },
            { 750, CountryCode.Mexico },
            { 754, CountryCode.Canada },
            { 755, CountryCode.Canada },
            { 759, CountryCode.Venezuela },
            { 760, CountryCode.SwitzerlandAndLiechtenstein },
            { 761, CountryCode.SwitzerlandAndLiechtenstein },
            { 762, CountryCode.SwitzerlandAndLiechtenstein },
            { 763, CountryCode.SwitzerlandAndLiechtenstein },
            { 764, CountryCode.SwitzerlandAndLiechtenstein },
            { 765, CountryCode.SwitzerlandAndLiechtenstein },
            { 766, CountryCode.SwitzerlandAndLiechtenstein },
            { 767, CountryCode.SwitzerlandAndLiechtenstein },
            { 768, CountryCode.SwitzerlandAndLiechtenstein },
            { 769, CountryCode.SwitzerlandAndLiechtenstein },
            { 770, CountryCode.Colombia },
            { 771, CountryCode.Colombia },
            { 773, CountryCode.Uruguay },
            { 775, CountryCode.Peru },
            { 777, CountryCode.Bolivia },
            { 778, CountryCode.Argentina },
            { 779, CountryCode.Argentina },
            { 780, CountryCode.Chile },
            { 784, CountryCode.Paraguay },
            { 786, CountryCode.Ecuador },
            { 789, CountryCode.Brazil },
            { 790, CountryCode.Brazil },
            { 800, CountryCode.ItalySanMarinoAndVaticanCity },
            { 801, CountryCode.ItalySanMarinoAndVaticanCity },
            { 802, CountryCode.ItalySanMarinoAndVaticanCity },
            { 803, CountryCode.ItalySanMarinoAndVaticanCity },
            { 804, CountryCode.ItalySanMarinoAndVaticanCity },
            { 805, CountryCode.ItalySanMarinoAndVaticanCity },
            { 806, CountryCode.ItalySanMarinoAndVaticanCity },
            { 807, CountryCode.ItalySanMarinoAndVaticanCity },
            { 808, CountryCode.ItalySanMarinoAndVaticanCity },
            { 809, CountryCode.ItalySanMarinoAndVaticanCity },
            { 810, CountryCode.ItalySanMarinoAndVaticanCity },
            { 811, CountryCode.ItalySanMarinoAndVaticanCity },
            { 812, CountryCode.ItalySanMarinoAndVaticanCity },
            { 813, CountryCode.ItalySanMarinoAndVaticanCity },
            { 814, CountryCode.ItalySanMarinoAndVaticanCity },
            { 815, CountryCode.ItalySanMarinoAndVaticanCity },
            { 816, CountryCode.ItalySanMarinoAndVaticanCity },
            { 817, CountryCode.ItalySanMarinoAndVaticanCity },
            { 818, CountryCode.ItalySanMarinoAndVaticanCity },
            { 819, CountryCode.ItalySanMarinoAndVaticanCity },
            { 820, CountryCode.ItalySanMarinoAndVaticanCity },
            { 821, CountryCode.ItalySanMarinoAndVaticanCity },
            { 822, CountryCode.ItalySanMarinoAndVaticanCity },
            { 823, CountryCode.ItalySanMarinoAndVaticanCity },
            { 824, CountryCode.ItalySanMarinoAndVaticanCity },
            { 825, CountryCode.ItalySanMarinoAndVaticanCity },
            { 826, CountryCode.ItalySanMarinoAndVaticanCity },
            { 827, CountryCode.ItalySanMarinoAndVaticanCity },
            { 828, CountryCode.ItalySanMarinoAndVaticanCity },
            { 829, CountryCode.ItalySanMarinoAndVaticanCity },
            { 830, CountryCode.ItalySanMarinoAndVaticanCity },
            { 831, CountryCode.ItalySanMarinoAndVaticanCity },
            { 832, CountryCode.ItalySanMarinoAndVaticanCity },
            { 833, CountryCode.ItalySanMarinoAndVaticanCity },
            { 834, CountryCode.ItalySanMarinoAndVaticanCity },
            { 835, CountryCode.ItalySanMarinoAndVaticanCity },
            { 836, CountryCode.ItalySanMarinoAndVaticanCity },
            { 837, CountryCode.ItalySanMarinoAndVaticanCity },
            { 838, CountryCode.ItalySanMarinoAndVaticanCity },
            { 839, CountryCode.ItalySanMarinoAndVaticanCity },
            { 840, CountryCode.SpainAndAndorra },
            { 841, CountryCode.SpainAndAndorra },
            { 842, CountryCode.SpainAndAndorra },
            { 843, CountryCode.SpainAndAndorra },
            { 844, CountryCode.SpainAndAndorra },
            { 845, CountryCode.SpainAndAndorra },
            { 846, CountryCode.SpainAndAndorra },
            { 847, CountryCode.SpainAndAndorra },
            { 848, CountryCode.SpainAndAndorra },
            { 849, CountryCode.SpainAndAndorra },
            { 850, CountryCode.Cuba },
            { 858, CountryCode.Slovakia },
            { 859, CountryCode.CzechRepublic },
            { 860, CountryCode.Serbia },
            { 865, CountryCode.Mongolia },
            { 867, CountryCode.NorthKorea },
            { 868, CountryCode.Turkey },
            { 869, CountryCode.Turkey },
            { 870, CountryCode.Netherlands },
            { 871, CountryCode.Netherlands },
            { 872, CountryCode.Netherlands },
            { 873, CountryCode.Netherlands },
            { 874, CountryCode.Netherlands },
            { 875, CountryCode.Netherlands },
            { 876, CountryCode.Netherlands },
            { 877, CountryCode.Netherlands },
            { 878, CountryCode.Netherlands },
            { 879, CountryCode.Netherlands },
            { 880, CountryCode.SouthKorea },
            { 884, CountryCode.Cambodia },
            { 885, CountryCode.Thailand },
            { 888, CountryCode.Singapore },
            { 890, CountryCode.India },
            { 893, CountryCode.Vietnam },
            { 896, CountryCode.Pakistan },
            { 899, CountryCode.Indonesia },
            { 900, CountryCode.Austria },
            { 901, CountryCode.Austria },
            { 902, CountryCode.Austria },
            { 903, CountryCode.Austria },
            { 904, CountryCode.Austria },
            { 905, CountryCode.Austria },
            { 906, CountryCode.Austria },
            { 907, CountryCode.Austria },
            { 908, CountryCode.Austria },
            { 909, CountryCode.Austria },
            { 910, CountryCode.Austria },
            { 911, CountryCode.Austria },
            { 912, CountryCode.Austria },
            { 913, CountryCode.Austria },
            { 914, CountryCode.Austria },
            { 915, CountryCode.Austria },
            { 916, CountryCode.Austria },
            { 917, CountryCode.Austria },
            { 918, CountryCode.Austria },
            { 919, CountryCode.Austria },
            { 930, CountryCode.Australia },
            { 931, CountryCode.Australia },
            { 932, CountryCode.Australia },
            { 933, CountryCode.Australia },
            { 934, CountryCode.Australia },
            { 935, CountryCode.Australia },
            { 936, CountryCode.Australia },
            { 937, CountryCode.Australia },
            { 938, CountryCode.Australia },
            { 939, CountryCode.Australia },
            { 940, CountryCode.NewZealand },
            { 941, CountryCode.NewZealand },
            { 942, CountryCode.NewZealand },
            { 943, CountryCode.NewZealand },
            { 944, CountryCode.NewZealand },
            { 945, CountryCode.NewZealand },
            { 946, CountryCode.NewZealand },
            { 947, CountryCode.NewZealand },
            { 948, CountryCode.NewZealand },
            { 949, CountryCode.NewZealand },
            { 950, CountryCode.GlobalOffice },
            { 951, CountryCode.GeneralManagerNumber },
            { 955, CountryCode.Malaysia },
            { 958, CountryCode.Macau },
            { 960, CountryCode.UnitedKingdomOfficeGtin8Allocation },
            { 961, CountryCode.UnitedKingdomOfficeGtin8Allocation },
            { 962, CountryCode.GlobalOfficeGtin8Allocation },
            { 963, CountryCode.GlobalOfficeGtin8Allocation },
            { 964, CountryCode.GlobalOfficeGtin8Allocation },
            { 965, CountryCode.GlobalOfficeGtin8Allocation },
            { 966, CountryCode.GlobalOfficeGtin8Allocation },
            { 967, CountryCode.GlobalOfficeGtin8Allocation },
            { 968, CountryCode.GlobalOfficeGtin8Allocation },
            { 969, CountryCode.GlobalOfficeGtin8Allocation },
            { 977, CountryCode.SerialPublicationIssn },
            { 978, CountryCode.BooklandIsbn },
            { 979, CountryCode.BooklandIsbn },
            { 980, CountryCode.RefundReceipt },
            { 981, CountryCode.CouponIdentificationForCommonCurrencyArea },
            { 982, CountryCode.CouponIdentificationForCommonCurrencyArea },
            { 983, CountryCode.CouponIdentificationForCommonCurrencyArea },
            { 984, CountryCode.CouponIdentificationForCommonCurrencyArea },
            { 990, CountryCode.CouponIdentification },
            { 991, CountryCode.CouponIdentification },
            { 992, CountryCode.CouponIdentification },
            { 993, CountryCode.CouponIdentification },
            { 994, CountryCode.CouponIdentification },
            { 995, CountryCode.CouponIdentification },
            { 996, CountryCode.CouponIdentification },
            { 997, CountryCode.CouponIdentification },
            { 998, CountryCode.CouponIdentification },
            { 999, CountryCode.CouponIdentification }
        };

    /// <summary>
    ///     Converts the value of this instance to its equivalent string representation using culture-invariant format
    ///     information.
    /// </summary>
    /// <param name="thisCharacter">The character to be converted.</param>
    /// <returns>A culture-invariant string.</returns>
    internal static string ToInvariantString(this char thisCharacter) {
        return thisCharacter.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///     Converts the numeric value of this instance to its equivalent string representation using culture-invariant format
    ///     information.
    /// </summary>
    /// <param name="thisInteger">The integer to be converted.</param>
    /// <returns>A culture-invariant string representing the integer.</returns>
    internal static string ToInvariantString(this int thisInteger) {
        return thisInteger.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///     Converts the numeric value of this instance to its equivalent string representation using the specified format and
    ///     culture-invariant format information.
    /// </summary>
    /// <param name="thisInteger">The integer to be converted.</param>
    /// <param name="format">The string format to apply.</param>
    /// <returns>A culture-invariant string representing the integer.</returns>
    internal static string ToInvariantString(this int thisInteger, string format) {
        return thisInteger.ToString(format, CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///     Gets the description of an enumerated value.
    /// </summary>
    /// <param name="value">The Company Prefix value.</param>
    /// <returns>The description of the company prefix value.</returns>
    public static string GetCompanyPrefixDescription(this CountryCode value) {
        var type = typeof(CountryCode);
        var name = Enum.GetNames(type)
                       .Where(f => f.Equals(value.ToString(), StringComparison.Ordinal))
                       .Select(d => d)
                       .FirstOrDefault();

        if (name is null) {
            return string.Empty;
        }

        var field = type.GetField(name);
        var customAttribute = field?.GetCustomAttributes(typeof(LocalisedDescriptionAttribute), false);
        return customAttribute is { Length: > 0 } ? ((LocalisedDescriptionAttribute)customAttribute[0]).Description : name;
    }

    /// <summary>
    ///     Determines if a GS1 key has a correct checksum digit. Include the checksum in the
    ///     key value.
    /// </summary>
    /// <param name="key">The GS1 key.</param>
    /// <returns>True, if the product code contains a correct checksum; otherwise false.</returns>
    internal static bool Gs1ChecksumIsValid(this string key) {
        if (string.IsNullOrWhiteSpace(key)) {
            return false;
        }

        // Ensure that the string contains only integer values.
        if (key.Any(c => (int)char.GetNumericValue(c) == -1)) {
            return false;
        }

        // Test the checksum.
        var sum = key.Reverse().Select((c, i) => (int)char.GetNumericValue(c) * (i % 2 == 0 ? 1 : 3)).Sum();

        // ReSharper disable once ArrangeRedundantParentheses
        return (10 - sum % 10) % 10 == 0;
    }

    /// <summary>
    ///     Resolve a GTIN or NTIN to a GS1 country code.
    /// </summary>
    /// <param name="productCode">The product code.</param>
    /// <returns>A GS1 country code.</returns>
    public static CountryCode ResolveGtinNtinToGs1Country(this string productCode) {
        return string.IsNullOrWhiteSpace(productCode)
                   ? CountryCode.Unknown
                   : TestProductCodeLengthGt3();

        CountryCode TestSheetMusicSpecifier() =>
            int.TryParse(productCode[4..5], out var sheetMusicSpecifier)
                ? sheetMusicSpecifier switch {
                    0 => CountryCode.BooklandIsbnIsmn,
                    _ => CountryCode.BooklandIsbn
                }
                : CountryCode.Unknown;

        CountryCode TestCountryCode() =>
            int.TryParse(productCode[1..4], out var countryCode)
                ? countryCode switch {
                    <= 99 and >= 0 => ResolveToGs1UpcACompatible(productCode),
                    979 => TestSheetMusicSpecifier(),
                    _ => countryCode.ResolveToGs1Country()
                }
                : CountryCode.Unknown;

        CountryCode TestProductCodeLengthGt3() =>
            productCode.Length > 3
                ? TestCountryCode()
                : CountryCode.Unknown;
    }

    /// <summary>
    ///     Resolve an integer prefix to a GS1 country code.
    /// </summary>
    /// <param name="countryCode">The integer prefix representing the country code.</param>
    /// <returns>A GS1 country code.</returns>

    // ReSharper disable once UnusedMember.Global
    internal static CountryCode ResolveToGs1Country(this string countryCode) {
        return string.IsNullOrWhiteSpace(countryCode)
                   ? CountryCode.Unknown
                   : TestCountryCode();

        CountryCode TestCountryCode() =>
            int.TryParse(countryCode, out var countryCodeInt)
                ? countryCodeInt.ResolveToGs1Country()
                : CountryCode.Unknown;
    }

    /// <summary>
    ///     Resolve an integer prefix to a GS1 country code.
    /// </summary>
    /// <param name="countryCode">The integer prefix representing the country code.</param>
    /// <returns>A GS1 country code.</returns>

    // ReSharper disable once MemberCanBePrivate.Global
    internal static CountryCode ResolveToGs1Country(this int countryCode) {
        if (CompanyPrefixes.TryGetValue(countryCode, out var prefix)) {
            return prefix;
        }

        var countryCodeAsString = countryCode.ToInvariantString();

        return Enum.TryParse(countryCodeAsString, out CountryCode gs1Country)
                   ? gs1Country.ToString() switch {
                       var ccs when ccs == countryCodeAsString => CountryCode.Unknown,
                       _ => gs1Country
                   }
                   : CountryCode.Unknown;
    }

    /// <summary>
    ///     Resolve a product code to a UPC-A Compatible company prefix.
    /// </summary>
    /// <param name="productCode">The product code.</param>
    /// <returns>The UPC-A Compatible company prefix.</returns>
    private static CountryCode ResolveToGs1UpcACompatible(string productCode) {
        if (string.IsNullOrWhiteSpace(productCode) || !UpcACompatibleRegex().IsMatch(productCode)
                                                   || productCode.Length > 14) {
            return CountryCode.Unknown;
        }

        productCode = productCode.PadLeft(14);

        return GetCompanyPrefix();

        CountryCode TestForUpcACompatibleUnitedStatesDrugs() =>
            productCode[1..3] == "03"
                ? CountryCode.UpcACompatibleUnitedStatesDrugs
                : TestForUpcACompatibleUnitedStatesReserved();

        int GetCountryCode()
        {
            if (int.TryParse(productCode[1..4].Trim(), NumberStyles.None, CultureInfo.InvariantCulture,
                    out var countryCode))
                return countryCode;

            return (int)CountryCode.Unknown;
        }
        
        CountryCode GetCompanyPrefix() =>
            productCode[1..4] == "000"
                ? TestForFiveZeros()
                : GetCountryCode().ResolveToGs1Country();

        CountryCode TestForFiveZeros() =>
            productCode[1..6] == "00000"
                ? CountryCode.UpcACompatibleGtin8
                : TestForUpcACompatibleUnitedStatesAndCanada();

        CountryCode TestForUpcACompatibleUnitedStatesAndCanada() =>
            UpcaCompatibleUnitedStatesAndCanadaRegex().IsMatch(productCode) ||
            productCode[1..3] == "06"
                ? CountryCode.UpcACompatibleUnitedStatesAndCanada
                : TestForUpcACompatibleRestrictedCirculation();

        CountryCode TestForUpcACompatibleRestrictedCirculation() =>
            productCode[1..3] == "02" ||
            productCode[1..3] == "04"
                ? CountryCode.UpcACompatibleRestrictedCirculation
                : TestForUpcACompatibleUnitedStatesDrugs();

        CountryCode TestForUpcACompatibleUnitedStatesReserved() =>
            productCode[1..3] == "05"
                ? CountryCode.UpcACompatibleUnitedStatesReserved
                : int.Parse(
                        productCode[1..4],
                        NumberStyles.None,
                        CultureInfo.InvariantCulture)
                    .ResolveToGs1Country();
    }
}