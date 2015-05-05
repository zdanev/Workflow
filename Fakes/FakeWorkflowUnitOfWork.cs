using Workflow.Interfaces;
using Workflow.Models;

namespace Workflow.Fakes
{
    public class FakeWorkflowUnitOfWork : FakeUnitOfWork, IWorkflowUnitOfWork
    {
        private readonly IRepository<Item> _items = new FakeRepository<Item>();
        private readonly IRepository<History> _history = new FakeRepository<History>();

        public IRepository<Item> Items { get { return _items; } }

        public IRepository<History> History { get { return _history; } }
    }
}