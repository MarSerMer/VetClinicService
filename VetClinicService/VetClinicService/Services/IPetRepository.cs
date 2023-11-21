using VetClinicService.Models;
using VetClinicService.Services;

namespace VetClinicService.Services
{
    public interface IPetRepository : IRepository<Pet, int> 
    { 
    }
}
