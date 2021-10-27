using System;
using System.Collections.Generic;

namespace Observer;

class Program
{
    public static void Main()
    {
        WeatherStation();
    }

    public static void WeatherStation()
    {
        var weatherData = new WeatherData();
        var currentDisplay = new CurrentConditionDisplay();
        var staticDisplay = new StaticDisplay();

        weatherData.AddObserver(currentDisplay);
        weatherData.AddObserver(staticDisplay);

        weatherData.SetMeasurement(1, 2, 3);
        weatherData.SetMeasurement(2, 5, 6);
        weatherData.SetMeasurement(3, 9, 8);
    }
}

public interface Subject
{
    public void AddObserver(Observer observer);
    public void RemoveObserver(Observer observer);
    public void Notify();
}

public interface Observer
{
    public void Update(float temparature, float humidity, float pressure);
}

public interface DisplayElement
{
    public void Display();
}

public class WeatherData : Subject
{
    private List<Observer> _obseverList = new();
    private float _temparature;
    private float _humidity;
    private float _pressure;

    public void AddObserver(Observer observer)
    {
        _obseverList.Add(observer);
    }

    public void Notify()
    {
        foreach (var observer in _obseverList)
        {
            observer.Update(_temparature, _humidity, _pressure);
        }
    }

    public void RemoveObserver(Observer observer)
    {
        _obseverList.Remove(observer);
    }

    public void SetMeasurement(float temparature, float humidity, float pressure)
    {
        _temparature = temparature;
        _humidity = humidity;
        _pressure = pressure;
        Notify();
    }
}

public class CurrentConditionDisplay : DisplayElement, Observer
{
    private float _temparature;
    private float _humidity;
    private float _pressure;

    public void Display()
    {
        Console.WriteLine($"{nameof(CurrentConditionDisplay)} \t Temparature: {_temparature}, Humidity: {_humidity}, Pressure: {_pressure}");
    }

    public void Update(float temparature, float humidity, float pressure)
    {
        _temparature = temparature;
        _humidity = humidity;
        _pressure = pressure;

        Display();
    }
}

public class StaticDisplay : DisplayElement, Observer
{
    private float _temparature;
    private float _humidity;
    private float _pressure;

    public void Display()
    {
        Console.WriteLine($"{nameof(StaticDisplay)} \t\t\t Temparature: {_temparature}, Humidity: {_humidity}, Pressure: {_pressure}");
    }

    public void Update(float temparature, float humidity, float pressure)
    {
        _temparature = temparature;
        _humidity = humidity;
        _pressure = pressure;

        Display();
    }
}