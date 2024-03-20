namespace Data.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Data.Repository.Interfaces;
    using Data.Services.Interfaces;

    using Domain.Model;
    using Domain.Model.Requests;

    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            this.pedidoRepository = pedidoRepository;
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await this.pedidoRepository.GetAllAsync();
        }

        public async Task<Pedido> GetByIdAsync(int pedidoId)
        {
            return await this.pedidoRepository.GetByIdAsync(pedidoId);
        }

        public async Task<Pedido> CreateAsync(PedidoRequest request)
        {
            return await this.pedidoRepository.CreateAsync(request);
        }
    }
}
