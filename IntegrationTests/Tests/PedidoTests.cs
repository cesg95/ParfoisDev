namespace IntegrationTests.Tests
{
    using System.Net;
    using System.Threading.Tasks;

    using Application.Dto;
    using Application.Dto.Requests;

    public class PedidoTests : BaseTests
    {
        private readonly string BaseUri = "api/pedido";

        [Fact]
        public async Task PostAndGetById_CreateAndGetPedido_Success()
        {
            // Arrange
            var server = CreateServer();
            var httpClient = server.CreateClient();

            var request = new PedidoRequest
            {
                Itens = new List<Item>
                {
                    new Item
                    {
                        Descricao = "test",
                        PrecoUnitario = 1,
                        Qtd = 2,
                    },
                },
            };
            var requestContent = Helper.BuildRequestContent(request);

            // Act
            var responsePost = await httpClient.PostAsync(BaseUri, requestContent);
            Assert.Equal(HttpStatusCode.Created, responsePost.StatusCode);
            var pedidoPost = await Helper.GetRequestContent<Pedido>(responsePost);
            Assert.NotNull(pedidoPost);

            var responseGetById = await httpClient.GetAsync($"{BaseUri}/{pedidoPost.Id}");
            Assert.Equal(HttpStatusCode.OK, responseGetById.StatusCode);
            var pedidoGetById = await Helper.GetRequestContent<Pedido>(responseGetById);
            Assert.NotNull(pedidoGetById);

            // Assert
            this.ValidatePedido(pedidoPost, pedidoGetById);
        }

        [Fact]
        public async Task PostUpdateAndGetById_CreateEditAndGetPedido_Success()
        {
            // Arrange
            var server = CreateServer();
            var httpClient = server.CreateClient();

            var request = new PedidoRequest
            {
                Itens = new List<Item>
                {
                    new Item
                    {
                        Descricao = "test",
                        PrecoUnitario = 1,
                        Qtd = 2,
                    },
                },
            };
            var requestContent = Helper.BuildRequestContent(request);

            // Act
            var responsePost = await httpClient.PostAsync(BaseUri, requestContent);
            Assert.Equal(HttpStatusCode.Created, responsePost.StatusCode);
            var pedidoPost = await Helper.GetRequestContent<Pedido>(responsePost);
            Assert.NotNull(pedidoPost);

            var expected = new Pedido
            {
                Id = pedidoPost.Id,
                Itens = new List<Item>
                {
                    new Item
                    {
                        Descricao = "update",
                        PrecoUnitario = 10,
                        Qtd = 1,
                    },
                },
            };
            var expectedContent = Helper.BuildRequestContent(expected);
            var responsePut = await httpClient.PutAsync($"{BaseUri}/{pedidoPost.Id}", expectedContent);
            Assert.Equal(HttpStatusCode.NoContent, responsePut.StatusCode);

            var responseGetById = await httpClient.GetAsync($"{BaseUri}/{pedidoPost.Id}");
            Assert.Equal(HttpStatusCode.OK, responseGetById.StatusCode);
            var pedidoGetById = await Helper.GetRequestContent<Pedido>(responseGetById);
            Assert.NotNull(pedidoGetById);

            // Assert
            this.ValidatePedido(expected, pedidoGetById);
        }

        [Fact]
        public async Task PostDeleteAndGetById_CreateDeleteAndGetPedido_Success()
        {
            // Arrange
            var server = CreateServer();
            var httpClient = server.CreateClient();

            var request = new PedidoRequest
            {
                Itens = new List<Item>
                {
                    new Item
                    {
                        Descricao = "test",
                        PrecoUnitario = 1,
                        Qtd = 2,
                    },
                },
            };
            var requestContent = Helper.BuildRequestContent(request);

            // Act
            var responsePost = await httpClient.PostAsync(BaseUri, requestContent);
            Assert.Equal(HttpStatusCode.Created, responsePost.StatusCode);
            var pedidoPost = await Helper.GetRequestContent<Pedido>(responsePost);
            Assert.NotNull(pedidoPost);

            var responsePut = await httpClient.DeleteAsync($"{BaseUri}/{pedidoPost.Id}");
            Assert.Equal(HttpStatusCode.NoContent, responsePut.StatusCode);

            // Assert
            var responseGetById = await httpClient.GetAsync($"{BaseUri}/{pedidoPost.Id}");
            Assert.Equal(HttpStatusCode.NotFound, responseGetById.StatusCode);
        }

        private void ValidatePedido(Pedido expected, Pedido result)
        {
            Assert.Equal(expected.Id, result.Id);
            Assert.NotNull(expected.Itens);
            Assert.NotNull(result.Itens);
            Assert.Equal(1, expected.Itens.Count());
            Assert.Equal(1, result.Itens.Count());
            Assert.Equal(expected.Itens.Single().Descricao, result.Itens.Single().Descricao);
            Assert.Equal(expected.Itens.Single().PrecoUnitario, result.Itens.Single().PrecoUnitario);
            Assert.Equal(expected.Itens.Single().Qtd, result.Itens.Single().Qtd);
        }
    }
}
