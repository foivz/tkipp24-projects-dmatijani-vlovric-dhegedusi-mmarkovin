Feature: F04_S02 - Borrows sorted by status

As an employee, I want to be able to view my library's borrows sorted into different status categories

Scenario: F04_S02_C01 - Borrows are sorted by status
	Given an employee from a library which has borrows is logged in
	When the user clicks the Borrows button
	Then the user should see the borrows sorted into different categories
