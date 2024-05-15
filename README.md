# ApplicationForm

**Configuration Management **
- Centralized Configuration : Used Centralized Configuration for cosmos DB setting  including endpoint URI, database name, primary key, and container names in appsettings.json

**Architectural Seperation **

1)Controller : Acts as an API endpoint, handling HTTP request and responses. It delegates the business logic and data operation to helper and manager classes.
2)Helper classes: It Handles DTO to Entity conversion and vice versa and encapsulate business logic and acts as a communication between controller and manager classes.
3)Manager classes: Performs the database operation,such as saving data to DB and fetching.

**Model and DTO Separation**

1)ProgramApplication Models and DTOs:
  i)Created separate models and DTOs for program application creation. These are designed to save and fetch only the field information necessary for creating a program application.
2)Response Models and DTOs:
   i)Created distinct models and DTOs for handling responses. These are tailored to save and fetch only the question and answer information related to program responses.

**Custom Exception Handling**

ValidationException:

1)Introduced a custom exception class called ValidationException to handle validation-related errors in a more structured and meaningful way.

**Enum for Question Types**

QuestionType Enum:

1)Introduced an enumeration (QuestionType) for defining various types of questions, such as YesNo, Date, Number, Paragraph, and DropDown.
**Test Class Setup**

The ResponseHelperTests class sets up the necessary mock dependencies (IResponseManager and IProgramApplicationHelper) and initializes the ResponseHelper with these mocks.

Scenarios Covered

1)SubmitResponse_ShouldSubmitResponse_WhenFormExists:

Purpose: Verifies that the SubmitResponse method correctly submits a response when the form exists.
Setup: Mocks the GetProgramApplicationById method to return a valid ProgramApplicationDto. Also, mocks the SubmitApplication method to complete successfully.
Assertions: Ensures the form is fetched once and the response is submitted once.

2)SubmitResponse_ShouldThrowValidationException_WhenFormNotFound:

Purpose: Ensures that a ValidationException is thrown when the form is not found.
Setup: Mocks the GetProgramApplicationById method to return null.
Assertions: Verifies that the correct exception is thrown with the message "Form not found". Also, checks that the SubmitApplication method is never called.

3)SubmitResponse_ShouldThrowValidationException_WhenRequiredFieldIsMissing

Purpose: Ensures that a ValidationException is thrown when a required field is missing in the response DTO.
Setup: Mocks the GetProgramApplicationById method to return a valid ProgramApplicationDto. Creates a ResponseDto with a missing required field (e.g., FirstName).
Assertions: Verifies that the correct exception is thrown with the appropriate message (e.g., "FirstName should not be null or empty"). Also, ensures the response is not submitted.

The ProgramApplicationHelperTests class sets up the necessary mock dependency (IProgramApplicationManager) and initializes the ProgramApplicationHelper with this mock.

Scenarios Covered

CreateProgram_ShouldReturnProgramId_WhenProgramIsCreated:

Purpose: Verifies that the CreateProgram method returns the correct program ID when a program is successfully created.
Setup: Mocks the CreateProgram method to return a generated program ID.
Assertions: Ensures the correct program ID is returned and the CreateProgram method is called exactly once.

CreateProgram_ShouldThrowException_WhenManagerFails:

Purpose: Ensures that an exception is thrown when the program creation fails.
Setup: Mocks the CreateProgram method to throw an exception.
Assertions: Verifies that the correct exception is thrown and the CreateProgram method is called exactly once.

UpdateProgram_ShouldReturnProgramId_WhenProgramIsUpdated:

Purpose: Verifies that the UpdateProgram method returns the correct program ID when a program is successfully updated.
Setup: Mocks the UpdateProgram method to return the same program ID.
Assertions: Ensures the correct program ID is returned and the UpdateProgram method is called exactly once.

GetProgramApplicationById_ShouldReturnProgramApplicationDto_WhenProgramExists:

Purpose: Verifies that the GetProgramApplicationById method returns the correct ProgramApplicationDto when the program exists.
Setup: Mocks the GetProgramApplicationById method to return a valid ProgramApplication.
Assertions: Ensures a non-null ProgramApplicationDto is returned, verifies the type and checks that the properties match the expected values. The method is called exactly once.

GetProgramApplicationById_ShouldThrowValidationException_WhenProgramDoesNotExist:

Purpose: Ensures that a ValidationException is thrown when the program does not exist.
Setup: Mocks the GetProgramApplicationById method to return null.
Assertions: Verifies that the correct exception is thrown with the message "Form Not found". The method is called exactly once.





