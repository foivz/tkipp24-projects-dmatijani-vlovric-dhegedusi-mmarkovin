Feature: Genre statistics

Background: 
Given the user is logged in as a library employee
And the user is on the Statistics form

Scenario: Viewing borrowings by genres
  Given there are records of at least one borrowed book with defined genre in the system
  When  the user selects the option Posudbe po žanrovima from the criteria list
  Then the user should see a list of genres along with the number of books borrowed for each genre

Scenario: No borrowed books with defined genres in the system
  Given there are no records of borrowed books with defined genres in the system
  When  the user selects the option Posudbe po žanrovima from the criteria list
  Then the user should not see any genre listed