// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scenario.cs" company="Solidsoft Reply Ltd">
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
// Represents the scenario for barcode parsing with respect to the correspondence between
// barcodes and physical entities.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai;

/// <summary>
/// Represents the scenario for barcode parsing with respect to the correspondence between
/// barcodes and physical entities.
/// </summary>
public enum Scenario {
    /// <summary>
    /// The list of barcodes appear on a single physical entity.
    /// </summary>
    /// <remarks>
    /// The parser will perform data relationship tests across all barcodes in the list.
    /// </remarks>
    SinglePhysicalEntity = 0,

    /// <summary>
    /// Each barcode appears on a single physical entity.
    /// </summary>
    /// <remarks>
    /// The parser will perform data relationship tests for each individual barcode.
    /// </remarks>
    SinglePhysicalEntityPerBarcode = 1,

    /// <summary>
    /// No assumption is made regarding the correspondence of barcodes to physical entities.
    /// </summary>
    /// <remarks>
    /// The parser will not perform data relationship tests.
    /// </remarks>
    Arbitrary = 2,
}
