using System;
using System.Linq;
using RentalSystem.Domain;
using RentalSystem.Services;

namespace RentalSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new RentalService();

            var laptop1 = new Laptop("Dell XPS", 16, "i7");
            var projector1 = new Projector("Epson X100", 3000, "1080p");
            var camera1 = new Camera("Sony A7", true, "E-mount");
            service.AddEquipment(laptop1);
            service.AddEquipment(projector1);
            service.AddEquipment(camera1);

            var student = new Student("Jan", "Kowalski");
            var employee = new Employee("Anna", "Nowak");
            service.AddUser(student);
            service.AddUser(employee);

            Console.WriteLine("--- SCENARIUSZ DEMONSTRACYJNY ---");

            Console.WriteLine("Wypozyczam laptopa studentowi...");
            service.RentEquipment(student, laptop1, TimeSpan.FromDays(7));
            
            try
            {
                Console.WriteLine("Proba wypozyczenia niedostepnego laptopa pracownikowi...");
                service.RentEquipment(employee, laptop1, TimeSpan.FromDays(3));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BLAD: {ex.Message}");
            }

            service.RentEquipment(student, projector1, TimeSpan.FromDays(2));
            try
            {
                Console.WriteLine("Proba przekroczenia limitu przez studenta...");
                service.RentEquipment(student, camera1, TimeSpan.FromDays(1));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BLAD: {ex.Message}");
            }

            var activeRentals = service.GetActiveRentalsForUser(student).ToList();

            Console.WriteLine("\nStudent zwraca projektor w terminie...");
            service.ReturnEquipment(activeRentals[1], DateTime.Now);
            Console.WriteLine($"Kara za projektor: {activeRentals[1].Penalty} PLN");

            Console.WriteLine("Student zwraca laptopa 5 dni po terminie...");
            var lateReturnDate = activeRentals[0].DueDate.AddDays(5);
            service.ReturnEquipment(activeRentals[0], lateReturnDate);
            Console.WriteLine($"Kara za spozniony laptop: {activeRentals[0].Penalty} PLN");

            Console.WriteLine();
            service.PrintSummaryReport();
        }
    }
}