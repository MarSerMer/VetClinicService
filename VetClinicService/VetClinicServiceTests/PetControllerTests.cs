using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VetClinicService.Controllers;
using VetClinicService.Models;
using VetClinicService.Models.Requests;
using VetClinicService.Services;

namespace VetClinicServiceTests
{
    public class PetControllerTests
    {
        private PetController _petController;
        private Mock<IPetRepository> _mockPetRepository;

        public PetControllerTests()
        {
            _mockPetRepository = new Mock<IPetRepository>();
            _petController = new PetController(_mockPetRepository.Object);
        }


        // Тестирование метода получения списка всех животных


        [Fact] 
        public void GetAllPetsTest()
        {
            // 1. Подготовка данных для тестирования

            List<Pet> petsList = new List<Pet>();
            petsList.Add(new Pet());
            petsList.Add(new Pet());
            petsList.Add(new Pet());

            _mockPetRepository.Setup(repository => repository.GetAll()).Returns(petsList);

            // 2. Исполнение тестируемого метода

            var operationResult = _petController.GetAll();

            // 3. Подготовка эталонного результата и проверка полученного результата
            Assert.IsType<OkObjectResult>(operationResult.Result);
            var okObjectResult = (OkObjectResult)operationResult.Result;
            Assert.IsAssignableFrom<List<Pet>>(okObjectResult.Value);
            _mockPetRepository.Verify(repository => repository.GetAll(), Times.AtLeastOnce);
        }

        [Fact]
        public void CreatePetWithGoodDatasTest()
        {
            // 1. Подготовка данных для тестирования
            var createPetRequest = new CreatePetRequest();
            createPetRequest.ClientId = 15;
            createPetRequest.Name = "TestPetName";
            createPetRequest.Birthday = DateTime.Now.AddYears(-4);
     
            _mockPetRepository.Setup(repository =>
            repository.Create(It.IsNotNull<Pet>())).Returns(1).Verifiable();

            // 2. Исполнение тестируемого метода
            var operationResult = _petController.Create(createPetRequest);

            // 3. Подготовка эталонного результата и проверка полученного результата
            Assert.IsType<OkObjectResult>(operationResult.Result);
            var okObjectResult = (OkObjectResult)operationResult.Result;
            Assert.IsAssignableFrom<int>(okObjectResult.Value);
            _mockPetRepository.Verify(repository =>
            repository.Create(It.IsNotNull<Pet>()), Times.AtLeastOnce);
        }


        // Теперь можно протестировать создание животных на массиве данных.
        // Данные в массиве корректные.


        public static object[][] PetsGoodData =
        {
            new object[] { 77, "Пушок", new DateTime(2010, 1, 22)},
            new object[] { 78, "Барсик", new DateTime(2012, 1, 22)},
            new object[] { 79, "Рекс", new DateTime(2014, 1, 22)},
            new object[] { 80, "Буч", new DateTime(2016, 1, 22)},
            new object[] { 81, "Вилли", new DateTime(2018, 1, 22)},

        };

        [Theory]
        [MemberData(nameof(PetsGoodData))]
        public void CreatePetGoodDataTest(
            int clientId, string name, DateTime birthday)
        {
            // [1] Подготовка данных для тестирования

            CreatePetRequest createPetRequest = new CreatePetRequest();
            createPetRequest.Name = name;
            createPetRequest.Birthday = birthday;
            createPetRequest.ClientId = clientId;

            _mockPetRepository.Setup(repository =>
            repository.Create(It.IsNotNull<Pet>())).Returns(1).Verifiable();

            // [2] Исполнение тестируемого метода

            var operationResult = _petController.Create(createPetRequest);

            // [3] Подготовка эталонного результата, проверка результата
            Assert.IsType<OkObjectResult>(operationResult.Result);
            var okObjectResult = (OkObjectResult)operationResult.Result;
            Assert.IsAssignableFrom<int>(okObjectResult.Value);
            _mockPetRepository.Verify(repository =>
            repository.Create(It.IsNotNull<Pet>()), Times.AtLeastOnce());
        }


        // Тестирование создания животных на массиве некорректных данных


        public static object[][] PetsBadData =
        {
            new object[] { "Пушок", new DateTime(2010, 1, 22), -2 },
            new object[] { "Барсик", new DateTime(2012, 1, 22), -10},
            new object[] { "", new DateTime(2014, 1, 22), 77},
            new object[] { "", new DateTime(2016, 1, 22), 78},
        };

        [Theory]
        [MemberData(nameof(PetsBadData))]
        public void CreatePetBadDataTest(
            string name, DateTime birthday, int clientId)
        {
            // [1] Подготовка данных для тестирования

            CreatePetRequest createPetRequest = new CreatePetRequest();
            createPetRequest.Name = name;
            createPetRequest.Birthday = birthday;
            createPetRequest.ClientId = clientId;

            _mockPetRepository.Setup(repository =>
            repository.Create(It.IsNotNull<Pet>())).Returns(1).Verifiable();

            // [2] Исполнение тестируемого метода

            var operationResult = _petController.Create(createPetRequest);

            // [3] Подготовка эталонного результата, проверка результата
            Assert.IsType<OkObjectResult>(operationResult.Result);
            var okObjectResult = (OkObjectResult)operationResult.Result;

            _mockPetRepository.Verify(repository =>
            repository.Create(It.IsNotNull<Pet>()), Times.Never());
        }


        // Тестирование удаления животного


        [Fact]
        public void DeletePetTest()
        {
            // 1. Подготовка данных для тестирования

            int petId = 2;
            _mockPetRepository.Setup(repository =>
            repository.Delete(It.IsNotNull<int>())).Returns(1).Verifiable();

            // 2. Исполнение тестируемого метода

            var operationResult = _petController.Delete(petId);


            // 3. Подготовка эталонного результата и проверка полученного результата

            Assert.IsType<OkObjectResult>(operationResult.Result);
            var okObjectResult = (OkObjectResult)operationResult.Result;
            Assert.IsAssignableFrom<int>(okObjectResult.Value);
            _mockPetRepository.Verify(repository =>
            repository.Delete(It.IsNotNull<int>()), Times.AtLeastOnce);
        }

    }
}
