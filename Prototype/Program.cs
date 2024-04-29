using System;
using System.Text.Json;

namespace Prototype
{
    class Person
    {
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    class Address
    {
        public string Area { get; set; }
        public int HouseNo { get; set; }
    }

    public static class Extensions
    {
        public static T DeepCopy<T>(this T self)
        {
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };
            var jsonString = JsonSerializer.Serialize(self);
            return JsonSerializer.Deserialize<T>(jsonString);
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
            var person1 = new Person
            {
                Name = "Mahabubul Hasan",
                Age = 35,
                DateOfBirth = DateTime.Today,
                Address = new Address { Area = "Mohammadi Housing Society", HouseNo = 131 }
            };

            var person2 = person1.DeepCopy();
            person2.Address.Area = "Mohammadpur";

            Console.WriteLine(person1.Address.Area);
            Console.WriteLine(person2.Address.Area);

            // Alternate approch
            var car1 = new Car { Brand = "Toyota", Model = new Model { Year = "2019" } };
            var car2 = car1.DeepCopy();
            car2.Model.Year = "2020";

            Console.WriteLine(car1.Model.Year);
            Console.WriteLine(car2.Model.Year);

        }
    }
}
