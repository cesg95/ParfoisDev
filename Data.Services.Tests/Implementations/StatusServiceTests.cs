namespace Data.Services.Tests.Implementations
{
    using AutoFixture;

    using Data.Services.Implementations;
    using Data.Services.Interfaces;
    using Data.Services.Rules;

    using Domain.Model;
    using Domain.Model.Enums;
    using Domain.Model.Requests;

    using Infrastructure.CrossCutting.Helpers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestFixture]
    public class StatusServiceTests
    {
        private Mock<IPedidoService> mockPedidoService;
        private Mock<IRuleFactory> mockRuleFactory;
        private IRule firstRule;
        private StatusService service;
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            this.mockPedidoService = new Mock<IPedidoService>();
            this.mockRuleFactory = new Mock<IRuleFactory>();

            this.service = new StatusService(
                this.mockPedidoService.Object,
                this.mockRuleFactory.Object);

            this.firstRule = new ReprovadoRule();

            this.firstRule
                .SetNext(new AprovadoRule())
                .SetNext(new ItemsAprovadosAMaiorRule())
                .SetNext(new ItemsAprovadosAMenorRule())
                .SetNext(new ValorAprovadoAMaiorRule())
                .SetNext(new ValorAprovadoAMenorRule());

            this.fixture = new Fixture();
        }

        [Test]
        public async Task GetPedidoWorkflowAsync_PedidoDoesNotExists_CodigoPedidoInvalido()
        {
            // Arrange
            var request = this.fixture.Build<StatusRequest>()
                .With(x => x.Pedido, "1")
                .Create();

            this.mockPedidoService
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Pedido);

            // Act
            var result = await this.service.GetPedidoWorkflowAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(request.Pedido, result.Pedido);
            Assert.AreEqual(1, result.Status.Count);
            Assert.AreEqual(Status.CodigoPedidoInvalido.ToMessage(), result.Status.First());

            this.mockPedidoService
                .Verify(x => x.GetByIdAsync(int.Parse(request.Pedido)), Times.Once);
            this.mockRuleFactory
                .Verify(x => x.GetFirstRule(), Times.Never);
        }

        [Test]
        public async Task GetPedidoWorkflowAsync_PedidoExists_Reprovado()
        {
            // Arrange
            var request = this.fixture.Build<StatusRequest>()
                .With(x => x.Pedido, "1")
                .With(x => x.Status, Status.Reprovado.ToMessage())
                .Create();

            var pedido = this.fixture.Create<Pedido>();

            this.mockPedidoService
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(pedido);
            this.mockRuleFactory
                .Setup(x => x.GetFirstRule())
                .Returns(this.firstRule);

            // Act
            var result = await this.service.GetPedidoWorkflowAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(request.Pedido, result.Pedido);
            Assert.AreEqual(1, result.Status.Count);
            Assert.AreEqual(Status.Reprovado.ToMessage(), result.Status.First());

            this.mockPedidoService
                .Verify(x => x.GetByIdAsync(int.Parse(request.Pedido)), Times.Once);
            this.mockRuleFactory
                .Verify(x => x.GetFirstRule(), Times.Once);
        }

        [Test]
        public async Task GetPedidoWorkflowAsync_PedidoExists_Aprovado()
        {
            // Arrange
            var request = new StatusRequest
            {
                Pedido = "1",
                Status = Status.Aprovado.ToMessage(),
                ItensAprovados = 3,
                ValorAprovado = 20,
            };

            var pedido = this.GetPedido();

            this.mockPedidoService
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(pedido);
            this.mockRuleFactory
                .Setup(x => x.GetFirstRule())
                .Returns(this.firstRule);

            // Act
            var result = await this.service.GetPedidoWorkflowAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(request.Pedido, result.Pedido);
            Assert.AreEqual(1, result.Status.Count);
            Assert.AreEqual(Status.Aprovado.ToMessage(), result.Status.First());

            this.mockPedidoService
                .Verify(x => x.GetByIdAsync(int.Parse(request.Pedido)), Times.Once);
            this.mockRuleFactory
                .Verify(x => x.GetFirstRule(), Times.Once);
        }

        [Test]
        public async Task GetPedidoWorkflowAsync_PedidoExists_AprovadoValorAMenor()
        {
            // Arrange
            var request = new StatusRequest
            {
                Pedido = "1",
                Status = Status.Aprovado.ToMessage(),
                ItensAprovados = 3,
                ValorAprovado = 10,
            };

            var pedido = this.GetPedido();

            this.mockPedidoService
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(pedido);
            this.mockRuleFactory
                .Setup(x => x.GetFirstRule())
                .Returns(this.firstRule);

            // Act
            var result = await this.service.GetPedidoWorkflowAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(request.Pedido, result.Pedido);
            Assert.AreEqual(1, result.Status.Count);
            Assert.AreEqual(Status.AprovadoValorAMenor.ToMessage(), result.Status.First());

            this.mockPedidoService
                .Verify(x => x.GetByIdAsync(int.Parse(request.Pedido)), Times.Once);
            this.mockRuleFactory
                .Verify(x => x.GetFirstRule(), Times.Once);
        }

        [Test]
        public async Task GetPedidoWorkflowAsync_PedidoExists_AprovadoValorAMaiorAndAprovadoQtdAMaior()
        {
            // Arrange
            var request = new StatusRequest
            {
                Pedido = "1",
                Status = Status.Aprovado.ToMessage(),
                ItensAprovados = 4,
                ValorAprovado = 21,
            };

            var pedido = this.GetPedido();

            this.mockPedidoService
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(pedido);
            this.mockRuleFactory
                .Setup(x => x.GetFirstRule())
                .Returns(this.firstRule);

            // Act
            var result = await this.service.GetPedidoWorkflowAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(request.Pedido, result.Pedido);
            Assert.AreEqual(2, result.Status.Count);
            Assert.AreEqual(Status.AprovadoQtdAMaior.ToMessage(), result.Status.First());
            Assert.AreEqual(Status.AprovadoValorAMaior.ToMessage(), result.Status.Last());

            this.mockPedidoService
                .Verify(x => x.GetByIdAsync(int.Parse(request.Pedido)), Times.Once);
            this.mockRuleFactory
                .Verify(x => x.GetFirstRule(), Times.Once);
        }

        [Test]
        public async Task GetPedidoWorkflowAsync_PedidoExists_AprovadoQtdAMenor()
        {
            // Arrange
            var request = new StatusRequest
            {
                Pedido = "1",
                Status = Status.Aprovado.ToMessage(),
                ItensAprovados = 2,
                ValorAprovado = 20,
            };

            var pedido = this.GetPedido();

            this.mockPedidoService
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(pedido);
            this.mockRuleFactory
                .Setup(x => x.GetFirstRule())
                .Returns(this.firstRule);

            // Act
            var result = await this.service.GetPedidoWorkflowAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(request.Pedido, result.Pedido);
            Assert.AreEqual(1, result.Status.Count);
            Assert.AreEqual(Status.AprovadoQtdAMenor.ToMessage(), result.Status.First());

            this.mockPedidoService
                .Verify(x => x.GetByIdAsync(int.Parse(request.Pedido)), Times.Once);
            this.mockRuleFactory
                .Verify(x => x.GetFirstRule(), Times.Once);
        }

        private Pedido GetPedido()
        {
            return new Pedido
            {
                Itens = new List<Item>
                {
                    new Item
                    {
                        PrecoUnitario = 10,
                        Qtd = 1,
                    },
                    new Item
                    {
                        PrecoUnitario = 5,
                        Qtd = 2,
                    },
                },
            };
        }
    }
}