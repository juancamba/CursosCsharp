
using CarRentalApi3.Hexagonal.Domain.Abstractions;


namespace CarRentalApi3.Hexagonal.Domain.Cars;

public class Car : Entity<Guid>
{
    private Car() { }
    private Car(Guid id, string brand, string model, decimal price, int year)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Price = price;
        Year = year;
    }


    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public decimal Price { get; set; }  // Price per day
    public int Year { get; set; } // Año de fabricación

    public static Car Create(string brand, string model, decimal price, int year)
    {
        var car = new Car(Guid.NewGuid(), brand, model, price, year);
        return car;
    }
}
