using System;

namespace Workflow.Models
{
    public class Item : Entity
    {
        public string State { get; set; }

        public Guid EntityId { get; set; }
    }
}