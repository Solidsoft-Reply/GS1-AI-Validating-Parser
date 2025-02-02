// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalisedDescriptionAttribute.cs" company="Solidsoft Reply Ltd">
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
// Specifies a localised description for a property or event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai;

using System.ComponentModel;

using Properties;

/// <summary>
///   Specifies a localised description for a property or event.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="LocalisedDescriptionAttribute"/> class.
/// </remarks>
/// <param name="key">The description key.</param>
public class LocalisedDescriptionAttribute(string key)
    : DescriptionAttribute(Localise(key)) {
    /// <summary>
    /// Return the localised string for a given description key.
    /// </summary>
    /// <param name="key">The descriptor key.</param>
    /// <returns>The localised descriptor.</returns>
    private static string Localise(string key) {
        return Gs1CountryCode.ResourceManager.GetString(key) ?? string.Empty;
    }
}
