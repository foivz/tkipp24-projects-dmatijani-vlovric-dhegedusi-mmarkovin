Feature: Borrowing a book

As an administrator, I want to be able to borrow a pending book by a member

Scenario: Borrowing a book by choosing from the list
	Given a member made a new borrow
	And an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	And the user clicks the Pending borrows tab
	And the user clicks on a Pending borrow
	And the user clicks the Borrow a new book button
	And the user enters the correct borrow duration
	And the user clicks the Borrow this book button
	Then the borrow should change it's status to borrowed

Scenario: Book barcode not entered
	Given an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	And the user clicks the Borrow a new book button
	And the user enters the correct member barcode
	And the user enters the correct borrow duration
	And the user doesn't enter the book barcode
	And the user clicks the Borrow this book button
	Then the system should show an error message that the borrow can't be made

Scenario: Member barcode not entered
	Given an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	And the user clicks the Borrow a new book button
	And the user enters the correct book barcode
	And the user enters the correct borrow duration
	And the user doesn't enter the member barcode
	And the user clicks the Borrow this book button
	Then the system should show an error message that the borrow can't be made

Scenario: Borrow duration not entered
	Given an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	And the user clicks the Borrow a new book button
	And the user enters the correct member barcode
	And the user enters the correct book barcode
	And the user doesn't enter the borrow duration
	And the user clicks the Borrow this book button
	Then the system should show an error message that the borrow can't be made

Scenario: Non existent book barcode entered
	Given an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	And the user clicks the Borrow a new book button
	And the user enters the correct member barcode
	And the user enters the correct borrow duration
	And the user enters the book barcode <bookbarcode>
	And the user clicks the Borrow this book button
	Then the system should show an error message that the borrow can't be made

	Examples:
		| bookbarcode |
		| 11111599999 |
		| 99511       |
		| abcd        |

Scenario: Non existent member barcode entered
	Given an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	And the user clicks the Borrow a new book button
	And the user enters the correct book barcode
	And the user enters the correct borrow duration
	And the user enters the member barcode <memberbarcode>
	And the user clicks the Borrow this book button
	Then the system should show an error message that the borrow can't be made

	Examples:
		| memberbarcode |
		| 11111599999   |
		| 99511         |
		| abcd          |

Scenario: Already existing borrow
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
		| 65625036    | 1238439564    |

Scenario: Borrowing a book by entering the barcodes
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
		| 18935995    | 1238439564    |
