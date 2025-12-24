// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidPairs.cs" company="Solidsoft Reply Ltd">
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
// A read-only dictionary of invalid AI pairs keyed by string that is initialized internally.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.Parsers.Gs1Ai.Relationships;

using Solidsoft.Reply.Parsers.Common;
using Solidsoft.Reply.Parsers.Gs1Ai.Properties;

using System.Collections.ObjectModel;
using System.Globalization;

/// <summary>
/// A read-only dictionary of invalid AI pairs. Keys are strings and values are instances of <see cref="InvalidPairList"/>.
/// Construction is not allowed; use <see cref="Instance"/> to access the singleton.
/// </summary>
internal sealed class InvalidPairs : ReadOnlyDictionary<string, InvalidPairList> {

    /// <summary>
    /// The internal dictionary of invalid AI pairs.
    /// </summary>
    private static readonly Dictionary<string, InvalidPairList> Pairs = new() {
        { "01", new InvalidPairList(["01", "02", "03", "37", "255"]) },
        { "03", new InvalidPairList(["02", "37"]) },
        { "21", new InvalidPairList(["235"]) },
        { "420", new InvalidPairList(["421"]) },
        { "421", new InvalidPairList(["4307"]) },
        { "422", new InvalidPairList(["426"]) },
        { "423", new InvalidPairList(["426"]) },
        { "424", new InvalidPairList(["426"]) },
        { "425", new InvalidPairList(["426"]) },
        { "3900", new InvalidPairList(["3910", "3911", "3912", "3913", "3914", "3915", "3916", "3917", "3918", "3919", "3940", "3941", "3942", "3943", "8111"]) },
        { "3901", new InvalidPairList(["3910", "3911", "3912", "3913", "3914", "3915", "3916", "3917", "3918", "3919", "3940", "3941", "3942", "3943", "8111"]) },
        { "3902", new InvalidPairList(["3910", "3911", "3912", "3913", "3914", "3915", "3916", "3917", "3918", "3919", "3940", "3941", "3942", "3943", "8111"]) },
        { "3903", new InvalidPairList(["3910", "3911", "3912", "3913", "3914", "3915", "3916", "3917", "3918", "3919", "3940", "3941", "3942", "3943", "8111"]) },
        { "3904", new InvalidPairList(["3910", "3911", "3912", "3913", "3914", "3915", "3916", "3917", "3918", "3919", "3940", "3941", "3942", "3943", "8111"]) },
        { "3905", new InvalidPairList(["3910", "3911", "3912", "3913", "3914", "3915", "3916", "3917", "3918", "3919", "3940", "3941", "3942", "3943", "8111"]) },
        { "3906", new InvalidPairList(["3910", "3911", "3912", "3913", "3914", "3915", "3916", "3917", "3918", "3919", "3940", "3941", "3942", "3943", "8111"]) },
        { "3907", new InvalidPairList(["3910", "3911", "3912", "3913", "3914", "3915", "3916", "3917", "3918", "3919", "3940", "3941", "3942", "3943", "8111"]) },
        { "3908", new InvalidPairList(["3910", "3911", "3912", "3913", "3914", "3915", "3916", "3917", "3918", "3919", "3940", "3941", "3942", "3943", "8111"]) },
        { "3909", new InvalidPairList(["3910", "3911", "3912", "3913", "3914", "3915", "3916", "3917", "3918", "3919", "3940", "3941", "3942", "3943", "8111"]) },
        { "3920", new InvalidPairList(["3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "3950", "3951", "3952", "3953", "3954", "3955"]) },
        { "3921", new InvalidPairList(["3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "3950", "3951", "3952", "3953", "3954", "3955"]) },
        { "3922", new InvalidPairList(["3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "3950", "3951", "3952", "3953", "3954", "3955"]) },
        { "3923", new InvalidPairList(["3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "3950", "3951", "3952", "3953", "3954", "3955"]) },
        { "3924", new InvalidPairList(["3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "3950", "3951", "3952", "3953", "3954", "3955"]) },
        { "3925", new InvalidPairList(["3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "3950", "3951", "3952", "3953", "3954", "3955"]) },
        { "3926", new InvalidPairList(["3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "3950", "3951", "3952", "3953", "3954", "3955"]) },
        { "3927", new InvalidPairList(["3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "3950", "3951", "3952", "3953", "3954", "3955"]) },
        { "3928", new InvalidPairList(["3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "3950", "3951", "3952", "3953", "3954", "3955"]) },
        { "3929", new InvalidPairList(["3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "3950", "3951", "3952", "3953", "3954", "3955"]) },
        { "3940", new InvalidPairList(["8111"]) },
        { "3941", new InvalidPairList(["8111"]) },
        { "3942", new InvalidPairList(["8111"]) },
        { "3943", new InvalidPairList(["8111"]) },
        { "3950", new InvalidPairList(["3920", "3921", "3922", "3923", "3924", "3925", "3926", "3927", "3928", "3929", "3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "8005"]) },
        { "3951", new InvalidPairList(["3920", "3921", "3922", "3923", "3924", "3925", "3926", "3927", "3928", "3929", "3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "8005"]) },
        { "3952", new InvalidPairList(["3920", "3921", "3922", "3923", "3924", "3925", "3926", "3927", "3928", "3929", "3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "8005"]) },
        { "3953", new InvalidPairList(["3920", "3921", "3922", "3923", "3924", "3925", "3926", "3927", "3928", "3929", "3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "8005"]) },
        { "3954", new InvalidPairList(["3920", "3921", "3922", "3923", "3924", "3925", "3926", "3927", "3928", "3929", "3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "8005"]) },
        { "3955", new InvalidPairList(["3920", "3921", "3922", "3923", "3924", "3925", "3926", "3927", "3928", "3929", "3930", "3931", "3932", "3933", "3934", "3935", "3936", "3937", "3938", "3939", "8005"]) },
        { "4330", new InvalidPairList(["4331"]) },
        { "4332", new InvalidPairList(["4333"]) },
        { "7250", new InvalidPairList(["7251"]) },
        { "7256", new InvalidPairList(["7253", "7254", "7255", "7259"]) },
        { "7259", new InvalidPairList(["7253", "7254", "7255", "7256"]) },
        { "8006", new InvalidPairList(["01", "37"]) },
        { "8018", new InvalidPairList(["8017"]) },
        { "8026", new InvalidPairList(["02", "8006"]) },
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidPairs"/> class.
    /// Private to prevent external construction.
    /// </summary>
    private InvalidPairs()
        : base(Pairs)
    {
    }

    /// <summary>
    /// Gets the singleton instance of the <see cref="InvalidPairs"/> read-only dictionary.
    /// </summary>
    public static InvalidPairs Instance { get; } = new InvalidPairs();

    /// <summary>
    /// Tests the collected AI entries for any invalid pairs and returns (ai, exception 2201) for violations.
    /// </summary>
    /// <returns>A read-only list of tuples, each containing an AI and its associated exception.</returns>
    public static IReadOnlyList<(string ai, ParserException ex)> Test()
    {
        var exceptions = new List<(string ai, ParserException ex)>();
        var entries = ResolvedAiList.Current;

        for (int i = 0; i < entries.Count; i++)
        {
            var currentAi = entries[i].Identifier;
            if (!Pairs.TryGetValue(currentAi, out var invalidList))
            {
                continue;
            }

            for (int j = 0; j < entries.Count; j++)
            {
                if (i == j) continue;
                var otherAi = entries[j].Identifier;

                if (invalidList.Contains(otherAi))
                {
                    var ex = new ParserException(
                        currentAi,
                        2201,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.GS1_Error_201,
                            currentAi,
                            otherAi),
                        true,
                        entries[j].Position - entries[i].Position - entries[i].Identifier.Length + entries[j].Identifier.Length - 1);
                    exceptions.Add((currentAi, ex));
                }
            }
        }

        return new ReadOnlyCollection<(string ai, ParserException ex)>(exceptions);
    }

    // Internal initialization helper(s) can be added here as needed to populate 'pairs'.
}