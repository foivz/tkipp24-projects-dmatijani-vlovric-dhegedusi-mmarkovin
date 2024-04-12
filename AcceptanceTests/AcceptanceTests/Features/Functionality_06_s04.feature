Feature: Deleting members

As an employee
I wnat to be able to delete existing memebers
So that they can't be the part of library any more
Background: 
	Given Given the user is logged into app as employee
	And is on the Member Management panel

@tag1
Scenario: Deleting non existing member
	When employee clicks delete button
	Then the error message should appear "Odaberite člana!"

Scenario: Member did not return the book
	When employee selects employee selects member with username
	And employee clicks delete button
	And employee clicks ok button on alert window
	Then the employee should see members menagment window
	And the table should still show that member

Scenario: 
	When employee selects member with username
	And employee clicks delete button
	And employee clicks ok button on alert window
	Then the employee should see members menagment window
	And the table without that member
