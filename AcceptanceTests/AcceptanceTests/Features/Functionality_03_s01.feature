Feature: Book catalogue management

As an employee I want to be able to enter new books, genres, authors, add new copies of a book, archive books and view the archive.

Background: 
	Given the user is logged in as an employee
	And the user is on the "New book entry" screen

@tag1
Scenario: Empty book name field 
	When the employee leaves the book name field empty
	And the employee clicks on the Insert button
	Then the employee should remain on the same screen
	And the employee should see a warning message that the book name cannot be empty

Scenario: Wrong publish date format
	When the employee enters all required fields and options
	And the employee enters a date different than dd-MM-yyyy
	And the employee clicks the Insert button
	Then the employee should remain on the same screen
	And the employee should see a warning message that the date has to be in the dd-MM-yyyy format

Scenario: Non numerical entry for number of pages
	When the employee enters all required fields and options before the number of pages
	And the employee enters a non numerical or negative value into the number of pages field
	And the employee clicks on the Insert button
	Then the employee should remain on the same screen
	And the employee should see a warning message that the number of pages can only be a valid numerical value

Scenario: Radio button empty
	When the employee enters all required fields and options before the radio button
	And the employee leaves the radio button selection empty
	And the employee clicks on the Insert button
	Then the employee should remain on the same screen
	And the employee should see a warning message that one of the two radio button options has to be chosen

Scenario: Empty number of copies field
	When the employee enters all required fields and options before the number of copies
	And the employee leaves the number of copies field empty
	And the employee clicks on the Insert button
	Then the employee should remain on the same screen
	And the employee should see a warning message that the number of copies field cannot be empty

Scenario: Non numerical entry for number of copies
	When the employee enters all required fields and options
	And the employee enters a non numerical or negative value into the number of pages field
	And the employee clicks on the Insert button
	Then the employee should remain on the same screen
	And the employee should see a warning message that the number of copies can only be a valid numerical value

Scenario: Empty genre
	When the employee enters all required fields and options before the genre selection
	And the employee leaves the genre selection empty
	And the employee clicks on the Insert button
	Then the employee should remain on the same screen
	And the employee should see a warning message that the genre selection cannot be empty

Scenario: Empty author
	When the employee enters all required fields and options before the author selection
	And the employee leaves the author selection empty
	And the employee clicks on the Insert button
	Then the employee should remain on the same screen
	And the employee should see a warning message that the author selection cannot be empty

Scenario: Empty genre name field
	Given the employee is on the "New genre" screen
	When the employee leaves the genre name field empty
	And the employee clicks on the Insert button
	Then the employee should remain on the same screen
	And the employee should see a warning message that the genre name field cannot be empty

Scenario: Empty author name field
	Given the employee is on the "New author" screen
	When the employee leaves the author name field empty
	And the employee clicks on the Insert button
	Then the employee should remain on the same screen
	And the employee should see a warning message that the author name field cannot be empty

Scenario: Empty author last name field
	Given the employee is on the "New author" screen
	When the employee enters a name into the author name field
	And the employee leaves the author last name field empty
	And the employee clicks on the Insert button
	Then the employee should remain on the same screen
	And the employee should see a warning message that the author last name field cannot be empty

Scenario: Wrong birth date format
	Given the employee is on the "New author" screen
	When the employee enters all required fields before the birth date
	And the employee enters a date different than dd-MM-yyyy
	And the employee clicks the Insert button
	Then the employee should remain on the same screen
	And the employee should see a warning message that the date has to be in the dd-MM-yyyy format

Scenario: Succesful book insertion with only required information entered
	When the employee enters valid inputs, chooses options and selects dropdowns for name, radio button, number of copies, genre and author
	And the employee clicks on the Insert button
	Then the employee should remain on the same screen
	And the employee should see a message that the insertion was succesful

Scenario: Succesful book insertion with all information entered
	When the employee enters and chooses all valid information
	And the employee clicks on the Insert button
	Then the employee should remain on the same screen
	And the employee should see a message that the insertion was succesful 

Scenario: Succesful new genre insertion
	Given the employee is on the "New genre" screen
	When the employee enters the genre name field
	And the employee clicks on the Insert button
	Then the employee should remain on the same screen
	And the employee should see a message that the insertion was succesful 
	And the employee should be redirected to the "New book entry" screen

Scenario: Succesful new author insertion with date of birth empty
	Given the employee is on the "New author" screen
	When the employee enters the required fields
	And the employee clicks on the Insert button
	Then the employee should remain on the same screen
	And the employee should see a message that the insertion was succesful 
	And the employee should be redirected to the "New book entry" screen

Scenario: Succesful new author insertion with all information
	Given the employee is on the "New author" screen
	When the employee enters all information
	And the employee clicks on the Insert button
	Then the employee should remain on the same screen
	And the employee should see a message that the insertion was succesful 
	And the employee should be redirected to the "New book entry" screen

Scenario: Back button from "New book entry" leads to "Action choice" screen
	When the employee clicks on the "Back" button
	Then the employee should be redirected to the "Action choice" screen

Scenario: Back button from "New genre" leads to "New book entry" screen
	Given the employee is on the "New genre" screen
	When the employee clicks on the "Back" button
	Then the employee should be redirected to the "New book entry" screen
	And the employee should see all his entered inputs there

Scenario: Back button from "New author" leads to "New book entry" screen
	Given the employee is on the "New author" screen
	When the employee clicks on the "Back" button
	Then the employee should be redirected to the "New book entry" screen
	And the employee should see all his entered inputs there