using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TodoCqrs.Web.Data;
using TodoCqrs.Web.Models;
using TodoCqrs.Web.Queries;
using TodoCqrs.Web.QueryHandlers;

namespace TodoCqrs.Web.Test.Unit.QueryHandlers
{
    [TestFixture]
    public class ResolvedTasksQueryHandlerTest
    {
        private List<Task> tasks;

        [Test]
        public void Should_query_resolved_tasks()
        {
            var repository = new Mock<IRepository>();
            tasks = new List<Task>
            {
                new Task { Resolved = true },
                new Task(),
                new Task { Resolved = true }
            };
            repository.Setup(r => r.Query<Task>()).Returns(tasks.AsQueryable());

            var handler = new ResolvedTasksQueryHandler(repository.Object);

            IEnumerable<Task> result = handler.Handle(new ResolvedTasksQuery());

            IEnumerable<Task> expected = new[] {tasks[0], tasks[2]};
            Assert.IsTrue(result.SequenceEqual(expected));
        }
    }
}