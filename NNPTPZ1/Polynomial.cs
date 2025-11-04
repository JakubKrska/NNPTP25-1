using System;
using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    /// <summary>
    /// Třída reprezentující polynom s komplexními koeficienty.
    /// </summary>
    public class Polynomial
    {
        /// <summary>
        /// Seznam koeficientů polynomu, začínající od konstantního členu.
        /// </summary>
        public List<ComplexNumber> Coe { get; }

        /// <summary>
        /// Inicializuje nový prázdný polynom.
        /// </summary>
        public Polynomial() => Coe = new List<ComplexNumber>();

        /// <summary>
        /// Přidá nový koeficient k polynomu.
        /// </summary>
        /// <param name="coefficient">Koeficient, který se má přidat.</param>
        public void Add(ComplexNumber coefficient) => Coe.Add(coefficient);

        /// <summary>
        /// Vrátí derivaci tohoto polynomu.
        /// </summary>
        /// <returns>Nový polynom, který je derivací aktuálního.</returns>
        public Polynomial Derive()
        {
            var derived = new Polynomial();
            for (int i = 1; i < Coe.Count; i++)
            {
                derived.Add(Coe[i].Multiply(new ComplexNumber { Re = i }));
            }
            return derived;
        }

        /// <summary>
        /// Vyhodnotí polynom pro reálné číslo.
        /// </summary>
        /// <param name="x">Reálné číslo, pro které se má polynom vyhodnotit.</param>
        /// <returns>Komplexní výsledek výpočtu.</returns>
        public ComplexNumber Eval(double x) => Eval(new ComplexNumber { Re = x, Imaginari = 0 });

        /// <summary>
        /// Vyhodnotí polynom pro komplexní číslo.
        /// </summary>
        /// <param name="x">Komplexní číslo.</param>
        /// <returns>Komplexní výsledek výpočtu.</returns>
        public ComplexNumber Eval(ComplexNumber x)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int i = 0; i < Coe.Count; i++)
            {
                ComplexNumber term = Coe[i];
                for (int j = 0; j < i; j++)
                {
                    term = term.Multiply(x);
                }
                result = result.Add(term);
            }
            return result;
        }

        /// <summary>
        /// Vrátí textovou reprezentaci polynomu.
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Coe.Count; i++)
            {
                sb.Append(Coe[i]);
                for (int j = 0; j < i; j++)
                    sb.Append("x");

                if (i < Coe.Count - 1)
                    sb.Append(" + ");
            }
            return sb.ToString();
        }
    }
}
