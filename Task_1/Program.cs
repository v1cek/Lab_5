using System;
using System.Collections.Generic;

// Абстрактний клас Vehicle
abstract class Vehicle
{
    public int Speed { get; set; }
    public int Capacity { get; set; }
    public int CurrentPassengers { get; private set; } = 0;

    // Метод для посадки пасажирів
    public virtual void BoardPassengers(int passengers)
    {
        if (CurrentPassengers + passengers > Capacity)
        {
            Console.WriteLine($"{GetType().Name}: Не можна посадити {passengers} пасажирів. Місць лишилось: {Capacity - CurrentPassengers}.");
        }
        else
        {
            CurrentPassengers += passengers;
            Console.WriteLine($"{GetType().Name}: Посадили {passengers} пасажирів. Пасажирів на борту: {CurrentPassengers}.");
        }
    }

    // Метод для висадки пасажирів
    public virtual void UnboardPassengers(int passengers)
    {
        if (passengers > CurrentPassengers)
        {
            Console.WriteLine($"{GetType().Name}: Неможливо висадити {passengers} пасажирів. На борту: {CurrentPassengers}.");
        }
        else
        {
            CurrentPassengers -= passengers;
            Console.WriteLine($"{GetType().Name}: Висадили {passengers} пасажирів. Залишилось: {CurrentPassengers}.");
        }
    }

    public abstract void Move();
}

// Клас Car
class Car : Vehicle
{
    public override void Move()
    {
        Console.WriteLine("Автомобіль рухається дорогою.");
    }
}

// Клас Bus
class Bus : Vehicle
{
    public override void Move()
    {
        Console.WriteLine("Автобус їде по маршруту.");
    }
}

// Клас Train
class Train : Vehicle
{
    public override void Move()
    {
        Console.WriteLine("Поїзд рухається по рейках.");
    }
}

// Клас Route
class Route
{
    public string Start { get; set; }
    public string End { get; set; }
    public double Distance { get; set; }

    public Route(string start, string end, double distance)
    {
        Start = start;
        End = end;
        Distance = distance;
    }

    // Розрахунок часу для заданого транспорту
    public double CalculateTime(Vehicle vehicle)
    {
        if (vehicle.Speed <= 0)
        {
            throw new ArgumentException("Швидкість транспорту повинна бути більше нуля.");
        }

        return Distance / vehicle.Speed;
    }
}

// Клас TransportNetwork
class TransportNetwork
{
    private List<Vehicle> vehicles = new List<Vehicle>();

    public void AddVehicle(Vehicle vehicle)
    {
        vehicles.Add(vehicle);
    }

    public void MoveAll()
    {
        Console.WriteLine("\nРух транспорту:");
        foreach (var vehicle in vehicles)
        {
            vehicle.Move();
        }
    }

    public void SimulatePassengerFlow(Route route, Dictionary<Vehicle, (int board, int unboard)> passengerData)
    {
        Console.WriteLine($"\n--- Симуляція руху за маршрутом {route.Start} -> {route.End} ({route.Distance} км) ---");

        foreach (var entry in passengerData)
        {
            Vehicle vehicle = entry.Key;
            (int board, int unboard) = entry.Value;

            Console.WriteLine($"\n{vehicle.GetType().Name}:");
            vehicle.BoardPassengers(board);
            vehicle.Move();
            vehicle.UnboardPassengers(unboard);

            double time = route.CalculateTime(vehicle);
            Console.WriteLine($"Час у дорозі: {time:F2} год.");
        }
    }
}

class Program
{
    static void Main()
    {
        // Створюємо транспортні засоби
        var car = new Car { Speed = 120, Capacity = 4 };
        var bus = new Bus { Speed = 60, Capacity = 40 };
        var train = new Train { Speed = 150, Capacity = 200 };

        // Додаємо транспорт у мережу
        var network = new TransportNetwork();
        network.AddVehicle(car);
        network.AddVehicle(bus);
        network.AddVehicle(train);

        // Створюємо маршрут
        var route = new Route("Київ", "Львів", 540);

        // Симуляція руху транспорту з посадкою та висадкою пасажирів
        var passengerData = new Dictionary<Vehicle, (int board, int unboard)>
        {
            { car, (3, 2) },
            { bus, (35, 10) },
            { train, (150, 100) }
        };

        network.SimulatePassengerFlow(route, passengerData);
    }
}
