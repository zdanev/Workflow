using System;

namespace Workflow.Models
{
    public class Entity
    {
        public Guid Id { get; set; }

        public Entity(Guid id)
        {
            Id = id;
        }

        public Entity()
            : this(Guid.NewGuid())
        {

        }
    }
}