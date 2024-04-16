Feature: Registering new members

As an employee
I wnat to be able to register new memebers
So that tehy can borrow and reserve books

Background: 
	Given Given the user is logged into app as employee
	And is on the Member Management panel
	And clicks register member button
@tag1
Scenario Outline: Invalid required parameters
	When the employee enters OIB <OIB> username <username> and password <password>
	And clicks the generate barcode button
	And clicks Save button on that screen
	Then the error message should appear

	Examples: 
	| OIB | username | password |
	| 1111111mmmm | pjuric21 | juric123 |
	| 23423455869 | pjuric21 | juric123 |
	| 45672345678 | horvatkr | horvat123 |
	| 45672345678 | pjuric21 |  |

	Scenario Outline: Valid required and nonrequired parameters
		When the employee enters name <name> surename <surename> OIB <OIB> username <username> password <password>
		And clicks the generate barcode button
		And clicks Save button on that screen
		Then the member managment panel should be visible
		And the table with members should contain member with <OIB> <username>
	Examples: 
	| name | surename | OIB | username | password |
	|  |  | 44115345678 | jana2345 | jjjj | 
	| Magdalena | Markovinović | 45672345678 |  megica08 | lozinka123 |

Scenario Outline: Giving up on registration
	When the employee enters name <name> surename <surename> OIB <OIB> username <username> password <password>
	And clicks the generate barcode button
	And clicks Cancle button
	Then the member managment panel should be visible
	And the table should not contain that member <OIB> <username>

	Examples: 
		| name | surename | OIB | username | password |
		| Magdalena | Markovinović | 45672345678 |  megica08 | lozinka123 |
	
