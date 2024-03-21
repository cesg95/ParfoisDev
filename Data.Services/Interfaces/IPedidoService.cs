namespace Data.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Domain.Model;
    using Domain.Model.Requests;

    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetAllAsync();

        Task<Pedido> GetByIdAsync(int pedidoId);

        Task<Pedido> CreateAsync(PedidoRequest request);

        Task UpdateAsync(Pedido pedido);

        Task DeleteAsync(Pedido pedido);
    }
}
