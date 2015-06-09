using NUnit.Framework;
using SimpleInjector;
using TodoCqrs.Web.Bus;
using TodoCqrs.Web.QueryHandlers;

namespace TodoCqrs.Web.Test.Unit.Bus
{
    [TestFixture]
    public class QueryBusTest
    {
        private class TestQuery : IQuery<TestQueryResult>
        {
        }

        private class TestQueryResult
        {
            public bool QueryHandled { get; set; }
        }

        // ReSharper disable once ClassNeverInstantiated.Local - Simple Injector instantiates this test stub
        private class TestQueryHandler : IQueryHandler<TestQuery, TestQueryResult>
        {
            public TestQueryResult Handle(TestQuery query)
            {
                return new TestQueryResult{QueryHandled = true};
            }
        }

        [Test]
        public void Should_handle_query_with_correct_query_handler()
        {
            var container = new Container();
            container.Register<IQueryHandler<TestQuery, TestQueryResult>, TestQueryHandler>();

            var queryBus = new QueryBus(container);
            var result = queryBus.Query<TestQuery, TestQueryResult>(new TestQuery());

            Assert.IsTrue(result.QueryHandled);
        }
    }
}