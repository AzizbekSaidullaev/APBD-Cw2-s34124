using System;
using System.Collections.Generic;
using System.Linq;
using RentalSystem.Domain;

namespace RentalSystem.Services
{
    public class RentalService
    {
        private readonly List<User> _users = new();
        private readonly List<Equipment> _equipment = new();
        private readonly List<Rental> _rentals = new();
        private const decimal DailyPenaltyRate = 10.0m;

        public void AddUser(User user) => _users.Add(user);
        public void AddEquipment(Equipment eq) => _equipment.Add(eq);

        public IEnumerable<Equipment> GetAllEquipment() => _equipment;
        public IEnumerable<Equipment> GetAvailableEquipment() => _equipment.Where(e => e.IsAvailable);

        public void RentEquipment(User user, Equipment equipment, TimeSpan duration)
        {
            if (!equipment.IsAvailable)
                throw new InvalidOperationException($"Sprzet {equipment.Name} jest niedostepny.");

            int activeRentalsCount = _rentals.Count(r => r.RentedBy.Id == user.Id && r.ActualReturnDate == null);
            if (activeRentalsCount >= user.MaxActiveRentals)
                throw new InvalidOperationException($"Uzytkownik przekroczyl limit wypozyczen ({user.MaxActiveRentals}).");

            var rental = new Rental(user, equipment, DateTime.Now, duration);
            _rentals.Add(rental);
            equipment.IsAvailable = false;
        }

        public void ReturnEquipment(Rental rental, DateTime returnDate)
        {
            decimal penalty = 0;
            if (returnDate > rental.DueDate)
            {
                var daysLate = (returnDate - rental.DueDate).Days;
                penalty = daysLate * DailyPenaltyRate;
            }

            rental.MarkAsReturned(returnDate, penalty);
            rental.RentedEquipment.IsAvailable = true;
        }

        public void MarkAsBroken(Equipment equipment)
        {
            equipment.IsAvailable = false;
        }

        public IEnumerable<Rental> GetActiveRentalsForUser(User user)
        {
            return _rentals.Where(r => r.RentedBy.Id == user.Id && r.ActualReturnDate == null);
        }

        public IEnumerable<Rental> GetOverdueRentals(DateTime currentDate)
        {
            return _rentals.Where(r => r.ActualReturnDate == null && currentDate > r.DueDate);
        }

        public void PrintSummaryReport()
        {
            Console.WriteLine("=== RAPORT WYPOZYCZALNI ===");
            Console.WriteLine($"Calkowita liczba sprzetu: {_equipment.Count}");
            Console.WriteLine($"Sprzet dostepny: {_equipment.Count(e => e.IsAvailable)}");
            Console.WriteLine($"Aktywne wypozyczenia: {_rentals.Count(r => r.ActualReturnDate == null)}");
            Console.WriteLine("===========================");
        }
    
    }
}