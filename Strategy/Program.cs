namespace Strategy;

public class Program
{
    public static void Main()
    {
        Duck mallard = new MallardDuck();
        Perform(mallard);

        Duck rubber = new RubberDuck();
        Perform(rubber);

        rubber.QuackBehavior = new MuteQuack();
        Perform(rubber);
    }

    public static void Perform(Duck duck)
    {
        duck.PerformFly();
        duck.PerformQuack();
        duck.Display();
        Console.WriteLine("\n");
    }
}

public abstract class Duck
{
    public IFlyBehavior FlyBehavior { get; set; } = null!;
    public IQuackBehavior QuackBehavior { get; set; } = null!;

    public void Swim()
    {
        Console.WriteLine("All ducks float, even decoys!");
    }

    public abstract void Display();

    public void PerformFly() => FlyBehavior.Fly();

    public void PerformQuack() => QuackBehavior.Quack();

}

public interface IFlyBehavior
{
    public void Fly();
}

public class FlyWithWings : IFlyBehavior
{
    public void Fly() => Console.WriteLine("I am flying!!");
}

public class FlyNoWay: IFlyBehavior
{
    public void Fly() => Console.WriteLine("I can't fly");
}

public interface IQuackBehavior
{
    public void Quack();
}


public class SayQuack: IQuackBehavior
{
    public void Quack() => Console.WriteLine("Quack");
}

public class SaySqueak: IQuackBehavior
{
    public void Quack() => Console.WriteLine("Squeak");
}

public class MuteQuack: IQuackBehavior
{
    public void Quack() => Console.WriteLine("<<Silence>>");
}

public class MallardDuck: Duck
{
    public MallardDuck()
    {
        FlyBehavior = new FlyWithWings();
        QuackBehavior = new SayQuack();
    }

    public override void Display()
    {
        Console.WriteLine($"I'm a real {nameof(MallardDuck)}");
    }
}

public class RubberDuck: Duck
{
    public RubberDuck()
    {
        FlyBehavior = new FlyNoWay();
        QuackBehavior = new SaySqueak();
    }

    public override void Display()
    {
        Console.WriteLine($"I'm a real {nameof(RubberDuck)}");
    }
}