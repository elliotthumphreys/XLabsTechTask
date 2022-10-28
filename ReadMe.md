# Beer Quest Api

## 01 — Approach

I have attempted to make this solution as easy to run as possible. I have used docker compose to containerize the 
application and sql server instance. If you don't have this on your machine already then you'll need to [download 
it here](https://www.docker.com/products/docker-desktop/). I have also set up a github workflow to run the unit 
tests and integration tests.



## 02 — Running the solution

You have a few options when running the main application:

1. In visual studio run the solution using the `docker-compose` startup project option selected. This will run the web api and 
the sql server instance in a docker container.

2. Navigate to the solution folder in a cli tool of your choice and run the following command `docker-compose --verbose up -d`. 
Again this will run the web api and the sql server instance in a docker container.

3. The final option is to not use docker at all and to just run the dotnet project locally. You will need to update 
the connection string for the sql server in the appsettings. Don't worry about creating the database as that will be 
done in the start up of the application. To run the WebApi navigate to the solution folder and run the following 
command `cd WebApi && dotnet run`. In development mode the project will fail to start if the sql server is unreachable.

## 03 — Tests

### 03.1 — Unit Tests

I have created a unit tests project that works based on an in memory database and targets the `Application` layer of my project.
After each test runs the `BaseTestFixture` has a tear down method that drops the database - this ensures the tests are running in
isolation. To run the unit tests navigate to the main solution folder and run `dotnet test Tests/UnitTests/`

### 03.2 — Integration Tests

The integration tests uses a HttpClient and the DataAccess project to target the WebApi and ensure the expected changes occur to the 
database. The implementation I have done is pretty crude, it simply runs each tests and then removes any new `Venue` records created in 
the `TearDown` method. This approach has some drawbacks, the main one being if you don't clear down the db properly after running the 
tests then you could end up with a 'dirty' db.

To run the integration tests you must first ensure the main project is running and the database has been created with the relevant table. 
You can then run the integration tests in visual studio using the test explorer, or by running one of the following commands in the main
solution folder:

1. `dotnet test Tests/IntegrationTests/`
2. `docker-compose -f docker-compose-tests.yml run integration-tests`
## 04 — Database

The `Domain` project contains all the entities for the database. 
I have then created a `DataAccess` project that is versioned with dotnet ef migrations.

### 04.1 — Credentials

For local development, database will be seeded on first migration. So when you run the solution
locally you should have a populated database at the below details.

- **Server:** localhost, 1433
- **User:** sa
- **Password:** LocalUserPassword1*
- **Database:** BeerQuest

### 04.2 — Migrations

The following code can be used to perform a new migration on the db after making 
changes to the entities in the `Domain`.

```
dotnet-ef migrations add [name of migration] --startup-project=WebApi --project=DataAccess --verbose
```

I have also added some code to apply the migrations programmatically when in development mode
only.

```
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<BeerQuestDbContext>();
        db.Database.Migrate();
    }

    ...
}
```

This has only been done for local development as this approach is not recommended
for in-production environments 
[[ref]](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli#apply-migrations-at-runtime). 
If we deployed this application to a production server we would have to
apply these database changes manually, most likely through the `dotnet ef bundles` command 
[[ref]](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli#bundles).

## Improvements

1. Swagger docs have been used here but I have not provided any of the response dtos, so the swagger UI looks a bit bare.
2. I have not done any pagination on the get-all endpoints, this would be simple enough to achieve I just didn't get around to doing it.
3. Integration tests are sparse. I just wanted to show how they could be achieved and haven't tested everything that could / should have been.
4. Docker - I have never used docker before and I just wanted to try it out for this project. Turns out it is pretty powerful but is a massive 
pain to get setup properly, especially with the integration tests I was trying to run.
5. The way I have read the seed data into the system has also not been tested, a few unit tests around this would probably be better.
6. Connection strings are just directly stored in my docker-compose and appsettings files - would not do this in production but for a tech task
I thought it would be overkill to do anything else. 