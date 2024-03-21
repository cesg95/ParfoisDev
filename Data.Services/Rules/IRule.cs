namespace Data.Services.Rules
{
    using Domain.Model;
    using Domain.Model.Requests;

    public interface IRule
    {
        IRule SetNext(IRule rule);

        object Handle(Pedido pedido, StatusRequest request, PedidoWorkflow workflow);
    }
}
