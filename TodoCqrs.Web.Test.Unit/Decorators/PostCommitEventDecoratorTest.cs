using System;
using Moq;
using NUnit.Framework;
using TodoCqrs.Web.CommandHandlers;
using TodoCqrs.Web.Decorators;
using TodoCqrs.Web.Infrastructure;

namespace TodoCqrs.Web.Test.Unit.Decorators
{
    [TestFixture]
    public class PostCommitEventDecoratorTest
    {
        private Mock<ICommandHandler<object>> decoratedCommand;
        private PostCommitEventDecorator<object> decorator;
        private PostCommitEvent postCommitEvent;

        [SetUp]
        public void SetUp()
        {
            decoratedCommand = new Mock<ICommandHandler<object>>();
            postCommitEvent = new PostCommitEvent();
            decorator = new PostCommitEventDecorator<object>(decoratedCommand.Object, postCommitEvent);
        }

        [Test]
        public void Should_handle_decorated_command()
        {
            var command = new object();
            decorator.Handle(command);

            decoratedCommand.Verify(cmd => cmd.Handle(command));
        }

        [Test]
        public void Should_raise_post_commit_event()
        {
            bool postCommitEventHandled = false;
            postCommitEvent.PostCommit += () => postCommitEventHandled = true;

            decorator.Handle(new object());

            Assert.IsTrue(postCommitEventHandled);
        }

        [Test]
        public void Should_clear_registered_events_for_garbage_collection()
        {
            int callCount = 0;
            postCommitEvent.PostCommit += () => { callCount++; };

            decorator.Handle(new object());

            postCommitEvent.Raise();
            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Should_clear_registered_events_for_garbage_collection_when_eventhandler_throws_exception()
        {
            postCommitEvent.PostCommit += () => { throw new Exception(); };
            Assert.Throws<Exception>(() => decorator.Handle(new object()));
            postCommitEvent.Raise();
        }
    }
}