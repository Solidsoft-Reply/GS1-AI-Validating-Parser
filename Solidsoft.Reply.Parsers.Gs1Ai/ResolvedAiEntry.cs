// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResolvedAiEntry.cs" company="Solidsoft Reply Ltd">
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
// Thread-safe per-thread list for collecting AI identifiers and values during Parse operations.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.Parsers.Gs1Ai;

/// <summary>
/// Represents a collected AI identifier and its value (snapshot for relationship testing).
/// </summary>
public sealed class ResolvedAiEntry {
    /// <summary>
    /// Initializes a new instance of the <see cref="ResolvedAiEntry"/> class.
    /// </summary>
    /// <param name="identifier">The AI identifier.</param>
    /// <param name="value">The AI value.</param>
    /// <param name="position">The position of the AI.</param>
    public ResolvedAiEntry(string identifier, string value, int position) {
        Identifier = identifier ?? string.Empty;
        Value = value ?? string.Empty;
        Position = position;
    }

    /// <summary>
    /// Gets the AI identifier.
    /// </summary>
    public string Identifier { get; }

    /// <summary>
    /// Gets the AI value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Gets the position of the AI.
    /// </summary>
    public int Position { get; }

}