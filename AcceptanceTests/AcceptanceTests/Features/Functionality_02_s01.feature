Feature: User login

As user
I want to be able to login into the app with my credentials 
So that i can do things based on my user role (admin, employee, member)

Background:
	Given the user is on the login form
	And the user is not logged into the system
@tag1
Scenario: Invalid username
	When the user enters invalid username "noUsername"
	And the user enters enter valid password "cindricka123"
	And the user clicks the login button
	Then the application remains on the login form
	But an error message appears with the message "Unijeli ste krive korisničke podatke!"

Scenario: Invalid password
	When the user enters valid username "pcindric89"
	And the user enters enter invalid password "noPass"
	And the user clicks the login button
	Then the application remains on the login form
	But an error message appears with the message "Unijeli ste krive korisničke podatke!"

Scenario: Invalid user credentials
	When the user enters invalid username "noUsername"
	And the user enters enter invalid password "noPass"
	And the user clicks the login button
	Then the application remains on the login form
	But an error message appears with the message "Unijeli ste krive korisničke podatke!"

Scenario: Empty user credentials
	When the user clicks the login button
	Then the application remains on the login form
	But an error message appears with the message "Unijeli ste krive korisničke podatke!"

Scenario: Memership expired
	When the user enters username "megi"
	And the user enters enter password "megi123"
	And the user clicks the login button
	Then the application remains on the login form
	But an error message appears with the message "Članarina je istekla! Članarinu možete produljiti u svojoj knjižnici."

Scenario: Valid user credentials
	When the user enters username "pcindric89"
	And the user enters enter password "cindricka123"
	And the user clicks the login button