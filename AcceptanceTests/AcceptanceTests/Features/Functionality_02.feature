Feature: Functionality_02

A short summary of the feature

@tag1
Scenario: Invalid username
	Given I am on the login form
	And the user is not logged into the system
	When the user enters invalid username "noUsername"
	And the user enters enter valid password "cindricka123"
	And the user clicks the login button "Prijava"
	Then the aplication remains on the login form
	But an error message appears with the message "You have entered incorrect credentials!"
	When [action]
	Then [outcome]
