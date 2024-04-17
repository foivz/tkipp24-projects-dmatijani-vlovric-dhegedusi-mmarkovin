Feature: F01_S06 - Adding a new employee

As an administrator, I want to be able to add a new employee into the system for the chosen library
and also know whether I can or cannot enter in an employee with different attributes

Background: 
	Given the user is logged in as an administrator
	And the user is on the New employee screen

Scenario: F01_S06_C01 - Library not chosen
	When the library for employee is not chosen
	And the user enters a correct employee OIB
	And the user enters an employee name
	And the user enters a correct employee username
	And the user enters a correct employee password
	And the user clicks the Save new employee button
	Then the system should show an error message that the employee can't be added

Scenario: F01_S06_C02 - Employee OIB not entered
	When the employee OIB is not entered
	And the library for employee is chosen
	And the user enters an employee name
	And the user enters a correct employee username
	And the user enters a correct employee password
	And the user clicks the Save new employee button
	Then the system should show an error message that the employee can't be added

Scenario: F01_S06_C03 - Already existing employee OIB entered
	When the employee OIB <oib> is entered
	And the library for employee is chosen
	And the user enters an employee name
	And the user enters a correct employee username
	And the user enters a correct employee password
	And the user clicks the Save new employee button
	Then the system should show an error message that the employee can't be added

	Examples:
		| oib         |
		| 38948392341 |
		| 12312312334 |
		| 12345678901 |
		| 66885599443 |
	
Scenario: F01_S06_C04 - Employee username not entered
	When the employee username is not entered
	And the library for employee is chosen
	And the user enters an employee name
	And the user enters a correct employee OIB
	And the user enters a correct employee password
	And the user clicks the Save new employee button
	Then the system should show an error message that the employee can't be added

Scenario: F01_S06_C05 - Already existing employee username entered
	When the employee username <username> is entered
	And the library for employee is chosen
	And the user enters an employee name
	And the user enters a correct employee OIB
	And the user enters a correct employee password
	And the user clicks the Save new employee button
	Then the system should show an error message that the employee can't be added

	Examples:
		| username   |
		| hmihovic   |
		| mjakic2    |
		| mmarkic    |
		| pcindric89 |

Scenario: F01_S06_C06 - Password not entered
	When the user doesn't enter the employee password
	And the user enters a correct employee username
	And the library for employee is chosen
	And the user enters an employee name
	And the user enters a correct employee OIB
	And the user clicks the Save new employee button
	Then the employee should be visible in the All employees list for the chosen library

Scenario: F01_S06_C07 - Successful new employee
	When the user enters a correct employee username
	And the library for employee is chosen
	And the user enters an employee name
	And the user enters a correct employee OIB
	And the user enters a correct employee password
	And the user clicks the Save new employee button
	Then the employee should be visible in the All employees list for the chosen library