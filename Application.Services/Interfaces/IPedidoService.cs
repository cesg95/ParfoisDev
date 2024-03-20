namespace Application.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Application.Dto;
    using Application.Dto.Requests;

    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetAllAsync();

        Task<Pedido> GetByIdAsync(int pedidoId);

        Task<Pedido> CreateAsync(PedidoRequest request);

        Task UpdateAsync(int pedidoId, Pedido pedido);

        Task DeleteAsync(int pedidoId);
    }
}
