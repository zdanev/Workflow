namespace Workflow.Models
{
    public class State
    {
        public string Name { get; set; }

        public bool IsInitialState { get; set; }

        public bool IsFinalState { get; set; }
    }
}