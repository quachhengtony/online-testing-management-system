# Online Testing Management System

Online Testing Management System provides a web-based platform that enables efficient creation, administration, management, and participation of tests while incorporating advanced features like cheat detection and test randomization to maintain the integrity of the testing process.

## Description

The Online Testing Management System is a web-based application designed using 3-layers architecture that facilitates the creation, administration, management, and participation of tests online. The system has been designed with three user roles - Test Creators, Test Takers, and Admin. Test creators can create, manage and organize question banks to be used to create tests, while test takers can take tests using unique key codes provided by the test creators. The admin role provides management of user accounts, test settings, and the overall system's operations. The system also includes features such as randomization of tests, test timer, and an advanced cheat detection system.

## Features

- Test creators:
  - Create and manage question banks.
  - Create and manage tests using question banks.
  - Set time limits for tests.
  - Randomize tests if the test creators created many tests to use in the same batch.
  - View test results of test takers.
- Test takers
  - Participate in tests using key codes provided by the test creators.
  - Complete tests within the set time limit.
  - Submit tests and view their results.
  - Get an alert when they switch tabs or attempt to copy and paste outside of the test screen.
  - Their eye movement is checked to detect if they are looking outside the test screen.
- Admin
  - Manage test creators' and test takers' accounts.

## Installation

1. Clone the repository.
2. Open the project in Visual Studio.
3. Configure the database connection string in the appsettings.json file.
4. Run the following commands using dotnet CLI to create the database and run the migrations:
   ```sh
   dotnet ef database update
   ```
5. Build and run the project.

## Technologies

- Frameworks: [.NET](https://dotnet.microsoft.com/en-us/).
- Libraries: [Entity Framework](https://docs.microsoft.com/en-us/ef/).
- Database: [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
- Others: [jQuery](https://jquery.com/), [SignalR](https://dotnet.microsoft.com/en-us/apps/aspnet/signalr).

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change. Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
