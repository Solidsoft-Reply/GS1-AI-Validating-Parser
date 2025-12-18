**NuGet package**

dotnet add package Solidsoft.Reply.Parsers.Gs1Ai

---
 
This library provides a comprehensive validating parser for GS1 Application Identifiers (AIs).  GS1 defines AIs as part of its general specifications.  See [GS1 General Specifications - Standards | GS1](https://www.gs1.org/standards/barcodes-epcrfid-id-keys/gs1-general-specifications)
The parser validates each AI against the format defined for that AI.  It calls back into an Action for each AI reported to it.  For each AI, parsed data is reported as a Resolved Entity object.  Each Resolved Entity includes a collection of all errors reported while parsing the AI.
The library depends on the Solidsoft.Reply.Parsers.Common library.



### Validation Errors and Warnings

The GS1 Parser raises errors and warnings in the range of 2000-2499. Each error is categorised as fatal or non-fatal. Warnings are always non-fatal.



* A fatal error is any error that prevents the GS1 Parser from representing a parsed entity that conforms to, or is known to conform to, the GS1 specification for that entity.
* A non-fatal error does not prevent the parser from representing a parsed entity that conforms directly to the GS1 specifications.
* A non-fatal error may fail validation against a third-party or industry-specific GS1 specification. This facilitates the use of test/simulation data that conforms to the GS1 General Specifications  but does not conform to recognised or registered values specified by third-party or industry-specific standards or catalogues.



The following errors and warnings are raised:



#### 2001

Severity: Error

Fatal: Yes

Message: No data provided.

Reason: An attempt was made to parse a string, but the string was empty or null.



#### 2002

Severity: Error

Fatal: Yes

Message: Invalid application identifier {0}.

Reason: The parsed string contains an unrecognised application identifier. NB., the GS1 Parser is reviewed and updated, as required, on an annual basis to ensure support for all application identifiers defined in the GS1 General Specifications. This occurs shortly after publication of the current version of the GS1 General Specifications (typically in January). Further ad hoc updates may occur during the year for ratified AIs published through the GSCN (General Specifications Change Notifications) mechanism. If support for a new ratified AI is required urgently, please contact the authors. The GS1 Parser does not support non-ratified application identifiers.



#### 2003

Severity: Error

Fatal: Yes

Message: Invalid application identifier {0}. AIs must be between two and four digits in length.

Reason: The parsed string contains an unrecognised application identifier containing a single character, only.



#### 2004

Severity: Error

Fatal: No

Message: Entity is incorrectly terminated with an FNC1.

Reason: An entity in the parsed string is incorrectly terminated with an FNC1 (ASCII 29 character). The GS1 Parser will parse the data correctly if no other errors exists.



#### 2005

Severity: Error

Fatal: Yes

Message: A predefined-length entity does not contain a sufficient number of characters.

Reason: The value of a predefined-length entity (see Figure 7.8.5-2 in GS1 General Specifications) in the parsed string has less characters than allowed before its terminator. NB., this is only detected for the last entity in the data string.



#### 2006

Severity: Error

Fatal: Yes

Message: The value{0} is invalid for AI {1}.

Reason: The value of an entity does not pass the validation rules for the given AI. The GS1 Parser uses regular expression patterns to validate entity values, and does not provide detailed semantic error  descriptions.



#### 2007

Severity: Error

Fatal: Yes

Message: No entity value provided for AI {0}.

Reason: No entity value was provided for the given AI.



#### 2008

Severity: Error

Fatal: Yes

Message: Validation for AI {0} timed out.

Reason: The regular expression evaluator timed out while validating the entity value. This may indicate an underlying environmental issue that interferes with data processing and is classified as a fatal error, as if the value had failed validation.



#### 2009

Severity: Error

Fatal: Yes

Message: The value{0} for {1} has an invalid check digit.

Reason: The check digit in the entity value is invalid.



#### 2010

Severity: Error

Fatal: Yes

Message: The implied decimal point position (inverse exponent) for AI {0} is specified incorrectly.

Reason: The character in the AI representing the implied decimal point position is invalid.



#### 2011

Severity: Error

Fatal: No

Message: The value{0} does not conform to the IBAN standard.

Reason: The value of the IBAN (International Bank Account Number) does not conform to ISO 13616, or it is not catalogued for general international use and is not in development.



#### 2012

Severity: Error

Fatal: No

Message: The value{0} does not match the pattern specified for IBAN numbers for {1}.

Reason: The value of the IBAN (International Bank Account Number) does not conform to the pattern specified for the given country.



#### 2013

Severity: Error

Fatal: No

Message: The value{0} matches the pattern specified for IBAN numbers for {1} that is currently in development and not catalogued for general international use.

Reason: The value of the IBAN (International Bank Account Number) is aspirational. It matches a pattern that is not yet specified for use by the given country.



#### 2014

Severity: Error

Fatal: No

Message: The IBAN value{0} contains incorrect check digits.

Reason: The check digits in the IBAN number are incorrect with respect to ISO/IEC 7064:2003 (MOD-97-10).



#### 2015

Severity: Error

Fatal: No

Message: The value{0} does not match the pattern specified for North American coupons.

Reason: The value of the North American Coupon does not conform to the industry standard developed by the Joint Industry Coupon Committee (JICC) together with GS1 US. This standard is defined in 'North American Coupon Application Guideline using GS1 DataBar Expanded Symbols' available from GS1 US.



#### 2016

Severity: Error

Fatal: No

Message: The value{0} does not match the pattern specified for North American positive offer file coupons.

Reason: The value of the North American positive offer file coupon code does not conform to industry specifications developed by The Coupon Bureau and the Joint Industry Coupon Committee (JICC).



#### 2017

Severity: Warning

Fatal: No

Message: The value{0} is not a recognised UN/ECE or GS1 package type code.

Reason: The value of the package type code does not conform to UN/CEFACT Recommendation No. 21 – Codes for Types of Cargo, Packages and Packaging Materials. In addition, it is not defined defined in the GS1 Packaging Type Code List.



#### 2100

Severity: Error

Fatal: Yes

Message: The value{0} does not match the specified pattern for the data element.

Reason: The value is invalid with respect to the pattern specified for the AI in the GS1 General Specifications.



#### 2201

Severity: Error

Fatal: Yes

Message: Invalid data relationship found between AI {0} and AI {1}.

Reason: The data contains a combination of two AIs that must not appear together in the same barcode or data structure.



#### 2202

Severity: Error

Fatal: Yes

Message: Mandatory data relationship not found between AI {0} and {1}.

Reason: The data contains an AI that must always appear together with one or more AIs in the same barcode or data structure. However, one or more of these additional AIs is not present in the data.
