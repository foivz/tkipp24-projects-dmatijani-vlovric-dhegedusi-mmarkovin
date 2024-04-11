Feature: Book catalogue search and filter

As a member I want to be able to search and filter the book catalogue

Background: 
	Given the user is logged in as a member
	And the user is on the Search library catalogue screen

@tag1
Scenario: Book search works
	When the member enters information such as author name, author last name, year of release or book name of books that exist
	Then the member should see the book with that information in the list

Scenario: Digital book inclusion
	When the member enters information into the search bar
	And the member includes digital books into the search
	Then the member should see digital books with that information

Scenario: Search criteria
	When the member selects one type of criteria
	And the member enters information into the search bar
	Then the member should see books that match information of only the chosen criteria

Scenario: Clear filters
	When the member chooses a search criteria
	And the member includes digital books
	And the member types into the search bar
	And the member clicks the button Clear filters
	Then the member should see that all of the inputs have been returned to default and empty

Scenario: Book details
	When the member enters information into the search bar
	And the member chooses a book from the list
	And the member clicks on the See details button
	Then the member should be redirected to the Book details screen

Scenario: Book details with no book
	When the member doesn't choose a book from the list
	And the member clicks on the See details button
	Then the member should stay at the same screen
	And the member should see a warning message that a book has to be chosen

