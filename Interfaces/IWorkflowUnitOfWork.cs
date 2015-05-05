using Workflow.Models;

namespace Workflow.Interfaces
{
    public interface IWorkflowUnitOfWork : IUnitOfWork
    {
        IRepository<Item> Items { get; }

        IRepository<History> History { get; }
    }
}