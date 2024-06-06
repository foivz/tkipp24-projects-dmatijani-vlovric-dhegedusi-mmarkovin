Feature: F01_S03 - Editing library info

As an administrator, I want to be able to edit library information if needed
and also know whether I can or cannot enter in specific information

Background: 
	Given the user is logged in as an administrator
	And the user is on the All libraries screen
	And the user chooses a library from the list
	And the user clicks on the Edit library button

Scenario: F01_S03_C03 - Library name removed
	When the user removes the library name
	And the user clicks the Save changes for the library button
	Then The system should show an error message that the library can't be modified

Scenario: F01_S03_C04 - Library OIB removed
	When the user removes the library OIB
	And the user clicks the Save changes for the library button
	Then The system should show an error message that the library can't be modified

Scenario: F01_S03_C05 - Already existing library OIB entered
	When the user removes the library OIB
	And the library OIB <oib> is entered
	And the user clicks the Save changes for the library button
	Then The system should show an error message that the library can't be modified

	Examples:
		| oib         |
		| 19283758473 |
		| 96060105940 |
		| 96857345682 |
		| 1234443443  |

Scenario: F01_S03_C06 - Price per day late removed
	When the user removes the price per day late
	And the user clicks the Save changes for the library button
	Then The system should show an error message that the library can't be modified

Scenario: F01_S03_C07 - Membership duration removed
	When the user removes the membership duration
	And the user clicks the Save changes for the library button
	Then The system should show an error message that the library can't be modified

Scenario: F01_S03_C08 - No changes in the library
	When the user doesn't change anything about the library
	And the user clicks the Save changes for the library button
	Then the system should not change anything about the library

Scenario: F01_S03_C09 - Successful library edit
	When the user changes the library's name
	And the user enters the library address
	And the user clicks the Save changes for the library button
	Then the library changes should be visible in the All libraries list