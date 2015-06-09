using System;
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
    public class GetTaskByIdQueryHandlerTest
    {
        private GetTaskByIdQueryHandler handler;
        private List<Task> tasks;

        [SetUp]
        public void SetUp()
        {
            var repository = new Mock<IRepository>();
            tasks = new List<Task>
            {
                new Task{ Id = 1},
                new Task{ Id = 2},
                new Task{ Id = 3}
            };
            repository.Setup(r => r.Query<Task>()).Returns(tasks.AsQueryable());

            handler = new GetTaskByIdQueryHandler(repository.Object);
        }

        [Test]
        public void Should_get_task_by_id()
        {
            var actual = handler.Handle(
                new GetTaskByIdQuery
                {
                    Id = 2
                });

            Assert.AreEqual(tasks[1], actual);
        }

        [Test]
        public void Should_throw_exception_when_task_is_not_found()
        {
            Assert.Throws<InvalidOperationException>(() => handler.Handle(
                new GetTaskByIdQuery
                {
                    Id = 56
                }));
        }
    }
}