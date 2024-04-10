Feature: Functionality_03_s03

As an employee I want to be able to archive books

Background: 
	Given the user is logged in as an employee
	And the user is on the "Archive book" screen

@tag1
Scenario: Book not selected
	When the employee doesn't choose a book from the list
	And the employee clicks the Archive button
	Then the employee should remain on the same screen
	And the employee should see a warning message that a book has to be selected

Scenario: Book search works
	When the employee enters an existing book name into the book search field
	Then the empoloyee sees the desired book in the list

Scenario: Back button from "Archive book" leads to "Action choice" screen
	When the employee clicks on the "Back" button
	Then the employee should be redirected to the "Action choice" screen

Scenario: Book succesfully archived
	When the employee chooses a book from the list
	And the employee clicks on the Archive button
	Then the employee should remain on the same screen
	And the employee should see a message that the book is succesfully archived
	And the employee should see a refreshed list of books

