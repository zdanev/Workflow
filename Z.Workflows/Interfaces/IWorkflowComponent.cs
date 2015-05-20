using System;
using System.Collections.Generic;

using Z.Data.Models;
using Z.Workflows.Models;

namespace Z.Workflows.Interfaces
{
    public interface IWorkflowComponent
    {
        void Start(Workflow workflow, Entity entity);

        void Action(Workflow workflow, Entity entity, string action, DateTime? triggerDate = null);

        int ItemsInState(string state); // todo: remove

        IEnumerable<Workflow> GetWorkflows();

        Workflow GetWorkflow(string name);

        IEnumerable<Summary> GetSummary(Guid workflowId, DateTime fromDate, DateTime toDate);

        IEnumerable<History> GetItemHistory(Guid itemId);
    }
}