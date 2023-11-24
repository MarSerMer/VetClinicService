using VetClinicService.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using VetClinicService.Models;
using VetClinicService.Services;
using VetClinicService.Services.Impl;
using System.Reflection.Metadata;

namespace VetClinicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private IPetRepository _petRepository;

        public PetController(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }


        [HttpPost("create", Name = "CreatePet")]
        public ActionResult<int> Create([FromBody] CreatePetRequest createRequest)
        {
            if (createRequest.ClientId < 0) 
                return Ok(new
                {
                    ErrCode = -21,
                    ErrMessage = "Хозяин животного указан неверно."
                });
            if (string.IsNullOrEmpty(createRequest.Name))
                return Ok(new
                {
                    ErrCode = -22,
                    ErrMessage = "Не указано имя животного."
                });

            Pet pet = new Pet();
            pet.ClientId = createRequest.ClientId;
            pet.Name = createRequest.Name;
            pet.Birthday = createRequest.Birthday;
            return Ok(_petRepository.Create(pet));
        }

        [HttpPut("edit", Name = "EditPet")]
        public ActionResult<int> Update([FromBody] UpdatePetRequest updateRequest)
        {
            Pet pet = new Pet();
            pet.PetId = updateRequest.PetId;
            pet.ClientId = updateRequest.ClientId;
            pet.Name = updateRequest.Name;
            pet.Birthday = updateRequest.Birthday;
            return Ok(_petRepository.Update(pet));
        }


        [HttpDelete("delete", Name = "DeletePet")]
        public ActionResult<int> Delete([FromQuery] int petId)
        {
            int res = _petRepository.Delete(petId);
            return Ok(res);
        }

        [HttpGet("get-all", Name = "GetAllPets")]
        public ActionResult<List<Pet>> GetAll()
        {
            return Ok(_petRepository.GetAll());
        }


        [HttpGet("get/{petId}", Name = "GetPetById")]
        public ActionResult<Pet> GetById([FromRoute] int petId)
        {
            return Ok(_petRepository.GetById(petId));
        }
    }
}
