using System.Data.Entity;
using TodoCqrs.Web.Models;

namespace TodoCqrs.Web.Data
{
    public class TodoDatabaseInitializer : DropCreateDatabaseAlways<TodoDbContext>
    {
        protected override void Seed(TodoDbContext context)
        {
            context.Tasks.AddRange(
                new[]
                {
                    new Task
                    {
                        Text = "Change script bundling to automatically include all scripts."
                    },
                    new Task
                    {
                        Text = "Remove all obsolete MVC template code."
                    },
                    new Task
                    {
                        Text = "Remove need to explicitly specify query type when using query bus."
                    },
                    new Task
                    {
                        Text = "Try to use exiting framework for message bus."
                    },
                    new Task
                    {
                        Text = "Document your design choices to readme page."
                    },
                    new Task
                    {
                        Text = "Implement updating task functionality."
                    },
                    new Task
                    {
                        Text = "Replace serializing entities with DTOs and use automapper to map entities to them."
                    },
                    new Task
                    {
                        Text = "Write acceptance test through UI with selenium and specflow."
                    },
                    new Task
                    {
                        Text = "Implement serverside validation with command handler decorator."
                    }
                });
            context.SaveChanges();

            base.Seed(context);
        }
    }
}