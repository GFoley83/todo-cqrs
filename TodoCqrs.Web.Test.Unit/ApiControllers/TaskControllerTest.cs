using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TodoCqrs.Web.ApiControllers;
using TodoCqrs.Web.Bus;
using TodoCqrs.Web.Commands;
using TodoCqrs.Web.Models;
using TodoCqrs.Web.Queries;

namespace TodoCqrs.Web.Test.Unit.ApiControllers
{
    [TestFixture]
    public class TaskControllerTest
    {
        private Mock<ICommandBus> commandBus;
        private Mock<IQueryBus> queryBus;
        private TaskController taskController;

        [SetUp]
        public void SetUp()
        {
            queryBus = new Mock<IQueryBus>();
            commandBus = new Mock<ICommandBus>();
            taskController = new TaskController(queryBus.Object, commandBus.Object);
        }

        [Test]
        public void GetOpen_should_run_all_tasks_query()
        {
            IEnumerable<Task> expected = new List<Task>();
            queryBus.Setup(qb => qb.Query<OpenTasksQuery, IEnumerable<Task>>(It.IsAny<OpenTasksQuery>()))
                .Returns(expected);

            IEnumerable<Task> actual = taskController.GetOpen();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Add_should_run_add_new_task_command()
        {
            var command = new AddNewTaskCommand();
            taskController.Add(command);

            commandBus.Verify(cb => cb.Run(command));
        }

        [Test]
        public void Add_should_return_created_task_id()
        {
            var command = new AddNewTaskCommand
            {
                CreatedTaskId = 123
            };

            CreatedTaskResult result = taskController.Add(command);
            Assert.AreEqual(123, result.Id);
        }

        [Test]
        public void Get_should_run_GetTaskQuery()
        {
            var expected = new Task();
            var query = new GetTaskByIdQuery();
            queryBus.Setup(qb => qb.Query<GetTaskByIdQuery, Task>(query))
                .Returns(expected);

            Task actual = taskController.Get(query);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Delete_should_run_delete_task_command()
        {
            var command = new DeleteTaskCommand();
            taskController.Delete(command);

            commandBus.Verify(cb => cb.Run(command));
        }

        [Test]
        public void Resolve_should_run_resolve_task_command()
        {
            var command = new ResolveTaskCommand();
            taskController.Resolve(command);

            commandBus.Verify(cb => cb.Run(command));
        }

        [Test]
        public void GetResolved_should_run_resolved_tasks_query()
        {
            var expected = new List<Task>();
            queryBus.Setup(qb => qb.Query<ResolvedTasksQuery, IEnumerable<Task>>(It.IsAny<ResolvedTasksQuery>()))
                .Returns(expected);

            IEnumerable<Task> actual = taskController.GetResolved();

            Assert.AreEqual(expected, actual);
        }
    }
}