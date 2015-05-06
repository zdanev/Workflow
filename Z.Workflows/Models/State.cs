using Z.Data.Models;

namespace Z.Workflows.Models
{
    public class State : Entity
    {
        public string Name { get; set; }

        public bool IsInitialState { get; set; }

        public bool IsFinalState { get; set; }
    }
}