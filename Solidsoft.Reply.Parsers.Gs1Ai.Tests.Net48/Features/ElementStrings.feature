Feature: ElementStrings

A short summary of the feature

Scenario Outline: Parentheses in Fixed Length Element Strings
	Given the input is <input>
	When the input to submitted to the parser
	Then the entity should be <entity>
		And the AI should be <ai>
		And the value should be <value>
		And the data value should be <data value>
		And the description should be <description>
		And the length of the value should be fixed
		And there should be no errors

Examples: Trade item / logistic unit identification & dates
	| input                  | entity | ai | value              | data value             | description                                                |
	| (00)001234560000000018 |     00 | 00 | 001234560000000018 | SSCC                   | Identification of a logistic unit (SSCC)                   |
	| (01)12345678901231     |     01 | 01 |     12345678901231 | GTIN                   | Identification of a trade item (GTIN)                      |
	| (02)12345678901231     |     02 | 02 |     12345678901231 | CONTENT                | Identification of trade items contained in a logistic unit |
	| (03)12345678901231     |     03 | 03 |     12345678901231 | MTO GTIN               | Identification of a Made-to-Order (MtO) trade item (GTIN)  |
	| (11)231231             |     11 | 11 |             231231 | PROD DATE              | Production date                                            |
	| (12)231231             |     12 | 12 |             231231 | DUE DATE               | Due date for amount on payment slip                        |
	| (13)231231             |     13 | 13 |             231231 | PACK DATE              | Packaging date                                             |
	| (15)231231             |     15 | 15 |             231231 | BEST BEFORE or BEST BY | Best before date                                           |
	| (16)231231             |     16 | 16 |             231231 | SELL BY                | Sell by date                                               |
	| (17)231231             |     17 | 17 |             231231 | USE BY OR EXPIRY       | Expiration date                                            |
	| (20)01                 |     20 | 20 |                 01 | VARIANT                | Internal product variant                                   |

Examples: Shipment / location / parties / country
	| input                  | entity | ai  | value             | data value             | description                                                          |
	| (402)50609175112345629 |    402 | 402 | 50609175112345629 | GSIN                   | Global Shipment Identification Number (GSIN)                         |
	| (410)5060917510004     |    410 | 410 |     5060917510004 | SHIP TO LOC            | Ship to - Deliver to Global Location Number (GLN)                    |
	| (411)5060917510004     |    411 | 411 |     5060917510004 | BILL TO                | Bill to - Invoice to Global Location Number (GLN)                    |
	| (412)5060917510004     |    412 | 412 |     5060917510004 | PURCHASE FROM          | Purchased from Global Location Number (GLN)                          |
	| (413)5060917510004     |    413 | 413 |     5060917510004 | SHIP FOR LOC           | Ship for - Deliver for - Forward to Global Location Number (GLN)     |
	| (414)5060917510004     |    414 | 414 |     5060917510004 | LOC No.                | Identification of a physical location - Global Location Number (GLN) |
	| (415)5060917510004     |    415 | 415 |     5060917510004 | PAY TO                 | Global Location Number (GLN) of the invoicing party                  |
	| (416)5060917510004     |    416 | 416 |     5060917510004 | PROD/SERV LOC          | Global Location Number (GLN) of the production or service location   |
	| (417)5060917510004     |    417 | 417 |     5060917510004 | PARTY                  | Party Global Location Number (GLN)                                   |
	| (422)826               |    422 | 422 |               826 | ORIGIN                 | Country of origin of a trade item                                    |
	| (424)826               |    424 | 424 |               826 | COUNTRY - PROCESS      | Country of processing                                                |
	| (426)826               |    426 | 426 |               826 | COUNTRY – FULL PROCESS | Country covering full process chain                                  |

Examples: Ship-to / return-to geo and country
	| input                      | entity | ai   | value                | data value      | description                       |
	| (4307)GB                   |   4307 | 4307 | GB                   | SHIP TO COUNTRY | Ship-to / Deliver-to country code |
	| (4309)02790858483015297971 |   4309 | 4309 | 02790858483015297971 | SHIP TO GEO     | Ship-to / Deliver-to GEO location |
	| (4317)GB                   |   4317 | 4317 | GB                   | RTN TO COUNTRY  | Return-to country code            |

Examples: Flags and date/time controls
	| input            | entity | ai   | value      | data value      | description                   |
	| (4321)1          |   4321 | 4321 |          1 | DANGEROUS GOODS | Dangerous goods flag          |
	| (4322)1          |   4322 | 4322 |          1 | AUTH LEAVE      | Authority to leave flag       |
	| (4323)1          |   4323 | 4323 |          1 | SIG REQUIRED    | Signature required flag       |
	| (4324)2312310900 |   4324 | 4324 | 2312310900 | NBEF DEL DT     | Not before delivery date/time |
	| (4325)2312311700 |   4325 | 4325 | 2312311700 | NAFT DEL DT     | Not after delivery date/time  |
	| (4326)231231     |   4326 | 4326 |     231231 | REL DATE        | Release date                  |

Examples: Defence / logistics / regulated
	| input               | entity | ai   | value         | data value        | description                                 |
	| (7001)5310997032519 |   7001 | 7001 | 5310997032519 | NSN               | NATO Stock Number (NSN)                     |
	| (7003)2312312359    |   7003 | 7003 |    2312312359 | EXPIRY TIME       | Expiration date and time                    |
	| (7006)231231        |   7006 | 7006 |        231231 | FIRST FREEZE DATE | First freeze date                           |
	| (7040)3PA_          |   7040 | 7040 | 3PA_          | UIC+EXT           | GS1 UIC with Extension 1 and Importer index |
	| (7241)05            |   7241 | 7241 |            05 | AIDC MEDIA TYPE   | AIDC media type                             |

Examples: Person-related fixed-length
	| input              | entity | ai   | value        | data value     | description                   |
	| (7250)20240214     |   7250 | 7250 |     20240214 | DOB            | Date of birth                 |
	| (7251)202402141743 |   7251 | 7251 | 202402141743 | DOB TIME       | Date and time of birth        |
	| (7252)1            |   7252 | 7252 |            1 | BIO SEX        | Biological sex                |
	| (7258)2/3          |   7258 | 7258 | 2/3          | BIRTH SEQUENCE | Baby birth sequence indicator |

Examples: Trade item / coupon fixed-length
	| input                    | entity | ai   | value              | data value     | description                                                                  |
	| (8001)15000003056000     |   8001 | 8001 |     15000003056000 | DIMENSIONS     | Roll products - width, length, core diameter, direction, splices             |
	| (8005)000150             |   8005 | 8005 |             000150 | PRICE PER UNIT | Price per unit of measure                                                    |
	| (8006)050609175100040102 |   8006 | 8006 | 050609175100040102 | ITIP           | Identification of an individual trade item (ITIP) piece                      |
	| (8026)050609175100040102 |   8026 | 8026 | 050609175100040102 | ITIP CONTENT   | Identification of pieces of a trade item (ITIP) contained in a logistic unit |
	| (8111)0310               |   8111 | 8111 |               0310 | POINTS         | Loyalty points of a coupon                                                   |


Scenario Outline: Parentheses in Variable Length Element Strings
	Given the input is <input>
	When the input to submitted to the parser
	Then the entity should be <entity>
		And the AI should be <ai>
		And the value should be <value>
		And the data value should be <data value>
		And the description should be <description>
		And the length of the value should be variable
		And there should be no errors

Examples: Core variable identifiers
	| input                    | entity | ai  | value               | data value | description                                                                           |
	| (10)ABC123D              |     10 |  10 | ABC123D             | BATCH/LOT  | Batch or lot number                                                                   |
	| (21)7337203174393624     |     21 |  21 |    7337203174393624 | SERIAL     | Serial number                                                                         |
	| (22)733AC720317439R3624  |     22 |  22 | 733AC720317439R3624 | CPV        | Consumer product variant                                                              |
	| (235)733AC720317439R3624 |    235 | 235 | 733AC720317439R3624 | TPX        | Third Party Controlled, Serialised Extension of Global Trade Item Number (GTIN) (TPX) |

Examples: Product and partner references
	| input                             | entity | ai  | value                        | data value       | description                                                    |
	| (240)This+is+some+identifier+1234 |    240 | 240 | This+is+some+identifier+1234 | ADDITIONAL ID    | Additional product identification assigned by the manufacturer |
	| (241)This+is+some+part+no+1234    |    241 | 241 | This+is+some+part+no+1234    | CUST. PART No.   | Customer part number                                           |
	| (242)1234                         |    242 | 242 |                         1234 | MTO VARIANT      | Made-to-Order variation number                                 |
	| (243)This+is+some+pcn+123         |    243 | 243 | This+is+some+pcn+123         | PCN              | Packaging component number                                     |
	| (250)733AC720317439R3624          |    250 | 250 | 733AC720317439R3624          | SECONDARY SERIAL | Secondary serial number                                        |
	| (251)This+is+some+reference+1234  |    251 | 251 | This+is+some+reference+1234  | REF. TO SOURCE   | Reference to source entity                                     |

Examples: Document and coupon identifiers
	| input                               | entity | ai  | value                          | data value              | description                                      |
	| (253)1234567890128733AC720317439R36 |    253 | 253 | 1234567890128733AC720317439R36 | GDTI                    | Global Document Type Identifier (GDTI)           |
	| (254)Gln+Extension+254              |    254 | 254 | Gln+Extension+254              | GLN EXTENSION COMPONENT | Global Location Number (GLN) extension component |
	| (255)1234567890128733720317439      |    255 | 255 |      1234567890128733720317439 | GCN                     | Global Coupon Number (GCN)                       |

Examples: Counts and orders
	| input                  | entity | ai  | value             | data value   | description                                                            |
	| (30)000999             |     30 |  30 |            000999 | VAR. COUNT   | Variable count of items                                                |
	| (37)12345678           |     37 |  37 |          12345678 | COUNT        | Count of trade items or trade item pieces contained in a logistic unit |
	| (400)1234567890        |    400 | 400 |        1234567890 | ORDER NUMBER | Customer's purchase order number                                       |
	| (401)506091751123456   |    401 | 401 |   506091751123456 | GINC         | Global Identification Number for Consignment (GINC)                    |
	| (403)Routing+Code+1234 |    403 | 403 | Routing+Code+1234 | ROUTE        | Routing code                                                           |

Examples: Postal / country subdivision / initial processing
	| input           | entity | ai  | value      | data value                | description                                                        |
	| (420)SE220PF    |    420 | 420 | SE220PF    | SHIP TO POST              | Ship-to / Deliver-to postal code within a single postal authority  |
	| (421)826SE220PF |    421 | 421 | 826SE220PF | SHIP TO POST              | Ship-to / Deliver-to postal code with three-digit ISO country code |
	| (423)826        |    423 | 423 |        826 | COUNTRY - INITIAL PROCESS | Country of initial processing                                      |
	| (425)826        |    425 | 425 |        826 | COUNTRY - DISASSEMBLY     | Country of disassembly                                             |
	| (427)ENG        |    427 | 427 | ENG        | ORIGIN SUBDIVISION        | Country subdivision of origin code for a trade item                |

Examples: Ship-to address components
	| input                   | entity | ai   | value             | data value    | description                           |
	| (4300)Acme+Corp         |   4300 | 4300 | Acme+Corp         | SHIP TO COMP  | Ship-to / Deliver-to Company name     |
	| (4301)John+Smith        |   4301 | 4301 | John+Smith        | SHIP TO NAME  | Ship-to / Deliver-to contact name     |
	| (4302)100+Acadia+Avenue |   4302 | 4302 | 100+Acadia+Avenue | SHIP TO ADD1  | Ship-to / Deliver-to address line 1   |
	| (4303)Noborough         |   4303 | 4303 | Noborough         | SHIP TO ADD2  | Ship-to / Deliver-to address line 2   |
	| (4304)Lower+District    |   4304 | 4304 | Lower+District    | SHIP TO SUB   | Ship-to / Deliver-to suburb           |
	| (4305)Anytown           |   4305 | 4305 | Anytown           | SHIP TO LOC   | Ship-to / Deliver-to locality         |
	| (4306)United+Kingdom    |   4306 | 4306 | United+Kingdom    | SHIP TO REG   | Ship-to / Deliver-to region           |
	| (4308)+32-2-788-78-00   |   4308 | 4308 | +32-2-788-78-00   | SHIP TO PHONE | Ship-to / Deliver-to telephone number |

Examples: Return-to address components
	| input                                | entity | ai   | value                          | data value      | description                |
	| (4310)Acme+Corp                      |   4310 | 4310 | Acme+Corp                      | RTN TO COMP     | Return-to company name     |
	| (4311)John+Smith                     |   4311 | 4311 | John+Smith                     | RTN TO NAME     | Return-to contact name     |
	| (4312)100+Acadia+Avenue              |   4312 | 4312 | 100+Acadia+Avenue              | RTN TO ADD1     | Return-to address line 1   |
	| (4313)Noborough                      |   4313 | 4313 | Noborough                      | RTN TO ADD2     | Return-to address line 2   |
	| (4314)Lower+District                 |   4314 | 4314 | Lower+District                 | RTN TO SUB      | Return-to suburb           |
	| (4315)Anytown                        |   4315 | 4315 | Anytown                        | RTN TO LOC      | Return-to locality         |
	| (4316)United+Kingdom                 |   4316 | 4316 | United+Kingdom                 | RTN TO REG      | Return-to region           |
	| (4318)SE220PF                        |   4318 | 4318 | SE220PF                        | RTN TO POST     | Return-to postal code      |
	| (4319)+32-2-788-78-00                |   4319 | 4319 | +32-2-788-78-00                | RTN TO PHONE    | Return-to telephone number |
	| (4320)Service+code+description+12345 |   4320 | 4320 | Service+code+description+12345 | SRV DESCRIPTION | Service code description   |

Examples: Temperature (unsigned)
	| input        | entity | ai   | value  | data value | description                       |
	| (4330)023020 |   4330 | 4330 | 023020 | MAX TEMP F | Maximum temperature in Fahrenheit |
	| (4331)000090 |   4331 | 4331 | 000090 | MAX TEMP C | Maximum temperature in Celsius    |
	| (4332)023020 |   4332 | 4332 | 023020 | MIN TEMP F | Minimum temperature in Fahrenheit |
	| (4333)000090 |   4333 | 4333 | 000090 | MIN TEMP C | Minimum temperature in Celsius    |

Examples: Temperature (signed)
	| input         | entity | ai   | value   | data value | description                       |
	| (4330)000250- |   4330 | 4330 | 000250- | MAX TEMP F | Maximum temperature in Fahrenheit |
	| (4331)001000- |   4331 | 4331 | 001000- | MAX TEMP C | Maximum temperature in Celsius    |
	| (4332)000250- |   4332 | 4332 | 000250- | MIN TEMP F | Minimum temperature in Fahrenheit |
	| (4333)001000- |   4333 | 4333 | 001000- | MIN TEMP C | Minimum temperature in Celsius    |

Examples: Meat / fishery / production method
	| input                      | entity | ai   | value                | data value        | description                                  |
	| (7002)44932211340000145100 |   7002 | 7002 | 44932211340000145100 | MEAT CUT          | UNECE meat carcasses and cuts classification |
	| (7004)3001                 |   7004 | 7004 |                 3001 | ACTIVE POTENCY    | Active potency                               |
	| (7005)27.6.b.1             |   7005 | 7005 | 27.6.b.1             | CATCH AREA        | Catch area                                   |
	| (7007)230801230831         |   7007 | 7007 |         230801230831 | HARVEST DATE      | Harvest date                                 |
	| (7008)BWQ                  |   7008 | 7008 | BWQ                  | AQUATIC SPECIES   | Species for fishery purposes                 |
	| (7009)01.1.1               |   7009 | 7009 |               01.1.1 | FISHING GEAR TYPE | Fishing gear type                            |
	| (7010)01                   |   7010 | 7010 |                   01 | PROD METHOD       | Production method                            |
	| (7011)2312311200           |   7011 | 7011 |           2312311200 | TEST BY DATE      | Test by date                                 |

Examples: Refurb / revision / asset
	| input                      | entity | ai   | value                | data value      | description                                       |
	| (7020)ABC123DE             |   7020 | 7020 | ABC123DE             | REFURB LOT      | Refurbishment lot ID                              |
	| (7021)Functional+status+01 |   7021 | 7021 | Functional+status+01 | FUNC STAT       | Functional status                                 |
	| (7022)Revision+status+01   |   7022 | 7022 | Revision+status+01   | REV STAT        | Revision status                                   |
	| (7023)506091751ASSET+0001  |   7023 | 7023 | 506091751ASSET+0001  | GIAI – ASSEMBLY | Global Individual Asset Identifier of an assembly |

Examples: Healthcare reimbursement numbers
	| input                | entity | ai  | value           | data value | description                                                                           |
	| (710)3675419         |    710 | 710 |         3675419 | NHRN PZN   | National Healthcare Reimbursement Number (NHRN) - Germany PZN                         |
	| (711)3400935974419   |    711 | 711 |   3400935974419 | NHRN CIP   | National Healthcare Reimbursement Number (NHRN) - France CIP                          |
	| (712)384756.8        |    712 | 712 |        384756.8 | NHRN CN    | National Healthcare Reimbursement Number (NHRN) - Spain CN                            |
	| (713)40056320000011  |    713 | 713 |  40056320000011 | NHRN DRN   | National Healthcare Reimbursement Number (NHRN) - Brasil DRN                          |
	| (714)142199          |    714 | 714 |          142199 | NHRN AIM   | National Healthcare Reimbursement Number (NHRN) - Portugal AIM                        |
	| (715)0777310502      |    715 | 715 |      0777310502 | NHRN NDC   | National Healthcare Reimbursement Number (NHRN) - United States of America NDC        |
	| (716)A012345676      |    716 | 716 | A012345676      | NHRN AIC   | National Healthcare Reimbursement Number (NHRN) – Italy AIC                           |
	| (717)052-20220012345 |    717 | 717 | 052-20220012345 | NHRN SRN   | National Healthcare Reimbursement Number (NHRN) – Costa Rica Sanitary Register Number |

Examples: Protocol / certification / person attributes
	| input                                        | entity | ai   | value                                  | data value      | description                 |
	| (7041)1A                                     |   7041 | 7041 | 1A                                     | UFRGT UNIT TYPE | UN/CEFACT freight unit type |
	| (7230)EMBABT-MED00108                        |    723 | 7230 | EMBABT-MED00108                        | CERT # s        | Certification reference     |
	| (7240)CACZ885N2301E2                         |   7240 | 7240 | CACZ885N2301E2                         | PROTOCOL        | Protocol ID                 |
	| (7253)Doe                                    |   7253 | 7253 | Doe                                    | FAMILY NAME     | Family name of person       |
	| (7254)John                                   |   7254 | 7254 | John                                   | GIVEN NAME      | Given name of person        |
	| (7255)Junior                                 |   7255 | 7255 | Junior                                 | SUFFIX          | Name suffix of person       |
	| (7256)Doe,John,Junior                        |   7256 | 7256 | Doe,John,Junior                        | FULL NAME       | Full name of person         |
	| (7257)123+Main+St,+Anytown,+Anyregion,+12345 |   7257 | 7257 | 123+Main+St,+Anytown,+Anyregion,+12345 | PERSON ADDR     | Address of person           |
	| (7259)Alice+Betty                            |   7259 | 7259 | Alice+Betty                            | BABY            | Baby of family name         |

Examples: Telecom / components / digital / URLs
	| input                                                                                            | entity | ai   | value                                                                                      | data value  | description                                                             |
	| (8002)RF1DB6K177Y                                                                                |   8002 | 8002 | RF1DB6K177Y                                                                                | CMT No.     | Cellular mobile telephone identifier                                    |
	| (8003)0506091751000434B1UL09036                                                                  |   8003 | 8003 | 0506091751000434B1UL09036                                                                  | GRAI        | Global Returnable Asset Identifier (GRAI)                               |
	| (8004)5060917ASSET+0001                                                                          |   8004 | 8004 | 5060917ASSET+0001                                                                          | GIAI        | Global Individual Asset Identifier (GIAI)                               |
	| (8008)231231142652                                                                               |   8008 | 8008 |                                                                               231231142652 | PROD TIME   | Date and time of production                                             |
	| (8009)01190531                                                                                   |   8009 | 8009 |                                                                                   01190531 | OPTSEN      | Optically readable sensor indicator                                     |
	| (8010)506091751DR4529P327                                                                        |   8010 | 8010 | 506091751DR4529P327                                                                        | CPID        | Component/Part Identifier (CPID)                                        |
	| (8011)422393761701                                                                               |   8011 | 8011 |                                                                               422393761701 | CPID SERIAL | Component/Part Identifier serial number                                 |
	| (8012)15.0.4701.1001                                                                             |   8012 | 8012 |                                                                             15.0.4701.1001 | VERSION     | Software version                                                        |
	| (8013)1987654Ad4X4bL5ttr2310c2K                                                                  |   8013 | 8013 | 1987654Ad4X4bL5ttr2310c2K                                                                  | GMN         | Global Model Number (GMN)                                               |
	| (8014)1987654Ad4X4bL5ttr2310c2K                                                                  |   8014 | 8014 | 1987654Ad4X4bL5ttr2310c2K                                                                  | MUDI        | Highly Individualised Device Registration Identifier (HIDRI)            |
	| (8019)00499427                                                                                   |   8019 | 8019 |                                                                                   00499427 | SRIN        | Service Relation Instance Number (SRIN)                                 |
	| (8020)000B231297726310000                                                                        |   8020 | 8020 | 000B231297726310000                                                                        | REF No.     | Payment slip reference number                                           |
	| (8030)41703319222174341186086981525711229423385405386071942069864323665892369845781264435120798= |   8030 | 8030 | 41703319222174341186086981525711229423385405386071942069864323665892369845781264435120798= | DIGSIG      | Digital Signature (DigSig)                                              |
	| (8040)490154203237518                                                                            |   8040 | 8040 |                                                                            490154203237518 | IMEI        | International Mobile Equipment Identity (IMEI)                          |
	| (8041)356938035643809                                                                            |   8041 | 8041 |                                                                            356938035643809 | IMEI2       | International Mobile Equipment Identity 2 (IMEI2)                       |
	| (8042)89033023456789012345678901234567                                                           |   8042 | 8042 |                                                           89033023456789012345678901234567 | ESIM        | Embedded SIM                                                            |
	| (8043)8944500101234567890                                                                        |   8043 | 8043 |                                                                        8944500101234567890 | PSIM        | Physical SIM                                                            |
	| (8110)106141416543213500110000310123196000                                                       |   8110 | 8110 |                                                       106141416543213500110000310123196000 | -           | Coupon code identification for use in North America                     |
	| (8112)106141416543213500110000310123196000                                                       |   8112 | 8112 |                                                       106141416543213500110000310123196000 | -           | Positive offer file coupon code identification for use in North America |
	| (8200)https://acme.com/                                                                          |   8200 | 8200 | https://acme.com/                                                                          | PRODUCT URL | Extended packaging URL                                                  |

Examples: Internal (90-99)
	| input                                                               | entity | ai | value                                                           | data value | description                                          |
	| (90)Some+information+1234                                           |     90 | 90 | Some+information+1234                                           | INTERNAL   | Information mutually agreed between trading partners |
	| (91)The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 |     91 | 91 | The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 | INTERNAL   | Company internal information                         |
	| (92)The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 |     92 | 92 | The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 | INTERNAL   | Company internal information                         |
	| (93)The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 |     93 | 93 | The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 | INTERNAL   | Company internal information                         |
	| (94)The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 |     94 | 94 | The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 | INTERNAL   | Company internal information                         |
	| (95)The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 |     95 | 95 | The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 | INTERNAL   | Company internal information                         |
	| (96)The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 |     96 | 96 | The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 | INTERNAL   | Company internal information                         |
	| (97)The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 |     97 | 97 | The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 | INTERNAL   | Company internal information                         |
	| (98)The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 |     98 | 98 | The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 | INTERNAL   | Company internal information                         |
	| (99)The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 |     99 | 99 | The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890 | INTERNAL   | Company internal information                         |


Scenario Outline: Parentheses in Fixed Length Element Strings with Inverse Exponent
	Given the input is <input>
	When the input to submitted to the parser
	Then the entity should be <entity>
		And the AI should be <ai>
		And the inverse exponent should be <inverse exponent>
		And the value should be <value>
		And the data value should be <data value>
		And the description should be <description>
		And the length of the value should be fixed
		And there should be no errors

Examples: Variable measure trade item dimensions & weights (fixed length)
	| input        | entity | ai   | inverse exponent | value  | data value      | description                                                                        |
	| (3102)123456 |    310 | 3102 |                2 | 123456 | NET WEIGHT (kg) | Net weight, kilograms (variable measure trade item)                                |
	| (3112)123456 |    311 | 3112 |                2 | 123456 | LENGTH (m)      | Length or first dimension, metres (variable measure trade item)                    |
	| (3122)123456 |    312 | 3122 |                2 | 123456 | WIDTH (m)       | Width, diameter, or second dimension, metres (variable measure trade item)         |
	| (3132)123456 |    313 | 3132 |                2 | 123456 | HEIGHT (m)      | Depth, thickness, height, or third dimension, metres (variable measure trade item) |
	| (3142)123456 |    314 | 3142 |                2 | 123456 | AREA (m²)       | Area, square metres (variable measure trade item)                                  |
	| (3152)123456 |    315 | 3152 |                2 | 123456 | NET VOLUME (l)  | Net volume, litres (variable measure trade item)                                   |
	| (3162)123456 |    316 | 3162 |                2 | 123456 | NET VOLUME (m³) | Net volume, cubic metres (variable measure trade item)                             |
	| (3202)123456 |    320 | 3202 |                2 | 123456 | NET WEIGHT (lb) | Net weight, pounds (variable measure trade item)                                   |
	| (3212)123456 |    321 | 3212 |                2 | 123456 | LENGTH (i)      | Length or first dimension, inches (variable measure trade item)                    |
	| (3222)123456 |    322 | 3222 |                2 | 123456 | LENGTH (f)      | Length or first dimension, feet (variable measure trade item)                      |
	| (3232)123456 |    323 | 3232 |                2 | 123456 | LENGTH (y)      | Length or first dimension, yards (variable measure trade item)                     |
	| (3242)123456 |    324 | 3242 |                2 | 123456 | WIDTH (i)       | Width, diameter, or second dimension, inches (variable measure trade item)         |
	| (3252)123456 |    325 | 3252 |                2 | 123456 | WIDTH (f)       | Width, diameter, or second dimension, feet (variable measure trade item)           |
	| (3262)123456 |    326 | 3262 |                2 | 123456 | WIDTH (y)       | Width, diameter, or second dimension, yards (variable measure trade item)          |
	| (3272)123456 |    327 | 3272 |                2 | 123456 | HEIGHT (i)      | Depth, thickness, height, or third dimension, inches (variable measure trade item) |
	| (3282)123456 |    328 | 3282 |                2 | 123456 | HEIGHT (f)      | Depth, thickness, height, or third dimension, feet (variable measure trade item)   |
	| (3292)123456 |    329 | 3292 |                2 | 123456 | HEIGHT (y)      | Depth, thickness, height, or third dimension, yards (variable measure trade item)  |

Examples: Logistic measures (fixed length)
	| input        | entity | ai   | inverse exponent | value  | data value        | description                                          |
	| (3302)123456 |    330 | 3302 |                2 | 123456 | GROSS WEIGHT (kg) | Logistic weight, kilograms                           |
	| (3312)123456 |    331 | 3312 |                2 | 123456 | LENGTH (m), log   | Length or first dimension, metres                    |
	| (3322)123456 |    332 | 3322 |                2 | 123456 | WIDTH (m), log    | Width, diameter, or second dimension, metres         |
	| (3332)123456 |    333 | 3332 |                2 | 123456 | HEIGHT (m), log   | Depth, thickness, height, or third dimension, metres |
	| (3342)123456 |    334 | 3342 |                2 | 123456 | AREA (m²), log    | Area, square metres                                  |
	| (3352)123456 |    335 | 3352 |                2 | 123456 | VOLUME (l), log   | Logistic volume, litres                              |
	| (3362)123456 |    336 | 3362 |                2 | 123456 | VOLUME (m³), log  | Logistic volume, cubic metres                        |
	| (3372)123456 |    337 | 3372 |                2 | 123456 | KG PER m²         | Kilograms per square metre                           |
	| (3402)123456 |    340 | 3402 |                2 | 123456 | GROSS WEIGHT (lb) | Logistic weight, pounds                              |
	| (3412)123456 |    341 | 3412 |                2 | 123456 | LENGTH (i), log   | Length or first dimension, inches                    |
	| (3422)123456 |    342 | 3422 |                2 | 123456 | LENGTH (f), log   | Length or first dimension, feet                      |
	| (3432)123456 |    343 | 3432 |                2 | 123456 | LENGTH (y), log   | Length or first dimension, yards                     |
	| (3442)123456 |    344 | 3442 |                2 | 123456 | WIDTH (i), log    | Width, diameter, or second dimension, inches         |
	| (3452)123456 |    345 | 3452 |                2 | 123456 | WIDTH (f), log    | Width, diameter, or second dimension, feet           |
	| (3462)123456 |    346 | 3462 |                2 | 123456 | WIDTH (y), log    | Width, diameter, or second dimension, yard           |
	| (3472)123456 |    347 | 3472 |                2 | 123456 | HEIGHT (i), log   | Depth, thickness, height, or third dimension, inches |
	| (3482)123456 |    348 | 3482 |                2 | 123456 | HEIGHT (f), log   | Depth, thickness, height, or third dimension, feet   |
	| (3492)123456 |    349 | 3492 |                2 | 123456 | HEIGHT (y), log   | Depth, thickness, height, or third dimension, yards  |

Examples: Area & volume (fixed length)
	| input        | entity | ai   | inverse exponent | value  | data value             | description                                                  |
	| (3502)123456 |    350 | 3502 |                2 | 123456 | AREA (i²)              | Area, square inches (variable measure trade item)            |
	| (3512)123456 |    351 | 3512 |                2 | 123456 | AREA (f²)              | Area, square feet (variable measure trade item)              |
	| (3522)123456 |    352 | 3522 |                2 | 123456 | AREA (y²)              | Area, square yards (variable measure trade item)             |
	| (3532)123456 |    353 | 3532 |                2 | 123456 | AREA (i²), log         | Area, square inches                                          |
	| (3542)123456 |    354 | 3542 |                2 | 123456 | AREA (f²), log         | Area, square feet                                            |
	| (3552)123456 |    355 | 3552 |                2 | 123456 | AREA (y²), log         | Area, square yards                                           |
	| (3562)123456 |    356 | 3562 |                2 | 123456 | NET WEIGHT (tr oz)     | Net weight, troy ounces (variable measure trade item)        |
	| (3572)123456 |    357 | 3572 |                2 | 123456 | NET VOLUME (oz)        | Net weight (or volume), ounces (variable measure trade item) |
	| (3602)123456 |    360 | 3602 |                2 | 123456 | NET VOLUME (qt (US))   | Net volume, quarts (variable measure trade item)             |
	| (3612)123456 |    361 | 3612 |                2 | 123456 | NET VOLUME (gal (US))  | Net volume, gallons U.S. (variable measure trade item)       |
	| (3622)123456 |    362 | 3622 |                2 | 123456 | VOLUME (qt (US)), log  | Logistic volume, quarts                                      |
	| (3632)123456 |    363 | 3632 |                2 | 123456 | VOLUME (gal (US)), log | Logistic volume, gallons U.S.                                |
	| (3642)123456 |    364 | 3642 |                2 | 123456 | VOLUME (i³)            | Net volume, cubic inches (variable measure trade item)       |
	| (3652)123456 |    365 | 3652 |                2 | 123456 | VOLUME (f³)            | Net volume, cubic feet (variable measure trade item)         |
	| (3662)123456 |    366 | 3662 |                2 | 123456 | VOLUME (y³)            | Net volume, cubic yards (variable measure trade item)        |
	| (3672)123456 |    367 | 3672 |                2 | 123456 | VOLUME (i³), log       | Logistic volume, cubic inches                                |
	| (3682)123456 |    368 | 3682 |                2 | 123456 | VOLUME (f³), log       | Logistic volume, cubic feet                                  |
	| (3692)123456 |    369 | 3692 |                2 | 123456 | VOLUME (y³), log       | Logistic volume, cubic yards                                 |

Examples: Coupon/price-related fixed-length with inverse exponent
	| input        | entity | ai   | inverse exponent | value  | data value | description                                                                           |
	| (3942)1234   |    394 | 3942 |                2 |   1234 | PRCNT OFF  | Percentage discount of a coupon                                                       |
	| (3952)123456 |    395 | 3952 |                2 | 123456 | PRICE/UoM  | Amount payable per unit of measure single monetary area (variable measure trade item) |


Scenario Outline: Parentheses in Variable Length Element Strings with Inverse Exponent
	Given the input is <input>
	When the input to submitted to the parser
	Then the entity should be <entity>
		And the AI should be <ai>
		And the inverse exponent should be <inverse exponent>
		And the value should be <value>
		And the data value should be <data value>
		And the description should be <description>
		And the length of the value should be variable
		And there should be no errors

Examples: Monetary amount / price with inverse exponent (variable length)
	| input                    | entity | ai   | inverse exponent | value              | data value | description                                                             |
	| (3902)123456789012345    |    390 | 3902 |                2 |    123456789012345 | AMOUNT     | Amount payable or coupon value - Single monetary area                   |
	| (3912)826123456789012345 |    391 | 3912 |                2 | 826123456789012345 | AMOUNT     | Amount payable and ISO currency code                                    |
	| (3922)123456789012345    |    392 | 3922 |                2 |    123456789012345 | PRICE      | Amount payable for a variable measure trade item - Single monetary area |
	| (3932)826123456789012345 |    393 | 3932 |                2 | 826123456789012345 | PRICE      | Amount payable for a variable measure trade item and ISO currency code  |
