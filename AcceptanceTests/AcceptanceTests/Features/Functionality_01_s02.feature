Feature: F01_S02 - Adding a new library

As an administrator, I want to be able to add a new library into the system
and also know whether I can or cannot enter in a library with different attributes

Background:
	Given the user is logged in as an administrator
	And the user is on the New library screen screen
	And the user enters a correct library price per day late
	And the user enters correct membership duration

Scenario: F01_S02_C02 - Library ID not entered
	When the library ID is not entered
	And the user enters a correct library name
	And the user enters a correct library OIB
	And the user clicks the Save new library button
	Then the system should show an error message that the library can't be added

Scenario: F01_S02_C03 - Already existing library ID entered
	When the library ID <id> is entered
	And the user enters a correct library name
	And the user enters a correct library OIB
	And the user clicks the Save new library button
	Then the system should show an error message that the library can't be added

	Examples:
		| id    |
		| 123   |
		| 456   |
		| 1111  |
		| 12343 |

Scenario: F01_S02_C04 - Library OIB not entered
	When the library OIB is not entered
	And the user enters a correct library name
	And the user enters a correct library ID
	And the user clicks the Save new library button
	Then the system should show an error message that the library can't be added

Scenario: F01_S02_C05 - Already existing library OIB entered
	When the library OIB <oib> is entered
	And the user enters a correct library name
	And the user enters a correct library ID
	And the user clicks the Save new library button
	Then the system should show an error message that the library can't be added

	Examples:
		| oib         |
		| 19283758473 |
		| 96060105940 |
		| 96857345682 |
		| 1234443443  |

Scenario: F01_S02_C06 - Library name not entered
	When the library name is not entered
	And the user enters a correct library OIB
	And the user enters a correct library ID
	And the user clicks the Save new library button
	Then the system should show an error message that the library can't be added

Scenario: F01_S02_C07 - Successful new library
	When the user enters a correct library name
	And the user enters a correct library OIB
	And the user enters a correct library ID
	And the user clicks the Save new library button
	Then the library should be visible in the All libraries list