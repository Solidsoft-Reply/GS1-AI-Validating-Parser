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
using Solidsoft.Reply.Parsers.Gs1Ai;
using Solidsoft.Reply.Parsers.Gs1Ai.Properties;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;

/// <summary>
/// Provides a read-only mapping of GS1 Application Identifier (AI) patterns and context-specific rules to their
/// corresponding mandatory element requirements, as defined by the GS1 specification.
/// </summary>
/// <remarks>The MandatedElements class enforces the presence of required AIs for various GS1 barcode contexts,
/// such as standard, variable measure, or custom GTINs. It is implemented as a singleton and exposes a static Instance
/// property for global access. The mapping is keyed by tuples of AI patterns and optional regular expressions, allowing
/// for flexible rule evaluation based on both identifier and value patterns. This class is intended for internal use in
/// validating that parsed barcode data includes all mandated elements for a given context.</remarks>
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
        { ("17", ":Coupon"), new AiNode("255") },
        { ("20", string.Empty), new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")]) },
        { ("21", string.Empty), new XorNode([new AiNode("01"), new AiNode("03"), new AiNode("8006")]) },
        { ("22", string.Empty), new AiNode("01") },
        { ("235", string.Empty), new AiNode("01") },
        { ("240", string.Empty), new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")]) },
        { ("241", string.Empty), new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")]) },
        { ("242", string.Empty), new XorNode([new AiNode("01", "^9\\d{13}$"), new AiNode("02", "^9\\d{13}$"), new AiNode("8006", "^9\\d{13}\\d{2}\\d{2}$"), new AiNode("8026", "^9\\d{13}\\d{2}\\d{2}$")]) },
        { ("243", string.Empty), new AiNode("01") },
        { ("250", string.Empty), new AndNode([new XorNode([new AiNode("01"), new AiNode("8006")]), new AiNode("21")]) },
        { ("251", string.Empty), new XorNode([new AiNode("01"), new AiNode("8006")]) },
        { ("254", string.Empty), new AiNode("414") },
        { ("30", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("^3(1[0-6]|2\\d|5[0-267]|6[014-6])\\d$", string.Empty), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("^3(1[0-6]|2\\d|5[0-267]|6[014-6])\\d$:VariableMeasure", ":VariableMeasure"), new XorNode([new AiNode("01"), new AiNode("02")]) },
        { ("^3(3[0-6]|4\\d|5[3-5]|6[237-9])\\d$", string.Empty), new XorNode([new AiNode("00"), new AiNode("01")]) },
        { ("337n", string.Empty), new AiNode("01") },
        { ("37", string.Empty), new AndNode([new AiNode("00"), new XorNode([new AiNode("02"), new AiNode("8026")])]) },
        { ("390n", string.Empty), new AndNode([new AiNode("8020"), new AiNode("8026")]) },
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

    private MandatedElements()
        : base(Rules)
    {
    }

    /// <summary>
    /// Gets the singleton instance of the <see cref="MandatedElements"/> class.
    /// </summary>
    public static MandatedElements Instance { get; } = new MandatedElements();

    /// <summary>
    /// Tests the current resolved AI entries against the mandated elements rules.
    /// </summary>
    /// <param name="semantics">The AI semantics when evaluating rules (e.g., General, VariableMeasure, or Custom of AI 01).</param>
    /// <returns>A read-only list of tuples containing the AI and the associated parser exception for any issues found.</returns>
    public static IReadOnlyList<(string ai, ParserException ex)> Test(Semantics semantics)
    {
        var issues = new List<(string ai, ParserException ex)>();
        var entries = ResolvedAiList.Current;
        var matched = new List<((string ai, string regex) key, MandatoryNode node)>();

        foreach (var entry in entries) {
            foreach (var rule in Rules) {
                var ruleKey = rule.Key;
                var ruleNode = rule.Value;

                // Single context/semantics check covering both 01 and 17 cases
                if (!RuleContextMatches(ruleKey.regex, entry.Identifier, semantics)) {
                    continue;
                }

                if (!ruleKey.ai.AiPatternMatches(entry.Identifier)
                    && !ruleKey.ai.AiRegExMatches(entry.Identifier)) {
                    continue;
                }

                Regex? valueRegex = null;
                if (!string.IsNullOrEmpty(ruleKey.regex)) {
                    var colonIndex = ruleKey.regex.LastIndexOf(':');
#if NET6_0_OR_GREATER
                    var pattern = colonIndex >= 0 ? ruleKey.regex[..colonIndex] : ruleKey.regex;
#else
                    var pattern = colonIndex >= 0 ? ruleKey.regex.Substring(0, colonIndex) : ruleKey.regex;
#endif
                    if (!string.IsNullOrEmpty(pattern)) {
                        valueRegex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);
                    }
                }

                if (valueRegex is not null && !valueRegex.IsMatch(entry.Value)) continue;

                matched.Add((ruleKey, ruleNode));
            }
        }

        foreach (var (ruleKey, ruleNode) in matched) {
            bool fullMatch;

            if (ruleNode is AiNode aiNode) {
                if (aiNode.ValueRegex is not null) {
                    var anyValueMatch = false;
                    foreach (var e in entries) {
                        if (aiNode.Ai.AiPatternMatches(e.Identifier) && aiNode.ValueRegex.IsMatch(e.Value)) {
                            anyValueMatch = true;
                            break;
                        }
                    }

                    if (!anyValueMatch) {
                        issues.Add((ruleKey.ai, new ParserException(ruleKey.ai, 2202, string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_202, ruleKey.ai, $"AI {aiNode.Ai}"), true, ruleKey.ai.Length)));
                        continue;
                    }
                }

                fullMatch = false;
                foreach (var e in entries) {
                    if (aiNode.Ai.AiPatternMatches(e.Identifier)) {
                        fullMatch = true;
                        break;
                    }
                }

                if (!fullMatch) {
                    issues.Add((ruleKey.ai, new ParserException(ruleKey.ai, 2202, string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_202, ruleKey.ai, $"AI {aiNode.Ai}"), true, ruleKey.ai.Length)));
                }

                continue;
            }

            fullMatch = EvaluateComposite(ruleNode, entries);
            if (!fullMatch) {
                issues.Add((ruleKey.ai, new ParserException(ruleKey.ai, 2202, string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_202, ruleKey.ai, "one of multiple AIs"), true, ruleKey.ai.Length)));
            }
        }

        return new ReadOnlyCollection<(string ai, ParserException ex)>(issues);
    }

    private static bool RuleContextMatches(string? keyRegex, string identifier, Semantics semantics)
    {
        if (string.IsNullOrEmpty(keyRegex)) return true;
        var idx = keyRegex.LastIndexOf(':');
        if (idx < 0) return true;
#if NET6_0_OR_GREATER
        var suffix = keyRegex[(idx + 1)..];
#else
        var suffix = keyRegex.Substring(idx + 1);
#endif
        if (identifier == "01") {
            if (Enum.TryParse<GtinSemantics>(suffix, false, out var gtin)) {
                return semantics.GtinSemantics == gtin;
            }
            return true;
        }
        if (identifier == "17") {
            if (Enum.TryParse<ExpiryDateSemantics>(suffix, false, out var exp)) {
                return semantics.ExpiryDateSemantics == exp;
            }
            return true;
        }
        return true;
    }

    private static bool EvaluateComposite(MandatoryNode node, IReadOnlyList<ResolvedAiEntry> entries)
    {
        if (node is AiNode ai)
        {
            foreach (var e in entries)
            {
                if (!ai.Ai.AiPatternMatches(e.Identifier))
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

    //////////private static bool AiPatternMatches(string pattern, string ai)
    //////////{
    //////////    if (string.IsNullOrEmpty(pattern)) return false;
    //////////    if (pattern.Length != ai.Length) return false;
    //////////    for (int i = 0; i < pattern.Length; i++)
    //////////    {
    //////////        char pc = pattern[i];
    //////////        char ac = ai[i];
    //////////        if (pc == 'n' || pc == 'N' || pc == 's')
    //////////        {
    //////////            if (ac < '0' || ac > '9') return false;
    //////////        }
    //////////        else if (pc != ac)
    //////////        {
    //////////            return false;
    //////////        }
    //////////    }
    //////////    return true;
    //////////}

    //////////private static bool AiRegExMatches(string pattern, string ai) {
    //////////    if (string.IsNullOrEmpty(pattern)) return false;
    //////////    try {
    //////////        var aiRegex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);
    //////////        return aiRegex.IsMatch(ai);
    //////////    }
    //////////    catch {
    //////////        return false;
    //////////    }
    //////////}

}