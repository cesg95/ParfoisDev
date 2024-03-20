namespace ParfoisDev.Controllers
{
    using Application.Dto;
    using Application.Dto.Requests;
    using Application.Services.Interfaces;

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
        public async Task<ActionResult<Pedido>> GetByIdAsync(int id)
        {
            var pedido = await this.pedidoService.GetByIdAsync(id);

            if (pedido == null)
            {
                return this.NotFound();
            }

            return pedido;
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> PostAsync(PedidoRequest request)
        {
            var pedido = await this.pedidoService.CreateAsync(request);

            return this.CreatedAtRoute("GetById", new { id = pedido.Id }, pedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Pedido pedido)
        {
            await this.pedidoService.UpdateAsync(id, pedido);

            // TODO not found e bad request

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await this.pedidoService.DeleteAsync(id);

            // TODO not found

            return this.NoContent();
        }
    }
}
