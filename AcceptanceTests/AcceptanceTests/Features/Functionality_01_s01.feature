Feature: F01_S01 - Admin library view

As an administrator, I want to be able to see all the libraries in the system
and also know if there are none in the system

Background:
	Given the user is logged in as an administrator
	And the user is on the All libraries screen

Scenario: F01_S01_C01 - No libraries
	When there are no libraries in the database
	Then the user should know that the system was loading the libraries
	And the user should be notified that there are no libraries

Scenario: F01_S01_C02 - Showing libraries
	When there is at least one library in the database
	Then the user should know that the system was loading the libraries
	And the user should see all libraries
	