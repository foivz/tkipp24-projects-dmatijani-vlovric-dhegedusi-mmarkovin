﻿Feature: New book copies

As an employee I want to be able to enter new book copies

Background: 
	Given the user is logged in as an employee
	And the user is on the Add new book copies screen

@tag1
Scenario: F03_S02_C01 Empty number of copies field
	When the employee chooses a book from the list
	And the employee leaves the number of copies field empty
	And the employee clicks the Insert button
	Then the employee should remain on the Add new book copies screen
	And the employee should see a warning message that the number of new copies field cannot be empty

Scenario: F03_S02_C02 Non numerical entry for number of copies field
	When the employee chooses a book from the list
	And the employee enters a non numerical or negative value into the number of copies field
	And the employee clicks the Insert button
	Then the employee should remain on the Add new book copies screen
	And the employee should see a warning message that the number of copies field has to contain a valid numerical value

Scenario: F03_S02_C03 Book not chosen
	When the employee does not choose a book from the list
	And the employee enters a valid number of copies
	And the employee clicks the Insert button
	Then the employee should remain on the Add new book copies screen
	And the employee should see a warning message that a book must be chosen

Scenario: F03_S02_C04 Book copies succesfully added
	When the employee chooses a book from the list
	And the employee enters a valid number of copies
	And the employee clicks the Insert button
	Then the employee should remain on the Add new book copies screen
	And the employee should see a message that the copies have been succesfully added
	And the employee should see a refreshed list

Scenario: F03_S02_C05 Back button from Add new book copies leads to Action choice screen
	When the employee clicks on the Back button
	Then the employee should be redirected to the Action choice screen

Scenario: F03_S02_C06 Book search works
	When the employee enters an existing book name into the book search field
	Then the empoloyee sees the desired book in the list