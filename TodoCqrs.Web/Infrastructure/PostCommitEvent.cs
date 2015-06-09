using System;

namespace TodoCqrs.Web.Infrastructure
{
    public class PostCommitEvent : IPostCommitEvent
    {
        public event Action PostCommit = () => { };

        public void Raise()
        {
            PostCommit();
        }

        public void Reset()
        {
            PostCommit = () => { };
        }
    }
}