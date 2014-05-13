Feature: SimpleStory
	In order to record accidents
	As an authorised user
	I want to be able to create a record in the accident book

@mytag
Scenario: Create an accident report
	Given That I am an authorised user 
	And I have entered a description of the accident and a date and time
	When I press save
	Then the record should be created and displayed on the screen.
