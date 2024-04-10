Feature: Functionality_08_s03

As a member I want to be able to use the wishlist functionality
Background: 
	Given the user is logged in as a member

@tag1
Scenario: Adding to wishlist
	Given the member is on the details page of a non digital book
	And the member has 2 or less books in his wishlist
	When the member presses the button Add to wishlist
	Then the member should see a confirmation message
	And the member should see the book in his wishlist

Scenario: Maximum number of books
	Given the member is on the details page of a non digital book
	And the member has 3 books in his wishlist
	When the member clicks the button Add to wishlist
	Then the member should see a warning message that the maximum number of wishlisted books has been reached
	And the member shouldn't see the book in his wishlist

Scenario: Already added book
	Given the member is on the details page of a non digital book
	And the member has that same book added to his wishlist
	When the member clicks the button Add to wishlist
	Then the member should see a warning message that the book is already on his wishlist
	And the member shouldn't see the book in his wishlist two times

Scenario: Book removal
	Given the member is on the Wishlist screen
	And the member has a book added to his wishlist
	When the member chooses a book from the list
	And the member clicks on the Remove book from wishlist button
	Then the member should see a refreshed list with the book removed

Scenario: Non selected book removal
	Given the member is on the Wishlist screen
	When the member clicks the Remove book from wishlist button
	Then the member should see a warning message that a book has to be selected
