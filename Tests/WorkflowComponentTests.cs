using Microsoft.VisualStudio.TestTools.UnitTesting;

using Workflow.Components;
using Workflow.Data;
using Workflow.Interfaces;
using Workflow.Models;

namespace Workflow.Tests
{
    [TestClass]
    public class WorkflowComponentTests
    {
        private IRepository<Item> _items;
        private IRepository<History> _history; 
        private IWorkflowComponent _workflow;

        [TestInitialize]
        public void Init()
        {
            _items = new Repository<Item>();
            _history = new Repository<History>();
            _workflow = new WorkflowComponent(_items, _history);
            _workflow.States.Add(new State {Name = "state1", IsInitialState = true});
            _workflow.States.Add(new State {Name = "state2"});
            _workflow.States.Add(new State {Name = "state3", IsFinalState = true});
            _workflow.Routes.Add(new Route { FromState = "state1", ToState = "state2", Action = "GO"});
            _workflow.Routes.Add(new Route { FromState = "state2", ToState = "state3", Action = "GO"});
        }

        [TestMethod]
        public void WorkflowComponent_InitialState()
        {
            // arrange
            var item = new Entity();

            // act
            _workflow.Start(item);

            // assert
            Assert.AreEqual(1, _items.Count());
            Assert.AreEqual(1, _history.Count());
            Assert.AreEqual(1, _workflow.ItemsInState("state1"));
            Assert.AreEqual(0, _workflow.ItemsInState("state2"));
            Assert.AreEqual(0, _workflow.ItemsInState("state3"));
        }

        [TestMethod]
        public void WorkflowComponent_Route()
        {
            // arrange
            var item = new Entity();

            // act
            _workflow.Start(item);
            _workflow.Action(item, "GO");

            // assert
            Assert.AreEqual(1, _items.Count());
            Assert.AreEqual(2, _history.Count());
            Assert.AreEqual(0, _workflow.ItemsInState("state1"));
            Assert.AreEqual(1, _workflow.ItemsInState("state2"));
            Assert.AreEqual(0, _workflow.ItemsInState("state3"));
        }

        [TestMethod]
        public void WorkflowComponent_FinalState()
        {
            // arrange
            var item = new Entity();

            // act
            _workflow.Start(item);
            _workflow.Action(item, "GO");
            _workflow.Action(item, "GO");

            // assert
            Assert.AreEqual(0, _items.Count());
            Assert.AreEqual(3, _history.Count());
            Assert.AreEqual(0, _workflow.ItemsInState("state1"));
            Assert.AreEqual(0, _workflow.ItemsInState("state2"));
            Assert.AreEqual(0, _workflow.ItemsInState("state3"));
        }
    }
}