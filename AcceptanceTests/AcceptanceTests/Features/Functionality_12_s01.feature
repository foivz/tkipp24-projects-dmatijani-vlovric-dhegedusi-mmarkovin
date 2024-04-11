Feature: Reading Online Books

Background:
Given the user is logged in as a library member

 Scenario: Reading an existing book in digital format
  Given the user has internet access
  And the user is on the "Book details" form
  And there is a digital version of the selected book in the system
  And the book has a correct link
  When the user clicks on the "Digital Version" button
  Then an in-app web browser should open displaying the text of the book
  And the user can successfully read the book in digital format

Scenario: Attempting to read a non-online book
  Given the user has internet access
  And the user is on the "Search books" form
  And there is no digital version of the selected book in the system
  When the user selects a book
  And clicks on the "Detalji" button
  Then the book details page should not have a "Digitalna verzija" button

Scenario: Attempting to read an online book without internet access
  Given the user does not have internet access
  And the user is on the "Book details" form
  And there is a digital version of the selected book in the system
  And the book has a correct link
  When the user clicks on the "Digitalna verzija" button
  Then an in-app web browser should open
  And display an error message due to the inability to connect to the internet

Scenario: The book has an invalid link for the digital version
  Given the user has internet access
  And the user is on the "Book details" form
  And there is a digital version of the selected book in the system 
  And the book has an invalid link
  When the user clicks on the "Digitalna verzija" button
  Then the user should see an error message stating "Knjiga ima nevažeći link!"
