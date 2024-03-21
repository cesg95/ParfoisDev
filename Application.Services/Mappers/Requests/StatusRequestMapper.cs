namespace Application.Services.Mappers.Requests
{
    public static class StatusRequestMapper
    {
        public static Domain.Model.Requests.StatusRequest ToModel(this Dto.Requests.StatusRequest request)
        {
            if (request == null)
            {
                return null;
            }

            return new Domain.Model.Requests.StatusRequest
            {
                Status = request.Status,
                ItensAprovados = request.ItensAprovados,
                ValorAprovado = request.ValorAprovado,
                Pedido = request.Pedido,
            };
        }
    }
}
