namespace Domain.Model
{
    public class Pedido
    {
        public int Id { get; set; }

        public ICollection<Item> Itens { get; set; }
    }
}
