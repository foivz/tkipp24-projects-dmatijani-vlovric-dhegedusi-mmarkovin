Feature: Business Statistics

Background: 
Given the user is logged in as a library employee
And the user is on the "Statistics" form

Scenario: Viewing revenue statistics
  Given there are records of at least one registered member in the library
  When  the user selects the option "Prihodi" from the criteria list
  Then the user should see two details: the number of registered members and the total revenue from membership fees

Scenario: No registered members in the library
  Given there are no records of registered members in the system
  When  the user selects the option "Prihodi" from the criteria list
  Then the user should not see any revenue details