using System;
using System.Linq;

using Workflow.Models;

namespace Workflow.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);

        IQueryable<T> Query();

        T Get(Guid id);

        void Update(T entity);

        void Delete(T entity);

        int Count();
    }
}