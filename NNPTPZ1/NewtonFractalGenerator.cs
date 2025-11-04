using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Fractals
{
    /// <summary>
    /// Generátor Newtonových fraktálů pomocí Newtonovy metody.
    /// </summary>
    public class NewtonFractalGenerator
    {
        private readonly Polynomial polynomial;
        private readonly Polynomial derivative;
        private readonly Color[] colors =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow,
            Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        /// <summary>
        /// Inicializuje generátor fraktálu pro daný polynom.
        /// </summary>
        /// <param name="polynomial">Polynom, pro který se fraktál generuje.</param>
        public NewtonFractalGenerator(Polynomial polynomial)
        {
            this.polynomial = polynomial;
            derivative = polynomial.Derive();
        }

        /// <summary>
        /// Vygeneruje bitmapu Newtonova fraktálu.
        /// </summary>
        /// <param name="width">Šířka obrázku.</param>
        /// <param name="height">Výška obrázku.</param>
        /// <param name="xmin">Minimální reálná hodnota osy X.</param>
        /// <param name="xmax">Maximální reálná hodnota osy X.</param>
        /// <param name="ymin">Minimální imaginární hodnota osy Y.</param>
        /// <param name="ymax">Maximální imaginární hodnota osy Y.</param>
        /// <returns>Bitmapa s vygenerovaným fraktálem.</returns>
        public Bitmap Generate(int width, int height, double xmin, double xmax, double ymin, double ymax)
        {
            var bitmap = new Bitmap(width, height);
            var roots = new List<ComplexNumber>();

            double xstep = (xmax - xmin) / width;
            double ystep = (ymax - ymin) / height;

            for (int px = 0; px < width; px++)
            {
                for (int py = 0; py < height; py++)
                {
                    double x = xmin + px * xstep;
                    double y = ymin + py * ystep;

                    ComplexNumber z = new ComplexNumber { Re = x, Imaginari = (float)y };
                    z = IterateNewton(z, 30);

                    int rootId = GetRootId(roots, z);
                    Color color = Colorize(rootId, roots.Count, 5);
                    bitmap.SetPixel(px, py, color);
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Provádí iterace Newtonovy metody pro nalezení kořene.
        /// </summary>
        /// <param name="z">Počáteční hodnota komplexního čísla.</param>
        /// <param name="maxIterations">Maximální počet iterací.</param>
        /// <returns>Odhad nalezeného kořene.</returns>
        private ComplexNumber IterateNewton(ComplexNumber z, int maxIterations)
        {
            for (int i = 0; i < maxIterations; i++)
            {
                var diff = polynomial.Eval(z).Divide(derivative.Eval(z));
                z = z.Subtract(diff);

                if (diff.GetAbS() < 1e-6)
                    break;
            }
            return z;
        }

        /// <summary>
        /// Vrací ID existujícího nebo nového kořene v seznamu.
        /// </summary>
        private int GetRootId(List<ComplexNumber> roots, ComplexNumber candidate)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if (candidate.Subtract(roots[i]).GetAbS() < 0.01)
                    return i;
            }

            roots.Add(candidate);
            return roots.Count - 1;
        }

        /// <summary>
        /// Vrací barvu pro daný kořen podle jeho ID.
        /// </summary>
        private Color Colorize(int rootId, int totalRoots, int fadeStep)
        {
            var baseColor = colors[rootId % colors.Length];
            int fade = Math.Min(255, fadeStep * rootId);
            return Color.FromArgb(
                Math.Max(0, baseColor.R - fade),
                Math.Max(0, baseColor.G - fade),
                Math.Max(0, baseColor.B - fade)
            );
        }
    }
}
