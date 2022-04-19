# PokemonTeamAnalyzer
Pokemon Team Analyzer is a desktop razor pages web application for viewing and analyzing individual Pokemon information, as well as Pokemon team information.

## Project Setup
As Pokemon Team Analyzer uses git submodules, in order to clone the project correctly use `git clone --recurse-submodule`.
This will clone the project into the desired directory and create and initialize all submodules (In this case just one: PokeNetApi).

##### Microsoft Sql Server
Pokemon Team Analyzer utilizes Microsoft SQL server and is required to run the project locally. If you do not have Microsoft SQL server installed it can be installed from Microsoft's website: https://www.microsoft.com/en-us/sql-server/sql-server-downloads.

##### Database setup
The application uses a database to fill all of its data into, so you will need to generate the database schema locally and provide the application with the connection string for that database. The Visual Studio Solution contains a SQL project type that can be published to generate the database and provide you with the connection string. Simply right click on the project and select "Publish". Then a window will pop up that looks like this:

![Screenshot (10)](https://user-images.githubusercontent.com/92762777/163693288-77eaf16f-fff9-47a2-9b5e-42f60dbc5a66.png)

Fill out the target database connection field, leave the others as they are, copy the connection string, and finally hit publish and visual studio will generate the database. To verify, you can check that it exists in the SQL Server Object Explorer.

##### Connection String
The application will expect the default connection string for it to use to be in the "Connection Strings" property in `appsettings.json` (PokemonTeamAnalyzerRazorUI project) under "Default".

![Screenshot (11)](https://user-images.githubusercontent.com/92762777/163693474-76092b29-e5f6-47ac-b76d-8e5a69bf4c8c.png)

The app also requires MARS (Multiple active result sets) to be enabled for databse operations. So find the property in your connection string and set it to `MultipleActiveResultSets=True`

If that property isnt in your connection string, simply add it in.

## Running the Project
The first-time run of the project should take a little while to setup as it is going to be pulling a lot of data and seeding it into the database. If you would like to see its progress: Dont run the project through IIS Express, click the dropdown and select the option with the same name as the solution (PokemonTeamAnalyzer).

![Screenshot (12)](https://user-images.githubusercontent.com/92762777/163693580-8ef2ba45-cc3c-43f0-b794-c3598d223133.png)

This will launch the application with the Console active, which will give updates on what it's doing.
