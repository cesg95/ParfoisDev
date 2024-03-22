namespace Application.Services.Tests.Mappers
{
    using Application.Services.Mappers;

    using AutoFixture;

    using Model = Domain.Model;

    [TestFixture]
    public class PedidoWorkflowMapperTests
    {
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            this.fixture = new Fixture();
        }

        [Test]
        public void ToDto_NullPedidoWorkflow_Null()
        {
            // Arrange
            Model.PedidoWorkflow pedidoWorkflow = null;

            // Act
            var result = pedidoWorkflow.ToDto();

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ToDto_MapPedidoWorkflow_Success()
        {
            // Arrange
            var pedidoWorkflow = this.fixture.Create<Model.PedidoWorkflow>();

            // Act
            var result = pedidoWorkflow.ToDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(pedidoWorkflow.Pedido, result.Pedido);
            Assert.AreEqual(pedidoWorkflow.Status, result.Status);
        }
    }
}