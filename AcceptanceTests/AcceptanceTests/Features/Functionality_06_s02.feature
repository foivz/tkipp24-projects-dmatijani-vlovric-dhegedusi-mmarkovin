Feature: Functionality_06_s02

As an employee
I wnat to be able to register new memebers
So that tehy can borrow and reserve books

Background: 
	Given Given the user is logged into app as employee
	And is on the Member Management panel
@tag1
Scenario Outline: Invalid required parameters
	When the employee enters OIB <OIB>, username <username> and password <password>
	And clicks the generate barcode button
	And clicks Save button
	Then the error message should appear <error>

	Examples: 
	| OIB | username | password |
	| 1111111mmmm | pjuric21 | juric123 |
	| 23423455869 | pjuric21 | juric123 |
	| 45672345678 | horvatkr | horvat123 |
	| 45672345678 | pjuric21 |  |

	Scenario Outline: Valid required and nonrequired parameters
		When the employee enters name <name>, surename <surename>, OIB <OIB>, username <username> password <password>
		And clicks the generate barcode button
		And clicks Save button
		Then the employee should see member managment panel
		And the table with members should contain that member

	Examples: 
	| name | surename | OIB | username | password |
	|  |  | 55552345678 | pjuric21 | juric123 |
	| Magdalena | Markovinović | 45672345678 |  megica08 | lozinka123 |

Scenario Outline: Giving up on registration
	When the employee enters name <name>, surename <surename>, OIB <OIB>, username <username> password <password>
	And clicks the generate barcode button
	And clicks Cancle button
	Then the employee should see member managment panel
	And the table with members should not contain that member

	Examples: 
		| name | surename | OIB | username | password |
		| Magdalena | Markovinović | 45672345678 |  megica08 | lozinka123 |
	
