namespace Application.Services.Tests.Validators
{
    using Application.Dto;
    using Application.Services.Validators;

    using Infrastructure.CrossCutting.ErrorMessages;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Helpers;

    [TestFixture]
    public class ItemValidatorTests
    {
        [Test]
        public void Validate_ItemIsNull_ExceptionInvalidItem()
        {
            // Arrange
            Item item = null;

            try
            {
                // Act
                item.Validate();

                Assert.Fail("Missing BadRequestException");
            }
            catch (BadRequestException ex)
            {
                // Assert
                Assert.IsNotNull(ex);
                Assert.AreEqual(BadRequestMessages.InvalidItem.ToMessage(), ex.Message);
            }
        }

        [Test]
        public void Validate_DescricaoIsInvalid_ExceptionItemMustHaveDescription()
        {
            // Arrange
            var item = new Item
            {
                Descricao = " ",
            };

            try
            {
                // Act
                item.Validate();

                Assert.Fail("Missing BadRequestException");
            }
            catch (BadRequestException ex)
            {
                // Assert
                Assert.IsNotNull(ex);
                Assert.AreEqual(BadRequestMessages.ItemMustHaveDescription.ToMessage(), ex.Message);
            }
        }

        [Test]
        public void Validate_PrecoUnitarioIsInvalid_ExceptionPrecoUnitarioCannotBeNegative()
        {
            // Arrange
            var item = new Item
            {
                Descricao = "test",
                PrecoUnitario = -1,
            };

            try
            {
                // Act
                item.Validate();

                Assert.Fail("Missing BadRequestException");
            }
            catch (BadRequestException ex)
            {
                // Assert
                Assert.IsNotNull(ex);
                Assert.AreEqual(BadRequestMessages.PrecoUnitarioCannotBeNegative.ToMessage(), ex.Message);
            }
        }

        [Test]
        public void Validate_PrecoUnitarioIsInvalid_ExceptionQtdCannotBeNegative()
        {
            // Arrange
            var item = new Item
            {
                Descricao = "test",
                PrecoUnitario = 1,
                Qtd = -1,
            };

            try
            {
                // Act
                item.Validate();

                Assert.Fail("Missing BadRequestException");
            }
            catch (BadRequestException ex)
            {
                // Assert
                Assert.IsNotNull(ex);
                Assert.AreEqual(BadRequestMessages.QtdCannotBeNegative.ToMessage(), ex.Message);
            }
        }

        [Test]
        public void Validate_ItemIsValid_Success()
        {
            // Arrange
            var item = new Item
            {
                Descricao = "test",
                PrecoUnitario = 1,
                Qtd = 1,
            };

            // Act
            item.Validate();

            // Assert
        }
    }
}