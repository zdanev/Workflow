using System;

namespace Z.Workflows.Exceptions
{
    public class StateNotFoundException : Exception
    {
        public string StateName { get; private set; }

        public StateNotFoundException(string stateName) : base("State " + stateName + " not found.")
        {
            StateName = stateName;
        }
    }
}