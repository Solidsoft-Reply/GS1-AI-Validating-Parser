// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpiryDateSemantics.cs" company="Solidsoft Reply Ltd">
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
// The semantics of the Expiry Date (AI 17).
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai.Relationships;

/// <summary>
/// The semantics of the Expiry Date (AI 17).
/// </summary>
public enum ExpiryDateSemantics {
    /// <summary>
    /// Expiry date of a trade item, or no specific semantics provided.
    /// </summary>
    TradeItem = 0,

    /// <summary>
    /// Expiry date of a coupon.
    /// </summary>
    Coupon,
}