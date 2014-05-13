Feature: SimpleStoryControllerTest
	To allow the recording of incidents
	the user will need to be able
	to enter the details in a web page and save them.

@mytag
Scenario: The user navigates to the accident entry page
	Given That I am a user authorised to create Accident Reports
	When I navigate to the accident entry page
	Then the result should be the accident entry view

Scenario: The unathorised user navigates to the accident entry page
	Given That I am a user not authorised to create Accident Reports
	When I navigate to the accident entry page
	Then the result shoulbe be a 404