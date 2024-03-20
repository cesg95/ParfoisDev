namespace Application.Dto
{
    public class Pedido
    {
        public int Id { get; set; }

        public IEnumerable<Item> Itens { get; set; }
    }
}
