using Solidsoft.Reply.Parsers.Common;
using Solidsoft.Reply.Parsers.Gs1Ai.Relationships;

namespace Solidsoft.Reply.Parsers.Gs1Ai.Tests.Net5.StepDefinitions;
[Binding]
public sealed class Gs1AiParserStepDefinitions {

    private string _data = string.Empty;
    private readonly List<string> _barcodes = [];
    private GtinSemantics _gtinSemantics = GtinSemantics.General;
    private ExpiryDateSemantics _expiryDateSemantics = ExpiryDateSemantics.TradeItem;
    private AmountPayableSemantics _amountPayableSemantics = AmountPayableSemantics.Invoice;

    private readonly IDictionary<int, IResolvedEntity> _resolvedEntities = new Dictionary<int, IResolvedEntity>();
    private readonly IDictionary<string, IResolvedEntity> _resolvedAIs = new Dictionary<string, IResolvedEntity>();
    private readonly List<IResolvedEntity> _dataRelationshipExceptions = [];
    private string _ai = "";

    [Given("the input is (.*)")]
    public void GivenTheValueIs(string input) {
        _data = input.Replace("[GS]", "\u001d");
        _gtinSemantics = GtinSemantics.General;
        _expiryDateSemantics = ExpiryDateSemantics.TradeItem;
        _amountPayableSemantics = AmountPayableSemantics.Invoice;
    }

    [Given("the input for barcode (.*) is (.*)")]
    public void GivenTheInputForBarcodeIs(int barcodeNumber, string input) {
        _barcodes.Add(input.Replace("[GS]", "\u001d"));
        _gtinSemantics = GtinSemantics.General;
        _expiryDateSemantics = ExpiryDateSemantics.TradeItem;
        _amountPayableSemantics = AmountPayableSemantics.Invoice;
    }

    [Given("the semantics are (.*)")]
    public void GivenTheSemanticsIs(string input) {
        var parts = input.Split('.', 2);
        (string ai, string semantics) = parts.Length == 2 ? (parts[0], parts[1]) : (parts[0], string.Empty);
        _gtinSemantics = ai == "01"
            ? Enum.Parse<GtinSemantics>(semantics)
            : GtinSemantics.General;
        _expiryDateSemantics = ai == "17"
            ? Enum.Parse<ExpiryDateSemantics>(semantics)
            : ExpiryDateSemantics.TradeItem;
        _amountPayableSemantics = ai.StartsWith("390")
            ? Enum.Parse<AmountPayableSemantics>(semantics)
            : AmountPayableSemantics.Invoice;
    }

    [When("the input to submitted to the parser")]
    public void WhenTheInputIsSubmittedToTheParser() {
        _resolvedEntities.Clear();
        _resolvedAIs.Clear();
        _dataRelationshipExceptions.Clear();
        Parser.Parse(_data, OnResolvedEntity);
    }

    [When("the input to submitted to the parser and data relationship tests are required")]
    public void WhenTheInputIsSubmittedToTheParserAndDataRelationshipTestsAreRequired() {
        _resolvedEntities.Clear();
        _resolvedAIs.Clear();
        _dataRelationshipExceptions.Clear();
        Parser.Parse(_data, OnResolvedEntity, relationshipTests: DataRelationshipTests.All, semantics: new(gtinSemantics: _gtinSemantics, expiryDateSemantics: _expiryDateSemantics, amountPayableSemantics: _amountPayableSemantics));
    }

    [When("the barcodes are submitted to the parser")]
    public void WhenTheBarcodesAReSubmittedToTheParser() {
        _resolvedEntities.Clear();
        _resolvedAIs.Clear();
        _dataRelationshipExceptions.Clear();
        Parser.ParseMulti(_barcodes, OnResolvedEntity);
        _barcodes.Clear();
    }

    public void OnResolvedEntity(IResolvedEntity resolvedEntity) {
        if (resolvedEntity.Entity < 0
            && resolvedEntity.Exceptions.Any(e => e.ErrorNumber == 2201 || e.ErrorNumber == 2203)) {
            _dataRelationshipExceptions.Add(resolvedEntity);
            return;
        }

        if (resolvedEntity.Exceptions.Any(e => e.ErrorNumber == 2202)) {
            _dataRelationshipExceptions.Add(resolvedEntity);
        }

        if (!_resolvedEntities.TryGetValue(resolvedEntity.Entity, out _))
            _resolvedEntities.Add(resolvedEntity.Entity, resolvedEntity);

        if (!_resolvedAIs.TryGetValue(resolvedEntity.Identifier, out _))
            _resolvedAIs.Add(resolvedEntity.Identifier, resolvedEntity);
            _ai = resolvedEntity.Identifier;

    }

    [Then("the entity should be (.*)")]
    public void ThenTheEntityShouldBe(int expectedEntity) {
        _resolvedEntities.Should().ContainKey(expectedEntity);
    }

    [Then("the AI should be (.*)")]
    [Then("we should detect AI (.*)")]
    public void ThenTheAiShouldBe(string expectedAi) {
        _resolvedAIs[expectedAi].Identifier.Should().Be(expectedAi);
        _ai = expectedAi;
    }

    [Then("the value should be (.*)")]
    public void ThenTheValueShouldBe(string expectedValue) {
        _resolvedAIs[_ai].Value.Should().Be(expectedValue);
    }

    [Then("the data value should be (.*)")]
    public void ThenTheDataValueShouldBe(string expectedDataValue) {
        _resolvedAIs[_ai].DataTitle.Should().Be(expectedDataValue);
    }

    [Then("the description should be (.*)")]
    public void ThenTheDescriptionShouldBe(string expectedDescription) {
        _resolvedAIs[_ai].Description.Should().Be(expectedDescription);
    }

    [Then("the inverse exponent should be (.*)")]
    public void ThenTheInverseExponentShouldBe(int exponent) {
        ((ResolvedApplicationIdentifier)_resolvedAIs[_ai]).InverseExponent.Should().Be(exponent);
    }

    [Then("the sequence number should be (.*)")]
    public void ThenTheSequenceNumberShouldBe(int sequence) {
        ((ResolvedApplicationIdentifier)_resolvedAIs[_ai]).Sequence.Should().Be(sequence);
    }

    [Then("the length of the value should be fixed")]
    public void ThenTheValueShouldBeFixed() {
        ((ResolvedApplicationIdentifier)_resolvedAIs[_ai]).IsFixedWidth.Should().Be(true);
    }

    [Then("the length of the value should be variable")]
    public void ThenTheValueShouldBeVariable() {
        ((ResolvedApplicationIdentifier)_resolvedAIs[_ai]).IsFixedWidth.Should().BeFalse();
    }

    [Then("there should be no errors")]
    public void ThenThereShouldBeNoErrors() {
        foreach (var entity in _resolvedAIs) {
            entity.Value.IsError.Should().BeFalse();
        }
    }

    [Given("a request to parse data")]
    public static void GivenARequestToParseData() {
        // Intentionally empty - this is provided for semantics, only.
    }

    [When("the input submitted to the parser is empty")]
    public void WhenTheInputSubmittedToTheParserIsEmpty() {
        _resolvedAIs.Clear();
        Parser.Parse(string.Empty, OnResolvedEntity);
    }

    [When("the input submitted to the parser is null")]
    public void WhenTheInputSubmittedToTheParserIsNull() {
        _resolvedAIs.Clear();
        Parser.Parse(null, OnResolvedEntity);
    }

    [When("the AI of (.*) is incorrectly terminated with an FNC1 and the value is (.*)")]
    public void WhenTheAiOfIsIncorrectlyTerminatedWithAnFncAndTheValueIs(string ai, string value) {
        _resolvedAIs.Clear();
        Parser.Parse(ai + value + (char)29, OnResolvedEntity);
    }

    [Then("there should be errors")]
    public void TheThereShouldBeErrors() {
        ((ResolvedApplicationIdentifier)_resolvedAIs[_ai]).IsError.Should().Be(true);
    }

    [Then("there should be invalid pairs")]
    public void TheThereShouldBInvalidPairs() {
        _dataRelationshipExceptions
            .Should()
            .Contain(ent => ent.Exceptions != null && ent.Exceptions.Any(ex => ex.ErrorNumber == 2201));
    }

    [Then("there should be invalid duplicate AI pairs")]
    public void TheThereShouldBInvalidDuplicateAIPairs() {
        _dataRelationshipExceptions
            .Should()
            .Contain(ent => ent.Exceptions != null && ent.Exceptions.Any(ex => ex.ErrorNumber == 2202));
    }

    [Then("the errors should include a fatal (.*) error")]
    public void ThenTheErrorsShouldIncludeAFatalError(int errorNumber) {
        if (string.IsNullOrEmpty(_ai)) {
            foreach (var entity in _resolvedAIs.Values) {
                if (entity.Exceptions.Any(e => e.ErrorNumber == errorNumber && e.IsFatal)) {
                    return;
                }
            }
        }

        ((ResolvedApplicationIdentifier)_resolvedAIs[_ai]).Exceptions.Should()
            .Contain(e => e.ErrorNumber == errorNumber && e.IsFatal);
    }

    [Then("the errors should include a non-fatal (.*) error")]
    public void ThenTheErrorsShouldIncludeANonFatalError(int errorNumber) {
        ((ResolvedApplicationIdentifier)_resolvedAIs[_ai]).Exceptions.Should()
            .Contain(e => e.ErrorNumber == errorNumber && !e.IsFatal);
    }
}