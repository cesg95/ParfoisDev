namespace Application.Services.Tests.Mappers.Requests
{
    using Application.Services.Mappers;
    using Application.Services.Mappers.Requests;

    using AutoFixture;

    using Dto = Dto.Requests;

    [TestFixture]
    public class StatusRequestMapperTests
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
            Dto.StatusRequest request = null;

            // Act
            var result = request.ToModel();

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ToModel_MapRequest_Success()
        {
            // Arrange
            var request = this.fixture.Create<Dto.StatusRequest>();

            // Act
            var result = request.ToModel();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(request.Status, result.Status);
            Assert.AreEqual(request.ItensAprovados, result.ItensAprovados);
            Assert.AreEqual(request.ValorAprovado, result.ValorAprovado);
            Assert.AreEqual(request.Pedido, result.Pedido);
        }
    }
}