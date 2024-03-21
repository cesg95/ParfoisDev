namespace Data.Services.Tests.Implementations
{
    using AutoFixture;

    using Data.Repository.Interfaces;
    using Data.Services.Implementations;

    using Domain.Model;
    using Domain.Model.Requests;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestFixture]
    public class PedidoServiceTests
    {
        private Mock<IPedidoRepository> mockPedidoRepository;
        private PedidoService service;
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            this.mockPedidoRepository = new Mock<IPedidoRepository>();

            this.service = new PedidoService(this.mockPedidoRepository.Object);

            this.fixture = new Fixture();
        }

        [Test]
        public async Task GetAllAsync_NoData_EmptyList()
        {
            // Arrange
            this.mockPedidoRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Pedido>());

            // Act
            var result = await this.service.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());

            this.mockPedidoRepository
                .Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_Entries_ListWithEntities()
        {
            // Arrange
            var expected = this.fixture.CreateMany<Pedido>();

            this.mockPedidoRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(expected);

            // Act
            var result = await this.service.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());

            this.mockPedidoRepository
                .Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetByIdAsync_NotFound_Null()
        {
            // Arrange
            var id = 1;

            this.mockPedidoRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Pedido);

            // Act
            var result = await this.service.GetByIdAsync(id);

            // Assert
            Assert.IsNull(result);

            this.mockPedidoRepository
                .Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Test]
        public async Task GetByIdAsync_OneEntry_OnePedido()
        {
            // Arrange
            var id = 1;
            var expected = this.fixture.Create<Pedido>();

            this.mockPedidoRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expected);

            // Act
            var result = await this.service.GetByIdAsync(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Id, result.Id);

            this.mockPedidoRepository
                .Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Test]
        public async Task CreateAsync_Success_PedidoCreated()
        {
            // Arrange
            var request = this.fixture.Create<PedidoRequest>();
            var expected = this.fixture.Create<Pedido>();

            this.mockPedidoRepository
                .Setup(x => x.CreateAsync(It.IsAny<PedidoRequest>()))
                .ReturnsAsync(expected);

            // Act
            var result = await this.service.CreateAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Id, result.Id);

            this.mockPedidoRepository
                .Verify(x => x.CreateAsync(It.IsAny<PedidoRequest>()), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_Success_PedidoUpdated()
        {
            // Arrange
            var request = this.fixture.Create<Pedido>();

            this.mockPedidoRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Pedido>()))
                .Returns(Task.CompletedTask);

            // Act
            await this.service.UpdateAsync(request);

            // Assert
            this.mockPedidoRepository
                .Verify(x => x.UpdateAsync(It.IsAny<Pedido>()), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_Success_PedidoDeleted()
        {
            // Arrange
            var request = this.fixture.Create<Pedido>();

            this.mockPedidoRepository
                .Setup(x => x.DeleteAsync(It.IsAny<Pedido>()))
                .Returns(Task.CompletedTask);

            // Act
            await this.service.DeleteAsync(request);

            // Assert
            this.mockPedidoRepository
                .Verify(x => x.DeleteAsync(It.IsAny<Pedido>()), Times.Once);
        }
    }
}