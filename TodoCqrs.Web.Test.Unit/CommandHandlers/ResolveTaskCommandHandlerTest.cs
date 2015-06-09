using Moq;
using NUnit.Framework;
using TodoCqrs.Web.CommandHandlers;
using TodoCqrs.Web.Commands;
using TodoCqrs.Web.Data;
using TodoCqrs.Web.Models;

namespace TodoCqrs.Web.Test.Unit.CommandHandlers
{
    [TestFixture]
    public class ResolveTaskCommandHandlerTest
    {
        private ResolveTaskCommandHandler handler;
        private Mock<IRepository> repository;
        private Task task;

        [SetUp]
        public void SetUp()
        {
            repository = new Mock<IRepository>();
            handler = new ResolveTaskCommandHandler(repository.Object);

            task = new Task();
            repository.Setup(r => r.GetById<Task>(It.IsAny<int>())).Returns(task);
        }

        [Test]
        public void Should_get_task_from_repository()
        {
            var command = new ResolveTaskCommand();
            handler.Handle(command);

            repository.Verify(r => r.GetById<Task>(command.Id));
        }

        [Test]
        public void Should_set_task_to_resolved()
        {
            handler.Handle(new ResolveTaskCommand());
            Assert.IsTrue(task.Resolved);
        }
    }
}