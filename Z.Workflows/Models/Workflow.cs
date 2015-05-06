using System.Collections.Generic;
using System.Linq;

using Z.Workflows.Exceptions;
using Z.Data.Models;

namespace Z.Workflows.Models
{
    public class Workflow : Entity
    {
        public string Name { get; set; }

        public IList<State> States { get; set; }

        public IList<Route> Routes { get; set; }

        public Workflow()
        {
            States = new List<State>();
            Routes = new List<Route>();
        }

        public State GetInitialState()
        {
            try
            {
                var state = States.Single(t => t.IsInitialState);

                return state;
            }
            catch
            {
                throw new InitialStateNotFoundException();
            }
        }

        public State GetNextState(string fromState, string action)
        {
            var route = FindRoute(fromState, action);

            var state = FindState(route.ToState);

            return state;
        }

        public Route FindRoute(string fromState, string action)
        {
            try
            {
                var route = Routes.Single(r => r.FromState == fromState && r.Action == action);

                return route;
            }
            catch
            {
                throw new ActionNotFoundException(fromState, action);
            }
        }

        public State FindState(string name)
        {
            try
            {
                var state = States.Single(s => s.Name == name);

                return state;
            }
            catch
            {
                throw new StateNotFoundException(name);
            }
        }
    }
}