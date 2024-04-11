Feature: Functionality_09_s01

As a member I want to be able to put in reservations for books
Background: 
	Given the user is logged in as a member

@tag1
Scenario: Digital book
	Given the member is on the "Search book catalogue" screen
	When the member chooses a digital book
	And the member clicks on the See details button
	Then the member shouldn't see a Reserve button

Scenario: Reservation
	Given the member is on the Details screen of a non digital book
	And the member has 2 or less reservations
	When the member clicks the Reserve button
	Then the member should see a confirmation message
	When the member confirms the reservation
	Then the member should see his reservation position instead of the Reserve button
	When the member goes to the Reservations screen
	Then the member should see the book in his list

Scenario: Decline reservation
	Given the member is on the Details screen of a non digital book
	And the member has 2 or less reservations
	When the member clicks the Reserve button
	Then the member should see a confirmation message
	When the member  declines the confirmation
	And the member goes to the Reservations screen
	Then the member shouldn't see the book in his list

Scenario: Maximum number of reservations
	Given the member is on the Details screen of a non digital book
	And the member has 3 reservations
	When the member clicks the Reserve button
	Then the member should see a warning message
	And the member goes to the Reservations screen
	Then the member shouldn't see the book in his list

Scenario: Remove reservation
	Given the member is on the Reservations screen
	And the member has a reserved book
	When the member chooses a book from the list
	And the member clicks on the Remove reservation button
	Then the member should see a confirmation message
	And the member shouldn't see the book in his list
	When the member goes to the See details screen of that book
	Then the member should see the Reserve button

Scenario: Notification receival
	Given the member has a reserved book
	And an employee entered a new copy of the book
	When the member logs in
	Then the member should see a notification with his reservation expiry date
	When the member goes to the Reservations page
	Then the member should see a date in the Expiry column of the book

Scenario: Multiple notification receival
	Given the member has multiple reserved books
	And an employee entered a new copy of the books
	When the member logs in
	Then the member should see a notification with his reservation expiry dates
	When the member goes to the Reservations page
	Then the member should see a date in the Expiry column of the books

Scenario: Solo reservation expiry
	Given the member has a reserved book
	And no other member has that book reserved
	And an employee entered a new copy of the book
	When the member logs in
	And the member closes the notification
	And the member logs in after the reservation expiry date
	Then the member shouldn't see that book in his Reservations list
	And the member should see that the available copies of the book on the Details page is increased by 1

Scenario: Reservation expiry
	Given the member has a reserved book
	And another member has that book reserved in the position behind the first member
	And an employee entered a new copy of the book
	When the member logs in
	And the member closes the notification
	And the member logs in after the reservation expiry date
	Then the member shouldn't see that book in his Reservations list
	When the second member logs in
	Then the second member should see a notification with his reservation expiry date
	And the second member should see that the number of available copies of the book on the Details page is 0

Scenario: Solo removal
	Given the member has a reserved book
	And no other member has that book reserved
	And the expiry date column of the book has a date in it
	When the member chooses the book
	And the member clicks the Remove reservation button
	Then the member shouldn't see the book in his list
	And the member should see that the number of available copies of the book on the Details page is increased by 1

Scenario: Removal
	Given the member has a reserved book
	And the expiry date column of the book has a date in it
	And another member has that book reserved
	When the member chooses the book
	And the member clicks the Remove reservation button
	Then the member shouldn't see the book in his list
	When the second member logs in
	Then the second member should see a notification window
	And the second member should see that the number of available copies of the book on the Details page is 0
	
