Feature: Functionality_06_s02

As an employee
I wnat to be able to register new memebers
So that tehy can borrow and reserve books

Background: 
	Given Given the user is logged into app as employee
	And is on the Member Management panel
@tag1
Scenario Outline: Invalid parameters
	When [action]
	Then [outcome]

Scenario: Giving up on registration
