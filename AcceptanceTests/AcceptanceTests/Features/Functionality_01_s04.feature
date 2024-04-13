Feature: F01_S04 - Deleting a library

As an administrator, I want to be able to delete a library if needed
and also be warned before deleting

Background:
	Given the user is logged in as an administrator
	And the user is on the All libraries screen

Scenario: F01_S04_C01 - No libraries chosen
	When the user clicks the Delete library button
	Then the system should show an error message that it cannot delete the library selection

Scenario: F01_S04_C02 - One library chosen
	When the user chooses one library to delete
	And the user clicks the Delete library button
	Then the system should warn the user before deleting a library
	And the system should delete the selected library

Scenario: F01_S04_C03 - Multiple libraries chosen
	When the user chooses multiple libraries to delete
	And the user clicks the Delete library button
	Then the system should show an error message that it cannot delete the library selection
