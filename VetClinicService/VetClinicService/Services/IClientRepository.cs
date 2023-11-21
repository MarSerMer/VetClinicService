using VetClinicService.Models;
using VetClinicService.Services;

namespace VetClinicService.Services
{
    public interface IClientRepository : IRepository<Client, int>
    {
    }
}
