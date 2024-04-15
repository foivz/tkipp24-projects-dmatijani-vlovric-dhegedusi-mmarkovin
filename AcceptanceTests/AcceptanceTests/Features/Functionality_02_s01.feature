Feature: User login

As user
I want to be able to login into the app with my credentials 
So that i can do things based on my user role (admin, employee, member)

Background:
	Given the user is on the login form

Scenario Outline: Invalid credentials
	When the user enters username <username> and password <password>
	And the user clicks the Login button
	Then the user shold see <error> message

	Examples: 
	| username   | password     | error                                                                 |
	| megi       | megi123      | Članarina je istekla! Članarinu možete produljiti u svojoj knjižnici. |
	| noUsername | cindricka123 | Unijeli ste krive korisničke podatke!                                 |
	| pcindric89 | noPass       | Unijeli ste krive korisničke podatke!                                 |
	| noUsername | noPass       | Unijeli ste krive korisničke podatke!                                 |
	|            |              | Unijeli ste krive korisničke podatke!                                 |

Scenario Outline: Valid user credentials
	When the user enters username <username> and password <password>
	And the user clicks the Login button
	Then the user should see employee window 

	Examples: 
	| username | password | 
	| pcindric89 | cindricka123 |