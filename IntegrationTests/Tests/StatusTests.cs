namespace IntegrationTests.Tests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Application.Dto;
    using Application.Dto.Requests;

    public class StatusTests : BaseTests
    {
        private readonly string BaseUri = "api/status";
        private readonly string PedidoUri = "api/pedido";

        [Fact]
        public async Task PostPedidoAndPostStatus_CreatePedidoAndGetWorkflow_APROVADO()
        {
            // Arrange
            var server = CreateServer();
            var httpClient = server.CreateClient();

            // Act
            var request = this.GetPedidoRequest();
            var requestContent = Helper.BuildRequestContent(request);
            var pedidoPost = await this.PostAsync<Pedido>(httpClient, requestContent, HttpStatusCode.Created, PedidoUri);

            var statusRequest = new StatusRequest
            {
                Pedido = pedidoPost.Id,
                ItensAprovados = 3,
                ValorAprovado = 20,
                Status = "APROVADO",
            };
            var statusRequestContent = Helper.BuildRequestContent(statusRequest);
            var pedidoWorkflow = await this.PostAsync<PedidoWorkflow>(httpClient, statusRequestContent, HttpStatusCode.OK, BaseUri);

            // Assert
            Assert.Equal(pedidoPost.Id, pedidoWorkflow.Pedido);
            Assert.NotNull(pedidoWorkflow.Status);
            Assert.Equal(1, pedidoWorkflow.Status.Count());
            Assert.Equal("APROVADO", pedidoWorkflow.Status.Single());
        }

        [Fact]
        public async Task PostPedidoAndPostStatus_CreatePedidoAndGetWorkflow_APROVADO_VALOR_A_MENOR()
        {
            // Arrange
            var server = CreateServer();
            var httpClient = server.CreateClient();

            // Act
            var request = this.GetPedidoRequest();
            var requestContent = Helper.BuildRequestContent(request);
            var pedidoPost = await this.PostAsync<Pedido>(httpClient, requestContent, HttpStatusCode.Created, PedidoUri);

            var statusRequest = new StatusRequest
            {
                Pedido = pedidoPost.Id,
                ItensAprovados = 3,
                ValorAprovado = 10,
                Status = "APROVADO",
            };
            var statusRequestContent = Helper.BuildRequestContent(statusRequest);
            var pedidoWorkflow = await this.PostAsync<PedidoWorkflow>(httpClient, statusRequestContent, HttpStatusCode.OK, BaseUri);

            // Assert
            Assert.Equal(pedidoPost.Id, pedidoWorkflow.Pedido);
            Assert.NotNull(pedidoWorkflow.Status);
            Assert.Equal(1, pedidoWorkflow.Status.Count());
            Assert.Equal("APROVADO_VALOR_A_MENOR", pedidoWorkflow.Status.Single());
        }

        [Fact]
        public async Task PostPedidoAndPostStatus_CreatePedidoAndGetWorkflow_APROVADO_VALOR_A_MAIOR_And_APROVADO_QTD_A_MAIOR()
        {
            // Arrange
            var server = CreateServer();
            var httpClient = server.CreateClient();

            // Act
            var request = this.GetPedidoRequest();
            var requestContent = Helper.BuildRequestContent(request);
            var pedidoPost = await this.PostAsync<Pedido>(httpClient, requestContent, HttpStatusCode.Created, PedidoUri);

            var statusRequest = new StatusRequest
            {
                Pedido = pedidoPost.Id,
                ItensAprovados = 4,
                ValorAprovado = 21,
                Status = "APROVADO",
            };
            var statusRequestContent = Helper.BuildRequestContent(statusRequest);
            var pedidoWorkflow = await this.PostAsync<PedidoWorkflow>(httpClient, statusRequestContent, HttpStatusCode.OK, BaseUri);

            // Assert
            Assert.Equal(pedidoPost.Id, pedidoWorkflow.Pedido);
            Assert.NotNull(pedidoWorkflow.Status);
            Assert.Equal(2, pedidoWorkflow.Status.Count());
            Assert.Equal("APROVADO_QTD_A_MAIOR", pedidoWorkflow.Status.First());
            Assert.Equal("APROVADO_VALOR_A_MAIOR", pedidoWorkflow.Status.Last());
        }

        [Fact]
        public async Task PostPedidoAndPostStatus_CreatePedidoAndGetWorkflow_APROVADO_QTD_A_MENOR()
        {
            // Arrange
            var server = CreateServer();
            var httpClient = server.CreateClient();

            // Act
            var request = this.GetPedidoRequest();
            var requestContent = Helper.BuildRequestContent(request);
            var pedidoPost = await this.PostAsync<Pedido>(httpClient, requestContent, HttpStatusCode.Created, PedidoUri);

            var statusRequest = new StatusRequest
            {
                Pedido = pedidoPost.Id,
                ItensAprovados = 2,
                ValorAprovado = 20,
                Status = "APROVADO",
            };
            var statusRequestContent = Helper.BuildRequestContent(statusRequest);
            var pedidoWorkflow = await this.PostAsync<PedidoWorkflow>(httpClient, statusRequestContent, HttpStatusCode.OK, BaseUri);

            // Assert
            Assert.Equal(pedidoPost.Id, pedidoWorkflow.Pedido);
            Assert.NotNull(pedidoWorkflow.Status);
            Assert.Equal(1, pedidoWorkflow.Status.Count());
            Assert.Equal("APROVADO_QTD_A_MENOR", pedidoWorkflow.Status.Single());
        }

        [Fact]
        public async Task PostPedidoAndPostStatus_CreatePedidoAndGetWorkflow_REPROVADO()
        {
            // Arrange
            var server = CreateServer();
            var httpClient = server.CreateClient();

            // Act
            var request = this.GetPedidoRequest();
            var requestContent = Helper.BuildRequestContent(request);
            var pedidoPost = await this.PostAsync<Pedido>(httpClient, requestContent, HttpStatusCode.Created, PedidoUri);

            var statusRequest = new StatusRequest
            {
                Pedido = pedidoPost.Id,
                ItensAprovados = 2,
                ValorAprovado = 20,
                Status = "REPROVADO",
            };
            var statusRequestContent = Helper.BuildRequestContent(statusRequest);
            var pedidoWorkflow = await this.PostAsync<PedidoWorkflow>(httpClient, statusRequestContent, HttpStatusCode.OK, BaseUri);

            // Assert
            Assert.Equal(pedidoPost.Id, pedidoWorkflow.Pedido);
            Assert.NotNull(pedidoWorkflow.Status);
            Assert.Equal(1, pedidoWorkflow.Status.Count());
            Assert.Equal("REPROVADO", pedidoWorkflow.Status.Single());
        }

        [Fact]
        public async Task PostPedidoAndPostStatus_CreatePedidoAndGetWorkflow_CODIGO_PEDIDO_INVALIDO()
        {
            // Arrange
            var server = CreateServer();
            var httpClient = server.CreateClient();

            // Act
            var request = this.GetPedidoRequest();
            var requestContent = Helper.BuildRequestContent(request);
            var pedidoPost = await this.PostAsync<Pedido>(httpClient, requestContent, HttpStatusCode.Created, PedidoUri);

            var statusRequest = new StatusRequest
            {
                Pedido = "abc",
                ItensAprovados = 2,
                ValorAprovado = 20,
                Status = "APROVADO",
            };
            var statusRequestContent = Helper.BuildRequestContent(statusRequest);
            var pedidoWorkflow = await this.PostAsync<PedidoWorkflow>(httpClient, statusRequestContent, HttpStatusCode.OK, BaseUri);

            // Assert
            Assert.NotEqual(pedidoPost.Id, pedidoWorkflow.Pedido);
            Assert.NotNull(pedidoWorkflow.Status);
            Assert.Equal(1, pedidoWorkflow.Status.Count());
            Assert.Equal("CODIGO_PEDIDO_INVALIDO", pedidoWorkflow.Status.Single());
        }

        private async Task<T> PostAsync<T>(
            HttpClient httpClient,
            StringContent statusRequestContent,
            HttpStatusCode statusCode,
            string uri)
        {
            var response = await httpClient.PostAsync(uri, statusRequestContent);
            Assert.Equal(statusCode, response.StatusCode);

            var entity = await Helper.GetRequestContent<T>(response);
            Assert.NotNull(entity);

            return entity;
        }

        private PedidoRequest GetPedidoRequest()
        {
            return new PedidoRequest
            {
                Itens = new List<Item>
                {
                    new Item
                    {
                        Descricao = "Item A",
                        PrecoUnitario = 10,
                        Qtd = 1,
                    },
                    new Item
                    {
                        Descricao = "Item B",
                        PrecoUnitario = 5,
                        Qtd = 2,
                    },
                },
            };
        }
    }
}
