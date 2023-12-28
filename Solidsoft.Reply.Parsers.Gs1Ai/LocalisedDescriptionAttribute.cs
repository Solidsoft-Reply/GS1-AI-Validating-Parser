// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalisedDescriptionAttribute.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018-2023 Solidsoft Reply Ltd.  All rights reserved.
// </copyright>
// <license>
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
// </license>
// <summary>
// Specifies a localised description for a property or event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using Solidsoft.Reply.Parsers.Gs1Ai.Properties;

namespace Solidsoft.Reply.Parsers.Gs1Ai;

/// <summary>
///   Specifies a localised description for a property or event.
/// </summary>
public class LocalisedDescriptionAttribute : DescriptionAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalisedDescriptionAttribute"/> class.
    /// </summary>
    /// <param name="key">The description key.</param>
    public LocalisedDescriptionAttribute(string key)
        : base(Localise(key))
    {
    }
    
    /// <summary>
    /// Return the localised string for a given description key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    static string Localise(string key)
    {
        return Gs1CountryCode.ResourceManager.GetString(key) ?? string.Empty;
    }

}
