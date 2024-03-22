namespace Application.Services.Tests.Mappers
{
    using Application.Services.Mappers;

    using AutoFixture;

    using Model = Domain.Model;

    [TestFixture]
    public class ItemMapperTests
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
            IEnumerable<Model.Item> items = null;

            // Act
            var result = items.ToDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [Test]
        public void ToDto_ListWithoutItems_EmptyList()
        {
            // Arrange
            var items = new List<Model.Item>();

            // Act
            var result = items.ToDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [Test]
        public void ToDto_ListWithItems_FilledList()
        {
            // Arrange
            var items = this.fixture.CreateMany<Model.Item>();

            // Act
            var result = items.ToDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(items.Count(), result.Count());

            for (var i = 0; i < result.Count(); i++)
            {
                var item = items.ElementAt(i);
                var resultItem = result.ElementAt(i);

                Assert.AreEqual(item.Descricao, resultItem.Descricao);
                Assert.AreEqual(item.PrecoUnitario, resultItem.PrecoUnitario);
                Assert.AreEqual(item.Qtd, resultItem.Qtd);
            }
        }

        [Test]
        public void ToModel_NullList_EmptyList()
        {
            // Arrange
            IEnumerable<Dto.Item> items = null;

            // Act
            var result = items.ToModel();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [Test]
        public void ToModel_ListWithoutItems_EmptyList()
        {
            // Arrange
            var items = new List<Dto.Item>();

            // Act
            var result = items.ToModel();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [Test]
        public void ToModel_ListWithItems_FilledList()
        {
            // Arrange
            var items = this.fixture.CreateMany<Dto.Item>();

            // Act
            var result = items.ToModel();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(items.Count(), result.Count());

            for (var i = 0; i < result.Count(); i++)
            {
                var item = items.ElementAt(i);
                var resultItem = result.ElementAt(i);

                Assert.AreEqual(item.Descricao, resultItem.Descricao);
                Assert.AreEqual(item.PrecoUnitario, resultItem.PrecoUnitario);
                Assert.AreEqual(item.Qtd, resultItem.Qtd);
            }
        }
    }
}