namespace Application.Services.Tests.Implementations
{
    using Application.Dto.Requests;
    using Application.Services.Implementations;

    using AutoFixture;

    using Data.Services.Interfaces;

    using Domain.Model.Enums;

    using Infrastructure.CrossCutting.ErrorMessages;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Helpers;

    using Moq;

    [TestFixture]
    public class StatusServiceTests
    {
        private Mock<IStatusService> mockStatusService;
        private StatusService service;
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            this.mockStatusService = new Mock<IStatusService>();

            this.service = new StatusService(this.mockStatusService.Object);

            this.fixture = new Fixture();
        }

        [Test]
        public async Task GetPedidoWorkflowAsync_RequestIsNull_Exception()
        {
            // Arrange
            StatusRequest request = null;

            try
            {
                // Act
                var result = await this.service.GetPedidoWorkflowAsync(request);

                Assert.Fail("Missing BadRequestException");
            }
            catch (BadRequestException ex)
            {
                // Assert
                Assert.IsNotNull(ex);
                Assert.AreEqual(BadRequestMessages.InvalidRequest.ToMessage(), ex.Message);

                this.mockStatusService
                    .Verify(x => x.GetPedidoWorkflowAsync(It.IsAny<Domain.Model.Requests.StatusRequest>()), Times.Never);
            }
        }

        [Test]
        public async Task GetPedidoWorkflowAsync_PedidoIdIsInvalid_CodigoPedidoInvalido()
        {
            // Arrange
            var request = new StatusRequest
            {
                Pedido = "a",
                Status = Status.Aprovado.ToMessage(),
                ItensAprovados = 1,
                ValorAprovado = 1,
            };

            // Act
            var result = await this.service.GetPedidoWorkflowAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(request.Pedido, result.Pedido);
            Assert.AreEqual(1, result.Status.Count());
            Assert.AreEqual(Status.CodigoPedidoInvalido.ToMessage(), result.Status.Single());

            this.mockStatusService
                .Verify(x => x.GetPedidoWorkflowAsync(It.IsAny<Domain.Model.Requests.StatusRequest>()), Times.Never);
        }

        [Test]
        public async Task GetPedidoWorkflowAsync_ValidRequest_PedidoWorkflow()
        {
            // Arrange
            var request = new StatusRequest
            {
                Pedido = "1",
                Status = Status.Aprovado.ToMessage(),
                ItensAprovados = 1,
                ValorAprovado = 1,
            };

            var expected = new Domain.Model.PedidoWorkflow
            {
                Pedido = request.Pedido,
                Status = new List<string> { request.Status },
            };

            this.mockStatusService
                .Setup(x => x.GetPedidoWorkflowAsync(It.IsAny<Domain.Model.Requests.StatusRequest>()))
                .ReturnsAsync(expected);

            // Act
            var result = await this.service.GetPedidoWorkflowAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Pedido, result.Pedido);
            Assert.AreEqual(1, result.Status.Count());
            Assert.AreEqual(expected.Status.Single(), result.Status.Single());

            this.mockStatusService
                .Verify(x => x.GetPedidoWorkflowAsync(It.IsAny<Domain.Model.Requests.StatusRequest>()), Times.Once);
        }
    }
}