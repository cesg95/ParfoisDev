namespace Data.Services.Rules
{
    using Domain.Model;
    using Domain.Model.Enums;
    using Domain.Model.Requests;

    using Infrastructure.CrossCutting.Helpers;

    public class AprovadoRule : AbstractRule
    {
        public override object Handle(Pedido pedido, StatusRequest request, PedidoWorkflow workflow)
        {
            if (!request.Status.Equals(Status.Aprovado.ToMessage()))
            {
                return base.Handle(pedido, request, workflow);
            }

            var qtdItems = pedido.Itens.Sum(x => x.Qtd);
            var valorItems = pedido.Itens.Sum(x => x.Qtd * x.PrecoUnitario);

            if (request.ItensAprovados == qtdItems && request.ValorAprovado == valorItems)
            {
                workflow.Status.Add(Status.Aprovado.ToMessage());
            }

            return base.Handle(pedido, request, workflow);
        }
    }
}
