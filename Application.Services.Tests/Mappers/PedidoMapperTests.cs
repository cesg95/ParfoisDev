namespace Application.Services.Tests.Mappers
{
    using Application.Services.Mappers;

    using AutoFixture;

    using Infrastructure.CrossCutting.ErrorMessages;
    using Infrastructure.CrossCutting.Helpers;

    using Model = Domain.Model;

    [TestFixture]
    public class PedidoMapperTests
    {
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            this.fixture = new Fixture();
        }

        [Test]
        public void ToDto_NullList_EmptyList()
        {
            // Arrange
            IEnumerable<Model.Pedido> pedidos = null;

            // Act
            var result = pedidos.ToDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [Test]
        public void ToDto_ListWithoutPedidos_EmptyList()
        {
            // Arrange
            var pedidos = new List<Model.Pedido>();

            // Act
            var result = pedidos.ToDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [Test]
        public void ToDto_ListWithPedidos_FilledList()
        {
            // Arrange
            var pedidos = this.fixture.CreateMany<Model.Pedido>();

            // Act
            var result = pedidos.ToDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(pedidos.Count(), result.Count());

            for (var i = 0; i < result.Count(); i++)
            {
                var pedido = pedidos.ElementAt(i);
                var resultPedido = result.ElementAt(i);

                Assert.AreEqual(pedido.Id.ToString(), resultPedido.Id);
                Assert.AreEqual(pedido.Itens.Count, resultPedido.Itens.Count());
            }
        }

        [Test]
        public void ToModel_NullPedido_Null()
        {
            // Arrange
            Dto.Pedido pedido = null;

            // Act
            var result = pedido.ToModel();

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ToModel_InvalidPedidoId_Exception()
        {
            // Arrange
            var pedido = this.fixture.Create<Dto.Pedido>();
            pedido.Id = "a";

            try
            {
                // Act
                pedido.ToModel();

                Assert.Fail("Missing InvalidCastException");
            }
            catch (InvalidCastException ex)
            {
                // Assert
                Assert.IsNotNull(ex);
                Assert.AreEqual(BadRequestMessages.InvalidIdentifier.ToMessage(), ex.Message);
            }
        }

        [Test]
        public void ToModel_MapPedido_Success()
        {
            // Arrange
            var pedido = this.fixture.Create<Dto.Pedido>();
            pedido.Id = "1";

            // Act
            var result = pedido.ToModel();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(pedido.Id, result.Id.ToString());
            Assert.AreEqual(pedido.Itens.Count(), result.Itens.Count);
        }
    }
}