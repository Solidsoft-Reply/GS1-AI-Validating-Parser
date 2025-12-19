// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MandatedElements.cs" company="Solidsoft Reply Ltd">
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
// A read-only dictionary of mandated relationship rules keyed by AI string (with optional pattern) and regex string.
// Provides capability to test resolved AI entries collected during parsing against the rules.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.Gs1Ai.Relationships;

using Solidsoft.Reply.Parsers.Common;
using Solidsoft.Reply.Parsers.Gs1Ai.Properties;

using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;

/// <summary>
/// Read-only dictionary of mandated elements where keys are AI strings and values are <see cref="MandatoryNode"/> instances.
/// Construction is not allowed; use <see cref="Instance"/> to access the singleton.
/// </summary>
internal sealed class MandatedElements : ReadOnlyDictionary<(string ai, string regex), MandatoryNode>
{
    private static readonly Dictionary<(string ai, string regex), MandatoryNode> Rules = new()
    {
        { ("01", "^0\\d{13}$:VariableMeasure"), new OrNode([new AiNode("30"), new AiNode("3nnn")]) },
        { ("01", "^9\\d{13}$"), new OrNode([new AiNode("30"), new AiNode("3nnn"), new AiNode("8001"), new AiNode("242")]) },
        { ("01", "^9\\d{13}$:VariableMeasure"), new OrNode([new AiNode("30"), new AiNode("3nnn"), new AiNode("8001")]) },
        { ("02", "^9\\d{13}$"), new OrNode([new AiNode("30"), new AiNode("3nnn"), new AiNode("8001")]) },
        { ("01", "^9\\d{13}$:Custom"), new AiNode("242") },
        { ("02", string.Empty), new AndNode([new AiNode("00"), new AiNode("37")]) },
        { ("10", string.Empty), new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("03"), new AiNode("8006"), new AiNode("8026")]) },
        { ("11", string.Empty), new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")]) },
        { ("13", string.Empty), new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")]) },
        { ("15", string.Empty), new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")]) },
        { ("16", string.Empty), new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")]) },
        { ("17", string.Empty), new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")]) },
        { ("12", string.Empty), new AndNode([new AiNode("8020"), new AiNode("415")]) },
        //{ ("17", string.Empty), new AiNode("255") },
        { ("20", string.Empty), new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")]) },
        { ("21", string.Empty), new XorNode([new AiNode("01"), new AiNode("03"), new AiNode("8006")]) },
        { ("22", string.Empty), new AiNode("01") },
        { ("235", string.Empty), new AiNode("01") },
        { ("240", string.Empty), new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")]) },
        { ("241", string.Empty), new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")]) },
        { ("242", string.Empty), new XorNode([new AiNode("01", "^9\\d{13}$"), new AiNode("02", "^9\\d{13}$"), new AiNode("8006", "^9\\d{13}\\d{4}\\d{4}$"), new AiNode("8026", "^9\\d{13}\\d{4}\\d{4}$")]) },
        { ("243", string.Empty), new AiNode("01") },
        { ("250", string.Empty), new AndNode([new XorNode([new AiNode("01"), new AiNode("8006")]), new AiNode("21")]) },
        { ("251", string.Empty), new XorNode([new AiNode("01"), new AiNode("8006")]) },
        { ("254", string.Empty), new AiNode("414") },
        { ("30", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("3nnn", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        //{ ("3nnn", string.Empty), new OrNode([new AiNode("00"), new AiNode("01")]) },
        { ("337n", string.Empty), new AiNode("01") },
        { ("37", string.Empty), new AndNode([new AiNode("00"), new XorNode([new AiNode("02"), new AiNode("8026")])]) },
        { ("390n", string.Empty), new AndNode([new AiNode("8020"), new AiNode("8026")]) },
        //{ ("390n", string.Empty), new AiNode("255") },
        { ("391n", string.Empty), new AndNode([new AiNode("8020"), new AiNode("415")]) },
        { ("392n", string.Empty), new AndNode([new AiNode("01"), new XorNode([new AiNode("30"), new AiNode("31nn"), new AiNode("32nn"), new AiNode("35nn"), new AiNode("36nn")])]) },
        { ("393n", string.Empty), new AndNode([new AiNode("01"), new XorNode([new AiNode("30"), new AiNode("31nn"), new AiNode("32nn"), new AiNode("35nn"), new AiNode("36nn")])]) },
        { ("394n", string.Empty), new AiNode("255") },
        { ("395n", string.Empty), new AndNode([new AiNode("01"), new XorNode([new AiNode("30"), new AiNode("31nn"), new AiNode("32nn"), new AiNode("35nn"), new AiNode("36nn")])]) },
        { ("403", string.Empty), new AiNode("00") },
        { ("415", string.Empty), new AiNode("8020") },
        { ("422", string.Empty), new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")]) },
        { ("423", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("424", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("425", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("426", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("427", string.Empty), new AndNode([new XorNode([new AiNode("01"), new AiNode("02")]), new AiNode("422")]) },
        { ("430N", string.Empty), new AiNode("00") },
        { ("4303", string.Empty), new AndNode([new AiNode("4302"), new AiNode("00")]) },
        { ("4309", string.Empty), new AiNode("00") },
        { ("431N", string.Empty), new AiNode("00") },
        { ("4313", string.Empty), new AndNode([new AiNode("4312"), new AiNode("00")]) },
        { ("432N", string.Empty), new AiNode("00") },
        { ("4330", string.Empty), new AiNode("00") },
        { ("4331", string.Empty), new AiNode("00") },
        { ("4332", string.Empty), new AiNode("00") },
        { ("4333", string.Empty), new AiNode("00") },
        { ("7001", string.Empty), new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")]) },
        { ("7002", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("7003", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("7004", string.Empty), new AndNode([new AiNode("01"), new AiNode("10")]) },
        { ("7005", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("7006", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("7007", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("7008", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("7009", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("7010", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("7011", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("703s", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("710", string.Empty), new AiNode("01") },
        { ("711", string.Empty), new AiNode("01") },
        { ("712", string.Empty), new AiNode("01") },
        { ("713", string.Empty), new AiNode("01") },
        { ("714", string.Empty), new AiNode("01") },
        { ("715", string.Empty), new AiNode("01") },
        { ("716", string.Empty), new AiNode("01") },
        { ("717", string.Empty), new AiNode("01") },
        { ("7020", string.Empty), new AndNode([new XorNode([new AiNode("01, 8006")]), new AiNode("416")]) },
        { ("7021", string.Empty), new XorNode([new AiNode("01, 8006")]) },
        { ("7022", string.Empty), new AndNode([new XorNode([new AiNode("01, 8006")]), new AiNode("7021")]) },
        { ("7041", string.Empty), new AiNode("00") },
        { ("723s", string.Empty), new XorNode([new AiNode("01"), new AiNode("8004")]) },
        { ("7240", string.Empty), new XorNode([new AiNode("01"), new AiNode("8006")]) },
        { ("7241", string.Empty), new XorNode([new AiNode("8017"), new AiNode("8018")]) },
        { ("7242", string.Empty), new XorNode([new AiNode("8017"), new AiNode("8018")]) },
        { ("7250", string.Empty), new AiNode("8018") },
        { ("7251", string.Empty), new AiNode("8018") },
        { ("7252", string.Empty), new AiNode("8018") },
        { ("7257", string.Empty), new AiNode("8018") },
        { ("7259", string.Empty), new AiNode("8018") },
        { ("7253", string.Empty), new XorNode([new AiNode("8017"), new AiNode("8018")]) },
        { ("7254", string.Empty), new XorNode([new AiNode("8017"), new AiNode("8018")]) },
        { ("7255", string.Empty), new XorNode([new AiNode("8017"), new AiNode("8018")]) },
        { ("7256", string.Empty), new XorNode([new AiNode("8017"), new AiNode("8018")]) },
        { ("7258", string.Empty), new AndNode([new AiNode("8018"), new AiNode("7259")]) },
        { ("8001", string.Empty), new AiNode("01") },
        { ("8005", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("8007", string.Empty), new AndNode([new AiNode("8020"), new AiNode("415")]) },
        { ("8008", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("8009", string.Empty), new OrNode([new AiNode("01"), new AiNode("00")]) },
        { ("8011", string.Empty), new AiNode("8010") },
        { ("8012", string.Empty), new XorNode([new AiNode("01"), new AiNode("8006")]) },
        { ("8014", string.Empty), new AiNode("01") },
        { ("8019", string.Empty), new XorNode([new AiNode("8017"), new AiNode("8018")]) },
        { ("8020", string.Empty), new AiNode("415") },
        { ("8026", string.Empty), new AndNode([new AiNode("00"), new AiNode("37")]) },
        { ("8030", string.Empty), new XorNode([new AndNode([new AiNode("01"), new AiNode("21")]), new AndNode([new AiNode("8006"), new AiNode("21")]), new AndNode([new AiNode("8010"), new AiNode("8011")]), new AiNode("8003"), new AiNode("8004"), new AiNode("8017"), new AiNode("8018"), new AiNode("00"), new AiNode("253"), new AiNode("255")]) },
        { ("8040", string.Empty), new AndNode([new AiNode("01"), new AiNode("21")]) },
        { ("8041", string.Empty), new AndNode([new AiNode("01"), new AiNode("21"), new AiNode("8040")]) },
        { ("8042", string.Empty), new AndNode([new AiNode("01"), new AiNode("21"), new AiNode("8041")]) },
        { ("8043", string.Empty), new AndNode([new AiNode("01"), new AiNode("21")]) },
        { ("8111", string.Empty), new AiNode("255") },
        { ("8200", string.Empty), new AiNode("01") },
    };

    private MandatedElements() : base(Rules) { }

    public static MandatedElements Instance { get; } = new MandatedElements();

    /// <summary>
    /// Tests the thread-static list of resolved AI entries against the mandate rules.
    /// Applies each matched rule and records exceptions for failures.
    /// </summary>
    /// <param name="gtinContext">Optional GTIN context; defaults to None.</param>
    /// <returns>A read-only list of exceptions for failed rules.</returns>
    public static IReadOnlyList<ParserException> Test(GtinContext gtinContext = GtinContext.None)
    {
        var exceptions = new List<ParserException>();
        var entries = ResolvedAiList.Current;

        // Find matched rules per entry first (as previously filtered)
        var matched = new List<((string ai, string regex) key, MandatoryNode node)>();
        foreach (var entry in entries)
        {
            foreach (var kvp in Rules)
            {
                var key = kvp.Key;
                var node = kvp.Value;

                // GTIN context filtering for AI "01" with suffix
                if (entry.Identifier == "01")
                {
                    var suffixIndex = key.regex?.LastIndexOf(':') ?? -1;
                    if (suffixIndex >= 0)
                    {
#if NET6_0_OR_GREATER
                        var ctxSuffix = key.regex?[(suffixIndex + 1)..];
#else
                        var ctxSuffix = key.regex?.Substring(suffixIndex + 1);
#endif
                        if (string.Equals(ctxSuffix, nameof(GtinContext.VariableMeasure), StringComparison.Ordinal)
                            || string.Equals(ctxSuffix, nameof(GtinContext.Custom), StringComparison.Ordinal))
                        {
                            var requiredContext = ctxSuffix == nameof(GtinContext.VariableMeasure)
                                ? GtinContext.VariableMeasure
                                : GtinContext.Custom;
                            if (gtinContext == GtinContext.None || gtinContext != requiredContext)
                            {
                                continue;
                            }
                        }
                    }
                }

                if (!AiPatternMatches(key.ai, entry.Identifier))
                {
                    continue;
                }

                Regex? valueRegex = null;
                if (!string.IsNullOrEmpty(key.regex))
                {
                    var colonIndex = key.regex.LastIndexOf(':');
#if NET6_0_OR_GREATER
                    var pattern = colonIndex >= 0 ? key.regex[..colonIndex] : key.regex;
#else
                    var pattern = colonIndex >= 0 ? key.regex.Substring(0, colonIndex) : key.regex;
#endif
                    if (!string.IsNullOrEmpty(pattern))
                    {
                        valueRegex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);
                    }
                }

                if (valueRegex is not null && !valueRegex.IsMatch(entry.Value))
                {
                    continue;
                }

                matched.Add((key, node));
            }
        }

        // Apply each matched rule to the full set of entries
        foreach (var rule in matched)
        {
            var node = rule.node;
            bool fullMatch;

            if (node is AiNode aiNode)
            {
                // First, if the AI node has a value regex, enforce it against the corresponding entry values
                if (aiNode.ValueRegex is not null)
                {
                    var anyValueMatch = false;
                    foreach (var e in entries)
                    {
                        if (AiPatternMatches(aiNode.Ai, e.Identifier) && aiNode.ValueRegex.IsMatch(e.Value))
                        {
                            anyValueMatch = true;
                            break;
                        }
                    }

                    if (!anyValueMatch)
                    {
                        exceptions.Add(new ParserException(
                            rule.key.ai,
                            2202,
                            string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.GS1_Error_202,
                            rule.key.ai,
                            $"AI {aiNode.Ai}"),
                            true,
                            rule.key.ai.Length));
                        continue; // Move to next rule
                    }
                }

                // Then ensure the AI is present in entries (respecting wildcard 'n','N','s')
                fullMatch = false;
                foreach (var e in entries)
                {
                    if (AiPatternMatches(aiNode.Ai, e.Identifier))
                    {
                        fullMatch = true;
                        break;
                    }
                }

                if (!fullMatch)
                {
                    exceptions.Add(new ParserException(
                        rule.key.ai,
                        2202,
                        string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.GS1_Error_202,
                        rule.key.ai,
                        $"AI {aiNode.Ai}"),
                        true,
                        rule.key.ai.Length));
                }

                continue; // root processed
            }

            // Composite nodes: depth-first evaluation
            fullMatch = EvaluateComposite(node, entries);
            if (!fullMatch)
            {
                exceptions.Add(new ParserException(
                    rule.key.ai,
                    2202,
                    string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.GS1_Error_202,
                    rule.key.ai,
                    "one of multiple AIs"),
                    true,
                    rule.key.ai.Length));
            }
        }

        return new ReadOnlyCollection<ParserException>(exceptions);
    }

    private static bool EvaluateComposite(MandatoryNode node, IReadOnlyList<ResolvedAiEntry> entries)
    {
        if (node is AiNode ai)
        {
            // Leaf evaluation: check AI presence and optional value regex
            foreach (var e in entries)
            {
                if (!AiPatternMatches(ai.Ai, e.Identifier))
                {
                    continue;
                }

                if (ai.ValueRegex is null || ai.ValueRegex.IsMatch(e.Value))
                {
                    return true;
                }
            }

            return false;
        }

        if (node is CompositeNode composite)
        {
            switch (composite.NodeType)
            {
                case MandatoryNodeType.And:
                {
                    foreach (var child in composite.Children)
                    {
                        if (!EvaluateComposite(child, entries))
                        {
                            return false;
                        }
                    }

                    return true;
                }
                case MandatoryNodeType.Or:
                {
                    foreach (var child in composite.Children)
                    {
                        if (EvaluateComposite(child, entries))
                        {
                            return true;
                        }
                    }

                    return false;
                }
                case MandatoryNodeType.Xor:
                {
                    var matchCount = 0;
                    foreach (var child in composite.Children)
                    {
                        if (EvaluateComposite(child, entries))
                        {
                            matchCount++;
                            if (matchCount > 1)
                            {
                                break;
                            }
                        }
                    }

                    return matchCount == 1;
                }
            }
        }

        return false;
    }

    private static bool AiPatternMatches(string pattern, string ai)
    {
        if (string.IsNullOrEmpty(pattern)) return false;
        if (pattern.Length != ai.Length) return false;
        for (int i = 0; i < pattern.Length; i++)
        {
            char pc = pattern[i];
            char ac = ai[i];
            if (pc == 'n' || pc == 'N' || pc == 's')
            {
                if (ac < '0' || ac > '9') return false;
            }
            else if (pc != ac)
            {
                return false;
            }
        }
        return true;
    }
}