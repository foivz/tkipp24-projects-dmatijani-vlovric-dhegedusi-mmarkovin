Feature: Writing reviews

Scenario: User has previously written a review for the selected book
  Given the user has previously written a review for the selected book
  And the user is on All Reviews form
  When the user selects the Add Review option
  Then the application remains on the All Reviews form
  But the application displays an error message Već si napisao recenziju za ovu knjigu!

Scenario: Correctly selected rating and filled comment field
  Given the user has previously borrowed the book through the system
  And the user has not written a review for the selected book
  And the user is on the Add Review form
  When the user selects a rating for the review
  And the user enters an optional comment
  And the user clicks the Dodaj button
  Then the review is stored in the database
  And the user is shown All Reviews form where he can also see his review

Scenario: Correctly selected rating and empty comment field
  Given the user has previously borrowed the book through the system
  And the user has not written a review for the selected book
  And the user is on the Add Review form
  When the user selects a rating for the review
  And the user clicks the Add button 
  But the user doesn't fill the comment field
  Then the review is stored in the database
  And the user is shown the All Reviews form where he can also see his review

Scenario: Correctly selected rating and filled comment field but canceling writing review
  Given the user has previously borrowed the book through the system 
  And the user has not written a review for the selected book
  And the user is on the Add Review form
  When the user selects a rating
  And the user enters an optional comment
  But the user clicks the Odustani button
  Then the review is not stored in the database
  And the user is taken back to the All Reviews form

Scenario: Correctly selected rating and empty comment field but canceled writing review
  Given the user has previously borrowed the book through the system 
  And the user has not written a review for the selected book
  And the user is on the Add Review form  
  When the user selects the Add Review option
  When the user selects a rating
  But the user doesn't fill the comment field
  And the user presses the Cancel button
  Then the review is not stored in the database
  And the user is taken back to the All Reviews form

Scenario: Empty rating of the review
  Given the user has previously borrowed the book through the system 
  And the user has not written a review for the selected book
  And the user is on the Add Review form
  When the user presses the Dodaj button without selecting a rating
  Then the application does not add the review to the database
  And the application remains on Add Review form
  But an error window appears with the message Moraš dodati ocjenu prije objavljivanja recenzije!

Scenario: User has not borrowed the book before writing a review
  Given the user has never borrowed the book through the system
  And the user is on All Reviews form
  When the user selects the Add Review option
  Then the application does not open a form for writing a new review
  But an error window appears with the message Moraš posuditi knjigu prije pisanja recenzije!
