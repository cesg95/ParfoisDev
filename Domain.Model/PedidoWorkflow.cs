namespace Domain.Model
{
    using System.Collections.Generic;

    public class PedidoWorkflow
    {
        public string Pedido { get; set; }

        public ICollection<string> Status { get; set; }
    }
}
