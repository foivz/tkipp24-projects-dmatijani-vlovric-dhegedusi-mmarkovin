Feature: Reading notifications

As a member
I wnat to be able to read notifications of my library
So that i can be informed about its activities

Background: 
	Given Given the user is logged into app as member
@tag1
Scenario: Reading all notifications
	Given the member is on members panel
	When member clicks Notifocation buttons
	Then the notification control should appear

Scenario: Reading read notifications
	Given the member is on notifications control
	When member clicks Read button
	Then all read notifications should appear

Scenario: Reading unread notifications
	Given the member is on notifications control
	When member clicks Unread button
	Then all unread notifications should appear

Scenario: Notification is not selacted
	Given the member is on notifications control
	When member clicks Details button
	Then an error message should appear wit message "Odaberite obavijest!"

Scenario: Reading notification details
	Given the member is on notifications control
	When member clicks Details button
	Then selected notification detail control should apear
