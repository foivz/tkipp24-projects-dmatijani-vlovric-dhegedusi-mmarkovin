Feature: Functionality_02

A short summary of the feature

@tag1
Scenario: Invalid username
	Given I am on the login form
	And the user is not logged into the system
	When the user enters invalid username "noUsername"
	And the user enters enter valid password "cindricka123"
	And the user clicks the login button
	Then the application remains on the login form
	But an error message appears with the message "Unijeli ste krive korisničke podatke!"

Scenario: Invalid password
	Given I am on the login form
	And the user is not logged into the system
	When the user enters valid username "pcindric89"
	And the user enters enter invalid password "noPass"
	And the user clicks the login button
	Then the application remains on the login form
	But an error message appears with the message "Unijeli ste krive korisničke podatke!"

Scenario: Invalid user credentials
	Given I am on the login form
	And the user is not logged into the system
	When the user enters invalid username "noUsername"
	And the user enters enter invalid password "noPass"
	And the user clicks the login button
	Then the application remains on the login form
	But an error message appears with the message "Unijeli ste krive korisničke podatke!"

Scenario: Empty user credentials
	Given I am on the login form
	And the user is not logged into the system
	When the user clicks the login button
	Then the application remains on the login form
	But an error message appears with the message "Unijeli ste krive korisničke podatke!"

Scenario: Memership expired
	Given I am on the login form
	And the user is not logged into the system
	When the user enters username "megi"
	And the user enters enter password "megi123"
	And the user clicks the login button
	Then the application remains on the login form
	But an error message appears with the message "Članarina je istekla! Članarinu možete produljiti u svojoj knjižnici."

Scenario: Valid user credentials
	Given I am on the login form
	And the user is not logged into the system
	When the user enters username "pcindric89"
	And the user enters enter password "cindricka123"
	And the user clicks the login button
	Then the user should see specific window depending on its role
