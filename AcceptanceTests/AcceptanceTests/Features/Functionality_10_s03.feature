Feature: Deleting reviews


Scenario: Deleting an existing review
  Given the user has previously borrowed the book through the system
  And the user has previously written a review for the selected book
  And the user is on "All Reviews" form
  When the user presses the "Obriši Recenziju" button
  Then the user's review is deleted from the database
  And an error window appears with the message "Vaša recenzija je uspješno obrisana!"

Scenario: Deleting a non-existing review
  Given the user has previously borrowed the book through the system
  And the user is on "All Reviews" form
  And the user has not previously written a review for the selected book
  When the user presses the "Obriši Recenziju" button
  Then an error window appears with the message "Niste napisali recenziju za ovu knjigu!"