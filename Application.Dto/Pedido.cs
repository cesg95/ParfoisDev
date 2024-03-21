namespace Application.Dto
{
    public class Pedido
    {
        public string Id { get; set; }

        public IEnumerable<Item> Itens { get; set; }
    }
}
