using Z.Data.Interfaces;

namespace Z.Data.Fakes
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public int SaveChanges()
        {
            return 0;
        }
    }
}