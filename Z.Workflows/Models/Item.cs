using System;

using Z.Data.Models;

namespace Z.Workflows.Models
{
    public class Item : Entity
    {
        public string State { get; set; }

        public Guid EntityId { get; set; }
    }
}