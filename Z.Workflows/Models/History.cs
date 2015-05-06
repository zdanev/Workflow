using System;

using Z.Data.Models;

namespace Z.Workflows.Models
{
    public class History : Entity
    {
        public Guid ItemId { get; set; }

        public Guid EntityId { get; set; }

        public string State { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}