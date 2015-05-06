using System;

namespace Z.Workflows.Exceptions
{
    public class ActionNotFoundException : Exception
    {
        public string State { get; private set; }

        public string Action { get; private set; }

        public ActionNotFoundException(string state, string action) : base("Action  " + action + " not found for state " + state + ".")
        {
            State = state;
            Action = action;
        }
    }
}