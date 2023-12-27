# Solidsoft.Reply.Parsers.Gs1Ai
The Solidsoft Reply parser for GS1 Application Identifiers is a comprehensive validating parser that conforms to the GS1 General Specifications and parses strings containing a concatenation of AIs /value pairs using ASCII 29 as the FNC1 delimiter.

GS1 defines AIs as part of its general specifications.  See [GS1 General Specifications - Standards | GS1]( https://www.gs1.org/standards/barcodes-epcrfid-id-keys/gs1-general-specifications)
The parser validates each AI against the format defined for that AI.  It calls back into an Action for each AI reported to it.  For each AI, parsed data is reported as a Resolved Entity object.  Each Resolved Entity includes a collection of all errors reported while parsing the AI.
The library depends on the Solidsoft.Reply.Parsers.Common library.

This solution contains two projects - the parser itself, and a set of tests. Further information is provided in project-level read-me files and the Wiki.
