namespace Workflow.Models
{
    public class Route : Entity
    {
        public string FromState { get; set; }

        public string ToState { get; set; }

        public string Action { get; set; }
    }
}