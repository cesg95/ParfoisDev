namespace Application.Services.Tests.Validators
{
    using Application.Dto;
    using Application.Services.Validators;

    using Infrastructure.CrossCutting.ErrorMessages;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Helpers;

    [TestFixture]
    public class PedidoValidatorTests
    {
        [Test]
        public void Validate_PedidoIsNull_ExceptionInvalidRequest()
        {
            // Arrange
            Pedido pedido = null;

            try
            {
                // Act
                pedido.Validate("a");

                Assert.Fail("Missing BadRequestException");
            }
            catch (BadRequestException ex)
            {
                // Assert
                Assert.IsNotNull(ex);
                Assert.AreEqual(BadRequestMessages.InvalidRequest.ToMessage(), ex.Message);
            }
        }

        [Test]
        public void Validate_PedidoIdIsInvalid_ExceptionIdentifierMustBeAnInteger()
        {
            // Arrange
            var pedido = new Pedido();

            try
            {
                // Act
                pedido.Validate("a");

                Assert.Fail("Missing BadRequestException");
            }
            catch (BadRequestException ex)
            {
                // Assert
                Assert.IsNotNull(ex);
                Assert.AreEqual(BadRequestMessages.IdentifierMustBeAnInteger.ToMessage(), ex.Message);
            }
        }

        [Test]
        public void Validate_PedidoIdIsInvalid_ExceptionIdentifierMustBeEqualToRequestParameter()
        {
            // Arrange
            var pedido = new Pedido();

            try
            {
                // Act
                pedido.Validate("1");

                Assert.Fail("Missing BadRequestException");
            }
            catch (BadRequestException ex)
            {
                // Assert
                Assert.IsNotNull(ex);
                Assert.AreEqual(BadRequestMessages.IdentifierMustBeEqualToRequestParameter.ToMessage(), ex.Message);
            }
        }

        [Test]
        public void Validate_ItensIsInvalid_ExceptionRequestMustHaveAtLeastOneItem()
        {
            // Arrange
            var pedido = new Pedido
            {
                Id = "1",
            };

            try
            {
                // Act
                pedido.Validate(pedido.Id);

                Assert.Fail("Missing BadRequestException");
            }
            catch (BadRequestException ex)
            {
                // Assert
                Assert.IsNotNull(ex);
                Assert.AreEqual(BadRequestMessages.RequestMustHaveAtLeastOneItem.ToMessage(), ex.Message);
            }
        }

        [Test]
        public void Validate_PedidoIsValid_Success()
        {
            // Arrange
            var pedido = new Pedido
            {
                Id = "1",
                Itens = new List<Item>
                {
                    new Item
                    {
                        Descricao = "test",
                        PrecoUnitario = 1,
                        Qtd = 1,
                    },
                },
            };

            // Act
            pedido.Validate(pedido.Id);

            // Assert
        }
    }
}