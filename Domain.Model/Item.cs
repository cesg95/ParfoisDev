namespace Domain.Model
{
    public class Item
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public decimal PrecoUnitario { get; set; }

        public int Qtd { get; set; }

        public int PedidoId { get; set; }
    }
}
