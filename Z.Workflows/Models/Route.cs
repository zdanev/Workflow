using Z.Data.Models;

namespace Z.Workflows.Models
{
    public class Route : Entity
    {
        public string FromState { get; set; }

        public string ToState { get; set; }

        public string Action { get; set; }
    }
}