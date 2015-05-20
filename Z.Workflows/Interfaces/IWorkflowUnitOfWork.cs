using Z.Data.Interfaces;
using Z.Workflows.Models;

namespace Z.Workflows.Interfaces
{
    public interface IWorkflowUnitOfWork : IUnitOfWork
    {
        IRepository<Workflow> Workflows { get; }

        IRepository<Item> Items { get; }

        IRepository<History> History { get; }
    }
}