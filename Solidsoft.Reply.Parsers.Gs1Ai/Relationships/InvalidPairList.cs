// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidPairList.cs" company="Solidsoft Reply Ltd">
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
// A list of invalid AI pairs that will be associated with a 'check' AI via an Invalid Pair Dictionary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai.Relationships;

using System.Collections.ObjectModel;

/// <summary>
/// A read-only list of invalid AI pairs.
/// </summary>
internal sealed class InvalidPairList : ReadOnlyCollection<string> {

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidPairList"/> class with the specified collection of items.
    /// </summary>
    /// <param name="items">The collection of string items to include in the list. Cannot be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if the items parameter is null.</exception>
    public InvalidPairList(IEnumerable<string> items)
        : base(items is null
            ? throw new ArgumentNullException(nameof(items))
            : new List<string>(items)) {
    }
}