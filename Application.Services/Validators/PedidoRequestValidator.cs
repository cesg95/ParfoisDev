namespace Application.Services.Validators
{
    using Application.Dto.Requests;

    using Infrastructure.CrossCutting.ErrorMessages;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Helpers;

    public static class PedidoRequestValidator
    {
        public static void Validate(this PedidoRequest request)
        {
            if (request == null)
            {
                throw new BadRequestException(BadRequestMessages.InvalidRequest.ToMessage());
            }

            if (request.Itens == null || !request.Itens.Any())
            {
                throw new BadRequestException(BadRequestMessages.RequestMustHaveAtLeastOneItem.ToMessage());
            }

            foreach (var item in request.Itens)
            {
                item.Validate();
            }
        }
    }
}
