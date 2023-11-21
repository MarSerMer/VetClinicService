using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetClinicService.Models;
using VetClinicService.Models.Requests;
using VetClinicService.Services;

namespace VetClinicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        private IConsultationRepository _consultationRepository;

        public ConsultationController(IConsultationRepository consultationRepository)
        {
            _consultationRepository = consultationRepository;
        }


        [HttpPost("create", Name = "CreateConsultation")]
        public ActionResult<int> Create([FromBody] CreateConsultationRequest createRequest)
        {
            Consultation consultation = new Consultation();
            consultation.ClientId = createRequest.ClientId;
            consultation.PetId = createRequest.PetId;
            consultation.ConsultationDate = createRequest.ConsultationDate;
            consultation.Description = createRequest.Description;
            return Ok(_consultationRepository.Create(consultation));
        }

        [HttpPut("edit", Name = "EditConsultation")]
        public ActionResult<int> Update([FromBody] UpdateConsultationRequest updateRequest)
        {
            Consultation consultation = new Consultation();
            consultation.ConsultationId = updateRequest.ConsultationId;
            consultation.ClientId = updateRequest.ClientId;
            consultation.PetId = updateRequest.PetId;
            consultation.ConsultationDate = updateRequest.ConsultationDate;
            consultation.Description = updateRequest.Description;
            return Ok(_consultationRepository.Update(consultation));
        }


        [HttpDelete("delete", Name = "DeleteConsultation")]
        public ActionResult<int> Delete([FromQuery] int consultationId)
        {
            int res = _consultationRepository.Delete(consultationId);
            return Ok(res);
        }

        [HttpGet("get-all", Name = "GetAllConsultations")]
        public ActionResult<List<Consultation>> GetAll()
        {
            return Ok(_consultationRepository.GetAll());
        }


        [HttpGet("get/{consultationId}", Name = "GetConsultationById")]
        public ActionResult<Consultation> GetById([FromRoute] int consultationId)
        {
            return Ok(_consultationRepository.GetById(consultationId));
        }

    }
}
