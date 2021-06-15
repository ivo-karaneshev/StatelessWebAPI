# StatelessWebAPI
Simple .Net 5.0 stateless Web API with Redis caching and MS SQL database.
## How to run
You must have Redis running and configured on port **6379**. Edit the `appsettings.json` file to change the server. The default one is **localhost**.
The API uses Entity Framework Core for data access. Database migrations will apply automatically when running in **Debug** mode. If Redis is configured correctly, just hit F5 from Visual Studio and the API should be up and running.
## Caching
Data caching is done using the Cache-Aside pattern. The basic algorithm is:
- Read: get data from cache if present; if not - get data from database and then write it to the cache
- Write: first update the database then invalidate the updated data from the cache

The API also supports cache expiration which is configured from `appsettings.json` by setting `CacheExpirationTimeMinutes`. If set to `0` or missing then cached data won't expire until invalidated.

## Testing
A Postman collection can be found at `/Testing/Postman`. It uses collection variables for request parameters so no further configuration is required. After starting the API, just run the collection and it will execute each request and validate its tests.
