﻿Feature: Editing notifications

As an employee
I wnat to be able to write notifications for my library
So that members can read them

Background: 
	Given the user is logged into app with epmloyee credentials
	And is on the notifications control

@tag1
Scenario: Notification is not selected
	When employee clicks edit button
	Then the error message should appear "Odaberite obavijest!"

Scenario: Editing notification
	When the employee selects member with title "kjsrhflsdukrhk"
	And employee clicks edit button
	And enters new title "Novi naslov"
	And clicks Save button
	Then the employee should see notifications window
	And the last row in table should have notification with title "Novi naslov"

Scenario: Giving up on editing notification
	When the employee selects member with title "Novi naslov"
	And employee clicks edit button
	And enters new title "Drugi naslov"
	And clicks Cancel button
	Then the employee should see notifications window
	And the table should not contain edited notification
