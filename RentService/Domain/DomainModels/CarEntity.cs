using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DomainModels
{
    // IMPROVEMENT: Create separate Car entity with car infromation and CarOffer with price etc.

    /// <summary>
    /// The car rental offer.
    /// </summary>
    public class CarEntity
    {
        public CarEntity(Vin vin, string brand, string model, long mileage, int year)
        {
            if (mileage < 0)
            {
                throw new ArgumentException($"Cannot create new Car entity. Mileage must be possitive but was: {mileage}");
            }    

            Vin = vin;
            Brand = brand;
            Model = model;
            Mileage = mileage;
            Year = year;
        }

        public Vin Vin { get; }
        public string Brand { get; }
        public string Model { get; }
        public long Mileage { get; set; } // TODO: Validate setter
        public decimal PricePerDay { get; set; } // TODO: Validate setter
        public int Year { get; }

        public override bool Equals(object obj)
        {
            return obj is CarEntity car &&
                   Vin.Equals(car.Vin) &&
                   Brand == car.Brand &&
                   Model == car.Model &&
                   Mileage == car.Mileage &&
                   Year == car.Year;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Vin, Brand, Model, Mileage, Year);
        }
    }
}
