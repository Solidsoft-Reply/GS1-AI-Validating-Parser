// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompositeNode.cs" company="Solidsoft Reply Ltd">
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
// Base class for composite nodes that maintain a list of child nodes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.Parsers.Gs1Ai.Relationships;

using System.Collections.ObjectModel;

/// <summary>
/// Base class for composite nodes that maintain a list of child nodes.
/// </summary>
internal abstract class CompositeNode : MandatoryNode {
    private readonly ReadOnlyCollection<MandatoryNode> children;

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeNode"/> class.
    /// </summary>
    /// <param name="type">The type of the composite node.</param>
    /// <param name="children">The child nodes.</param>
    protected CompositeNode(MandatoryNodeType type, IEnumerable<MandatoryNode> children)
        : base(type) {
        if (children is null) {
            throw new ArgumentNullException(nameof(children));
        }

        this.children = new ReadOnlyCollection<MandatoryNode>(children is null ? new List<MandatoryNode>() : new List<MandatoryNode>(children));
    }

    /// <summary>
    /// Gets the read-only list of child nodes.
    /// </summary>
    public ReadOnlyCollection<MandatoryNode> Children => children;
}