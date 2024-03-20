namespace Application.Services.Mappers
{
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
                Id = pedido.Id,
                Itens = pedido.Itens.ToDto(),
            };
        }
    }
}
