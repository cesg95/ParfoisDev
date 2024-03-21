namespace Data.Repository.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Data.Repository.Interfaces;

    using Domain.Model;
    using Domain.Model.Requests;

    using Microsoft.EntityFrameworkCore;

    public class PedidoRepository : IPedidoRepository
    {
        private ApiContext context;

        public PedidoRepository(ApiContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await this.context.Pedidos.Include(x => x.Itens).AsNoTracking().ToListAsync();
        }

        public async Task<Pedido> GetByIdAsync(int pedidoId)
        {
            var pedidos = await this.GetAllAsync();

            return pedidos.SingleOrDefault(x => x.Id == pedidoId);
        }

        public async Task<Pedido> CreateAsync(PedidoRequest request)
        {
            var pedido = new Pedido
            {
                Itens = request.Itens.ToList(),
            };

            await this.context.Pedidos.AddAsync(pedido);
            await this.context.SaveChangesAsync();

            return pedido;
        }

        public async Task UpdateAsync(Pedido pedido)
        {
            var items = this.context.Items.Where(x => x.PedidoId == pedido.Id);
            this.context.Items.RemoveRange(items);

            this.context.Pedidos.Update(pedido);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Pedido pedido)
        {
            this.context.Pedidos.Remove(pedido);
            await this.context.SaveChangesAsync();
        }
    }
}
