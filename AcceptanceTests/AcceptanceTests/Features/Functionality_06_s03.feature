Feature: Editing members

As an employee
I wnat to be able to register new memebers
So that tehy can borrow and reserve books

Background: 
	Given Given the user is logged into app as employee
	And is on the Member Management panel

@tag1
Scenario: Editing existing member
	When employee selects member and clicks edit button
	And the employee edits name "Petar" and surename "Krešimir"
	And clicks Save button
	Then the employee should see member managment panel
	And the table with members should contain edited member

Scenario: Editing non exsisting member
	When employee selects clicks edit button
	Then the error message should appear "Odaberite člana!"

Scenario: Giving up on editing
	When employee selects member and clicks edit button
	And the employee edits name "Petar" and surename "Krešimir"
	And clicks Cancle button
	Then the employee should see member managment panel
	And the table without that member