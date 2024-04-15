Feature: F04_S03 - Borrowing a book

As an administrator, I want to be able to borrow a pending book by a member

Scenario: F04_S03_C01 - Borrowing a book by choosing from the list
	Given a member made a new borrow
	And an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	And the user clicks the Pending borrows tab
	And the user clicks on a Pending borrow
	And the user clicks the Borrow a new book button
	And the user enters the correct borrow duration
	And the user clicks the Borrow this book button
	Then the borrow should change it's status to borrowed

Scenario: F04_S03_C02 - Book barcode not entered
	Given an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	And the user clicks the Borrow a new book button
	And the user enters the member barcode <memberbarcode>
	And the user enters the correct borrow duration
	And the user doesn't enter the book barcode
	And the user clicks the Borrow this book button
	Then the system should show an error message that the borrow can't be made

	Examples:
		| memberbarcode |
		| z48rSx4m      |

Scenario: F04_S03_C03 - Member barcode not entered
	Given an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	And the user clicks the Borrow a new book button
	And the user enters the book barcode <bookbarcode>
	And the user enters the correct borrow duration
	And the user doesn't enter the member barcode
	And the user clicks the Borrow this book button
	Then the system should show an error message that the borrow can't be made

	Examples:
		| bookbarcode |
		| 65625036    |

Scenario: F04_S03_C04 - Borrow duration not entered
	Given an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	And the user clicks the Borrow a new book button
	And the user enters the member barcode <memberbarcode>
	And the user enters the book barcode <bookbarcode>
	And the user doesn't enter the borrow duration
	And the user clicks the Borrow this book button
	Then the system should show an error message that the borrow can't be made

	Examples:
		| bookbarcode | memberbarcode |
		| 65625036    | z48rSx4m      |

Scenario: F04_S03_C05 - Non existent book barcode entered
	Given an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	And the user clicks the Borrow a new book button
	And the user enters the member barcode <memberbarcode>
	And the user enters the correct borrow duration
	And the user enters the book barcode <bookbarcode>
	And the user clicks the Borrow this book button
	Then the system should show an error message that the borrow can't be made

	Examples:
		| bookbarcode | memberbarcode |
		| 11111599999 | z48rSx4m      |
		| 99511       | z48rSx4m      |
		| abcd        | z48rSx4m      |

Scenario: F04_S03_C06 - Non existent member barcode entered
	Given an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	And the user clicks the Borrow a new book button
	And the user enters the book barcode <bookbarcode>
	And the user enters the correct borrow duration
	And the user enters the member barcode <memberbarcode>
	And the user clicks the Borrow this book button
	Then the system should show an error message that the borrow can't be made

	Examples:
		| memberbarcode | bookbarcode |
		| 11111599999   | 65625036    |
		| 99511         | 65625036    |
		| abcd          | 65625036    |

Scenario: F04_S03_C07 - Already existing borrow
	Given an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	And the user clicks the Borrow a new book button
	And the user enters the book barcode <bookbarcode>
	And the user enters the member barcode <memberbarcode>
	And the user enters the correct borrow duration
	And the user clicks the Borrow this book button
	Then the system should show an error message that the borrow can't be made

	Examples:
		| bookbarcode | memberbarcode |
		| 18935995    | z48rSx4m      |

Scenario: F04_S03_C08 - Borrowing a book by entering the barcodes
	Given an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	And the user clicks the Borrow a new book button
	And the user enters the book barcode <bookbarcode>
	And the user enters the member barcode <memberbarcode>
	And the user enters the correct borrow duration
	And the user clicks the Borrow this book button
	Then a new book with the status borrowed should appear

	Examples:
		| bookbarcode | memberbarcode |
		| 65625036    | z48rSx4m      |
