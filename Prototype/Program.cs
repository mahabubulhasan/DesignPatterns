using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Prototype
{
    [Serializable]
    class Person
    {
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }

        public Car ShallowCopy()
        {
            return (Car)this.MemberwiseClone();
        }

    }

    [Serializable]
    class Address
    {
        public string area;
        public int houseNo;

        public Address(string area, int houseNo)
        {
            this.area = area;
            this.houseNo = houseNo;
        }
    }

    public static class Extensions
    {
        public static T DeepCopy<T>(this T self)
        {
            using var stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }
    }

    class Program
    {
        /// <summary>
        /// see detaisl here
        /// https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone?view=netcore-3.1#System_Object_MemberwiseClone
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            // Serialization Approch
            var person1 = new Person()
            {
                Name = "Mahabubul Hasan",
                Age = 35,
                DateOfBirth = DateTime.Today,
                Address = new Address("Mohammadi Housing Society", 131)
            };

            var person2 = person1.DeepCopy();
            person2.Address.area = "Mohammadpur";

            Console.WriteLine(person1.Address.area);
            Console.WriteLine(person2.Address.area);

            // Alternate approch

            var car1 = new Car() { Brand = "Toyota", Model = new Model() { Year = "2019" } };
            var car2 = car1.DeepCopy();
            car2.Model.Year = "2020";

            Console.WriteLine(car1.Model.Year);
            Console.WriteLine(car2.Model.Year);

        }
    }
}
