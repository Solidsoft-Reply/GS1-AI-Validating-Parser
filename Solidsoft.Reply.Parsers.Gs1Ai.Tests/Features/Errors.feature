Feature: Errors

A short summary of the feature

No data provided.
Entity is incorrectly terminated with an FNC1.
Fixed-width entity does not contain sufficient number of characters.
--Validation for AI {0} timed out.
--The entity value cannot be null for AI {0}.
The value{0} is invalid for AI {1}.
Invalid application identifier {0}.
The GTIN value{0} has an invalid checksum.
The value{0} does not match the specified pattern for the data element.
--The implied decimal point position (inverse exponent) is specified incorrectly.
--No entity value provided.

Scenario: Trigger a 'no data provided' error
	Given a request to parse data
	When the input submitted to the parser is empty
	Then there should be errors
	    And the errors should include a fatal 2001 error

	Given a request to parse data
	When the input submitted to the parser is null
	Then there should be errors
	    And the errors should include a fatal 2001 error

Scenario: Trigger an 'entity is incorrectly terminated with an FNC1' error
	Given a request to parse data
	When the AI of 01 is incorrectly terminated with an FNC and the value is 12345678901231 
	Then there should be errors
	    And the errors should include a non-fatal 2003 error

Scenario: Trigger a 'fixed-width entity does not contain sufficient number of characters' error
	Given the input is 011234567890123
	When the input to submitted to the parser 
	Then there should be errors
	    And the errors should include a non-fatal 2004 error

	Given the input is 01
	When the input to submitted to the parser 
	Then there should be errors
	    And the errors should include a non-fatal 2004 error

Scenario: Trigger an 'invalid application identifier {0}' error
	Given the input is 38SomeValue
	When the input to submitted to the parser 
	Then there should be errors
	    And the errors should include a fatal 2002 error

Scenario: Trigger a 'the GTIN value{0} has an invalid checksum' error
	Given the input is 0112345678901233
	When the input to submitted to the parser 
	Then there should be errors
	    And the errors should include a non-fatal 2008 error

Scenario: 2 Trigger a 'the GTIN value{0} has an invalid checksum' error
	Given the input is 2531234567890123
		When the input to submitted to the parser 
		Then there should be errors
			And the errors should include a non-fatal 2009 error

Scenario: 3 Trigger a 'the GTIN value{0} has an invalid checksum' error
	Given the input is 800301234567890123
		When the input to submitted to the parser 
		Then there should be errors
			And the errors should include a non-fatal 2010 error

Scenario: Trigger a 'the value{0} does not match the specified pattern for the data element' error
	Given the input is 01ABCDEFGHIJKLMN
	When the input to submitted to the parser 
	Then there should be errors
	    And the errors should include a non-fatal 2100 error
