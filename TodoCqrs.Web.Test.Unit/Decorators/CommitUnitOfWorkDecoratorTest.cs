using Moq;
using NUnit.Framework;
using TodoCqrs.Web.CommandHandlers;
using TodoCqrs.Web.Data;
using TodoCqrs.Web.Decorators;

namespace TodoCqrs.Web.Test.Unit.Decorators
{
    [TestFixture]
    public class CommitUnitOfWorkDecoratorTest
    {
        private object command;
        private Mock<ICommandHandler<object>> decoratedCommand;
        private Mock<IUnitOfWork> unitOfWork;

        [SetUp]
        public void SetUp()
        {
            decoratedCommand = new Mock<ICommandHandler<object>>();
            unitOfWork = new Mock<IUnitOfWork>();

            var decorator = new CommitUnitOfWorkDecorator<object>(decoratedCommand.Object, unitOfWork.Object);
            
            command = new object();
            decorator.Handle(command);
        }

        [Test]
        public void Should_handle_command()
        {
            decoratedCommand.Verify(dc => dc.Handle(command));
        }

        [Test]
        public void Should_commit_unit_of_work()
        {
            unitOfWork.Verify(uow => uow.Commit());
        }
    }
}