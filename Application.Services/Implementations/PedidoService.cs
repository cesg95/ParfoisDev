namespace Application.Services.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Application.Dto;
    using Application.Dto.Requests;
    using Application.Services.Interfaces;
    using Application.Services.Mappers;
    using Application.Services.Mappers.Requests;
    using Application.Services.Validators;

    using Infrastructure.CrossCutting.ErrorMessages;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Helpers;

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

        public async Task<Pedido> GetByIdAsync(string pedidoId)
        {
            if (!int.TryParse(pedidoId, out int id))
            {
                throw new BadRequestException(BadRequestMessages.InvalidIdentifier.ToMessage());
            }

            var pedido = await this.pedidoService.GetByIdAsync(id);

            if (pedido == null)
            {
                throw new NotFoundException(NotFoundMessages.PedidoNotFound.ToMessage());
            }

            return pedido.ToDto();
        }

        public async Task<Pedido> CreateAsync(PedidoRequest request)
        {
            request.Validate();

            var requestMapped = request.ToModel();

            var pedido = await this.pedidoService.CreateAsync(requestMapped);

            return pedido.ToDto();
        }

        public async Task UpdateAsync(string pedidoId, Pedido pedido)
        {
            pedido.Validate(pedidoId);
            var id = int.Parse(pedidoId);

            var pedidoSaved = await this.pedidoService.GetByIdAsync(id);

            if (pedidoSaved == null)
            {
                throw new NotFoundException(NotFoundMessages.PedidoNotFound.ToMessage());
            }

            var pedidoMapped = pedido.ToModel();

            await this.pedidoService.UpdateAsync(pedidoMapped);
        }

        public async Task DeleteAsync(string pedidoId)
        {
            if (!int.TryParse(pedidoId, out int id))
            {
                throw new BadRequestException(BadRequestMessages.InvalidIdentifier.ToMessage());
            }

            var pedidoSaved = await this.pedidoService.GetByIdAsync(id);

            if (pedidoSaved == null)
            {
                throw new NotFoundException(NotFoundMessages.PedidoNotFound.ToMessage());
            }

            await this.pedidoService.DeleteAsync(pedidoSaved);
        }
    }
}
