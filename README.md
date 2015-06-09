# todo-cqrs
Demo project of using CQRS with ASP.NET Web API.

## Why to bother with CQRS?
* Clean separation of business logic from presentation tier (MVC/Web API Controllers are part of presentation tier)
* Possibility to use different data stores for reading and writing data
* All system inputs and outputs go through one single interface (bus or mediator) which can be easily extended with decorators to handle cross-cutting concerns (logging, transactions, validation, etc...)
* Commands and queries can be divided to small classes and tested separately (SRP!) instead of using usually bloated service layer classes 

## Tech
* ASP.NET MVC & Web API
* AngularJS
* Simple Injector
* Entity Framework

##  Implementation notes
* This demo does not use two separate data stores and focuses only for separation of queries and commands.
* Serializing EF entities to client instead of DTOs does not work for long term because of relations and virtual proxies will cause problems in serialization -> Queries should return DTOs.
* Using database generated IDs (identity fields) might also cause problems in future.

## Related articles & blogs
* https://lostechies.com/jimmybogard/2013/10/29/put-your-controllers-on-a-diet-gets-and-queries/
* https://lostechies.com/jimmybogard/2013/12/19/put-your-controllers-on-a-diet-posts-and-commands/
* http://codebetter.com/gregyoung/2010/02/16/cqrs-task-based-uis-event-sourcing-agh/
* http://martinfowler.com/bliki/CQRS.html
