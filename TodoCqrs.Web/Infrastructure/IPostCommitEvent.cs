using System;

namespace TodoCqrs.Web.Infrastructure
{
    public interface IPostCommitEvent
    {
        event Action PostCommit;
    }
}