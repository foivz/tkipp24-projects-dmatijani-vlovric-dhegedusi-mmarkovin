Feature: F01_S07 - Editing employee info

As an administrator, I want to be able to edit employee information if needed
and also know whether I can or cannot enter in specific information

Background: 
	Given the user is logged in as an administrator
	And the user is on the All employees screen
	And the user chooses a library with employees
	And the user chooses an employee from the All employees list
	And the user clicks on the Edit employee button

Scenario: F01_S07_C03 - Employee name removed
	When the user removes the employee name
	And the user clicks the Save changes for the employee button
	Then the system should show the employee without the name

Scenario: F01_S07_C04 - Employee surname removed
	When the user removes the employee surname
	And the user clicks the Save changes for the employee button
	Then the system should show the employee without the surname

Scenario: F01_S07_C05 - Employee username removed
	When the user removes the employee username
	And the user clicks the Save changes for the employee button
	Then the system should show an error message that the employee can't be modified

Scenario: F01_S07_C06 - Already existing employee username entered
	When the user removes the employee username
	And the employee username <username> is entered
	And the user clicks the Save changes for the employee button
	Then the system should show an error message that the employee can't be modified

	Examples:
		| username   |
		| hmihovic   |
		| mmarkic    |
		| pcindric89 |

Scenario: F01_S07_C07 - Employee password removed
	When the user removes the employee password
	And the user clicks the Save changes for the employee button
	Then the system should show an error message that the employee can't be modified

Scenario: F01_S07_C08 - Successful employee edit
	When the user changes the employee's name
	And the user enters the employee surname
	And the user clicks the Save changes for the employee button
	Then the employee changes should be visible in the All employees list
