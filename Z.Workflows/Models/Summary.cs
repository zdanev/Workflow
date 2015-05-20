using System;

namespace Z.Workflows.Models
{
    public class Summary
    {
        // public Guid WorkflowId { get; set; }

        // public string WorkflowName { get; set; }

        public Guid StateId { get; set; }

        public string StateName { get; set; }

        public DateTime Date { get; set; }

        public int Count { get; set; }
    }
}