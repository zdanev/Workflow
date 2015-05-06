using Z.Data.Models;

namespace Z.Workflows.Interfaces
{
    public interface IWorkflowComponent
    {
        void Start(Models.Workflow workflow, Entity entity);

        void Action(Models.Workflow workflow, Entity entity, string action);

        int ItemsInState(string state);
    }
}