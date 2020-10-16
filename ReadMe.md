# Artists_Search
This project is a .Net core 3.1 web API that uses Spotify services to search for artists, and also expose an API end-point to allow end-user to search for an artist

## Assumptions

- I'm returning the first matched result from the search result and ignore the rest
- Spotify service calls will not fail
- Spotify results do not change frequently
- This service doesn't require authentication

## architecture

This project is N tier architecture

1. **ArtistSearch.API**  
  This project is a .net core 3.1 web API contains the API layer, DI, and service configuration.

2. **ArtistSearch.BusinessLogic**
  This project is a .net standard project that holds the business logic

3. **ArtistSearch.Infrastructure**
  This project is a .net standard project that holds the infrastructure logic and data access and isolates it from the rest of the app.


## Getting Started

- Make sure you have docker installed
- Go to project root directory and execute this line in the command prompt

```
docker-compose up --build  
```

## Logging

In real life we log to a logging service and also I would implement a middleware for logging but I want to simplify that for now and just print simple logs to the console

# Spotify requests

Retries on failure should be implemented but I didn't have enough time to do that.

## Tests
- Xunit project is used to implement tests
- I added 2 test cases but didn't have enough time to create more unit or integration tests for the solution
