namespace Application.Services.Mappers.Requests
{
    public static class PedidoRequestMapper
    {
        public static Domain.Model.Requests.PedidoRequest ToModel(this Dto.Requests.PedidoRequest request)
        {
            if (request == null)
            {
                return null;
            }

            return new Domain.Model.Requests.PedidoRequest
            {
                Itens = request.Itens.ToModel(),
            };
        }
    }
}
