using System.Collections.Generic;

using Workflow.Models;

namespace Workflow.Interfaces
{
    public interface IWorkflowComponent
    {
        void Start(Entity entity);

        void Action(Entity entity, string action);

        IList<State> States { get; }

        IList<Route> Routes { get; }

        int ItemsInState(string state);
    }
}