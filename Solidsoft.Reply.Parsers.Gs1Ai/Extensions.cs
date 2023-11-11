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

using System.ComponentModel;

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
    private static readonly Dictionary<int, CompanyPrefix> CompanyPrefixes =
        new()
        {
            { 100, CompanyPrefix.UnitedStates },
            { 101, CompanyPrefix.UnitedStates },
            { 102, CompanyPrefix.UnitedStates },
            { 103, CompanyPrefix.UnitedStates },
            { 104, CompanyPrefix.UnitedStates },
            { 105, CompanyPrefix.UnitedStates },
            { 106, CompanyPrefix.UnitedStates },
            { 107, CompanyPrefix.UnitedStates },
            { 108, CompanyPrefix.UnitedStates },
            { 109, CompanyPrefix.UnitedStates },
            { 110, CompanyPrefix.UnitedStates },
            { 111, CompanyPrefix.UnitedStates },
            { 112, CompanyPrefix.UnitedStates },
            { 113, CompanyPrefix.UnitedStates },
            { 114, CompanyPrefix.UnitedStates },
            { 115, CompanyPrefix.UnitedStates },
            { 116, CompanyPrefix.UnitedStates },
            { 117, CompanyPrefix.UnitedStates },
            { 118, CompanyPrefix.UnitedStates },
            { 119, CompanyPrefix.UnitedStates },
            { 120, CompanyPrefix.UnitedStates },
            { 121, CompanyPrefix.UnitedStates },
            { 122, CompanyPrefix.UnitedStates },
            { 123, CompanyPrefix.UnitedStates },
            { 124, CompanyPrefix.UnitedStates },
            { 125, CompanyPrefix.UnitedStates },
            { 126, CompanyPrefix.UnitedStates },
            { 127, CompanyPrefix.UnitedStates },
            { 128, CompanyPrefix.UnitedStates },
            { 129, CompanyPrefix.UnitedStates },
            { 130, CompanyPrefix.UnitedStates },
            { 131, CompanyPrefix.UnitedStates },
            { 132, CompanyPrefix.UnitedStates },
            { 133, CompanyPrefix.UnitedStates },
            { 134, CompanyPrefix.UnitedStates },
            { 135, CompanyPrefix.UnitedStates },
            { 136, CompanyPrefix.UnitedStates },
            { 137, CompanyPrefix.UnitedStates },
            { 138, CompanyPrefix.UnitedStates },
            { 139, CompanyPrefix.UnitedStates },
            { 200, CompanyPrefix.RestrictedCirculation },
            { 201, CompanyPrefix.RestrictedCirculation },
            { 202, CompanyPrefix.RestrictedCirculation },
            { 203, CompanyPrefix.RestrictedCirculation },
            { 204, CompanyPrefix.RestrictedCirculation },
            { 205, CompanyPrefix.RestrictedCirculation },
            { 206, CompanyPrefix.RestrictedCirculation },
            { 207, CompanyPrefix.RestrictedCirculation },
            { 208, CompanyPrefix.RestrictedCirculation },
            { 209, CompanyPrefix.RestrictedCirculation },
            { 210, CompanyPrefix.RestrictedCirculation },
            { 211, CompanyPrefix.RestrictedCirculation },
            { 212, CompanyPrefix.RestrictedCirculation },
            { 213, CompanyPrefix.RestrictedCirculation },
            { 214, CompanyPrefix.RestrictedCirculation },
            { 215, CompanyPrefix.RestrictedCirculation },
            { 216, CompanyPrefix.RestrictedCirculation },
            { 217, CompanyPrefix.RestrictedCirculation },
            { 218, CompanyPrefix.RestrictedCirculation },
            { 219, CompanyPrefix.RestrictedCirculation },
            { 220, CompanyPrefix.RestrictedCirculation },
            { 221, CompanyPrefix.RestrictedCirculation },
            { 222, CompanyPrefix.RestrictedCirculation },
            { 223, CompanyPrefix.RestrictedCirculation },
            { 224, CompanyPrefix.RestrictedCirculation },
            { 225, CompanyPrefix.RestrictedCirculation },
            { 226, CompanyPrefix.RestrictedCirculation },
            { 227, CompanyPrefix.RestrictedCirculation },
            { 228, CompanyPrefix.RestrictedCirculation },
            { 229, CompanyPrefix.RestrictedCirculation },
            { 230, CompanyPrefix.RestrictedCirculation },
            { 231, CompanyPrefix.RestrictedCirculation },
            { 232, CompanyPrefix.RestrictedCirculation },
            { 233, CompanyPrefix.RestrictedCirculation },
            { 234, CompanyPrefix.RestrictedCirculation },
            { 235, CompanyPrefix.RestrictedCirculation },
            { 236, CompanyPrefix.RestrictedCirculation },
            { 237, CompanyPrefix.RestrictedCirculation },
            { 238, CompanyPrefix.RestrictedCirculation },
            { 239, CompanyPrefix.RestrictedCirculation },
            { 240, CompanyPrefix.RestrictedCirculation },
            { 241, CompanyPrefix.RestrictedCirculation },
            { 242, CompanyPrefix.RestrictedCirculation },
            { 243, CompanyPrefix.RestrictedCirculation },
            { 244, CompanyPrefix.RestrictedCirculation },
            { 245, CompanyPrefix.RestrictedCirculation },
            { 246, CompanyPrefix.RestrictedCirculation },
            { 247, CompanyPrefix.RestrictedCirculation },
            { 248, CompanyPrefix.RestrictedCirculation },
            { 249, CompanyPrefix.RestrictedCirculation },
            { 250, CompanyPrefix.RestrictedCirculation },
            { 251, CompanyPrefix.RestrictedCirculation },
            { 252, CompanyPrefix.RestrictedCirculation },
            { 253, CompanyPrefix.RestrictedCirculation },
            { 254, CompanyPrefix.RestrictedCirculation },
            { 255, CompanyPrefix.RestrictedCirculation },
            { 256, CompanyPrefix.RestrictedCirculation },
            { 257, CompanyPrefix.RestrictedCirculation },
            { 258, CompanyPrefix.RestrictedCirculation },
            { 259, CompanyPrefix.RestrictedCirculation },
            { 260, CompanyPrefix.RestrictedCirculation },
            { 261, CompanyPrefix.RestrictedCirculation },
            { 262, CompanyPrefix.RestrictedCirculation },
            { 263, CompanyPrefix.RestrictedCirculation },
            { 264, CompanyPrefix.RestrictedCirculation },
            { 265, CompanyPrefix.RestrictedCirculation },
            { 266, CompanyPrefix.RestrictedCirculation },
            { 267, CompanyPrefix.RestrictedCirculation },
            { 268, CompanyPrefix.RestrictedCirculation },
            { 269, CompanyPrefix.RestrictedCirculation },
            { 270, CompanyPrefix.RestrictedCirculation },
            { 271, CompanyPrefix.RestrictedCirculation },
            { 272, CompanyPrefix.RestrictedCirculation },
            { 273, CompanyPrefix.RestrictedCirculation },
            { 274, CompanyPrefix.RestrictedCirculation },
            { 275, CompanyPrefix.RestrictedCirculation },
            { 276, CompanyPrefix.RestrictedCirculation },
            { 277, CompanyPrefix.RestrictedCirculation },
            { 278, CompanyPrefix.RestrictedCirculation },
            { 279, CompanyPrefix.RestrictedCirculation },
            { 280, CompanyPrefix.RestrictedCirculation },
            { 281, CompanyPrefix.RestrictedCirculation },
            { 282, CompanyPrefix.RestrictedCirculation },
            { 283, CompanyPrefix.RestrictedCirculation },
            { 284, CompanyPrefix.RestrictedCirculation },
            { 285, CompanyPrefix.RestrictedCirculation },
            { 286, CompanyPrefix.RestrictedCirculation },
            { 287, CompanyPrefix.RestrictedCirculation },
            { 288, CompanyPrefix.RestrictedCirculation },
            { 289, CompanyPrefix.RestrictedCirculation },
            { 290, CompanyPrefix.RestrictedCirculation },
            { 291, CompanyPrefix.RestrictedCirculation },
            { 292, CompanyPrefix.RestrictedCirculation },
            { 293, CompanyPrefix.RestrictedCirculation },
            { 294, CompanyPrefix.RestrictedCirculation },
            { 295, CompanyPrefix.RestrictedCirculation },
            { 296, CompanyPrefix.RestrictedCirculation },
            { 297, CompanyPrefix.RestrictedCirculation },
            { 298, CompanyPrefix.RestrictedCirculation },
            { 299, CompanyPrefix.RestrictedCirculation },
            { 300, CompanyPrefix.FranceAndMonaco },
            { 301, CompanyPrefix.FranceAndMonaco },
            { 302, CompanyPrefix.FranceAndMonaco },
            { 303, CompanyPrefix.FranceAndMonaco },
            { 304, CompanyPrefix.FranceAndMonaco },
            { 305, CompanyPrefix.FranceAndMonaco },
            { 306, CompanyPrefix.FranceAndMonaco },
            { 307, CompanyPrefix.FranceAndMonaco },
            { 308, CompanyPrefix.FranceAndMonaco },
            { 309, CompanyPrefix.FranceAndMonaco },
            { 310, CompanyPrefix.FranceAndMonaco },
            { 311, CompanyPrefix.FranceAndMonaco },
            { 312, CompanyPrefix.FranceAndMonaco },
            { 313, CompanyPrefix.FranceAndMonaco },
            { 314, CompanyPrefix.FranceAndMonaco },
            { 315, CompanyPrefix.FranceAndMonaco },
            { 316, CompanyPrefix.FranceAndMonaco },
            { 317, CompanyPrefix.FranceAndMonaco },
            { 318, CompanyPrefix.FranceAndMonaco },
            { 319, CompanyPrefix.FranceAndMonaco },
            { 320, CompanyPrefix.FranceAndMonaco },
            { 321, CompanyPrefix.FranceAndMonaco },
            { 322, CompanyPrefix.FranceAndMonaco },
            { 323, CompanyPrefix.FranceAndMonaco },
            { 324, CompanyPrefix.FranceAndMonaco },
            { 325, CompanyPrefix.FranceAndMonaco },
            { 326, CompanyPrefix.FranceAndMonaco },
            { 327, CompanyPrefix.FranceAndMonaco },
            { 328, CompanyPrefix.FranceAndMonaco },
            { 329, CompanyPrefix.FranceAndMonaco },
            { 330, CompanyPrefix.FranceAndMonaco },
            { 331, CompanyPrefix.FranceAndMonaco },
            { 332, CompanyPrefix.FranceAndMonaco },
            { 333, CompanyPrefix.FranceAndMonaco },
            { 334, CompanyPrefix.FranceAndMonaco },
            { 335, CompanyPrefix.FranceAndMonaco },
            { 336, CompanyPrefix.FranceAndMonaco },
            { 337, CompanyPrefix.FranceAndMonaco },
            { 338, CompanyPrefix.FranceAndMonaco },
            { 339, CompanyPrefix.FranceAndMonaco },
            { 340, CompanyPrefix.FranceAndMonaco },
            { 341, CompanyPrefix.FranceAndMonaco },
            { 342, CompanyPrefix.FranceAndMonaco },
            { 343, CompanyPrefix.FranceAndMonaco },
            { 344, CompanyPrefix.FranceAndMonaco },
            { 345, CompanyPrefix.FranceAndMonaco },
            { 346, CompanyPrefix.FranceAndMonaco },
            { 347, CompanyPrefix.FranceAndMonaco },
            { 348, CompanyPrefix.FranceAndMonaco },
            { 349, CompanyPrefix.FranceAndMonaco },
            { 350, CompanyPrefix.FranceAndMonaco },
            { 351, CompanyPrefix.FranceAndMonaco },
            { 352, CompanyPrefix.FranceAndMonaco },
            { 353, CompanyPrefix.FranceAndMonaco },
            { 354, CompanyPrefix.FranceAndMonaco },
            { 355, CompanyPrefix.FranceAndMonaco },
            { 356, CompanyPrefix.FranceAndMonaco },
            { 357, CompanyPrefix.FranceAndMonaco },
            { 358, CompanyPrefix.FranceAndMonaco },
            { 359, CompanyPrefix.FranceAndMonaco },
            { 360, CompanyPrefix.FranceAndMonaco },
            { 361, CompanyPrefix.FranceAndMonaco },
            { 362, CompanyPrefix.FranceAndMonaco },
            { 363, CompanyPrefix.FranceAndMonaco },
            { 364, CompanyPrefix.FranceAndMonaco },
            { 365, CompanyPrefix.FranceAndMonaco },
            { 366, CompanyPrefix.FranceAndMonaco },
            { 367, CompanyPrefix.FranceAndMonaco },
            { 368, CompanyPrefix.FranceAndMonaco },
            { 369, CompanyPrefix.FranceAndMonaco },
            { 370, CompanyPrefix.FranceAndMonaco },
            { 371, CompanyPrefix.FranceAndMonaco },
            { 372, CompanyPrefix.FranceAndMonaco },
            { 373, CompanyPrefix.FranceAndMonaco },
            { 374, CompanyPrefix.FranceAndMonaco },
            { 375, CompanyPrefix.FranceAndMonaco },
            { 376, CompanyPrefix.FranceAndMonaco },
            { 377, CompanyPrefix.FranceAndMonaco },
            { 378, CompanyPrefix.FranceAndMonaco },
            { 379, CompanyPrefix.FranceAndMonaco },
            { 380, CompanyPrefix.Bulgaria },
            { 383, CompanyPrefix.Slovenia },
            { 385, CompanyPrefix.Croatia },
            { 387, CompanyPrefix.BosniaAndHerzegovina },
            { 389, CompanyPrefix.Montenegro },
            { 400, CompanyPrefix.Germany },
            { 401, CompanyPrefix.Germany },
            { 402, CompanyPrefix.Germany },
            { 403, CompanyPrefix.Germany },
            { 404, CompanyPrefix.Germany },
            { 405, CompanyPrefix.Germany },
            { 406, CompanyPrefix.Germany },
            { 407, CompanyPrefix.Germany },
            { 408, CompanyPrefix.Germany },
            { 409, CompanyPrefix.Germany },
            { 410, CompanyPrefix.Germany },
            { 411, CompanyPrefix.Germany },
            { 412, CompanyPrefix.Germany },
            { 413, CompanyPrefix.Germany },
            { 414, CompanyPrefix.Germany },
            { 415, CompanyPrefix.Germany },
            { 416, CompanyPrefix.Germany },
            { 417, CompanyPrefix.Germany },
            { 418, CompanyPrefix.Germany },
            { 419, CompanyPrefix.Germany },
            { 420, CompanyPrefix.Germany },
            { 421, CompanyPrefix.Germany },
            { 422, CompanyPrefix.Germany },
            { 423, CompanyPrefix.Germany },
            { 424, CompanyPrefix.Germany },
            { 425, CompanyPrefix.Germany },
            { 426, CompanyPrefix.Germany },
            { 427, CompanyPrefix.Germany },
            { 428, CompanyPrefix.Germany },
            { 429, CompanyPrefix.Germany },
            { 430, CompanyPrefix.Germany },
            { 431, CompanyPrefix.Germany },
            { 432, CompanyPrefix.Germany },
            { 433, CompanyPrefix.Germany },
            { 434, CompanyPrefix.Germany },
            { 435, CompanyPrefix.Germany },
            { 436, CompanyPrefix.Germany },
            { 437, CompanyPrefix.Germany },
            { 438, CompanyPrefix.Germany },
            { 439, CompanyPrefix.Germany },
            { 440, CompanyPrefix.Germany },
            { 450, CompanyPrefix.Japan },
            { 451, CompanyPrefix.Japan },
            { 452, CompanyPrefix.Japan },
            { 453, CompanyPrefix.Japan },
            { 454, CompanyPrefix.Japan },
            { 455, CompanyPrefix.Japan },
            { 456, CompanyPrefix.Japan },
            { 457, CompanyPrefix.Japan },
            { 458, CompanyPrefix.Japan },
            { 459, CompanyPrefix.Japan },
            { 460, CompanyPrefix.Russia },
            { 461, CompanyPrefix.Russia },
            { 462, CompanyPrefix.Russia },
            { 463, CompanyPrefix.Russia },
            { 464, CompanyPrefix.Russia },
            { 465, CompanyPrefix.Russia },
            { 466, CompanyPrefix.Russia },
            { 467, CompanyPrefix.Russia },
            { 468, CompanyPrefix.Russia },
            { 469, CompanyPrefix.Russia },
            { 470, CompanyPrefix.Kyrgyzstan },
            { 471, CompanyPrefix.Taiwan },
            { 474, CompanyPrefix.Estonia },
            { 475, CompanyPrefix.Latvia },
            { 476, CompanyPrefix.Azerbaijan },
            { 477, CompanyPrefix.Lithuania },
            { 478, CompanyPrefix.Uzbekistan },
            { 479, CompanyPrefix.SriLanka },
            { 480, CompanyPrefix.Philippines },
            { 481, CompanyPrefix.Belarus },
            { 482, CompanyPrefix.Ukraine },
            { 483, CompanyPrefix.Turkmenistan },
            { 484, CompanyPrefix.Moldova },
            { 485, CompanyPrefix.Armenia },
            { 486, CompanyPrefix.Georgia },
            { 487, CompanyPrefix.Kazakhstan },
            { 488, CompanyPrefix.Tajikistan },
            { 489, CompanyPrefix.HongKong },
            { 490, CompanyPrefix.JapanOriginalJan },
            { 491, CompanyPrefix.JapanOriginalJan },
            { 492, CompanyPrefix.JapanOriginalJan },
            { 493, CompanyPrefix.JapanOriginalJan },
            { 494, CompanyPrefix.JapanOriginalJan },
            { 495, CompanyPrefix.JapanOriginalJan },
            { 496, CompanyPrefix.JapanOriginalJan },
            { 497, CompanyPrefix.JapanOriginalJan },
            { 498, CompanyPrefix.JapanOriginalJan },
            { 499, CompanyPrefix.JapanOriginalJan },
            { 500, CompanyPrefix.UnitedKingdom },
            { 501, CompanyPrefix.UnitedKingdom },
            { 502, CompanyPrefix.UnitedKingdom },
            { 503, CompanyPrefix.UnitedKingdom },
            { 504, CompanyPrefix.UnitedKingdom },
            { 505, CompanyPrefix.UnitedKingdom },
            { 506, CompanyPrefix.UnitedKingdom },
            { 507, CompanyPrefix.UnitedKingdom },
            { 508, CompanyPrefix.UnitedKingdom },
            { 509, CompanyPrefix.UnitedKingdom },
            { 520, CompanyPrefix.Greece },
            { 521, CompanyPrefix.Greece },
            { 528, CompanyPrefix.Lebanon },
            { 529, CompanyPrefix.Cyprus },
            { 530, CompanyPrefix.Albania },
            { 531, CompanyPrefix.NorthMacedonia },
            { 535, CompanyPrefix.Malta },
            { 539, CompanyPrefix.Ireland },
            { 540, CompanyPrefix.BelgiumAndLuxembourg },
            { 541, CompanyPrefix.BelgiumAndLuxembourg },
            { 542, CompanyPrefix.BelgiumAndLuxembourg },
            { 543, CompanyPrefix.BelgiumAndLuxembourg },
            { 544, CompanyPrefix.BelgiumAndLuxembourg },
            { 545, CompanyPrefix.BelgiumAndLuxembourg },
            { 546, CompanyPrefix.BelgiumAndLuxembourg },
            { 547, CompanyPrefix.BelgiumAndLuxembourg },
            { 548, CompanyPrefix.BelgiumAndLuxembourg },
            { 549, CompanyPrefix.BelgiumAndLuxembourg },
            { 560, CompanyPrefix.Portugal },
            { 569, CompanyPrefix.Iceland },
            { 570, CompanyPrefix.DenmarkFaroeIslandsAndGreenland },
            { 571, CompanyPrefix.DenmarkFaroeIslandsAndGreenland },
            { 572, CompanyPrefix.DenmarkFaroeIslandsAndGreenland },
            { 573, CompanyPrefix.DenmarkFaroeIslandsAndGreenland },
            { 574, CompanyPrefix.DenmarkFaroeIslandsAndGreenland },
            { 575, CompanyPrefix.DenmarkFaroeIslandsAndGreenland },
            { 576, CompanyPrefix.DenmarkFaroeIslandsAndGreenland },
            { 577, CompanyPrefix.DenmarkFaroeIslandsAndGreenland },
            { 578, CompanyPrefix.DenmarkFaroeIslandsAndGreenland },
            { 579, CompanyPrefix.DenmarkFaroeIslandsAndGreenland },
            { 590, CompanyPrefix.Poland },
            { 594, CompanyPrefix.Romania },
            { 599, CompanyPrefix.Hungary },
            { 600, CompanyPrefix.SouthAfrica },
            { 601, CompanyPrefix.SouthAfrica },
            { 603, CompanyPrefix.Ghana },
            { 604, CompanyPrefix.Senegal },
            { 608, CompanyPrefix.Bahrain },
            { 609, CompanyPrefix.Mauritius },
            { 611, CompanyPrefix.Morocco },
            { 613, CompanyPrefix.Algeria },
            { 615, CompanyPrefix.Nigeria },
            { 616, CompanyPrefix.Kenya },
            { 618, CompanyPrefix.IvoryCoast },
            { 619, CompanyPrefix.Tunisia },
            { 620, CompanyPrefix.Tanzania },
            { 621, CompanyPrefix.Syria },
            { 622, CompanyPrefix.Egypt },
            { 623, CompanyPrefix.Brunei },
            { 624, CompanyPrefix.Libya },
            { 625, CompanyPrefix.Jordan },
            { 626, CompanyPrefix.Iran },
            { 627, CompanyPrefix.Kuwait },
            { 628, CompanyPrefix.SaudiArabia },
            { 629, CompanyPrefix.UnitedArabEmirates },
            { 640, CompanyPrefix.Finland },
            { 641, CompanyPrefix.Finland },
            { 642, CompanyPrefix.Finland },
            { 643, CompanyPrefix.Finland },
            { 644, CompanyPrefix.Finland },
            { 645, CompanyPrefix.Finland },
            { 646, CompanyPrefix.Finland },
            { 647, CompanyPrefix.Finland },
            { 648, CompanyPrefix.Finland },
            { 649, CompanyPrefix.Finland },
            { 690, CompanyPrefix.PeoplesRepublicOfChina },
            { 691, CompanyPrefix.PeoplesRepublicOfChina },
            { 692, CompanyPrefix.PeoplesRepublicOfChina },
            { 693, CompanyPrefix.PeoplesRepublicOfChina },
            { 694, CompanyPrefix.PeoplesRepublicOfChina },
            { 695, CompanyPrefix.PeoplesRepublicOfChina },
            { 696, CompanyPrefix.PeoplesRepublicOfChina },
            { 697, CompanyPrefix.PeoplesRepublicOfChina },
            { 698, CompanyPrefix.PeoplesRepublicOfChina },
            { 699, CompanyPrefix.PeoplesRepublicOfChina },
            { 700, CompanyPrefix.Norway },
            { 701, CompanyPrefix.Norway },
            { 702, CompanyPrefix.Norway },
            { 703, CompanyPrefix.Norway },
            { 704, CompanyPrefix.Norway },
            { 705, CompanyPrefix.Norway },
            { 706, CompanyPrefix.Norway },
            { 707, CompanyPrefix.Norway },
            { 708, CompanyPrefix.Norway },
            { 709, CompanyPrefix.Norway },
            { 729, CompanyPrefix.Israel },
            { 730, CompanyPrefix.Sweden },
            { 731, CompanyPrefix.Sweden },
            { 732, CompanyPrefix.Sweden },
            { 733, CompanyPrefix.Sweden },
            { 734, CompanyPrefix.Sweden },
            { 735, CompanyPrefix.Sweden },
            { 736, CompanyPrefix.Sweden },
            { 737, CompanyPrefix.Sweden },
            { 738, CompanyPrefix.Sweden },
            { 739, CompanyPrefix.Sweden },
            { 740, CompanyPrefix.Guatemala },
            { 741, CompanyPrefix.ElSalvador },
            { 742, CompanyPrefix.Honduras },
            { 743, CompanyPrefix.Nicaragua },
            { 744, CompanyPrefix.CostaRica },
            { 745, CompanyPrefix.Panama },
            { 746, CompanyPrefix.DominicanRepublic },
            { 750, CompanyPrefix.Mexico },
            { 754, CompanyPrefix.Canada },
            { 755, CompanyPrefix.Canada },
            { 759, CompanyPrefix.Venezuela },
            { 760, CompanyPrefix.SwitzerlandAndLiechtenstein },
            { 761, CompanyPrefix.SwitzerlandAndLiechtenstein },
            { 762, CompanyPrefix.SwitzerlandAndLiechtenstein },
            { 763, CompanyPrefix.SwitzerlandAndLiechtenstein },
            { 764, CompanyPrefix.SwitzerlandAndLiechtenstein },
            { 765, CompanyPrefix.SwitzerlandAndLiechtenstein },
            { 766, CompanyPrefix.SwitzerlandAndLiechtenstein },
            { 767, CompanyPrefix.SwitzerlandAndLiechtenstein },
            { 768, CompanyPrefix.SwitzerlandAndLiechtenstein },
            { 769, CompanyPrefix.SwitzerlandAndLiechtenstein },
            { 770, CompanyPrefix.Colombia },
            { 771, CompanyPrefix.Colombia },
            { 773, CompanyPrefix.Uruguay },
            { 775, CompanyPrefix.Peru },
            { 777, CompanyPrefix.Bolivia },
            { 778, CompanyPrefix.Argentina },
            { 779, CompanyPrefix.Argentina },
            { 780, CompanyPrefix.Chile },
            { 784, CompanyPrefix.Paraguay },
            { 786, CompanyPrefix.Ecuador },
            { 789, CompanyPrefix.Brazil },
            { 790, CompanyPrefix.Brazil },
            { 800, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 801, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 802, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 803, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 804, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 805, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 806, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 807, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 808, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 809, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 810, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 811, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 812, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 813, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 814, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 815, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 816, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 817, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 818, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 819, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 820, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 821, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 822, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 823, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 824, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 825, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 826, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 827, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 828, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 829, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 830, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 831, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 832, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 833, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 834, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 835, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 836, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 837, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 838, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 839, CompanyPrefix.ItalySanMarinoAndVaticanCity },
            { 840, CompanyPrefix.SpainAndAndorra },
            { 841, CompanyPrefix.SpainAndAndorra },
            { 842, CompanyPrefix.SpainAndAndorra },
            { 843, CompanyPrefix.SpainAndAndorra },
            { 844, CompanyPrefix.SpainAndAndorra },
            { 845, CompanyPrefix.SpainAndAndorra },
            { 846, CompanyPrefix.SpainAndAndorra },
            { 847, CompanyPrefix.SpainAndAndorra },
            { 848, CompanyPrefix.SpainAndAndorra },
            { 849, CompanyPrefix.SpainAndAndorra },
            { 850, CompanyPrefix.Cuba },
            { 858, CompanyPrefix.Slovakia },
            { 859, CompanyPrefix.CzechRepublic },
            { 860, CompanyPrefix.Serbia },
            { 865, CompanyPrefix.Mongolia },
            { 867, CompanyPrefix.NorthKorea },
            { 868, CompanyPrefix.Turkey },
            { 869, CompanyPrefix.Turkey },
            { 870, CompanyPrefix.Netherlands },
            { 871, CompanyPrefix.Netherlands },
            { 872, CompanyPrefix.Netherlands },
            { 873, CompanyPrefix.Netherlands },
            { 874, CompanyPrefix.Netherlands },
            { 875, CompanyPrefix.Netherlands },
            { 876, CompanyPrefix.Netherlands },
            { 877, CompanyPrefix.Netherlands },
            { 878, CompanyPrefix.Netherlands },
            { 879, CompanyPrefix.Netherlands },
            { 880, CompanyPrefix.SouthKorea },
            { 884, CompanyPrefix.Cambodia },
            { 885, CompanyPrefix.Thailand },
            { 888, CompanyPrefix.Singapore },
            { 890, CompanyPrefix.India },
            { 893, CompanyPrefix.Vietnam },
            { 896, CompanyPrefix.Pakistan },
            { 899, CompanyPrefix.Indonesia },
            { 900, CompanyPrefix.Austria },
            { 901, CompanyPrefix.Austria },
            { 902, CompanyPrefix.Austria },
            { 903, CompanyPrefix.Austria },
            { 904, CompanyPrefix.Austria },
            { 905, CompanyPrefix.Austria },
            { 906, CompanyPrefix.Austria },
            { 907, CompanyPrefix.Austria },
            { 908, CompanyPrefix.Austria },
            { 909, CompanyPrefix.Austria },
            { 910, CompanyPrefix.Austria },
            { 911, CompanyPrefix.Austria },
            { 912, CompanyPrefix.Austria },
            { 913, CompanyPrefix.Austria },
            { 914, CompanyPrefix.Austria },
            { 915, CompanyPrefix.Austria },
            { 916, CompanyPrefix.Austria },
            { 917, CompanyPrefix.Austria },
            { 918, CompanyPrefix.Austria },
            { 919, CompanyPrefix.Austria },
            { 930, CompanyPrefix.Australia },
            { 931, CompanyPrefix.Australia },
            { 932, CompanyPrefix.Australia },
            { 933, CompanyPrefix.Australia },
            { 934, CompanyPrefix.Australia },
            { 935, CompanyPrefix.Australia },
            { 936, CompanyPrefix.Australia },
            { 937, CompanyPrefix.Australia },
            { 938, CompanyPrefix.Australia },
            { 939, CompanyPrefix.Australia },
            { 940, CompanyPrefix.NewZealand },
            { 941, CompanyPrefix.NewZealand },
            { 942, CompanyPrefix.NewZealand },
            { 943, CompanyPrefix.NewZealand },
            { 944, CompanyPrefix.NewZealand },
            { 945, CompanyPrefix.NewZealand },
            { 946, CompanyPrefix.NewZealand },
            { 947, CompanyPrefix.NewZealand },
            { 948, CompanyPrefix.NewZealand },
            { 949, CompanyPrefix.NewZealand },
            { 950, CompanyPrefix.GlobalOffice },
            { 951, CompanyPrefix.GeneralManagerNumber },
            { 955, CompanyPrefix.Malaysia },
            { 958, CompanyPrefix.Macau },
            { 960, CompanyPrefix.UnitedKingdomOfficeGtin8Allocation },
            { 961, CompanyPrefix.UnitedKingdomOfficeGtin8Allocation },
            { 962, CompanyPrefix.GlobalOfficeGtin8Allocation },
            { 963, CompanyPrefix.GlobalOfficeGtin8Allocation },
            { 964, CompanyPrefix.GlobalOfficeGtin8Allocation },
            { 965, CompanyPrefix.GlobalOfficeGtin8Allocation },
            { 966, CompanyPrefix.GlobalOfficeGtin8Allocation },
            { 967, CompanyPrefix.GlobalOfficeGtin8Allocation },
            { 968, CompanyPrefix.GlobalOfficeGtin8Allocation },
            { 969, CompanyPrefix.GlobalOfficeGtin8Allocation },
            { 977, CompanyPrefix.SerialPublicationIssn },
            { 978, CompanyPrefix.BooklandIsbn },
            { 979, CompanyPrefix.BooklandIsbn },
            { 980, CompanyPrefix.RefundReceipt },
            { 981, CompanyPrefix.CouponIdentificationForCommonCurrencyArea },
            { 982, CompanyPrefix.CouponIdentificationForCommonCurrencyArea },
            { 983, CompanyPrefix.CouponIdentificationForCommonCurrencyArea },
            { 984, CompanyPrefix.CouponIdentificationForCommonCurrencyArea },
            { 990, CompanyPrefix.CouponIdentification },
            { 991, CompanyPrefix.CouponIdentification },
            { 992, CompanyPrefix.CouponIdentification },
            { 993, CompanyPrefix.CouponIdentification },
            { 994, CompanyPrefix.CouponIdentification },
            { 995, CompanyPrefix.CouponIdentification },
            { 996, CompanyPrefix.CouponIdentification },
            { 997, CompanyPrefix.CouponIdentification },
            { 998, CompanyPrefix.CouponIdentification },
            { 999, CompanyPrefix.CouponIdentification }
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
    public static string GetCompanyPrefixDescription(this CompanyPrefix value) {
        var type = typeof(CompanyPrefix);
        var name = Enum.GetNames(type)
                       .Where(f => f.Equals(value.ToString(), StringComparison.Ordinal))
                       .Select(d => d)
                       .FirstOrDefault();

        if (name is null) {
            return string.Empty;
        }

        var field = type.GetField(name);
        var customAttribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return customAttribute is { Length: > 0 } ? ((DescriptionAttribute)customAttribute[0]).Description : name;
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
    /// <param name="productCode">The product code code.</param>
    /// <returns>A GS1 country code.</returns>
    public static CompanyPrefix ResolveGtinNtinToGs1Country(this string productCode) {
        return string.IsNullOrWhiteSpace(productCode)
                   ? CompanyPrefix.Unknown
                   : TestProductCodeLengthGt3();

        CompanyPrefix TestSheetMusicSpecifier() =>
            int.TryParse(productCode[4..5], out var sheetMusicSpecifier)
                ? sheetMusicSpecifier switch {
                    0 => CompanyPrefix.BooklandIsbnIsmn,
                    _ => CompanyPrefix.BooklandIsbn
                }
                : CompanyPrefix.Unknown;

        CompanyPrefix TestCountryCode() =>
            int.TryParse(productCode[1..4], out var countryCode)
                ? countryCode switch {
                    <= 99 and >= 0 => ResolveToGs1UpcACompatible(productCode),
                    979 => TestSheetMusicSpecifier(),
                    _ => countryCode.ResolveToGs1Country()
                }
                : CompanyPrefix.Unknown;

        CompanyPrefix TestProductCodeLengthGt3() =>
            productCode.Length > 3
                ? TestCountryCode()
                : CompanyPrefix.Unknown;
    }

    /// <summary>
    ///     Resolve an integer prefix to a GS1 country code.
    /// </summary>
    /// <param name="countryCode">The integer prefix representing the country code.</param>
    /// <returns>A GS1 country code.</returns>

    // ReSharper disable once UnusedMember.Global
    internal static CompanyPrefix ResolveToGs1Country(this string countryCode) {
        return string.IsNullOrWhiteSpace(countryCode)
                   ? CompanyPrefix.Unknown
                   : TestCountryCode();

        CompanyPrefix TestCountryCode() =>
            int.TryParse(countryCode, out var countryCodeInt)
                ? countryCodeInt.ResolveToGs1Country()
                : CompanyPrefix.Unknown;
    }

    /// <summary>
    ///     Resolve an integer prefix to a GS1 country code.
    /// </summary>
    /// <param name="countryCode">The integer prefix representing the country code.</param>
    /// <returns>A GS1 country code.</returns>

    // ReSharper disable once MemberCanBePrivate.Global
    internal static CompanyPrefix ResolveToGs1Country(this int countryCode) {
        if (CompanyPrefixes.TryGetValue(countryCode, out var prefix)) {
            return prefix;
        }

        var countryCodeAsString = countryCode.ToInvariantString();

        return Enum.TryParse(countryCodeAsString, out CompanyPrefix gs1Country)
                   ? gs1Country.ToString() switch {
                       var ccs when ccs == countryCodeAsString => CompanyPrefix.Unknown,
                       _ => gs1Country
                   }
                   : CompanyPrefix.Unknown;
    }

    /// <summary>
    ///     Resolve a product code to a UPC-A Compatible company prefix.
    /// </summary>
    /// <param name="productCode">The product code.</param>
    /// <returns>The UPC-A Compatible company prefix.</returns>
    private static CompanyPrefix ResolveToGs1UpcACompatible(string productCode) {
        if (string.IsNullOrWhiteSpace(productCode) || !UpcACompatibleRegex().IsMatch(productCode)
                                                   || productCode.Length > 14) {
            return CompanyPrefix.Unknown;
        }

        productCode = productCode.PadLeft(14);

        return GetCompanyPrefix();

        CompanyPrefix TestForUpcACompatibleUnitedStatesDrugs() =>
            productCode[1..3] == "03"
                ? CompanyPrefix.UpcACompatibleUnitedStatesDrugs
                : TestForUpcACompatibleUnitedStatesReserved();

        CompanyPrefix GetCompanyPrefix() =>
            productCode[1..4] == "000"
                ? TestForFiveZeros()
                : int.Parse(productCode[1..4], NumberStyles.None, CultureInfo.InvariantCulture).ResolveToGs1Country();

        CompanyPrefix TestForFiveZeros() =>
            productCode[1..6] == "00000"
                ? CompanyPrefix.UpcACompatibleGtin8
                : TestForUpcACompatibleUnitedStatesAndCanada();

        CompanyPrefix TestForUpcACompatibleUnitedStatesAndCanada() =>
            UpcaCompatibleUnitedStatesAndCanadaRegex().IsMatch(productCode) ||
            productCode[1..3] == "06"
                ? CompanyPrefix.UpcACompatibleUnitedStatesAndCanada
                : TestForUpcACompatibleRestrictedCirculation();

        CompanyPrefix TestForUpcACompatibleRestrictedCirculation() =>
            productCode[1..3] == "02" ||
            productCode[1..3] == "04"
                ? CompanyPrefix.UpcACompatibleRestrictedCirculation
                : TestForUpcACompatibleUnitedStatesDrugs();

        CompanyPrefix TestForUpcACompatibleUnitedStatesReserved() =>
            productCode[1..3] == "05"
                ? CompanyPrefix.UpcACompatibleUnitedStatesReserved
                : int.Parse(
                        productCode[1..4],
                        NumberStyles.None,
                        CultureInfo.InvariantCulture)
                    .ResolveToGs1Country();
    }
}