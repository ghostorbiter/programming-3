using System;

namespace Vehicles
{
    abstract class Vehicle
    {
        public static int UniqueID;

        protected Vehicle()
        {
            ++UniqueID;
        }

        public abstract void Travel(double km);
        public abstract void Beep();

        public override string ToString()
        {
            return base.ToString();
        }
    }

    class Car : Vehicle
    {
        private string name;
        private int ID;
        public Car(string _name)
        {
            name = _name;
            ID = UniqueID;
            Console.WriteLine($"Car ({ID}) {{ " + name + " } was created");
        }

        public override void Travel(double km)
        {
            Console.WriteLine($"Car {name} traveled {km} km");
        }

        public override void Beep()
        {
            Console.WriteLine($"Car {name} beeps!");
        }

        public override string ToString()
        {
            return string.Format("Car ({0}) {{ {1} }}", ID, name);
        }
    }

    class Bus : Vehicle
    {
        private string name;
        private int ID;
        public const int passengerLimit = 42;
        private uint passengers = 0;


        public Bus(string _name)
        {
            name = _name;
            ID = UniqueID;
            Console.WriteLine($"Bus ({ID}) {{ " + name + $" }} [{passengers}/{passengerLimit}] was created");
        }

        public bool SetPassengerCount(uint pass)
        {
            if (PassengerCount() + pass > passengerLimit)
                return false;
            else
            {
                passengers += pass;
                return true;
            }
        }

        public uint PassengerCount()
        {
            return passengers;
        }

        public override void Travel(double km)
        {
            Console.WriteLine($"Bus {name} traveled {km} km with {passengers} passengers");
        }

        public override void Beep()
        {
            Console.WriteLine($"Bus {name} beeps!");
        }

        public override string ToString()
        {
            return string.Format("Bus ({0}) {{ {1} }} [ {2}/{3} ]", ID, name, passengers, passengerLimit);
        }
    }

    class Truck : Vehicle
    {
        private string name;
        private int ID;
        public const int MaxLoad = 2500;
        private double capacity = 0;
        public Truck(string _name)
        {
            name = _name;
            ID = UniqueID;
            Console.WriteLine($"Truck ({ID}) {{ " + name + $" }} [{capacity}/{MaxLoad}] was created");
        }

        public bool SetLoad(double _load)
        {
            if (Load() + _load > MaxLoad)
                return false;
            else
            {
                capacity += _load;
                return true;
            }
        }

        public double Load()
        {
            return capacity;
        }

        public override void Travel(double km)
        {
            Console.WriteLine($"Truck {name} traveled {km} km with load of {capacity} kg");
        }

        public override void Beep()
        {
            Console.WriteLine($"Truck {name} beeps with loud trumpet!");
        }

        public override string ToString()
        {
            return string.Format("Car ({0}) {{ {1} }} [ {2}/{3} ]", ID, name, capacity, MaxLoad);
        }
    }
}

