Feature: Deleting an employee

As an administrator, I want to be able to delete an employee if needed
and also be warned before deleting

Background:
	Given the user is logged in as an administrator
	And the user is on the All employees screen
	And the user chooses a library with employees

Scenario: No employees chosen
	When the user clicks the Delete employee button
	Then the system should show an error message that it cannot delete the employee selection

Scenario: One employee chosen
	When the user chooses one employee to delete
	And the user clicks the Delete employee button
	Then the system should warn the user before deleting an employee
	And the system should delete the selected employee

Scenario: Multiple employees chosen
	When the user chooses multiple employees to delete
	And the user clicks the Delete employee button
	Then the system should show an error message that it cannot delete the employee selection
