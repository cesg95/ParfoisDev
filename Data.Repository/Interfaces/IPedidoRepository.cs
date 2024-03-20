namespace Data.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Domain.Model;
    using Domain.Model.Requests;

    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> GetAllAsync();

        Task<Pedido> GetByIdAsync(int pedidoId);

        Task<Pedido> CreateAsync(PedidoRequest request);
    }
}
