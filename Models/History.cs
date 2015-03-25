using System;

namespace Workflow.Models
{
    public class History : Entity
    {
        public Guid ItemId { get; set; }

        public Guid EntityId { get; set; }

        public string State { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}