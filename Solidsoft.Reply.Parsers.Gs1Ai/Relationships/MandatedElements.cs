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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
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
internal sealed class MandatedElements : ReadOnlyDictionary<(string ai, string regex), (string description, MandatoryNode node)>
{
    private static readonly Dictionary<(string ai, string regex), (string description, MandatoryNode node)> Rules = new()
    {
        { ("01", "^0\\d{13}$:VariableMeasure"), Make(new OrNode([new AiNode("30"), new AiNode("3nnn")])) },
        { ("01", "^9\\d{13}$"), Make(new OrNode([new AiNode("30"), new AiNode("3nnn"), new AiNode("8001"), new AiNode("242")])) },
        { ("01", "^9\\d{13}$:VariableMeasure"), Make(new OrNode([new AiNode("30"), new AiNode("3nnn"), new AiNode("8001")])) },
        { ("02", "^9\\d{13}$"), Make(new OrNode([new AiNode("30"), new AiNode("3nnn"), new AiNode("8001")])) },
        { ("01", "^9\\d{13}$:Custom"), Make(new AiNode("242")) },
        ////{ ("03", string.Empty), Make(new AiNode("242")) },  Removed from standard
        { ("02", string.Empty), Make(new AndNode([new AiNode("00"), new AiNode("37")])) },
        { ("10", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("03"), new AiNode("8006"), new AiNode("8026")])) },
        { ("11", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")])) },
        { ("13", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")])) },
        { ("15", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")])) },
        { ("16", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")])) },
        { ("17", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")])) },
        { ("12", string.Empty), Make(new AndNode([new AiNode("8020"), new AiNode("415")])) },
        { ("17", ":Coupon"), Make(new AiNode("255")) },
        { ("20", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")])) },
        { ("21", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("03"), new AiNode("8006")])) },
        { ("22", string.Empty), Make(new AiNode("01")) },
        { ("235", string.Empty), Make(new AiNode("01")) },
        { ("240", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")])) },
        { ("241", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")])) },
        { ("242", string.Empty), Make(new XorNode([new AiNode("01", "^9\\d{13}$"), new AiNode("02", "^9\\d{13}$"), new AiNode("8006", "^9\\d{13}\\d{2}\\d{2}$"), new AiNode("8026", "^9\\d{13}\\d{2}\\d{2}$")])) },
        { ("243", string.Empty), Make(new AiNode("01")) },
        { ("250", string.Empty), Make(new AndNode([new XorNode([new AiNode("01"), new AiNode("8006")]), new AiNode("21")])) },
        { ("251", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("8006")])) },
        { ("254", string.Empty), Make(new AiNode("414")) },
        { ("30", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("^3(1[0-6]|2\\d|5[0-267]|6[014-6])\\d$", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("^3(3[0-6]|4\\d|5[3-5]|6[237-9])\\d$", string.Empty), Make(new OrNode([new AiNode("01"), new AiNode("02")])) },
        { ("337n", string.Empty), Make(new AiNode("01")) },
        { ("37", string.Empty), Make(new AndNode([new AiNode("00"), new XorNode([new AiNode("02"), new AiNode("8026")])])) },
        { ("390n", string.Empty), Make(new AndNode([new AiNode("8020"), new AiNode("415")])) },
        { ("390n", ":CouponValue"), Make(new AiNode("255")) },
        { ("391n", string.Empty), Make(new AndNode([new AiNode("8020"), new AiNode("415")])) },
        { ("392n", string.Empty), Make(new AndNode([new AiNode("01"), new XorNode([new AiNode("30"), new AiNode("^3(1[0-6]|2\\d|5[0-267]|6[014-6])\\d$")])])) },
        { ("393n", string.Empty), Make(new AndNode([new AiNode("01"), new XorNode([new AiNode("30"), new AiNode("^3(1[0-6]|2\\d|5[0-267]|6[014-6])\\d$")])])) },
        { ("394n", string.Empty), Make(new AiNode("255")) },
        { ("395n", string.Empty), Make(new AndNode([new AiNode("01"), new XorNode([new AiNode("30"), new AiNode("^3(1[0-6]|2\\d|5[0-267]|6[014-6])\\d$")])])) },
        { ("403", string.Empty), Make(new AiNode("00")) },
        { ("415", string.Empty), Make(new AiNode("8020")) },
        { ("422", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")])) },
        { ("423", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("424", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("425", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("426", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("427", string.Empty), Make(new AndNode([new XorNode([new AiNode("01"), new AiNode("02")]), new AiNode("422")])) },
        { ("430N", string.Empty), Make(new AiNode("00")) },
        { ("4303", string.Empty), Make(new AndNode([new AiNode("4302"), new AiNode("00")])) },
        { ("4309", string.Empty), Make(new AiNode("00")) },
        { ("431N", string.Empty), Make(new AiNode("00")) },
        { ("4313", string.Empty), Make(new AndNode([new AiNode("4312"), new AiNode("00")])) },
        { ("^432[0-6]$", string.Empty), Make(new AiNode("00")) },
        { ("4330", string.Empty), Make(new AiNode("00")) },
        { ("4331", string.Empty), Make(new AiNode("00")) },
        { ("4332", string.Empty), Make(new AiNode("00")) },
        { ("4333", string.Empty), Make(new AiNode("00")) },
        { ("7001", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02"), new AiNode("8006"), new AiNode("8026")])) },
        { ("7002", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("7003", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("7004", string.Empty), Make(new AndNode([new AiNode("01"), new AiNode("10")])) },
        { ("7005", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("7006", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("7007", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("7008", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("7009", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("7010", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("7011", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("703s", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("710", string.Empty), Make(new AiNode("01")) },
        { ("711", string.Empty), Make(new AiNode("01")) },
        { ("712", string.Empty), Make(new AiNode("01")) },
        { ("713", string.Empty), Make(new AiNode("01")) },
        { ("714", string.Empty), Make(new AiNode("01")) },
        { ("715", string.Empty), Make(new AiNode("01")) },
        { ("716", string.Empty), Make(new AiNode("01")) },
        { ("717", string.Empty), Make(new AiNode("01")) },
        { ("7020", string.Empty), Make(new AndNode([new XorNode([new AiNode("01"), new AiNode("8006")]), new AiNode("416")])) },
        { ("7021", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("8006")])) },
        { ("7022", string.Empty), Make(new AndNode([new XorNode([new AiNode("01"), new AiNode("8006")]), new AiNode("7021")])) },
        { ("7041", string.Empty), Make(new AiNode("00")) },
        { ("723s", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("8004")])) },
        { ("7240", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("8006")])) },
        { ("7241", string.Empty), Make(new XorNode([new AiNode("8017"), new AiNode("8018")])) },
        { ("7242", string.Empty), Make(new XorNode([new AiNode("8017"), new AiNode("8018")])) },
        { ("7250", string.Empty), Make(new AiNode("8018")) },
        { ("7251", string.Empty), Make(new AiNode("8018")) },
        { ("7252", string.Empty), Make(new AiNode("8018")) },
        { ("7257", string.Empty), Make(new AiNode("8018")) },
        { ("7259", string.Empty), Make(new AiNode("8018")) },
        { ("7253", string.Empty), Make(new XorNode([new AiNode("8017"), new AiNode("8018")])) },
        { ("7254", string.Empty), Make(new XorNode([new AiNode("8017"), new AiNode("8018")])) },
        { ("7255", string.Empty), Make(new XorNode([new AiNode("8017"), new AiNode("8018")])) },
        { ("7256", string.Empty), Make(new XorNode([new AiNode("8017"), new AiNode("8018")])) },
        { ("7258", string.Empty), Make(new AndNode([new AiNode("8018"), new AiNode("7259")])) },
        { ("8001", string.Empty), Make(new AiNode("01")) },
        { ("8005", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("8007", string.Empty), Make(new AndNode([new AiNode("8020"), new AiNode("415")])) },
        { ("8008", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("02")])) },
        { ("8009", string.Empty), Make(new OrNode([new AiNode("01"), new AiNode("00")])) },
        { ("8011", string.Empty), Make(new AiNode("8010")) },
        { ("8012", string.Empty), Make(new XorNode([new AiNode("01"), new AiNode("8006")])) },
        { ("8014", string.Empty), Make(new AiNode("01")) },
        { ("8019", string.Empty), Make(new XorNode([new AiNode("8017"), new AiNode("8018")])) },
        { ("8020", string.Empty), Make(new AiNode("415")) },
        { ("8026", string.Empty), Make(new AndNode([new AiNode("00"), new AiNode("37")])) },
        { ("8030", string.Empty), Make(new XorNode([new AndNode([new AiNode("01"), new AiNode("21")]), new AndNode([new AiNode("8006"), new AiNode("21")]), new AndNode([new AiNode("8010"), new AiNode("8011")]), new AiNode("8003"), new AiNode("8004"), new AiNode("8017"), new AiNode("8018"), new AiNode("00"), new AiNode("253"), new AiNode("255")])) },
        { ("8040", string.Empty), Make(new AndNode([new AiNode("01"), new AiNode("21")])) },
        { ("8041", string.Empty), Make(new AndNode([new AiNode("01"), new AiNode("21"), new AiNode("8040")])) },
        { ("8042", string.Empty), Make(new AndNode([new AiNode("01"), new AiNode("21"), new AiNode("8041")])) },
        { ("8043", string.Empty), Make(new AndNode([new AiNode("01"), new AiNode("21")])) },
        { ("8111", string.Empty), Make(new AiNode("255")) },
        { ("8200", string.Empty), Make(new AiNode("01")) },
    };

    private static (string description, MandatoryNode node) Make(MandatoryNode node) => (Describe(node), node);

    private static string Describe(MandatoryNode node) {
        return node switch {
            AiNode ai => $"AI {ai.Ai}",
            CompositeNode c when c.NodeType == MandatoryNodeType.And => string.Join($" {Resources.GS1_Error_202_and} ", c.Children.Select(Describe)),
            CompositeNode c when c.NodeType == MandatoryNodeType.Or => $"{Resources.GS1_Error_202_or} ({string.Join(", ", c.Children.Select(Describe))})",
            CompositeNode c when c.NodeType == MandatoryNodeType.Xor => $"{Resources.GS1_Error_202_xor} ({string.Join(", ", c.Children.Select(Describe))})",
            _ => string.Empty
        };
    }

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
    /// <param name="semantics">The AI semantics when evaluating rules (e.g., for AI 01, General, VariableMeasure, or Custom).</param>
    /// <returns>A read-only list of tuples containing the AI and the associated parser exception for any issues found.</returns>
    public static IReadOnlyList<(string ai, ParserException ex)> Test(Semantics semantics)
    {
        var issues = new List<(string ai, ParserException ex)>();
        var entries = ResolvedAiList.Current;
        var matched = new List<(string ai, string description, MandatoryNode node)>();

        foreach (var entry in entries) {

            // Find all rules that match the current entry
            foreach (var rule in Rules) {
                var ruleKey = rule.Key;
                var ruleValue = rule.Value;
                var ruleNode = ruleValue.node;
                var ruleDescription = ruleValue.description;

                // Special handling for variable measure and coupon/custom GTIN contexts (DRY)
                bool RuleContextMatches(string? ruleRegex, string identifier)
                {
                    var suffixIndex = ruleRegex?.LastIndexOf(':') ?? -1;
                    var ctxSuffix = suffixIndex < 0
                        ? string.Empty
#if NET6_0_OR_GREATER
                        : ruleRegex?[(suffixIndex + 1)..];
#else
                        : ruleRegex?.Substring(suffixIndex + 1);
#endif
                    return identifier switch {
                        "01" => ctxSuffix switch
                        {
                            nameof(GtinSemantics.VariableMeasure) => semantics.GtinSemantics == GtinSemantics.VariableMeasure,
                            nameof(GtinSemantics.Custom) => semantics.GtinSemantics == GtinSemantics.Custom,
                            _ => true
                        },
                        "17" => ctxSuffix switch
                        {
                            nameof(ExpiryDateSemantics.TradeItem) => semantics.ExpiryDateSemantics == ExpiryDateSemantics.TradeItem,
                            nameof(ExpiryDateSemantics.Coupon) => semantics.ExpiryDateSemantics == ExpiryDateSemantics.Coupon,
                            _ => semantics.ExpiryDateSemantics == ExpiryDateSemantics.TradeItem
                        },
                        _ when identifier.StartsWith("390") => ctxSuffix switch
                        {
                            nameof(AmountPayableSemantics.Invoice) => semantics.AmountPayableSemantics == AmountPayableSemantics.Invoice,
                            nameof(AmountPayableSemantics.CouponValue) => semantics.AmountPayableSemantics == AmountPayableSemantics.CouponValue,
                            _ => semantics.AmountPayableSemantics == AmountPayableSemantics.Invoice
                        },
                        _ => true
                    };
                }

                // Skip rules that do not match the current semantic context
                if (!RuleContextMatches(ruleKey.regex, entry.Identifier)) {
                    continue;
                }

                // Check if the resolved AI matches the rule's AI pattern
                if (!AiPatternMatches(ruleKey.ai, entry.Identifier)) continue;

                // Extract the rule's regular expression, if provided.
                Regex? valueRegex = null;
                if (!string.IsNullOrEmpty(ruleKey.regex)) {
                    var colonIndex = ruleKey.regex?.LastIndexOf(':');
#if NET6_0_OR_GREATER
                    var pattern = colonIndex >= 0 ? ruleKey.regex?[..(colonIndex ?? 0)] : ruleKey.regex;
#else
                    var pattern = colonIndex >= 0 ? ruleKey.regex?.Substring(0, colonIndex ?? 0) : ruleKey.regex;
#endif
                    valueRegex = !string.IsNullOrEmpty(pattern)
                        ? new Regex(pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant)
                        : null;
                }

                if (valueRegex is not null && !valueRegex.IsMatch(entry.Value)) continue;

                matched.Add((entry.Identifier, ruleDescription, ruleNode));
            }
        }

        foreach (var (ai, description, ruleNode) in matched) {
            bool fullMatch;

            if (ruleNode is AiNode aiNode) {
                // Look for a resolved AI that matches the AI node directly, including a regualar expression
                if (aiNode.ValueRegex is not null) {
                    var anyValueMatch = false;
                    foreach (var e in entries) {
                        if (AiPatternMatches(aiNode.Ai, e.Identifier) && aiNode.ValueRegex.IsMatch(e.Value)) {
                            anyValueMatch = true;
                            break;
                        }
                    }

                    // If no match exists, report the issue
                    if (!anyValueMatch) {
                        issues.Add((ai, new ParserException(ai, 2202, string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_202, ai, description), true, ai.Length)));
                        continue;
                    }
                }

                // If no issues arose for nodes with value regex, check for AI pattern match only
                fullMatch = false;
                foreach (var e in entries) {
                    if (AiPatternMatches(aiNode.Ai, e.Identifier)) {
                        fullMatch = true;
                        break;
                    }
                }

                // If no match exists, report the issue
                if (!fullMatch) {
                    issues.Add((ai, new ParserException(ai, 2202, string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_202, ai, description), true, ai.Length)));
                }

                continue;
            }

            // Now evaluate composite nodes
            fullMatch = EvaluateComposite(ruleNode, entries);
            if (!fullMatch) {
                issues.Add((ai, new ParserException(ai, 2202, string.Format(CultureInfo.CurrentCulture, Resources.GS1_Error_202, ai, description), true, ai.Length)));
            }
        }
        return new ReadOnlyCollection<(string ai, ParserException ex)>(issues);
    }

    /// <summary>
    /// Evaluates the composite mandatory ruleNode against the provided resolved AI entries.
    /// </summary>
    /// <param name="node">The mandatory ruleNode to evaluate, which may be a composite or AI ruleNode.</param>
    /// <param name="entries">The list of resolved AI entries to test against the mandatory ruleNode.</param>
    /// <returns>true if the entries satisfy the mandatory ruleNode's requirements; otherwise, false.</returns>
    private static bool EvaluateComposite(MandatoryNode node, IReadOnlyList<ResolvedAiEntry> entries)
    {
        return node switch
        {
            AiNode aiNode =>
                EvaluateAiNode(aiNode),
            CompositeNode compositeNode when compositeNode.NodeType == MandatoryNodeType.And =>
                compositeNode.Children.All(child => EvaluateComposite(child, entries)),
            CompositeNode compositeNode when compositeNode.NodeType == MandatoryNodeType.Or =>
                compositeNode.Children.Any(child => EvaluateComposite(child, entries)),
            CompositeNode compositeNode when compositeNode.NodeType == MandatoryNodeType.Xor =>
                compositeNode.Children.Count(child => EvaluateComposite(child, entries)) == 1,
            _ => false,
        };

        bool EvaluateAiNode(AiNode ai) {
            foreach (var e in entries) {
                if (!AiPatternMatches(ai.Ai, e.Identifier)) continue;
                if (ai.ValueRegex is null || ai.ValueRegex.IsMatch(e.Value)) return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Determines whether the specified AI string matches the given pattern, where certain pattern characters represent
    /// digit placeholders.
    /// </summary>
    /// <remarks>A pattern character of 'n', 'N', or 's' matches any single digit ('0'-'9') in the
    /// corresponding position of the AI string. All other characters in the pattern must match exactly. The method
    /// returns false if the pattern is null, empty, or if the pattern and AI strings are of different
    /// lengths.</remarks>
    /// <param name="pattern">The pattern string to match against. Characters 'n', 'N', or 's' in the pattern represent digit placeholders;
    /// all other characters must match exactly. Cannot be null or empty.</param>
    /// <param name="ai">The AI string to test for a match against the pattern. Must be the same length as the pattern.</param>
    /// <returns>true if the AI string matches the pattern, treating 'n', 'N', or 's' as digit placeholders; otherwise, false.</returns>
    private static bool AiPatternMatches(string pattern, string ai)
    {
        if (string.IsNullOrEmpty(pattern)) return false;
        if (pattern.Length < ai.Length) return false;

        try {
            if (pattern.StartsWith("^") && new Regex(pattern).Match(ai).Success) {
                return true;
            }

            for (int i = 0; i < pattern.Length; i++) {
                char pc = pattern[i];
                char ac = ai[i];

                var digitMatch = pc switch {
                    _ when pc == 'n' && (ac >= '0' || ac <= '9') => true,
                    _ when pc == 'N' && (ac >= '0' || ac <= '9') => true,
                    _ when pc == 's' && (ac >= '0' || ac <= '9') => true,
                    _ when pc == ac => true,
                    _ => false
                };

                if (!digitMatch) return digitMatch;
            }

            return true;
        }
        catch {
            return false;
        }
    }
}