# BookingManagementSystem

Problem
Using .net; and whichever SQL database (e.g. SQLite, PostgreSQL) or in-memory and third-party libraries you feel are appropriate, create a web application which is capable of the following:
•	Uploading the attached csv files (via command line or a web form or API endpoint) and writing the data to a database.
•	Booking an item from the inventory in the name of a member via a /book endpoint. Whenever a member makes a booking, keep a record of it in the database, including datetime of booking. A booking will only be possible if a member hasn’t reached MAX_BOOKINGS = 2, and if the inventory’s remaining count hasn’t been depleted.
•	Cancelling a booking for a member based on a booking reference via a /cancel endpoint.
Please use the two files provided along with this test, to help you set up a database with some information in it:
-	The “members.csv” file contains some data about the members
-	The “inventory.csv” file contains bookable experiences

Essential skills to demonstrate for assessment
•	ability to manipulate raw data files
•	create a suitable database structure, read and write data to a database
•	use a framework that allows customers to connect via web, to create suitable models to store this data
•	create endpoints
•	Write unit test cases
•	give us the end-to-end API build 
•	show us the deployable working

The structure, layout and content of the application are up to you.
It is preferred to use following technologies but not mandatory 
•	.NET 6 or above
•	GRPC protocol 
•	Design patterns (e.g. Mediator)
•	AutoMapper
•	Unit test cases (Xunit)

Please use Git source control for the solution and if possible, upload the solution and its dependencies to a repository such as bitbucket or GitHub.

To be supplied
-	Assessment Code – preferable on GitHub or similar
-	Any database scripts
-	Any setup instructions
