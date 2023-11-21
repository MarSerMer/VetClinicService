using VetClinicNamespace;
namespace VetClinicConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Нажмите любую клавишу для загрузки данных...");
            Console.ReadKey();

            VetClinicClient vetClinicClient = new VetClinicClient(" http://localhost:5285/", new HttpClient());

            List<Client> clients = vetClinicClient.GetAllClientsAsync().Result.ToList();
            foreach(Client client in clients)
            {
                Console.WriteLine("Клиент: " + client.FirstName + " " + client.SurName);
                Console.WriteLine();
            }

            Client clt = vetClinicClient.GetClientByIdAsync(3).Result;
            Console.WriteLine("Поиск одного клиента:");
            Console.WriteLine("Клиент: " + clt.FirstName + " " + clt.SurName);
            Console.WriteLine();

            List<Pet> pets = vetClinicClient.GetAllPetsAsync().Result.ToList();
            foreach(Pet pet in pets)
            {
                Console.WriteLine("Пациент клиники: " + pet.Name);
                Console.WriteLine();
            }

            Pet pt = vetClinicClient.GetPetByIdAsync(2).Result;
            Console.WriteLine("Поиск одного питомца:");
            Console.WriteLine("Питомец: " + pt.Name);
            Console.WriteLine();

            List<Consultation> consultations = vetClinicClient.GetAllConsultationsAsync().Result.ToList();
            foreach(Consultation cons in consultations)
            {
                Console.WriteLine("Проведённая консультация: " + cons.ConsultationId + " / " + cons.Description);
                Console.WriteLine();
            }


            Console.WriteLine("Нажмите любую клавишу для завершения работы программы");
            Console.ReadKey();
        }
    }
}