using System;
using System.Collections.Generic;
using System.Linq;

using Z.Data.Models;

using Z.Workflows.Exceptions;
using Z.Workflows.Interfaces;
using Z.Workflows.Models;

using static Z.Data.Tools.Tools;

namespace Z.Workflows.Components
{
    public class WorkflowComponent : IWorkflowComponent
    {
        public IWorkflowUnitOfWork Context { get; private set; }

        public WorkflowComponent(IWorkflowUnitOfWork context)
        {
            Require(Context = context, "WorkflowUnitOfWork");
        }

        public void Start(Workflow workflow, Entity entity)
        {
            var initialState = workflow.GetInitialState();

            // todo: check for existing item

            var item = new Item();
            item.WorkflowId = workflow.Id;
            item.StateId = initialState.Id;
            item.EntityId = entity.Id;
            // todo: item.Date = ???
            item.State = initialState.Name;

            Context.Items.Add(item);

            AddHistory(item);

            // Context.SaveChanges();
        }

        public void Action(Workflow workflow, Entity entity, string action, DateTime? triggerDate = null)
        {
            var item = GetItemByEntityId(entity.Id);
            var nextState = workflow.GetNextState(item.State, action);

            item.State = nextState.Name;
            item.TriggerDate = triggerDate ?? DateTime.MaxValue;

            if (nextState.IsFinalState)
            {
                Context.Items.Delete(item);
            }
            else
            {
                Context.Items.Update(item);
            }

            AddHistory(item);

            // Context.SaveChanges();
        }

        private Item GetItemByEntityId(Guid entityId)
        {
            try
            {
                var item = Context.Items
                    .Query()
                    .Single(i => i.EntityId == entityId);

                return item;
            }
            catch
            {
                throw new ItemNotFoundException();
            }
        }

        private void AddHistory(Item item)
        {
            var history = new History();
            history.ItemId = item.Id;
            history.EntityId = item.EntityId;
            history.State = item.State;
            history.TimeStamp = DateTime.UtcNow;

            Context.History.Add(history);
        }

        public int ItemsInState(string state)
        {
            return Context.Items
                .Query()
                .Count(i => i.State == state);
        }

        public IEnumerable<Workflow> GetWorkflows()
        {
            return Context.Workflows
                .Query()
                .ToList();
        }

        public Workflow GetWorkflow(string name)
        {
            return Context.Workflows
                .Query()
                .First(w => w.Name == name);
        }

        public IEnumerable<Summary> GetSummary(Guid workflowId, DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<History> GetItemHistory(Guid itemId)
        {
            return Context.History
                .Query()
                .Where(h => h.ItemId == itemId)
                .OrderBy(h => h.TimeStamp).ToList();
        }
    }
}