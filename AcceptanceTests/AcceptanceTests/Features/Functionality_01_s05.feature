Feature: F01_S05 - Admin employee view

As an administrator, I want to be able to see all employees for a chosen library in the system
and also know if there are no employees for the chosen library

Background:
	Given the user is logged in as an administrator
	And the user is on the All employees screen

Scenario: F01_S05_C01 - No employees
	When the administrator chooses a library with no employees
	Then the user should be notified that there are no employees for the chosen library

Scenario: F01_S05_C02 - Showing employees
	When the administrator chooses a library with atleast one employee
	Then the user should be shown the employees for that library
	