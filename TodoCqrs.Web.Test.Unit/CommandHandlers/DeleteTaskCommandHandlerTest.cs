using Moq;
using NUnit.Framework;
using TodoCqrs.Web.CommandHandlers;
using TodoCqrs.Web.Commands;
using TodoCqrs.Web.Data;
using TodoCqrs.Web.Models;

namespace TodoCqrs.Web.Test.Unit.CommandHandlers
{
    [TestFixture]
    public class DeleteTaskCommandHandlerTest
    {
        private DeleteTaskCommandHandler handler;
        private Mock<IRepository> repository;
        private Mock<IUnitOfWork> unitOfWork;

        [SetUp]
        public void SetUp()
        {
            repository = new Mock<IRepository>();
            unitOfWork = new Mock<IUnitOfWork>();
            handler = new DeleteTaskCommandHandler(repository.Object, unitOfWork.Object);
        }

        [Test]
        public void Should_get_task_by_id()
        {
            var command = new DeleteTaskCommand();
            handler.Handle(command);

            repository.Verify(r => r.GetById<Task>(command.Id));
        }

        [Test]
        public void Should_delete_found_task()
        {
            var task = new Task();
            repository.Setup(r => r.GetById<Task>(It.IsAny<int>())).Returns(task);

            handler.Handle(new DeleteTaskCommand());

            unitOfWork.Verify(uow => uow.Delete(task));
        }
    }
}