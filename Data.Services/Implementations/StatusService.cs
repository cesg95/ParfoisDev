namespace Data.Services.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Data.Services.Interfaces;
    using Data.Services.Rules;

    using Domain.Model;
    using Domain.Model.Enums;
    using Domain.Model.Requests;

    using Infrastructure.CrossCutting.Helpers;

    public class StatusService : IStatusService
    {
        private readonly IPedidoService pedidoService;
        private readonly IRuleFactory ruleFactory;

        public StatusService(
            IPedidoService pedidoService,
            IRuleFactory ruleFactory)
        {
            this.pedidoService = pedidoService;
            this.ruleFactory = ruleFactory;
        }

        public async Task<PedidoWorkflow> GetPedidoWorkflowAsync(StatusRequest request)
        {
            var pedido = await this.pedidoService.GetByIdAsync(int.Parse(request.Pedido));

            var workflow = new PedidoWorkflow
            {
                Pedido = request.Pedido,
                Status = new List<string>(),
            };

            if (pedido == null)
            {
                workflow.Status.Add(Status.CodigoPedidoInvalido.ToMessage());
                return workflow;
            }

            var rule = this.ruleFactory.GetFirstRule();
            rule.Handle(pedido, request, workflow);

            return workflow;
        }
    }
}
