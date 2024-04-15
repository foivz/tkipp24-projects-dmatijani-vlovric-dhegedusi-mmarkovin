Feature: Reviewing all reviews

Scenario: Viewing all reviews related to a selected book
  Given the user has selected a book and is on the book details page
  When the user presses the Recenzije button
  Then the user should see a form with all written reviews for the selected book
