using Moq;
using NUnit.Framework;
using TodoCqrs.Web.CommandHandlers;
using TodoCqrs.Web.Commands;
using TodoCqrs.Web.Data;
using TodoCqrs.Web.Infrastructure;
using TodoCqrs.Web.Models;

namespace TodoCqrs.Web.Test.Unit.CommandHandlers
{
    [TestFixture]
    public class AddNewTaskCommandHandlerTest
    {
        private AddNewTaskCommandHandler handler;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IPostCommitEvent> postCommitEvent;

        [SetUp]
        public void SetUp()
        {
            postCommitEvent = new Mock<IPostCommitEvent>();
            unitOfWork = new Mock<IUnitOfWork>();
            handler = new AddNewTaskCommandHandler(unitOfWork.Object, postCommitEvent.Object);
        }

        [Test]
        public void Should_add_new_task_to_unit_of_work()
        {
            handler.Handle(
                new AddNewTaskCommand
                {
                    Text = "test"
                });

            unitOfWork.Verify(uow => uow.Add(It.Is<Task>(t => t.Text == "test")));
        }

        [Test]
        public void Should_set_created_task_id_in_post_commit_event()
        {
            unitOfWork.Setup(uow => uow.Add(It.IsAny<Task>()))
                .Callback((Task t) => t.Id = 123);

            var command = new AddNewTaskCommand();
            handler.Handle(command);

            Assert.AreEqual(0, command.CreatedTaskId, "id should not be set before event is raised");
            postCommitEvent.Raise(p => p.PostCommit += null);

            Assert.AreEqual(123, command.CreatedTaskId, "id should be 123 after event");
        }
    }
}