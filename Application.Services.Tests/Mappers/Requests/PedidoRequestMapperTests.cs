namespace Application.Services.Tests.Mappers.Requests
{
    using Application.Services.Mappers;
    using Application.Services.Mappers.Requests;

    using AutoFixture;

    using Dto = Dto.Requests;

    [TestFixture]
    public class PedidoRequestMapperTests
    {
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            this.fixture = new Fixture();
        }

        [Test]
        public void ToModel_NullRequest_Null()
        {
            // Arrange
            Dto.PedidoRequest request = null;

            // Act
            var result = request.ToModel();

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ToModel_MapRequest_Success()
        {
            // Arrange
            var request = this.fixture.Create<Dto.PedidoRequest>();

            // Act
            var result = request.ToModel();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(request.Itens.Count(), result.Itens.Count());
        }
    }
}