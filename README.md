This library provides a standards-compliant and comprehensive validating parser for GS1 Application Identifiers (AIs). GS1 defines AIs as part of its General Specifications. See [GS1 General Specifications - Standards | GS1](https://www.gs1.org/standards/barcodes-epcrfid-id-keys/gs1-general-specifications)

The parser validates each AI against the rules defined by GS1 for that AI. It calls back into an Action for each AI reported to it. For each AI, parsed data is reported as a Resolved Application Identifier object. Each Resolved Application Identifier includes a collection of all errors reported while parsing the AI.

The library depends on the Solidsoft.Reply.Parsers.Common library.

## NuGet package

**dotnet add package Solidsoft.Reply.Parsers.Gs1Ai**

The NuGet package provides support for a wide range of .NET versions, including modern versions of .NET, the .NET Framework, and any older (pre-.NET 6)version of NET that supports .NET Standard 2.0.

# Using the Library

The Parser class provides a simple API for parsing data containing a sequence of elements. Each element string in the sequence has an Application Identifier (AI) between 2-4 digits in length and a numeric or alphanumeric data value of fixed or variable length, depending on the AI’s definition.

Sequences of elements strings may be provided in one of two formats:

1. FNC1 Format: Each element string is delimited by an ASCII 29 character, designated as FNC1 (Function Code 1), except when it occurs as the final element string in the sequence or it is an element string with a pre-defined length listed in Figure 7.8.5-2 in GS1 General Specifications.
2. Human-Readable Information (HRI) Format: The AI in each element string in enclosed in parentheses. No delimiters are used.

A single sequence of element strings, typically representing the contents of a single barcode, may be passed to the Parse() or ParseEx() methods as a string or a ReadOnlySpan<char>. The choice of Parse() overloads or the ParseEx() method depends on a balance of performance against ease of use. See the section on performance below for additional guidance.

Each time the Parser resolves an element string within the sequence, it calls back to the client code. The call-back Action (Parse method) or delegate (ParseEx method) accepts a ResolvedApplicationIdentifier record (Parse method) or ResolvedApplicationIdentifierRef struct (ParseEx method) containing information about the resolved element. A single call to Parse() or ParseEx() may therefore result in multiple call-backs. If parsing of any individual element string fails for any reason, the parser does not throw exceptions. Instead, it passes a collection of exceptions back to the client code as part of the record or struct. In effect, the client code is event-driven. Each event represents a single resolved element string.

### Setting the Initial Position

In some advanced scenarios, you may wish to specify the initial position at which parsing of the element string sequence starts. You can do this using the optional initialPosition parameter. To parse the sequence correctly from a character position greater than 0, you must ensure that the position corresponds to the index in the character array of the initial character of an AI, or to an '(' character when using HRI format.

## Parsing multiple barcodes at once

In some scenarios, you may prefer to provide the contents of multiple sequences of element string in a single call to the Parser API. This can be done using an overload of the Parse() method. Typically, each sequence represents an individual barcode. The Parse() method accepts a list of element string sequences and processes them in the provided order. The Parse() method calls back to the client code using the provided Action. The ResolvedApplicationIdentifier record passed to the action specifies the index of the current element string sequence using the Index property. This is the index of the sequence in the list passed to Parse(). Do not confuse the Index property with the Sequence property. The Sequence number is used for AIs that specify a sequence using the fourth AI digit.

By default, the Parse() method assumes that the list of element string sequences represents data associated with a single physical entity (e.g., a box or carton). This assumption influences the application of data relationship rules (see below). Other scenarios are supported using the optional scenario parameter. Supported scenarios are:

* **Single Physical Entity** (default): the list of element string sequences (e.g., barcodes) appear on a single physical entity.
* **Single Physical Entity per Sequence**: each element string sequence (e.g., barcode) appears uniquely on a single physical entity.
* **Arbitrary**: no assumption is made regarding the correspondence of element string sequences to physical entities.

## Data Relationship rules

The Parser library validates each individual element string according to rules specified in GS1 General Specifications. In addition, it may also validate the set of element strings within a single sequence or across multiple sequences according to data relationship rules specified by GS1. These rules specify the following:

* **invalid pairs** of AIs that should not appear together
* **mandatory associations** between two or more AIs.

The data relationship rules apply to all element strings associated with a single physical entity, even when the element strings are represented in multiple sequences (e.g., multiple barcodes on the same carton, box or pallet). For this reason, and also to allow maximum performance, data relationship rules are not, by default, performed when calling the Parse() or ParseEx() methods. Use the optional relationshipTests parameter to control the application of data relationship rules when using these methods:

* **None** (default): do not run any data relationship tests.
* **Invalid Pairs**: run only invalid pairs tests.
* **All**: run both invalid pairs and mandatory association tests.

When parsing multiple barcodes at once, the Parse() method applies data relationship rules according to the selected scenario. By default, Parse() assumes that all element string sequences in the list are associated with a single physical entity. In this case, it performs both the invalid pairs and mandatory association tests across the entire set of element string sequences.

In the 'Single Physical Entity per Sequence' scenario, the invalid pairs and mandatory association tests are applied to each element string sequence separately. They are not applied across the set of sequences. This is equivalent to repeatedly calling Parse() on a single element string sequence with the relationshipTests parameter set to All.

In the Arbitrary scenario, no assumption is made regarding the correspondence of element string sequences to physical entities. Therefore, no data relationship tests are applied.

It is possible to apply the invalid pairs tests, only, to each individual element string sequence. Set the relationshipTests parameter to any integer value except 0, 1 or 2.

### Semantics

Some data relationship tests defined by GS1 depend on additional semantics associated with certain AIs. These semantics cannot be determined from the element string. To apply these tests, you must specify the relevant AI semantics within the call to Parse() or ParseEx() using the optional semantics parameter. At the time of writing, these additional semantics affect AI 01 (GTIN), AI 17 (Expiry Date) and 390n AIs (Amount Payable - single monetary area).

* **AI 01** (GTIN): by default, GTINs are treated as general trade item identifiers. However, you can assert that they are specifically variable measure trade items or custom trade items.
* **AI 17** (Expiry Date): by default, expiry dates are assumed to apply to trade items. However, you can assert that they specifically apply to coupons.
* **390n AIs** (Amount payable - single monetary area): by default, AIs starting '390' are assumed to be amounts payable on an invoice slip. However, you can assert that they refer to the value of a coupon.

By setting explicit semantics, you change which rules match these AIs when performing mandatory association data relationship tests. See section 4.13.2 in GS1 General Specification for further details.

## Global Company Prefixes

In some scenarios, it may be necessary to perform additional validation checks on the values of certain AIs to ensure that they contain a recognised Global Company Prefix (GCP). This includes all designated GS1 keys and some additional AIs. A GCP is a sequence of 4 to 12 digits allocated to an individual company by a GS1 Member Organisation (MO). To perform these additional checks, provide a list of GCP strings using the optional gcps parameter on the Parse() and ParseEx() methods.  You must ensure that GCPs are represented correctly in this list.  This mechanism can also be used to verify national prefixes (e.g., for healthcare NTINs) and other prefixes reserved by GS1.

Please note that the library does not support on-line look-ups using the 'Verified by GS1' service. GS1 offers batch querying and API connection to this service. You must contact your local GS1 office to arrange access and obtain technical specifications.

## Resolved Application Identifiers

Each time the Parser calls back to the client code, it provides a ResolvedApplicationIdentifier or ResolvedApplicationIdentifierRef) object that provides information about a single resolved data element. The following information is provided:

* **Identifier**: a string containing the AI (Application Identifier) of the data element.
* **Entity**: an integer representing the AI.
* **Data Title**: the data title of the AI, as specified in GS1 General Specifications.
* **Description**: a short description of the AI semantics.
* **Character Position**: The position within the element string sequence at which the data element was found. If any exceptions are raised, they provide an offset to this value.
* **Value**: The value of the data element.
* **Inverse Exponent**: the implied decimal point position specified in the AI, if supported. Otherwise, null.
* **Sequence**: the sequence within a real-world process represented by the data element, if supported. Otherwise, null.
* **Exceptions**: a collection of exceptions that occurred, if any, when resolving the data element.
* **'Is Error' indicator**: True, if any exceptions where detected when resolving the data element. Otherwise, false.
* **'Is Fatal' indicator**: True, if any exceptions prevented the GS1 Parser from representing a parsed data element that conforms to, or is known to conform to, the GS1 specification for that data element. Otherwise, false.
* **'Is Fixed Width' indicator**: True if the value of the data element is specified as a fixed-width value. This does not imply that the data element has a predefined length specified in Figure 7.8.5-2 in GS1 General Specifications. However, all AIs with a predefined length have fixed-width values.
* **Index**: The index of the element string sequence containing the resolved data element in the list of sequences passed to the Parse() method.

# Performance

The GS1 AI Parser is a high-performance library. As far as possible, parsing operations are carried out on the stack and heap allocations are avoided. This significantly increases the performance of the library and avoids unnecessary garbage collection. Internally, modern versions of .NET use SIMD instructions where appropriate to further boost performance.

The Parse() method accepts either strings or ReadOnlySpan<char>. There is no significant difference in performance between these options, but there is always a small amount of heap allocation because these overloads call back to an Action<IResolvedEntity>, passing a ResolvedApplicationIdentifier record which lives on the heap.

If the client application is written using the older .NET Framework, it will not have native access to the ReadOnlySpan<char> type and can therefore only pass strings to Parse(). However, Microsoft provides a System.Memory package via NuGet which provides the ReadOnlySpan<char> type and which supports later versions of .NET Framework.

To eliminate heap allocations, use ParseEx(). This method is supported only on the .NET 7.0 platform, or higher. ParseEx() does not generally provide any significant performance improvement. However, it invokes a ResolvedElementDelegate, passing a ResolvedApplicationIdentifierRef ref struct which lives on the stack.  ParseEx() is only recommended for use in systems that require reliable high-performance and real-time processing.

As a broad indication of performance, you could reasonably expect, in an ideal scenario, that a modern CPU can parse 600,000 - 800,000 instances per second of valid barcode data containing four GS1 entities (e.g., unique identifiers on the secondary packaging of medicinal products). Of course, actual performance will depend on other factors, including the version of .NET, the number of errors detected in the barcode data, the performance of your client code, CPU and memory specifications, environmental load on the CPU, etc.

There are four scenarios is which heap allocations are always performed.

1. If the Parse() method is used, internal heap allocation is performed to create ResolvedApplicationIdentifier records which are passed back to the client application.
2. If any validation errors or warnings are raised in any mode, the library instantiates ParserException objects on the heap and returns references to the client code.
3. If the optional relationshipTests parameter on the Parse() and ParseEx() methods is set to DataRelationshipTests.InvalidPairs or DataRelationshipTests.All, the library will perform additional data relationship tests. These tests require additional heap allocation internally.
4. If a list of element string sequences is passed to the Parse() method, and the scenario parameter is set to any value except Scenario.Arbitrary, the library will perform additional data relationship tests. These tests require additional heap allocation internally.

If the code is run on older versions of .NET or the .NET Framework, there may be additional heap allocations performed internally by Microsoft code. Broadly, the older the framework, the less performant it will be.

Performance drops when data relationship rules are performed. Memory allocations also increase, leading to increased garbage collection. The invalid pairs tests add perhaps 20-30% additional time per typical element string sequence with very little additional memory allocation.. However, mandatory association tests introduce significant additional CPU and memory allocation overhead, reducing the number of element string sequences that can be processed per second to a few hundred.

The library is not compiled using pre-jitting ('ready-to-run' - requires the .NET runtime). This could be useful to maximise performance in scenarios where the parser is instantiated and invoked in a one-shot manner. While it is possible to publish multiple pre-jitted images in a NuGet package, there are limitations which make the package harder to use. If you need to pre-jit the code, try pre-jitting your client code. Alternatively, compile and publish the source code yourself.  For example, to publish for the Win-64 platform, use the following command lines:

````pwsh
dotnet restore -p:PublishReadyToRun=true

dotnet publish .\Solidsoft.Reply.Parsers.Gs1Ai.csproj -c Release -f net7.0 -r win-x64 -p:PublishReadyToRun=true
````
# Interoperability

The GS1 parser library is CLS-compliant, ensuring good interoperability across different .NET languages.

## Native Code Library

Modern versions of .NET provide good support for AOT (Ahead-of-Time) Native code generation. The generated code targets a specific platform (e.g., win-x64) and runs independently of the .NET runtime. Native code libraries can be used by a wide range of compiler technologies, bytecode environments (Java, WASM etc.) and languages. To facilitate this approach to interoperability, we provide the Solidsoft.Reply.Parsers.Gs1Ai.Native.win-x64 project. This project is configured to publish a native code library for win-x64. You can reconfigure it for other target platforms. To build the code, you must ensure you have the correct toolchain installed. For example, Visual Studio users can include the 'Desktop development with C++' workload in their Visual Studio installation to build the code for the win-x64 target platform. See https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/?tabs=windows%2Cnet8 for additional information. To demonstrate the use of the native code generated by this project, we have included the Solidsoft.Reply.Parsers.Gs1Ai.Native.Tester project. This is a C++ Console application that demonstrates the use of the native GS1 Parser library.

The native code library exports three functions as a C ABI:

````c
Gs1Ai_Callback
Gs1Ai_SetExceptionCallback
Gs1Ai_Parse
````
Use the first two functions to set the main call-back and an additional call-back for exceptions. Then use Gs1\_Parse to parse the input string.

````c
void __stdcall Gs1Ai_Callback(
    wchar_t*,        // identifier (UTF-16 on Windows)
    int,             // length of identifier
    wchar_t*,        // value (UTF-16 on Windows)
    int,             // length of value
    int,             // entity
    wchar_t*,        // Data title (UTF-16 on Windows)
    int,             // Length of data title
    wchar_t*,        // Description (UTF-16 on Windows)
    int,             // Length of description
    int,             // Inverse exponent
    int,             // Sequence
    int,             // Is fixed width (1 = true, 0 = false)
    int,             // Is error (1 = true, 0 = false)
    int,             // Is fatal (1 = true, 0 = false)
    int,             // Character position
    int);            // Index

void __stdcall Gs1Ai_SetExceptionCallback(
    int,             // Entity
    int,             // Error number
    wchar_t*,        // Message (UTF-16 on Windows)
    int,             // length of message
    int,             // Is fatal (1 = true, 0 = false)
    int);            // Offset

int __stdcall Gs1Ai_Parse(
    wchar_t*,        // Input (UTF-16 on Windows)
    int,             // Length of input
    int,             // Relationship Tests - 0 = None, 1 = Invalid pairs, 2 = All
    int,             // GTIN semantics = 0 = General, 1 = Variable Measure, 2 = Custom
    int,             // Expiry Date semantics - 0 = Trade Item, 1 = Coupon
    int,             // Amount Payable semantics - 0 = Invoice, 1 - Coupon Value
    const wchar_t**, // GCP list (UTF-16 on Windows)
    int);            // Count of GCPs in list
````
# Validation Errors and Warnings

The GS1 Parser raises errors and warnings in the range of 2000-2499. Each error is categorised as fatal or non-fatal. Warnings are always non-fatal.

* A fatal error is any error that prevents the GS1 Parser from representing a parsed data element that conforms to, or is known to conform to, the GS1 specification for that data element.
* A non-fatal error does not prevent the parser from representing a parsed data element that conforms directly to the GS1 specifications.
* A non-fatal error may fail validation against a third-party or industry-specific GS1 specification. This facilitates the use of test/simulation data that conforms to the GS1 General Specifications but does not conform to recognised or registered values specified by third-party or industry-specific standards or catalogues.

The following errors and warnings are raised:

## 2001

Severity: Error

Fatal: Yes

Message: No data provided.

Reason: An attempt was made to parse a string, but the string was empty or null.

## 2002

Severity: Error

Fatal: Yes

Message: Invalid application identifier {0}.

Reason: The parsed string contains an unrecognised application identifier. NB., the GS1 Parser is reviewed and updated, as required, on an annual basis to ensure support for all application identifiers defined in the GS1 General Specifications. This occurs shortly after publication of the current version of the GS1 General Specifications (typically in January). Further ad hoc updates may occur during the year for ratified AIs published through the GSCN (General Specifications Change Notifications) mechanism. If support for a new ratified AI is required urgently, please contact the authors. The GS1 Parser does not support non-ratified application identifiers.

## 2003

Severity: Error

Fatal: Yes

Message: Invalid application identifier {0}. AIs must be between two and four digits in length.

Reason: The parsed string contains an unrecognised application identifier containing a single character only.

## 2004

Severity: Error

Fatal: No

Message: Element is incorrectly terminated with an FNC1.

Reason: An element in the parsed string is incorrectly terminated with an FNC1 (ASCII 29 character). The GS1 Parser will parse the data correctly if no other errors exists.

## 2005

Severity: Error

Fatal: Yes

Message: A predefined-length element does not contain a sufficient number of characters.

Reason: The value of a predefined-length element (see Figure 7.8.5-2 in GS1 General Specifications) in the parsed string has less characters than allowed before its terminator. NB., this is only detected for the last element in the data string.

## 2006

Severity: Error

Fatal: Yes

Message: The value{0} is invalid for AI {1}.

Reason: The value of an element does not pass the validation rules for the given AI. The GS1 Parser uses regular expression patterns to validate element values and does not provide detailed semantic error descriptions.

## 2007

Severity: Error

Fatal: Yes

Message: No element value provided for AI {0}.

Reason: No element value was provided for the given AI.

## 2008

Severity: Error

Fatal: Yes

Message: Validation for AI {0} timed out.

Reason: The regular expression evaluator timed out while validating the element value. This may indicate an underlying environmental issue that interferes with data processing and is classified as a fatal error, as if the value had failed validation.

## 2009

Severity: Error

Fatal: Yes

Message: The value{0} for {1} has an invalid check digit.

Reason: The check digit in the element value is invalid.

## 2010

Severity: Error

Fatal: Yes

Message: The implied decimal point position (inverse exponent) for AI {0} is specified incorrectly.

Reason: The character in the AI representing the implied decimal point position is invalid.

## 2011

Severity: Error

Fatal: No

Message: The value{0} does not conform to the IBAN standard.

Reason: The value of the IBAN (International Bank Account Number) does not conform to ISO 13616, or it is not catalogued for general international use and is not in development.

## 2012

Severity: Error

Fatal: No

Message: The value{0} does not match the pattern specified for IBAN numbers for {1}.

Reason: The value of the IBAN (International Bank Account Number) does not conform to the pattern specified for the given country.

## 2013

Severity: Error

Fatal: No

Message: The value{0} matches the pattern specified for IBAN numbers for {1} that is currently in development and not catalogued for general international use.

Reason: The value of the IBAN (International Bank Account Number) is aspirational. It matches a pattern that is not yet specified for use by the given country.

## 2014

Severity: Error

Fatal: No

Message: The IBAN value{0} contains incorrect check digits.

Reason: The check digits in the IBAN number are incorrect with respect to ISO/IEC 7064:2003 (MOD-97-10).

## 2015

Severity: Error

Fatal: No

Message: The value{0} does not match the pattern specified for North American coupons.

Reason: The value of the North American Coupon does not conform to the industry standard developed by the Joint Industry Coupon Committee (JICC) together with GS1 US. This standard is defined in 'North American Coupon Application Guideline using GS1 DataBar Expanded Symbols' available from GS1 US.

## 2016

Severity: Error

Fatal: No

Message: The value{0} does not match the pattern specified for North American positive offer file coupons.

Reason: The value of the North American positive offer file coupon code does not conform to industry specifications developed by The Coupon Bureau and the Joint Industry Coupon Committee (JICC).

## 2017

Severity: Warning

Fatal: No

Message: The value{0} is not a recognised UN/ECE or GS1 package type code.

Reason: The value of the package type code does not conform to UN/CEFACT Recommendation No. 21 – Codes for Types of Cargo, Packages and Packaging Materials. In addition, it is not defined in the GS1 Packaging Type Code List.

## 2100

Severity: Error

Fatal: Yes

Message: The value{0} does not match the specified pattern for the data element.

Reason: The value is invalid with respect to the pattern specified for the AI in the GS1 General Specifications.

## 2101

Severity: Error

Fatal: Yes

Message: The value of AI {0} does contains an recognised Global Company Prefix (GCP).

Reason: The value contains an unrecognised Global Company Prefix (GCP), based on a list of GCPs provided by the client code.

## 2201

Severity: Error

Fatal: Yes

Message: Invalid data relationship found between AI {0} and AI {1}.

Reason: The data contains a combination of two AIs that must not appear together on the same physical entity, whether is a single or multiple barcodes or data structures.

## 2202

Severity: Error

Fatal: Yes

Message: AI {0} appears more than once with different values.

Reason: The data contains multiple instances of the same AI with different values. If the same AI appears more than once on a physical entity, whether is a single or multiple barcodes or data structures, all occurrences must have the same value.

## 2203

Severity: Error

Fatal: Yes

Message: Mandatory data relationship not found between AI {0} and {1}.

Reason: The data contains an AI that must always appear together with one or more AIs in a physical entity, whether is a single or multiple barcodes or data structures. However, one or more of these additional AIs is not present in the data.

# Update Policy

The parser supports the full set of AIs. GS1 General Specifications are updated on an annual basis (normally in mid to late January) and may adopt new AIs from time to time. Occasionally, GS1 publishes an interim (mid-year) update to General Specifications, although this is rare. On a best-endeavours basis, we aim to update the code, at the latest, to incorporate new AIs shortly after they are published in General Specifications. We do not generally support new AIs while they are at the General Specifications Change Notifications (GSCN) stage but may make exceptions from time to time. GSCNs represent ratified changes which are intended for inclusion in General Specifications in the next publishing round.
