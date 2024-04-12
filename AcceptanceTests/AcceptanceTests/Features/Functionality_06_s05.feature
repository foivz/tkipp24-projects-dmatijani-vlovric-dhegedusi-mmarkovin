Feature: Membership extension

As an employee
I wnat to be able to extend membership to memebers
So that they can can borrow and reserve books again

Background: 
	Given Given the user is logged into app as employee
	And is on the Member Management panel
@tag1
Scenario: Member not selected
	When employee clicks extend button
	Then the error message should appear "Odaberite člana!"

Scenario: Membership did not expire
	When the employee selects member with username "sebastijank"
	And  employee clicks extend button
	Then the error message should appear "Članstvo još nije isteklo!"

Scenario: Membership extendes successfully
	When the employee selects member with username "ppintaric50"
	And  employee clicks extend button
	Then the membership date of that member should be today's date


