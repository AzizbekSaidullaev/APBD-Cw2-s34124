using System;

namespace RentalSystem.Domain
{
    public class Rental
    {
        public User RentedBy { get; }
        public Equipment RentedEquipment { get; }
        public DateTime RentDate { get; }
        public DateTime DueDate { get; }
        public DateTime? ActualReturnDate { get; private set; }
        public decimal Penalty { get; private set; } = 0;

        public Rental(User user, Equipment equipment, DateTime rentDate, TimeSpan duration)
        {
            RentedBy = user;
            RentedEquipment = equipment;
            RentDate = rentDate;
            DueDate = rentDate.Add(duration);
        }

        public void MarkAsReturned(DateTime returnDate, decimal penaltyAmount)
        {
            ActualReturnDate = returnDate;
            Penalty = penaltyAmount;
        }
    }
}