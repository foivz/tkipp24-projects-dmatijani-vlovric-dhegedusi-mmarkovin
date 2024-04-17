Feature: Filtering members

As an employee
I wnat to be able to filter memebers by name and surname
So that I can easily find them

Background: 
	Given the user is logged into app as employee
	And is on the Member Management panel

Scenario Outline: Filtering members by name and surename
	When Employee enters name <name> and surename <surename>
	And clicks the Filter button
	Then application shows <name> and <surename>

	Examples: 
		| name | surename |
		| Sebastijan |          | 
		|            | Kralj    |
		| Sebastijan | Kralj    |

Scenario: Filtering members that do not exist
	When Employee enters name <name> and surename <surename>
	And clicks the Filter button
	Then application shows empty table

	Examples: 
		| name   | surename |
		| Petra | Perković |

Scenario: Clearing the filter
	When Employee enters name <name> and surename <surename>
	And Employee clicks the clear button
	Then application shows all members

	Examples: 
		| name  | surename |
		| Sebastijan | Kralj    |