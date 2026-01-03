// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResolvedAiList.cs" company="Solidsoft Reply Ltd">
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
/// Provides a per-thread static list of <see cref="ResolvedAiEntry"/> used when DataRelationshipTest is Yes.
/// </summary>
public static class ResolvedAiList
{
    /// <summary>
    /// Per-thread storage of the resolved AI list, initialized with capacity for 10 items.
    /// </summary>
    [ThreadStatic]
    private static List<ResolvedAiEntry>? list;

    /// <summary>
    /// Gets the per-thread list, creating it if necessary.
    /// </summary>
    public static List<ResolvedAiEntry> Current
    {
        get
        {
            list ??= new List<ResolvedAiEntry>(capacity: 10);
            return list;
        }
    }

    /// <summary>
    /// Adds an entry using string identifier and value.
    /// </summary>
    /// <param name="identifier">The AI identifier.</param>
    /// <param name="value">The AI value.</param>
    /// <param name="position">The position of the AI.</param>
    public static void Add(string identifier, string value, int position)
    {
        Current.Add(new ResolvedAiEntry(identifier, value, position));
    }

    /// <summary>
    /// Adds an entry from spans (e.g., taken from ref struct fields) by materializing to strings.
    /// </summary>
    /// <param name="identifier">The identifier span.</param>
    /// <param name="value">The value span.</param>
    /// <param name="position">The position of the AI.</param>
    public static void Add(ReadOnlySpan<char> identifier, ReadOnlySpan<char> value, int position)
    {
        Current.Add(new ResolvedAiEntry(identifier.ToString().TrimEnd('\0'), value.ToString().TrimEnd('\0'), position));
    }

    /// <summary>
    /// Clears the current per-thread list.
    /// </summary>
    public static void Clear()
    {
        list?.Clear();
    }
}