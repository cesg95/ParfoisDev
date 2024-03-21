namespace ParfoisDev.Controllers
{
    using Application.Dto;
    using Application.Dto.Requests;
    using Application.Services.Interfaces;

    using Infrastructure.CrossCutting.ErrorMessages;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Helpers;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            this.pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<IEnumerable<Pedido>> GetAsync()
        {
            return await this.pedidoService.GetAllAsync();
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<Pedido>> GetByIdAsync(string id)
        {
            try
            {
                var pedido = await this.pedidoService.GetByIdAsync(id);

                if (pedido == null)
                {
                    return this.NotFound(NotFoundMessages.PedidoNotFound.ToMessage());
                }

                return pedido;
            }
            catch (NotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> PostAsync(PedidoRequest request)
        {
            try
            {
                var pedido = await this.pedidoService.CreateAsync(request);

                return this.CreatedAtRoute("GetById", new { id = pedido.Id }, pedido);
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, Pedido pedido)
        {
            try
            {
                await this.pedidoService.UpdateAsync(id, pedido);

                return this.NoContent();
            }
            catch (NotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                await this.pedidoService.DeleteAsync(id);

                return this.NoContent();
            }
            catch (NotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
