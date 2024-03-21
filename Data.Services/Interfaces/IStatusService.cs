namespace Data.Services.Interfaces
{
    using System.Threading.Tasks;

    using Domain.Model;
    using Domain.Model.Requests;

    public interface IStatusService
    {
        Task<PedidoWorkflow> CreateAsync(StatusRequest request);
    }
}
