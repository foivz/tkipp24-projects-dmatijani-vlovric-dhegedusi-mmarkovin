Feature: Functionality_08_s02

As a member I want to be able to see details about books

Background:
	Given the user is logged in as a member
	And the user is on the "Search library catalogue" screen

@tag1
Scenario: Book details with all information
	When the member chooses a book that has all its information entered
	And the member clicks on the Book details button
	Then the member should be redirected to the Book details screen
	And the member should see all the book information

Scenario: Book details with only required information
	When the member chooses a book that has only its required information entered
	And the member clicks on the Book details button
	Then the member should be redirected to the Book details screen
	And the member should see all the required book information
	And the member should see the non entered information as blank or with a placeholder text

Scenario: Back button
	When the member chooses a book
	And the member clicks on the Book details button
	And the member clicks the Back button
	Then the member should be redirected back to the Search library catalogue screen
