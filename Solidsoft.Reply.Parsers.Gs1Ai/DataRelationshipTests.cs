// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataRelationshipTest.cs" company="Solidsoft Reply Ltd">
// Copyright (c) 2018-2025 Solidsoft Reply Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may obtain a copy of the License at
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
// Enumeration to control data relationship testing during parsing.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai;

/// <summary>
/// Controls whether data relationship tests are performed during Parse().
/// </summary>
public enum DataRelationshipTests
{
    /// <summary>
    /// Indicates that data relationship tests will not be carried out.
    /// </summary>
    No = 0,

    /// <summary>
    /// Indicates that data relationship tests will be carried out.
    /// </summary>
    Yes = 1,
}