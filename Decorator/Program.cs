namespace Decorator;

public class Program
{
    public static void Main()
    {
        Beverage espresso = new Espresso();
        Console.WriteLine($"{espresso.Description}, ${espresso.Cost()}");

        Beverage houseblend = new HouseBlend();
        houseblend = new Mocha(houseblend);
        houseblend = new Mocha(houseblend);
        Console.WriteLine($"{houseblend.Description}, ${houseblend.Cost()}");
    }
}

public abstract class Beverage
{
    protected string _description = "Unknown Beverage";
    public string Description => _description;

    public abstract double Cost();
}

public abstract class CondimentDecorator : Beverage
{
    protected Beverage _beverage = null!;
}

public class Espresso : Beverage {
    public Espresso()
    {
        _description = "Espresso";
    }

    public override double Cost() => 1.99;
}

public class HouseBlend : Beverage
{
    public HouseBlend()
    {
        _description = "House blend coffee";
    }

    public override double Cost() => .89;
}

public class Mocha : CondimentDecorator
{
    public Mocha(Beverage beverage)
    {
        _beverage = beverage;
        _description = _beverage.Description + ", Mocha";
    }

    public override double Cost() => _beverage.Cost() + .20;
}