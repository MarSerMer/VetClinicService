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
    public class ClientControllerTests
    {
        private ClientController _clientController;
        // далее объявляем "игрушечный" репозиторий с помощью фреймворка Moq - создаём объект класса Mock.
        // он же "заглушка"
        // это делаем для того, чтобы не трогать этими своими тестами настоящий живой репозиторий (а значит, и настоящую живую БД)
        private Mock<IClientRepository> _mockClientRepository;

        public ClientControllerTests()
        {
            _mockClientRepository = new Mock<IClientRepository>();
            _clientController = new ClientController(_mockClientRepository.Object);
        }
        
        [Fact] // ЭТО значит, что следующий метод является методом тестирования
        public void GetAllClientsTest()
        {
            // 1. Подготовка данных для тестирования
            // конкретно прямо тут входных параметров нет, и в этом аспекте подготовка данных не нужна
            // НО надо состряпать что-то правдоподобное вместо настоящего репозитория
            // (мы же заглушку вместо него применяем)

            // стряпаем:
            // создаем список клиентов:
            List<Client> clientsList = new List<Client>();
            clientsList.Add(new Client());
            clientsList.Add(new Client());
            clientsList.Add(new Client());
            // просим заглушку при вызове метода GetAll вернуть нам эту коллекцию
            // (в данном случае repository это просто имя, оно может быть любым)
            _mockClientRepository.Setup(repository => repository.GetAll()).Returns(clientsList);


            // 2. Исполнение тестируемого метода
            // var, т.к. мы не знаем, какой будет тип ответа. Какой будет, такой и будет
            var operationResult = _clientController.GetAll();

            // 3. Подготовка эталонного результата и проверка полученного результата
            Assert.IsType<OkObjectResult>(operationResult.Result);
            var okObjectResult = (OkObjectResult)operationResult.Result;
            Assert.IsAssignableFrom<List<Client>>(okObjectResult.Value);

            // уточняем, а был ли репозиторий вообще хоть раз использован во время проведения теста
            _mockClientRepository.Verify(repository => repository.GetAll(), Times.AtLeastOnce);
        }

        [Fact]
        public void CreateClientWithGoodDatasTest()
        {
            // 1. Подготовка данных для тестирования
            // создаем объект CreateClientRequest
            var createClientRequest = new CreateClientRequest();
            createClientRequest.FirstName = "TestFirstName";
            createClientRequest.SurName = "TestSurName";
            createClientRequest.Patronymic = "TestPatronymic";
            createClientRequest.Birthday = DateTime.Now.AddYears(-21);
            createClientRequest.Document = "TestDataDocument";

            // настраиваем заглушку
            _mockClientRepository.Setup(repository => 
            repository.Create(It.IsNotNull<Client>())).Returns(1).Verifiable();

            // 2. Исполнение тестируемого метода
            var operationResult = _clientController.Create(createClientRequest);

            // 3. Подготовка эталонного результата и проверка полученного результата
            Assert.IsType<OkObjectResult>(operationResult.Result);
            var okObjectResult = (OkObjectResult)operationResult.Result;
            Assert.IsAssignableFrom<int>(okObjectResult.Value);
            _mockClientRepository.Verify(repository =>
            repository.Create(It.IsNotNull<Client>()), Times.AtLeastOnce);
        }
    }
}
