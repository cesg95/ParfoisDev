namespace ParfoisDev.Controllers
{
    using Application.Dto;
    using Application.Dto.Requests;
    using Application.Services.Interfaces;

    using Infrastructure.CrossCutting.Exceptions;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService statusService;

        public StatusController(IStatusService statusService)
        {
            this.statusService = statusService;
        }

        [HttpPost]
        public async Task<ActionResult<PedidoWorkflow>> PostAsync(StatusRequest request)
        {
            try
            {
                var workflow = await this.statusService.GetPedidoWorkflowAsync(request);

                return this.Ok(workflow);
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
