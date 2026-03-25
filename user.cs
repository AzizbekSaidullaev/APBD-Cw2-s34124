using System;

namespace RentalSystem.Domain
{
    public abstract class User
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string FirstName { get; }
        public string LastName { get; }
        public abstract int MaxActiveRentals { get; }

        protected User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }

    public class Student : User
    {
        public override int MaxActiveRentals => 2;
        public Student(string firstName, string lastName) : base(firstName, lastName) { }
    }

    public class Employee : User
    {
        public override int MaxActiveRentals => 5;
        public Employee(string firstName, string lastName) : base(firstName, lastName) { }
    }
}