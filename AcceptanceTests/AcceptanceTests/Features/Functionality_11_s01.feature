Feature: Book statistics

Background: 
Given the user is logged in as a library employee
And the user is on the Statistics form

Scenario: Viewing the most borrowed books
  Given there are records of at least one borrowed book in the system
  When  the user selects the option Najposuđenije knjige from the criteria list
  Then the user should see details of the most borrowed books sorted from most to least borrowed

Scenario: No borrowed books in the system
  Given there are no records of borrowed books in the system
  When  the user selects the option Najposuđenije knjige from the criteria list
  Then the user should not see any book details