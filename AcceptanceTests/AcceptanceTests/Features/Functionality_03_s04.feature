Feature: Archive

As an employee I want to be able to see archived books

Background: 
	Given the user is logged in as an employee
	And the user is on the Archive screen

@tag1
Scenario: Archived books are listed
	Then the employee should see a list of archived books

Scenario: Back button from Archive leads to Action choice screen
	When the employee clicks on the Back button from the archive
	Then the employee should be redirected to the Action choice screen
