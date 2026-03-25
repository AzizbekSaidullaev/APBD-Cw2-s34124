using System;

namespace RentalSystem.Domain
{
    public abstract class Equipment
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public bool IsAvailable { get; set; } = true;

        protected Equipment(string name)
        {
            Name = name;
        }
    }

    public class Laptop : Equipment
    {
        public int RamInGB { get; }
        public string Processor { get; }

        public Laptop(string name, int ram, string processor) : base(name)
        {
            RamInGB = ram;
            Processor = processor;
        }
    }

    public class Projector : Equipment
    {
        public int Lumens { get; }
        public string Resolution { get; }

        public Projector(string name, int lumens, string resolution) : base(name)
        {
            Lumens = lumens;
            Resolution = resolution;
        }
    }

    public class Camera : Equipment
    {
        public bool IsDigital { get; }
        public string MountType { get; }

        public Camera(string name, bool isDigital, string mountType) : base(name)
        {
            IsDigital = isDigital;
            MountType = mountType;
        }
    }
}