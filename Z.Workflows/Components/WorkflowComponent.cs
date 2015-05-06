using System;
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
        private IWorkflowUnitOfWork _uow;

        public WorkflowComponent(IWorkflowUnitOfWork uow)
        {
            Require(_uow = uow, "WorkflowUnitOfWork");
        }

        public void Start(Workflow workflow, Entity entity)
        {
            var stateInitialState = workflow.GetInitialState();

            var item = new Item();
            item.EntityId = entity.Id;
            item.State = stateInitialState.Name;

            _uow.Items.Add(item);

            AddHistory(item);
        }

        public void Action(Workflow workflow, Entity entity, string action)
        {
            var item = GetItemByEntityId(entity.Id);
            var nextState = workflow.GetNextState(item.State, action);

            item.State = nextState.Name;

            if (nextState.IsFinalState)
            {
                _uow.Items.Delete(item);
            }
            else
            {
                _uow.Items.Update(item);
            }

            AddHistory(item);
        }

        private Item GetItemByEntityId(Guid entityId)
        {
            try
            {
                var item = _uow.Items.Query().Single(i => i.EntityId == entityId);

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

            _uow.History.Add(history);
        }

        public int ItemsInState(string state)
        {
            return _uow.Items.Query().Count(i => i.State == state);
        }
    }
}