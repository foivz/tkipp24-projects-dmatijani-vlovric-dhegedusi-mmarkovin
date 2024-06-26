﻿Feature: F05_S01 - Returning a borrowed book

As an administrator, I want to be able to return a borrowed book

Background: 
	Given an employee from a library which has borrows is logged in
	When the user clicks the Borrows button

Scenario: F05_S01_C01 - Returning a book by choosing from the list
	When the user clicks the Current borrows tab
	And the user clicks the borrow for the book <book>
	And the user clicks the Return book button
	And the user clicks the Check borrow button
	And the user clicks the Return this book button
	Then the user should see the book <returnedbook> in the Returned borrows tab

	Examples:
		| book              | returnedbook        |
		| 18935995 - Hamlet | 18935995 - Hamlet   |

Scenario: F05_S01_C03 - Book barcode not entered
	When the user clicks the Return book button
	And the user enters the member barcode <memberbarcode>
	And the user doesn't enter the book barcode
	And the user clicks the Check borrow button
	Then the system should show an error that the book can't be returned

	Examples:
		| memberbarcode |
		| z48rSx4m      |

Scenario: F05_S01_C04 - Member barcode not entered
	When the user clicks the Return book button
	And the user enters the book barcode <bookbarcode>
	And the user doesn't enter the member barcode
	And the user clicks the Check borrow button
	Then the system should show an error that the book can't be returned

	Examples:
		| bookbarcode |
		| 65625036    |

Scenario: F05_S01_C05 - Non existent book barcode entered
	When the user clicks the Return book button
	And the user enters the book barcode <bookbarcode>
	And the user enters the member barcode <memberbarcode>
	And the user clicks the Check borrow button
	Then the system should show an error that the book can't be returned

	Examples:
		| bookbarcode | memberbarcode |
		| 11111599999 | z48rSx4m      |
		| 99511       | z48rSx4m      |
		| abcd        | z48rSx4m      |

Scenario: F05_S01_C06 - Non existent member barcode entered
	When the user clicks the Return book button
	And the user enters the book barcode <bookbarcode>
	And the user enters the member barcode <memberbarcode>
	And the user clicks the Check borrow button
	Then the system should show an error that the book can't be returned

	Examples:
		| memberbarcode | bookbarcode |
		| 11111599999   | 65625036    |
		| 99511         | 65625036    |
		| abcd          | 65625036    |

Scenario: F05_S01_C07 - Currently non-existent borrow
	When the user clicks the Return book button
	And the user enters the book barcode <bookbarcode>
	And the user enters the member barcode <memberbarcode>
	And the user clicks the Check borrow button
	Then the system should show a message that there isn't a current borrow

	Examples:
		| bookbarcode | memberbarcode |
		| 21932274    | uDRcqmgi      |

Scenario: F05_S01_C08 - Trying to return a previous borrow
	When the user clicks the Return book button
	And the user enters the book barcode <bookbarcode>
	And the user enters the member barcode <memberbarcode>
	And the user clicks the Check borrow button
	Then the system should show a message that there isn't a current borrow

	Examples:
		| bookbarcode | memberbarcode |
		| 21932274    | z48rSx4m      |

Scenario: F05_S01_C09 - Returning a book by entering the barcodes
	When the user clicks the Return book button
	And the user enters the book barcode <bookbarcode>
	And the user enters the member barcode <memberbarcode>
	And the user clicks the Check borrow button
	And the user clicks the Return this book button
	Then the user should see the book <returnedbook> in the Returned borrows tab

	Examples:
		| bookbarcode | memberbarcode | returnedbook                   |
		| 65625036    | z48rSx4m      | 65625036 - Prolaz kroz Vrijeme |