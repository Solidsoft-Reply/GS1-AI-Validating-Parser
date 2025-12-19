// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MandatoryNode.cs" company="Solidsoft Reply Ltd">
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
// Base class for mandatory relationship nodes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai.Relationships;

/// <summary>
/// Base class for mandatory relationship nodes.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="MandatoryNode"/> class.
/// </remarks>
/// <param name="type">The type of the mandatory node.</param>
internal abstract class MandatoryNode(MandatoryNodeType type) {

    /// <summary>
    /// Gets the node type discriminator.
    /// </summary>
    public MandatoryNodeType NodeType { get; } = type;
}
