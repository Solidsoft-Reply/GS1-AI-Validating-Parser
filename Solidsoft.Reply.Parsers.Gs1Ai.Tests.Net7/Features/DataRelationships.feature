Feature: DataRelationships

A short summary of the feature

@tag1
Scenario: Validate data relationships between two data elements
	Given the input is 011234567890123110ABC123
	When the input to submitted to the parser and data relationship tests are required
	Then we should detect entity 01
	And we should detect entity 10
	And there should be no errors

Scenario: Detect invalid data relationships between two data elements
	Given the input is 0109506000134376023506091751986210ABC123
	When the input to submitted to the parser and data relationship tests are required
	Then we should detect entity 01
	And we should detect entity 02
	And we should detect entity 10
	And there should be invalid pairs


