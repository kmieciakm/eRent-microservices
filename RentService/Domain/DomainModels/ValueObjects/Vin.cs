using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DomainModels.ValueObjects
{
    /// <summary>
    /// Represents Car identifier
    /// </summary>
    public class Vin
    {
        public static int RequireVinLength { get; } = 17;
        public static string VinNormNumber { get; } = "ISO-3779";
        public string Value { get; private set; }

        private Vin() { }

        /// <summary>Creates Vin object.</summary>
        /// <remarks>Does not check all norms, only the length required !!!</remarks>
        /// <exception cref="ArgumentException">Thrown when length of given vin is wrong.</exception>
        /// <param name="vin">Vin string representation to convert from.</param>
        /// <returns>Vin object.</returns>
        public static Vin FromString(string vin)
        {
            // IMPROVEMENT: Check additional norms

            if (vin.Length != RequireVinLength)
                throw new ArgumentException($"Vin number must be {RequireVinLength} long according to {VinNormNumber}");

            return new Vin() { Value = vin };
        }
    }
}
