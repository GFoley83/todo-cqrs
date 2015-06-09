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
    public class OpenTasksQueryHandlerTest
    {
        [Test]
        public void Should_return_open_tasks()
        {
            var tasks = new List<Task>
            {
                new Task{ Resolved = false },
                new Task{ Resolved = true },
                new Task{ Resolved = false },
            };

            var repository = new Mock<IRepository>();
            repository.Setup(r => r.Query<Task>()).Returns(tasks.AsQueryable());

            var handler = new OpenTasksQueryHandler(repository.Object);
            var actual = handler.Handle(new OpenTasksQuery());

            var expected = new[] {tasks[0], tasks[2]};
            Assert.IsTrue(actual.SequenceEqual(expected));
        }
    }
}