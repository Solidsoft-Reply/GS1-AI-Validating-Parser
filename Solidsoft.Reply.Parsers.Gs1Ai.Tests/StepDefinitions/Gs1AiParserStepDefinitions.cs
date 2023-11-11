namespace Solidsoft.Reply.Parsers.Gs1Ai.Tests.StepDefinitions;

using Gs1Ai;
using Common;

[Binding]
public sealed class Gs1AiParserStepDefinitions {

    private string _data = string.Empty;
    private readonly IDictionary<int, IResolvedEntity> _resolvedEntites = new Dictionary<int, IResolvedEntity>();
    private int _ai = -1;

    [Given("the input is (.*)")]
    public void GivenTheValueIs(string input) {
        _data = input;
    }

    [When("the input to submitted to the parser")]
    public void WhenTheInputIsSubmittedToTheParser() {
        _resolvedEntites.Clear();
        Parser.Parse(_data, OnResolvedEntity);
    }

    public void OnResolvedEntity(IResolvedEntity resolvedEntity) {
        _resolvedEntites.Add(resolvedEntity.Entity, resolvedEntity);
    }

    [Then("the entity should be (.*)")]
    public void ThenTheEntityShouldBe(int expectedAi) {
        _resolvedEntites.Should().ContainKey(expectedAi);
        _ai = expectedAi;
    }

    [Then("the AI should be (.*)")]
    public void ThenTheAiShouldBe(string expectedAi) {
        _resolvedEntites[_ai].Identifier.Should().Be(expectedAi);
    }

    [Then("the value should be (.*)")]
    public void ThenTheValueShouldBe(string expectedValue) {
        _resolvedEntites[_ai].Value.Should().Be(expectedValue);
    }

    [Then("the data value should be (.*)")]
    public void ThenTheDataValueShouldBe(string expectedDataValue) {
        _resolvedEntites[_ai].DataTitle.Should().Be(expectedDataValue);
    }

    [Then("the description should be (.*)")]
    public void ThenTheDescriptionShouldBe(string expectedDescription) {
        _resolvedEntites[_ai].Description.Should().Be(expectedDescription);
    }

    [Then("the inverse exponent should be (.*)")]
    public void ThenTheInverseExponentShouldBe(int exponent) {
        ((ResolvedApplicationIdentifier)_resolvedEntites[_ai]).InverseExponent.Should().Be(exponent);
    }

    [Then("the sequence number should be (.*)")]
    public void ThenTheSequenceNumberShouldBe(int sequence) {
        ((ResolvedApplicationIdentifier)_resolvedEntites[_ai]).Sequence.Should().Be(sequence);
    }

    [Then("the length of the value should be fixed")]
    public void ThenTheValueShouldBeFixed() {
        ((ResolvedApplicationIdentifier)_resolvedEntites[_ai]).IsFixedWidth.Should().Be(true);
    }

    [Then("the length of the value should be variable")]
    public void ThenTheValueShouldBeVariable() {
        ((ResolvedApplicationIdentifier)_resolvedEntites[_ai]).IsFixedWidth.Should().Be(false);
    }


    [Then("there should be no errors")]
    public void ThenThereShouldBeNoErrors() {
        ((ResolvedApplicationIdentifier)_resolvedEntites[_ai]).IsError.Should().Be(false);
    }

    [Given("a request to parse data")]
    public static void GivenARequestToParseData() {
    }

    [When("the input submitted to the parser is empty")]
    public void WhenTheInputSubmittedToTheParserIsEmpty() {
        _resolvedEntites.Clear();
        Parser.Parse(string.Empty, OnResolvedEntity);
    }

    [When("the input submitted to the parser is null")]
    public void WhenTheInputSubmittedToTheParserIsNull() {
        _resolvedEntites.Clear();
        Parser.Parse(null, OnResolvedEntity);
    }

    [When("the AI of (.*) is incorrectly terminated with an FNC and the value is (.*)")]
    public void WhenTheAiOfIsIncorrectlyTerminatedWithAnFncAndTheValueIs(string ai, string value) {
        _resolvedEntites.Clear();
        Parser.Parse(ai + (char)29 + value, OnResolvedEntity);
    }

    [Then("there should be errors")]
    public void TheThereShouldBeErrors() {
        ((ResolvedApplicationIdentifier)_resolvedEntites[_ai]).IsError.Should().Be(true);
    }

    [Then("the errors should include a fatal (.*) error")]
    public void ThenTheErrorsShouldIncludeAFatalError(int errorNumber) {
        ((ResolvedApplicationIdentifier)_resolvedEntites[_ai]).Exceptions.Should()
            .Contain(e => e.ErrorNumber == errorNumber && e.IsFatal);
    }

    [Then("the errors should include a non-fatal (.*) error")]
    public void ThenTheErrorsShouldIncludeANonFatalError(int errorNumber) {
        ((ResolvedApplicationIdentifier)_resolvedEntites[_ai]).Exceptions.Should()
            .Contain(e => e.ErrorNumber == errorNumber && !e.IsFatal);
    }
}