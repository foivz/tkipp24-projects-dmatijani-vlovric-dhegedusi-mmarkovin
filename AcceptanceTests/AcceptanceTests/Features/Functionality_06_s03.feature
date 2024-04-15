Feature: Editing members

As an employee
I wnat to be able to register existing memebers
So that they can borrow and reserve books

Background: 
	Given Given the user is logged into app as employee
	And is on the Member Management panel

@tag1
Scenario: Editing existing member
	When employee selects member with username "anabol" and clicks edit button
	And the employee edits name "AAAaaa" and surename "BBBbb"
	And clicks Save button on screeen
	Then the employee should see member managment panel
	And the table with members should contain edited member "AAAaaa" "BBBbb"

Scenario: Editing non exsisting member
	When employee clicks edit button on screeen
	Then the error message should appear "Odaberite člana!"

Scenario: Giving up on editing
	When employee selects member with username "anabol" and clicks edit button
	And the employee edits name "Veselko" and surename "Miličević"
	And clicks Cancle button on screeen
	Then the employee should see member managment panel
	And the table without that member "Veselko" "Miličević"