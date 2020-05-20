using System;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            new Client().Main();
        }
    }

    public class Client
    {
        public void Main()
        {
            // The client code can work with any concrete factory class.
            Console.WriteLine("Client: Testing client code with the first factory type...");
            ClientMethod(new ConcreteFactory1());
            Console.WriteLine();

            Console.WriteLine("Client: Testing the same client code with the second factory type...");
            ClientMethod(new ConcreteFactory2());
        }

        public void ClientMethod(IAbstractFactory factory)
        {
            var productA = factory.CreateProductA();
            var productB = factory.CreateProductB();

            Console.WriteLine(productB.UsefulFunctionB());
            Console.WriteLine(productB.AnotherUsefulFunctionB(productA));
        }
    }

    public interface IAbstractFactory
    {
        IAbstractProductA CreateProductA();

        IAbstractProductB CreateProductB();
    }

    public class ConcreteFactory1: IAbstractFactory
    {
        public IAbstractProductA CreateProductA()
        {
            return new ConcreateProductA1();
        }

        public IAbstractProductB CreateProductB()
        {
            return new ConcreateProductB1();
        }
    }

    public class ConcreteFactory2 : IAbstractFactory
    {
        public IAbstractProductA CreateProductA()
        {
            return new ConcreateProductA2();
        }

        public IAbstractProductB CreateProductB()
        {
            return new ConcreateProductB2();
        }
    }

    public interface IAbstractProductA
    {
        string UsefulFunctionA();
    }

    public interface IAbstractProductB
    {
        string UsefulFunctionB();

        string AnotherUsefulFunctionB(IAbstractProductA collaborator);
    }

    public class ConcreateProductA1: IAbstractProductA
    {
        public string UsefulFunctionA()
        {
            return "The result of the product A1.";
        }
    }

    public class ConcreateProductA2: IAbstractProductA
    {
        public string UsefulFunctionA()
        {
            return "The result of the product A1.";
        }
    }

    public class ConcreateProductB1: IAbstractProductB
    {
        public string UsefulFunctionB()
        {
            return "The result of the product B1.";
        }

        public string AnotherUsefulFunctionB(IAbstractProductA collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"The result of the B1 collaborating with the ({result})";
        }
    }

    public class ConcreateProductB2 : IAbstractProductB
    {
        public string UsefulFunctionB()
        {
            return "The result of the product B2.";
        }

        public string AnotherUsefulFunctionB(IAbstractProductA collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"The result of the B2 collaborating with the ({result})";
        }
    }
}
