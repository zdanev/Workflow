using Z.Data.Interfaces;
using Z.Workflows.Models;

namespace Z.Workflows.Interfaces
{
    public interface IWorkflowUnitOfWork : IUnitOfWork
    {
        IRepository<Item> Items { get; }

        IRepository<History> History { get; }
    }
}