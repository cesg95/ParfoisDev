namespace Application.Services.Validators
{
    using Application.Dto.Requests;

    using Domain.Model.Enums;

    using Infrastructure.CrossCutting.ErrorMessages;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Helpers;

    public static class StatusRequestValidator
    {
        private static List<string> ValidStatus = new List<string>
        {
            Status.Aprovado.ToMessage(),
            Status.Reprovado.ToMessage(),
        };

        public static void Validate(this StatusRequest request)
        {
            if (request == null)
            {
                throw new BadRequestException(BadRequestMessages.InvalidRequest.ToMessage());
            }

            if (!ValidStatus.Contains(request.Status))
            {
                throw new BadRequestException(BadRequestMessages.InvalidStatus.ToMessage());
            }

            if (request.ItensAprovados < 0)
            {
                throw new BadRequestException(BadRequestMessages.ItensAprovadosCannotBeNegative.ToMessage());
            }

            if (request.ValorAprovado < 0)
            {
                throw new BadRequestException(BadRequestMessages.ValorAprovadoCannotBeNegative.ToMessage());
            }
        }
    }
}
