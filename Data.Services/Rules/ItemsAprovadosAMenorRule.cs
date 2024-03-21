namespace Data.Services.Rules
{
    using Domain.Model;
    using Domain.Model.Enums;
    using Domain.Model.Requests;

    using Infrastructure.CrossCutting.Helpers;

    public class ItemsAprovadosAMenorRule : AbstractRule
    {
        public override object Handle(Pedido pedido, StatusRequest request, PedidoWorkflow workflow)
        {
            if (!request.Status.Equals(Status.Aprovado.ToMessage()))
            {
                return base.Handle(pedido, request, workflow);
            }

            var qtdItems = pedido.Itens.Sum(x => x.Qtd);

            if (request.ItensAprovados < qtdItems)
            {
                workflow.Status.Add(Status.AprovadoQtdAMenor.ToMessage());
            }

            return base.Handle(pedido, request, workflow);
        }
    }
}
