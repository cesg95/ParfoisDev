namespace Data.Repository.Tests.Implementations
{
    using AutoFixture;

    using Data.Repository.Implementations;

    using Domain.Model;
    using Domain.Model.Requests;

    [TestFixture]
    public class PedidoRepositoryTests
    {
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            this.fixture = new Fixture();
        }

        [Test]
        public async Task GetAllAsync_NoData_EmptyList()
        {
            // Arrange
            var context = new ApiContext();
            var repository = new PedidoRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [Test]
        public async Task GetAllAsync_HasPedidos_ListWithPedidos()
        {
            // Arrange
            var context = new ApiContext();
            var repository = new PedidoRepository(context);

            var pedido = this.fixture.Create<Pedido>();
            context.Pedidos.Add(pedido);
            context.SaveChanges();

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(pedido.Id, result.First().Id);
            Assert.AreEqual(pedido.Itens.Count, result.First().Itens.Count);

            context.Pedidos.RemoveRange(context.Pedidos);
            context.SaveChanges();
        }

        [Test]
        public async Task GetByIdAsync_NoData_Null()
        {
            // Arrange
            var context = new ApiContext();
            var repository = new PedidoRepository(context);

            // Act
            var result = await repository.GetByIdAsync(1);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetByIdAsync_HasPedido_Pedido()
        {
            // Arrange
            var context = new ApiContext();
            var repository = new PedidoRepository(context);

            var pedido = this.fixture.Create<Pedido>();
            context.Pedidos.Add(pedido);
            context.SaveChanges();

            // Act
            var result = await repository.GetByIdAsync(pedido.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(pedido.Id, result.Id);
            Assert.AreEqual(pedido.Itens.Count, result.Itens.Count);

            context.Pedidos.RemoveRange(context.Pedidos);
            context.SaveChanges();
        }

        [Test]
        public async Task CreateAsync_CreatePedido_Success()
        {
            // Arrange
            var context = new ApiContext();
            var repository = new PedidoRepository(context);

            var pedido = this.fixture.Create<PedidoRequest>();

            // Act
            var result = await repository.CreateAsync(pedido);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.Pedidos.Count());
            Assert.AreEqual(context.Pedidos.First().Id, result.Id);
            Assert.AreEqual(context.Pedidos.First().Itens.Count, result.Itens.Count);

            context.Pedidos.RemoveRange(context.Pedidos);
            context.SaveChanges();
        }

        [Test]
        public async Task DeleteAsync_RemovePedido_Success()
        {
            // Arrange
            var context = new ApiContext();
            var repository = new PedidoRepository(context);

            var pedido = this.fixture.Create<Pedido>();
            context.Pedidos.Add(pedido);
            context.SaveChanges();

            // Act
            await repository.DeleteAsync(pedido);
            var result = await repository.GetByIdAsync(pedido.Id);

            // Assert
            Assert.IsNull(result);
        }
    }
}