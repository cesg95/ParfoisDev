namespace Domain.Model.Requests
{
    public class PedidoRequest
    {
        public IEnumerable<Item> Itens { get; set; }
    }
}
