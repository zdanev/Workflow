using Microsoft.VisualStudio.TestTools.UnitTesting;

using Z.Data.Models;

using Z.Workflows.Components;
using Z.Workflows.Fakes;
using Z.Workflows.Interfaces;
using Z.Workflows.Models;

namespace Z.Workflows.Tests
{
    [TestClass]
    public class WorkflowComponentTests
    {
        private IWorkflowUnitOfWork _uow;
        private IWorkflowComponent _workflowComponent;
        private Workflow _workflow;

        [TestInitialize]
        public void Init()
        {
            _uow = new FakeWorkflowUnitOfWork();
            _workflowComponent = new WorkflowComponent(_uow);
            _workflow = new Workflow();

            _workflow.States.Add(new State { Name = "state1", IsInitialState = true });
            _workflow.States.Add(new State { Name = "state2" });
            _workflow.States.Add(new State { Name = "state3", IsFinalState = true });
            _workflow.Routes.Add(new Route { FromState = "state1", ToState = "state2", Action = "GO" });
            _workflow.Routes.Add(new Route { FromState = "state2", ToState = "state3", Action = "GO" });
        }

        [TestMethod]
        public void WorkflowComponent_InitialState()
        {
            // arrange
            var item = new Entity();

            // act
            _workflowComponent.Start(_workflow, item);

            // assert
            Assert.AreEqual(1, _uow.Items.Count());
            Assert.AreEqual(1, _uow.History.Count());
            Assert.AreEqual(1, _workflowComponent.ItemsInState("state1"));
            Assert.AreEqual(0, _workflowComponent.ItemsInState("state2"));
            Assert.AreEqual(0, _workflowComponent.ItemsInState("state3"));
        }

        [TestMethod]
        public void WorkflowComponent_Route()
        {
            // arrange
            var item = new Entity();

            // act
            _workflowComponent.Start(_workflow, item);
            _workflowComponent.Action(_workflow, item, "GO");

            // assert
            Assert.AreEqual(1, _uow.Items.Count());
            Assert.AreEqual(2, _uow.History.Count());
            Assert.AreEqual(0, _workflowComponent.ItemsInState("state1"));
            Assert.AreEqual(1, _workflowComponent.ItemsInState("state2"));
            Assert.AreEqual(0, _workflowComponent.ItemsInState("state3"));
        }

        [TestMethod]
        public void WorkflowComponent_FinalState()
        {
            // arrange
            var item = new Entity();

            // act
            _workflowComponent.Start(_workflow, item);
            _workflowComponent.Action(_workflow, item, "GO");
            _workflowComponent.Action(_workflow, item, "GO");

            // assert
            Assert.AreEqual(0, _uow.Items.Count());
            Assert.AreEqual(3, _uow.History.Count());
            Assert.AreEqual(0, _workflowComponent.ItemsInState("state1"));
            Assert.AreEqual(0, _workflowComponent.ItemsInState("state2"));
            Assert.AreEqual(0, _workflowComponent.ItemsInState("state3"));
        }
    }
}