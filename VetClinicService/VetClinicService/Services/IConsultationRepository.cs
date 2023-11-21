using VetClinicService.Models;
using VetClinicService.Services;

namespace VetClinicService.Services
{
    public interface IConsultationRepository : IRepository<Consultation, int>
    {
    }
}
