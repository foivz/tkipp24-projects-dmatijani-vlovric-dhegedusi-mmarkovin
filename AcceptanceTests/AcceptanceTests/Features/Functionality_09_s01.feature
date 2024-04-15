Feature: Book reservation

As a member I want to be able to put in reservations for books
Background: 
	Given the user is logged in as a member

@tag1
Scenario: F09_S01_C01 Digital book
	Given the member is on the Search book catalogue screen
	When the member chooses a digital book
	And the member clicks on the See details button
	Then the member shouldn't see a Reserve button

Scenario: F09_S01_C02 Reservation
	Given there exist books A, B, C and D
	And the member has 2 or less reservations
	And the member is on the Details screen of a non digital book A
	When the member clicks the Reserve button
	Then the member should see a reservation confirmation message
	When the member confirms the reservation
	Then the member should see his reservation position instead of the Reserve button
	When the member goes to the Reservations screen
	Then the member should see book A in his list

Scenario: F09_S01_C03 Decline reservation
	Given the member has 2 or less reservations
	And the member is on the Details screen of a non digital book B
	When the member clicks the Reserve button
	Then the member should see a reservation confirmation message
	When the member  declines the confirmation
	And the member goes to the Reservations screen
	Then the member shouldn't see book B in his list

Scenario: F09_S01_C04 Maximum number of reservations
	Given the member reserves books B and C
	And the member is on the Details screen of a non digital book D
	When the member clicks the Reserve button
	Then the member should see a warning message
	And the member goes to the Reservations screen
	Then the member shouldn't see book D in his list

Scenario: F09_S01_C05 Remove reservation see button
	Given the member is on the Reservations screen
	And the member has a reserved book A
	When the member chooses reserved book A from the list
	And the member clicks on the Remove reservation button
	Then the member should see a removal confirmation message
	And the member shouldn't see book A in his list
	When the member goes to the See details screen of book A
	Then the member should see the Reserve button

Scenario: F09_S01_C06 Notification receival
	Given the member has a reserved book B
	And an employee entered a new copy of the book B
	When the member logs in
	Then the member should see a notification with his reservation expiry date
	When the member goes to the Reservations page
	Then the member should see a date in the Expiry column of the book

Scenario: F09_S01_C07 Multiple notification receival
	Given the member reserves book A
	And an employee entered a new copy of the books A and C
	When the member logs in
	Then the member should see a notification with his reservation expiry dates
	When the member goes to the Reservations page
	Then the member should see a date in the Expiry column of the 2 books

Scenario: F09_S01_C10 Solo removal
	Given the member has a reserved book C
	And no other member has that book reserved
	And the expiry date column of book C has a date in it
	When the member chooses book C
	And the member clicks the Remove reservation button
	Then the member shouldn't see book C in his list
	And the member should see that the number of available copies of book A on the Details page is 1

Scenario: F09_S01_C11 Removal
	Given the member has a reserved book B and a notification
	And the expiry date column of book B has a date in it
	And another member has book B reserved
	When the member chooses book B
	And the member clicks the Remove reservation button
	Then the member shouldn't see book B in his list
	When the second member logs in
	Then the second member should see a notification window
	And the second member should see that the number of available copies of book B on the Details page is 0
	
