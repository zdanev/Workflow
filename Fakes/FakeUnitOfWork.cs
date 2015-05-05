using Workflow.Interfaces;

namespace Workflow.Fakes
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public int SaveChanges()
        {
            return 0;
        }
    }
}