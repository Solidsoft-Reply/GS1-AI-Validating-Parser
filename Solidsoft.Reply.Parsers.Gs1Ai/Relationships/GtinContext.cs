// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GtinContext.cs" company="Solidsoft Reply Ltd">
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
// The context in which to interpret the GTIN (AI 01).
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai.Relationships;

/// <summary>
/// The context in which to interpret the GTIN (AI 01).
/// </summary>
internal enum GtinContext {
    /// <summary>
    /// No context.
    /// </summary>
    None,

    /// <summary>
    /// A variable measure trade item scanned at POS.
    /// </summary>
    VariableMeasure,

    /// <summary>
    /// A custom trade item.
    /// </summary>
    Custom,
}