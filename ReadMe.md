Please ensure the required .NET 9.0.202 version is installed:
- If not kindly download and install from https://dotnet.microsoft.com/en-us/download.

Third Party Package:
- No need to install manually, added a nuget.config which will install automatically on build

UserManagementAPI:
- User Controller
    - Consists of different http requests such as GetAllUsers, GetUserById, AddUser, UpdateUser, DeleteUser
- User Service 
    - Used to perform all business logic. For example: Validate the duplicate user name
- User Repository
    - Used to perform all database logic here.

Database:
    - Used In-memory sqllite

UserManagementAPITest:
    Used to cover both positive & negative scenarios
- UserControllerUnitTest
    - Which will validate all the controller methods
    - User Service is mocked
- UserServiceUnitTest
    - Which will validate all the business logic
    - User repository is mocked

SOLID Practice:

1. Single Responsibility Principle (SRP)
UserService is responsible for handling business logic related to only user operations (create, update, delete).
UsersController is responsible for handling API requests and delegating tasks to the service layer.
AppDbContext is responsible for managing database interactions.

2. Open/Closed Principle (OCP)
The IUserService interface makes it easy to add new implementations of user management without altering the current implementation.

3. Liskov Substitution Principle (LSP)
The IUserService interface guarantees that any implementation can be used as a substitute without breaking functionality.

4. Interface Segregation Principle (ISP)
The IUserService interface defines a focused contract for user-related operations.

5. Dependency Inversion Principle (DIP)
The controller depends on the abstraction (IUserService) rather than the concrete implementation (UserService).

Swagger:
- Basic authentication is enabled to secure the request
    - Authentication Credentials:
        - UserName : admin
        - Password : docosoft
