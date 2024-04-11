Feature: User logout

As user
I want to be able to log out of application 
So that i can log in with different credentials
@tag1
Scenario: User logout
	Given the user is logged into app
	When the user clicks Logout button
	Then the user should see the Login form
