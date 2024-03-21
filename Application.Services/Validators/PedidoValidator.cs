namespace Application.Services.Validators
{
    using Application.Dto;

    using Infrastructure.CrossCutting.ErrorMessages;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Helpers;

    public static class PedidoValidator
    {
        public static void Validate(this Pedido pedido, string pedidoId)
        {
            if (pedido == null)
            {
                throw new BadRequestException(BadRequestMessages.InvalidRequest.ToMessage());
            }

            if (!int.TryParse(pedidoId, out int id))
            {
                throw new BadRequestException(BadRequestMessages.IdentifierMustBeAnInteger.ToMessage());
            }

            if (pedidoId != pedido.Id)
            {
                throw new BadRequestException(BadRequestMessages.IdentifierMustBeEqualToRequestParameter.ToMessage());
            }

            if (pedido.Itens == null || !pedido.Itens.Any())
            {
                throw new BadRequestException(BadRequestMessages.RequestMustHaveAtLeastOneItem.ToMessage());
            }

            foreach (var item in pedido.Itens)
            {
                item.Validate();
            }
        }
    }
}
