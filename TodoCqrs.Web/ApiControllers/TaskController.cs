using System.Collections.Generic;
using System.Web.Http;
using TodoCqrs.Web.Bus;
using TodoCqrs.Web.Commands;
using TodoCqrs.Web.Models;
using TodoCqrs.Web.Queries;

namespace TodoCqrs.Web.ApiControllers
{
    [RoutePrefix("api/task")]
    public class TaskController : ApiController
    {
        private readonly IQueryBus queryBus;
        private readonly ICommandBus commandBus;

        public TaskController(IQueryBus queryBus, ICommandBus commandBus)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Task> GetOpen()
        {
            return queryBus.Query<OpenTasksQuery, IEnumerable<Task>>(new OpenTasksQuery());
        }

        [HttpGet]
        [Route("{id}")]
        public Task Get([FromUri]GetTaskByIdQuery query)
        {
            return queryBus.Query<GetTaskByIdQuery, Task>(query);
        }

        [HttpPost]
        [Route("")]
        public CreatedTaskResult Add(AddNewTaskCommand command)
        {
            commandBus.Run(command);
            return new CreatedTaskResult
            {
                Id = command.CreatedTaskId
            };
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete([FromUri]DeleteTaskCommand command)
        {
            commandBus.Run(command);
        }

        [HttpPut]
        [Route("resolved/{id}")]
        public void Resolve([FromUri]ResolveTaskCommand command)
        {
            commandBus.Run(command);
        }

        [HttpGet]
        [Route("resolved")]
        public IEnumerable<Task> GetResolved()
        {
            return queryBus.Query<ResolvedTasksQuery, IEnumerable<Task>>(new ResolvedTasksQuery());
        }
    }
}