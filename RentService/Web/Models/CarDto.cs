using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Requests
{
    public class CarDto
    {
        public string Vin { get; }
        public string Brand { get; }
        public string Model { get; }
        public long Mileage { get; set; }
        public decimal PricePerDay { get; set; }
        public int Year { get; }
        public CarDto(CarEntity carEntity)
        {
            Vin = carEntity.Vin.Value;
            Brand = carEntity.Brand;
            Model = carEntity.Model;
            Mileage = carEntity.Mileage;
            PricePerDay = carEntity.PricePerDay;
            Year = carEntity.Year;
        }
    }
}
