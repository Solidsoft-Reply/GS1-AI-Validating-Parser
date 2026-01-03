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
// Represents the scenario for element string sequence parsing with respect to the correspondence between
// element string sequences and physical entities.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai;

/// <summary>
/// Represents the scenario for element string sequence parsing with respect to the correspondence between
/// element string sequences and physical entities.
/// </summary>
public enum Scenario {
    /// <summary>
    /// The list of element string sequences (e.g., barcodes) appear on a single physical entity.
    /// </summary>
    /// <remarks>
    /// The parser will perform data relationship tests across all element string sequences in the list.
    /// </remarks>
    SinglePhysicalEntity = 0,

    /// <summary>
    /// Each element string sequence (e.g., barcode) appears uniquely on a single physical entity.
    /// </summary>
    /// <remarks>
    /// The parser will perform data relationship tests for each individual element string sequence.
    /// </remarks>
    SinglePhysicalEntityPerSequence = 1,

    /// <summary>
    /// No assumption is made regarding the correspondence of element string sequences (e.g barcodes) to physical entities.
    /// </summary>
    /// <remarks>
    /// The parser will not perform data relationship tests.
    /// </remarks>
    Arbitrary = 2,
}
