using System;
using System.Collections.Generic;
using System.Linq;

using Workflow.Interfaces;
using Workflow.Models;

namespace Workflow.Fakes
{
    public class FakeRepository<T> : IRepository<T> where T : Entity
    {
        private readonly List<T> _list = new List<T>();

        public void Add(T entity)
        {
            _list.Add(entity);
        }

        public IQueryable<T> Query()
        {
            return _list.AsQueryable();
        }

        public T Get(Guid id)
        {
            return _list.Single(e => e.Id == id);
        }

        public void Update(T entity)
        {
            var original = _list.Single(e => e.Id == entity.Id);

            foreach (var pi in typeof (T).GetProperties())
            {
                var value = pi.GetValue(entity);
                pi.SetValue(original, value);
            }
        }

        public void Delete(T entity)
        {
            _list.Remove(entity);
        }

        public int Count()
        {
            return _list.Count;
        }
    }
}