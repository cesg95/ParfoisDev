namespace Infrastructure.CrossCutting.ErrorMessages
{
    using System.ComponentModel;

    public enum NotFoundMessages
    {
        [Description("Pedido was not found.")]
        PedidoNotFound = 0,
    }
}
