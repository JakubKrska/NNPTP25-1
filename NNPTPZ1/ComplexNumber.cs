using System;

namespace NNPTPZ1.Mathematics
{
    /// <summary>
    /// Reprezentuje komplexní číslo a poskytuje základní aritmetické operace.
    /// </summary>
    public class ComplexNumber
    {
        /// <summary>
        /// Reálná složka komplexního čísla.
        /// </summary>
        public double Re { get; set; }

        /// <summary>
        /// Imaginární složka komplexního čísla.
        /// </summary>
        public float Imaginari { get; set; }

        /// <summary>
        /// Reprezentuje nulové komplexní číslo (0 + 0i).
        /// </summary>
        public static readonly ComplexNumber Zero = new ComplexNumber { Re = 0, Imaginari = 0 };

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber x)
            {
                return Math.Abs(Re - x.Re) < 1e-9 && Math.Abs(Imaginari - x.Imaginari) < 1e-9;
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Re.GetHashCode();
                hash = hash * 23 + Imaginari.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Sečte dvě komplexní čísla.
        /// </summary>
        public ComplexNumber Add(ComplexNumber b) =>
            new ComplexNumber { Re = Re + b.Re, Imaginari = Imaginari + b.Imaginari };

        /// <summary>
        /// Odečte druhé komplexní číslo od aktuálního.
        /// </summary>
        public ComplexNumber Subtract(ComplexNumber b) =>
            new ComplexNumber { Re = Re - b.Re, Imaginari = Imaginari - b.Imaginari };

        /// <summary>
        /// Vynásobí dvě komplexní čísla.
        /// </summary>
        public ComplexNumber Multiply(ComplexNumber b) =>
            new ComplexNumber
            {
                Re = Re * b.Re - Imaginari * b.Imaginari,
                Imaginari = (float)(Re * b.Imaginari + Imaginari * b.Re)
            };

        /// <summary>
        /// Vydělí aktuální číslo druhým komplexním číslem.
        /// </summary>
        public ComplexNumber Divide(ComplexNumber b)
        {
            double denom = b.Re * b.Re + b.Imaginari * b.Imaginari;
            var numerator = Multiply(new ComplexNumber { Re = b.Re, Imaginari = -b.Imaginari });

            return new ComplexNumber
            {
                Re = numerator.Re / denom,
                Imaginari = (float)(numerator.Imaginari / denom)
            };
        }

        /// <summary>
        /// Vrátí absolutní hodnotu komplexního čísla (modul).
        /// </summary>
        public double GetAbS() => Math.Sqrt(Re * Re + Imaginari * Imaginari);

        /// <summary>
        /// Vrátí argument komplexního čísla ve stupních.
        /// </summary>
        public double GetAngleInDegrees() => Math.Atan2(Imaginari, Re) * (180.0 / Math.PI);

        /// <summary>
        /// Vrátí textovou reprezentaci komplexního čísla.
        /// </summary>
        public override string ToString() => $"({Re} + {Imaginari}i)";
    }
}
