namespace Application.Services.Mappers
{
    public static class PedidoWorkflowMapper
    {
        public static Dto.PedidoWorkflow ToDto(this Domain.Model.PedidoWorkflow workflow)
        {
            if (workflow == null)
            {
                return null;
            }

            return new Dto.PedidoWorkflow
            {
                Pedido = workflow.Pedido,
                Status = workflow.Status,
            };
        }
    }
}
