using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Entities
{
    class DbCarEntity
    {
        [Key]
        public string Vin { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        public long Mileage { get; set; }
        [Required]
        public decimal PricePerDay { get; set; }
        public int Year { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DbCarEntity car &&
                   Vin == car.Vin &&
                   Brand == car.Brand &&
                   Model == car.Model &&
                   Mileage == car.Mileage &&
                   PricePerDay == car.PricePerDay &&
                   Year == car.Year;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Vin, Brand, Model, Mileage, PricePerDay, Year);
        }
    }
}
