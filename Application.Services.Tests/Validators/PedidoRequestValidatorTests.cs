namespace Application.Services.Tests.Validators
{
    using Application.Dto;
    using Application.Dto.Requests;
    using Application.Services.Validators;

    using Infrastructure.CrossCutting.ErrorMessages;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Helpers;

    [TestFixture]
    public class PedidoRequestValidatorTests
    {
        [Test]
        public void Validate_RequestIsNull_ExceptionInvalidRequest()
        {
            // Arrange
            PedidoRequest request = null;

            try
            {
                // Act
                request.Validate();

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
        public void Validate_ItensIsInvalid_ExceptionRequestMustHaveAtLeastOneItem()
        {
            // Arrange
            var request = new PedidoRequest();

            try
            {
                // Act
                request.Validate();

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
        public void Validate_RequestIsValid_Success()
        {
            // Arrange
            var request = new PedidoRequest
            {
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
            request.Validate();

            // Assert
        }
    }
}