using System;

using Z.Data.Models;

namespace Z.Workflows.Models
{
    public class Item : Entity
    {
        public Guid WorkflowId { get; set; }

        public Guid StateId { get; set; }

        public Guid EntityId { get; set; }

        public DateTime Date { get; set; }

        public string State { get; set; } // todo: remove

        public DateTime TriggerDate { get; set; }
    }
}