Feature: Membership extension

As an employee
I wnat to be able to extend membership to memebers
So that they can can borrow and reserve books again

Background: 
	Given Given the user is logged into app as employee hmihovic
	And is on the Member Management panel
@tag1
Scenario: Member not selected
	When employee clicks extend button
	Then the error should appear

Scenario: Membership did not expire
	When the employee selects member with username "sebastijank"
	And  employee clicks extend button
	Then the error should appear

Scenario: Membership extendes successfully
	When the employee selects member with username "test03"
	And  employee clicks extend button
	Then the members menagment window should appear
	And the membership date of that member should be today's date "16/4/2024"


