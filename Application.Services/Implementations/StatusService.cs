namespace Application.Services.Implementations
{
    using System.Threading.Tasks;

    using Application.Dto;
    using Application.Dto.Requests;
    using Application.Services.Interfaces;
    using Application.Services.Mappers;
    using Application.Services.Mappers.Requests;
    using Application.Services.Validators;

    using Domain.Model.Enums;

    using Infrastructure.CrossCutting.Helpers;

    public class StatusService : IStatusService
    {
        private readonly Data.Services.Interfaces.IStatusService statusService;

        public StatusService(Data.Services.Interfaces.IStatusService statusService)
        {
            this.statusService = statusService;
        }

        public async Task<PedidoWorkflow> GetPedidoWorkflowAsync(StatusRequest request)
        {
            request.Validate();

            if (!int.TryParse(request.Pedido, out int id))
            {
                return new PedidoWorkflow
                {
                    Pedido = request.Pedido,
                    Status = new List<string>
                    {
                        Status.CodigoPedidoInvalido.ToMessage(),
                    },
                };
            }

            var requestMapped = request.ToModel();

            var workflow = await this.statusService.GetPedidoWorkflowAsync(requestMapped);

            return workflow.ToDto();
        }
    }
}
