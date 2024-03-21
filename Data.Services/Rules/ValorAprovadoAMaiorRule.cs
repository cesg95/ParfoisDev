namespace Data.Services.Rules
{
    using Domain.Model;
    using Domain.Model.Enums;
    using Domain.Model.Requests;

    using Infrastructure.CrossCutting.Helpers;

    public class ValorAprovadoAMaiorRule : AbstractRule
    {
        public override object Handle(Pedido pedido, StatusRequest request, PedidoWorkflow workflow)
        {
            if (!request.Status.Equals(Status.Aprovado.ToMessage()))
            {
                return base.Handle(pedido, request, workflow);
            }

            var valorItems = pedido.Itens.Sum(x => x.Qtd * x.PrecoUnitario);

            if (request.ValorAprovado > valorItems)
            {
                workflow.Status.Add(Status.AprovadoValorAMaior.ToMessage());
            }

            return base.Handle(pedido, request, workflow);
        }
    }
}
