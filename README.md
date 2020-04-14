# isolated-development-demo
A demo of how to replace dependencies in order to isolate the application during development

## Running the application
.NET Core 3.1 must be install on your machine in order to build the application
The application may be run in two modes: integrated mode where production dependencies are used, or isolated mode, where stubs are used.
To run in integrated mode:
1. Open a shell and cd into [repo directory]/IsolatedDevelopment.Web
2. Execute `dotnet run`
3. Open in browser

To run in isolated mode:
1. cd into [repo directory]/IsolatedDevelopment.Isolated.Web
2. Execute `dotnet run`
3. Open in browser

(Or open the solution in Visual Studio and run the projects from there )

## Notable files and concepts
In integrated mode, `IntegratedDependency` is used and the Foo header is not set. In isolated mode, `IntegratedDependencyStub` is used and `InjectCookieMiddleware` is executed, setting a value for the Foo header. This allows for bypassing steps such as logging in when testing new features, and this is achieved with no test code in the production project.
All of this is achieved with a minimum of duplication. The Program.cs file in the `Isolated.Web` assembly uses all of the code in the `Web` assembly, but injects middleware to the start of the pipeline and replaces the dependency with a stub. The application will look and behave like in integrated mode apart from injected middleware and dependency replacements.
[TestHost](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1) is used in the test assembly to run integration tests on an in memory instance of the application. These tests don't have any external dependencies and can be run in any environments

## Author's notes
The design is far from perfect, as this is merely meant as a demonstration of the possibilities. Especially `FirstMiddlewares` in Startup.cs is not as neat as I wanted it to be. Feedback and suggestions are very welcome!