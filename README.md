# NextechExercice
1- The news that we're going to show are the ones that we can get from the newsstories endpoint. (Latest 500 stories should be enough).
2- As search criteria I've used just the title of the news. The searching is case insensitive.
3- The search it's going to retrieve all the news that contains in their title the filterName that we write.
4- Cache keys duration is of 5 minutes. It can be changed from the appsettings.json file modifying "ExpirationTimeCache" values section with a numeric value.

****Development period****
Start date and time: 2/27/2023 13:00
End date and time: 3/2/2023 16:00

****Frameworks used****
Bcakend API: .NET 7.0
Frontend: Angular 14.2.5