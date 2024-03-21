namespace Application.Dto
{
    using System.Collections.Generic;

    public class PedidoWorkflow
    {
        public string Pedido { get; set; }

        public IEnumerable<string> Status { get; set; }
    }
}
