Feature: ParseEntity

Basic tests for each GS1 entity, based on the GS1 General Specifications.

@N2+N18
Scenario: Parse SSCO
	Given the input is 00001234560000000018
	When the input to submitted to the parser
	Then the entity should be 00
	    And the AI should be 00
		And the value should be 001234560000000018
		And the data value should be SSCC
		And the description should be Identification of a logistic unit (SSCC)
		And the length of the value should be fixed
		And there should be no errors

@N2+N14
Scenario: Parse GTIN
	Given the input is 0112345678901231
	When the input to submitted to the parser
	Then the entity should be 01
	    And the AI should be 01
		And the value should be 12345678901231
		And the data value should be GTIN
		And the description should be Identification of a trade item (GTIN)
		And the length of the value should be fixed
		And there should be no errors

@N2+N14
Scenario: Parse CONTENT
	Given the input is 0212345678901231
	When the input to submitted to the parser
	Then the entity should be 02
	    And the AI should be 02
		And the value should be 12345678901231
		And the data value should be CONTENT
		And the description should be Identification of trade items contained in a logistic unit
		And the length of the value should be fixed
		And there should be no errors

@N2+N14
Scenario: Parse MTO GTIN
	Given the input is 0312345678901231
	When the input to submitted to the parser
	Then the entity should be 03
	    And the AI should be 03
		And the value should be 12345678901231
		And the data value should be MTO GTIN
		And the description should be Identification of a Made-to-Order (MtO) trade item (GTIN)
		And the length of the value should be fixed
		And there should be no errors

@N2+X..20
Scenario: Parse BATCH/LOT
	Given the input is 10ABC123D
	When the input to submitted to the parser
	Then the entity should be 10
	    And the AI should be 10
		And the value should be ABC123D
		And the data value should be BATCH/LOT
		And the description should be Batch or lot number
		And the length of the value should be variable
		And there should be no errors

@N2+N6
Scenario: Parse PROD DATE
	Given the input is 11231231
	When the input to submitted to the parser
	Then the entity should be 11
	    And the AI should be 11
		And the value should be 231231
		And the data value should be PROD DATE
		And the description should be Production date
		And the length of the value should be fixed
		And there should be no errors

@N2+N6
Scenario: Parse DUE DATE
	Given the input is 12231231
	When the input to submitted to the parser
	Then the entity should be 12
	    And the AI should be 12
		And the value should be 231231
		And the data value should be DUE DATE
		And the description should be Due date for amount on payment slip
		And the length of the value should be fixed
		And there should be no errors

@N2+N6
Scenario: Parse PACK DATE
	Given the input is 13231231
	When the input to submitted to the parser
	Then the entity should be 13
	    And the AI should be 13
		And the value should be 231231
		And the data value should be PACK DATE
		And the description should be Packaging date
		And the length of the value should be fixed
		And there should be no errors

@N2+N6
Scenario: Parse BEST BEFORE or BEST BY
	Given the input is 15231231
	When the input to submitted to the parser
	Then the entity should be 15
	    And the AI should be 15
		And the value should be 231231
		And the data value should be BEST BEFORE or BEST BY
		And the description should be Best before date
		And the length of the value should be fixed
		And there should be no errors

@N2+N6
Scenario: Parse SELL BY
	Given the input is 16231231
	When the input to submitted to the parser
	Then the entity should be 16
	    And the AI should be 16
		And the value should be 231231
		And the data value should be SELL BY
		And the description should be Sell by date
		And the length of the value should be fixed
		And there should be no errors

@N2+N6
Scenario: Parse USE BY OR EXPIRY
	Given the input is 17231231
	When the input to submitted to the parser
	Then the entity should be 17
	    And the AI should be 17
		And the value should be 231231
		And the data value should be USE BY OR EXPIRY
		And the description should be Expiration date
		And the length of the value should be fixed
		And there should be no errors

@N2+N2
Scenario: Parse VARIANT
	Given the input is 2001
	When the input to submitted to the parser
	Then the entity should be 20
	    And the AI should be 20
		And the value should be 01
		And the data value should be VARIANT
		And the description should be Internal product variant
		And the length of the value should be fixed
		And there should be no errors

@N2+X..20
Scenario: Parse SERIAL
	Given the input is 217337203174393624
	When the input to submitted to the parser
	Then the entity should be 21
	    And the AI should be 21
		And the value should be 7337203174393624
		And the data value should be SERIAL
		And the description should be Serial number
		And the length of the value should be variable
		And there should be no errors

@N2+X..20
Scenario: Parse CPV
	Given the input is 22733AC720317439R3624
	When the input to submitted to the parser
	Then the entity should be 22
	    And the AI should be 22
		And the value should be 733AC720317439R3624
		And the data value should be CPV
		And the description should be Consumer product variant
		And the length of the value should be variable
		And there should be no errors

@N3+X..28
Scenario: Parse TPX
	Given the input is 235733AC720317439R3624
	When the input to submitted to the parser
	Then the entity should be 235
	    And the AI should be 235
		And the value should be 733AC720317439R3624
		And the data value should be TPX
		And the description should be Third Party Controlled, Serialised Extension of Global Trade Item Number (GTIN) (TPX)
		And the length of the value should be variable
		And there should be no errors

@N3+X..30
Scenario: Parse ADDITIONAL ID
	Given the input is 240This+is+some+identifier+1234
	When the input to submitted to the parser
	Then the entity should be 240
	    And the AI should be 240
		And the value should be This+is+some+identifier+1234
		And the data value should be ADDITIONAL ID
		And the description should be Additional product identification assigned by the manufacturer
		And the length of the value should be variable
		And there should be no errors

@N3+X..30
Scenario: Parse CUST. PART No.
	Given the input is 241This+is+some+part+no+1234
	When the input to submitted to the parser
	Then the entity should be 241
	    And the AI should be 241
		And the value should be This+is+some+part+no+1234
		And the data value should be CUST. PART No.
		And the description should be Customer part number
		And the length of the value should be variable
		And there should be no errors

@N3+N..6
Scenario: Parse MTO VARIANT
	Given the input is 2421234
	When the input to submitted to the parser
	Then the entity should be 242
	    And the AI should be 242
		And the value should be 1234
		And the data value should be MTO VARIANT
		And the description should be Made-to-Order variation number
		And the length of the value should be variable
		And there should be no errors

@N3+X..20
Scenario: Parse PCN
	Given the input is 243This+is+some+pcn+123
	When the input to submitted to the parser
	Then the entity should be 243
	    And the AI should be 243
		And the value should be This+is+some+pcn+123
		And the data value should be PCN
		And the description should be Packaging component number
		And the length of the value should be variable
		And there should be no errors

@N3+X..30
Scenario: Parse SECONDARY SERIAL
	Given the input is 250733AC720317439R3624
	When the input to submitted to the parser
	Then the entity should be 250
	    And the AI should be 250
		And the value should be 733AC720317439R3624
		And the data value should be SECONDARY SERIAL
		And the description should be Secondary serial number
		And the length of the value should be variable
		And there should be no errors

@N3+X..30
Scenario: Parse REF. TO SOURCE 
	Given the input is 251This+is+some+reference+1234
	When the input to submitted to the parser
	Then the entity should be 251
	    And the AI should be 251
		And the value should be This+is+some+reference+1234
		And the data value should be REF. TO SOURCE 
		And the description should be Reference to source entity
		And the length of the value should be variable
		And there should be no errors

@N3+N13[+X..17]
Scenario: Parse GDTI
	Given the input is 2531234567890128733AC720317439R36
	When the input to submitted to the parser
	Then the entity should be 253
	    And the AI should be 253
		And the value should be 1234567890128733AC720317439R36
		And the data value should be GDTI 
		And the description should be Global Document Type Identifier (GDTI)
		And the length of the value should be variable
		And there should be no errors

@N3+X..20
Scenario: Parse GLN EXTENSION COMPONENT
	Given the input is 254Gln+Extension+254
	When the input to submitted to the parser
	Then the entity should be 254
	    And the AI should be 254
		And the value should be Gln+Extension+254
		And the data value should be GLN EXTENSION COMPONENT 
		And the description should be Global Location Number (GLN) extension component
		And the length of the value should be variable
		And there should be no errors

@N3+N13[+N..12]
Scenario: Parse Global Coupon Number (GCN)
	Given the input is 2551234567890128733720317439
	When the input to submitted to the parser
	Then the entity should be 255
	    And the AI should be 255
		And the value should be 1234567890128733720317439
		And the data value should be GCN 
		And the description should be Global Coupon Number (GCN)
		And the length of the value should be variable
		And there should be no errors

@N2+N..8
Scenario: Parse VAR. COUNT
	Given the input is 30000999
	When the input to submitted to the parser
	Then the entity should be 30
	    And the AI should be 30
		And the value should be 000999
		And the data value should be VAR. COUNT 
		And the description should be Variable count of items
		And the length of the value should be variable
		And there should be no errors

@N4+N6
Scenario: Parse NET WEIGHT (kg)
	Given the input is 3102123456
	When the input to submitted to the parser
	Then the entity should be 310
	    And the AI should be 3102
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be NET WEIGHT (kg) 
		And the description should be Net weight, kilograms (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse LENGTH (m)
	Given the input is 3112123456
	When the input to submitted to the parser
	Then the entity should be 311
	    And the AI should be 3112
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be LENGTH (m)
		And the description should be Length or first dimension, metres (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse WIDTH (m)
	Given the input is 3122123456
	When the input to submitted to the parser
	Then the entity should be 312
	    And the AI should be 3122
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be WIDTH (m)
		And the description should be Width, diameter, or second dimension, metres (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse HEIGHT (m)
	Given the input is 3132123456
	When the input to submitted to the parser
	Then the entity should be 313
	    And the AI should be 3132
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be HEIGHT (m)
		And the description should be Depth, thickness, height, or third dimension, metres (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse AREA (m²)
	Given the input is 3142123456
	When the input to submitted to the parser
	Then the entity should be 314
	    And the AI should be 3142
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be AREA (m²)
		And the description should be Area, square metres (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse NET VOLUME (l)
	Given the input is 3152123456
	When the input to submitted to the parser
	Then the entity should be 315
	    And the AI should be 3152
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be NET VOLUME (l)
		And the description should be Net volume, litres (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse NET VOLUME (m³)
	Given the input is 3162123456
	When the input to submitted to the parser
	Then the entity should be 316
	    And the AI should be 3162
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be NET VOLUME (m³)
		And the description should be Net volume, cubic metres (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse NET WEIGHT (lb)
	Given the input is 3202123456
	When the input to submitted to the parser
	Then the entity should be 320
	    And the AI should be 3202
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be NET WEIGHT (lb)
		And the description should be Net weight, pounds (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse LENGTH (i)
	Given the input is 3212123456
	When the input to submitted to the parser
	Then the entity should be 321
	    And the AI should be 3212
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be LENGTH (i)
		And the description should be Length or first dimension, inches (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse LENGTH (f)
	Given the input is 3222123456
	When the input to submitted to the parser
	Then the entity should be 322
	    And the AI should be 3222
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be LENGTH (f)
		And the description should be Length or first dimension, feet (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse LENGTH (y)
	Given the input is 3232123456
	When the input to submitted to the parser
	Then the entity should be 323
	    And the AI should be 3232
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be LENGTH (y)
		And the description should be Length or first dimension, yards (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse WIDTH (i)
	Given the input is 3242123456
	When the input to submitted to the parser
	Then the entity should be 324
	    And the AI should be 3242
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be WIDTH (i)
		And the description should be Width, diameter, or second dimension, inches (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse WIDTH (f)
	Given the input is 3252123456
	When the input to submitted to the parser
	Then the entity should be 325
	    And the AI should be 3252
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be WIDTH (f)
		And the description should be Width, diameter, or second dimension, feet (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse WIDTH (y)
	Given the input is 3262123456
	When the input to submitted to the parser
	Then the entity should be 326
	    And the AI should be 3262
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be WIDTH (y)
		And the description should be Width, diameter, or second dimension, yards (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse HEIGHT (i)
	Given the input is 3272123456
	When the input to submitted to the parser
	Then the entity should be 327
	    And the AI should be 3272
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be HEIGHT (i)
		And the description should be Depth, thickness, height, or third dimension, inches (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse HEIGHT (f)
	Given the input is 3282123456
	When the input to submitted to the parser
	Then the entity should be 328
	    And the AI should be 3282
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be HEIGHT (f)
		And the description should be Depth, thickness, height, or third dimension, feet (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse HEIGHT (y)
	Given the input is 3292123456
	When the input to submitted to the parser
	Then the entity should be 329
	    And the AI should be 3292
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be HEIGHT (y)
		And the description should be Depth, thickness, height, or third dimension, yards (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse GROSS WEIGHT (kg)
	Given the input is 3302123456
	When the input to submitted to the parser
	Then the entity should be 330
	    And the AI should be 3302
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be GROSS WEIGHT (kg)
		And the description should be Logistic weight, kilograms
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse LENGTH (m), log
	Given the input is 3312123456
	When the input to submitted to the parser
	Then the entity should be 331
	    And the AI should be 3312
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be LENGTH (m), log
		And the description should be Length or first dimension, metres
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse WIDTH (m), log
	Given the input is 3322123456
	When the input to submitted to the parser
	Then the entity should be 332
	    And the AI should be 3322
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be WIDTH (m), log
		And the description should be Width, diameter, or second dimension, metres
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse HEIGHT (m), log
	Given the input is 3332123456
	When the input to submitted to the parser
	Then the entity should be 333
	    And the AI should be 3332
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be HEIGHT (m), log
		And the description should be Depth, thickness, height, or third dimension, metres
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse AREA (m²), log
	Given the input is 3342123456
	When the input to submitted to the parser
	Then the entity should be 334
	    And the AI should be 3342
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be AREA (m²), log
		And the description should be Area, square metres
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse VOLUME (l), log
	Given the input is 3352123456
	When the input to submitted to the parser
	Then the entity should be 335
	    And the AI should be 3352
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be VOLUME (l), log
		And the description should be Logistic volume, litres
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse VOLUME (m³), log
	Given the input is 3362123456
	When the input to submitted to the parser
	Then the entity should be 336
	    And the AI should be 3362
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be VOLUME (m³), log
		And the description should be Logistic volume, cubic metres
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse KG PER m²
	Given the input is 3372123456
	When the input to submitted to the parser
	Then the entity should be 337
	    And the AI should be 3372
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be KG PER m²
		And the description should be Kilograms per square metre
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse GROSS WEIGHT (lb)
	Given the input is 3402123456
	When the input to submitted to the parser
	Then the entity should be 340
	    And the AI should be 3402
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be GROSS WEIGHT (lb)
		And the description should be Logistic weight, pounds
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse LENGTH (i), log
	Given the input is 3412123456
	When the input to submitted to the parser
	Then the entity should be 341
	    And the AI should be 3412
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be LENGTH (i), log
		And the description should be Length or first dimension, inches
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse LENGTH (f), log
	Given the input is 3422123456
	When the input to submitted to the parser
	Then the entity should be 342
	    And the AI should be 3422
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be LENGTH (f), log
		And the description should be Length or first dimension, feet
		And the length of the value should be fixed
		And there should be no errors

@N4+N6 
Scenario: Parse LENGTH (y), log
	Given the input is 3432123456
	When the input to submitted to the parser
	Then the entity should be 343
	    And the AI should be 3432
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be LENGTH (y), log
		And the description should be Length or first dimension, yards
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse WIDTH (i), log
	Given the input is 3442123456
	When the input to submitted to the parser
	Then the entity should be 344
	    And the AI should be 3442
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be WIDTH (i), log
		And the description should be Width, diameter, or second dimension, inches
		And the length of the value should be fixed
		And there should be no errors



@N4+N6
Scenario: Parse WIDTH (f), log
	Given the input is 3452123456
	When the input to submitted to the parser
	Then the entity should be 345
	    And the AI should be 3452
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be WIDTH (f), log
		And the description should be Width, diameter, or second dimension, feet
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse WIDTH (y), log
	Given the input is 3462123456
	When the input to submitted to the parser
	Then the entity should be 346
	    And the AI should be 3462
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be WIDTH (y), log
		And the description should be Width, diameter, or second dimension, yard
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse HEIGHT (i), log
	Given the input is 3472123456
	When the input to submitted to the parser
	Then the entity should be 347
	    And the AI should be 3472
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be HEIGHT (i), log
		And the description should be Depth, thickness, height, or third dimension, inches
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse HEIGHT (f), log
	Given the input is 3482123456
	When the input to submitted to the parser
	Then the entity should be 348
	    And the AI should be 3482
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be HEIGHT (f), log
		And the description should be Depth, thickness, height, or third dimension, feet
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse HEIGHT (y), log
	Given the input is 3492123456
	When the input to submitted to the parser
	Then the entity should be 349
	    And the AI should be 3492
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be HEIGHT (y), log
		And the description should be Depth, thickness, height, or third dimension, yards
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse AREA (i²)
	Given the input is 3502123456
	When the input to submitted to the parser
	Then the entity should be 350
	    And the AI should be 3502
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be AREA (i²)
		And the description should be Area, square inches (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse AREA (f²)
	Given the input is 3512123456
	When the input to submitted to the parser
	Then the entity should be 351
	    And the AI should be 3512
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be AREA (f²)
		And the description should be Area, square feet (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse AREA (y²)
	Given the input is 3522123456
	When the input to submitted to the parser
	Then the entity should be 352
	    And the AI should be 3522
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be AREA (y²)
		And the description should be Area, square yards (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse AREA (i²), log
	Given the input is 3532123456
	When the input to submitted to the parser
	Then the entity should be 353
	    And the AI should be 3532
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be AREA (i²), log
		And the description should be Area, square inches
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse AREA (f²), log
	Given the input is 3542123456
	When the input to submitted to the parser
	Then the entity should be 354
	    And the AI should be 3542
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be AREA (f²), log
		And the description should be Area, square feet
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse AREA (y²), log
	Given the input is 3552123456
	When the input to submitted to the parser
	Then the entity should be 355
	    And the AI should be 3552
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be AREA (y²), log
		And the description should be Area, square yards
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse NET WEIGHT (t)
	Given the input is 3562123456
	When the input to submitted to the parser
	Then the entity should be 356
	    And the AI should be 3562
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be NET WEIGHT (t)
		And the description should be Net weight, troy ounces (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse NET VOLUME (oz)
	Given the input is 3572123456
	When the input to submitted to the parser
	Then the entity should be 357
	    And the AI should be 3572
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be NET VOLUME (oz)
		And the description should be Net weight (or volume), ounces (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse NET VOLUME (q)
	Given the input is 3602123456
	When the input to submitted to the parser
	Then the entity should be 360
	    And the AI should be 3602
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be NET VOLUME (q)
		And the description should be Net volume, quarts (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse NET VOLUME (g)
	Given the input is 3612123456
	When the input to submitted to the parser
	Then the entity should be 361
	    And the AI should be 3612
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be NET VOLUME (g)
		And the description should be Net volume, gallons U.S. (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse VOLUME (q), log
	Given the input is 3622123456
	When the input to submitted to the parser
	Then the entity should be 362
	    And the AI should be 3622
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be VOLUME (q), log
		And the description should be Logistic volume, quarts
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse VOLUME (g), log
	Given the input is 3632123456
	When the input to submitted to the parser
	Then the entity should be 363
	    And the AI should be 3632
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be VOLUME (g), log
		And the description should be Logistic volume, gallons U.S.
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse VOLUME (i³)
	Given the input is 3642123456
	When the input to submitted to the parser
	Then the entity should be 364
	    And the AI should be 3642
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be VOLUME (i³)
		And the description should be Net volume, cubic inches (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse VOLUME (f³)
	Given the input is 3652123456
	When the input to submitted to the parser
	Then the entity should be 365
	    And the AI should be 3652
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be VOLUME (f³)
		And the description should be Net volume, cubic feet (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse VOLUME (y³)
	Given the input is 3662123456
	When the input to submitted to the parser
	Then the entity should be 366
	    And the AI should be 3662
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be VOLUME (y³)
		And the description should be Net volume, cubic yards (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse VOLUME (i³), log
	Given the input is 3672123456
	When the input to submitted to the parser
	Then the entity should be 367
	    And the AI should be 3672
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be VOLUME (i³), log
		And the description should be Logistic volume, cubic inches
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse VOLUME (f³), log
	Given the input is 3682123456
	When the input to submitted to the parser
	Then the entity should be 368
	    And the AI should be 3682
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be VOLUME (f³), log
		And the description should be Logistic volume, cubic feet
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse VOLUME (y³), log
	Given the input is 3692123456
	When the input to submitted to the parser
	Then the entity should be 369
	    And the AI should be 3692
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be VOLUME (y³), log
		And the description should be Logistic volume, cubic yards
		And the length of the value should be fixed
		And there should be no errors

@N2+N..8
Scenario: Parse COUNT
	Given the input is 3712345678
	When the input to submitted to the parser
	Then the entity should be 37
	    And the AI should be 37
		And the value should be 12345678
		And the data value should be COUNT
		And the description should be Count of trade items or trade item pieces contained in a logistic unit
		And the length of the value should be variable
		And there should be no errors

@N4+N..15
Scenario: Parse AMOUNT
	Given the input is 3902123456789012345
	When the input to submitted to the parser
	Then the entity should be 390
	    And the AI should be 3902
		And the inverse exponent should be 2
		And the value should be 123456789012345
		And the data value should be AMOUNT
		And the description should be Amount payable or coupon value - Single monetary area
		And the length of the value should be variable
		And there should be no errors

@N4+N3+N..15
Scenario: Parse AMOUNT (ISO)
	Given the input is 3912826123456789012345
	When the input to submitted to the parser
	Then the entity should be 391
	    And the AI should be 3912
		And the inverse exponent should be 2
		And the value should be 826123456789012345
		And the data value should be AMOUNT
		And the description should be Amount payable and ISO currency code
		And the length of the value should be variable
		And there should be no errors

@N4+N..15
Scenario: Parse PRICE
	Given the input is 3922123456789012345
	When the input to submitted to the parser
	Then the entity should be 392
	    And the AI should be 3922
		And the inverse exponent should be 2
		And the value should be 123456789012345
		And the data value should be PRICE
		And the description should be Amount payable for a variable measure trade item - Single monetary area
		And the length of the value should be variable
		And there should be no errors

@N4+N3+N..15
Scenario: Parse PRICE (ISO)
	Given the input is 3932826123456789012345
	When the input to submitted to the parser
	Then the entity should be 393
	    And the AI should be 3932
		And the inverse exponent should be 2
		And the value should be 826123456789012345
		And the data value should be PRICE
		And the description should be Amount payable for a variable measure trade item and ISO currency code
		And the length of the value should be variable
		And there should be no errors

@N4+N4
Scenario: Parse PRCNT OFF
	Given the input is 39421234
	When the input to submitted to the parser
	Then the entity should be 394
	    And the AI should be 3942
		And the inverse exponent should be 2
		And the value should be 1234
		And the data value should be PRCNT OFF
		And the description should be Percentage discount of a coupon
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse PRICE/UoM
	Given the input is 3952123456
	When the input to submitted to the parser
	Then the entity should be 395
	    And the AI should be 3952
		And the inverse exponent should be 2
		And the value should be 123456
		And the data value should be PRICE/UoM
		And the description should be Amount payable per unit of measure single monetary area (variable measure trade item)
		And the length of the value should be fixed
		And there should be no errors

@N3+X..30
Scenario: Parse ORDER NUMBER
	Given the input is 4001234567890
	When the input to submitted to the parser
	Then the entity should be 400
	    And the AI should be 400
		And the value should be 1234567890
		And the data value should be ORDER NUMBER
		And the description should be Customer's purchase order number
		And the length of the value should be variable
		And there should be no errors

@N3+X..30
Scenario: Parse GINC
	Given the input is 401506091751123456
	When the input to submitted to the parser
	Then the entity should be 401
	    And the AI should be 401
		And the value should be 506091751123456
		And the data value should be GINC
		And the description should be Global Identification Number for Consignment (GINC)
		And the length of the value should be variable
		And there should be no errors

@N3+N17
Scenario: Parse GSIN
	Given the input is 40250609175112345629
	When the input to submitted to the parser
	Then the entity should be 402
	    And the AI should be 402
		And the value should be 50609175112345629
		And the data value should be GSIN
		And the description should be Global Shipment Identification Number (GSIN)
		And the length of the value should be fixed
		And there should be no errors

@N3+X..30
Scenario: Parse ROUTE
	Given the input is 403Routing+Code+1234
	When the input to submitted to the parser
	Then the entity should be 403
	    And the AI should be 403
		And the value should be Routing+Code+1234
		And the data value should be ROUTE
		And the description should be Routing code
		And the length of the value should be variable
		And there should be no errors

@N3+N13
Scenario: Parse SHIP TO LOC
	Given the input is 4105060917510004
	When the input to submitted to the parser
	Then the entity should be 410
	    And the AI should be 410
		And the value should be 5060917510004
		And the data value should be SHIP TO LOC
		And the description should be Ship to - Deliver to Global Location Number (GLN)
		And the length of the value should be fixed
		And there should be no errors

@N3+N13
Scenario: Parse BILL TO
	Given the input is 4115060917510004
	When the input to submitted to the parser
	Then the entity should be 411
	    And the AI should be 411
		And the value should be 5060917510004
		And the data value should be BILL TO
		And the description should be Bill to - Invoice to Global Location Number (GLN)
		And the length of the value should be fixed
		And there should be no errors

@N3+N13
Scenario: Parse PURCHASE FROM
	Given the input is 4125060917510004
	When the input to submitted to the parser
	Then the entity should be 412
	    And the AI should be 412
		And the value should be 5060917510004
		And the data value should be PURCHASE FROM
		And the description should be Purchased from Global Location Number (GLN)
		And the length of the value should be fixed
		And there should be no errors

@N3+N13
Scenario: Parse SHIP FOR LOC
	Given the input is 4135060917510004
	When the input to submitted to the parser
	Then the entity should be 413
	    And the AI should be 413
		And the value should be 5060917510004
		And the data value should be SHIP FOR LOC
		And the description should be Ship for - Deliver for - Forward to Global Location Number (GLN)
		And the length of the value should be fixed
		And there should be no errors

@N3+N13
Scenario: Parse LOC No.
	Given the input is 4145060917510004
	When the input to submitted to the parser
	Then the entity should be 414
	    And the AI should be 414
		And the value should be 5060917510004
		And the data value should be LOC No.
		And the description should be Identification of a physical location - Global Location Number (GLN)
		And the length of the value should be fixed
		And there should be no errors

@N3+N13
Scenario: Parse PAY TO
	Given the input is 4155060917510004
	When the input to submitted to the parser
	Then the entity should be 415
	    And the AI should be 415
		And the value should be 5060917510004
		And the data value should be PAY TO
		And the description should be Global Location Number (GLN) of the invoicing party
		And the length of the value should be fixed
		And there should be no errors

@N3+N13
Scenario: Parse PROD/SERV LOC
	Given the input is 4165060917510004
	When the input to submitted to the parser
	Then the entity should be 416
	    And the AI should be 416
		And the value should be 5060917510004
		And the data value should be PROD/SERV LOC
		And the description should be Global Location Number (GLN) of the production or service location
		And the length of the value should be fixed
		And there should be no errors

@N3+N13
Scenario: Parse PARTY
	Given the input is 4175060917510004
	When the input to submitted to the parser
	Then the entity should be 417
	    And the AI should be 417
		And the value should be 5060917510004
		And the data value should be PARTY
		And the description should be Party Global Location Number (GLN)
		And the length of the value should be fixed
		And there should be no errors

@N3+X..20
Scenario: Parse SHIP TO POST
	Given the input is 420SE220PF
	When the input to submitted to the parser
	Then the entity should be 420
	    And the AI should be 420
		And the value should be SE220PF
		And the data value should be SHIP TO POST
		And the description should be Ship-to / Deliver-to postal code within a single postal authority
		And the length of the value should be variable
		And there should be no errors

@N3+N3+X..9
Scenario: Parse SHIP TO POST ISO
	Given the input is 421826SE220PF
	When the input to submitted to the parser
	Then the entity should be 421
	    And the AI should be 421
		And the value should be 826SE220PF
		And the data value should be SHIP TO POST
		And the description should be Ship-to / Deliver-to postal code with three-digit ISO country code
		And the length of the value should be variable
		And there should be no errors

@N3+N3
Scenario: Parse ORIGIN
	Given the input is 422826
	When the input to submitted to the parser
	Then the entity should be 422
	    And the AI should be 422
		And the value should be 826
		And the data value should be ORIGIN
		And the description should be Country of origin of a trade item
		And the length of the value should be fixed
		And there should be no errors

@N3+N3+N..12
Scenario: Parse COUNTRY - INITIAL PROCESS
	Given the input is 423826
	When the input to submitted to the parser
	Then the entity should be 423
	    And the AI should be 423
		And the value should be 826
		And the data value should be COUNTRY - INITIAL PROCESS
		And the description should be Country of initial processing
		And the length of the value should be variable
		And there should be no errors

@N3+N3
Scenario: Parse COUNTRY - PROCESS
	Given the input is 424826
	When the input to submitted to the parser
	Then the entity should be 424
	    And the AI should be 424
		And the value should be 826
		And the data value should be COUNTRY - PROCESS
		And the description should be Country of processing
		And the length of the value should be fixed
		And there should be no errors

@N3+N3+N..12
Scenario: Parse COUNTRY - DISASSEMBLY
	Given the input is 425826
	When the input to submitted to the parser
	Then the entity should be 425
	    And the AI should be 425
		And the value should be 826
		And the data value should be COUNTRY - DISASSEMBLY
		And the description should be Country of disassembly
		And the length of the value should be variable
		And there should be no errors

@N3+N3
Scenario: Parse COUNTRY – FULL PROCESS
	Given the input is 426826
	When the input to submitted to the parser
	Then the entity should be 426
	    And the AI should be 426
		And the value should be 826
		And the data value should be COUNTRY – FULL PROCESS
		And the description should be Country covering full process chain
		And the length of the value should be fixed
		And there should be no errors

@N3+X..3
Scenario: Parse ORIGIN SUBDIVISION
	Given the input is 427ENG
	When the input to submitted to the parser
	Then the entity should be 427
	    And the AI should be 427
		And the value should be ENG
		And the data value should be ORIGIN SUBDIVISION
		And the description should be Country subdivision of origin code for a trade item
		And the length of the value should be variable
		And there should be no errors

@N4+X..35
Scenario: Parse SHIP TO COMP
	Given the input is 4300Acme+Corp
	When the input to submitted to the parser
	Then the entity should be 4300
	    And the AI should be 4300
		And the value should be Acme+Corp
		And the data value should be SHIP TO COMP
		And the description should be Ship-to / Deliver-to Company name
		And the length of the value should be variable
		And there should be no errors

@N4+X..35
Scenario: Parse SHIP TO NAME
	Given the input is 4301John+Smith
	When the input to submitted to the parser
	Then the entity should be 4301
	    And the AI should be 4301
		And the value should be John+Smith
		And the data value should be SHIP TO NAME
		And the description should be Ship-to / Deliver-to contact name
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse SHIP TO ADD1
	Given the input is 4302100+Acadia+Avenue
	When the input to submitted to the parser
	Then the entity should be 4302
	    And the AI should be 4302
		And the value should be 100+Acadia+Avenue
		And the data value should be SHIP TO ADD1
		And the description should be Ship-to / Deliver-to address line 1
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse SHIP TO ADD2
	Given the input is 4303Noborough
	When the input to submitted to the parser
	Then the entity should be 4303
	    And the AI should be 4303
		And the value should be Noborough
		And the data value should be SHIP TO ADD2
		And the description should be Ship-to / Deliver-to address line 2
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse SHIP TO SUB
	Given the input is 4304Lower+District
	When the input to submitted to the parser
	Then the entity should be 4304
	    And the AI should be 4304
		And the value should be Lower+District
		And the data value should be SHIP TO SUB
		And the description should be Ship-to / Deliver-to suburb
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse SHIP TO LOCALITY
	Given the input is 4305Anytown
	When the input to submitted to the parser
	Then the entity should be 4305
	    And the AI should be 4305
		And the value should be Anytown
		And the data value should be SHIP TO LOC
		And the description should be Ship-to / Deliver-to locality
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse SHIP TO REG
	Given the input is 4306United+Kingdom
	When the input to submitted to the parser
	Then the entity should be 4306
	    And the AI should be 4306
		And the value should be United+Kingdom
		And the data value should be SHIP TO REG
		And the description should be Ship-to / Deliver-to region
		And the length of the value should be variable
		And there should be no errors

@N4+X2
Scenario: Parse SHIP TO COUNTRY
	Given the input is 4307GB
	When the input to submitted to the parser
	Then the entity should be 4307
	    And the AI should be 4307
		And the value should be GB
		And the data value should be SHIP TO COUNTRY
		And the description should be Ship-to / Deliver-to country code
		And the length of the value should be fixed
		And there should be no errors

@N4+X..30
Scenario: Parse SHIP TO PHONE
	Given the input is 4308+32-2-788-78-00
	When the input to submitted to the parser
	Then the entity should be 4308
	    And the AI should be 4308
		And the value should be +32-2-788-78-00
		And the data value should be SHIP TO PHONE
		And the description should be Ship-to / Deliver-to telephone number
		And the length of the value should be variable
		And there should be no errors

@N4+N20
Scenario: Parse SHIP TO GEO
	Given the input is 430902790858483015297971
	When the input to submitted to the parser
	Then the entity should be 4309
	    And the AI should be 4309
		And the value should be 02790858483015297971
		And the data value should be SHIP TO GEO
		And the description should be Ship-to / Deliver-to GEO location
		And the length of the value should be fixed
		And there should be no errors

@N4+X..35
Scenario: Parse RTN TO COMP
	Given the input is 4310Acme+Corp
	When the input to submitted to the parser
	Then the entity should be 4310
	    And the AI should be 4310
		And the value should be Acme+Corp
		And the data value should be RTN TO COMP
		And the description should be Return-to company name
		And the length of the value should be variable
		And there should be no errors

@N4+X..35
Scenario: Parse RTN TO NAME
	Given the input is 4311John+Smith
	When the input to submitted to the parser
	Then the entity should be 4311
	    And the AI should be 4311
		And the value should be John+Smith
		And the data value should be RTN TO NAME
		And the description should be Return-to contact name
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse RTN TO ADD1
	Given the input is 4312100+Acadia+Avenue
	When the input to submitted to the parser
	Then the entity should be 4312
	    And the AI should be 4312
		And the value should be 100+Acadia+Avenue
		And the data value should be RTN TO ADD1
		And the description should be Return-to address line 1
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse RTN TO ADD2
	Given the input is 4313Noborough
	When the input to submitted to the parser
	Then the entity should be 4313
	    And the AI should be 4313
		And the value should be Noborough
		And the data value should be RTN TO ADD2
		And the description should be Return-to address line 2
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse RTN TO SUB
	Given the input is 4314Lower+District
	When the input to submitted to the parser
	Then the entity should be 4314
	    And the AI should be 4314
		And the value should be Lower+District
		And the data value should be RTN TO SUB
		And the description should be Return-to suburb
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse RTN TO LOC
	Given the input is 4315Anytown
	When the input to submitted to the parser
	Then the entity should be 4315
	    And the AI should be 4315
		And the value should be Anytown
		And the data value should be RTN TO LOC
		And the description should be Return-to locality
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse RTN TO REG
	Given the input is 4316United+Kingdom
	When the input to submitted to the parser
	Then the entity should be 4316
	    And the AI should be 4316
		And the value should be United+Kingdom
		And the data value should be RTN TO REG
		And the description should be Return-to region
		And the length of the value should be variable
		And there should be no errors

@N4+X2
Scenario: Parse RTN TO COUNTRY
	Given the input is 4317GB
	When the input to submitted to the parser
	Then the entity should be 4317
	    And the AI should be 4317
		And the value should be GB
		And the data value should be RTN TO COUNTRY
		And the description should be Return-to country code
		And the length of the value should be fixed
		And there should be no errors

@N4+X..20
Scenario: Parse RTN TO POST
	Given the input is 4318SE220PF
	When the input to submitted to the parser
	Then the entity should be 4318
	    And the AI should be 4318
		And the value should be SE220PF
		And the data value should be RTN TO POST
		And the description should be Return-to postal code
		And the length of the value should be variable
		And there should be no errors

@N4+X..30
Scenario: Parse RTN TO PHONE
	Given the input is 4319+32-2-788-78-00
	When the input to submitted to the parser
	Then the entity should be 4319
	    And the AI should be 4319
		And the value should be +32-2-788-78-00
		And the data value should be RTN TO PHONE
		And the description should be Return-to telephone number
		And the length of the value should be variable
		And there should be no errors

@N4+X..35
Scenario: Parse SRV DESCRIPTION
	Given the input is 4320Service+code+description+12345
	When the input to submitted to the parser
	Then the entity should be 4320
	    And the AI should be 4320
		And the value should be Service+code+description+12345
		And the data value should be SRV DESCRIPTION
		And the description should be Service code description
		And the length of the value should be variable
		And there should be no errors

@N4+N1
Scenario: Parse DANGEROUS GOODS
	Given the input is 43211
	When the input to submitted to the parser
	Then the entity should be 4321
	    And the AI should be 4321
		And the value should be 1
		And the data value should be DANGEROUS GOODS
		And the description should be Dangerous goods flag
		And the length of the value should be fixed
		And there should be no errors

@N4+N1
Scenario: Parse AUTH LEAVE
	Given the input is 43221
	When the input to submitted to the parser
	Then the entity should be 4322
	    And the AI should be 4322
		And the value should be 1
		And the data value should be AUTH LEAVE
		And the description should be Authority to leave flag
		And the length of the value should be fixed
		And there should be no errors

@N4+N1
Scenario: Parse SIG REQUIRED
	Given the input is 43231
	When the input to submitted to the parser
	Then the entity should be 4323
	    And the AI should be 4323
		And the value should be 1
		And the data value should be SIG REQUIRED
		And the description should be Signature required flag
		And the length of the value should be fixed
		And there should be no errors

@N4+N10
Scenario: Parse NBEF DEL DT
	Given the input is 43242312310900
	When the input to submitted to the parser
	Then the entity should be 4324
	    And the AI should be 4324
		And the value should be 2312310900
		And the data value should be NBEF DEL DT
		And the description should be Not before delivery date/time
		And the length of the value should be fixed
		And there should be no errors

@N4+N10
Scenario: Parse NAFT DEL DT
	Given the input is 43252312311700
	When the input to submitted to the parser
	Then the entity should be 4325
	    And the AI should be 4325
		And the value should be 2312311700
		And the data value should be NAFT DEL DT
		And the description should be Not after delivery date/time
		And the length of the value should be fixed
		And there should be no errors

@N4+N6
Scenario: Parse REL DATE
	Given the input is 4326231231
	When the input to submitted to the parser
	Then the entity should be 4326
	    And the AI should be 4326
		And the value should be 231231
		And the data value should be REL DATE
		And the description should be Release date
		And the length of the value should be fixed
		And there should be no errors

@N4+N6+[-]
Scenario: Parse MAX TEMP F
	Given the input is 4330023020
	When the input to submitted to the parser
	Then the entity should be 4330
	    And the AI should be 4330
		And the value should be 023020
		And the data value should be MAX TEMP F
		And the description should be Maximum temperature in Fahrenheit 
		And the length of the value should be variable
		And there should be no errors

@N4+N6+[-]
Scenario: Parse MAX TEMP C
	Given the input is 4331000090
	When the input to submitted to the parser
	Then the entity should be 4331
	    And the AI should be 4331
		And the value should be 000090
		And the data value should be MAX TEMP C
		And the description should be Maximum temperature in Celsius
		And the length of the value should be variable
		And there should be no errors

@N4+N6+[-]
Scenario: Parse MIN TEMP F
	Given the input is 4332023020
	When the input to submitted to the parser
	Then the entity should be 4332
	    And the AI should be 4332
		And the value should be 023020
		And the data value should be MIN TEMP F
		And the description should be Minimum temperature in Fahrenheit
		And the length of the value should be variable
		And there should be no errors

@N4+N6+[-]
Scenario: Parse MIN TEMP C
	Given the input is 4333000090
	When the input to submitted to the parser
	Then the entity should be 4333
	    And the AI should be 4333
		And the value should be 000090
		And the data value should be MIN TEMP C
		And the description should be Minimum temperature in Celsius
		And the length of the value should be variable
		And there should be no errors

@N4+N6+[-]
Scenario: Parse negative MAX TEMP F
	Given the input is 4330000250-
	When the input to submitted to the parser
	Then the entity should be 4330
	    And the AI should be 4330
		And the value should be 000250-
		And the data value should be MAX TEMP F
		And the description should be Maximum temperature in Fahrenheit 
		And the length of the value should be variable
		And there should be no errors

@N4+N6+[-]
Scenario: Parse negative MAX TEMP C
	Given the input is 4331001000-
	When the input to submitted to the parser
	Then the entity should be 4331
	    And the AI should be 4331
		And the value should be 001000-
		And the data value should be MAX TEMP C
		And the description should be Maximum temperature in Celsius
		And the length of the value should be variable
		And there should be no errors

@N4+N6+[-]
Scenario: Parse negative MIN TEMP F
	Given the input is 4332000250-
	When the input to submitted to the parser
	Then the entity should be 4332
	    And the AI should be 4332
		And the value should be 000250-
		And the data value should be MIN TEMP F
		And the description should be Minimum temperature in Fahrenheit
		And the length of the value should be variable
		And there should be no errors

@N4+N6+[-]
Scenario: Parse negative MIN TEMP C
	Given the input is 4333001000-
	When the input to submitted to the parser
	Then the entity should be 4333
	    And the AI should be 4333
		And the value should be 001000-
		And the data value should be MIN TEMP C
		And the description should be Minimum temperature in Celsius
		And the length of the value should be variable
		And there should be no errors

@N4+N13
Scenario: Parse NSN
	Given the input is 70015310997032519
	When the input to submitted to the parser
	Then the entity should be 7001
	    And the AI should be 7001
		And the value should be 5310997032519
		And the data value should be NSN
		And the description should be NATO Stock Number (NSN)
		And the length of the value should be fixed
		And there should be no errors

@N4+X..30
Scenario: Parse MEAT CUT
	Given the input is 700244932211340000145100
	When the input to submitted to the parser
	Then the entity should be 7002
	    And the AI should be 7002
		And the value should be 44932211340000145100
		And the data value should be MEAT CUT
		And the description should be UNECE meat carcasses and cuts classification
		And the length of the value should be variable
		And there should be no errors

@N4+N10
Scenario: Parse EXPIRY TIME
	Given the input is 70032312312359
	When the input to submitted to the parser
	Then the entity should be 7003
	    And the AI should be 7003
		And the value should be 2312312359
		And the data value should be EXPIRY TIME
		And the description should be Expiration date and time
		And the length of the value should be fixed
		And there should be no errors

@N4+N..4
Scenario: Parse ACTIVE POTENCY
	Given the input is 70043001
	When the input to submitted to the parser
	Then the entity should be 7004
	    And the AI should be 7004
		And the value should be 3001
		And the data value should be ACTIVE POTENCY
		And the description should be Active potency
		And the length of the value should be variable
		And there should be no errors

@N4+X..12
Scenario: Parse CATCH AREA
	Given the input is 700527.6.b.1
	When the input to submitted to the parser
	Then the entity should be 7005
	    And the AI should be 7005
		And the value should be 27.6.b.1
		And the data value should be CATCH AREA
		And the description should be Catch area
		And the length of the value should be variable
		And there should be no errors

@N4+N6
Scenario: Parse FIRST FREEZE DATE
	Given the input is 7006231231
	When the input to submitted to the parser
	Then the entity should be 7006
	    And the AI should be 7006
		And the value should be 231231
		And the data value should be FIRST FREEZE DATE
		And the description should be First freeze date
		And the length of the value should be fixed
		And there should be no errors

@N4+N6[+N6]
Scenario: Parse HARVEST DATE
	Given the input is 7007230801230831
	When the input to submitted to the parser
	Then the entity should be 7007
	    And the AI should be 7007
		And the value should be 230801230831
		And the data value should be HARVEST DATE
		And the description should be Harvest date
		And the length of the value should be variable
		And there should be no errors

@N4+X..3
Scenario: Parse AQUATIC SPECIES
	Given the input is 7008BWQ
	When the input to submitted to the parser
	Then the entity should be 7008
	    And the AI should be 7008
		And the value should be BWQ
		And the data value should be AQUATIC SPECIES
		And the description should be Species for fishery purposes
		And the length of the value should be variable
		And there should be no errors

@N4+X..10
Scenario: Parse FISHING GEAR TYPE
	Given the input is 700901.1.1
	When the input to submitted to the parser
	Then the entity should be 7009
	    And the AI should be 7009
		And the value should be 01.1.1
		And the data value should be FISHING GEAR TYPE
		And the description should be Fishing gear type
		And the length of the value should be variable
		And there should be no errors

@N4+X..2
Scenario: Parse PROD METHOD
	Given the input is 701001
	When the input to submitted to the parser
	Then the entity should be 7010
	    And the AI should be 7010
		And the value should be 01
		And the data value should be PROD METHOD
		And the description should be Production method
		And the length of the value should be variable
		And there should be no errors

@N4+N6[+N4]
Scenario: Parse TEST BY DATE
	Given the input is 70112312311200
	When the input to submitted to the parser
	Then the entity should be 7011
	    And the AI should be 7011
		And the value should be 2312311200
		And the data value should be TEST BY DATE
		And the description should be Test by date
		And the length of the value should be variable
		And there should be no errors

@N4+X..20
Scenario: Parse REFURB LOT
	Given the input is 7020ABC123DE
	When the input to submitted to the parser
	Then the entity should be 7020
	    And the AI should be 7020
		And the value should be ABC123DE
		And the data value should be REFURB LOT
		And the description should be Refurbishment lot ID
		And the length of the value should be variable
		And there should be no errors

@N4+X..20
Scenario: Parse FUNC STAT
	Given the input is 7021Functional+status+01
	When the input to submitted to the parser
	Then the entity should be 7021
	    And the AI should be 7021
		And the value should be Functional+status+01
		And the data value should be FUNC STAT
		And the description should be Functional status
		And the length of the value should be variable
		And there should be no errors

@N4+X..20
Scenario: Parse REV STAT
	Given the input is 7022Revision+status+01
	When the input to submitted to the parser
	Then the entity should be 7022
	    And the AI should be 7022
		And the value should be Revision+status+01
		And the data value should be REV STAT
		And the description should be Revision status
		And the length of the value should be variable
		And there should be no errors

@N4+X..30
Scenario: Parse GIAI – ASSEMBLY
	Given the input is 7023506091751ASSET+0001
	When the input to submitted to the parser
	Then the entity should be 7023
	    And the AI should be 7023
		And the value should be 506091751ASSET+0001
		And the data value should be GIAI – ASSEMBLY
		And the description should be Global Individual Asset Identifier of an assembly
		And the length of the value should be variable
		And there should be no errors

@N4+N3+X..27
Scenario: Parse PROCESSOR # s
	Given the input is 7030826FSSC+22000+-+00020281
	When the input to submitted to the parser
	Then the entity should be 703
	    And the AI should be 7030
		And the sequence number should be 0
		And the value should be 826FSSC+22000+-+00020281
		And the data value should be PROCESSOR # s
		And the description should be Number of processor with three-digit ISO country code
		And the length of the value should be variable
		And there should be no errors

@N4+N1+X3
Scenario: Parse UIC+EXT
	Given the input is 70403PA_
	When the input to submitted to the parser
	Then the entity should be 7040
	    And the AI should be 7040
		And the value should be 3PA_
		And the data value should be UIC+EXT
		And the description should be GS1 UIC with Extension 1 and Importer index
		And the length of the value should be fixed
		And there should be no errors

@N4+N1..X4
Scenario: Parse UFRGT UNIT TYPE
	Given the input is 70411A
	When the input to submitted to the parser
	Then the entity should be 7041
	    And the AI should be 7041
		And the value should be 1A
		And the data value should be UFRGT UNIT TYPE
		And the description should be UN/CEFACT freight unit type
		And the length of the value should be variable
		And there should be no errors

@N3+X..20
Scenario: Parse NHRN PZN
	Given the input is 7103675419
	When the input to submitted to the parser
	Then the entity should be 710
	    And the AI should be 710
		And the value should be 3675419
		And the data value should be NHRN PZN
		And the description should be National Healthcare Reimbursement Number (NHRN) - Germany PZN
		And the length of the value should be variable
		And there should be no errors

@N3+X..20
Scenario: Parse NHRN CIP
	Given the input is 7113400935974419
	When the input to submitted to the parser
	Then the entity should be 711
	    And the AI should be 711
		And the value should be 3400935974419
		And the data value should be NHRN CIP
		And the description should be National Healthcare Reimbursement Number (NHRN) - France CIP
		And the length of the value should be variable
		And there should be no errors

@N3+X..20
Scenario: Parse NHRN CN
	Given the input is 712384756.8
	When the input to submitted to the parser
	Then the entity should be 712
	    And the AI should be 712
		And the value should be 384756.8
		And the data value should be NHRN CN
		And the description should be National Healthcare Reimbursement Number (NHRN) - Spain CN
		And the length of the value should be variable
		And there should be no errors

@N3+X..20
Scenario: Parse NHRN DRN
	Given the input is 71340056320000011
	When the input to submitted to the parser
	Then the entity should be 713
	    And the AI should be 713
		And the value should be 40056320000011
		And the data value should be NHRN DRN
		And the description should be National Healthcare Reimbursement Number (NHRN) - Brasil DRN
		And the length of the value should be variable
		And there should be no errors

@N3+X..20
Scenario: Parse NHRN AIM
	Given the input is 714142199
	When the input to submitted to the parser
	Then the entity should be 714
	    And the AI should be 714
		And the value should be 142199
		And the data value should be NHRN AIM
		And the description should be National Healthcare Reimbursement Number (NHRN) - Portugal AIM
		And the length of the value should be variable
		And there should be no errors

@N3+X..20
Scenario: Parse NHRN NDC
	Given the input is 7150777310502
	When the input to submitted to the parser
	Then the entity should be 715
	    And the AI should be 715
		And the value should be 0777310502
		And the data value should be NHRN NDC
		And the description should be National Healthcare Reimbursement Number (NHRN) - United States of America NDC
		And the length of the value should be variable
		And there should be no errors

@N3+X..20
Scenario: Parse NHRN AIC
	Given the input is 716A012345676
	When the input to submitted to the parser
	Then the entity should be 716
	    And the AI should be 716
		And the value should be A012345676
		And the data value should be NHRN AIC
		And the description should be National Healthcare Reimbursement Number (NHRN) – Italy AIC
		And the length of the value should be variable
		And there should be no errors

@N4+X2+X..28
Scenario: Parse CERT # s
	Given the input is 7230EMBABT-MED00108
	When the input to submitted to the parser
	Then the entity should be 723
	    And the AI should be 7230
		And the sequence number should be 0
		And the value should be EMBABT-MED00108
		And the data value should be CERT # s
		And the description should be Certification reference
		And the length of the value should be variable
		And there should be no errors

@N4+X..20
Scenario: Parse PROTOCOL
	Given the input is 7240CACZ885N2301E2
	When the input to submitted to the parser
	Then the entity should be 7240
	    And the AI should be 7240
		And the value should be CACZ885N2301E2
		And the data value should be PROTOCOL
		And the description should be Protocol ID
		And the length of the value should be variable
		And there should be no errors

@N4+N8
Scenario: Parse DOB
	Given the input is 725020240214
	When the input to submitted to the parser
	Then the entity should be 7250
	    And the AI should be 7250
		And the value should be 20240214
		And the data value should be DOB
		And the description should be Date of birth
		And the length of the value should be fixed
		And there should be no errors

@N4+N12
Scenario: Parse DOB TIME
	Given the input is 7251202402141743
	When the input to submitted to the parser
	Then the entity should be 7251
	    And the AI should be 7251
		And the value should be 202402141743
		And the data value should be DOB TIME
		And the description should be Date and time of birth
		And the length of the value should be fixed
		And there should be no errors

@N4+N1
Scenario: Parse BIO SEX
	Given the input is 72521
	When the input to submitted to the parser
	Then the entity should be 7252
	    And the AI should be 7252
		And the value should be 1
		And the data value should be BIO SEX
		And the description should be Biological sex
		And the length of the value should be fixed
		And there should be no errors

@N4+X..40
Scenario: Parse FAMILY NAME
	Given the input is 7253Doe
	When the input to submitted to the parser
	Then the entity should be 7253
	    And the AI should be 7253
		And the value should be Doe
		And the data value should be FAMILY NAME
		And the description should be Family name of person
		And the length of the value should be variable
		And there should be no errors

@N4+X..40
Scenario: Parse GIVEN NAME
	Given the input is 7254John
	When the input to submitted to the parser
	Then the entity should be 7254
	    And the AI should be 7254
		And the value should be John
		And the data value should be GIVEN NAME
		And the description should be Given name of person
		And the length of the value should be variable
		And there should be no errors

@N4+X..10
Scenario: Parse SUFFIX
	Given the input is 7255Junior
	When the input to submitted to the parser
	Then the entity should be 7255
	    And the AI should be 7255
		And the value should be Junior
		And the data value should be SUFFIX
		And the description should be Name suffix of person
		And the length of the value should be variable
		And there should be no errors

@N4+X..90
Scenario: Parse FULL NAME
	Given the input is 7256Doe,John,Junior
	When the input to submitted to the parser
	Then the entity should be 7256
	    And the AI should be 7256
		And the value should be Doe,John,Junior
		And the data value should be FULL NAME
		And the description should be Full name of person
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse PERSON ADDR
	Given the input is 7257123+Main+St,+Anytown,+Anyregion,+12345
	When the input to submitted to the parser
	Then the entity should be 7257
	    And the AI should be 7257
		And the value should be 123+Main+St,+Anytown,+Anyregion,+12345
		And the data value should be PERSON ADDR
		And the description should be Address of person
		And the length of the value should be variable
		And there should be no errors

@N4+N1+X1+N1
Scenario: Parse BIRTH SEQUENCE
	Given the input is 72582/3
	When the input to submitted to the parser
	Then the entity should be 7258
	    And the AI should be 7258
		And the value should be 2/3
		And the data value should be BIRTH SEQUENCE
		And the description should be Baby birth sequence indicator
		And the length of the value should be fixed
		And there should be no errors

@N4+X..40
Scenario: Parse BABY
	Given the input is 7259Alice+Betty
	When the input to submitted to the parser
	Then the entity should be 7259
	    And the AI should be 7259
		And the value should be Alice+Betty
		And the data value should be BABY
		And the description should be Baby of family name
		And the length of the value should be variable
		And there should be no errors

@N4+N2
Scenario: Parse AIDC MEDIA TYPE
	Given the input is 724105
	When the input to submitted to the parser
	Then the entity should be 7241
	    And the AI should be 7241
		And the value should be 05
		And the data value should be AIDC MEDIA TYPE
		And the description should be AIDC media type
		And the length of the value should be fixed
		And there should be no errors

@N4+X..25
Scenario: Parse VCN
	Given the input is 7242094672AG22L44
	When the input to submitted to the parser
	Then the entity should be 7242
	    And the AI should be 7242
		And the value should be 094672AG22L44
		And the data value should be VCN
		And the description should be Version Control Number (VCN)
		And the length of the value should be variable
		And there should be no errors

@N4+N14
Scenario: Parse DIMENSIONS
	Given the input is 800115000003056000
	When the input to submitted to the parser
	Then the entity should be 8001
	    And the AI should be 8001
		And the value should be 15000003056000
		And the data value should be DIMENSIONS
		And the description should be Roll products - width, length, core diameter, direction, splices
		And the length of the value should be fixed
		And there should be no errors

@N4+X..20
Scenario: Parse CMT No.
	Given the input is 8002RF1DB6K177Y
	When the input to submitted to the parser
	Then the entity should be 8002
	    And the AI should be 8002
		And the value should be RF1DB6K177Y
		And the data value should be CMT No.
		And the description should be Cellular mobile telephone identifier
		And the length of the value should be variable
		And there should be no errors

@N4+N14[+X..16]
Scenario: Parse GRAI
	Given the input is 80030506091751000434B1UL09036
	When the input to submitted to the parser
	Then the entity should be 8003
	    And the AI should be 8003
		And the value should be 0506091751000434B1UL09036
		And the data value should be GRAI
		And the description should be Global Returnable Asset Identifier (GRAI)
		And the length of the value should be variable
		And there should be no errors

@N4+X..30
Scenario: Parse GIAI
	Given the input is 80045060917ASSET+0001
	When the input to submitted to the parser
	Then the entity should be 8004
	    And the AI should be 8004
		And the value should be 5060917ASSET+0001
		And the data value should be GIAI
		And the description should be Global Individual Asset Identifier (GIAI)
		And the length of the value should be variable
		And there should be no errors

@N4+N6
Scenario: Parse PRICE PER UNIT
	Given the input is 8005000150
	When the input to submitted to the parser
	Then the entity should be 8005
	    And the AI should be 8005
		And the value should be 000150
		And the data value should be PRICE PER UNIT
		And the description should be Price per unit of measure
		And the length of the value should be fixed
		And there should be no errors

@N4+N14+N2+N2
Scenario: Parse ITIP
	Given the input is 8006050609175100040102
	When the input to submitted to the parser
	Then the entity should be 8006
	    And the AI should be 8006
		And the value should be 050609175100040102
		And the data value should be ITIP
		And the description should be Identification of an individual trade item (ITIP) piece
		And the length of the value should be fixed
		And there should be no errors

@N4+X..34
Scenario: Parse IBAN
	Given the input is 8007GB82WEST12345698765432
	When the input to submitted to the parser
	Then the entity should be 8007
	    And the AI should be 8007
		And the value should be GB82WEST12345698765432
		And the data value should be IBAN
		And the description should be International Bank Account Number (IBAN)
		And the length of the value should be variable
		And there should be no errors

@N4+N8[+N..4]
Scenario: Parse PROD TIME
	Given the input is 8008231231142652
	When the input to submitted to the parser
	Then the entity should be 8008
	    And the AI should be 8008
		And the value should be 231231142652
		And the data value should be PROD TIME
		And the description should be Date and time of production
		And the length of the value should be variable
		And there should be no errors

@N4+X..50
Scenario: Parse OPTSEN
	Given the input is 800901190531
	When the input to submitted to the parser
	Then the entity should be 8009
	    And the AI should be 8009
		And the value should be 01190531
		And the data value should be OPTSEN
		And the description should be Optically readable sensor indicator
		And the length of the value should be variable
		And there should be no errors

@N4+Y..30
Scenario: Parse CPID
	Given the input is 8010506091751DR4529P327
	When the input to submitted to the parser
	Then the entity should be 8010
	    And the AI should be 8010
		And the value should be 506091751DR4529P327
		And the data value should be CPID
		And the description should be Component/Part Identifier (CPID)
		And the length of the value should be variable
		And there should be no errors

@N4+N..12
Scenario: Parse CPID SERIAL
	Given the input is 8011422393761701
	When the input to submitted to the parser
	Then the entity should be 8011
	    And the AI should be 8011
		And the value should be 422393761701
		And the data value should be CPID SERIAL
		And the description should be Component/Part Identifier serial number
		And the length of the value should be variable
		And there should be no errors

@N4+X..20
Scenario: Parse VERSION
	Given the input is 801215.0.4701.1001
	When the input to submitted to the parser
	Then the entity should be 8012
	    And the AI should be 8012
		And the value should be 15.0.4701.1001
		And the data value should be VERSION
		And the description should be Software version
		And the length of the value should be variable
		And there should be no errors

@N4+X..25
Scenario: Parse GMN
	Given the input is 80131987654Ad4X4bL5ttr2310c2K
	When the input to submitted to the parser
	Then the entity should be 8013
	    And the AI should be 8013
		And the value should be 1987654Ad4X4bL5ttr2310c2K
		And the data value should be GMN
		And the description should be Global Model Number (GMN)
		And the length of the value should be variable
		And there should be no errors

@N4+X..25
Scenario: Parse MUDI
	Given the input is 80141987654Ad4X4bL5ttr2310c2K
	When the input to submitted to the parser
	Then the entity should be 8014
	    And the AI should be 8014
		And the value should be 1987654Ad4X4bL5ttr2310c2K
		And the data value should be MUDI
		And the description should be Highly Individualised Device Registration Identifier (HIDRI)
		And the length of the value should be variable
		And there should be no errors

@N4+N18
Scenario: Parse GSRN - PROVIDER
	Given the input is 8017506091751000315180
	When the input to submitted to the parser
	Then the entity should be 8017
	    And the AI should be 8017
		And the value should be 506091751000315180
		And the data value should be GSRN - PROVIDER
		And the description should be Global Service Relation Number (GSRN) to identify the relationship between an organisation offering services and the provider of services
		And the length of the value should be fixed
		And there should be no errors

@N4+N18
Scenario: Parse GSRN - RECIPIENT
	Given the input is 8018506091751000315180
	When the input to submitted to the parser
	Then the entity should be 8018
	    And the AI should be 8018
		And the value should be 506091751000315180
		And the data value should be GSRN - RECIPIENT
		And the description should be Global Service Relation Number (GSRN) to identify the relationship between an organisation offering services and the recipient of services
		And the length of the value should be fixed
		And there should be no errors

@N4+N..10
Scenario: Parse SRIN
	Given the input is 801900499427
	When the input to submitted to the parser
	Then the entity should be 8019
	    And the AI should be 8019
		And the value should be 00499427
		And the data value should be SRIN
		And the description should be Service Relation Instance Number (SRIN)
		And the length of the value should be variable
		And there should be no errors

@N4+X..25
Scenario: Parse REF No.
	Given the input is 8020000B231297726310000
	When the input to submitted to the parser
	Then the entity should be 8020
	    And the AI should be 8020
		And the value should be 000B231297726310000
		And the data value should be REF No.
		And the description should be Payment slip reference number
		And the length of the value should be variable
		And there should be no errors

@N4+N14+N2+N2
Scenario: Parse ITIP CONTENT
	Given the input is 8026050609175100040102
	When the input to submitted to the parser
	Then the entity should be 8026
	    And the AI should be 8026
		And the value should be 050609175100040102
		And the data value should be ITIP CONTENT
		And the description should be Identification of pieces of a trade item (ITIP) contained in a logistic unit
		And the length of the value should be fixed
		And there should be no errors

@N4+Z..90
Scenario: Parse DIGSIG
	Given the input is 803041703319222174341186086981525711229423385405386071942069864323665892369845781264435120798=
	When the input to submitted to the parser
	Then the entity should be 8030
	    And the AI should be 8030
		And the value should be 41703319222174341186086981525711229423385405386071942069864323665892369845781264435120798=
		And the data value should be DIGSIG
		And the description should be Digital Signature (DigSig)
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse COUPON 1a
	Given the input is 8110106141416543213500110000310123196000
	When the input to submitted to the parser
	Then the entity should be 8110
	    And the AI should be 8110
		And the value should be 106141416543213500110000310123196000
		And the data value should be -
		And the description should be Coupon code identification for use in North America
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse COUPON 1b
	Given the input is 81101061414165432131501101201211014092110256100126663101231
	When the input to submitted to the parser
	Then the entity should be 8110
	    And the AI should be 8110
		And the value should be 1061414165432131501101201211014092110256100126663101231
		And the data value should be -
		And the description should be Coupon code identification for use in North America
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse COUPON 1c
	Given the input is 8110106141410012342501106501013085093101231
	When the input to submitted to the parser
	Then the entity should be 8110
	    And the AI should be 8110
		And the value should be 106141410012342501106501013085093101231
		And the data value should be -
		And the description should be Coupon code identification for use in North America
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse COUPON 1d
	Given the input is 8110106141410012471011076011110850921108609310123191000
	When the input to submitted to the parser
	Then the entity should be 8110
	    And the AI should be 8110
		And the value should be 106141410012471011076011110850921108609310123191000
		And the data value should be -
		And the description should be Coupon code identification for use in North America
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse COUPON 1e
	Given the input is 81101061414154321031501101201211014092110256100126663101231
	When the input to submitted to the parser
	Then the entity should be 8110
	    And the AI should be 8110
		And the value should be 1061414154321031501101201211014092110256100126663101231
		And the data value should be -
		And the description should be Coupon code identification for use in North America
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse COUPON 1f
	Given the input is 8110106141416543213500110000310123196000
	When the input to submitted to the parser
	Then the entity should be 8110
	    And the AI should be 8110
		And the value should be 106141416543213500110000310123196000
		And the data value should be -
		And the description should be Coupon code identification for use in North America
		And the length of the value should be variable
		And there should be no errors

@N4+N4
Scenario: Parse POINTS
	Given the input is 81110310
	When the input to submitted to the parser
	Then the entity should be 8111
	    And the AI should be 8111
		And the value should be 0310
		And the data value should be POINTS
		And the description should be Loyalty points of a coupon
		And the length of the value should be fixed
		And there should be no errors

@N4+X..70
Scenario: Parse COUPON 2
	Given the input is 8112106141416543213500110000310123196000
	When the input to submitted to the parser
	Then the entity should be 8112
	    And the AI should be 8112
		And the value should be 106141416543213500110000310123196000
		And the data value should be -
		And the description should be Positive offer file coupon code identification for use in North America
		And the length of the value should be variable
		And there should be no errors

@N4+X..70
Scenario: Parse PRODUCT URL
	Given the input is 8200https://acme.com/
	When the input to submitted to the parser
	Then the entity should be 8200
	    And the AI should be 8200
		And the value should be https://acme.com/
		And the data value should be PRODUCT URL
		And the description should be Extended packaging URL
		And the length of the value should be variable
		And there should be no errors

@N2+X..30
Scenario: Parse INTERNAL 90
	Given the input is 90Some+information+1234
	When the input to submitted to the parser
	Then the entity should be 90
	    And the AI should be 90
		And the value should be Some+information+1234
		And the data value should be INTERNAL
		And the description should be Information mutually agreed between trading partners
		And the length of the value should be variable
		And there should be no errors

@N2+X..90
Scenario: Parse INTERNAL 91
	Given the input is 91The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
	When the input to submitted to the parser
	Then the entity should be 91
	    And the AI should be 91
		And the value should be The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
		And the data value should be INTERNAL
		And the description should be Company internal information
		And the length of the value should be variable
		And there should be no errors

@N2+X..90
Scenario: Parse INTERNAL 92
	Given the input is 92The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
	When the input to submitted to the parser
	Then the entity should be 92
	    And the AI should be 92
		And the value should be The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
		And the data value should be INTERNAL
		And the description should be Company internal information
		And the length of the value should be variable
		And there should be no errors

@N2+X..90
Scenario: Parse INTERNAL 93
	Given the input is 93The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
	When the input to submitted to the parser
	Then the entity should be 93
	    And the AI should be 93
		And the value should be The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
		And the data value should be INTERNAL
		And the description should be Company internal information
		And the length of the value should be variable
		And there should be no errors

@N2+X..90
Scenario: Parse INTERNAL 94
	Given the input is 94The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
	When the input to submitted to the parser
	Then the entity should be 94
	    And the AI should be 94
		And the value should be The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
		And the data value should be INTERNAL
		And the description should be Company internal information
		And the length of the value should be variable
		And there should be no errors

@N2+X..90
Scenario: Parse INTERNAL 95
	Given the input is 95The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
	When the input to submitted to the parser
	Then the entity should be 95
	    And the AI should be 95
		And the value should be The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
		And the data value should be INTERNAL
		And the description should be Company internal information
		And the length of the value should be variable
		And there should be no errors

@N2+X..90
Scenario: Parse INTERNAL 96
	Given the input is 96The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
	When the input to submitted to the parser
	Then the entity should be 96
	    And the AI should be 96
		And the value should be The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
		And the data value should be INTERNAL
		And the description should be Company internal information
		And the length of the value should be variable
		And there should be no errors

@N2+X..90
Scenario: Parse INTERNAL 97
	Given the input is 97The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
	When the input to submitted to the parser
	Then the entity should be 97
	    And the AI should be 97
		And the value should be The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
		And the data value should be INTERNAL
		And the description should be Company internal information
		And the length of the value should be variable
		And there should be no errors

@N2+X..90
Scenario: Parse INTERNAL 98
	Given the input is 98The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
	When the input to submitted to the parser
	Then the entity should be 98
	    And the AI should be 98
		And the value should be The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
		And the data value should be INTERNAL
		And the description should be Company internal information
		And the length of the value should be variable
		And there should be no errors

@N2+X..90
Scenario: Parse INTERNAL 99
	Given the input is 99The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
	When the input to submitted to the parser
	Then the entity should be 99
	    And the AI should be 99
		And the value should be The+quick+brown+fox+jumped+over+the+lazy+dog's+back+01234567890
		And the data value should be INTERNAL
		And the description should be Company internal information
		And the length of the value should be variable
		And there should be no errors
