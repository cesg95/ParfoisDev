namespace Application.Services.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Application.Dto;
    using Application.Dto.Requests;
    using Application.Services.Interfaces;
    using Application.Services.Mappers;
    using Application.Services.Mappers.Requests;

    public class PedidoService : IPedidoService
    {
        private readonly Data.Services.Interfaces.IPedidoService pedidoService;

        public PedidoService(Data.Services.Interfaces.IPedidoService pedidoService)
        {
            this.pedidoService = pedidoService;
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            var pedidos = await this.pedidoService.GetAllAsync();

            return pedidos.ToDto();
        }

        public async Task<Pedido> GetByIdAsync(int pedidoId)
        {
            var pedido = await this.pedidoService.GetByIdAsync(pedidoId);

            return pedido.ToDto();
        }

        public async Task<Pedido> CreateAsync(PedidoRequest request)
        {
            var requestMapped = request.ToModel();

            var pedido = await this.pedidoService.CreateAsync(requestMapped);

            return pedido.ToDto();
        }

        public Task UpdateAsync(int pedidoId, Pedido pedido)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int pedidoId)
        {
            throw new NotImplementedException();
        }
    }
}
