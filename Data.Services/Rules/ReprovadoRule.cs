namespace Data.Services.Rules
{
    using Domain.Model;
    using Domain.Model.Enums;
    using Domain.Model.Requests;

    using Infrastructure.CrossCutting.Helpers;

    public class ReprovadoRule : AbstractRule
    {
        public override object Handle(Pedido pedido, StatusRequest request, PedidoWorkflow workflow)
        {
            if (request.Status.Equals(Status.Reprovado.ToMessage()))
            {
                workflow.Status.Add(Status.Reprovado.ToMessage());
            }

            return base.Handle(pedido, request, workflow);
        }
    }
}
