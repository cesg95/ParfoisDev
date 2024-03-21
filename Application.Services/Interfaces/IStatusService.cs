namespace Application.Services.Interfaces
{
    using System.Threading.Tasks;

    using Application.Dto;
    using Application.Dto.Requests;

    public interface IStatusService
    {
        Task<PedidoWorkflow> CreateAsync(StatusRequest request);
    }
}
