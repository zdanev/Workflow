using Z.Data.Models;

namespace Z.Workflows.Models
{
    public class Route : Entity
    {
        // public Guid FromState { get; set; }

        // public Guid ToState { get; set; }

        public string FromState { get; set; }

        public string ToState { get; set; }

        public string Action { get; set; }
    }
}