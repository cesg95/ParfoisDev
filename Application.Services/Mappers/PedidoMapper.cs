namespace Application.Services.Mappers
{
    using Infrastructure.CrossCutting.ErrorMessages;
    using Infrastructure.CrossCutting.Helpers;

    public static class PedidoMapper
    {
        public static IEnumerable<Dto.Pedido> ToDto(this IEnumerable<Domain.Model.Pedido> pedidos)
        {
            if (pedidos == null)
            {
                return new List<Dto.Pedido>();
            }

            return pedidos.Select(ToDto);
        }

        public static Dto.Pedido ToDto(this Domain.Model.Pedido pedido)
        {
            if (pedido == null)
            {
                return null;
            }

            return new Dto.Pedido
            {
                Id = pedido.Id.ToString(),
                Itens = pedido.Itens.ToDto(),
            };
        }

        public static Domain.Model.Pedido ToModel(this Dto.Pedido pedido)
        {
            if (pedido == null)
            {
                return null;
            }

            if (!int.TryParse(pedido.Id, out int id))
            {
                throw new InvalidCastException(BadRequestMessages.InvalidIdentifier.ToMessage());
            }

            return new Domain.Model.Pedido
            {
                Id = id,
                Itens = pedido.Itens.ToModel().ToList(),
            };
        }
    }
}
