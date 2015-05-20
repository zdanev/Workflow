namespace Z.Workflows.Interfaces
{
    public interface IWorkflowConfiguration
    {
        void Seed(IWorkflowUnitOfWork uow);
    }
}