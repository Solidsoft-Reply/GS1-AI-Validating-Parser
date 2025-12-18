// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AiNode.cs" company="Solidsoft Reply Ltd">
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
// AI leaf node representing an AI value and optional compiled regex to validate the AI's value format/content.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai.Relationships;

using System.Text.RegularExpressions;

/// <summary>
/// AI leaf node representing an AI value and optional compiled regex to validate the AI's value format/content.
/// </summary>
internal sealed class AiNode : MandatoryNode {

    /// <summary>
    /// Initializes a new instance of the <see cref="AiNode"/> class.
    /// </summary>
    /// <param name="ai">The AI code value. Cannot be null or empty.</param>
    /// <param name="valueRegex">Optional regex pattern to validate the AI's value. Compiled if provided.</param>
    public AiNode(string ai, string? valueRegex = null)
        : base(MandatoryNodeType.Ai) {
        if (string.IsNullOrWhiteSpace(ai)) {
            throw new ArgumentException("AI must be provided.", nameof(ai));
        }

        Ai = ai;
        ValueRegex = string.IsNullOrEmpty(valueRegex)
            ? null
            : new Regex(valueRegex, RegexOptions.Compiled | RegexOptions.CultureInvariant);
    }

    /// <summary>
    /// Gets the AI code.
    /// </summary>
    public string Ai { get; }

    /// <summary>
    /// Gets the optional compiled regex used to validate the AI's value.
    /// </summary>
    public Regex? ValueRegex { get; }
}
