using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5_EngB
{
    /* Person Implementation Here */
    struct Person
    {
        public int weight;
        public string name;
        public string surname;

        public Person(string _name, string _surname, int _weight = 75)
        {
            weight = _weight;
            name = _name;
            surname = _surname;
        }

        public override string ToString()
        {
            return $"{name} {surname}";
        }

        public void Deconstruct(out string _name, out string _surname)
        {
            _name = name;
            _surname = surname;
        }
    }

    class Plane
    {
        public enum Class
        {
            Business,
            Economy,
        }

        /* Plane Implementation Here */

        public readonly int Mass;
        public readonly int Torque;
        public readonly int HorsePower;
        public readonly int TankCapacity;

        protected double FlightHours;
        protected double MaxSpeed;
        protected int[] MaxCapacity;
        protected Person[][] Passengers;
        protected Person[] Crew;

        private int BusinessCounter;
        private int EconomyCouter;

        public Plane(int mass, int torque, int horsePower, int tankCapacity, int[] maxCapacity, params Person[] crew)
        {
            Mass = mass;
            Torque = torque;
            HorsePower = horsePower;
            TankCapacity = tankCapacity;
            MaxCapacity = maxCapacity;
            FlightHours = 0.0;
            MaxSpeed = 700.0;
            Crew = crew;
            Passengers = new Person[2][] { new Person[MaxCapacity[0]], new Person[MaxCapacity[1]] };
        }

        public void PrintInfo(bool printPassengers = true)
        {
            Console.WriteLine($"Flight Hours: {FlightHours}\nMass: {Mass}\nTorque: {Torque}\nHorse Power: {HorsePower}\nMax Capcity Business: {MaxCapacity[0]}\nMax Capacity Economy: {MaxCapacity[1]}\nTank Capacity: {TankCapacity}");
            if (printPassengers)
            {
                Console.WriteLine("Business Passangers:");
                for (int i = 0; i < BusinessCounter; i++)
                {
                    Console.WriteLine($"    {Passengers[0][i]}");
                }
                Console.WriteLine("Economy Passangers:");
                for (int i = 0; i < EconomyCouter; i++)
                {
                    Console.WriteLine($"    {Passengers[1][i]}");
                }
            }
        }

        public void Deconstruct(out int torque, out int horsepower, out int tankcapacity)
        {
            torque = Torque;
            horsepower = HorsePower;
            tankcapacity = TankCapacity;
        }

        public double CalculateRange()
        {
            double totalMass = Mass;
            for (int i = 0; i < BusinessCounter; i++)
                totalMass += Passengers[0][i].weight;

            for (int i = 0; i < EconomyCouter; i++)
                totalMass += Passengers[1][i].weight;

            //for (int i = 0; i < Crew.Length; i++)
            //    totalMass += Crew[i].weight;

            return TankCapacity * (Torque + HorsePower) / (totalMass / 17.46);
        }

        public bool AddPassenger(Class _class, Person person)
        {
            if (_class == Class.Business)
            {
                if (BusinessCounter < MaxCapacity[0])
                {
                    Passengers[0][BusinessCounter++] = person;
                    return true;
                }
                else
                    return false;
            }
            else if (_class == Class.Economy)
            {
                if (EconomyCouter < MaxCapacity[1])
                {
                    Passengers[1][EconomyCouter++] = person;
                    return true;
                }
                else return false;
            }
            else
                return false;
        }

        public bool AddBusinessPassenger(Person person)
        {
            if (BusinessCounter < MaxCapacity[0])
            {
                Passengers[0][BusinessCounter++] = person;
                return true;
            }
            else
                return false;
        }
        public bool AddEconomyPassenger(Person person)
        {
            if (EconomyCouter < MaxCapacity[1])
            {
                Passengers[1][EconomyCouter++] = person;
                return true;
            }
            else return false;
        }

        public (bool reach, double remaining) Travel(double flight)
        {
            double maxRange = CalculateRange();
            double remainingTime = maxRange / MaxSpeed;
            double Final = remainingTime - FlightHours;
            FlightHours += flight;

            if (Final >= flight)
                return (true, 0);
            else
                return (false, flight - Final);
        }

        public bool AddPassengers((Person person, Class class_)[] p)
        {
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i].class_ == Class.Business)
                {
                    if (!AddBusinessPassenger(p[i].person))
                        return false;
                }

                else if (p[i].class_ == Class.Economy)
                {
                    if (!AddEconomyPassenger(p[i].person))
                        return false;
                }

                else
                    return false;
            }

            return true;
        }
    }
}
