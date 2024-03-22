namespace Application.Services.Tests.Validators
{
    using Application.Dto.Requests;
    using Application.Services.Validators;

    using Domain.Model.Enums;

    using Infrastructure.CrossCutting.ErrorMessages;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Helpers;

    [TestFixture]
    public class StatusRequestValidatorTests
    {
        [Test]
        public void Validate_RequestIsNull_ExceptionInvalidRequest()
        {
            // Arrange
            StatusRequest request = null;

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
        public void Validate_StatusIsInvalid_ExceptionInvalidStatus()
        {
            // Arrange
            var request = new StatusRequest
            {
                Status = Status.AprovadoValorAMaior.ToMessage(),
            };

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
                Assert.AreEqual(BadRequestMessages.InvalidStatus.ToMessage(), ex.Message);
            }
        }

        [Test]
        public void Validate_ItensAprovadosIsInvalid_ExceptionItensAprovadosCannotBeNegative()
        {
            // Arrange
            var request = new StatusRequest
            {
                Status = Status.Aprovado.ToMessage(),
                ItensAprovados = -1,
            };

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
                Assert.AreEqual(BadRequestMessages.ItensAprovadosCannotBeNegative.ToMessage(), ex.Message);
            }
        }

        [Test]
        public void Validate_ValorAprovadoIsInvalid_ExceptionValorAprovadoCannotBeNegative()
        {
            // Arrange
            var request = new StatusRequest
            {
                Status = Status.Reprovado.ToMessage(),
                ItensAprovados = 1,
                ValorAprovado = -1,
            };

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
                Assert.AreEqual(BadRequestMessages.ValorAprovadoCannotBeNegative.ToMessage(), ex.Message);
            }
        }

        [Test]
        public void Validate_RequestIsValid_Success()
        {
            // Arrange
            var request = new StatusRequest
            {
                Status = Status.Aprovado.ToMessage(),
                ItensAprovados = 1,
                ValorAprovado = 1,
            };

            // Act
            request.Validate();

            // Assert
        }
    }
}