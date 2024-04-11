Feature: Review Statistics

Background: 
Given the user is logged in as a library employee
And the user is on the Statistics form

Scenario: Viewing review statistics
  Given there are records of at least one review written by a member
  When  the user selects the option Statistika recenzija from the criteria list
  Then the user should see a list of all ratings along with the number of times each rating has been used by members

Scenario: No written reviews in the system
  Given there are no records of written reviews by members in the system
  When  the user selects the option Statistika recenzija from the criteria list
  Then the user should not see any review listed