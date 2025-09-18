**NuGet package**

dotnet add package Solidsoft.Reply.Parsers.Gs1Ai

---
 
This library provides a comprehensive validating parser for GS1 Application Identifiers (AIs).  GS1 defines AIs as part of its general specifications.  See [GS1 General Specifications - Standards | GS1]( https://www.gs1.org/standards/barcodes-epcrfid-id-keys/gs1-general-specifications)
The parser validates each AI against the format defined for that AI.  It calls back into an Action for each AI reported to it.  For each AI, parsed data is reported as a Resolved Entity object.  Each Resolved Entity includes a collection of all errors reported while parsing the AI.
The library depends on the Solidsoft.Reply.Parsers.Common library.

## Validation Errors:
For each invalid AI, the parser may output a combination of one or more errors.

The following validation errors are supported:

2001  
No data provided.

2002  
Invalid application identifier {0}.

2003  
Entity is incorrectly terminated with an FNC1.

2004  
Fixed-width entity does not contain sufficient number of characters.

2005  
The value{0} is invalid for AI {1}.

2006  
No entity value provided for AI {0}.

2007  
Validation for AI {0} timed out.

2008 - GS1 identifiers whose last digit is checksum  
The value{0} has an invalid checksum.

2009 - GS1 identifiers with a checksum at position 13 followed by optional text  
The value{0} has an invalid checksum.

2010 - GS1 identifiers with a checksum at position 14 followed by optional text  
The value{0} has an invalid checksum.

2011  
The implied decimal point position (inverse exponent) is specified incorrectly.

2100  
The value{0} does not match the specified pattern for the data element.

