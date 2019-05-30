# NetCoreLearning
Learning .Net Core 2.2 utilizing SignalR, Razor Pages, IHostedServices, BackGround Services, NCrontab, NServiceBus(with learning Transport and persistence), SQL Server, WEB API, XUnit, Moq, Redis distributed cache, IMemoryCache, Async programming,JWT Authentication, Swagger, Microservices Approach.


This solution is a dashboard showing live the daily grade assignments by professors to students for specific lessons.
It is not a valid business scenario, it has just a simple domain model so that it can be used by tha APIs and to support the nuget packages/products mentioned above.

NetCoreLearning.SchoolLessons.Razor.SignalR
The Ui is Razor Pages with SignalR javascript client.(client part)
In the same project there is a SignalR Hub that reads the Grade Assigments of the day from a Redis distributed cache.(server part)

How this Redis distributed cache gets filled:

NetCoreLearning.SchoolLessons.API:
There is a Web APi that uses JWT Authentication. It is connected with an Microsoft SQL Server Database. Through the API a user can create a Grade assignment related to a professor, a student and a lesson.
Upon a Grade assignment creation, a new event is published through the event bus containing this info.
The API also hase a NCronTab scheduler that re-publishes all the messages that failed to be published.


NetCoreLearning.SchoolLessons.API.Tests
The API is Unit tested with XUnit and Moq Framework.
The API is documented with Swagger.


NetCoreLearning.SchoolLessons.Worker:
On another host, a hosted service is subscribed to this event type and handles the event by adding the new Grade assignment to the Redis distributed cache.
On the same host(it could be in another), there is a worker that connects to the API and creates Grade assigments every 30 seconds.(Dummy generator)


Flow: 
1.NetCoreLearning.SchoolLessons.Worker worker generates dummy Grade assigments through the NetCoreLearning.SchoolLessons.API.
2.NetCoreLearning.SchoolLessons.API produces event with the new record.
3.NetCoreLearning.SchoolLessons.Worker event handler adds the new record to the Redis distributed Cache.
4.NetCoreLearning.SchoolLessons.Razor.SignalR hub reads every 1 minute the Redis distributed Cache and sends the changes to all the clients.

Redis distributed Cache clears every night.

everything reusable/generic is included in Infrastructure solution folder:
NetCoreLearning.Infrastructure.Authentication.JWT
   Implements a JWT authentication service with claims and policy requirements and handlers.

NetCoreLearning.Infrastructure.EventBus.Database
   Implements an event database that maintaince the event states and it is used for atomicity between an API action and an event Publish action.
NetCoreLearning.Infrastructure.EventBus.NServiceBus
  Implements an event utilizing NServiceBus nuget package with learning transport and persistence.

NetCoreLearning.Infrastructure.Scheduling.NCrontab:
  Implements a generic scheduler worker utilizing NCronTab nuget package.
