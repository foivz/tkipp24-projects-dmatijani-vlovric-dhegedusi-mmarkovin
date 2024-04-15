Feature: Writing notifications

As an employee
I wnat to be able to write notifications for my library
So that members can read them

Background: 
	Given Given the user is logged into app as employee
	And is on the notifications panel

@tag1
Scenario Outline: Writing notifiation
	When clicks buton New notification
	And the New notification screen appears
	And the employee enters title <title> and description <description>
	And clisks save button
	Then tables last row should contain notifiv+cation with <title> and <description>
	 
	Examples: 
		| title | description |
		| Neka nova obavijest | Opis obavijesti |
		|  |  |

Scenario: Giving up on editing
	When clicks buton New notification
	And the New notification screen appears
	And the employee enters title <title> and description <description>
	And clisks cancel button
	Then Then tables should not contain the message
	 
	Examples: 
		| title | description |
		| Neka nova obavijest  | Opis obavijesti |


