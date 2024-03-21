namespace Data.Services.Rules
{
    using Domain.Model;
    using Domain.Model.Requests;

    public abstract class AbstractRule : IRule
    {
        private IRule _nextRule;

        public IRule SetNext(IRule rule)
        {
            this._nextRule = rule;
            return rule;
        }

        public virtual object Handle(Pedido pedido, StatusRequest request, PedidoWorkflow workflow)
        {
            return this._nextRule?.Handle(pedido, request, workflow);
        }
    }
}
