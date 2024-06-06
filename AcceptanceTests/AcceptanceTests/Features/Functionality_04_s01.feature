Feature: F04_S01 - Borrowed books visible

As an employee, I want to be able to see borrowed books for my library
and know if there are none

Scenario: F04_S01_C01 - Borrows exist
	Given an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	Then the user should know that the system was loading the borrows
	And the user should see all borrowed books for his library

Scenario: F04_S01_C02 - Borrows don't exist
	Given an employee from a library which has NO borrows is logged in
	When the user clicks the Borrows button
	Then the user should know that the system was loading the borrows
	And the user should be notified that there are no borrows
