using NUnit.Framework;
using SimpleInjector;
using TodoCqrs.Web.Bus;
using TodoCqrs.Web.CommandHandlers;

namespace TodoCqrs.Web.Test.Unit.Bus
{
    [TestFixture]
    public class CommandBusTest
    {
        // ReSharper disable once ClassNeverInstantiated.Local - Simple Injector instantiates this test stub
        private class TestCommandHandler : ICommandHandler<TestCommand>
        {
            public void Handle(TestCommand command)
            {
                command.Handled = true;
            }
        }

        private class TestCommand
        {
            public bool Handled { get; set; }
        }

        [Test]
        public void Should_handle_command_with_correct_handler()
        {
            var container = new Container();
            container.Register<ICommandHandler<TestCommand>, TestCommandHandler>();

            var commandBus = new CommandBus(container);
            var command = new TestCommand();
            commandBus.Run(command);

            Assert.IsTrue(command.Handled);
        }
    }
}