using System;
using System.Collections.Generic;
using System.Linq;

using Workflow.Interfaces;
using Workflow.Models;

using static Workflow.Tools.Tools;

namespace Workflow.Components
{
    public class WorkflowComponent : IWorkflowComponent
    {
        private IWorkflowUnitOfWork _uow;

        public IList<State> States { get; set; }

        public IList<Route> Routes { get; set; } 

        public WorkflowComponent(IWorkflowUnitOfWork uow)
        {
            Require(_uow = uow, "WorkflowUnitOfWork");

            States = new List<State>();
            Routes = new List<Route>();
        }

        public void Start(Entity entity)
        {
            var state = States.Single(t => t.IsInitialState);

            var item = new Item();
            item.EntityId = entity.Id;
            item.State = state.Name;

            _uow.Items.Add(item);

            AddHistory(item);
        }

        public void Action(Entity entity, string action)
        {
            var item = _uow.Items.Query().Single(i => i.EntityId == entity.Id);
            var route = Routes.Single(r => r.FromState == item.State && r.Action == action);
            var state = States.Single(s => s.Name == route.ToState);

            item.State = state.Name;

            if (state.IsFinalState)
            {
                _uow.Items.Delete(item);
            }
            else
            {
                _uow.Items.Update(item);
            }

            AddHistory(item);
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